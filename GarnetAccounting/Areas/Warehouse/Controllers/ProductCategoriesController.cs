using GarnetAccounting.Areas.Warehouse.Models.Dtos;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ProductCategoriesController : Controller
    {
        private readonly IWhProductService _productService;
        private readonly UserContextService _userContext;
        public ProductCategoriesController(IWhProductService productService, UserContextService userContext)
        {
            _productService = productService;
            _userContext = userContext;
        }

        public async Task<ActionResult> ProductCategories()
        {
            if (_userContext.SellerId == null)
                return Ok();
            var categories = await _productService.GetCategoryTreeAsync(_userContext.SellerId.Value);
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            if (_userContext.SellerId == null)
                return Json(new { success = false, message = "شرکت فعال نمی‌باشد." });

            var categories = await _productService.GetAllCategoriesAsync(_userContext.SellerId.Value);
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryTree()
        {
            if (_userContext.SellerId == null)
                return Json(new { success = false, message = "شرکت فعال نمی‌باشد." });

            var categoryTree = await _productService.GetCategoryTreeAsync(_userContext.SellerId.Value);
            return Json(categoryTree);
        }

        [HttpGet]
        public IActionResult AddCategory(long? Id)
        {
            ViewBag.ParentCategoryId = Id;
            return PartialView("_AddCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Wh_ProductCategoryDto categoryDto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (_userContext.SellerId == null)
            {
                result.Message = "شرکت فعال نمی‌باشد.";
                return Json(result);
            }

            if (ModelState.IsValid)
            {
                categoryDto.SellerId = _userContext.SellerId.Value;
                result = await _productService.CreateCategoryAsync(categoryDto);
                if (result.Success)
                {
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    result.ShowMessage = true;
                    return Json(result.ToJsonResult());
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(long categoryId)
        {
            if (_userContext.SellerId == null)
                return Json(new { success = false, message = "شرکت فعال نمی‌باشد." });

            var category = await _productService.GetCategoryByIdAsync(_userContext.SellerId.Value, categoryId);
            return PartialView("_EditCategory", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Wh_ProductCategoryDto categoryDto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (_userContext.SellerId == null)
            {
                result.Message = "شرکت فعال نمی‌باشد.";
                return Json(result);
            }

            if (ModelState.IsValid)
            {
                categoryDto.SellerId = _userContext.SellerId.Value;
                result = await _productService.UpdateCategoryAsync(_userContext.SellerId.Value, categoryDto);
                if (result.Success)
                {
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                    return Json(result);
                }
            }

            var modelError = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelError)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            clsResult result = new clsResult();

            if (_userContext.SellerId == null)
            {
                result.Success = false;
                result.Message = "شرکت فعال نمی‌باشد.";
                return Json(result);
            }

            result = await _productService.DeleteCategoryAsync(_userContext.SellerId.Value, categoryId);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateCategory(long categoryId)
        {
            clsResult result = new clsResult();

            if (_userContext.SellerId == null)
            {
                result.Success = false;
                result.Message = "شرکت فعال نمی‌باشد.";
                return Json(result);
            }

            result = await _productService.ActivateCategoryAsync(_userContext.SellerId.Value, categoryId);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateCategory(long categoryId)
        {
            clsResult result = new clsResult();

            if (_userContext.SellerId == null)
            {
                result.Success = false;
                result.Message = "شرکت فعال نمی‌باشد.";
                return Json(result);
            }

            result = await _productService.DeactivateCategoryAsync(_userContext.SellerId.Value, categoryId);
            return Json(result);
        }
    }
}