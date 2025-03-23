using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductHub.DataAccess.Entities;
using ProductHub.Models;
using ProductHub.Models.Constants;
using ProductHub.Utility.Interface;
using ProductHub.Utility.Service;
using ProductHub.Utility.ViewModels.ShopingCart;
using System.Diagnostics;
using System.Security.Claims;

using static ProductHub.Models.Constants.MessageConstants;

namespace ProductHubWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger,
                        IUnitOfWork _unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = _unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            

            IEnumerable<Product> productList = await unitOfWork
                                       .ProductService
                                       .GetAllAsync(includeProperties:"Category");

            return View(productList);
        }

        public async Task<IActionResult> Details(int productId)
        {

            ShopingCartFormModel cart = new()
            {
                Product = await unitOfWork
                                       .ProductService
                                       .GetAsync(u => u.Id == productId, includeProperties: "Category"),
                Count=1,
                ProductId=productId
            };

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(ShopingCartFormModel shopingCartFormModel)
        {
            var claimsIdentity = (ClaimsIdentity?)User?.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShopingCart cartFromDb = await unitOfWork
                    .ShopingCartService
                    .GetAsync(u => u.ApplicationUserId == userId &&
                                  u.ProductId == shopingCartFormModel.ProductId);

            if (cartFromDb != null)
            {
                cartFromDb.Count+=shopingCartFormModel.Count;
                unitOfWork.ShopingCartService.Update(cartFromDb);
                await unitOfWork.SaveAsync();
            }
            else
            {
                ShopingCart shopingCart = new()
                {
                    ProductId = shopingCartFormModel.ProductId,
                    ApplicationUserId = userId,
                    Count = shopingCartFormModel.Count,
                    
                };
                await unitOfWork.ShopingCartService.AddAsync(shopingCart);
                
                await unitOfWork.SaveAsync();

                int count = unitOfWork.ShopingCartService.GetAllAsync(u => u.ApplicationUserId == userId)
                        .GetAwaiter().GetResult().Count();
                HttpContext.Session.SetInt32(SessionConstants.SessionShoppingCart,
                        count);
                

            }

            TempData["success"] = CartSuccessMsg;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
