using ClosedXML.Excel;
using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Areas.Treasury.Dto;
using GarnetAccounting.Areas.Treasury.TreasuryInterfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Authorize(Roles = "AccountingManager,AccountingBoss,AccountingUser")]
    [Area("Accounting")]
    public class AssistantsController : Controller
    {
        private protected IAccAsistantsService _assistant;
        private protected ITreBaseService _treBaseService;
        private readonly UserContextService _user;
        private readonly ITreBankImporterService _bankImporter;
        private readonly IAccCodingService _coding;
        private readonly IAccountingReportService _ser;
        public AssistantsController(IAccAsistantsService assistant
            , UserContextService UserService
            , ITreBaseService TreasuryBaseService
            , ITreBankImporterService bankImporter,
              IAccCodingService coding,
              IAccountingReportService ser)
        {
            _assistant = assistant;
            _user = UserService;
            _treBaseService = TreasuryBaseService;
            _bankImporter = bankImporter;
            _coding = coding;
            _ser = ser;
        }


        //----------------- Moadian ---------------------------------- Moadian ------------------------------------- Moadian -------------

        [HttpGet]
        public async Task<IActionResult> MoadianReports(MoadianReportFilterDto filter)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
                return BadRequest("درخواست نامعتبر !!");

            filter.SellerId = _user.SellerId.Value;
            ImportMoadianViewModel model = new ImportMoadianViewModel();

            if (filter.ShowAll)
            {
                filter.Ischecked = null;
            }
            var data = _assistant.GetMoadianReport(filter);
            model.Data = Pagination<Acc_MoadianReport>.Create(data, filter.CurrentPage, filter.PageSize);
            model.filter = filter;

            ViewBag.InvoiceStatuse = _assistant.SelectList_MoadianInvoiceStatuses();
            return View(model);
        }
        [HttpGet]
        public IActionResult ImportMoadianReports()
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue) return BadRequest("دوره مالی فعال شناسایی نشد");

            MoadianImporterDto model = new MoadianImporterDto();
            model.PeriodId = _user.PeriodId.Value;
            model.SellerId = _user.SellerId.Value;

            return PartialView("_ImportMoadianReports", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportMoadianReports(MoadianImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue) return NoContent();

            result = await _assistant.insertMoadianReportFromExcelAsync(dto);

            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
            }
            return Json(result.ToJsonResult());
        }
        [HttpGet]
        public IActionResult InsertMoadianReport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertMoadianReport(ExcelImportDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue) return NoContent();
            BulkDocDto ReportToDoc = new BulkDocDto();
            if (dto.Reporttype == 1)
            {
                var report = await _assistant.ReadMoadianReportFromExcelAsync(dto.File);
                ReportToDoc = await _assistant.PreparingToCreateSaleMoadianDocAsync(report, dto.AppendDoc, _user.SellerId.Value, _user.PeriodId.Value, User.Identity.Name);

            }
            else if (dto.Reporttype == 2)
            {
                var report = await _assistant.ReadMoadianReportFromExcelAsync(dto.File);
                ReportToDoc = await _assistant.PreparingToCreateBuyMoadianDocAsync(report, dto.AppendDoc, _user.SellerId.Value, _user.PeriodId.Value, User.Identity.Name);
            }
            else if (dto.Reporttype == 3)
            {
                var report = await _assistant.ReadGarnetMoadianFromExcelAsync(dto.File);
                ReportToDoc = await _assistant.PreparingToCreateGarnetMoadianDailyDocAsync(report, true, _user.SellerId.Value, _user.PeriodId.Value, User.Identity.Name);
            }

            if (!ReportToDoc.Success)
            {
                result.Message = ReportToDoc.Message;
                result.ShowMessage = true;
                return Json(result.ToJsonResult());
            }

            result = await _assistant.InsertBulkDocsAsync(ReportToDoc);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
            }
            return Json(result.ToJsonResult());
        }

        //----------------- Bank ---------------------------------- Bank ------------------------------------- Bank -------------
        public async Task<IActionResult> BankTransactions(BankReportFilterDto filter)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue) return NoContent();

            BankTransactionViewModel model = new BankTransactionViewModel();
            filter.SellerId = _user.SellerId.Value;
            if (filter.ShowAll)
            {
                filter.HasDoc = null;
                filter.IsChecked = null;
            }
            model.filter = filter;

            if (!string.IsNullOrEmpty(filter?.strFromDate))
                model.filter.FromDate = filter.strFromDate.PersianToLatin();
            if (!string.IsNullOrEmpty(filter?.strToDate))
                model.filter.ToDate = filter.strToDate.PersianToLatin();

            model.transactions = await _bankImporter.GetBankTransactionsAsync(filter);
            ViewBag.BankAccounts = await _treBaseService.SelectList_BankAccountsAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ImportBankTransaction()
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue) return NoContent();
            ViewBag.BankAccounts = await _treBaseService.SelectList_BankAccountsAsync();
            ViewBag.Banks = _bankImporter.Select_list_BankReportType();
            BankImporterDto model = new BankImporterDto();
            model.SellerId = _user.SellerId.Value;
            return PartialView("_ImportBankTransaction", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportBankTransaction(BankImporterDto model)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (ModelState.IsValid)
            {
                switch (model.Pattern)
                {
                    case 10:
                        result = await _bankImporter.ImportSamanAsync(model);
                        break;
                    case 11:
                        result = await _bankImporter.ImportBankSamanSadidAsync(model);
                        break;
                    case 20:
                        result = await _bankImporter.ImportTejaratAsync(model);
                        break;
                    case 21:
                        result = await _bankImporter.ImportTejaratInternetBankAsync(model);
                        break;
                    case 22:
                        result = await _bankImporter.ImportBankTejarat3Async(model);
                        break;
                    case 30:
                        result = await _bankImporter.ImportMelatAsync(model);
                        break;
                    case 31:
                        result = await _bankImporter.ImportMelat12Async(model);
                        break;
                    case 40:
                        result = await _bankImporter.ImportEghtesadNovinAsync(model);
                        break;
                    case 41:
                        result = await _bankImporter.ImportEghtesadInternetBankAsync(model);
                        break;
                    case 42:
                        result = await _bankImporter.ImportEghtesadInternetBank2Async(model);
                        break;
                    case 50:
                        result = await _bankImporter.ImportKeshavarziAsync(model);
                        break;
                    case 60:
                        result = await _bankImporter.ImportRefahJariAsync(model);
                        break;
                    case 61:
                        result = await _bankImporter.ImportRefahJari2Async(model);
                        break;
                    case 70:
                        result = await _bankImporter.ImportCityBankAsync(model);
                        break;
                    case 80:
                        result = await _bankImporter.ImportPostBankAsync(model);
                        break;
                    case 90:
                        result = await _bankImporter.ImportSaderat_SepehrAsync(model);
                        break;
                    case 91:
                        result = await _bankImporter.ImportSaderatAsync(model);
                        break;
                    case 100:
                        result = await _bankImporter.ImportSepahAsync(model);
                        break;
                    case 102:
                        result = await _bankImporter.ImportSepahJariAsync(model);
                        break;
                    case 103:
                        result = await _bankImporter.ImportSepah_InternetBankAsync(model);
                        break;
                    case 200:
                        result = await _bankImporter.ImportBankMeliAsync(model);
                        break;
                    case 201:
                        result = await _bankImporter.ImportBankMeli_InternetBankAsync(model);
                        break;
                    case 202:
                        result = await _bankImporter.ImportBankMeli3Async(model);
                        break;
                    case 300:
                        result = await _bankImporter.ImportPasargadAsync(model);
                        break;
                    case 400:
                        result = await _bankImporter.ImportParsianAsync(model);
                        break;
                    case 410:
                        result = await _bankImporter.ImportSinaAsync(model);
                        break;
                    default:
                        break;
                }

                if (result.Success)
                {
                    result.updateType = 2;
                    result.returnUrl = "";// Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var e in errors)
            {
                result.Message += "\n" + e.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> saveTransactionAsChecked(List<long> items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            result = await _assistant.BankTransactionSaveAsCheckedAsync(items);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> AddNoteToTransaction(long Id)
        {
            var model = await _assistant.GetBankTransactionByIdAsync(Id);
            return PartialView("_AddNoteToTransaction", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNoteToTransaction(BankTransactionEditDto model)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            result = await _assistant.BankEditTransactionUserCommentAsync(model);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        //TransactionCheckToggle
        [HttpPost]
        public async Task<IActionResult> TransactionCheckToggle(long id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            result = await _assistant.BankTransactionCheckTogleAsync(id);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        public async Task<IActionResult> BankTransactionEportToExcel(BankReportFilterDto filter)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue) return NoContent();

            BankTransactionViewModel model = new BankTransactionViewModel();
            filter.SellerId = _user.SellerId.Value;
            if (filter.ShowAll)
            {
                filter.HasDoc = null;
                filter.IsChecked = null;
            }

            if (!string.IsNullOrEmpty(filter?.strFromDate))
                filter.FromDate = filter.strFromDate.PersianToLatin();
            if (!string.IsNullOrEmpty(filter?.strToDate))
                filter.ToDate = filter.strToDate.PersianToLatin();

            model.filter = filter;
            model.transactions = await _bankImporter.GetBankTransactionsAsync(filter);
            if (model.transactions == null || model.transactions.Count <= 0)
                return View("BankTransactions");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Transaction");
                worksheet.RightToLeft = true;

                worksheet.Cell(1, 1).Value = "ردیف";
                worksheet.Cell(1, 2).Value = "تاریخ";
                worksheet.Cell(1, 3).Value = "ساعت";
                worksheet.Cell(1, 4).Value = "پیگیری";
                worksheet.Cell(1, 5).Value = "نوع عملیات";
                worksheet.Cell(1, 6).Value = "کد عملیات";
                worksheet.Cell(1, 7).Value = "شرح";
                worksheet.Cell(1, 8).Value = "شرح کاربر";
                worksheet.Cell(1, 9).Value = "واریز";
                worksheet.Cell(1, 10).Value = "برداشت";
                worksheet.Cell(1, 11).Value = "مانده";

                var headerRange = worksheet.Range(1, 1, 1, 11);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                int row = 2;
                foreach (var z in model.transactions)
                {
                    worksheet.Cell(row, 1).Value = row;
                    worksheet.Cell(row, 2).Value = z.Date?.LatinToPersian();
                    worksheet.Cell(row, 3).Value = z.Time;
                    worksheet.Cell(row, 4).Value = z.DocumentNumber;
                    worksheet.Cell(row, 5).Value = z.Operation;
                    worksheet.Cell(row, 6).Value = z.OperationCode;
                    worksheet.Cell(row, 7).Value = z.Description;
                    worksheet.Cell(row, 8).Value = z.Note;
                    worksheet.Cell(row, 9).Value = z.Debtor;
                    worksheet.Cell(row, 10).Value = z.Creditor;
                    worksheet.Cell(row, 11).Value = z.Balance;


                    worksheet.Cell(row, 9).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 10).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 11).Style.NumberFormat.Format = "#,##0";

                    row++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var excelData = stream.ToArray();

                    // بازگشت فایل اکسل به صورت FileContentResult برای دانلود
                    return new FileContentResult(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "Garnet Invoices list.xlsx"
                    };
                }

            }


        }
        //
        [HttpPost]
        public async Task<IActionResult> DeleteTransactions(List<long> items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            result = await _assistant.DeleteBankTransactionAsync(items);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMoadian(List<long> items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            result = await _assistant.DeleteMoadianAsync(items);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }

        //-------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------- Moving Account -----
        //-------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> MovingAccountsList(DocReportDto dto)
        {
            bool getReport = true;
            if (dto.filter == null)
            {
                getReport = false;
                dto.filter = new DocFilterDto();
                dto.filter.PageSize = 50;
                dto.filter.CurrentPage = 1;
            }

            if (_user.SellerId == null || _user.PeriodId == null)
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";

            dto.filter.SellerId = _user.SellerId.Value;
            dto.filter.PeriodId = 0;
            if (_user.PeriodId.HasValue)
                dto.filter.PeriodId = _user.PeriodId.Value;


            if (dto.filter.FromBody)
            {
                var data = _ser.GetArticlesQuery(dto.filter);
                dto.ArticlesPagin = Pagination<DocArticleDto>.Create(data, dto.filter.CurrentPage, dto.filter.PageSize);
            }


            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(dto.filter.SellerId);
            ViewBag.Tafsils = await _coding.SelectList_UsageTafsilsAsync(dto.filter.SellerId);
            ViewBag.Tafsils5 = await _coding.SelectList_UsageTafsils5Async(dto.filter.SellerId);
            ViewBag.Tafsils6 = await _coding.SelectList_UsageTafsils6Async(dto.filter.SellerId);
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> ConvertMoeinAccounts(List<Guid> items)
        {
            if (_user.SellerId == null || _user.PeriodId == null)
                return BadRequest("سال مالی یا شرکت فعال شناسایی نشد");

            ConvertAccountsDto model = new ConvertAccountsDto();
            model.ArticleIds = items;
            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(_user.SellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(_user.SellerId.Value);

            return PartialView("_ConvertMoeinAccounts", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConvertAccounts(ConvertAccountsDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (dto.Tafsil4Action == 3 && dto.Tafsil4Id == 0)
            {
                result.Message = "اگر قصد تغییر حساب تفصیلی 4 را دارید، باید حساب موردنظرتان را مشخص کنید";
                return Json(result.ToJsonResult());
            }
            if (dto.Tafsil5Action == 3 && dto.Tafsil5Id == 0)
            {
                result.Message = "اگر قصد تغییر حساب تفصیلی 5 را دارید، باید حساب موردنظرتان را مشخص کنید";
                return Json(result.ToJsonResult());
            }

            result = await _assistant.ConvertAccountsAsync(dto);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }


        //------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------- Moadian -----
        //------------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult GetMoadianSaleData(DocFilterDto filter)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue) return NoContent();
            filter.SellerId = _user.SellerId.Value;
            filter.PeriodId = _user.PeriodId.Value;

            return View(filter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportMoadianBill(DocFilterDto filter)
        {
            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
                return BadRequest("خطا در شناسایی لایسنس");

            var bills = await _assistant.CreateMoadianReport(filter);
            var party = bills.FirstOrDefault();
            if (bills == null || bills.Count == 0)
                return NotFound("اطلاعاتی جهت استخراج یافت نشد.");

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("صورتحساب‌ها");

            worksheet.Cell(1, 1).Value = "کد صورتحساب در سیستم حسابداری";
            worksheet.Cell(1, 2).Value = "شماره صورتحساب";
            worksheet.Cell(1, 3).Value = "تاریخ صورتحساب";
            worksheet.Cell(1, 4).Value = "نوع صورتحساب";
            worksheet.Cell(1, 5).Value = "نام کامل خریدار";
            worksheet.Cell(1, 6).Value = "نوع شخص حقیقی یا حقوقی";
            worksheet.Cell(1, 7).Value = "شماره / شناسه ملی";
            worksheet.Cell(1, 8).Value = "کد اقتصادی جدید";
            worksheet.Cell(1, 9).Value = "کدپستی";
            worksheet.Cell(1, 10).Value = "آدرس";
            worksheet.Cell(1, 11).Value = "کالا / خدمت";
            worksheet.Cell(1, 12).Value = "شناسه 13 رقمی کالا یا خدمت";
            worksheet.Cell(1, 13).Value = "شرح کالا یا خدمت";
            worksheet.Cell(1, 14).Value = "کد واحد اندازه گیری کالا یا خدمت";
            worksheet.Cell(1, 15).Value = "قیمت واحد (فی)";
            worksheet.Cell(1, 16).Value = "تعداد";
            worksheet.Cell(1, 17).Value = "تخفیف";
            worksheet.Cell(1, 18).Value = "نرخ ارزش افزوده";
            worksheet.Cell(1, 19).Value = "مبلغ ارزش افزوده";
            worksheet.Cell(1, 20).Value = "نوع تسویه حساب";

            var row = 2;
            foreach (var bill in bills)
            {
                worksheet.Cell(row, 1).Value = bill.AccountingInvoiceCode;
                worksheet.Cell(row, 2).Value = bill.InvoiceNumber;
                worksheet.Cell(row, 3).Value = bill.InvoiceDate;
                worksheet.Cell(row, 4).Value = bill.InvoiceType;
                worksheet.Cell(row, 5).Value = bill.BuyerFullName;
                worksheet.Cell(row, 6).Value = bill.BuyerType;
                worksheet.Cell(row, 7).Value = bill.NationalId;
                worksheet.Cell(row, 8).Value = bill.EconomicCode;
                worksheet.Cell(row, 9).Value = bill.PostalCode;
                worksheet.Cell(row, 10).Value = bill.Address;
                worksheet.Cell(row, 11).Value = bill.IsService;
                worksheet.Cell(row, 12).Value = bill.ItemIdentifier13;
                worksheet.Cell(row, 13).Value = bill.ItemDescription;
                worksheet.Cell(row, 14).Value = bill.UnitCode;
                worksheet.Cell(row, 15).Value = bill.TotalPriceAfterDiscount;
                worksheet.Cell(row, 16).Value = bill.Quantity;
                worksheet.Cell(row, 17).Value = bill.Discount;
                worksheet.Cell(row, 18).Value = bill.VATRate;
                worksheet.Cell(row, 19).Value = bill.VatPrice;
                worksheet.Cell(row, 20).Value = bill.SettlementType;
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"گزارش صورتحساب-مودیان-{party.BuyerFullName}.xlsx";

            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
        }

        //---- MergeDocs Day To Day

        [HttpGet]
        public IActionResult DocMergeDayToDay()
        {
            return PartialView("_DocMergeDayToDay");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoDocMergeDayToDay()
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (!_user.SellerId.HasValue || !_user.PeriodId.HasValue)
            {
                result.Message = "شرکت/سال مالی فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }

            result = await _assistant.MergeDocDaytodayAsync(_user.SellerId.Value, _user.PeriodId.Value);
            return Json(result.ToJsonResult());
        }

    }
}


