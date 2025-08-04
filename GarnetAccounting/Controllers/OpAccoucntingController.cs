using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Projects.ProjectInterfaces;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using GarnetAccounting.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GarnetAccounting.Controllers
{
    [Authorize(Roles = "AccountingManager,AccountingBoss,AccountingUser")]
    public class OpAccoucntingController : Controller
    {

        private readonly IAccBaseInfoService _base;
        private readonly IAccCodingService _codin;
        private readonly IAccOperationService _serv;
        private readonly IGeneralService _gs;
        private readonly UserContextService _userContext;
        private readonly IConProjectService _project;
        public OpAccoucntingController(IAccCodingService codingService
            , IAccOperationService operationService
            , IGeneralService GeneralService
            , IAccBaseInfoService baseInfoService
            , UserContextService userContext
            , IConProjectService projectService)
        {
            _codin = codingService;
            _serv = operationService;
            _gs = GeneralService;
            _base = baseInfoService;
            _userContext = userContext;
            _project = projectService;
        }

        public async Task<IActionResult> AccountingDocs(DocFilterDto filter)
        {

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = _userContext.PeriodId.Value;

            VmAccountingDocs model = new VmAccountingDocs();
            var data = _serv.GetDocs(filter);
            model.DocsPagin = Pagination<DocDto>.Create(data, filter.CurrentPage, filter.PageSize);
            model.filter = filter;

            ViewBag.docType = _base.SelectList_DocTypes();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CreateDocHeader()
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            { return Ok(null); }

            ViewBag.DocTypes = _codin.SelectList_DocTypes();
            ViewBag.DocNumber = await _serv.DocNumberGeneratorAsync(_userContext.SellerId.Value, _userContext.PeriodId.Value);
            return PartialView("_CreatDocHeader");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocHeader(DocDto_AddNew dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;


            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            if (string.IsNullOrEmpty(dto.strDocDate))
            {
                result.Message = "تاریخ سند وارد نشده است";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                dto.Id = dto.Id;
                dto.SellerId = _userContext.SellerId.Value;
                dto.PeriodId = _userContext.PeriodId.Value;
                dto.DocDate = dto.strDocDate.PersianToLatin();
                dto.CreatorUserName = User.Identity.Name;
                result = await _serv.CreateDocHeaderAsync(dto);

                if (result.Success)
                {
                    result.updateType = 1;
                    result.ShowMessage = false;
                    result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = dto.Id });
                    return Json(result.ToJsonResult());
                }
                result.ShowMessage = true;
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.DocTypes = _codin.SelectList_DocTypes();
            ViewBag.DocNumber = await _serv.DocNumberGeneratorAsync(_userContext.SellerId.Value, _userContext.PeriodId.Value);

            return Json(result.ToJsonResult());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocFromDash(HomePageViewModel dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;


            if (_userContext.SellerId == null || _userContext.PeriodId == null)
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
                dto.Doc.SellerId = _userContext.SellerId.Value;
                dto.Doc.PeriodId = _userContext.PeriodId.Value;
                dto.Doc.DocDate = dto.Doc.strDocDate.PersianToLatin();
                dto.Doc.CreatorUserName = User.Identity.Name;
                result = await _serv.CreateDocHeaderAsync(dto.Doc);

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

            ViewBag.DocTypes = _codin.SelectList_DocTypes();
            ViewBag.DocNumber = await _serv.DocNumberGeneratorAsync(_userContext.SellerId.Value, _userContext.PeriodId.Value);

            return Json(result.ToJsonResult());
        }
        public async Task<IActionResult> Doc(Guid id, Guid? selectedArticle)
        {
            AddUpdateDocDto model = new AddUpdateDocDto();
            model.Doc = await _serv.GetDocDtoAsync(id);
            ViewBag.DocTypes = _codin.SelectList_DocTypes();
            ViewBag.Moeins = await _codin.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Projects = await _project.SelectList_ProjectsAsync(_userContext.SellerId.Value);

            ViewBag.SelectedArticle = selectedArticle;
            //ViewBag.DocNumber = await _serv.DocNumberGeneratorAsync(userSett.ActiveSellerId.Value, userSett.ActiveSellerPeriod.Value);
            ViewData["Title"] = $"سنـد حسابداری {model.Doc.DocNumber}";
            return View(model);
        }
        public async Task<IActionResult> LastDoc()
        {

            long? sellerId = _userContext.SellerId;
            int? periodId = _userContext.PeriodId;
            if (sellerId == null || periodId == null)
                return Redirect(Request.Headers["Referer"].ToString());

            AddUpdateDocDto model = new AddUpdateDocDto();
            model.Doc = await _serv.GetLastUserDocAsync(sellerId.Value, periodId.Value, User.Identity.Name);
            if (model.Doc == null)
                return Redirect(Request.Headers["Referer"].ToString());

            ViewBag.DocTypes = _codin.SelectList_DocTypes();
            ViewBag.Moeins = await _codin.SelectList_MoeinsAsync(sellerId);
            ViewBag.Projects = await _project.SelectList_ProjectsAsync(_userContext.SellerId.Value);
            ViewData["Title"] = $"سنـد حسابداری {model.Doc.DocNumber}";
            return View("Doc", model);
        }
        public async Task<IActionResult> DocViaNumber(int number)
        {

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            long SellerId = _userContext.SellerId.Value;
            int PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                PeriodId = _userContext.PeriodId.Value;
            AddUpdateDocDto model = new AddUpdateDocDto();
            model.Doc = await _serv.GetDocWithNumberAsync(SellerId, PeriodId, number);
            if (model.Doc == null)
                return NoContent();

            ViewBag.DocTypes = _codin.SelectList_DocTypes();
            ViewBag.Moeins = await _codin.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Projects = await _project.SelectList_ProjectsAsync(_userContext.SellerId.Value);
            ViewData["Title"] = $"سنـد حسابداری {model.Doc.DocNumber}";

            return View("Doc", model);
        }

        [HttpGet]
        public async Task<IActionResult> getMoeinTafsils(int id)
        {
            if (id == 0 || id == null)
                return Ok();
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                return Ok();

            long SellerId = _userContext.SellerId.Value;
            int PeriodId = _userContext.PeriodId.Value;

            var tafsils = await _codin.GetMoeinTafsilsAsync(id, SellerId);
            ViewBag.Tafsil4 = tafsils.Tafsil4;
            ViewBag.Tafsil5 = tafsils.Tafsil5;
            ViewBag.Tafsil6 = tafsils.Tafsil6;
            ViewBag.Tafsil7 = tafsils.Tafsil7;
            ViewBag.Tafsil8 = tafsils.Tafsil8;
            ViewBag.MoeinId = id;
            AddUpdateDocDto model = new AddUpdateDocDto();
            model.AccountStatus = await _serv.GetMoeinStatusByIdAsync(id, SellerId, PeriodId);

            return PartialView("_getMoeinTafsils", model);
        }

        [HttpGet]
        public async Task<IActionResult> editArticle_getMoeinTafsils(int id)
        {
            if (id == 0 || id == null)
                return Ok();
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                return Ok();

            long SellerId = _userContext.SellerId.Value;
            int PeriodId = _userContext.PeriodId.Value;

            var tafsils = await _codin.GetMoeinTafsilsAsync(id, SellerId);
            ViewBag.Tafsil4 = tafsils.Tafsil4;
            ViewBag.Tafsil5 = tafsils.Tafsil5;
            ViewBag.Tafsil6 = tafsils.Tafsil6;
            ViewBag.Tafsil7 = tafsils.Tafsil7;
            ViewBag.Tafsil8 = tafsils.Tafsil8;
            AddUpdateDocDto model = new AddUpdateDocDto();
            model.AccountStatus = await _serv.GetMoeinStatusByIdAsync(id, SellerId, PeriodId);

            return PartialView("_editArticle_getMoeinTafsils", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArticle(AddUpdateDocDto model)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.updateType = 1;
            result.ShowMessage = true;

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                if (model.Article.ProjectId == 0)
                    model.Article.ProjectId = null;

                model.Article.Bed = Convert.ToInt64(model.Article.strBed.Replace(",", ""));
                model.Article.Bes = Convert.ToInt64(model.Article.strBes.Replace(",", ""));
                model.Article.SellerId = _userContext.SellerId.Value;
                model.Article.CreatorUserName = User.Identity.Name;

                result = await _serv.AddArticleAsync(model.Article);

                if (result.Success)
                {
                    result.ShowMessage = false;
                    result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = model.Article.DocId });
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.DocTypes = _codin.SelectList_DocTypes();
            ViewBag.DocNumber = await _serv.DocNumberGeneratorAsync(_userContext.SellerId.Value, _userContext.PeriodId.Value);

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDraftDoc(AddUpdateDocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {

                dto.Doc.DocDate = dto.Doc.strDocDate.PersianToLatin();
                dto.Doc.EditorUserName = User.Identity.Name;
                dto.Doc.LastUpdateDate = DateTime.Now;
                dto.Doc.StatusId = 1;
                result = await _serv.SaveDraftDocHeaderAsync(dto.Doc);

                if (result.Success)
                {
                    result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = dto.Doc.Id });
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.DocTypes = _codin.SelectList_DocTypes();

            return Json(result.ToJsonResult());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTemptDoc(AddUpdateDocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                var chackBalance = await _serv.GetDocDtoAsync(dto.Doc.Id);
                if (chackBalance == null)
                {
                    result.Message = "سند موردنظر یافت نشد.";
                    return Json(result.ToJsonResult());
                }
                if (chackBalance.Bed != chackBalance.Bes)
                {
                    result.Message = "سند موردنظر تراز نمی باشد، ثبت موقت سند امکانپذیر نیست.";
                    return Json(result.ToJsonResult());
                }
                if ((chackBalance.Bed <= 0 && chackBalance.Bes <= 0) || chackBalance.Articles?.Count <= 1)
                {
                    result.Message = "مقادیر بدهکار و بستانکار سند از لحاظ منطفی صحیح نمی باشد.";
                    return Json(result.ToJsonResult());
                }
                dto.Doc.DocDate = dto.Doc.strDocDate.PersianToLatin();
                dto.Doc.EditorUserName = User.Identity.Name;
                dto.Doc.LastUpdateDate = DateTime.Now;
                dto.Doc.StatusId = 2;

                result = await _serv.SaveDraftDocHeaderAsync(dto.Doc);

                if (result.Success)
                {

                    result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = dto.Doc.Id });
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.DocTypes = _codin.SelectList_DocTypes();

            return Json(result.ToJsonResult());
        }

        public async Task<IActionResult> DeleteDocArticle(Guid itemId)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            var article = await _serv.GetDocArticleDtoAsync(itemId);
            if (article == null)
            {
                result.Message = "آرتیکل موردنظر یافت نشد.";
                return Json(result.ToJsonResult());
            }

            result = await _serv.DeleteDocArticleAsync(itemId);

            if (result.Success)
            {
                result.ShowMessage = false;
                result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = article.DocId });
                return Json(result.ToJsonResult());
            }


            ViewBag.DocTypes = _codin.SelectList_DocTypes();

            return Json(result.ToJsonResult());
        }

        public async Task<IActionResult> DeleteDoc(Guid itemId)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _serv.DeleteDocAsync(itemId, User.Identity.Name);

            if (result.Success)
            {
                result.ShowMessage = false;
                result.returnUrl = Url.Action("AccountingDocs", "OpAccoucnting", new { Area = "" });
                return Json(result.ToJsonResult());
            }


            ViewBag.DocTypes = _codin.SelectList_DocTypes();

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(Guid id)
        {
            AddUpdateDocDto model = new AddUpdateDocDto();
            model.Article = await _serv.GetDocArticleDtoAsync(id);
            if (model.Article == null) { return Ok(); }

            var tafsils = await _codin.GetMoeinTafsilsAsync(model.Article.MoeinId, _userContext.SellerId.Value);
            ViewBag.Tafsil4 = tafsils.Tafsil4;
            ViewBag.Tafsil5 = tafsils.Tafsil5;
            ViewBag.Tafsil6 = tafsils.Tafsil6;
            ViewBag.Tafsil7 = tafsils.Tafsil7;
            ViewBag.Tafsil8 = tafsils.Tafsil8;
            ViewBag.Moeins = await _codin.SelectList_MoeinsAsync(_userContext.SellerId.Value);

            model.Article.SellerId = _userContext.SellerId.Value;
            ViewBag.Projects = await _project.SelectList_ProjectsAsync(_userContext.SellerId.Value);
            return PartialView("_EditArticle", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(AddUpdateDocDto model)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                if (model.Article.ProjectId == 0)
                    model.Article.ProjectId = null;

                model.Article.Bed = Convert.ToInt64(model.Article.strBed.Replace(",", ""));
                model.Article.Bes = Convert.ToInt64(model.Article.strBes.Replace(",", ""));
                model.Article.EditorUserName = User.Identity.Name;
                model.Article.LastUpdateDate = DateTime.Now;

                result = await _serv.UpdateArticleAsync(model.Article);

                if (result.Success)
                {
                    result.ShowMessage = false;
                    result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = model.Article.DocId });
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            ViewBag.DocTypes = _codin.SelectList_DocTypes();
            ViewBag.DocNumber = await _serv.DocNumberGeneratorAsync(_userContext.SellerId.Value, _userContext.PeriodId.Value);

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> ArticleRenumberRows(string jsonData, Guid docId)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;
            List<SetNumberDto>? dto = JsonConvert.DeserializeObject<List<SetNumberDto>>(jsonData);

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _serv.ArticlesRenumberRows(dto, User.Identity.Name);

            if (result.Success)
            {
                result.returnUrl = Url.Action("Doc", "OpAccoucnting", new { Area = "", id = docId });
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> ExportXlsxDoc(Guid id)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }

            var doc = await _serv.GetDocDtoAsync(id);

            var fileContent = _serv.GenerateAccountingDocumentExcel(doc);
            string fileName = $"سند حسابداری شماره {doc.DocNumber}.xlsx";
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public async Task<IActionResult> CheckBalance(Guid DocId)
        {
            var model = await _serv.CheckBalanceByArchiveAsync(DocId);
            return PartialView("_CheckBalance", model);
        }

    }
}




