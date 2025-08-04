using ClosedXML.Excel;
using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.DataTransfer.DataTransferInterfaces;
using GarnetAccounting.Areas.DataTransfer.Dto;
using GarnetAccounting.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.DataTransfer.Controllers
{
    [Area("DataTransfer")]
    public class KPDataTransferController : Controller
    {
        private readonly IKPDataTransferService _dataService;
        private readonly IGeneralService _gs;
        private readonly IAccImportService _import;
        public KPDataTransferController(IKPDataTransferService dataService, IGeneralService gs, IAccImportService import)
        {
            _dataService = dataService;
            _gs = gs;
            _import = import;
        }

        [HttpGet]
        public IActionResult KpSaleImport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KpSaleImport(IFormFile ExcelFile)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                ViewBag.msg = "شرکت فعالی یافت نشد";
                return View();
            }

            long sellerId = userSett.ActiveSellerId.Value;

            if (ExcelFile == null || ExcelFile.Length == 0)
            {
                ViewBag.msg = "فرمت فایل معتبر نیست.";
                return View();
            }

            var result = await _dataService.ImportFromExcelAsync(ExcelFile, sellerId);
            if (result.Success)
            {
                return RedirectToAction("SaleReport");
            }

            ViewBag.msg = result.Message;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SaleReport(SaleFilterDto filter)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                ViewBag.msg = "شرکت فعالی یافت نشد";
                return View();
            }
            filter.SellerId = userSett.ActiveSellerId.Value;

            if (string.IsNullOrEmpty(filter.strStartDate))
                filter.strStartDate = DateTime.Now.AddDays(-2).LatinToPersian();

            if (string.IsNullOrEmpty(filter.strEndDate))
                filter.strEndDate = DateTime.Now.LatinToPersian();

            var model = new SaleReportsViewModel();
            model.filter = filter;
            model.Reports = await _dataService.GetSalesAsync(filter);
            ViewBag.reps = await _dataService.SelectList_DestinationRepresentativesAsync(userSett.ActiveSellerId.Value);
            ViewBag.agency = await _dataService.SelectList_AgencyAsync(userSett.ActiveSellerId.Value);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleDoc(long[] items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (items.Length == 0)
            {
                result.Message = "ردیفی برای ثبت سند حسابداری انتخاب نشده است";
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                ViewBag.msg = "شرکت فعالی یافت نشد";
                return Json(result.ToJsonResult());
            }

            long sellerId = userSett.ActiveSellerId.Value;
            int periodId = userSett.ActiveSellerPeriod.Value;
            var data = await _dataService.PrepareSalesForAccountingAsync(items, userSett.ActiveSellerId.Value);
            result = await _import.AddBulkKpDocsAsync(data, userSett.UserName, sellerId, periodId, 2);
            if (result.Success)
            {
                result.ShowMessage = true;
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();

            }
            return Json(result.ToJsonResult());
        }

        public async Task<IActionResult> DelInvoices(long[] items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (items.Length == 0)
            {
                result.Message = "ردیفی برای حذف انتخاب نشده است";
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            result = await _dataService.DeleteBillOfLandingsAsync(items);

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> SaleErrorsReport(SaleFilterDto filter)
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                ViewBag.msg = "شرکت فعالی یافت نشد";
                return View();
            }
            filter.SellerId = userSett.ActiveSellerId.Value;

            var model = new SaleReportsViewModel();
            model.filter = filter;
            model.Reports = await _dataService.GetSalesErrorsAsync(filter);
            if (model.Reports.Count == 0) return NoContent();

            string mindate = model.Reports.Min(n => n.MiladiDate).Value.LatinToPersian();
            string maxDate = model.Reports.Max(n => n.MiladiDate).Value.LatinToPersian();

            ViewBag.title = "گزارش خطای بارنامه ها";
            ViewBag.subtitle = $"از تاریخ {mindate} لغایت {maxDate}";

            return View(model);
        }

        public async Task<IActionResult> ExportTax(long[] items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (items.Length == 0)
            {
                result.Message = "ردیفی نشده است";
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            var dataForExport = await _dataService.GetBillofladingByListIdAsync(items.ToList());
            if (dataForExport.Count == 0)
            {
                result.Message = "در ردیف های انتخاب شده بارناه اعتباری وجود ندارد.";
                return Json(result.ToJsonResult());
            }
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Export Data");

                // افزودن هدرها
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

                // افزودن داده‌ها
                for (int i = 0; i < dataForExport.Count; i++)
                {
                    var item = dataForExport[i];
                    worksheet.Cell(i + 2, 1).Value = item.AccountingSystemInvoiceCode;
                    worksheet.Cell(i + 2, 2).Value = item.InvoiceNumber;
                    worksheet.Cell(i + 2, 3).Value = item.InvoiceDate;
                    worksheet.Cell(i + 2, 4).Value = item.InvoiceType;
                    worksheet.Cell(i + 2, 5).Value = item.BuyerFullName;
                    worksheet.Cell(i + 2, 6).Value = item.PersonType;
                    worksheet.Cell(i + 2, 7).Value = item.NationalID;
                    worksheet.Cell(i + 2, 8).Value = item.NewEconomicCode;
                    worksheet.Cell(i + 2, 9).Value = item.PostalCode;
                    worksheet.Cell(i + 2, 10).Value = item.Address;
                    worksheet.Cell(i + 2, 11).Value = item.GoodsOrServices;
                    worksheet.Cell(i + 2, 12).Value = item.GoodsOrServices13DigitID;
                    worksheet.Cell(i + 2, 13).Value = item.GoodsOrServicesDescription;
                    worksheet.Cell(i + 2, 14).Value = item.GoodsOrServicesUnitCode;
                    worksheet.Cell(i + 2, 15).Value = item.UnitPrice;
                    worksheet.Cell(i + 2, 16).Value = item.Quantity;
                    worksheet.Cell(i + 2, 17).Value = item.Discount;
                    worksheet.Cell(i + 2, 18).Value = item.VATRate;
                    worksheet.Cell(i + 2, 19).Value = item.VATAmount;
                    worksheet.Cell(i + 2, 20).Value = item.SettlementType;
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "exports", "ExportData.xlsx");
                workbook.SaveAs(filePath);

                result.Success = true;
                result.Message = "فایل خروجی با موفقیت ایجاد شد";
                result.updateType = 1;
                result.returnUrl = Url.Content("~/exports/ExportData.xlsx");
            }

            return Json(result.ToJsonResult());
        }

    }
}

