using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    [Authorize(Roles = "AccountingManager,AccountingBoss,AccountingUser")]
    public class AccFuncController : Controller
    {
        private readonly IAccBaseInfoService _base;
        private readonly IAccCodingService _codin;
        private readonly IAccOperationService _serv;
        private readonly IAccEndOfPeriodService _endPeriodService;
        private readonly IGeneralService _gs;
        public AccFuncController(IAccCodingService codingService
            , IAccOperationService operationService
            , IGeneralService GeneralService
            , IAccBaseInfoService baseInfoService
            , IAccEndOfPeriodService endPeriodService)
        {
            _codin = codingService;
            _serv = operationService;
            _gs = GeneralService;
            _base = baseInfoService;
            _endPeriodService = endPeriodService;
        }

        [HttpGet]
        public async Task<IActionResult> DocSorting(DocFilterDto filter)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = 0;
            if (userSett.ActiveSellerPeriod.HasValue)
                filter.PeriodId = userSett.ActiveSellerPeriod.Value;

            if (userSett == null || userSett.ActiveSellerId == null)
            {
                ViewBag.Allert = "تنظیمات کاربری شما بدرستی انجام نشده است بنابراین مشاهده لیست اسناد امکان پذیر نیست";
            }
            VmAccountingDocs model = new VmAccountingDocs();
            if (!string.IsNullOrEmpty(filter.strStartDate))
                filter.StartDate = filter.strStartDate.PersianToLatin();
            if (!string.IsNullOrEmpty(filter.strEndDate))
                filter.EndDate = filter.strEndDate.PersianToLatin();

            model.Docs = await _serv.GetDocsAsync(filter);
            model.filter = filter;

            ViewBag.docType = _base.SelectList_DocTypes();

            return View("DocSorting", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DocSortingOp(int startNumber, Guid[] docIds, DocFilterDto filter)
        {
            VmAccountingDocs model = new VmAccountingDocs();

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = 0;
            if (userSett.ActiveSellerPeriod.HasValue)
                filter.PeriodId = userSett.ActiveSellerPeriod.Value;

            if (userSett == null || userSett.ActiveSellerId == null)
            {
                return View(model);
            }
            var result = await _serv.DocsSortingAsync(filter.SellerId, filter.PeriodId, docIds, startNumber);

            model.Docs = await _serv.GetDocsAsync(filter);
            model.filter = filter;

            ViewBag.docType = _base.SelectList_DocTypes();

            return View("DocSorting", model);
        }

        [HttpPost]
        public async Task<IActionResult> DocChangesToTemporary(Guid[] items)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _endPeriodService.UpdateDocumentsStatusAsync(items, 2, userSett.UserName);

            if (result.Success)
            {
                result.returnUrl = Url.Action("AccountingDocs", "OpAccoucnting", new { Area = "" });
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }
        [HttpPost]
        public async Task<IActionResult> DocChangesToFinal(Guid[] items)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _endPeriodService.UpdateDocumentsStatusAsync(items, 3, userSett.UserName);

            if (result.Success)
            {
                result.returnUrl = Url.Action("AccountingDocs", "OpAccoucnting", new { Area = "" });
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> DelDocs(Guid[] items)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _serv.BulkDeleteDocumentsAsync(items, userSett.UserName);

            if (result.Success)
            {
                result.returnUrl = Url.Action("AccountingDocs", "OpAccoucnting", new { Area = "" });
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> MergeDocs(Guid[] items)
        {
            var model = new DocsMergeDto();
            model.Doc = new DocMerge_Header();
            model.Doc.DocsForMerge = items.ToList();
            model.Articles = await _serv.GetMergeDocsArticlesAsync(items);
            if (model.Articles.Count == 0)
                return Ok();

            ViewBag.DocNumber = model.Articles.Max(n => n.DocNumber);
            DateTime docDate = model.Articles.Max(n => n.DocDate).Value;
            ViewBag.docDate = docDate.LatinToPersianForDatepicker();

            return PartialView("_MergeDocs", model);
        }

        [HttpPost]
        public async Task<IActionResult> DocMerge_GetArticles(DocsMergeDto dto)
        {
            var model = new DocsMergeDto();
            model.Doc = new DocMerge_Header();
            model.Articles = await _serv.GetMergeDocsArticlesAsync(dto.Doc.DocsForMerge.ToArray(), dto.Doc.MergeSameAccount, dto.Doc.MergeSameTafsil);
            if (model.Articles.Count == 0)
                return BadRequest();
            return PartialView("_DocMerge_GetArticles", model);
        }
        [HttpPost]
        public async Task<IActionResult> DocMerge_AddDoc(DocsMergeDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;


            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            if (string.IsNullOrEmpty(dto.Doc.strDocDate))
            {
                result.Message = "تاریخ سند وارد نشده است";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                dto.Doc.Id = dto.Doc.Id;
                dto.Doc.SellerId = userSett.ActiveSellerId.Value;
                dto.Doc.PeriodId = userSett.ActiveSellerPeriod.Value;
                dto.Doc.DocDate = dto.Doc.strDocDate.PersianToLatin();
                dto.Doc.CreatorUserName = User.Identity.Name;
                result = await _serv.AddMergedDocsAsync(dto.Doc);

                if (result.Success)
                {
                    result.updateType = 1;
                    result.ShowMessage = false;
                    result.returnUrl = Url.Action("AccountingDocs", "OpAccoucnting", new { Area = "" });
                    return Json(result.ToJsonResult());
                }
                result.ShowMessage = true;
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }


            return Json(result.ToJsonResult());
        }

        //Copy Doc 
        [HttpPost]
        public async Task<IActionResult> CopyDocs(Guid[] items)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
                return Ok();

            var model = new DocsMergeDto();
            model.Doc = new DocMerge_Header();
            model.Doc.DocsForMerge = items.ToList();
            model.Articles = await _serv.GetMergeDocsArticlesAsync(items);
            if (model.Articles.Count == 0)
                return Ok();

            ViewBag.DocNumber = await _serv.DocNumberGeneratorAsync(userSett.ActiveSellerId.Value, userSett.ActiveSellerPeriod.Value);
            DateTime docDate = model.Articles.Max(n => n.DocDate).Value;
            ViewBag.docDate = DateTime.Now.LatinToPersianForDatepicker();

            return PartialView("_CopyDocs", model);
        }
        [HttpPost]
        public async Task<IActionResult> CopyDoc_Insert(DocsMergeDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;


            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            if (string.IsNullOrEmpty(dto.Doc.strDocDate))
            {
                result.Message = "تاریخ سند وارد نشده است";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                dto.Doc.Id = dto.Doc.Id;
                dto.Doc.SellerId = userSett.ActiveSellerId.Value;
                dto.Doc.PeriodId = userSett.ActiveSellerPeriod.Value;
                dto.Doc.DocDate = dto.Doc.strDocDate.PersianToLatin();
                dto.Doc.CreatorUserName = User.Identity.Name;
                result = await _serv.CopyDocsAsync(dto.Doc);

                if (result.Success)
                {
                    result.updateType = 1;
                    result.ShowMessage = false;
                    result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = dto.Doc.Id });
                    return Json(result.ToJsonResult());
                }
                result.ShowMessage = true;
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }


            return Json(result.ToJsonResult());
        }

        //Deleted Doc
        public async Task<IActionResult> DeletedDocs(DocFilterDto filter)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = 0;
            if (userSett.ActiveSellerPeriod.HasValue)
                filter.PeriodId = userSett.ActiveSellerPeriod.Value;

            if (userSett == null || userSett.ActiveSellerId == null)
            {
                ViewBag.Allert = "تنظیمات کاربری شما بدرستی انجام نشده است بنابراین مشاهده لیست اسناد امکان پذیر نیست";
            }
            VmAccountingDocs model = new VmAccountingDocs();
            model.Docs = await _serv.GetDeletedDocsAsync(filter);
            model.filter = filter;

            ViewBag.docType = _base.SelectList_DocTypes();

            return View(model);
        }

        public async Task<IActionResult> UndeleteDoc(Guid itemId)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _serv.UndeleteDocAsync(itemId, User.Identity.Name);

            if (result.Success)
            {
                result.ShowMessage = false;
                result.returnUrl = Url.Action("DeletedDocs", "AccFunc", new { Area = "Accounting" });
                return Json(result.ToJsonResult());
            }


            ViewBag.DocTypes = _codin.SelectList_DocTypes();

            return Json(result.ToJsonResult());
        }

        //--------------------

        [HttpPost]
        public async Task<IActionResult> DelArticles(Guid[] items)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _serv.DeleteDocArticlesAsync(items.ToList(), userSett.UserName);

            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> GetArchiveNumber(Guid docNo)
        {
            string archiveNumber = await _serv.GetDocArchiveNumber(docNo);
            return Json(archiveNumber);
        }

        [HttpGet]
        public async Task<IActionResult> MoveArticles(List<Guid> items)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                return Json(null);
            }

            MoveArticlesToDocDto model = new MoveArticlesToDocDto();
            model.Articles = await _serv.GetArticlesDtoByIdAsync(items);

            if (model.Articles.Count == 0)
                return Ok();

            model.userName = User.Identity.Name;
            model.CurrentDocId = model.Articles.FirstOrDefault().DocId;
            ViewBag.Docs = await _serv.Selectlist_Dosc(userSett.ActiveSellerId.Value, userSett.ActiveSellerPeriod.Value);
            return PartialView("_MoveArticles", model);
        }
        [HttpPost]
        public async Task<IActionResult> MoveBulkArticles(List<Guid> items)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                return Json(null);
            }

            MoveArticlesToDocDto model = new MoveArticlesToDocDto();
            model.Articles = await _serv.GetArticlesDtoByIdAsync(items);

            if (model.Articles.Count == 0)
                return Ok();

            model.userName = User.Identity.Name;
            model.CurrentDocId = model.Articles.FirstOrDefault().DocId;
            ViewBag.Docs = await _serv.Selectlist_Dosc(userSett.ActiveSellerId.Value, userSett.ActiveSellerPeriod.Value);
            return PartialView("_MoveArticles", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveArticles(MoveArticlesToDocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (dto.DocId.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                result.Message = "سند مقصد را انتحاب نکردید.";
                return Json(result.ToJsonResult());
            }

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                result = await _serv.MoveToOtherDocAsyncAsync(dto);

                if (result.Success)
                {
                    result.updateType = 1;
                    result.ShowMessage = true;
                    result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = dto.CurrentDocId });
                    return Json(result.ToJsonResult());
                }
                result.ShowMessage = true;
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }


            return Json(result.ToJsonResult());
        }

    }
}
