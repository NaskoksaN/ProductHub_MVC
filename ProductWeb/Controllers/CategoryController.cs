using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.DataAccess;
using Product.Models.DataModels;
using Product.Models.ViewModels.Catgeroy;

using static Product.Models.Constants.MessageConstants;


namespace ProductWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        public CategoryController(ILogger<HomeController> logger, ApplicationDbContext _db)
        {
            _logger = logger;
            db=_db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categoryList = await db.Categories.ToListAsync();
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
            await db.Categories.AddAsync( new Category()
            {
                Name = obj.Name,
                DisplayOrder = obj.DisplayOrder,
            });
            await db.SaveChangesAsync();

            TempData["success"]=ItemCreated;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            Category? categoryFromDb = db.Categories.FirstOrDefault(c => c.Id == id);
            if(categoryFromDb == null)
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
            Category? categoryFromDb = db.Categories.FirstOrDefault(c => c.Id == obj.Id);
            if (categoryFromDb == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            categoryFromDb.Name = obj.Name;
            categoryFromDb.DisplayOrder = obj.DisplayOrder;
            db.Categories.Update(categoryFromDb);
            await db.SaveChangesAsync();

            TempData["success"] = ItemDelete;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            Category? categoryFromDb = db.Categories.FirstOrDefault(c => c.Id == id);
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
            Category? categoryFromDb = db.Categories.FirstOrDefault(c => c.Id == id);
                       
            if (categoryFromDb == null)
            {
                TempData["error"] = ItemNotFound;
                return NotFound();
            }
            
            db.Categories.Remove(categoryFromDb);
            await db.SaveChangesAsync();
            TempData["success"] = ItemDelete;

            return RedirectToAction(nameof(Index));
        }
    }
}
