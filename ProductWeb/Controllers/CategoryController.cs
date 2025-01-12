using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.DataAccess;
using Product.DataAccess.Entities;
using Product.Models.ViewModels.Catgeroy;
using Product.Utility.Interface;
using static Product.Models.Constants.MessageConstants;


namespace ProductWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService categoryService;
        public CategoryController(ILogger<HomeController> logger, ICategoryService _categoryService)
        {
            _logger = logger;
            categoryService = _categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categoryList = await categoryService.GetAllAsync();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryFormModel obj)
        {
            if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == obj.DisplayOrder.ToString().ToLower())
            {
                ModelState.AddModelError("name", NameAndDiplayorderMatch);
            }
            if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is not valid input");
            }
            if (!ModelState.IsValid)
            {
                TempData["error"] = ErrorCreated;
                return View(obj);
            }
            await categoryService.AddAsync( new Category()
            {
                Name = obj.Name,
                DisplayOrder = obj.DisplayOrder,
            });
            await categoryService.SaveAsync();

            TempData["success"]=ItemCreated;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || id == 0)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            Category? categoryFromcategoryService = await categoryService.GetAsync(c => c.Id == id);
            if(categoryFromcategoryService == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            return View(new CategoryFormModel()
            {
                Id = categoryFromcategoryService.Id,
                Name = categoryFromcategoryService.Name,
                DisplayOrder = categoryFromcategoryService.DisplayOrder
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormModel obj)
        {
            if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == obj.DisplayOrder.ToString().ToLower())
            {
                ModelState.AddModelError("name", NameAndDiplayorderMatch);
            }
            if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is not valid input");
            }
            if (!ModelState.IsValid)
            {
                TempData["error"] = ErrorUpdate;
                return View(obj);
            }
            Category? categoryFromcategoryService = await categoryService.GetAsync(c => c.Id == obj.Id);
            if (categoryFromcategoryService == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            categoryFromcategoryService.Name = obj.Name;
            categoryFromcategoryService.DisplayOrder = obj.DisplayOrder;
            categoryService.Update(categoryFromcategoryService);
            await categoryService.SaveAsync();

            TempData["success"] = ItemDelete;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            Category? categoryFromcategoryService = await categoryService.GetAsync(c => c.Id == id);
            if (categoryFromcategoryService == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            return View(categoryFromcategoryService);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Category? categoryFromcategoryService = await categoryService.GetAsync(c => c.Id == id);
                       
            if (categoryFromcategoryService == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            
            categoryService.Remove(categoryFromcategoryService);
            await categoryService.SaveAsync();
            TempData["success"] = ItemDelete;

            return RedirectToAction(nameof(Index));
        }
    }
}
