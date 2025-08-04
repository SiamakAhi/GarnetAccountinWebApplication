using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Courier.Controllers
{
    [Area("Courier")]
    public class PricingController : Controller
    {
        private readonly ICuPricingService _pricingService;
        private readonly UserContextService _userContext;
        private readonly IAccCodingService _accCoding;

        public PricingController(ICuPricingService pricingService,
            UserContextService userContext,
            IAccCodingService accountingCodingService)
        {
            _pricingService = pricingService;
            _userContext = userContext;
            _accCoding = accountingCodingService;
        }

        // نمایش لیست رنج‌های وزنی
        public async Task<IActionResult> RateWeightRanges()
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();

            var rateWeightRanges = await _pricingService.GetRateWeightRangesAsync();
            return View(rateWeightRanges);
        }

        // نمایش لیست مناطق قیمت‌گذاری
        public async Task<IActionResult> RateZones()
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();

            var rateZones = await _pricingService.GetRateZonesAsync();
            return View(rateZones);
        }


        // نمایش لیست آیتم‌های هزینه
        public async Task<IActionResult> CostItems()
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();

            var costItems = await _pricingService.GetCostItemsAsync();
            return View(costItems);
        }

        // افزودن رنج وزنی جدید
        [HttpGet]
        public IActionResult AddRateWeightRange()
        {
            return PartialView("_AddRateWeightRange");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddRateWeightRange(RateWeightRangeDto dto)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                result = await _pricingService.AddRateWeightRangeAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += "\n" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        // بروزرسانی رنج وزنی
        [HttpGet]
        public async Task<IActionResult> UpdateRateWeightRange(int id)
        {
            var rateWeightRange = await _pricingService.GetRateWeightRangeByIdAsync(id);
            if (rateWeightRange == null)
            {
                return NotFound();
            }

            return PartialView("_UpdateRateWeightRange", rateWeightRange);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateRateWeightRange(RateWeightRangeDto dto)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                result = await _pricingService.UpdateRateWeightRangeAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += "\n" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        // حذف رنج وزنی
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteRateWeightRange(int id)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            result = await _pricingService.DeleteRateWeightRangeAsync(id);
            if (result.Success)
            {
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        // افزودن آیتم هزینه جدید
        [HttpGet]
        public async Task<IActionResult> AddCostItem()
        {
            if (!_userContext.SellerId.HasValue) return NoContent();

            ViewBag.RateImpact = await _pricingService.SelectList_RateImpactTypeAsync();
            ViewBag.Moeins = await _accCoding.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Tafsils = await _accCoding.SelectList_TafsilsAsync(_userContext.SellerId.Value);

            return PartialView("_AddCostItem");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddCostItem(CostItemDto dto)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                if (dto.AccountMoeinId == 0)
                    dto.AccountMoeinId = null;
                if (dto.AccountTafsilId == 0)
                    dto.AccountTafsilId = null;
                result = await _pricingService.AddCostItemAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += "\n" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        // بروزرسانی آیتم هزینه
        [HttpGet]
        public async Task<IActionResult> UpdateCostItem(int id)
        {
            var costItem = await _pricingService.GetCostItemByIdAsync(id);
            if (costItem == null)
            {
                return NotFound();
            }

            return PartialView("_UpdateCostItem", costItem);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateCostItem(CostItemDto dto)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                result = await _pricingService.UpdateCostItemAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += "\n" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        // حذف آیتم هزینه
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteCostItem(int id)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            result = await _pricingService.DeleteCostItemAsync(id);
            if (result.Success)
            {
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }


        // ================================================================  Natures
        // نمایش لیست ماهیت‌های محموله
        public async Task<IActionResult> ConsignmentNatures()
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();

            var consignmentNatures = await _pricingService.GetConsignmentNaturesAsync();
            return View(consignmentNatures);
        }


        [HttpGet]
        public async Task<IActionResult> AddNewNature()
        {

            ViewBag.RateImpacts = await _pricingService.SelectList_RateImpactTypeAsync();
            return PartialView("_AddNewNature");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddNewNature(ConsignmentNatureDto dto)
        {
            var result = new clsResult { Success = false, ShowMessage = true };

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = _userContext.SellerId.Value;
                result = await _pricingService.AddConsigmentNatureAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += "\n" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

    }
}