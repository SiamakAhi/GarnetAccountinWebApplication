using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Controllers
{
    public class AccReportsController : Controller
    {
        private readonly IAccBaseInfoService _base;
        private readonly IAccountingReportService _ser;
        private readonly IGeneralService _gs;
        public AccReportsController(IAccountingReportService ser, IGeneralService gs, IAccBaseInfoService @base)
        {
            _ser = ser;
            _gs = gs;
            _base = @base;
        }

        public async Task<IActionResult> AccountBrowserKol(DocFilterDto filter)
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
            var model = new AccountsBrowserDto();
            model.filter = filter;
            model.Kols = await _ser.Report_KolAsync(filter);
            ViewBag.docType = _base.SelectList_DocTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AccountBrowserMoein(
             int targetId
            , string? strStartDate = null
            , string? strEndDate = null
            , int? FromDocNumer = null
            , int? ToDocNumer = null
            , short? docType = null
             )
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            //Filter
            DocFilterDto filter = new DocFilterDto();
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;
            filter.targetId = targetId;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.docType = docType;
            filter.FromDocNumer = FromDocNumer;
            filter.ToDocNumer = ToDocNumer;
            filter.docType = docType;

            //Model
            var model = new AccountsBrowserDto();
            model.filter = filter;
            model.Kols = await _ser.Report_MoeinAsync(filter);
            ViewBag.docType = _base.SelectList_DocTypes();

            return PartialView("_AccountBrowserMoein", model);
        }

        [HttpPost]
        public async Task<IActionResult> AccountBrowserTafsil(
             int targetId
            , string? strStartDate = null
            , string? strEndDate = null
            , int? FromDocNumer = null
            , int? ToDocNumer = null
            , short? docType = null
             )
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            //Filter
            DocFilterDto filter = new DocFilterDto();
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;
            filter.targetId = targetId;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.docType = docType;
            filter.FromDocNumer = FromDocNumer;
            filter.ToDocNumer = ToDocNumer;
            filter.docType = docType;

            //Model
            var model = new AccountsBrowserDto();
            model.filter = filter;
            model.Kols = await _ser.Report_Tafsil4Async(filter);
            ViewBag.docType = _base.SelectList_DocTypes();

            return PartialView("_AccountBrowserTafsil", model);
        }

        [HttpPost]
        public async Task<IActionResult> AccountBrowserArticles(
              int targetId
            , long? longId = null
            , string? strStartDate = null
            , string? strEndDate = null
            , int? FromDocNumer = null
            , int? ToDocNumer = null
            , short? docType = null
            )
        {
            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null)
            {
                ViewBag.Allert = "شرکت یا سال مالی فعال شناسایی نشد";
            }
            //Filter
            DocFilterDto filter = new DocFilterDto();
            filter.SellerId = userSett.ActiveSellerId.Value;
            filter.PeriodId = userSett.ActiveSellerPeriod.Value;
            filter.targetId = targetId;
            filter.strStartDate = strStartDate;
            filter.strEndDate = strEndDate;
            filter.docType = docType;
            filter.FromDocNumer = FromDocNumer;
            filter.ToDocNumer = ToDocNumer;
            filter.docType = docType;
            filter.tafsilId = longId;

            //Model
            var model = new AccountsBrowserDto();
            model.filter = filter;
            model.Articles = await _ser.Report_ArticlesAsync(filter);
            ViewBag.docType = _base.SelectList_DocTypes();

            return PartialView("_AccountBrowserArticles", model);
        }

    }
}
