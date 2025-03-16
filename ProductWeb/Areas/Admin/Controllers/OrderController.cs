using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.DataAccess.Entities;
using ProductHub.Models.Constants;
using ProductHub.Utility.Interface;
using ProductHub.Utility.ViewModels.Order;
using ProductHubWeb.Areas.Customer.Controllers;
using System.Security.Claims;

namespace ProductHubWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(ILogger<HomeController> logger,
                IUnitOfWork _unitOfWork)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int orderId)
        {
            OrderVM  = new()
            {
                OrderHeader = await unitOfWork
                                   .OrderHeaderService
                                   .GetAsync(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = await unitOfWork
                                     .OrderDetailService
                                     .GetAllAsync(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            }; 


            return View(OrderVM);
        }

        [HttpPost]
        [Authorize(Roles =SDRoles.Role_Admin+","+SDRoles.Role_Employee)]
        public async Task<IActionResult> UpdateOrderDetail()
        {
            var orderHeaderFormDb = await unitOfWork
                                        .OrderHeaderService
                                        .GetAsync(u => u.Id == OrderVM.OrderHeader.Id);

            orderHeaderFormDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFormDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFormDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFormDb.City = OrderVM.OrderHeader.City;
            orderHeaderFormDb.PostalCode = OrderVM.OrderHeader.PostalCode;
            if(!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFormDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFormDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            unitOfWork.OrderHeaderService.Update(orderHeaderFormDb);
            await unitOfWork.SaveAsync();

            TempData["Success"] = "Order Details Updated Successfully";

            return RedirectToAction(nameof(Details), new {orderId=orderHeaderFormDb.Id});
        }

        [HttpPost]
        [Authorize(Roles = SDRoles.Role_Admin + "," + SDRoles.Role_Employee)]
        public async Task<IActionResult> StartProcessing()
        {
            await unitOfWork
                    .OrderHeaderService
                    .UpdateStatus(OrderVM.OrderHeader.Id, StatusConstants.StatusInProcess);
            await unitOfWork.SaveAsync();

            TempData["Success"] = "Order Details Updated Successfully";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }



        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaders;

            if(User.IsInRole(SDRoles.Role_Admin) || User.IsInRole(SDRoles.Role_Employee))
            {
                objOrderHeaders = await unitOfWork.
                    OrderHeaderService.GetAllAsync(includeProperties: "ApplicationUser");
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity?)User.Identity;
                var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                objOrderHeaders = await unitOfWork
                                      .OrderHeaderService
                                      .GetAllAsync(u=>u.ApplicationUserId==userId, includeProperties:"ApplicationUser");
            }

                switch (status)
                {
                    case "pending":
                        objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == StatusConstants.PaymentStatusPending);
                        break;
                    case "inprocess":
                        objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StatusConstants.StatusInProcess);
                        break;
                    case "completed":
                        objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StatusConstants.StatusShipped);
                        break;
                    case "approved":
                        objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StatusConstants.PaymentStatusApproved);
                        break;
                    default:
                        break;
                }

            return Json(new { data = objOrderHeaders });
        }

        #endregion
    }
}
