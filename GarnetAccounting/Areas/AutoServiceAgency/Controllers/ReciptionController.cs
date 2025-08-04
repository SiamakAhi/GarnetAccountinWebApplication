using GarnetAccounting.Areas.AutoServiceAgency.CarAgencyInterfaces;
using GarnetAccounting.Areas.AutoServiceAgency.Dto;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.AutoServiceAgency.Controllers
{
    [Area("AutoServiceAgency")]
    [Authorize]
    public class ReciptionController : Controller
    {
        private readonly ICarAgancyInterface _rec;
        private readonly UserContextService _user;
        public ReciptionController(ICarAgancyInterface agancyService, UserContextService userContextService)
        {
            _rec = agancyService;
            _user = userContextService;
        }

        //-------------------------------------------------------------------------------------
        //================ Lamari =============================================================
        public async Task<IActionResult> LamariReciptions(ReciptionFilterDto filter)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
                return NoContent();
            filter.SellerId = _user.SellerId.Value;
            ReceptionsViewModel model = new ReceptionsViewModel();
            model.filter = filter;
            model.Receptions = await _rec.CarRec_GetReceptionHeaderAsync(filter);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> LamariAdmissionDetails(string receptionNumber)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
                return NoContent();
            var model = await _rec.GetSaveDetailsDtoAsync(_user.SellerId.Value, receptionNumber);

            ViewBag.Services = await _rec.SelectList_LamariServicesAsync(_user.SellerId.Value);
            ViewBag.Contractors = await _rec.SelectList_ContractorsAsync(_user.SellerId.Value);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LamariCreateDoc(SaveDetailsDto dto)
        {
            var result = new clsResult();

            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
            {
                result.Message = "شرکت یا سال مالی فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }
            if (string.IsNullOrEmpty(dto.CarNumber))
            {
                result.Message = "پلاک خودرو را بدرستی وارد کنید";
                return Json(result.ToJsonResult());
            }

            try
            {
                var reception = await _rec.GetReceptionItemsByNumbersAsync(_user.SellerId.Value, dto.ReceptionNember);
                if (!reception.Any())
                {
                    result.Message = "اطلاعات پذیرش یافت نشد";
                    return Json(result.ToJsonResult());
                }

                if (dto.Items != null)
                {
                    foreach (var item in dto.Items)
                    {
                        var receptionItem = reception.FirstOrDefault(x => x.Id == item.Id);
                        if (receptionItem != null)
                        {
                            receptionItem.LicensePlate = dto.CarNumber;
                            receptionItem.LamariServiceId = item.LamariServiceId;

                            receptionItem.ContractorId = item.ContractorId;
                            receptionItem.ContractorPersentage = item.Percentage;
                            receptionItem.ContractorShareMoney = long.TryParse(item.strContractorShareAmount.Replace(",", ""), out long contShare) ? contShare : 0;

                            receptionItem.lamariElectricShareAmount = long.TryParse(item.lamaristrElectricShareAmount.Replace(",", ""), out long elecamount) ? elecamount : 0;
                            receptionItem.lamariMechanicShareAmount = long.TryParse(item.lamaristrMechanicShareAmount.Replace(",", ""), out long mechAmount) ? mechAmount : 0;
                            receptionItem.lamariElectricContractor = item.lamariElectricContractor;
                            receptionItem.lamariMechanicContractor = item.lamariMechanicContractor;
                        }
                    }
                }

                // ذخیره شماره پلاک
                var header = reception.First();
                header.LicensePlate = dto.CarNumber;

                result = await _rec.UpdateLamariReceptionAsync(reception);

                if (result.Success)
                {
                    result.returnUrl = Url.Action("LamariReciptions", "Reciption", new { Area = "AutoServiceAgency" });
                    result.updateType = 2;
                    result.ShowMessage = true;
                    result.Message += $"<a href='{result.returnUrl}' class='btn btn-primary px-4'>بازگشت به لیست پذیرش ها</a>";
                }

            }
            catch (Exception ex)
            {
                result.Message = "خطا در ذخیره اطلاعات: " + ex.Message;
            }

            return Json(result.ToJsonResult());
        }

        //------------------------------------------------------------------------------------
        //================ Modiran Khodro ====================================================
        public async Task<IActionResult> ModiranReciptions(ReciptionFilterDto filter)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
                return NoContent();
            filter.SellerId = _user.SellerId.Value;
            ReceptionsViewModel model = new ReceptionsViewModel();
            model.filter = filter;
            model.Receptions = await _rec.CarRec_GetReceptionHeaderAsync(filter);
            return View(model);
        }
        [HttpGet]
        public IActionResult ImportModiranReceptions()
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
                return NoContent();
            ImpodtReceptionDto model = new ImpodtReceptionDto();
            model.UserCreator = User.Identity.Name;
            model.SellerId = _user.SellerId.Value;
            model.PeriodId = _user.PeriodId.Value;

            return PartialView("_ImportModiranReceptions", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportModiranReceptions(ImpodtReceptionDto model)
        {
            clsResult result = new clsResult();

            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
            {
                result.Message = "شرکت یا سال مالی فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }
            if (model.Brand == null || model.Brand.Length == 0)
            {
                result.Message = "برند را مشخص کنید";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                if (model.Brand == "Lamari")
                    result = await _rec.CarRec_ImportReception_LamariAsync(model);
                else
                    result = await _rec.CarRec_ImportReception_ModiranAsync(model);

                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            if (errors.Any())
            {
                foreach (var er in errors)
                {
                    result.Message += "</br>" + er.ErrorMessage;
                }
            }

            return Json(result.ToJsonResult());
        }
        [HttpGet]
        public async Task<IActionResult> CompleteAdmissionDetails(string receptionNumber)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
                return NoContent();
            var model = await _rec.GetSaveDetailsDtoAsync(_user.SellerId.Value, receptionNumber);

            ViewBag.Services = await _rec.SelectList_ServicesAsync(_user.SellerId.Value);
            ViewBag.Contractors = await _rec.SelectList_ContractorsAsync(_user.SellerId.Value);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetContractors()
        {
            if (!_user.SellerId.HasValue)
                return Json(new { success = false, message = "شرکت فعال شناسایی نشد" });

            var contractors = await _rec.SelectList_ContractorsAsync(_user.SellerId.Value);
            return Json(contractors);
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            if (!_user.SellerId.HasValue)
                return Json(new { success = false, message = "شرکت فعال شناسایی نشد" });

            var services = await _rec.SelectList_ServicesAsync(_user.SellerId.Value);
            return Json(services);
        }

        [HttpGet]
        public async Task<IActionResult> GetContractorPercentage(int id)
        {
            if (!_user.SellerId.HasValue)
                return Json(new { success = false, message = "شرکت فعال شناسایی نشد" });

            var contractor = await _rec.GetContractorByIdAsync(id);
            if (contractor == null)
                return Json(new { success = false, message = "پیمانکار یافت نشد" });

            return Json(new { success = true, percentage = contractor.SharePercentage });
        }

        [HttpPost]
        public async Task<IActionResult> SaveReceptionDetails(SaveDetailsDto dto)
        {
            var result = new clsResult();

            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
            {
                result.Message = "شرکت یا سال مالی فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }
            if (string.IsNullOrEmpty(dto.CarNumber))
            {
                result.Message = "پلاک خودرو را بدرستی وارد کنید";
                return Json(result.ToJsonResult());
            }

            try
            {
                var reception = await _rec.GetReceptionItemsByNumbersAsync(_user.SellerId.Value, dto.ReceptionNember);
                if (!reception.Any())
                {
                    result.Message = "اطلاعات پذیرش یافت نشد";
                    return Json(result.ToJsonResult());
                }
                if (dto.Items != null)
                {
                    foreach (var item in dto.Items)
                    {
                        var receptionItem = reception.FirstOrDefault(x => x.Id == item.Id);
                        if (receptionItem != null)
                        {
                            receptionItem.ContractorId = item.ContractorId;
                            receptionItem.ServiceId = item.ServiceId;
                            receptionItem.LicensePlate = dto.CarNumber;

                        }
                    }
                }


                // ذخیره شماره پلاک
                var header = reception.First();
                header.LicensePlate = dto.CarNumber;

                result = await _rec.UpdateReceptionDetailsAsync(reception);

                if (result.Success)
                {
                    result.returnUrl = Url.Action("ModiranReciptions", "Reciption", new { Area = "AutoServiceAgency" });
                    result.updateType = 1;
                    result.ShowMessage = true;
                    result.Message += $"<a href='{result.returnUrl}' class='btn btn-primary px-4'>بازگشت به لیست پذیرش ها</a>";
                }

            }
            catch (Exception ex)
            {
                result.Message = "خطا در ذخیره اطلاعات: " + ex.Message;
            }

            return Json(result.ToJsonResult());
        }
    }
}
