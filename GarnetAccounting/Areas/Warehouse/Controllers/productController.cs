using GarnetAccounting.Areas.Warehouse.Dto;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Warehouse.Controllers
{
    [Authorize]
    [Area("Warehouse")]
    public class productController : Controller
    {
        private readonly UserContextService _userContext;
        private readonly IWhProductService _productService;
        long? _sellerId = null;

        public productController(UserContextService userContext, IWhProductService productService)
        {
            _userContext = userContext;
            _productService = productService;

            _sellerId = _userContext.SellerId;
        }

        // نمایش لیست کالاها
        public async Task<ActionResult> products(ProductFilter filter)
        {
            VmProducts model = new VmProducts();

            if (filter.CurrentPage == 0)
            {
                filter = new ProductFilter();
                filter.PageSize = 20;
                filter.CurrentPage = 1;

            }
            if (_sellerId.HasValue)
                filter.SellerId = _sellerId.Value;
            var data = _productService.GetProducts(filter);
            model.Products = Pagination<ProductBaseDto>.Create(data, filter.CurrentPage, filter.PageSize);
            model.filter = filter;
            ViewBag.Categories = await _productService.SelectList_CategoriesFullnameAsync(_sellerId.Value);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            if (!_sellerId.HasValue) return Ok();
            ViewBag.Categories = await _productService.SelectList_CategoriesFullnameAsync(_sellerId.Value);
            ViewBag.UnitOfMeasures = await _productService.SelectList_UnitCountAsync(_sellerId.Value);
            ViewBag.code = await _productService.StuffCodeGenerator(_sellerId.Value);
            return View();
        }
        // افزودن کالا
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductBaseDto dto)
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
                result = await _productService.CreateProductAsync(dto);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.ShowMessage = true;
                    result.returnUrl = Url.Action("products", "product", new { Area = "Warehouse" });
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }
            ViewBag.Categories = await _productService.SelectList_CategoriesFullnameAsync(_sellerId.Value);
            ViewBag.UnitOfMeasures = await _productService.SelectList_UnitCountAsync(_sellerId.Value);

            return Json(result.ToJsonResult());
        }

        // ویرایش کالا
        [HttpGet]
        public async Task<IActionResult> EditProduct(long id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            ViewBag.Categories = await _productService.SelectList_CategoriesFullnameAsync(_sellerId.Value);
            ViewBag.UnitOfMeasures = await _productService.SelectList_UnitCountAsync(_sellerId.Value);
            return PartialView("_EditProduct", product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductBaseDto dto)
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
                result = await _productService.UpdateProductAsync(dto);
                if (result.Success)
                {
                    result.updateType = 1;
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }
            ViewBag.Categories = await _productService.SelectList_CategoriesFullnameAsync(_sellerId.Value);
            ViewBag.UnitOfMeasures = await _productService.SelectList_UnitCountAsync(_sellerId.Value);
            return Json(result.ToJsonResult());
        }

        // حذف کالا
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(long id)
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

            result = await _productService.DeleteProductAsync(id);
            if (result.Success)
            {
                result.updateType = 1;
                result.ShowMessage = true;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }

            return Json(result.ToJsonResult());
        }

        // تغییر وضعیت فعال یا غیرفعال کردن کالا
        [HttpPost]
        public async Task<IActionResult> ToggleProductStatus(long id)
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

            result = await _productService.ToggleProductStatusAsync(id);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }

            return Json(result.ToJsonResult());
        }
    }
}

