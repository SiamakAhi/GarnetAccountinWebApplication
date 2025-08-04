using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.PartyDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GarnetAccounting.Controllers
{
    public class PersenController : Controller
    {
        private readonly IPersonService _service;
        private readonly IAppIdentityUserManager _usermanager;
        private readonly IGeneralService _gs;
        private readonly IAccCodingService _coding;


        public PersenController(IPersonService service
            , IAppIdentityUserManager usermanager
            , IGeneralService generalService
            , IAccCodingService CodingService)
        {
            _service = service;
            _usermanager = usermanager;
            _gs = generalService;
            _coding = CodingService;
        }

        public async Task<IActionResult> PersenList()
        {
            string userName = User.Identity.Name;

            long? sellerId = await _gs.GetActiveSellerIdAsync(userName);
            ViewBag.SellerId = sellerId;
            if (sellerId == null)
                return Ok();
            var model = await _service.PersenAsync(sellerId.Value);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddPerson()
        {
            string userName = User.Identity.Name;
            ViewBag.customerId = await _usermanager.GetCustomerIdByUsername(userName);
            ViewBag.SellerId = await _gs.GetActiveSellerIdAsync(userName);
            ViewBag.PartyTypes = await _service.SelectList_PartyTypeAsync();
            return PartialView("_AddPerson");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerson(PersonDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            string userName = User.Identity.Name;
            int? cusId = await _usermanager.GetCustomerIdByUsername(userName);
            long? sellerId = await _gs.GetActiveSellerIdAsync(userName);

            if (string.IsNullOrEmpty(userName) || sellerId == null)
            {
                result.Message = "از لحاظ امنیتی و دسترسی مشکلی پیش آمده است. مجددا لاگین کنید";
                return Json(result.ToJsonResult());
            }

            ViewBag.customerId = cusId;
            ViewBag.SellerId = sellerId;

            if (ModelState.IsValid)
            {
                result = await _service.AddPersonAsync(dto);
                if (result.Success)
                {
                    result.Success = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.ShowMessage = true;
                    return Json(result.ToJsonResult());
                }
            }
            var modelErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message = "\n " + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePerson(long id)
        {
            var person = await _service.GetPersonDtoAsync(id);
            if (person == null) return Ok();
            ViewBag.PartyTypes = await _service.SelectList_PartyTypeAsync();

            return PartialView("_UpdatePerson", person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePerson(PersonDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            string userName = User.Identity.Name;
            int? cusId = await _usermanager.GetCustomerIdByUsername(userName);
            long? sellerId = await _gs.GetActiveSellerIdAsync(userName);

            if (string.IsNullOrEmpty(userName) || sellerId == null)
            {
                result.Message = "از لحاظ امنیتی و دسترسی مشکلی پیش آمده است. مجددا لاگین کنید";
                return Json(result.ToJsonResult());
            }

            ViewBag.customerId = cusId;
            ViewBag.SellerId = sellerId;

            if (ModelState.IsValid)
            {
                result = await _service.UpdatePersonAsync(dto);
                if (result.Success)
                {
                    result.Success = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.ShowMessage = true;
                    return Json(result.ToJsonResult());
                }
            }
            var modelErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message = "\n " + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> DelPerson(long itemId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }
            result = await _service.DeletePersonAsync(itemId);
            if (result.Success)
            {
                result.updateType = 1;
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Refere"].ToString();
            }
            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> SetTafsil(string name, long id)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            var model = new SetTafsilDto();
            model.SellerId = sellerId.Value;
            model.Name = name;
            model.IsPerson = false;
            model.intGroupsId = new int[] { 1 };
            model.longId = id;

            ViewBag.Title = "تعیین تفصیلی برای  " + name;
            ViewBag.Groups = await _coding.SelectList_TafsilGroupsAsync(sellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(sellerId);
            return PartialView("_SetTafsil", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPersonTafsil(SetTafsilDto dto)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            clsResult result = new clsResult();
            result.Success = false;

            if (!dto.longId.HasValue)
            {
                result.Message = "شناسه شخص موردنظر یافت نشد. مجددا تلاش کنید";
                return Json(result.ToJsonResult());
            }
            if (dto.Id <= 0)
            {
                result.Message = "حساب تفصیلی موردنظر خود را از لیست موجود انتخاب نمایید";
                return Json(result.ToJsonResult());
            }

            result = await _service.SetPersonTafsilAsync(dto.longId.Value, dto.Id);
            if (result.Success)
                result.returnUrl = Request.Headers["Referer"].ToString();

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePersonTafsil(SetTafsilDto dto)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            clsResult result = new clsResult();
            result.Success = false;
            if (!sellerId.HasValue)
            {
                result.Message = "فروشنده فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            if (dto.intGroupsId.Length == 0 || dto.intGroupsId == null)
            {
                dto.intGroupsId = new int[] { 4 };
            }
            if (!dto.longId.HasValue)
            {
                result.Message = "شناسه شخص موردنظر یافت نشد. مجددا تلاش کنید";
                return Json(result.ToJsonResult());
            }
            dto.strGroupsId = JsonConvert.SerializeObject(dto.intGroupsId);
            dto.SellerId = sellerId.Value;
            if (ModelState.IsValid)
            {
                result = await _coding.CreatePersonTafsilAsync(dto);
                if (result.Success)
                    result.returnUrl = Request.Headers["Referer"].ToString();
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();

            foreach (var er in errors)
            {
                result.Message += "\n " + er.ErrorMessage;
            }
            return Json(result.ToJsonResult());
        }

        public IActionResult RemovePersonTafsil(long id)
        {
            ViewBag.PersonId = id;
            return PartialView("_RemovePersonTafsil");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePersonTafsilp(long PersonId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result = await _service.RemoveTafsilFromPersonAsync(PersonId);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }
            result.Message = "خطایی در حین عملیات رخ داده است";
            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> Representatives(long id)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (sellerId == null)
                return Ok();

            VmRepresentative model = new VmRepresentative();
            model.presentatives = await _service.GetPersenRepresentativesDtoAsync(id);
            ViewBag.PersonId = id;
            ViewBag.PersonName = await _service.GetPersonNameByIdAsync(id);
            ViewBag.persen = await _service.SelectList_PersenAsync(sellerId.Value);

            return PartialView("_Representatives", model);
        }

        [HttpGet]
        public async Task<IActionResult> AddRepresentative(long id)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (sellerId == null)
                return Ok();

            ViewBag.PersonId = id;
            ViewBag.Reprensatives = await _service.SelectList_PersenAsync(sellerId.Value);

            return PartialView("_AddRepresentative");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRepresentative(VmRepresentative dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            string userName = User.Identity.Name;
            long? sellerId = await _gs.GetActiveSellerIdAsync(userName);

            if (string.IsNullOrEmpty(userName) || sellerId == null)
            {
                result.Message = "از لحاظ امنیتی و دسترسی مشکلی پیش آمده است. مجددا لاگین کنید";
                return Json(result.ToJsonResult());
            }

            ViewBag.SellerId = sellerId;

            if (ModelState.IsValid)
            {
                result = await _service.AddPartyRepresentativeAsync(dto.presentative.ParentId, dto.presentative.RepresentativeId);
                if (result.Success)
                {
                    result.Success = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.ShowMessage = true;
                    return Json(result.ToJsonResult());
                }
            }
            var modelErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message = "\n " + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRepresentative(long partyId, long repId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            string userName = User.Identity.Name;
            long? sellerId = await _gs.GetActiveSellerIdAsync(userName);

            if (string.IsNullOrEmpty(userName) || sellerId == null)
            {
                result.Message = "فروشنده فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            result = await _service.RemovePartyRepresentativeAsync(partyId, repId);
            if (result.Success)
            {
                result.Success = true;
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.ShowMessage = true;
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAllPersenTafsil()
        {
            clsResult result = new clsResult();
            result.Success = false;
            string userName = User.Identity.Name;
            long? sellerId = await _gs.GetActiveSellerIdAsync(userName);

            if (string.IsNullOrEmpty(userName) || sellerId == null)
            {
                result.Message = "فروشنده فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            result = await _service.CreateBulkTafsilsAsync(sellerId.Value);
            result.returnUrl = Request.Headers["Referer"].ToString();
            return Json(result.ToJsonResult());
        }

        public async Task<IActionResult> TaxPayerDataList()
        {
            string userName = User.Identity.Name;

            long? sellerId = await _gs.GetActiveSellerIdAsync(userName);
            if (sellerId == null)
                return Ok();
            var data = await _service.PersenAsync(sellerId.Value);
            var model = data.OrderBy(n => n.Name).ToList();
            return PartialView("_TaxPayerDataList", model);
        }
    }
}
