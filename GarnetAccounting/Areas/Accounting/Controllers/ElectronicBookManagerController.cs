using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> ExportEBook(EBookManagerDto model)
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null) { return BadRequest("License not fine."); }
            long SellerId = userSett.ActiveSellerId.Value;
            int PeriodId = userSett.ActiveSellerPeriod.Value;
            model.eBooksMetaData = await _ebook.GetEbookMetadata(SellerId, PeriodId);
            return View(model);
        }

    }
}
