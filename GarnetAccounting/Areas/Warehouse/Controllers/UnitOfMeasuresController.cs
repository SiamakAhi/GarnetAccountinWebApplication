using GarnetAccounting.Areas.Warehouse.Dto;
using GarnetAccounting.Areas.Warehouse.Models.Dtos;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Warehouse.Controllers
{
    [Authorize]
    [Area("Warehouse")]
    public class UnitOfMeasuresController : Controller
    {
        private readonly UserContextService _userContext;
        private readonly IWhProductService _productService;
        long? _sellerId = null;

        public UnitOfMeasuresController(UserContextService userContext, IWhProductService productService)
        {
            _userContext = userContext;
            _productService = productService;

            _sellerId = _userContext.SellerId;
        }

        public async Task<IActionResult> unitofmeasures()
        {
            var model = new VmUnitOfMeasures();
            model.Measures = new List<UnitOfMeasureDto>();
            if (_sellerId.HasValue)
                model.Measures = await _productService.GetUnitCounts(_sellerId.Value).ToListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUnitCount(UnitOfMeasureDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;
            if (!_sellerId.HasValue)
            {
                result.Message = "شرکت فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = _sellerId.Value;
                result = await _productService.AddUnitCountAsync(dto);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                }

            }
            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> EditTheMeasures(int id)
        {
            var unit = await _productService.GetUnitCountByIdAsync(id);
            return PartialView("_EditTheMeasures", unit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTheMeasures(UnitOfMeasureDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;
            if (!_sellerId.HasValue)
            {
                result.Message = "شرکت فعال شناسایی نشد";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                dto.SellerId = _sellerId.Value;
                result = await _productService.UpdateUnitCountAsync(dto);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                }

            }
            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }
    }
}
