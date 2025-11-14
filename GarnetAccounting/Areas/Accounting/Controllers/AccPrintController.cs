using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;


namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    [Authorize]
    public class AccPrintController : Controller
    {
        private readonly IAccOperationService _acc;
        private readonly IAccountingReportService _reportService;
        private readonly IGeneralService _gs;
        private readonly IAccCodingService _Cod;
        private readonly IWebHostEnvironment _env;
        private readonly IAccSettingService _accountingSettings;
        private readonly UserContextService _userContext;
        public AccPrintController(IAccOperationService AccountingService
            , IAccountingReportService ser
            , IGeneralService gs
            , IAccCodingService codingService
            , IWebHostEnvironment env,
              IAccSettingService accountingSettings
            , UserContextService userContextService)
        {
            _acc = AccountingService;
            _reportService = ser;
            _gs = gs;
            _Cod = codingService;
            _env = env;
            _accountingSettings = accountingSettings;
            _userContext = userContextService;

            StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkgpgFGkUl79uxVs8X+uspx6K+tqdtOB5G1S6PFPRrlVNvMUiSiNYl724EZbrUAWwAYHlGLRbvxMviMExTh2l9xZJ2xc4K1z3ZVudRpQpuDdFq+fe0wKXSKlB6okl0hUd2ikQHfyzsAN8fJltqvGRa5LI8BFkA/f7tffwK6jzW5xYYhHxQpU3hy4fmKo/BSg6yKAoUq3yMZTG6tWeKnWcI6ftCDxEHd30EjMISNn1LCdLN0/4YmedTjM7x+0dMiI2Qif/yI+y8gmdbostOE8S2ZjrpKsgxVv2AAZPdzHEkzYSzx81RHDzZBhKRZc5mwWAmXsWBFRQol9PdSQ8BZYLqvJ4Jzrcrext+t1ZD7HE1RZPLPAqErO9eo+7Zn9Cvu5O73+b9dxhE2sRyAv9Tl1lV2WqMezWRsO55Q3LntawkPq0HvBkd9f8uVuq9zk7VKegetCDLb0wszBAs1mjWzN+ACVHiPVKIk94/QlCkj31dWCg8YTrT5btsKcLibxog7pv1+2e4yocZKWsposmcJbgG0";
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
        //چاپ سند حسابداری

        public async Task<IActionResult> PrintDocOption(Guid id, int docNo)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett != null && userSett.ActiveSellerId.HasValue)
            {
                var setting = await _accountingSettings.GetSettingAsync(userSett.ActiveSellerId.Value);
                ViewBag.printLevel = setting.DocPrintDefault;
            }

            ViewBag.DocId = id;
            ViewBag.DocNumber = docNo;
            return PartialView("_PrintDocOption");
        }

        public IActionResult PrintDoc(Guid id, int? level = null)
        {
            return View();
        }
        public async Task<IActionResult> GetReport_PrintDoc(Guid id, int? level = null)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null) { return BadRequest(); }
            var accSetting = await _accountingSettings.GetSettingAsync(userSett.ActiveSellerId.Value);

            StiReport report = new StiReport();
            var doc = await _acc.GetDocPrintAsync(id);
            string path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrint.mrt");
            switch (level)
            {
                case 1:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrintKol.mrt");
                    doc = await _acc.GetDocPrintKolAsync(id);
                    break;
                case 2:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrintMoein.mrt");
                    doc = await _acc.GetDocPrintMoeinAsync(id);
                    break;
                case 3:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrint.mrt");
                    doc = await _acc.GetDocPrintAsync(id);
                    break;
                default:
                    break;
            }
            var header = doc.Header;
            var articles = doc.Articles;

            report.Load(path);
            report.RegData("header", header);
            report.RegData("articles", articles);

            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            //Doc Footer
            string Auther = accSetting.PrintCreator == true ? header.Auther : "";
            StiVariable auther = new StiVariable("Auther", Auther);
            StiVariable Approver1Title = new StiVariable("Approver1Title", string.IsNullOrEmpty(accSetting.Approver1Title) ? "" : accSetting.Approver1Title);
            StiVariable Approver1Name = new StiVariable("Approver1Name", string.IsNullOrEmpty(accSetting.Approver1Name) ? "" : accSetting.Approver1Name);
            StiVariable Approver2Title = new StiVariable("Approver2Title", string.IsNullOrEmpty(accSetting.Approver2Title) ? "" : accSetting.Approver2Title);
            StiVariable Approver2Name = new StiVariable("Approver2Name", string.IsNullOrEmpty(accSetting.Approver2Name) ? "" : accSetting.Approver2Name);
            report.Dictionary.Variables.Add(auther);
            report.Dictionary.Variables.Add(Approver1Title);
            report.Dictionary.Variables.Add(Approver1Name);
            report.Dictionary.Variables.Add(Approver2Title);
            report.Dictionary.Variables.Add(Approver2Name);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        //-----
        public IActionResult PrintDocLvl4(Guid id, int? level = null)
        {
            return View();
        }
        public async Task<IActionResult> GetReport_PrintDocLvl4(Guid id)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null) { return BadRequest(); }
            var accSetting = await _accountingSettings.GetSettingAsync(userSett.ActiveSellerId.Value);

            StiReport report = new StiReport();
            var doc = await _acc.GetStructuredDocPrintAsync(id);
            string path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrintLvl4.mrt");

            var header = doc.Header;
            var kols = doc.KolGroups;
            var moeins = doc.KolGroups.SelectMany(n => n.MoeinGroups).ToList();
            var tafsils = doc.KolGroups.SelectMany(n => n.MoeinGroups).SelectMany(n => n.TafsilDetails).ToList();


            report.Load(path);
            report.RegData("header", header);
            report.RegData("kols", kols);
            report.RegData("Moeins", moeins);
            report.RegData("Tafsils", tafsils);


            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            //Doc Footer
            string Auther = accSetting.PrintCreator == true ? header.Auther : "";
            StiVariable auther = new StiVariable("Auther", Auther);
            StiVariable Approver1Title = new StiVariable("Approver1Title", string.IsNullOrEmpty(accSetting.Approver1Title) ? "" : accSetting.Approver1Title);
            StiVariable Approver1Name = new StiVariable("Approver1Name", string.IsNullOrEmpty(accSetting.Approver1Name) ? "" : accSetting.Approver1Name);
            StiVariable Approver2Title = new StiVariable("Approver2Title", string.IsNullOrEmpty(accSetting.Approver2Title) ? "" : accSetting.Approver2Title);
            StiVariable Approver2Name = new StiVariable("Approver2Name", string.IsNullOrEmpty(accSetting.Approver2Name) ? "" : accSetting.Approver2Name);
            report.Dictionary.Variables.Add(auther);
            report.Dictionary.Variables.Add(Approver1Title);
            report.Dictionary.Variables.Add(Approver1Name);
            report.Dictionary.Variables.Add(Approver2Title);
            report.Dictionary.Variables.Add(Approver2Name);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        //...................................................
        public async Task<IActionResult> PrintDocPdf(Guid id)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null)
            {
                return BadRequest();
            }
            var accSetting = await _accountingSettings.GetSettingAsync(userSett.ActiveSellerId.Value);
            var doc = await _acc.GetDocPrintAsync(id);
            var header = doc.Header;
            var articles = doc.Articles;

            StiReport report = new StiReport();
            var path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrint.mrt");
            report.Load(path);
            report.RegData("header", header);
            report.RegData("articles", articles);



            //Doc Footer
            string Auther = accSetting.PrintCreator == true ? header.Auther : "";
            StiVariable auther = new StiVariable("Auther", Auther);
            StiVariable Approver1Title = new StiVariable("Approver1Title", string.IsNullOrEmpty(accSetting.Approver1Title) ? "" : accSetting.Approver1Title);
            StiVariable Approver1Name = new StiVariable("Approver1Name", string.IsNullOrEmpty(accSetting.Approver1Name) ? "" : accSetting.Approver1Name);
            StiVariable Approver2Title = new StiVariable("Approver2Title", string.IsNullOrEmpty(accSetting.Approver2Title) ? "" : accSetting.Approver2Title);
            StiVariable Approver2Name = new StiVariable("Approver2Name", string.IsNullOrEmpty(accSetting.Approver2Name) ? "" : accSetting.Approver2Name);
            report.Dictionary.Variables.Add(auther);
            report.Dictionary.Variables.Add(Approver1Title);
            report.Dictionary.Variables.Add(Approver1Name);
            report.Dictionary.Variables.Add(Approver2Title);
            report.Dictionary.Variables.Add(Approver2Name);

            // Set variables
            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);

            // Generate report and export to PDF
            var stream = new MemoryStream();
            await report.ExportDocumentAsync(StiExportFormat.Pdf, stream);
            stream.Position = 0;


            // Return the PDF as a file to be displayed in the browser
            string filaName = $"سند شماره {header.DocNumber}.pdf";
            return File(stream, "application/pdf", "doc.pdf");
        }
        //..................................................................
        public async Task<IActionResult> PrintDocsOption(List<Guid> ids, int? level = null)
        {
            var model = new DocPrintOptionDto
            {
                DocIds = ids,

            };
            model.PrintLevel = 1;
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett != null && userSett.ActiveSellerId.HasValue)
            {
                var setting = await _accountingSettings.GetSettingAsync(userSett.ActiveSellerId.Value);
                model.PrintLevel = (int)setting.DocPrintDefault;
            }
            return PartialView("_PrintDocsOption", model);
        }
        public IActionResult PrintDocs(List<Guid> DocIds, int? PrintLevel = null)
        {
            return View();
        }
        public async Task<IActionResult> GetReport_PrintDocs_old(List<Guid> DocIds, int? PrintLevel = null)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            var accSetting = await _accountingSettings.GetSettingAsync(userSett.ActiveSellerId.Value);

            StiReport rp = new StiReport();
            string path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrint.mrt");
            switch (PrintLevel)
            {
                case 1:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrintKol.mrt");
                    break;
                case 2:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrintMoein.mrt");
                    break;
                case 3:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrint.mrt");
                    break;
                default:
                    break;
            }
            rp.Load(path);
            await rp.CompileAsync();
            //.....

            StiReport report = new StiReport();
            report.NeedsCompiling = false;
            report.Culture = "fd-IR";
            await report.RenderAsync();
            report.RenderedPages.Clear();
            Stimulsoft.Report.Units.StiUnit newUnit = Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(report.ReportUnit);


            foreach (var id in DocIds)
            {
                var doc = await _acc.GetDocPrintAsync(id);
                switch (PrintLevel)
                {
                    case 1:
                        doc = await _acc.GetDocPrintKolAsync(id);
                        break;
                    case 2:
                        doc = await _acc.GetDocPrintMoeinAsync(id);
                        break;
                }

                var header = doc.Header;
                var articles = doc.Articles;
                rp.RegData("header", header);
                rp.RegData("articles", articles);

                // 🔹 ابتدا متغیرهای عمومی را اضافه کن
                rp.Dictionary.Variables.Clear();
                rp.Dictionary.Variables.Add(new StiVariable("CompanyName", userSett.ActiveSellerName));
                rp.Dictionary.Variables.Add(new StiVariable("ReportDate", DateTime.Now.LatinToPersian()));

                // 🔹 سپس رندر کن
                rp.Render(false);

                // 🔹 سپس متغیرهای پایین گزارش را برای گزارش اصلی تنظیم کن
                string Auther = accSetting.PrintCreator == true ? header.Auther : "";
                report.Dictionary.Variables.Add(new StiVariable("Auther", Auther));
                report.Dictionary.Variables.Add(new StiVariable("Approver1Title", accSetting.Approver1Title ?? ""));
                report.Dictionary.Variables.Add(new StiVariable("Approver1Name", accSetting.Approver1Name ?? ""));
                report.Dictionary.Variables.Add(new StiVariable("Approver2Title", accSetting.Approver2Title ?? ""));
                report.Dictionary.Variables.Add(new StiVariable("Approver2Name", accSetting.Approver2Name ?? ""));

                foreach (StiPage page in rp.RenderedPages)
                {
                    page.Report = report;
                    page.NewGuid();
                    page.Convert(
                        Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(rp.ReportUnit),
                        Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(report.ReportUnit)
                    );
                    report.RenderedPages.Add(page);
                }
            }


            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public async Task<IActionResult> GetReport_PrintDocs(List<Guid> DocIds, int? PrintLevel = null)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            var accSetting = await _accountingSettings.GetSettingAsync(userSett.ActiveSellerId.Value);

            StiReport rp = new StiReport();
            string path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrint.mrt");
            switch (PrintLevel)
            {
                case 1:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrintKol.mrt");
                    break;
                case 2:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrintMoein.mrt");
                    break;
                case 3:
                    path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/DocPrint.mrt");
                    break;
                default:
                    break;
            }
            rp.Load(path);

            // اضافه کردن متغیرها قبل از کامپایل
            rp.Dictionary.Variables.Add("CompanyName", userSett.ActiveSellerName);
            rp.Dictionary.Variables.Add("ReportDate", DateTime.Now.LatinToPersian());

            await rp.CompileAsync();

            StiReport report = new StiReport();
            report.NeedsCompiling = false;
            report.Culture = "fd-IR";

            // اضافه کردن متغیرهای فوتر به گزارش اصلی
            string Auther = accSetting.PrintCreator == true ? "" : ""; // مقدار اولیه
            report.Dictionary.Variables.Add("Auther", Auther);
            report.Dictionary.Variables.Add("Approver1Title",
                string.IsNullOrEmpty(accSetting.Approver1Title) ? "" : accSetting.Approver1Title);
            report.Dictionary.Variables.Add("Approver1Name",
                string.IsNullOrEmpty(accSetting.Approver1Name) ? "" : accSetting.Approver1Name);
            report.Dictionary.Variables.Add("Approver2Title",
                string.IsNullOrEmpty(accSetting.Approver2Title) ? "" : accSetting.Approver2Title);
            report.Dictionary.Variables.Add("Approver2Name",
                string.IsNullOrEmpty(accSetting.Approver2Name) ? "" : accSetting.Approver2Name);

            await report.RenderAsync();
            report.RenderedPages.Clear();
            Stimulsoft.Report.Units.StiUnit newUnit =
                Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(report.ReportUnit);

            foreach (var id in DocIds)
            {
                var doc = await _acc.GetDocPrintAsync(id);
                switch (PrintLevel)
                {
                    case 1:
                        doc = await _acc.GetDocPrintKolAsync(id);
                        break;
                    case 2:
                        doc = await _acc.GetDocPrintMoeinAsync(id);
                        break;
                    case 3:
                        doc = await _acc.GetDocPrintAsync(id);
                        break;
                    default:
                        break;
                }

                var header = doc.Header;
                var articles = doc.Articles;

                // آپدیت متغیر Auther برای هر سند
                Auther = accSetting.PrintCreator == true ? header.Auther : "";
                report.Dictionary.Variables["Auther"].Value = Auther;

                rp.RegData("header", header);
                rp.RegData("articles", articles);

                // رندر بعد از تنظیم همه متغیرها و داده‌ها
                rp.Render(false);

                foreach (StiPage page in rp.RenderedPages)
                {
                    page.Report = report;
                    page.NewGuid();
                    Stimulsoft.Report.Units.StiUnit oldUnit =
                        Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(rp.ReportUnit);
                    if (report.ReportUnit != rp.ReportUnit)
                        page.Convert(oldUnit, newUnit);
                    report.RenderedPages.Add(page);
                }

                // پاک کردن صفحات rp برای سند بعدی
                rp.RenderedPages.Clear();
            }

            return StiNetCoreViewer.GetReportResult(this, report);
        }
        //دفتر روزنامه
        public IActionResult DaftarRooznameh()
        {
            return PartialView("_DaftarRooznameh");
        }
        public IActionResult Print_DaftarRooznameh()
        {
            return View();
        }
        public async Task<IActionResult> GetReport_Print_DaftarRooznameh(int RowsCount)
        {


            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);

            var data = await _reportService.DafarRooznamehAsync(userSett.ActiveSellerId.Value, userSett.ActiveSellerPeriod.Value, RowsCount);

            StiReport report = new StiReport();

            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/acc_dafterRooznameh.mrt");

            report.Load(path);
            //report.RegData("data", data);
            report.RegBusinessObject("data", data);

            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriodName", userSett.ActiveSellerPeriodName);
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);

            return StiNetCoreViewer.GetReportResult(this, report);
        }
        //دفتر کل
        public IActionResult DaftarKol()
        {
            return PartialView("_DaftarKol");
        }
        public IActionResult Print_DaftarKol()
        {
            return View();
        }
        public async Task<IActionResult> GetReport_Print_DaftarKol(int RowsCount)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);

            var data = await _reportService.DaftarKolAsync(userSett.ActiveSellerId.Value, userSett.ActiveSellerPeriod.Value, RowsCount);

            StiReport report = new StiReport();

            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_PrintKol.mrt");

            report.Load(path);
            report.RegData("DaftarKol", data);

            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", userSett.ActiveSellerPeriodName);
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        // -------------------------------------------------------------------------------------------------------------- browser  -------------------------------
        public IActionResult Print_BrowserKol()
        {
            return View();
        }
        public async Task<IActionResult> GetReport_Print_BrowserKol(
               int ReportLevel
             , int BalanceColumnsQty
            , string? strStartDate = null
            , string? strEndDate = null
            , int? FromDocNumer = null
            , int? ToDocNumer = null)
        {

            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);

            DocFilterDto filter = new DocFilterDto();
            filter.ReportLevel = ReportLevel;
            filter.BalanceColumnsQty = BalanceColumnsQty;
            filter.FromDocNumer = FromDocNumer;
            filter.ToDocNumer = ToDocNumer;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;

            var data = await _reportService.Report_KolAsync(filter);
            StiReport report = new StiReport();
            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_AccountBrowser_Kol.mrt");
            report.Load(path);
            report.RegData("data", data);

            //Report Header
            var financePeriod = await _Cod.GetFinanceDtoAsync(userSett.ActiveSellerPeriod.Value);

            string fromDate = !string.IsNullOrEmpty(strStartDate) ? strStartDate.PersianToLatin().LatinToPersian() : financePeriod.StartDate.LatinToPersian();
            string toDate = !string.IsNullOrEmpty(strEndDate) ? $" تا {strEndDate.PersianToLatin().LatinToPersian()}" : financePeriod.EndDate.LatinToPersian();
            string title = $" گزارش تراز آزمایشی از تاریخ {fromDate} لغایت {toDate}";
            string fromDoc = "";
            string toDoc = "";
            string reportTitleDocRenge = "";
            if (filter.FromDocNumer.HasValue || filter.ToDocNumer.HasValue)
            {
                fromDoc = "از ستد 1  ";
                toDoc = " تا سند آخر";
                if (filter.FromDocNumer.HasValue)
                    fromDoc = $"از سند شماره {filter.FromDocNumer}";
                if (filter.ToDocNumer.HasValue)
                    toDoc = $" تا سند شماره {filter.ToDocNumer}";
                reportTitleDocRenge = fromDoc + " " + toDoc;
            }
            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable ReportTitle = new StiVariable("ReportTitle", title);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", userSett.ActiveSellerPeriodName);
            StiVariable varfromDoc = new StiVariable("FromDoc", reportTitleDocRenge);

            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(ReportTitle);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);
            report.Dictionary.Variables.Add(varfromDoc);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult Print_BrowserMoein()
        {
            return View();
        }
        public async Task<IActionResult> GetReport_Print_BrowserMoein(
               int ReportLevel
             , int BalanceColumnsQty
            , string? strStartDate = null
            , string? strEndDate = null
            , int? FromDocNumer = null
            , int? ToDocNumer = null
            , int? KolId = null
            , int? MoeinId = null
            , long? CurrentTafsilId = null
            , long? tafsil4Id = null
            , long? tafsil5Id = null)
        {

            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);

            DocFilterDto filter = new DocFilterDto();
            filter.ReportLevel = ReportLevel;
            filter.BalanceColumnsQty = BalanceColumnsQty;
            filter.FromDocNumer = FromDocNumer;
            filter.ToDocNumer = ToDocNumer;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.KolId = KolId;
            filter.MoeinId = MoeinId;
            filter.tafsil4Id = tafsil4Id;
            filter.tafsil5Id = tafsil5Id;
            filter.CurrentTafsilId = CurrentTafsilId;

            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;

            var data = await _reportService.Report_MoeinAsync(filter);
            StiReport report = new StiReport();
            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_AccountBrowser_Moein.mrt");
            report.Load(path);
            report.RegData("data", data);

            //Report Header
            var financePeriod = await _Cod.GetFinanceDtoAsync(userSett.ActiveSellerPeriod.Value);

            string fromDate = !string.IsNullOrEmpty(strStartDate) ? strStartDate.PersianToLatin().LatinToPersian() : financePeriod.StartDate.LatinToPersian();
            string toDate = !string.IsNullOrEmpty(strEndDate) ? $" تا {strEndDate.PersianToLatin().LatinToPersian()}" : financePeriod.EndDate.LatinToPersian();
            string title = $" گزارش تراز آزمایشی از تاریخ {fromDate} لغایت {toDate}";
            string fromDoc = "";
            string toDoc = "";
            string reportTitleDocRenge = "";
            if (filter.FromDocNumer.HasValue || filter.ToDocNumer.HasValue)
            {
                fromDoc = "از ستد 1  ";
                toDoc = " تا سند آخر";
                if (filter.FromDocNumer.HasValue)
                    fromDoc = $"از سند شماره {filter.FromDocNumer}";
                if (filter.ToDocNumer.HasValue)
                    toDoc = $" تا سند شماره {filter.ToDocNumer}";
                reportTitleDocRenge = fromDoc + " " + toDoc;
            }
            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable ReportTitle = new StiVariable("ReportTitle", title);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", userSett.ActiveSellerPeriodName);
            StiVariable varfromDoc = new StiVariable("FromDoc", reportTitleDocRenge);

            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(ReportTitle);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);
            report.Dictionary.Variables.Add(varfromDoc);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult Print_BrowserTafsil()
        {
            return View();
        }
        public async Task<IActionResult> GetReport_Print_BrowserTafsil(
               int ReportLevel
             , int BalanceColumnsQty
            , string? strStartDate = null
            , string? strEndDate = null
            , int? FromDocNumer = null
            , int? ToDocNumer = null
            , int? KolId = null
            , int? MoeinId = null
            , long? CurrentTafsilId = null
            , long? tafsil4Id = null
            , long? tafsil5Id = null)
        {

            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);

            DocFilterDto filter = new DocFilterDto();
            filter.ReportLevel = ReportLevel;
            filter.BalanceColumnsQty = BalanceColumnsQty;
            filter.FromDocNumer = FromDocNumer;
            filter.ToDocNumer = ToDocNumer;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.KolId = KolId;
            filter.MoeinId = MoeinId;
            filter.tafsil4Id = tafsil4Id;
            filter.tafsil5Id = tafsil5Id;
            filter.CurrentTafsilId = CurrentTafsilId;

            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;

            bool tafsilReport = false;
            for (int i = filter.ReportLevel.Value; i <= 8; i++)
            {
                bool checkHasData = await _reportService.HasAccountInLevelAsync(filter.SellerId, filter.PeriodId, filter.MoeinId.Value, i, filter.CurrentTafsilId);
                if (checkHasData)
                {
                    filter.ReportLevel = i;
                    tafsilReport = true;
                    if (i == 5)
                        filter.tafsil4Id = filter.CurrentTafsilId;
                    else if (i == 6)
                        filter.tafsil5Id = filter.CurrentTafsilId;
                    else if (i == 7)
                        filter.tafsil6Id = filter.CurrentTafsilId;
                    else if (i == 8)
                        filter.tafsil7Id = filter.CurrentTafsilId;
                    break;
                }
            }
            StiReport report = new StiReport();
            if (filter.ReportLevel <= 8 && filter.ReportLevel >= 3 && tafsilReport)
            {
                var data = await _reportService.Report_TafsilAsync(filter);
                var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_AccountBrowser_Tafsil.mrt");
                report.Load(path);
                report.RegData("data", data);
            }

            else
            {
                var data = await _reportService.Report_BrowserArticlesAsync(filter);
                var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_AccountBrowser_Article.mrt");
                report.Load(path);
                report.RegData("data", data);
            }


            //Report Header
            var financePeriod = await _Cod.GetFinanceDtoAsync(userSett.ActiveSellerPeriod.Value);

            string fromDate = !string.IsNullOrEmpty(strStartDate) ? strStartDate.PersianToLatin().LatinToPersian() : financePeriod.StartDate.LatinToPersian();
            string toDate = !string.IsNullOrEmpty(strEndDate) ? $" تا {strEndDate.PersianToLatin().LatinToPersian()}" : financePeriod.EndDate.LatinToPersian();
            string title = $" گزارش حساب ها از تاریخ {fromDate} لغایت {toDate}";

            string fromDoc = "";
            string toDoc = "";
            string reportTitleDocRenge = "";
            if (filter.FromDocNumer.HasValue || filter.ToDocNumer.HasValue)
            {
                fromDoc = "از ستد 1  ";
                toDoc = " تا سند آخر";
                if (filter.FromDocNumer.HasValue)
                    fromDoc = $"از سند شماره {filter.FromDocNumer}";
                if (filter.ToDocNumer.HasValue)
                    toDoc = $" تا سند شماره {filter.ToDocNumer}";
                reportTitleDocRenge = fromDoc + " " + toDoc;
            }
            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable ReportTitle = new StiVariable("ReportTitle", title);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", userSett.ActiveSellerPeriodName);
            StiVariable varfromDoc = new StiVariable("FromDoc", reportTitleDocRenge);

            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(ReportTitle);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);
            report.Dictionary.Variables.Add(varfromDoc);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        // -----------------------------------------------------------------------------------------------------------  Trial Balance --------------------
        public IActionResult Print_TrialBalance(
              int ReportLevel
             , int BalanceColumnsQty
            , string? strStartDate = null
            , string? strEndDate = null
            , int? FromDocNumer = null
            , int? ToDocNumer = null)
        {
            return View();
        }
        public async Task<IActionResult> GetReport_Print_TrialBalance(
              int ReportLevel
            , int BalanceColumnsQty
            , string? strStartDate = null
            , string? strEndDate = null
            , int? FromDocNumer = null
            , int? ToDocNumer = null)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);

            DocFilterDto filter = new DocFilterDto();
            filter.ReportLevel = ReportLevel;
            filter.BalanceColumnsQty = BalanceColumnsQty;
            filter.FromDocNumer = FromDocNumer;
            filter.ToDocNumer = ToDocNumer;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;

            List<TrialBalancePrintDto> data = new List<TrialBalancePrintDto>();
            if (BalanceColumnsQty == 4)
                data = await _reportService.GetTrialBalanceForPrintAsync(filter);
            else if (BalanceColumnsQty == 6)
                data = await _reportService.GetTrialBalance6ForPrintAsync(filter);

            StiReport report = new StiReport();
            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TrialBalance.mrt");
            if (BalanceColumnsQty == 4)
            {
                switch (ReportLevel)
                {
                    case 1:
                        path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TrialBalanceKol.mrt");
                        break;
                    case 2:
                        path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TrialBalanceMoein.mrt");
                        break;
                    case 3:
                        path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TrialBalanceTafsil.mrt");
                        break;
                    default:
                        break;
                }
            }
            else if (BalanceColumnsQty == 6)
            {
                switch (ReportLevel)
                {
                    case 1:
                        path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TrialBalanceKol6.mrt");
                        break;
                    case 2:
                        path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TrialBalanceMoein6.mrt");
                        break;
                    case 3:
                        path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TrialBalanceTafsil6.mrt");
                        break;
                    default:
                        break;
                }
            }
            report.Load(path);
            report.RegData("data", data);

            //Report Header
            var financePeriod = await _Cod.GetFinanceDtoAsync(userSett.ActiveSellerPeriod.Value);

            string fromDate = !string.IsNullOrEmpty(strStartDate) ? strStartDate.PersianToLatin().LatinToPersian() : financePeriod.StartDate.LatinToPersian();
            string toDate = !string.IsNullOrEmpty(strEndDate) ? $" تا {strEndDate.PersianToLatin().LatinToPersian()}" : financePeriod.EndDate.LatinToPersian();
            string title = $" گزارش تراز آزمایشی از تاریخ {fromDate} لغایت {toDate}";

            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable ReportTitle = new StiVariable("ReportTitle", title);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", userSett.ActiveSellerPeriodName);
            StiVariable varfromDate = new StiVariable("FromDate", fromDate);
            StiVariable vartoDate = new StiVariable("ToDate", toDate);

            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(ReportTitle);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);
            report.Dictionary.Variables.Add(varfromDate);
            report.Dictionary.Variables.Add(vartoDate);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        // Balance
        public IActionResult Print_Balance(string? strDate = null)
        {
            return View();
        }
        public async Task<IActionResult> GetReport_Print_Balance(string? strDate = null)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);

            var Data = await _reportService.GetBalance(userSett.ActiveSellerId.Value, userSett.ActiveSellerPeriod.Value, null);
            var asset = Data.Where(n => n.GroupType == 1 && n.Mandeh > 0).ToList();
            var Debt = Data.Where(n => n.GroupType != 1 && n.Mandeh > 0).ToList();
            StiReport report = new StiReport();
            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_Balance.mrt");
            report.Load(path);
            report.RegData("data", asset);
            report.RegData("Debt", Debt);

            var financePeriod = await _Cod.GetFinanceDtoAsync(userSett.ActiveSellerPeriod.Value);
            string balanceDate = financePeriod.EndDate.LatinToPersian();

            StiVariable BalanceDate = new StiVariable("BalanceDate", balanceDate);
            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", userSett.ActiveSellerPeriodName);

            report.Dictionary.Variables.Add(BalanceDate);
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);

            return StiNetCoreViewer.GetReportResult(this, report);
        }


        public async Task<IActionResult> DaftarRooznameh_Export(int RowsCount = 26)
        {
            if (!_userContext.SellerId.HasValue) return NoContent();

            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            ViewBag.SellerName = userSett.ActiveSellerName;
            var model = await _reportService.DafarRooznamehAsync(_userContext.SellerId.Value, _userContext.PeriodId.Value, RowsCount);

            return View(model);
        }

        public IActionResult PrintTurnover()
        {
            return View();
        }

        public async Task<IActionResult> GetReport_PrintTurnover()
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || !userSett.ActiveSellerPeriod.HasValue) { return BadRequest(); }

            DocFilterDto filter = new DocFilterDto
            {
                SellerId = userSett.ActiveSellerId.Value,
                PeriodId = userSett.ActiveSellerPeriod.Value
            };

            StiReport report = new StiReport();
            var data = await _reportService.GetSimpleArticlesAsync(filter);

            string path = StiNetCoreHelper.MapPath(this, @"wwwroot/Reports/acc/Acc_TurnoverTafsil.mrt");

            report.Load(path);
            report.RegData("articles", data);
            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("PeriodName", userSett.ActiveSellerPeriodName);
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult printTafsilGrouped()
        {
            return View();
        }
        public async Task<IActionResult> GetReport_printTafsilGrouped(
                                          int? TafsilGroup
                                        , List<long> Tafsil4Ids
                                        , List<long> Tafsil5Ids
                                        , List<int>? Kols
                                        , List<int>? Moeins
                                        , string? strStartDate
                                        , string? strEndDate
                                        , string? SelectedTafsilTexts
                                        , int? ProjectId
                                        , bool ShowJustWithValue
                                        , short ReportLevel
            )
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || !userSett.ActiveSellerId.HasValue || !userSett.ActiveSellerPeriod.HasValue) return NoContent();

            TafsilReportFilterDto filter = new TafsilReportFilterDto();
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;
            filter.Kols = Kols;
            filter.Moeins = Moeins;
            filter.TafsilGroup = TafsilGroup;
            filter.Tafsil4Ids = Tafsil4Ids;
            filter.Tafsil5Ids = Tafsil5Ids;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.ProjectId = ProjectId;
            filter.ShowJustWithValue = ShowJustWithValue;
            filter.ReportLevel = ReportLevel;

            var reportModel = await _reportService.GetTafsilBalaceAsync(filter);
            var reportData = reportModel.Report;
            StiReport report = new StiReport();

            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TurnoverTafsil4Grouped.mrt");
            if (ReportLevel == 5)
                path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/Acc_TurnoverTafsil5Grouped.mrt");

            report.Load(path);
            report.RegData("data", reportData);

            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", userSett.ActiveSellerPeriodName);
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);

            return StiNetCoreViewer.GetReportResult(this, report);
        }


        public IActionResult PrintArticles()
        {
            return View();
        }
        public async Task<IActionResult> GetReport_Articles(
                                         List<int>? Moeins
                                        , List<long?> Tafsil4Ids
                                        , List<long?> Tafsil5Ids
                                        , string? strStartDate
                                        , string? strEndDate
                                        , int? ProjectId

            )
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || !userSett.ActiveSellerId.HasValue || !userSett.ActiveSellerPeriod.HasValue) return NoContent();

            DocFilterDto filter = new DocFilterDto();
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;
            filter.MoeinIds = Moeins;
            filter.Tafsil4Ids = Tafsil4Ids;
            filter.Tafsil5Ids = Tafsil5Ids;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.ProjectId = ProjectId;

            var data = await _reportService.GetArticlesAsync(filter);

            var reportData = data;
            StiReport report = new StiReport();

            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/RpArticlestafsilGrouped.mrt");

            report.Load(path);
            report.RegBusinessObject("Data", reportData);

            var fp = await _Cod.GetFinanceDtoAsync(userSett.ActiveSellerPeriod.Value);
            string startDate = fp.StartDate.LatinToPersian();
            string endDate = fp.EndDate < DateTime.Now.Date ? fp.EndDate.LatinToPersian() : DateTime.Now.LatinToPersian();
            if (!string.IsNullOrEmpty(strStartDate))
                try
                {
                    startDate = strStartDate.PersianToLatin().LatinToPersian();
                }
                catch { }
            if (!string.IsNullOrEmpty(strEndDate)) { }
            try { endDate = strEndDate.PersianToLatin().LatinToPersian(); }
            catch { }

            string reportdate = $"از تاریخ {startDate} تا تاریخ {endDate}";
            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", reportdate);
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);

            return StiNetCoreViewer.GetReportResult(this, report);
        }



        //================================================================
        //====== Browser Accounts ========================================
        //================================================================
        public IActionResult PrintBrowserAccounts()
        {
            return View();
        }
        public async Task<IActionResult> GetReport_printBroswerAccount(
                                        List<int>? Moeins
                                       , List<long?> Tafsil4Ids
                                       , List<long?> Tafsil5Ids
                                       , string? strStartDate
                                       , string? strEndDate
                                       , int? ProjectId

           )
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || !userSett.ActiveSellerId.HasValue || !userSett.ActiveSellerPeriod.HasValue) return NoContent();

            DocFilterDto filter = new DocFilterDto();
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;
            filter.MoeinIds = Moeins;
            filter.Tafsil4Ids = Tafsil4Ids;
            filter.Tafsil5Ids = Tafsil5Ids;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.ProjectId = ProjectId;

            var data = await _reportService.GetArticlesAsync(filter);

            var reportData = data;
            StiReport report = new StiReport();

            var path = StiNetCoreHelper.MapPath(this, $"{_env.WebRootPath}/Reports/acc/RpArticlestafsilGrouped.mrt");

            report.Load(path);
            report.RegBusinessObject("Data", reportData);

            var fp = await _Cod.GetFinanceDtoAsync(userSett.ActiveSellerPeriod.Value);
            string startDate = fp.StartDate.LatinToPersian();
            string endDate = fp.EndDate < DateTime.Now.Date ? fp.EndDate.LatinToPersian() : DateTime.Now.LatinToPersian();
            if (!string.IsNullOrEmpty(strStartDate))
                try
                {
                    startDate = strStartDate.PersianToLatin().LatinToPersian();
                }
                catch { }
            if (!string.IsNullOrEmpty(strEndDate)) { }
            try { endDate = strEndDate.PersianToLatin().LatinToPersian(); }
            catch { }

            string reportdate = $"از تاریخ {startDate} تا تاریخ {endDate}";
            StiVariable CompanyName = new StiVariable("CompanyName", userSett.ActiveSellerName);
            StiVariable rpDate = new StiVariable("ReportDate", DateTime.Now.LatinToPersian());
            StiVariable periodName = new StiVariable("FinancePeriod", reportdate);
            report.Dictionary.Variables.Add(CompanyName);
            report.Dictionary.Variables.Add(rpDate);
            report.Dictionary.Variables.Add(periodName);

            return StiNetCoreViewer.GetReportResult(this, report);
        }




    }
}
