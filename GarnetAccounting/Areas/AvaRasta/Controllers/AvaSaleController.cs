using GarnetAccounting.Areas.AvaRasta.Interfaces;
using GarnetAccounting.Areas.AvaRasta.ViewModels;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GarnetAccounting.Areas.AvaRasta.Controllers
{
    [Area("AvaRasta")]
    [Authorize(Roles = "Admin,Support")]
    public class AvaSaleController : Controller
    {
        private readonly ICostomerService _customerService;
        private readonly IAppIdentityUserManager _userManager;

        public AvaSaleController(ICostomerService customerService, IAppIdentityUserManager userManager)
        {
            _customerService = customerService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Customers(string name = "", int currentPage = 1, int pageSize = 10)
        {
            var customers = _customerService.GetCustomers(name);
            var model = Pagination<VmCustomer>.Create(customers, currentPage, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(VmCustomer customer)
        {
            if (ModelState.IsValid)
            {
                customer.RegisterDate = DateTime.Now;
                customer.ActivationDate = customer.strActivationDate?.PersianToLatin();
                customer.LicenseExpireDate = customer.ActivationDate.Value.AddYears(1);

                var result = await _customerService.AddCustomerAsync(customer);
                if (result.Success)
                {
                    result.ReturnUrl = Url.Action("Customers", "AvaSale", new { Area = "AvaRasta", name = customer.LName });

                    return RedirectToAction("Customers", new { name = customer.Title });
                }
            }
            return View(customer);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var model = await _customerService.GetVmCustomerByIdAsync(id);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomerInfo(int id)
        {
            var model = await _customerService.GetVmCustomerByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(VmCustomer customer)
        {
            if (ModelState.IsValid)
            {
                customer.LastUpdate = DateTime.Now;

                var result = await _customerService.UpdateCustomerAsync(customer);
                if (result.Success)
                {
                    result.ReturnUrl = Url.Action("Customers", "AvaSale", new { Area = "AvaRasta", name = customer.LName });

                    return RedirectToAction("Customers", new { name = customer.Title });
                }
            }
            return View(customer);
        }
        [HttpGet]
        public async Task<IActionResult> AddCustomerAccount(int Id)
        {
            var customer = await _customerService.GetVmCustomerByIdAsync(Id);

            VmRegisterUser user = new VmRegisterUser();
            user.email = customer.Email;
            user.customerId = customer.Id;
            user.FName = customer.FName;
            user.LName = customer.LName;
            user.Mobile = customer.Mobile;
            user.Birthday = DateTime.Now;

            return PartialView("_AddCustomerAccount", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomerAccount(VmRegisterUser user)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (ModelState.IsValid)
            {
                user.Roles = new string[1];
                user.Roles[0] = "CustomerOwner";
                var Identityresult = await _userManager.AddCustomerOwnerAccount(user);
                if (Identityresult.Succeeded)
                {
                    result.Success = true;
                    result.ShowMessage = true;
                    result.updateType = 1;
                    result.Message = "کاربر جدید با موفقیت ایجاد شد";
                    result.returnUrl = Url.Action("Customers", "AvaSale", new { Area = "AvaRasta", name = user.LName });
                    return Json(result.ToJsonResult());
                }
                else
                {
                    result.Success = false;
                    result.ShowMessage = true;
                    foreach (var error in Identityresult.Errors)
                    {
                        result.Message += "<br>" + error.Description;
                    }
                    return Json(result.ToJsonResult());
                }
            }
            result.ShowMessage = true;

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());

        }

        [HttpPost]
        public async Task<IActionResult> DelCustomer(int id)
        {
            ResultDto result = new ResultDto();
            result.Success = false;
            result = await _customerService.DeleteCustomerAsync(id);
            if (result.Success)
                result.ReturnUrl = Request.Headers["Referer"].ToString();

            return Json(JsonConvert.SerializeObject(result));
        }
    }
}
