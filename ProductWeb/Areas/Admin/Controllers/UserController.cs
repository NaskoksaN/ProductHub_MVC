using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductHub.DataAccess.Entities;
using ProductHub.Utility.Interface;
using ProductHub.Utility.Service;
using ProductHub.Utility.ViewModels.User;
using ProductHubWeb.Areas.Customer.Controllers;
using static ProductHub.Models.Constants.SDRoles;

namespace ProductHubWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role_Admin)]
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(ILogger<HomeController> logger, 
                IUnitOfWork _unitOfWork,
                UserManager<IdentityUser> _userManager,
                RoleManager<IdentityRole> _roleManager)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
            userManager= _userManager;
            roleManager= _roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            

            return View();
        }

        public async Task<IActionResult> RoleManagement(string userId)
        {
           
            return View();
        }
        

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ApplicationUser> userList = await unitOfWork
                                            .ApplicationUserRepository
                                            .GetAllAsync(includeProperties:"Company");

            
            foreach (var user in userList)
            {
                var roles = await userManager.GetRolesAsync(user);
                user.Role = roles.FirstOrDefault();

                if (user.Company==null)
                {
                    user.Company = new() { Name = "" };
                }
            }
            return Json(new {data= userList });
        }

        [HttpPost]
        public async Task<IActionResult> LockUnlock([FromBody]string id)
        {
            var objFromDb = await unitOfWork
                                .ApplicationUserRepository
                                .GetAsync(u=> u.Id==id);
            if(objFromDb == null)
            {
                return Json(new {success=false,message="Error while Locking/Unlocking" });
            }

            if(objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd> DateTime.Now)
            {
                objFromDb.LockoutEnd= DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }

            unitOfWork.ApplicationUserRepository.Update(objFromDb);

            await unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Operation is Successful" });
        }

        #endregion
    }

}
