using Microsoft.AspNetCore.Mvc;
using ProductHub.DataAccess.Entities;
using ProductHub.Models;
using ProductHub.Utility.Interface;
using System.Diagnostics;

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
            Product product = await unitOfWork
                                       .ProductService
                                       .GetAsync(u=> u.Id== productId, includeProperties: "Category");

            return View(product);
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
