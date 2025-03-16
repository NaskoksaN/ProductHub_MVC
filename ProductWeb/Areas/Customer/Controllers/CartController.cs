using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.DataAccess.Entities;
using ProductHub.Models.Constants;
using ProductHub.Utility.Interface;
using ProductHub.Utility.ViewModels.ShopingCart;
using Stripe.Checkout;
using System.Security.Claims;

namespace ProductHubWeb.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class CartController : Controller
    {

        private readonly IUnitOfWork unitOfWork;

        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM = new()
            {
                ShopingCartList = await unitOfWork
                                        .ShopingCartService
                                        .GetAllAsync(u => u.ApplicationUserId == userId,
                                        includeProperties: "Product"),
                OrderHeader = new()
            };
            foreach (var cart in ShoppingCartVM.ShopingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return base.View(ShoppingCartVM);
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM = new()
            {
                ShopingCartList = await unitOfWork
                                        .ShopingCartService
                                        .GetAllAsync(u => u.ApplicationUserId == userId,
                                        includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = await unitOfWork
                                                         .ApplicationUserRepository.GetAsync(u => u.Id == userId);
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;

            foreach (var cart in ShoppingCartVM.ShopingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM.ShopingCartList = await unitOfWork
                                        .ShopingCartService
                                        .GetAllAsync(u => u.ApplicationUserId == userId,
                                        includeProperties: "Product");
            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

            ApplicationUser applicationUser = await unitOfWork
                                                    .ApplicationUserRepository.GetAsync(u => u.Id == userId);

            foreach (var cart in ShoppingCartVM.ShopingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                // regular user, we need payment
                ShoppingCartVM.OrderHeader.PaymentStatus = StatusConstants.PaymentStatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = StatusConstants.StatusPending;
            }
            else
            {
                //company user
                ShoppingCartVM.OrderHeader.PaymentStatus = StatusConstants.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = StatusConstants.StatusApproved;
            }

            await unitOfWork.OrderHeaderService.AddAsync(ShoppingCartVM.OrderHeader);
            await unitOfWork.SaveAsync();

            foreach (var cart in ShoppingCartVM.ShopingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count,
                };
                await unitOfWork.OrderDetailService.AddAsync(orderDetail);
                await unitOfWork.SaveAsync();
            }
            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                // regular user, we need payment
                //stripe log
                var domainUrl = "https://localhost:7210/";
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = domainUrl+$"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                    CancelUrl = domainUrl+ "customer/cart/Index",
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach(var item in ShoppingCartVM.ShopingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount=(long)(item.Price*100),
                            Currency="eur",
                            ProductData=new SessionLineItemPriceDataProductDataOptions
                            {
                                Name=item.Product.Name,
                            }
                        },
                        Quantity=item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new Stripe.Checkout.SessionService();
                Session session = service.Create(options);

                await unitOfWork.OrderHeaderService.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                await unitOfWork.SaveAsync();

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id });
        }

        public async Task<IActionResult> OrderConfirmation(int id)
        {
            OrderHeader orderHeader = await unitOfWork.OrderHeaderService.GetAsync(u => u.Id == id, includeProperties:"ApplicationUser");
            
            if(orderHeader.PaymentStatus!= StatusConstants.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SeesionId);
                if(session.PaymentStatus.ToLower()== "paid")
                {
                    await unitOfWork.OrderHeaderService.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
                    await unitOfWork.OrderHeaderService.UpdateStatus(id, StatusConstants.StatusApproved, StatusConstants.PaymentStatusApproved);
                    await unitOfWork.SaveAsync();
                }
               
            }
            IEnumerable<ShopingCart> shopiningCarts = await unitOfWork
                                            .ShopingCartService
                                            .GetAllAsync(u => u.ApplicationUserId == orderHeader.ApplicationUserId) ;
            unitOfWork.ShopingCartService.RemoveRange(shopiningCarts);
            await unitOfWork.SaveAsync();

            return View(id);
        }
        public async Task<IActionResult> PlusProduct(int cartId)
        {
            var cartFormDb = await unitOfWork.ShopingCartService.GetAsync(u => u.Id == cartId);
            cartFormDb.Count += 1;
            unitOfWork.ShopingCartService.Update(cartFormDb);
            await unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MinusProduct(int cartId)
        {
            var cartFormDb = await unitOfWork.ShopingCartService.GetAsync(u => u.Id == cartId);
            if (cartFormDb.Count <= 1)
            {
                unitOfWork.ShopingCartService.Remove(cartFormDb);
            }
            else
            {
                cartFormDb.Count -= 1;
                unitOfWork.ShopingCartService.Update(cartFormDb);
            }

            await unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveProduct(int cartId)
        {
            var cartFormDb = await unitOfWork.ShopingCartService.GetAsync(u => u.Id == cartId);
            unitOfWork.ShopingCartService.Remove(cartFormDb);
            await unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
