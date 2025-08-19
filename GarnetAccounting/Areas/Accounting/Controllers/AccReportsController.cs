using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using GarnetAccounting.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Area("Accounting")]

    public class AccReportsController : Controller
    {
        private readonly IAccBaseInfoService _base;
        private readonly IAccountingReportService _ser;
        private readonly IGeneralService _gs;
        private readonly IAccCodingService _coding;
        private readonly UserContextService _userContext;
        private readonly IAccGetBaseDataService _baseData;

        public AccReportsController(IAccBaseInfoService BaseInfo
            , IAccountingReportService ser
            , IGeneralService gs
            , IAccCodingService coding
            , IAccGetBaseDataService baseDataService
            , UserContextService userContext)
        {
            _base = BaseInfo;
            _ser = ser;
            _gs = gs;
            _coding = coding;
            _baseData = baseDataService;
            _userContext = userContext;
        }

        //===== مرورگر حساب ها
        //----- سطح کل.............................
        public async Task<IActionResult> AccountBrowserKol(AccountsBrowserDto model)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";

            if (model.filter == null)
            {
                model = new AccountsBrowserDto();
                model.filter = new DocFilterDto();
            }

            var minMax = await _ser.GetMinMaxDocNumberAsync(_userContext.SellerId.Value, _userContext.PeriodId.Value);
            model.filter.SellerId = _userContext.SellerId.Value;
            model.filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                model.filter.PeriodId = _userContext.PeriodId.Value;
            model.Kols = await _ser.Report_KolAsync(model.filter);
            ViewBag.docType = _base.SelectList_DocTypes();

            if (!model.filter.FromDocNumer.HasValue)
                model.filter.FromDocNumer = minMax.MinNumber;
            if (!model.filter.ToDocNumer.HasValue)
                model.filter.ToDocNumer = minMax.MaxNumber;
            if (string.IsNullOrEmpty(model.filter.strStartDate))
                model.filter.strStartDate = minMax.MinDate.LatinToPersian();
            if (string.IsNullOrEmpty(model.filter.strEndDate))
                model.filter.strEndDate = minMax.MaxDate.LatinToPersian();

            return View(model);
        }
        public async Task<IActionResult> Print_AccountBrowserKol(AccountsBrowserDto model)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";

            if (model.filter == null)
            {
                model = new AccountsBrowserDto();
                model.filter = new DocFilterDto();
            }

            model.filter.SellerId = _userContext.SellerId.Value;
            model.filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                model.filter.PeriodId = _userContext.PeriodId.Value;
            model.Kols = await _ser.Report_KolAsync(model.filter);
            ViewBag.docType = _base.SelectList_DocTypes();
            var period = await _coding.GetFinanceDtoAsync(_userContext.PeriodId.Value);

            string startDate = period.StartDate.LatinToPersian();
            string endDate = period.EndDate.LatinToPersian();
            if (!string.IsNullOrEmpty(model.filter.strStartDate))
                startDate = model.filter.strStartDate.PersianToLatin().LatinToPersian();
            if (!string.IsNullOrEmpty(model.filter.strEndDate))
                startDate = model.filter.strEndDate.PersianToLatin().LatinToPersian();

            ViewBag.ReportDate = DateTime.Now.LatinToPersian();
            ViewBag.SellerName = userSett.ActiveSellerName;
            ViewBag.PeriodName = $"گزارش از تاریخ {startDate} لغایت {endDate}";
            ViewBag.ReportTitle = "تراز آزمایشی در سطح کل";

            return View(model);
        }
        //---- سطخ معین
        public async Task<IActionResult> AccountBrowserMoein(AccountsBrowserDto model)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";

            model.filter.SellerId = _userContext.SellerId.Value;
            model.filter.PeriodId = _userContext.PeriodId.Value;
            if (model.filter.KolId == null)
                model.filter.KolId = model.filter.targetId;
            model.Kols = await _ser.Report_MoeinAsync(model.filter);

            ViewBag.docType = _base.SelectList_DocTypes();
            return View(model);
        }
        // سطح تفصیل
        public async Task<IActionResult> AccountBrowserTafsil(AccountsBrowserDto model)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";

            model.filter.SellerId = _userContext.SellerId.Value;
            model.filter.PeriodId = _userContext.PeriodId.Value;
            if (model.filter.LevelUp)
                model.filter.ReportLevel += 1;

            bool tafsilReport = false;
            for (int i = model.filter.ReportLevel.Value; i <= 8; i++)
            {
                bool checkHasData = await _ser.HasAccountInLevelAsync(model.filter.SellerId, model.filter.PeriodId, model.filter.MoeinId.Value, i, model.filter.CurrentTafsilId);
                if (checkHasData)
                {
                    model.filter.ReportLevel = i;
                    tafsilReport = true;
                    if (i == 5)
                    {
                        model.filter.tafsil4Id = model.filter.CurrentTafsilId;
                        model.filter.ReportLevel = 5;
                    }
                    else if (i == 6)
                    {
                        model.filter.tafsil5Id = model.filter.CurrentTafsilId;
                        model.filter.ReportLevel = 6;
                    }
                    else if (i == 7)
                    {
                        model.filter.tafsil6Id = model.filter.CurrentTafsilId;
                        model.filter.ReportLevel = 7;
                    }
                    else if (i == 8)
                    {
                        model.filter.tafsil7Id = model.filter.CurrentTafsilId;
                        model.filter.ReportLevel = 8;
                    }
                    break;
                }
            }
            if (model.filter.ReportLevel <= 8 && model.filter.ReportLevel >= 3 && tafsilReport)
            {
                model.Kols = await _ser.Report_TafsilAsync(model.filter);
                var info = model.Kols.FirstOrDefault();
                model.navInfo = new ArticleAccountInfo
                {
                    MoeinId = info.MoeinId,
                    MoeinName = info.MoeinName,
                    KolName = info.KolName,
                    KolId = info.KolId,
                    Tafsil4Id = info.Tafsil4Id,
                    Tafsil4Name = info.Tafsil4Name,
                    Tafsil5Id = info.Tafsil5Id,
                    Tafsil5Name = info.Tafsil5Name,
                    Tafsil6Id = info.Tafsil6Id,
                    Tafsil6Name = info.Tafsil6Name,
                    Tafsil7Id = info.Tafsil7Id,
                    Tafsil7Name = info.Tafsil7Name,
                    Tafsil8Id = info.Tafsil8Id,
                    Tafsil8Name = info.Tafsil8Name,
                    ReportLevel = model.filter.ReportLevel.Value
                };
            }

            else
            {
                model.Articles = await _ser.Report_BrowserArticlesAsync(model.filter);
                if (model.Articles.Count > 0)
                    model.navInfo = await _ser.GetArticleAccountInfoAsync(model.Articles.FirstOrDefault());
                model.navInfo.ReportLevel = 9;
            }
            ViewBag.docType = _base.SelectList_DocTypes();
            return View(model);
        }
        //..............................
        public async Task<IActionResult> TarazAzmayeshi(TarazDto dto)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            if (dto.filter == null)
            {
                dto.filter = new DocFilterDto();
                dto.filter.ReportLevel = 1;
            }
            dto.filter.SellerId = _userContext.SellerId.Value;
            dto.filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                dto.filter.PeriodId = _userContext.PeriodId.Value;
            if (dto.filter.BalanceColumnsQty == 6)
                dto.report = await _ser.GetTrialBalance_6ColAsync(dto.filter);
            else
                dto.report = await _ser.GetTrialBalanceAsync(dto.filter);
            return View(dto);
        }
        public async Task<IActionResult> CascadingReport(DocFilterDto filter)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            if (filter == null)
            {
                filter = new DocFilterDto();
                filter.ReportLevel = 4;
            }
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                filter.PeriodId = _userContext.PeriodId.Value;

            AccountsReportViewModel model = new AccountsReportViewModel();
            model.filter = filter;
            model.Reports = await _ser.GetAccountsCascadingReportAsync(filter);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExportXlsxTrialBalance(DocFilterDto filter)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";

            if (filter == null)
            {
                filter = new DocFilterDto();
                filter.ReportLevel = 1;
            }
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                filter.PeriodId = _userContext.PeriodId.Value;

            var fileContent = await _ser.GenerateTrialBalanceReportAsync(filter);
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TrialBalance.xlsx");
        }
        public async Task<IActionResult> ReportArticles(DocReportDto? dto)
        {
            bool getReport = true;
            if (dto.filter == null)
            {
                getReport = false;
                dto.filter = new DocFilterDto();
            }
            if (dto.Articles == null)
            {
                dto.Articles = new List<DocArticleDto>();
            }

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";

            dto.filter.SellerId = _userContext.SellerId.Value;
            dto.filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                dto.filter.PeriodId = _userContext.PeriodId.Value;
            if (getReport)
                dto.Articles = await _ser.GetArticlesAsync(dto.filter);
            else
                dto.Articles = new List<DocArticleDto>();
            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(dto.filter.SellerId);
            ViewBag.Tafsils = await _coding.SelectList_UsageTafsilsAsync(dto.filter.SellerId);
            ViewBag.Tafsils5 = await _coding.SelectList_UsageTafsils5Async(dto.filter.SellerId);
            ViewBag.Tafsils6 = await _coding.SelectList_UsageTafsils6Async(dto.filter.SellerId);

            return View(dto);
        }
        public async Task<IActionResult> ReportRooznameh(DocReportDto? dto)
        {
            bool getReport = true;
            if (dto.filter == null)
            {
                getReport = false;
                dto.filter = new DocFilterDto();
                dto.filter.PageSize = 20;
                dto.filter.CurrentPage = 1;
            }

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";

            dto.filter.SellerId = _userContext.SellerId.Value;
            dto.filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                dto.filter.PeriodId = _userContext.PeriodId.Value;

            var data = _ser.GetArticlesQuery(dto.filter);
            dto.ArticlesPagin = Pagination<DocArticleDto>.Create(data, dto.filter.CurrentPage, dto.filter.PageSize);

            ViewBag.TotalBed = await data.SumAsync(n => n.Bed);
            ViewBag.TotalBes = await data.SumAsync(n => n.Bes);
            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(dto.filter.SellerId);
            ViewBag.Tafsils = await _coding.SelectList_UsageTafsilsAsync(dto.filter.SellerId);
            ViewBag.Tafsils5 = await _coding.SelectList_UsageTafsils5Async(dto.filter.SellerId);
            ViewBag.Tafsils6 = await _coding.SelectList_UsageTafsils6Async(dto.filter.SellerId);

            return View(dto);
        }
        public IActionResult DaftarRooznameh()
        {
            return PartialView("_DaftarRooznameh");
        }
        [HttpGet]
        public async Task<IActionResult> TafsilReport()
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
                return BadRequest();
            }

            ViewBag.Moeins = await _coding.SelectList_UsageMoeinsAsync(_userContext.SellerId.Value);
            //ViewBag.Tafsils = await _coding.SelectList_UsageTafsilsAsync(userSett.ActiveSellerId.Value);
            return PartialView("_TafsilReport");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetTafsilReport(HomePageViewModel dto)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            dto.TafsilReport.filter.PeriodId = _userContext.PeriodId.Value;
            dto.TafsilReport.filter.SellerId = _userContext.SellerId.Value;

            ViewBag.Kols = await _baseData.SelectList_UsedKolsByTafsilAsync(dto.TafsilReport.filter.SellerId, null);
            ViewBag.Moeins = await _baseData.SelectListUsedMoeinsByKolsAsync(dto.TafsilReport.filter.SellerId, null);
            ViewBag.Tafsils = await _baseData.SelectList_UsageTafsilsAsync(dto.TafsilReport.filter.SellerId);
            var model = await _ser.PersonBalaceAsync(dto.TafsilReport.filter);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetTafsil4MoeinTurnover(HomePageViewModel dto)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            dto.TafsilReport.filter.PeriodId = _userContext.PeriodId.Value;
            dto.TafsilReport.filter.SellerId = _userContext.SellerId.Value;


            ViewBag.Kols = await _baseData.SelectList_UsedKolsByTafsilAsync(dto.TafsilReport.filter.SellerId, null);
            ViewBag.Moeins = await _baseData.SelectListUsedMoeinsByKolsAsync(dto.TafsilReport.filter.SellerId, null);
            ViewBag.Tafsils = await _baseData.SelectList_UsageTafsilsAsync(dto.TafsilReport.filter.SellerId);
            var model = await _ser.Tafsil4MoeinTurnoverAsync(dto.TafsilReport.filter);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetTafsil4Moein(TafsilReportFilterDto filter)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            filter.PeriodId = _userContext.PeriodId.Value;
            filter.SellerId = _userContext.SellerId.Value;


            ViewBag.Kols = await _baseData.SelectList_UsedKolsByTafsilAsync(filter.SellerId, null);
            ViewBag.Moeins = await _baseData.SelectListUsedMoeinsByKolsAsync(filter.SellerId, null);
            ViewBag.Tafsils = await _baseData.SelectList_UsageTafsilsAsync(filter.SellerId);
            var model = await _ser.Tafsil4MoeinTurnoverAsync(filter);

            return View("GetTafsil4MoeinTurnover", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TafsilReport(TafsilReportFilterDto filter)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            filter.PeriodId = _userContext.PeriodId.Value;
            filter.SellerId = _userContext.SellerId.Value;

            ViewBag.Kols = await _baseData.SelectList_UsedKolsByTafsilAsync(filter.SellerId, null);
            ViewBag.Moeins = await _baseData.SelectListUsedMoeinsByKolsAsync(filter.SellerId, null);
            ViewBag.Tafsils = await _baseData.SelectList_UsageTafsilsAsync(filter.SellerId);
            var model = await _ser.PersonBalaceAsync(filter);

            return View("GetTafsilReport", model);
        }

        //.....................  گردش حساب
        public async Task<IActionResult> TurnoverAccounts()
        {

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                return NoContent();

            DocReportDto dto = new DocReportDto();
            dto.filter.SellerId = _userContext.SellerId.Value;
            dto.filter.PeriodId = _userContext.PeriodId.Value;

            ViewBag.Kols = await _coding.SelectList_KolsAsync(_userContext.SellerId.Value);
            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(_userContext.SellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_UsageTafsilsAsync(_userContext.SellerId.Value);
            ViewBag.Tafsils5 = await _coding.SelectList_UsageTafsils5Async(_userContext.SellerId.Value);
            ViewBag.Tafsils6 = await _coding.SelectList_UsageTafsils6Async(_userContext.SellerId.Value);

            return View(dto);
        }
        public async Task<IActionResult> GetTurnoverAccounts(DocReportDto? dto)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                return NoContent();

            dto.filter.SellerId = _userContext.SellerId.Value;
            dto.filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                dto.filter.PeriodId = _userContext.PeriodId.Value;

            if (dto.filter.ReportLevel == 3)
                dto.TurnoverAccounts = await _ser.TurnoverAccounts_TafsilAsync(dto.filter);
            else if (dto.filter.ReportLevel == 2)
                dto.TurnoverAccounts = await _ser.TurnoverAccounts_MoeinAsync(dto.filter);

            ViewBag.Kols = await _coding.SelectList_KolsAsync(_userContext.SellerId.Value);
            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(dto.filter.SellerId);
            ViewBag.Tafsils = await _coding.SelectList_UsageTafsilsAsync(dto.filter.SellerId);
            ViewBag.Tafsils5 = await _coding.SelectList_UsageTafsils5Async(dto.filter.SellerId);
            ViewBag.Tafsils6 = await _coding.SelectList_UsageTafsils6Async(dto.filter.SellerId);

            return View("TurnoverAccounts", dto);
        }
        public async Task<IActionResult> GetTurnoverAccounts_Print(DocReportDto? dto)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
                return NoContent();
            var user = await _gs.GetUserSettingAsync(User.Identity.Name);

            dto.filter.SellerId = _userContext.SellerId.Value;
            dto.filter.PeriodId = 0;
            if (_userContext.PeriodId.HasValue)
                dto.filter.PeriodId = _userContext.PeriodId.Value;

            if (dto.filter.ReportLevel == 3)
                dto.TurnoverAccounts = await _ser.TurnoverAccounts_TafsilAsync(dto.filter);
            else if (dto.filter.ReportLevel == 2)
                dto.TurnoverAccounts = await _ser.TurnoverAccounts_MoeinAsync(dto.filter);

            ViewBag.SellerName = user.ActiveSellerName;
            ViewBag.PeriodName = user.ActiveSellerPeriodName;
            return View(dto);
        }

        public async Task<IActionResult> TafsilGroupedReport(HomePageViewModel model)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }

            model.TafsilReport.filter.PeriodId = _userContext.PeriodId.Value;
            model.TafsilReport.filter.SellerId = _userContext.SellerId.Value;


            ViewBag.Kols = await _baseData.SelectList_UsedKolsByTafsilAsync(model.TafsilReport.filter.SellerId, null);
            ViewBag.Moeins = await _baseData.SelectListUsedMoeinsByKolsAsync(model.TafsilReport.filter.SellerId, null);
            ViewBag.Tafsils = await _baseData.SelectList_UsageTafsilsAsync(model.TafsilReport.filter.SellerId);
            var vm = await _ser.GetTafsilTurnoverAsync(model.TafsilReport.filter);

            return View(vm);
        }

        public async Task<IActionResult> TafsilGroupedReport_print(HomePageViewModel model)
        {
            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }

            model.TafsilReport.filter.PeriodId = _userContext.PeriodId.Value;
            model.TafsilReport.filter.SellerId = _userContext.SellerId.Value;


            ViewBag.Kols = await _baseData.SelectList_UsedKolsByTafsilAsync(model.TafsilReport.filter.SellerId, null);
            ViewBag.Moeins = await _baseData.SelectListUsedMoeinsByKolsAsync(model.TafsilReport.filter.SellerId, null);
            ViewBag.Tafsils = await _baseData.SelectList_UsageTafsilsAsync(model.TafsilReport.filter.SellerId);
            var vm = await _ser.GetTafsilTurnoverAsync(model.TafsilReport.filter);

            return View(vm);
        }


    }
}
