using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.DataTransfer.DataTransferInterfaces;
using GarnetAccounting.Areas.DataTransfer.Models;
using GarnetAccounting.Areas.Representatives.Dtos;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Representatives.Controllers
{
    [Area("Representatives")]
    [Authorize]
    public class kpController : Controller
    {
        private readonly UserContextService _userContext;
        private readonly ICuBranchService _branchServic;
        private readonly IBranchUserService _branchUserService;
        private readonly IKPDataTransferService _sale;
        private readonly IBillofladingService _bill;
        public kpController(UserContextService userContext
            , ICuBranchService branchServic
            , IBranchUserService branchUserService
            , IKPDataTransferService sale
            , IBillofladingService billOfLadingImportService)
        {
            _userContext = userContext;
            _branchServic = branchServic;
            _branchUserService = branchUserService;
            _sale = sale;
            _bill = billOfLadingImportService;
        }

        public async Task<IActionResult> Index(BillOfLadingFilterDto filter)
        {
            if (!_userContext.SellerId.HasValue || _userContext.BranchId == null) return NoContent();
            var branch = await _branchServic.FindBranchByIdAsync(_userContext.BranchId.Value);
            if (branch == null) return NoContent();
            var currentUser = await _branchUserService.GetBUserByUsernameAsync(User.Identity.Name);
            if (currentUser == null) return NoContent();
            var model = new VmBranchUserPanel();
            model.Branch = branch;
            model.CurrentUser = currentUser;


            filter.SellerId = _userContext.SellerId.Value;
            filter.OriginBranchId = _userContext.BranchId.Value;
            model.filter = filter;

            var billsOut = _bill.GetBillsAsQuery(filter);
            model.BillsOut = Pagination<ViewBillOfLadings>.Create(billsOut, filter.CurrentPage, filter.PageSize);

            return View(model);
        }

        public async Task<IActionResult> loadLadding(long id)
        {
            var model = new VmBilloflading();
            model.Billodlading = await _sale.FindBillofladdingByIdAsync(id);
            return PartialView("_loadLadding", model);
        }

        public async Task<IActionResult> IncomingBillsOfLading(DataTransfer.Dto.SaleFilterDto filter)
        {

            if (!_userContext.SellerId.HasValue || _userContext.BranchId == null) return NoContent();
            var branch = await _branchServic.FindBranchByIdAsync(_userContext.BranchId.Value);
            if (branch == null) return NoContent();
            var currentUser = await _branchUserService.GetBUserByUsernameAsync(User.Identity.Name);
            if (currentUser == null) return NoContent();

            var model = new VmBranchUserPanel();
            model.Branch = branch;
            model.CurrentUser = currentUser;

            var data = _sale.GetIncomingBillsOfLadings(filter);
            //model.Pagin_IncomingBillsOfLading = Pagination<KPOldSystemSaleReport>.Create(data, filter.currentPage, filter.pageSize);
            return View();
        }

        public async Task<IActionResult> GetIncomingBillsOfLading(DataTransfer.Dto.SaleFilterDto filter)
        {

            var data = _sale.GetIncomingBillsOfLadings(filter);
            var model = Pagination<KPOldSystemSaleReport>.Create(data, filter.currentPage, filter.pageSize);
            return PartialView("GetIncomingBillsOfLading", model);
        }

    }

}
