using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GarnetAccounting.Areas.CustomerArea.CustomerInterfases;
using GarnetAccounting.Areas.CustomerArea.Dto;
using GarnetAccounting.ViewModels;

namespace GarnetAccounting.Areas.CustomerArea.Controllers
{
    [Area("CustomerArea")]
    [Authorize]
    public class CustomerManagerController : Controller
    {
        private readonly ICustomerService _cusService;
        private readonly int? _customerId;
        public CustomerManagerController(ICustomerService customerService)
        {
            _cusService = customerService;


        }

        public async Task<IActionResult> CustomerUserList()
        {
            var model = await _cusService.GetCustomerUsersAsync(await _cusService.GetCustomerIdByUsernameAsync(User?.Identity.Name));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddCustomerUserAccount()
        {
            string userName = User.Identity.Name;
            int? cusId = _cusService.GetCustomerIdByUsername(User.Identity.Name);
            AddCustomerUserDto dto = new AddCustomerUserDto();
            dto.customerId = cusId.Value;
            ViewBag.sellers = await _cusService.Selectlist_CustomerSellersAsync(userName);
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> AddCustomerUser()
        {
            string userName = User.Identity.Name;
            int? cusId = _cusService.GetCustomerIdByUsername(User.Identity.Name);
            AddCustomerUserDto dto = new AddCustomerUserDto();
            dto.customerId = cusId.Value;
            ViewBag.sellers = await _cusService.Selectlist_CustomerSellersAsync(userName);
            return PartialView("_AddCustomerUser", dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomerUser(AddCustomerUserDto dto)
        {
            ResultDto result = new ResultDto();
            if (ModelState.IsValid)
            {
                result = await _cusService.CreateCustomerUserAsync(dto);
                if (result.Success)
                {
                    result.ReturnUrl = Url.Action("CustomerUserList", "CustomerManager", "CustomerArea");

                }
            }
            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.sellers = await _cusService.Selectlist_CustomerSellersAsync(User.Identity.Name);
            string jsonResult = JsonConvert.SerializeObject(result);
            return Json(jsonResult);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserPermission(string userName)
        {
            var model = await _cusService.GetPermissionInfoAsync(userName);
            ViewBag.sellers = await _cusService.Selectlist_CustomerSellersAsync(userName);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetUserPermission(UpdatePermissionDto dto)
        {
            ResultDto result = new ResultDto();
            if (ModelState.IsValid)
            {
                result = await _cusService.UpdateUserSettingAsync(dto.UserSetting);
                if (result.Success)
                {
                    result.ReturnUrl = Url.Action("UpdateUserPermission", "CustomerManager", new { Area = "CustomerArea", userName = dto.UserSetting.UserName });
                }
            }
            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            string jsonResult = JsonConvert.SerializeObject(result);
            return Json(jsonResult);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSellerToUser(UpdatePermissionDto dto)
        {
            ResultDto result = new ResultDto();
            if (ModelState.IsValid)
            {
                result = await _cusService.AddSellerToUserAsync(dto.UserSeller);
                if (result.Success)
                {
                    result.ReturnUrl = Url.Action("UpdateUserPermission", "CustomerManager", new { Area = "CustomerArea", userName = dto.UserSeller.UserName });
                }
            }
            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            string jsonResult = JsonConvert.SerializeObject(result);
            return Json(jsonResult);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveSellerFromUser(Int64 Id, string userName)
        {
            ResultDto result = new ResultDto();

            result = await _cusService.RemoveSellerFromUserAsync(Id);
            if (result.Success)
            {
                result.ReturnUrl = Url.Action("UpdateUserPermission", "CustomerManager", new { Area = "CustomerArea", userName = userName });
            }
            string jsonResult = JsonConvert.SerializeObject(result);
            return Json(jsonResult);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            ResultDto result = new ResultDto();

            result = await _cusService.DelCustomerUserAsync(userName);
            if (result.Success)
            {

                result.ReturnUrl = Url.Action("CustomerUserList", "CustomerManager", new { Area = "CustomerArea" });
            }
            string jsonResult = JsonConvert.SerializeObject(result);
            return Json(jsonResult);
        }


    }

}

