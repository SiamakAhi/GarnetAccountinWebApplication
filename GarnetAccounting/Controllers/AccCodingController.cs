using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GarnetAccounting.Controllers
{
    public class AccCodingController : Controller
    {
        private readonly IAccCodingService _coding;
        private readonly IGeneralService _gs;
        private readonly IAccBaseInfoService _base;
        private readonly UserContextService _userContext;

        public AccCodingController(
            IAccCodingService coding
            , IGeneralService gs
            , IAccBaseInfoService baseInfoService
            , UserContextService userContextService)
        {
            _coding = coding;
            _gs = gs;
            _base = baseInfoService;
            _userContext = userContextService;
        }

        // ==== Load default Coding
        public async Task<IActionResult> LoadCommercialCoding()
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
            {
                result.Message = "شرکت فعالی یافت نشد";
                return Json(result.ToJsonResult());
            }
            result = await _coding.LoadDefaultCoding_CommercialAsync(sellerId.Value);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }
        public async Task<IActionResult> LoadDefaultGroups()
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
            {
                result.Message = "شرکت فعالی یافت نشد";
                return Json(result.ToJsonResult());
            }
            result = await _coding.LoadDefaultCoding_AddGroupsAsync(sellerId.Value);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }
        public async Task<IActionResult> SetGroupIdToKol()
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
            {
                result.Message = "شرکت فعالی یافت نشد";
                return Json(result.ToJsonResult());
            }
            result = await _coding.LoadDefaultCoding_SetGroupIdAsync(sellerId.Value);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }
        public async Task<IActionResult> ResetSellerAccountingData()
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
            {
                result.Message = "شرکت فعالی یافت نشد";
                return Json(result.ToJsonResult());
            }
            result = await _coding.ResetSellerAccountingData(sellerId.Value);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        //Groups
        public async Task<IActionResult> Groups()
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            var groups = await _coding.GetGroupsAsync(sellerId);
            return View(groups);
        }

        [HttpGet]
        public IActionResult AddAccGroup()
        {
            ViewBag.GroupType = _coding.SelectList_AccountType();
            ViewBag.GroupNatureType = _coding.SelectList_GroupType();
            return PartialView("_AddAccGroup");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccGroup(GroupDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (!sellerId.HasValue)
            {
                result.Message = "جهت ایجاد حساب، فعال بودن شرکت الزامی است.";

                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;

                result = await _coding.AddGroupAsync(dto);
                if (result.Success)
                {
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.GroupType = _coding.SelectList_AccountType();
            ViewBag.GroupNatureType = _coding.SelectList_GroupType();
            return Json(result.ToJsonResult());
        }
        [HttpGet]
        public async Task<IActionResult> EditGroup(int id)
        {
            var model = await _coding.GetGroupDtoAsync(id);
            ViewBag.GroupType = _coding.SelectList_AccountType();
            ViewBag.GroupNatureType = _coding.SelectList_GroupType();
            return PartialView("_EditGroup", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(GroupDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
            {
                result.Message = "جهت ایجاد حساب، فعال بودن شرکت الزامی است.";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                result = await _coding.EditGroupAsync(dto);
                if (result.Success)
                {
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }
            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.GroupType = _coding.SelectList_AccountType();
            ViewBag.GroupNatureType = _coding.SelectList_GroupType();
            return Json(result.ToJsonResult());
        }


        public async Task<IActionResult> DelGroup(short itemId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            result = await _coding.DeleteGroupAsync(itemId);
            result.returnUrl = Request.Headers["Referer"].ToString();

            return Json(result.ToJsonResult());
        }

        //===== Kol
        public async Task<IActionResult> Kols(short id, string groupName, string groupCode)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            var kols = await _coding.GetKolsAsync(id, sellerId);
            ViewBag.GroupName = groupName;
            ViewBag.GroupId = id;
            ViewBag.GroupCode = groupCode;

            return PartialView("_Kols", kols);
        }
        [HttpGet]
        public async Task<IActionResult> AddKol(short GroupId, string groupName, string groupCode)
        {
            ViewBag.GroupId = GroupId;
            ViewBag.GroupName = groupName;
            ViewBag.GroupCode = groupCode;
            ViewBag.type = _coding.SelectList_AccountType();
            ViewBag.Nature = _coding.SelectList_Nature();
            ViewBag.kolcode = await _coding.GenerateKolCodeAsync(GroupId);
            return PartialView("_AddKol");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddKol(KolDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (!sellerId.HasValue)
            {
                result.Message = "جهت ایجاد حساب، فعال بودن شرکت الزامی است.";

                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;

                result = await _coding.AddKolAsync(dto);
                if (result.Success)
                {
                    result.returnUrl = Url.Action("Kols", "AccCoding", new { Area = "", id = dto.GroupId, groupName = dto.GroupName, groupCode = dto.GroupCode });
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.type = _coding.SelectList_AccountType();
            ViewBag.Nature = _coding.SelectList_Nature();

            return Json(result.ToJsonResult());
        }
        [HttpGet]
        public async Task<IActionResult> EditKol(int id)
        {
            var model = await _coding.GetKolDtoAsync(id);

            ViewBag.type = _coding.SelectList_AccountType();
            ViewBag.Nature = _coding.SelectList_Nature();

            return PartialView("_EditKol", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditKol(KolDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (!sellerId.HasValue)
            {
                result.Message = "شرکت فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;

                result = await _coding.EditKolAsync(dto);
                if (result.Success)
                {
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.type = _coding.SelectList_AccountType();
            ViewBag.Nature = _coding.SelectList_Nature();

            return Json(result.ToJsonResult());
        }
        [HttpPost]
        public async Task<IActionResult> DeleteKol(int itemId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            result = await _coding.DeleteTheKolAsync(itemId);
            result.returnUrl = Request.Headers["Referer"].ToString();

            return Json(result.ToJsonResult());
        }

        // Moein
        public async Task<IActionResult> Moeins(int kolId, string groupName, string KolName, string kolCode, short groupId)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            var moeins = await _coding.GetMoeinsAsync(kolId, sellerId);
            ViewBag.GroupName = groupName;
            ViewBag.GroupId = groupId;
            ViewBag.KolName = KolName;
            ViewBag.KolId = kolId;
            ViewBag.KolCode = kolCode;

            return PartialView("_Moeins", moeins);
        }
        [HttpGet]
        public async Task<IActionResult> AddMoein(int kolId, string kolName, string kolCode, string groupName, short groupId)
        {
            ViewBag.KolId = kolId;
            ViewBag.KolName = kolName;
            ViewBag.KolCode = kolCode;
            ViewBag.GroupName = groupName;
            ViewBag.GroupId = groupId;
            ViewBag.Nature = _coding.SelectList_Nature();
            ViewBag.MoeinCode = await _coding.GenerateMoeinCodeAsync(kolId);
            return PartialView("_AddMoein");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMoein(MoeinDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (!sellerId.HasValue)
            {
                result.Message = "جهت ایجاد حساب، فعال بودن شرکت الزامی است.";

                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                if (dto.MoeinContraryNatureId == 0)
                    dto.MoeinContraryNatureId = null;
                if (dto.CurrencyId == 0)
                    dto.CurrencyId = null;

                result = await _coding.AddMoeinAsync(dto);
                if (result.Success)
                {
                    result.returnUrl = Url.Action("Moeins", "AccCoding", new { Area = "", kolId = dto.KolId, kolName = dto.KolName, groupName = dto.GroupName, kolCode = dto.KolCode, groupId = dto.GroupId });
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.type = _coding.SelectList_AccountType();
            ViewBag.Nature = _coding.SelectList_Nature();

            return Json(result.ToJsonResult());
        }
        [HttpGet]
        public async Task<IActionResult> EditMoein(int Id)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (sellerId == null) return BadRequest();

            var dto = await _coding.GetMoeinDtoByIdAsync(Id);
            if (dto == null) return BadRequest();
            ViewBag.Nature = _coding.SelectList_Nature();
            return PartialView("_EditMoein", dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMoein(MoeinDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (!sellerId.HasValue)
            {
                result.Message = "جهت ایجاد حساب، فعال بودن شرکت الزامی است.";

                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                if (dto.MoeinContraryNatureId == 0)
                    dto.MoeinContraryNatureId = null;
                if (dto.CurrencyId == 0)
                    dto.CurrencyId = null;

                result = await _coding.EditMoeinAsync(dto);
                if (result.Success)
                {
                    result.returnUrl = Request.Headers["Refere"].ToString();
                    result.ShowMessage = true;
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }


            ViewBag.Nature = _coding.SelectList_Nature();

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> DelMoein(int itemId)
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
            result = await _coding.DeleteMoeinAsync(itemId);
            if (result.Success)
            {
                result.updateType = 1;
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Refere"].ToString();
            }
            return Json(result.ToJsonResult());
        }

        //Tafsil Group
        public async Task<IActionResult> TafsilGroups()
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            var model = await _coding.TafsilsGroupAsync(sellerId);

            return View(model);
        }
        public async Task<IActionResult> DelTafsilGroups(int itemId)
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
            result = await _coding.DeleteTafsilGroupAsync(itemId);
            if (result.Success)
            {
                result.updateType = 1;
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Refere"].ToString();
            }
            return Json(result.ToJsonResult());
        }


        public async Task<IActionResult> AccountTafsil(int moeinId, int kolId, string moeinName, string moeinCode, string kolName, string groupName, short groupId)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            ViewBag.Groups = await _coding.SelectList_TafsilGroupsAsync(sellerId);

            var model = await _coding.GetAccountTafsilAsync(moeinId);

            ViewBag.KolId = model != null ? model.KolId : kolId;
            ViewBag.KolName = model != null ? model.KolName : kolName;
            ViewBag.MoeinId = model != null ? model.Id : moeinId;
            ViewBag.MoeinName = model != null ? model.MoeinName : moeinName;
            ViewBag.MoeinCode = model != null ? model.MoeinCode : moeinCode;
            ViewBag.GroupName = groupName;
            ViewBag.GroupId = model != null ? model.GroupId : groupId;

            return PartialView("_AccountTafsil", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetTafsilToAccount(AccountTafsilDto dto)
        {
            if (dto == null)
                return View("Error");
            clsResult result = new clsResult();
            result.Success = false;

            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                dto.Tafsil4_GroupIds = dto.Tafsil4_Array != null ? JsonConvert.SerializeObject(dto.Tafsil4_Array) : null;
                dto.Tafsil5_GroupIds = dto.Tafsil5_Array != null ? JsonConvert.SerializeObject(dto.Tafsil5_Array) : null;
                dto.Tafsil6_GroupIds = dto.Tafsil6_Array != null ? JsonConvert.SerializeObject(dto.Tafsil6_Array) : null;
                dto.Tafsil7_GroupIds = dto.Tafsil7_Array != null ? JsonConvert.SerializeObject(dto.Tafsil7_Array) : null;
                dto.Tafsil8_GroupIds = dto.Tafsil8_Array != null ? JsonConvert.SerializeObject(dto.Tafsil8_Array) : null;

                result = await _coding.SetTafsilsAsync(dto);
                if (result.Success)
                {
                    result.Message = "اطلاعات با موفقیت ثبت شد";
                    result.ShowMessage = true;
                    result.returnUrl = Url.Action("AccountTafsil", "AccCoding", new { Area = "", moeinId = dto.Id });

                }
            }

            return Json(result.ToJsonResult());

        }
        [HttpGet]
        public async Task<IActionResult> MoeinSetting(int id)
        {
            long? sellerId = _userContext.SellerId;
            if (!sellerId.HasValue)
                return Ok();

            ViewBag.Groups = await _coding.SelectList_TafsilGroupsAsync(sellerId);
            ViewBag.MoeinId = id;

            var model = await _coding.GetAccountTafsilAsync(id);
            return PartialView("_MoeinSetting", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoeinSetting(AccountTafsilDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = _userContext.SellerId;

            if (dto == null || !sellerId.HasValue)
            {
                result.Message = "خطا در ذخره سازی اطلاعات";
                result.ShowMessage = true;
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.Tafsil4_GroupIds = dto.Tafsil4_Array != null ? JsonConvert.SerializeObject(dto.Tafsil4_Array) : null;
                dto.Tafsil5_GroupIds = dto.Tafsil5_Array != null ? JsonConvert.SerializeObject(dto.Tafsil5_Array) : null;
                dto.Tafsil6_GroupIds = dto.Tafsil6_Array != null ? JsonConvert.SerializeObject(dto.Tafsil6_Array) : null;
                dto.Tafsil7_GroupIds = dto.Tafsil7_Array != null ? JsonConvert.SerializeObject(dto.Tafsil7_Array) : null;
                dto.Tafsil8_GroupIds = dto.Tafsil8_Array != null ? JsonConvert.SerializeObject(dto.Tafsil8_Array) : null;

                result = await _coding.SetTafsilsAsync(dto);
                if (result.Success)
                {
                    result.Message = "تنظیمات حساب با موفقیت انجام شد. مجددا حساب موردنطرتان را از لیستانتخاب کنید.";
                    result.ShowMessage = true;
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();

                }
            }

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public IActionResult AddTafsilGroup()
        {
            return PartialView("_AddTafsilGroup");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTafsilGroup(TafsilGroupDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (dto == null) { return Json(result.ToJsonResult()); }
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                dto.IsEditable = true;

                result = await _coding.AddTafsilGroupAsync(dto);
                if (result.Success)
                {
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


        [HttpGet]
        public async Task<IActionResult> UpdateTafsilGroup(int id)
        {

            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
                return NoContent();

            var model = await _coding.GetTafsilGroupById(id);
            if (model == null)
                return BadRequest();

            return PartialView("_UpdateTafsilGroup", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTafsilGroup(TafsilGroupDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (dto == null) { return Json(result.ToJsonResult()); }
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                dto.IsEditable = true;

                result = await _coding.UpdateTafsilGroupAsync(dto);
                if (result.Success)
                {
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

        public async Task<IActionResult> TafsilAccounts()
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            var data = await _coding.TafsilsAsync(sellerId);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> AddTafsil()
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (sellerId == null)
                return Ok();

            ViewBag.Groups = await _coding.SelectList_TafsilGroupsAsync(sellerId);
            ViewBag.TafsilCode = _coding.TafsilCodeGenerator(sellerId.Value);
            return PartialView("_AddTafsil");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTafsil(TafsilDto dto)
        {

            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            ViewBag.Groups = await _coding.SelectList_TafsilGroupsAsync(sellerId);

            if (dto == null) { return Json(result.ToJsonResult()); }

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                dto.strGroupsId = dto.intGroupsId.Length > 0 ? JsonConvert.SerializeObject(dto.intGroupsId) : "";

                result = await _coding.AddTafsilAsync(dto);
                if (result.Success)
                {
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

        [HttpGet]
        public async Task<IActionResult> UpdateTafsil(long id)
        {
            var model = await _coding.FindTafsilAsync(id);
            if (model == null)
                return Ok();
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            ViewBag.Groups = await _coding.SelectList_TafsilGroupsAsync(sellerId);

            return PartialView("_UpdateTafsil", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTafsil(TafsilDto dto)
        {

            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            ViewBag.Groups = await _coding.SelectList_TafsilGroupsAsync(sellerId);

            if (dto == null) { return Json(result.ToJsonResult()); }

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                dto.strGroupsId = dto.intGroupsId.Length > 0 ? JsonConvert.SerializeObject(dto.intGroupsId) : "";

                result = await _coding.EditTafsilAsync(dto);
                if (result.Success)
                {
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


        // Finance Periods
        public async Task<IActionResult> FinancePeriods()
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (sellerId == null) return Ok();
            var data = await _coding.FinancePeriodsAsync(sellerId.Value);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AddFinancePeriod()
        {
            return PartialView("_AddFinancePeriod");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFinancePeriod(FinancePeriodsDto dto)
        {

            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (dto == null) { return Json(result.ToJsonResult()); }

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                dto.StartDate = dto.strStartDate.PersianToLatin();
                dto.EndDate = dto.strEndDate.PersianToLatin();

                result = await _coding.AddPeriodAsync(dto);
                if (result.Success)
                {
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

        [HttpGet]
        public async Task<IActionResult> EditFinancePeriod(int id)
        {
            var dto = await _coding.GetFinanceDtoAsync(id);

            return PartialView("_EditFinancePeriod", dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFinancePeriod(FinancePeriodsDto dto)
        {

            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (dto == null) { return Json(result.ToJsonResult()); }

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = sellerId;
                dto.StartDate = dto.strStartDate.PersianToLatin();
                dto.EndDate = dto.strEndDate.PersianToLatin();
                result = await _coding.EditPeriodAsync(dto);
                if (result.Success)
                {
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

        [HttpPost]
        public async Task<IActionResult> SetSellerPeriod(int periodId)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (sellerId == null) return Ok();

            var result = await _coding.SetSellerPeriodAsync(User.Identity.Name, periodId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePeriod(int itemId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                result = await _coding.DeleteThePeriodAsync(itemId);
                if (result.Success)
                {
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

        [HttpPost]
        public async Task<IActionResult> DelTafsil(long itemId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                result = await _coding.DeleteTafsilAccountAsync(itemId);
                if (result.Success)
                {
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

        public async Task<IActionResult> updateTaf()
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            if (sellerId == null)
            {
                result.Message = "فروشنده فعالی وجود ندارد";
                return Json(result.ToJsonResult());
            }

            var res = await _coding.UpdateTafsilCodeAsync(sellerId.Value);

            string url = Request.Headers["Referer"].ToString();
            return View(url);
        }
    }
}
