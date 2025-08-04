using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Courier.Controllers
{
    [Area("Courier")]
    [Authorize]
    public class BillofladingController : Controller
    {

        private readonly UserContextService _userContext;
        private readonly IBillofladingService _bill;
        private readonly ICuBranchService _branchServic;
        private readonly IBranchUserService _branchUser;
        private readonly IPersonService _persen;
        private readonly ICourierServiceService _courier;
        private readonly ICuPricingService _pricing;
        long? _sellerId;

        public BillofladingController(UserContextService userContextService
            , IBillofladingService billofladingService
            , ICuBranchService branchServic
            , IBranchUserService branchUser
            , IPersonService personService
            , ICourierServiceService courier
            , ICuPricingService pricingService)
        {
            _userContext = userContextService;
            _bill = billofladingService;
            _branchServic = branchServic;
            _branchUser = branchUser;
            _persen = personService;
            _courier = courier;
            _pricing = pricingService;

            _sellerId = _userContext.SellerId;

        }

        [HttpGet]
        public async Task<IActionResult> CreateBillofladingHeader()
        {
            var user = await _branchUser.GetBUserByUsernameAsync(User.Identity.Name);
            var branch = await _branchServic.FindBranchByIdAsync(_userContext.BranchId.Value);
            if (!_sellerId.HasValue || user == null || branch == null) return NoContent();

            var model = new VmBillofladingPanel();
            model.Branch = branch;
            model.CurrentUser = user;

            ViewBag.persen = await _persen.SelectList_PersenListAsync(_sellerId.Value);
            ViewBag.routes = await _courier.SelectList_RoutesByOriginCityAsync(_sellerId.Value, user.BranchCityId);
            ViewBag.services = await _courier.SelectList_ServicesAsync(_sellerId.Value);
            ViewBag.BillNumber = await _bill.GenerateBillNumberAsync(_sellerId.Value, user.BranchCode);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBillofladingHeader(VmBillofladingPanel model)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            var user = await _branchUser.GetBUserByUsernameAsync(User.Identity.Name);
            var branch = await _branchServic.FindBranchByIdAsync(_userContext.BranchId.Value);
            if (!_sellerId.HasValue || user == null || branch == null) return NoContent();

            if (!branch.HubId.HasValue)
            {
                result.Message = "هاب متصل به شعبه شناسایی نشد";
                return Json(result.ToJsonResult());
            }

            model.BillOfLading.bill.SellerId = _sellerId.Value;
            model.BillOfLading.bill.BillOfLadingStatusId = 1;
            model.BillOfLading.bill.LastStatusDescription = "درحال صدور";

            if (ModelState.IsValid)
            {
                result = await _bill.CreateNewBillOfLadingAsync(model.BillOfLading.bill);
                if (result.Success)
                {
                    result.ShowMessage = false;
                    return RedirectToAction("Billoflading", new { id = model.BillOfLading.bill.Id });
                }
            }

            model.Branch = branch;
            model.CurrentUser = user;

            ViewBag.persen = await _persen.SelectList_PersenListAsync(_sellerId.Value);
            ViewBag.routes = await _courier.SelectList_RoutesByOriginCityAsync(_sellerId.Value, user.BranchCityId);
            ViewBag.services = await _courier.SelectList_ServicesAsync(_sellerId.Value);
            ViewBag.BillNumber = await _bill.GenerateBillNumberAsync(_sellerId.Value, user.BranchCode);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Billoflading(Guid id)
        {
            var user = await _branchUser.GetBUserByUsernameAsync(User.Identity.Name);
            var branch = await _branchServic.FindBranchByIdAsync(_userContext.BranchId.Value);
            if (!_sellerId.HasValue || user == null || branch == null) return NoContent();


            var model = new VmBillofladingPanel();
            model.Branch = branch;
            model.CurrentUser = user;
            model.BillOfLading = await _bill.GetBillOfLadingDtoAsync(id);

            ViewBag.persen = await _persen.SelectList_PersenListAsync(_sellerId.Value);
            ViewBag.routes = await _courier.SelectList_RoutesByOriginCityAsync(_sellerId.Value, user.BranchCityId);
            ViewBag.services = await _courier.SelectList_ServicesAsync(_sellerId.Value);
            ViewBag.BillNumber = await _bill.GenerateBillNumberAsync(_sellerId.Value, user.BranchCode);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddConsigment(Guid id)
        {
            var user = await _branchUser.GetBUserByUsernameAsync(User.Identity.Name);
            var branch = await _branchServic.FindBranchByIdAsync(_userContext.BranchId.Value);

            if (!_sellerId.HasValue || user == null || branch == null) return NoContent();

            var model = new VmBillofladingPanel();
            model.Branch = branch;
            model.CurrentUser = user;
            model.BillOfLading = await _bill.GetBillOfLadingDtoAsync(id);

            ViewBag.Natures = await _pricing.SelectList_ConsigmentNatureAsync();
            ViewBag.Packages = await _courier.SelectList_PackagesAsync(_sellerId.Value, false);

            return PartialView("_AddConsigment", model);
        }
    }
}
