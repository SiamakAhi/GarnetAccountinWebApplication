using GarnetAccounting.Areas.AvaRasta.Interfaces;
using GarnetAccounting.Areas.AvaRasta.ViewModels;
using GarnetAccounting.Classes;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.CommercialInterfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.CommercialViewModel;
using GarnetAccounting.ViewModels.PartyDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GarnetAccounting.Controllers
{
    [Authorize]

    public class SellerController : Controller
    {
        private readonly ISellerService _service;
        private readonly IAppIdentityUserManager _usermanager;
        private readonly ICostomerService _customerService;
        private readonly IBuyerService _buyer;
        private readonly IGeneralService _gs;
        public SellerController(ISellerService service
            , ICostomerService customerService
            , IAppIdentityUserManager userManager
            , IBuyerService buyer
            , IGeneralService gs)
        {
            _service = service;
            _customerService = customerService;
            _usermanager = userManager;
            _gs = gs;
            _buyer = buyer;
        }
        [HttpGet]
        public async Task<IActionResult> AddSeller()
        {
            int? customerId = await _usermanager.GetCustomerIdByUsername(User.Identity.Name);
            VmCustomer? cusInfo = await _customerService.GetVmCustomerByIdAsync(customerId.Value);
            AddSellerDto model = new AddSellerDto()
            {
                CustomerId = cusInfo.Id,
                MobilePhone = cusInfo.Mobile,
                Address = cusInfo.Address,
                EconomicCode = cusInfo.EconomicNumber,
                fullNameEn = cusInfo.Title.ToFinglish(),
                LegalStatus = cusInfo.Ishoghooghi == true ? (Int16)2 : (Int16)1,
                Name = cusInfo.FName + " " + cusInfo.LName,
                NationalId = cusInfo.NationalId,
                PostalCode = cusInfo.PostalCode,
                RegistrationNumber = cusInfo.RegistrationNumber,
            };
            ViewBag.customerId = _usermanager.GetCustomerIdByUsername(User.Identity.Name).Result;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSeller(AddSellerDto dto)
        {
            ResultDto res = new ResultDto();
            res.Success = false;
            string jsonResult = "";

            if (!string.IsNullOrEmpty(dto.NationalId))
            {
                if (dto.LegalStatus == 1)
                {
                    if (!dto.NationalId.IsValidNationalCode())
                    {
                        res.Message = "کد ملی نامعتبر است";
                        jsonResult = JsonConvert.SerializeObject(res);
                        return Json(jsonResult);
                    }

                }
                //else if (dto.LegalStatus == 2)
                //{
                //    if (!dto.NationalId.IsValidCompanyNationalId())
                //    {
                //        res.Message = "شناسه ملی نامعتبر است";
                //        jsonResult = JsonConvert.SerializeObject(res);
                //        return Json(jsonResult);
                //    }
                //}
            }

            if (ModelState.IsValid)
            {
                var privateKey = await GetKeyString.ProcessFileAsync(dto.SellerPrivateKeyFile);
                dto.SellerPrivateKey = privateKey.IsSuccess == false ? "er" : privateKey.Content;
                res = await _service.AddSeller(dto);
                if (res.Success)
                {
                    res.ReturnUrl = Url.Action("Sellers", "Seller", new { Area = "" });
                    string jsonResults = JsonConvert.SerializeObject(res);
                    return Json(jsonResults);
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                res.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.customerId = dto.CustomerId;
            jsonResult = JsonConvert.SerializeObject(res);
            return Json(jsonResult);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateSellerInfo(Int64 id)
        {
            var model = await _service.GetSellerByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSellerInfo(UpdateSellerDto dto)
        {

            if (ModelState.IsValid)
            {
                var res = await _service.UpdateSeller(dto);
                if (res.Success)
                {
                    return RedirectToAction("sellers", new { message = res.Message });
                }
            }

            return View(dto);
        }


        public async Task<IActionResult> Sellers(string message = "")
        {
            int? customerId = await _usermanager.GetCustomerIdByUsername(User.Identity.Name);
            var model = await _service.GetCustomerSellers(customerId.Value);
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }
            return View(model);
        }

        public async Task<IActionResult> SellerInfo(long id)
        {
            var model = await _service.GetSellerByIdAsync(id);
            return PartialView("_SellerInfo", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSellerOthersInfo(UpdateSellerDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (ModelState.IsValid)
            {
                result = await _service.SetOthersSellerInfoAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                }
            }

            var modelErrors = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        public async Task<IActionResult> GetTaxpayerInfo(long id, string? name = "")
        {
            var payerInfo = await _service.GetTaxPayerInfoAsync(id);
            ViewBag.Name = name;

            return View(payerInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTaxpayerInfo(TaxpayerInfoDto dto)
        {
            clsResult result = new clsResult
            {
                Success = false,
                ShowMessage = true,
                Message = "در بروزرسانی اطلاعات خطایی رخ داده است."
            };

            if (ModelState.IsValid)
            {
                // فراخوانی متد سرویس برای بروزرسانی اطلاعات
                result = await _service.UpdateTaxPayerInoAsync(dto);

                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                }
            }

            // جمع‌آوری خطاهای اعتبارسنجی
            var modelErrors = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += $" {error.ErrorMessage}";
            }

            // بازگرداندن نتیجه به صورت JSON
            return Json(result.ToJsonResult());
        }


    }
}
