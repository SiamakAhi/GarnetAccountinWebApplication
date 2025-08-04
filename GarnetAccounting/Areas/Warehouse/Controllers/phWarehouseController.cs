using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Warehouse.Models.Dtos;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Warehouse.Controllers
{
    [Authorize]
    [Area("Warehouse")]
    public class phWarehouseController : Controller
    {
        private readonly UserContextService _userContext;
        private readonly IWarehouseService _warehouseService;
        private readonly IAccCodingService _accCoding;
        long? _sellerId = null;

        public phWarehouseController(UserContextService userContext, IWarehouseService warehouseService, IAccCodingService CodingService)
        {
            _userContext = userContext;
            _warehouseService = warehouseService;
            _accCoding = CodingService;
            _sellerId = _userContext.SellerId;
        }

        // نمایش لیست انبارها
        public async Task<ActionResult> Warehouses()
        {
            if (!_sellerId.HasValue)
                return RedirectToAction("Index", "Home");

            var warehouses = await _warehouseService.GetWarehousesAsync(_sellerId.Value);
            return View(warehouses);
        }

        [HttpGet]
        public async Task<IActionResult> AddWarehouse()
        {
            ViewBag.Moeins = await _accCoding.SelectList_MoeinsAsync(_sellerId.Value);
            ViewBag.Tafsils = await _accCoding.SelectList_TafsilsAsync(_sellerId.Value);

            return PartialView("_AddWarehouse");
        }

        // افزودن انبار
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWarehouse(WarehouseDto dto)
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
                result = await _warehouseService.CreateWarehouseAsync(dto);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.Success = true;
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        // ویرایش انبار
        [HttpGet]
        public async Task<IActionResult> EditWarehouse(long id)
        {
            var warehouse = await _warehouseService.GetWarehousesAsync(_sellerId.Value);
            var warehouseToEdit = warehouse.FirstOrDefault(w => w.WarehouseId == id);

            ViewBag.Moeins = await _accCoding.SelectList_MoeinsAsync(_sellerId.Value);
            ViewBag.Tafsils = await _accCoding.SelectList_TafsilsAsync(_sellerId.Value);
            return PartialView("_EditWarehouse", warehouseToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWarehouse(WarehouseDto dto)
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
                result = await _warehouseService.UpdateWarehouseAsync(dto);
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

        // حذف انبار
        [HttpPost]
        public async Task<IActionResult> DeleteWarehouse(long id)
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

            result = await _warehouseService.DeleteWarehouseAsync(id);
            if (result.Success)
            {
                result.updateType = 1;
                result.Success = true;
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }

            return Json(result.ToJsonResult());
        }

        // تغییر وضعیت فعال یا غیرفعال کردن انبار
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleWarehouseStatus(long id)
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

            result = await _warehouseService.SetWarehouseActiveStatusAsync(id, true);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }

            return Json(result.ToJsonResult());
        }
    }
}
