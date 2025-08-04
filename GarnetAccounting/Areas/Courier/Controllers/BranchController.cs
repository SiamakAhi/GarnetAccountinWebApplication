using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos;
using GarnetAccounting.Areas.Geolocation.GeolocationInterfaces;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Courier.Controllers
{
    [Area("Courier")]
    public class BranchController : Controller
    {
        private readonly ICuBranchService _branchService;
        private readonly UserContextService _userContext;
        private readonly ICuRepresentativeService _representative;
        private readonly IPersonService _persen;
        private readonly IGeneralService _gs;
        private readonly IGeoGeneralService _locationService;
        private readonly ICuHubService _hub;

        public BranchController(ICuBranchService branchService
            , UserContextService userContext,
              ICuRepresentativeService representative
            , IPersonService persenService
            , IGeneralService generalService
            , IGeoGeneralService locationService
            , ICuHubService hubService)
        {
            _branchService = branchService;
            _userContext = userContext;
            _representative = representative;
            _persen = persenService;
            _gs = generalService;
            _locationService = locationService;
            _hub = hubService;
        }

        public async Task<ActionResult> Branches()
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            var model = new VmBranch();
            model.Branches = await _branchService.GetBranchesAsync(_userContext.SellerId.Value);
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> AddBranch()
        {
            string userName = User.Identity.Name;
            var userInfo = await _gs.UserSettingAsync(userName);
            long? sellerId = userInfo.ActiveSellerId;
            ViewBag.Persen = await _persen.SelectItems_TafsilsByGroupAsync(new int[] { 1 }, sellerId.Value);
            ViewBag.Cities = await _locationService.SelectItems_CitiesAsync();
            ViewBag.hubs = await _hub.SelectList_HubsAsync(sellerId.Value);

            return PartialView("_AddBranch");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> AddBranch(BranchDto dto)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }
            dto.SellerId = _userContext.SellerId.Value;
            if (ModelState.IsValid)
            {
                result = await _branchService.AddBranchAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;

                    return Json(result.ToJsonResult());
                }
            }

            var ModelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var er in ModelErrors)
            {
                result.Message += "\n" + er.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<ActionResult> UpdateBranchInfo(Guid id)
        {
            string userName = User.Identity.Name;
            var userInfo = await _gs.UserSettingAsync(userName);
            long? sellerId = userInfo.ActiveSellerId;
            ViewBag.Persen = await _persen.SelectItems_TafsilsByGroupAsync(new int[] { 1 }, sellerId.Value);
            ViewBag.Cities = await _locationService.SelectItems_CitiesAsync();
            ViewBag.hubs = await _hub.SelectList_HubsAsync(sellerId.Value);

            var branch = await _branchService.FindBranchByIdAsync(id);
            return PartialView("_UpdateBranchInfo", branch);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> UpdateBranchInfo(BranchDto dto)
        {
            clsResult result = new clsResult();

            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }
            dto.SellerId = _userContext.SellerId.Value;
            if (ModelState.IsValid)
            {
                result = await _branchService.UpdateBranchAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var ModelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var er in ModelErrors)
            {
                result.Message += "\n" + er.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }
    }
}
