using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    [Authorize(Roles = "AccountingManager,AccountingBoss,AccountingUser")]
    public class accsettingsController : Controller
    {
        private readonly UserContextService _userContext;
        private readonly IAccSettingService _setting;
        private readonly IAccCodingService _codingService;
        public accsettingsController(UserContextService userContextService, IAccSettingService settingService, IAccCodingService codingService)
        {
            _userContext = userContextService;
            _setting = settingService;
            _codingService = codingService;
        }

        public async Task<ActionResult> SellerAccSetting()
        {
            var model = new AccSettingDto();
            if (_userContext.SellerId.HasValue)
            {
                model = await _setting.GetSettingAsync(_userContext.SellerId.Value);
                ViewBag.Moeins = await _codingService.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SellerAccSetting(AccSettingDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.updateType = 1;
            result.ShowMessage = true;
            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "شرکت فعال شناسایی نشد.";
                return Json(result.ToJsonResult());
            }

            dto.SellerId = _userContext.SellerId.Value;
            result = await _setting.UpdateSettingAsync(dto);

            if (result.Success)
            {
                result.updateType = 1;
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }
            ViewBag.Moeins = await _codingService.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            return Json(result.ToJsonResult());
        }

    }
}
