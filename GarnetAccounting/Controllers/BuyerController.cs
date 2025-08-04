using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.CommercialInterfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.ViewModels.CommercialViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GarnetAccounting.Controllers
{
    [Authorize]

    public class BuyerController : Controller
    {
        private readonly IBuyerService _service;
        private readonly IAppIdentityUserManager _usermanager;
        private readonly IGeneralService _gs;
        public int? _customerId { get; set; }

        public BuyerController(IBuyerService service, IAppIdentityUserManager usermanager, IGeneralService generalService)
        {
            _service = service;
            _usermanager = usermanager;
            _gs = generalService;
            //_customerId = _usermanager.GetCustomerIdByUsername(User.Identity.Name).Result;
        }

        public async Task<IActionResult> Buyers(int currentPage = 1, int pageSize = 15, string message = "", string name = "", string NationalCode = "")
        {
            int? cusomerId = _usermanager.GetCustomerIdByUsername(User.Identity.Name).Result;
            Int64? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            var data = _service.GetBuyers(sellerId.Value, cusomerId.Value, name, NationalCode);
            var model = Pagination<VmBuyer>.Create(data, currentPage, pageSize);
            ViewBag.Message = message;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddBuyer()
        {
            string userName = User.Identity.Name;
            ViewBag.customerId = await _usermanager.GetCustomerIdByUsername(userName);
            ViewBag.SellerId = await _gs.GetActiveSellerIdAsync(userName);
            ViewBag.PartyTypes = await _service.SelectList_PartyType();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBuyer(BuyerAddDto dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _service.AddBuyer(dto);
                if (res.Success)
                {
                    return RedirectToAction("Buyers", new { message = res.Message, name = dto.Name });
                }
            }
            string userName = User.Identity.Name;
            ViewBag.customerId = await _usermanager.GetCustomerIdByUsername(userName);
            ViewBag.SellerId = await _gs.GetActiveSellerIdAsync(userName);
            ViewBag.PartyTypes = await _service.SelectList_PartyType();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBuyer(Int64 id)
        {
            var model = await _service.GetBuyerByIdAsync(id);
            ViewBag.PartyTypes = await _service.SelectList_PartyType();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBuyer(BuyerUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _service.UpdateBuyerAsync(dto);
                if (res.Success)
                {
                    return RedirectToAction("Buyers", new { message = res.Message, name = dto.Name });
                }
            }
            ViewBag.PartyTypes = await _service.SelectList_PartyType();
            ViewBag.customerId = dto.CustomerId;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> DelBuyer(Int64 id)
        {
            var result = await _service.DeleteBuyerAsync(id);
            if (result.Success)
                result.ReturnUrl = Request.Headers["Referer"].ToString();

            string jsonResult = JsonConvert.SerializeObject(result);
            return Json(jsonResult);
        }


    }
}
