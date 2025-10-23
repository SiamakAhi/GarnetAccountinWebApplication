using ClosedXML.Excel;
using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    [Authorize(Roles = "AccountingManager,AccountingBoss")]
    public class ElectronicBookManagerController : Controller
    {
        private readonly IAccEbookService _ebook;
        private readonly IAccExportService _export;
        private readonly IGeneralService _gs;

        public ElectronicBookManagerController(
            IAccExportService export
            , IGeneralService gs
            , IAccEbookService ebook)
        {
            _export = export;
            _gs = gs;
            _ebook = ebook;
        }

        [HttpGet]
        public async Task<IActionResult> ElectronicBookFiles()
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null) { return BadRequest("License not fine."); }
            long SellerId = userSett.ActiveSellerId.Value;
            int PeriodId = userSett.ActiveSellerPeriod.Value;
            EBookManagerDto model = new EBookManagerDto();
            model.eBooksMetaData = await _ebook.GetEbookMetadata(SellerId, PeriodId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ElectronicBookFiles(EBookManagerDto model)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null) { return BadRequest("License not fine."); }
            model.SellerId = userSett.ActiveSellerId.Value;
            model.PeriodId = userSett.ActiveSellerPeriod.Value;
            model.IsPosted = true;
            model.eBooksMetaData = await _ebook.GetEbookMetadata(model.SellerId, model.PeriodId);
            if (string.IsNullOrEmpty(model.strFromDate))
            {
                model.Successed = false;
                model.Message = "تاریخ شروع بازه گزارش را مشحص کنید.";
                return View(model);
            }
            if (string.IsNullOrEmpty(model.strToDate))
            {
                model.Successed = false;
                model.Message = "تاریخ پایان بازه گزارش را مشحص کنید.";
                return View(model);
            }
            DateTime minDate = model.strFromDate.PersianToLatin();
            DateTime maxDate = model.strToDate.PersianToLatin();
            var fp = await _gs.GetFinancialPeriodAsync(model.PeriodId);

            if (model.eBooksMetaData != null && model.eBooksMetaData.Any())
            {
                DateTime ebminDate = model.eBooksMetaData.Min(n => n.FromDate);
                DateTime ebmaxDate = model.eBooksMetaData.Max(n => n.ToDate);


                if (minDate <= ebmaxDate)
                {
                    model.Successed = false;
                    model.Message = "تاریخ شروع بازه گزارش باید بزرگتر از تاریخ آخرین سند ارسال شده باشد.";
                    return View(model);
                }
            }
            else
            {
                if (minDate != fp.StartDate)
                {
                    model.Successed = false;
                    model.Message = $"تاریخ شروع گزارش باید برابر با {fp.StartDate.LatinToPersian()} باشد";
                    return View(model);
                }
            }
            model = await _ebook.GetEbookAsync(model);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateEBook(EBookManagerDto model)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null)
                return BadRequest("License not fine.");

            model = await _ebook.GetEbookAsync(model);

            // فایل Excel برای گزارش‌های زیر 1000 رکورد
            if (model.eBooks.Count < 1000)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add($"{model.strFromDate} To {model.strToDate}");
                    worksheet.RightToLeft = true;

                    worksheet.Cell(1, 1).Value = "ردیف";
                    worksheet.Cell(1, 2).Value = "تاریخ";
                    worksheet.Cell(1, 3).Value = "کد حساب کل";
                    worksheet.Cell(1, 4).Value = "عنوان حساب کل";
                    worksheet.Cell(1, 5).Value = "کد حساب معین";
                    worksheet.Cell(1, 6).Value = "عنوان حساب معین";
                    worksheet.Cell(1, 7).Value = "شرح";
                    worksheet.Cell(1, 8).Value = "مبلغ بدهکار (ريال)";
                    worksheet.Cell(1, 9).Value = "مبلغ بستانکار (ريال)";

                    var headerRange = worksheet.Range(1, 1, 1, 9);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    int row = 2;
                    foreach (var x in model.eBooks)
                    {
                        worksheet.Cell(row, 1).Value = x.Row;
                        worksheet.Cell(row, 2).Value = x.docDate;
                        worksheet.Cell(row, 3).Value = x.KolCode;
                        worksheet.Cell(row, 4).Value = x.KolName;
                        worksheet.Cell(row, 5).Value = x.MoeinCode;
                        worksheet.Cell(row, 6).Value = x.MoeinName;
                        worksheet.Cell(row, 7).Value = x.Description;
                        worksheet.Cell(row, 8).Value = x.Bed;
                        worksheet.Cell(row, 9).Value = x.Bes;
                        row++;
                    }

                    worksheet.Columns().AdjustToContents();
                    var AddMetaData = await _ebook.AddMetaDataAsync(model);

                    if (AddMetaData.Successed)
                    {
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var excelData = stream.ToArray();

                            return new FileContentResult(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                            {
                                FileDownloadName = $"دفتر تجاری از تاریخ {model.strFromDate} تا {model.strToDate}.xlsx"
                            };
                        }
                    }
                }
            }
            else // ساخت فایل CSV برای رکوردهای بیش از 1000 عدد
            {
                var AddMetaData = await _ebook.AddMetaDataAsync(model);

                if (AddMetaData.Successed)
                {
                    var sb = new StringBuilder();

                    // هدر CSV
                    sb.AppendLine("ردیف,تاریخ,کد حساب کل,عنوان حساب کل,کد حساب معین,عنوان حساب معین,شرح,مبلغ بدهکار (ريال),مبلغ بستانکار (ريال)");

                    // داده‌ها
                    foreach (var x in model.eBooks)
                    {
                        // برای اطمینان از درست بودن فرمت CSV، متن‌ها را داخل "قرار می‌دهیم
                        sb.AppendLine($"\"{x.Row}\",\"{x.docDate}\",\"{x.KolCode}\",\"{x.KolName}\",\"{x.MoeinCode}\",\"{x.MoeinName}\",\"{x.Description}\",\"{x.Bed}\",\"{x.Bes}\"");
                    }

                    // تبدیل به بایت با UTF-8 (با BOM تا در Excel فارسی درست نمایش داده شود)
                    var utf8WithBom = new System.Text.UTF8Encoding(true);
                    var csvBytes = utf8WithBom.GetBytes(sb.ToString());

                    return new FileContentResult(csvBytes, "text/csv")
                    {
                        FileDownloadName = $"دفتر تجاری از تاریخ {model.strFromDate} تا {model.strToDate}.csv"
                    };
                }
            }

            return View(model);
        }


    }
}
