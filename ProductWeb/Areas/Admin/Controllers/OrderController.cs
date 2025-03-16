using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.DataAccess.Entities;
using ProductHub.Models.Constants;
using ProductHub.Utility.Interface;
using ProductHub.Utility.ViewModels.Order;
using ProductHubWeb.Areas.Customer.Controllers;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace ProductHubWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(ILogger<HomeController> logger,
                IUnitOfWork _unitOfWork)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int orderId)
        {
            OrderVM  = new()
            {
                OrderHeader = await unitOfWork
                                   .OrderHeaderService
                                   .GetAsync(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = await unitOfWork
                                     .OrderDetailService
                                     .GetAllAsync(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            }; 


            return View(OrderVM);
        }

        [HttpPost]
        [Authorize(Roles =SDRoles.Role_Admin+","+SDRoles.Role_Employee)]
        public async Task<IActionResult> UpdateOrderDetail()
        {
            var orderHeaderFormDb = await unitOfWork
                                        .OrderHeaderService
                                        .GetAsync(u => u.Id == OrderVM.OrderHeader.Id);

            orderHeaderFormDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFormDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFormDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFormDb.City = OrderVM.OrderHeader.City;
            orderHeaderFormDb.PostalCode = OrderVM.OrderHeader.PostalCode;
            if(!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFormDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFormDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            unitOfWork.OrderHeaderService.Update(orderHeaderFormDb);
            await unitOfWork.SaveAsync();

            TempData["Success"] = "Order Details Updated Successfully";

            return RedirectToAction(nameof(Details), new {orderId=orderHeaderFormDb.Id});
        }

        [HttpPost]
        [Authorize(Roles = SDRoles.Role_Admin + "," + SDRoles.Role_Employee)]
        public async Task<IActionResult> StartProcessing()
        {
            await unitOfWork
                    .OrderHeaderService
                    .UpdateStatus(OrderVM.OrderHeader.Id, StatusConstants.StatusInProcess);
            await unitOfWork.SaveAsync();

            TempData["Success"] = "Order Details Updated Successfully";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SDRoles.Role_Admin + "," + SDRoles.Role_Employee)]
        public async Task<IActionResult> ShipOrder()
        {
            var orderHeader = await unitOfWork
                                    .OrderHeaderService
                                    .GetAsync(u=>u.Id==OrderVM.OrderHeader.Id);
            orderHeader.TrackingNumber= OrderVM.OrderHeader.TrackingNumber;
            orderHeader.Carrier=OrderVM.OrderHeader.Carrier;
            orderHeader.OrderStatus= OrderVM.OrderHeader.OrderStatus;
            orderHeader.ShippingDate=DateTime.Now;
            if(orderHeader.PaymentStatus==StatusConstants.PaymentStatusDelayedPayment)
            {
                orderHeader.PaymentDueDate=DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }

            unitOfWork.OrderHeaderService.Update(orderHeader);
            await unitOfWork.SaveAsync();

            await unitOfWork
                    .OrderHeaderService
                    .UpdateStatus(OrderVM.OrderHeader.Id, StatusConstants.StatusShipped);
            await unitOfWork.SaveAsync();

            TempData["Success"] = "Order Details Ship Successfully";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SDRoles.Role_Admin + "," + SDRoles.Role_Employee)]
        public async Task<IActionResult> CancelOrder()
        {
            var orderHeader = await unitOfWork
                                    .OrderHeaderService
                                    .GetAsync(u => u.Id == OrderVM.OrderHeader.Id);

            if (orderHeader.PaymentStatus == StatusConstants.PaymentStatusApproved)
            {
                //give refund via stripe to customer
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId,
                };
                var service = new RefundService();
                //becouse os stripe error
                // Refund refund = service.Create(options);

                await unitOfWork
                      .OrderHeaderService
                      .UpdateStatus(orderHeader.Id, StatusConstants.StatusCancelled, StatusConstants.StatusRefunded);
                
            }
            else
            {
                await unitOfWork
                      .OrderHeaderService
                      .UpdateStatus(orderHeader.Id, StatusConstants.StatusCancelled, StatusConstants.StatusCancelled);
            }

            await unitOfWork.SaveAsync();

            TempData["Success"] = "Order Canceled Successfully";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });

            
        }

        [ActionName("Details")]
        [HttpPost]
        public async Task<IActionResult> Details_PAY_NOW()
        {

            OrderVM.OrderHeader = await unitOfWork
                            .OrderHeaderService
                            .GetAsync(u => u.Id == OrderVM.OrderHeader.Id, includeProperties: "ApplicationUser");
            OrderVM.OrderDetail = await unitOfWork
                            .OrderDetailService
                            .GetAllAsync(u => u.OrderHeaderId == OrderVM.OrderHeader.Id, includeProperties: "Product");
            //stripe logic
            var domainUrl = "https://localhost:7210/";
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domainUrl + $"admin/order/PaymentConfirmation?orderHeaderId={OrderVM.OrderHeader.Id}",
                CancelUrl = domainUrl + $"admin/order/details?orderId={OrderVM.OrderHeader.Id}",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in OrderVM.OrderDetail)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new Stripe.Checkout.SessionService();
            Session session = service.Create(options);

            await unitOfWork.OrderHeaderService.UpdateStripePaymentId(OrderVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            await unitOfWork.SaveAsync();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }

        public async Task<IActionResult> PaymentConfirmation(int orderHeaderId)
        {
            OrderHeader orderHeader = await unitOfWork
                            .OrderHeaderService
                            .GetAsync(u => u.Id == orderHeaderId, includeProperties: "ApplicationUser");

            if (orderHeader.PaymentStatus == StatusConstants.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SeesionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    await unitOfWork.OrderHeaderService.UpdateStripePaymentId(orderHeaderId, session.Id, session.PaymentIntentId);
                    await unitOfWork.OrderHeaderService.UpdateStatus(orderHeaderId, orderHeader.OrderStatus, StatusConstants.PaymentStatusApproved);
                    await unitOfWork.SaveAsync();
                }

            }

            return View(orderHeaderId);
        }



        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaders;

            if(User.IsInRole(SDRoles.Role_Admin) || User.IsInRole(SDRoles.Role_Employee))
            {
                objOrderHeaders = await unitOfWork.
                    OrderHeaderService.GetAllAsync(includeProperties: "ApplicationUser");
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity?)User.Identity;
                var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                objOrderHeaders = await unitOfWork
                                      .OrderHeaderService
                                      .GetAllAsync(u=>u.ApplicationUserId==userId, includeProperties:"ApplicationUser");
            }

                switch (status)
                {
                    case "pending":
                        objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == StatusConstants.PaymentStatusPending);
                        break;
                    case "inprocess":
                        objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StatusConstants.StatusInProcess);
                        break;
                    case "completed":
                        objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StatusConstants.StatusShipped);
                        break;
                    case "approved":
                        objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StatusConstants.PaymentStatusApproved);
                        break;
                    default:
                        break;
                }

            return Json(new { data = objOrderHeaders });
        }

        #endregion
    }
}
