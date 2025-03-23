using Microsoft.AspNetCore.Mvc;
using ProductHub.Models.Constants;
using ProductHub.Utility.Interface;
using System.Security.Claims;

namespace ProductHubWeb.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity?)User?.Identity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(SessionConstants.SessionShoppingCart) == null)
                {
                    var cartCount = (await unitOfWork.ShopingCartService
                                   .GetAllAsync(u => u.ApplicationUserId == claim.Value)).Count();
                    HttpContext.Session.SetInt32(SessionConstants.SessionShoppingCart, cartCount);
                }
                return View(HttpContext.Session.GetInt32(SessionConstants.SessionShoppingCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
