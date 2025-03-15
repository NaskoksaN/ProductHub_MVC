using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Utility.Interface;
using ProductHub.Utility.ViewModels.ShopingCart;
using System.Security.Claims;

namespace ProductHubWeb.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class CartController : Controller
    {

        private readonly IUnitOfWork unitOfWork;

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
                                        .GetAllAsync(u=> u.ApplicationUserId==userId ,
                                        includeProperties:"Product"),
            };
            foreach (var cart in ShoppingCartVM.ShopingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartVM.OrderTotal += (cart.Price * cart.Count);
            }
            return base.View(ShoppingCartVM);
        }

        public async Task<IActionResult> Summary()
        {
            return View();
        }

        public async Task<IActionResult> PlusProduct(int cartId)
        {
            var cartFormDb = await unitOfWork.ShopingCartService.GetAsync(u=> u.Id==cartId);
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
