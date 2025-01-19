using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductHub.DataAccess.Entities;
using ProductHub.Models.ViewModels.Company;
using ProductHub.Utility.Interface;
using ProductHubWeb.Areas.Customer.Controllers;

using static ProductHub.Models.Constants.MessageConstants;
using static ProductHub.Models.Constants.SDRoles;

namespace ProductHubWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CompanyController(ILogger<HomeController> logger, 
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
            IEnumerable<Company> companyList = await unitOfWork.
                    CompanyService.GetAllAsync();

            return View(companyList);
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
            CompanyFormModel companyFormModel = new ();
          
            if (id==null || id == 0)
            {
                return View(companyFormModel);
            }
            else
            {
                Company? companyFromDb = await unitOfWork.CompanyService.GetAsync(c => c.Id == id);
                companyFormModel.Id= companyFromDb.Id;
                companyFormModel.Name = companyFromDb.Name;
                companyFormModel.VAT = companyFromDb.VAT;
                companyFormModel.City = companyFromDb.City;
                companyFormModel.PostalCode = companyFromDb.PostalCode;
                companyFormModel.StreetAddress = companyFromDb.StreetAddress;
                companyFormModel.PhoneNumber = companyFromDb.PhoneNumber;

                return View(companyFormModel);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(CompanyFormModel companyFormModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = ErrorCreated;

                return View(companyFormModel);
            }
            else
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
               
                if(companyFormModel.Id==null || companyFormModel.Id==0)
                {
                    await unitOfWork.CompanyService.AddAsync(new Company()
                    {
                        Name = companyFormModel.Name,
                        VAT = companyFormModel.VAT,
                        City = companyFormModel.City,
                        PostalCode = companyFormModel.PostalCode,
                        StreetAddress = companyFormModel.StreetAddress,
                        PhoneNumber = companyFormModel.PhoneNumber,
                    });
                }
                else
                {
                    Company? companyFromDb = await unitOfWork.CompanyService.GetAsync(c => c.Id == companyFormModel.Id);
                   
                    companyFromDb.Name = companyFormModel.Name;
                    companyFromDb.VAT = companyFormModel.VAT;
                    companyFromDb.City = companyFormModel.City;
                    companyFromDb.PostalCode = companyFormModel.PostalCode;
                    companyFromDb.StreetAddress = companyFormModel.StreetAddress;
                    companyFromDb.PhoneNumber = companyFormModel.PhoneNumber;
                    unitOfWork.CompanyService.Update(companyFromDb);
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
            IEnumerable<Company> companyList = await unitOfWork.
                    CompanyService.GetAllAsync();

            return Json(new {data= companyList });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Company companyToBeDeleted = await unitOfWork
                .CompanyService.GetAsync (c => c.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new {success=false, message="Error while deleting"});
            }

            unitOfWork.CompanyService.Remove(companyToBeDeleted);
            await unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }

}
