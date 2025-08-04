using GarnetAccounting.Areas.AvaRasta.Dto;
using GarnetAccounting.Areas.AvaRasta.Interfaces;
using GarnetAccounting.Areas.AvaRasta.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.AvaRasta.Controllers
{
    [Area("AvaRasta")]
    public class CustomerLicenseController : Controller
    {
        private readonly ICostomerService _customerService;
        private readonly ILicenseService _licenseService;

        public CustomerLicenseController(ICostomerService customerService, ILicenseService licenseService)
        {
            _customerService = customerService;
            _licenseService = licenseService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var customers = await _customerService.GetCustomers("").ToListAsync();
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int customerId)
        {
            var customer = await _customerService.GetVmCustomerByIdAsync(customerId);
            var customerUsers = await _customerService.GetCustomerUsersAsync(customerId);
            var licenses = await _licenseService.GetLicensesByCustomerIdAsync(customerId);

            var model = new CustomerManagementViewModel
            {
                Customer = customer,
                CustomerUsers = customerUsers,
                Licenses = licenses,
                NewLicense = new AddLicenseDto { CustomerId = customerId }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLicense(CustomerManagementViewModel model)
        {
            clsResult result = new clsResult();
            if (ModelState.IsValid)
            {
                result = await _licenseService.AddLicenseAsync(model.NewLicense);
                if (result.Success)
                {
                    result.returnUrl = Url.Action("Index", new { customerId = model.NewLicense.CustomerId });
                    result.updateType = 1;
                    return Json(result);
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            model.Customer = await _customerService.GetVmCustomerByIdAsync(model.NewLicense.CustomerId);
            model.CustomerUsers = await _customerService.GetCustomerUsersAsync(model.NewLicense.CustomerId);
            model.Licenses = await _licenseService.GetLicensesByCustomerIdAsync(model.NewLicense.CustomerId);

            result.Success = false;
            result.Message = "Invalid data";
            return View("index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLicense(Guid id, int customerId)
        {
            clsResult result = new clsResult();
            result = await _licenseService.RemoveLicenseAsync(id);
            if (result.Success)
            {
                result.returnUrl = Url.Action("Index", new { customerId });
                result.updateType = 1;
                return Json(result);
            }
            result.Message = "Error removing license";
            return Json(result);
        }
    }
}
