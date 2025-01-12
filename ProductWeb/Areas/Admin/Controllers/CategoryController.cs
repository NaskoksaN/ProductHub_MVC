using Microsoft.AspNetCore.Mvc;
using ProductHub.DataAccess.Entities;
using ProductHub.Models.ViewModels.Catgeroy;
using ProductHub.Utility.Interface;
using ProductHubWeb.Areas.Customer.Controllers;
using static ProductHub.Models.Constants.MessageConstants;


namespace ProductHubWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public CategoryController(ILogger<HomeController> logger, IUnitOfWork _unitOfWork)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categoryList = await unitOfWork.CategoryService.GetAllAsync();
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
            await unitOfWork.CategoryService.AddAsync(new Category()
            {
                Name = obj.Name,
                DisplayOrder = obj.DisplayOrder,
            });
            await unitOfWork.SaveAsync();

            TempData["success"] = ItemCreated;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            Category? categoryFromDb = await unitOfWork.CategoryService.GetAsync(c => c.Id == id);
            if (categoryFromDb == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            return View(new CategoryFormModel()
            {
                Id = categoryFromDb.Id,
                Name = categoryFromDb.Name,
                DisplayOrder = categoryFromDb.DisplayOrder
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
            Category? categoryFromDb = await unitOfWork.CategoryService.GetAsync(c => c.Id == obj.Id);
            if (categoryFromDb == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            categoryFromDb.Name = obj.Name;
            categoryFromDb.DisplayOrder = obj.DisplayOrder;
            unitOfWork.CategoryService.Update(categoryFromDb);
            await unitOfWork.SaveAsync();

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
            Category? categoryFromDb = await unitOfWork.CategoryService.GetAsync(c => c.Id == id);
            if (categoryFromDb == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Category? categoryFromDb = await unitOfWork.CategoryService.GetAsync(c => c.Id == id);

            if (categoryFromDb == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }

            unitOfWork.CategoryService.Remove(categoryFromDb);
            await unitOfWork.SaveAsync();
            TempData["success"] = ItemDelete;

            return RedirectToAction(nameof(Index));
        }
    }
}
