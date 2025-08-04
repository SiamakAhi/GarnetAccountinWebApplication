using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.AutoServiceAgency.CarAgencyInterfaces;
using GarnetAccounting.Areas.AutoServiceAgency.Dto;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.AutoServiceAgency.Controllers
{
    [Area("AutoServiceAgency")]
    [Authorize]
    public class ContractorController : Controller
    {
        private readonly ICarAgancyInterface _rec;
        private readonly UserContextService _userContext;
        private readonly IAccCodingService _coding;
        public ContractorController(ICarAgancyInterface carAgancyService,
            UserContextService userContext
            , IAccCodingService accCodingService)
        {
            _rec = carAgancyService;
            _userContext = userContext;
            _coding = accCodingService;
        }
        //========================================================================================== Contractor ====================
        public async Task<IActionResult> Contractors()
        {

            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();
            var model = await _rec.GetContractorsAsync(_userContext.SellerId.Value);

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddContractor()
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(_userContext.SellerId.Value);
            return PartialView("_AddContractor");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContractor(ContractorDto model)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
            {
                result.Message = "شرکت فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                model.SellerId = _userContext.SellerId.Value;
                model.CreatedBy = User.Identity.Name;

                result = await _rec.AddContractorAsync(model);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    result.Message += "\n" + error.ErrorMessage;
                }
            }

            return Json(result.ToJsonResult());
        }
        [HttpGet]
        public async Task<IActionResult> UpdateContractor(int id)
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            var contractor = await _rec.GetContractorByIdAsync(id);
            if (contractor == null || contractor.SellerId != _userContext.SellerId)
                return NotFound();

            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(_userContext.SellerId.Value);
            return PartialView("_UpdateContractor", contractor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateContractor(ContractorDto model)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
            {
                result.Message = "شرکت فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                model.SellerId = _userContext.SellerId.Value;

                result = await _rec.UpdateContractorAsync(model);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    result.Message += "\n" + error.ErrorMessage;
                }
            }

            return Json(result.ToJsonResult());
        }

        //========================================================================================== Services =======================
        public async Task<IActionResult> Services()
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            var model = await _rec.GetServicesAsync(_userContext.SellerId.Value);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddService()
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(_userContext.SellerId.Value);
            return PartialView("_AddService");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddService(ServiceDto model)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
            {
                result.Message = "شرکت فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                model.SellerId = _userContext.SellerId.Value;
                if (model.TafsilId == 0)
                    model.TafsilId = null;
                result = await _rec.AddServiceAsync(model);

                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    result.Message += "\n" + error.ErrorMessage;
                }
            }

            return Json(result.ToJsonResult());
        }
        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            var service = await _rec.GetServiceByIdAsync(id); // باید پیاده‌سازی شود
            if (service == null || service.SellerId != _userContext.SellerId)
                return NotFound();

            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(_userContext.SellerId.Value);

            return PartialView("_UpdateService", service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateService(ServiceDto model)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
            {
                result.Message = "شرکت فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                model.SellerId = _userContext.SellerId.Value;
                if (model.TafsilId == 0)
                    model.TafsilId = null;
                result = await _rec.UpdateServiceAsync(model);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    result.Message += "\n" + error.ErrorMessage;
                }
            }

            return Json(result.ToJsonResult());
        }

        //=========================================================================================== Accounting Setting =============
        [HttpGet]
        public async Task<IActionResult> AsaSettings()
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            var setting = await _rec.GetOrCreateSettingsAsync(_userContext.SellerId.Value);

            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(_userContext.SellerId.Value);

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(ReceptionAccountingSettingDto dto)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
            {
                result.Message = "شرکت فعال شناسایی نشد.";
                return Json(result.ToJsonResult());
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    result.Message += "\n" + error.ErrorMessage;
                }
                return Json(result.ToJsonResult());
            }

            dto.SellerId = _userContext.SellerId.Value;

            result = await _rec.UpdateSettingsAsync(dto);

            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }

            return Json(result.ToJsonResult());
        }


        //========================================================================================== Lamari Services =======================
        public async Task<IActionResult> LamariServices()
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            var model = await _rec.GetLamariServicesAsync(_userContext.SellerId.Value);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddLamariService()
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(_userContext.SellerId.Value);
            return PartialView("_AddLamariService");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLamariService(LamariServiceDto model)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
            {
                result.Message = "شرکت فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                model.SellerId = _userContext.SellerId.Value;
                if (model.TafsilId == 0)
                    model.TafsilId = null;
                result = await _rec.AddLamriServiceAsync(model);

                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    result.Message += "\n" + error.ErrorMessage;
                }
            }

            return Json(result.ToJsonResult());
        }
        [HttpGet]
        public async Task<IActionResult> UpdateLamariService(int id)
        {
            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
                return BadRequest();

            var service = await _rec.GetLamariServiceByIdAsync(id);
            if (service == null || service.SellerId != _userContext.SellerId)
                return NotFound();

            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(_userContext.SellerId.Value);

            return PartialView("_UpdateLamariService", service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLamariService(LamariServiceDto model)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue || _userContext.SellerId == 0)
            {
                result.Message = "شرکت فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                model.SellerId = _userContext.SellerId.Value;
                if (model.TafsilId == 0)
                    model.TafsilId = null;
                result = await _rec.UpdateLamariServiceAsync(model);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    result.Message += "\n" + error.ErrorMessage;
                }
            }

            return Json(result.ToJsonResult());
        }

        public async Task<IActionResult> getServiceShareAmount(int id)
        {
            var service = await _rec.GetLamariServiceByIdAsync(id);
            if (service == null)
                return Json(new { success = false, message = "سرویس یافت نشد" });

            return Json(new { success = true, mechanic = service.MechanicShareAmount, electric = service.ElectricianShareAmount });
        }


    }
}
