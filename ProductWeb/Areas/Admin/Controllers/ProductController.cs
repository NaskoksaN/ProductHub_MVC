using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductHub.DataAccess.Entities;
using ProductHub.Models.ViewModels.Product;
using ProductHub.Utility.Interface;
using ProductHubWeb.Areas.Customer.Controllers;

using static ProductHub.Models.Constants.MessageConstants;

namespace ProductHubWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(ILogger<HomeController> logger, 
                IUnitOfWork _unitOfWork,
                IWebHostEnvironment _webHostEnvironment)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> productList = await unitOfWork.ProductService.GetAllAsync();

            return View(productList);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Category> categoryList = await unitOfWork.CategoryService.GetAllAsync();
            IEnumerable<SelectListItem> CategoryList = categoryList
                        .Select(u => new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.Id.ToString()
                        });
            ProductVM productVM = new ProductVM()
            {
                CategoryList = CategoryList,
                Form = new ProductFormModel()
            };
          
            //pass via ViewBag, TempData => ViewBag.CategoryList = CategoryList;
            if (id==null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                Product? productFromDb = await unitOfWork.ProductService.GetAsync(c => c.Id == id);
                productVM.Form.Id= productFromDb.Id;
                productVM.Form.Name = productFromDb.Name;
                productVM.Form.Description = productFromDb.Description;
                productVM.Form.Price = productFromDb.Price;
                productVM.Form.Amount = productFromDb.Amount;
                productVM.Form.CategoryId = productFromDb.CategoryId;
                productVM.Form.MeasurementUnit = productFromDb.MeasurementUnit;
                productVM.Form.ImgUrl = productFromDb.ImgUrl;

                return View(productVM);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductVM obj, IFormFile? file)
        {
            
            if (!ModelState.IsValid)
            {
                TempData["error"] = ErrorCreated;

                IEnumerable<Category> categoryList = await unitOfWork.CategoryService.GetAllAsync();
                IEnumerable<SelectListItem> CategoryList = categoryList
                            .Select(u => new SelectListItem
                            {
                                Text = u.Name,
                                Value = u.Id.ToString()
                            });
                obj.CategoryList = CategoryList;
                return View(obj);
            }
            else
            {
                await unitOfWork.ProductService.AddAsync(new Product()
                {
                    Name = obj.Form.Name,
                    Description = obj.Form.Description,
                    Price = obj.Form.Price,
                    Amount = obj.Form.Amount,
                    MeasurementUnit = obj.Form.MeasurementUnit,
                    ImgUrl = obj.Form.ImgUrl,
                    CategoryId = obj.Form.CategoryId

                });
                await unitOfWork.SaveAsync();

                TempData["success"] = ItemCreated;

                return RedirectToAction(nameof(Index));
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            Product? productFromDb = await unitOfWork.ProductService.GetAsync(c => c.Id == id);
            if (productFromDb == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Product? productFromDb = await unitOfWork.ProductService.GetAsync(c => c.Id == id);

            if (productFromDb == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }

            unitOfWork.ProductService.Remove(productFromDb);
            await unitOfWork.SaveAsync();
            TempData["success"] = ItemDelete;

            return RedirectToAction(nameof(Index));
        }
    }

}
