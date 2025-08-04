using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos;
using GarnetAccounting.Areas.DataTransfer.Models;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.Controllers
{
    [Area("Courier")]
    [Authorize]
    public class RepresentativeController : Controller
    {
        private readonly ICuRepresentativeService _rep;
        private readonly IGeneralService _gs;
        private readonly IPersonService _persen;
        private readonly ICuGaeOldSys_SaleDataService _saleData;
        private readonly UserContextService _userContext;

        public RepresentativeController(ICuRepresentativeService RepresentativeService
            , IGeneralService generalService
            , IPersonService persenService
            , UserContextService userContextService
            , ICuGaeOldSys_SaleDataService cuGaeOldSys_SaleDataService)
        {
            _rep = RepresentativeService;
            _gs = generalService;
            _persen = persenService;
            _userContext = userContextService;
            _saleData = cuGaeOldSys_SaleDataService;
        }
        public async Task<IActionResult> RepresentativesManager(SaleFilterDto filter)
        {
            if (string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.strStartDate = DateTime.Now.AddDays(-30).LatinToPersianForDatepicker();
            }

            var model = new VmRepresentativeManager();
            model.filter = filter;
            model.filter.SellerId = _userContext.SellerId.Value;

            model.Representatives = await _rep.GetRepresentativesAsync(_userContext.SellerId.Value);
            model.SaleDailyReportByRepresentative = await _saleData.RepresentativeReportAsync(filter).ToListAsync();
            ViewBag.reps = await _saleData.SelectList_DestinationRepresentativesAsync(model.filter.SellerId);
            ViewBag.agency = await _saleData.SelectList_AgencyAsync(model.filter.SellerId);
            return View(model);
        }
        public async Task<IActionResult> RepresentativesReportDetail(SaleFilterDto filter)
        {
            if (string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.strStartDate = DateTime.Now.AddDays(-30).LatinToPersianForDatepicker();
            }
            if (string.IsNullOrEmpty(filter.DestinationRepresentative))
                return NoContent();

            var model = new VmRepresentativeManager();
            model.filter = filter;
            model.filter.SellerId = _userContext.SellerId.Value;

            var report = _saleData.GetSalesAsQuery(model.filter);
            model.RepresentativeReportDetail = Pagination<KPOldSystemSaleReport>.Create(report, model.filter.CurrentPage, model.filter.PageSize);
            ViewBag.agency = await _saleData.SelectList_AgencyAsync(model.filter.SellerId);
            ViewBag.group = await _saleData.SelectList_BillOfLadingGroupAsync(model.filter.SellerId);

            return View(model);
        }
        public async Task<IActionResult> AgentPerformanceOverview(RepFilterDto filter)
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            AgentPerformanceOverviewDto model = new AgentPerformanceOverviewDto();
            model.filter = filter;
            model.filter.sellerId = _userContext.SellerId.Value;
            if (string.IsNullOrEmpty(model.filter.strStartDate))
                model.filter.strStartDate = DateTime.Now.AddDays(-30).LatinToPersian();

            var dataQuery = _rep.OldSys_RepresentativeRates(model.filter);
            model.Report = Pagination<RepresentativeRate>.Create(dataQuery, model.filter.CurrentPage, model.filter.PageSize);
            ViewBag.Agents = await _rep.SelectList_OldSys_RepresentativeAsync(model.filter.sellerId);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> AddRepresentative()
        {
            string userName = User.Identity.Name;
            var userInfo = await _gs.UserSettingAsync(userName);
            long? sellerId = userInfo.ActiveSellerId;
            ViewBag.Persen = await _persen.SelectList_PersenAsync(sellerId.Value);
            return PartialView("_AddRepresentative");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRepresentative(RepresentativeDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            string userName = User.Identity.Name;
            var userInfo = await _gs.UserSettingAsync(userName);
            long? sellerId = userInfo.ActiveSellerId;
            dto.SellerId = sellerId.Value;
            if (ModelState.IsValid)
            {
                result = await _rep.AddRepresentativeAsync(dto);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var er in errors)
            {
                result.Message += "\n" + er.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<ActionResult> EditRepresentativeInfo(Guid id)
        {
            string userName = User.Identity.Name;
            var userInfo = await _gs.UserSettingAsync(userName);
            long? sellerId = userInfo.ActiveSellerId;
            ViewBag.Persen = await _persen.SelectList_PersenAsync(sellerId.Value);
            return PartialView("_EditRepresentativeInfo");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRepresentativeInfo(RepresentativeDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            string userName = User.Identity.Name;
            var userInfo = await _gs.UserSettingAsync(userName);
            long? sellerId = userInfo.ActiveSellerId;
            dto.SellerId = sellerId.Value;
            if (ModelState.IsValid)
            {
                result = await _rep.AddRepresentativeAsync(dto);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var er in errors)
            {
                result.Message += "\n" + er.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }
    }
}
