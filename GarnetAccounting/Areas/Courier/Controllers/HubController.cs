using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Geolocation.GeolocationInterfaces;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GarnetAccounting.Areas.Courier.Controllers
{
    [Area("Courier")]
    [Authorize]
    public class HubController : Controller
    {
        private readonly ICuHubService _hubService;
        private readonly IGeneralService _gs;
        private readonly UserContextService _userContext;
        private readonly IGeoGeneralService _locationService;
        private long? _sellerId;

        public HubController(ICuHubService hubService
            , IGeneralService generalService
            , UserContextService userContextService
            , IGeoGeneralService geoLocationService)
        {
            _hubService = hubService;
            _gs = generalService;
            _userContext = userContextService;
            _locationService = geoLocationService;
            _sellerId = _userContext.SellerId;
        }

        // GET: Courier/Hub/Hubs
        public async Task<IActionResult> Hubs()
        {
            if (_sellerId == null) return NoContent();
            var hubs = await _hubService.GetHubsAsync(_sellerId.Value);
            return View(hubs);
        }

        // GET: Courier/Hub/AddHub
        [HttpGet]
        public async Task<IActionResult> AddHub()
        {
            string userName = User.Identity.Name;
            var userInfo = await _gs.UserSettingAsync(userName);
            long? sellerId = userInfo.ActiveSellerId;

            if (!sellerId.HasValue)
                return NoContent();

            ViewBag.Cities = await _locationService.SelectItems_CitiesAsync();
            return PartialView("_AddHub");
        }

        // POST: Courier/Hub/AddHub
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHub(HubDto dto)
        {
            clsResult result = new clsResult();

            if (!_sellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد.";
                return Json(result.ToJsonResult());
            }

            dto.SellerId = _sellerId.Value;

            if (ModelState.IsValid)
            {
                result = await _hubService.CreateHubAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            // Collect model validation errors
            var modelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += "\n" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        // GET: Courier/Hub/UpdateHubInfo/{id}
        [HttpGet]
        public async Task<IActionResult> UpdateHubInfo(Guid id)
        {
            string userName = User.Identity.Name;
            var userInfo = await _gs.UserSettingAsync(userName);
            long? sellerId = userInfo.ActiveSellerId;

            if (!sellerId.HasValue)
            {
                ViewBag.ErrorMessage = "دسترسی به شرکت فعال یافت نشد.";
                return PartialView("_UpdateHubInfo");
            }

            ViewBag.Cities = await _locationService.SelectItems_CitiesAsync();

            var hub = await _hubService.GetHubByIdAsync(id);

            return PartialView("_UpdateHubInfo", hub);
        }

        // POST: Courier/Hub/UpdateHubInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateHubInfo(HubDto dto)
        {
            clsResult result = new clsResult();

            if (!_sellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد.";
                return Json(result.ToJsonResult());
            }

            dto.SellerId = _sellerId.Value;

            if (ModelState.IsValid)
            {
                result = await _hubService.UpdateHubAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result.ToJsonResult());
                }
            }

            var modelErrors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += "\n" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        // POST: Courier/Hub/DeleteHub/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteHub(Guid itemId)
        {
            clsResult result = new clsResult();

            if (!_sellerId.HasValue)
            {
                result.Message = "دسترسی به شرکت فعال یافت نشد.";
                return Json(result.ToJsonResult());
            }

            result = await _hubService.DeleteHubAsync(itemId);
            if (result.Success)
            {
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }
    }
}

