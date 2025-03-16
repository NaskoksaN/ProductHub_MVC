using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductHub.DataAccess.Entities;
using ProductHub.Utility.ViewModels.Product;
using ProductHub.Utility.Interface;
using ProductHubWeb.Areas.Customer.Controllers;

using static ProductHub.Models.Constants.MessageConstants;
using static ProductHub.Models.Constants.SDRoles;

namespace ProductHubWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role_Admin)]
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
            IEnumerable<Product> productList = await unitOfWork.
                    ProductService.GetAllAsync(includeProperties:"Category");

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
        public async Task<IActionResult> Upsert(ProductVM productVM, IFormFile? file)
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
                productVM.CategoryList = CategoryList;
                return View(productVM);
            }
            else
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() 
                                    + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Form.ImgUrl))
                    {
                        var oldImagePath = 
                            Path.Combine(wwwRootPath,productVM.Form.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (FileStream fileStream = new (Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Form.ImgUrl = @"\images\product\" + fileName;
                }
                if(productVM.Form.Id==null || productVM.Form.Id==0)
                {
                    await unitOfWork.ProductService.AddAsync(new Product()
                    {
                        Name = productVM.Form.Name,
                        Description = productVM.Form.Description,
                        Price = productVM.Form.Price,
                        Amount = productVM.Form.Amount,
                        MeasurementUnit = productVM.Form.MeasurementUnit,
                        ImgUrl = productVM.Form.ImgUrl,
                        CategoryId = productVM.Form.CategoryId

                    });
                }
                else
                {
                    

                    unitOfWork.ProductService.Update(productVM.Form);
                }
                
                await unitOfWork.SaveAsync();

                TempData["success"] = ItemCreated;

                return RedirectToAction(nameof(Index));
            }
            
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Product> productList = await unitOfWork.
                    ProductService.GetAllAsync(includeProperties: "Category");

            return Json(new {data= productList });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Product productToBeDeleted = await unitOfWork
                .ProductService.GetAsync (c => c.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new {success=false, message="Error while deleting"});
            }
            var oldImagePath =
                            Path.Combine(webHostEnvironment.WebRootPath,
                            productToBeDeleted.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            unitOfWork.ProductService.Remove(productToBeDeleted);
            await unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }

}
