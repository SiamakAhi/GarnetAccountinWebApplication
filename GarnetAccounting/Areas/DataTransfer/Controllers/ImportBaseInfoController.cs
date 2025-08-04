using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Classes;
using GarnetAccounting.Interfaces.CommercialInterfaces;
using GarnetAccounting.Services;
using GarnetAccounting.ViewModels.CommercialViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.DataTransfer.Controllers
{
    [Area("DataTransfer")]
    [Authorize]
    public class ImportBaseInfoController : Controller
    {
        private readonly UserContextService _userContext;
        private readonly IBuyerService _personService;
        private readonly IWhProductService _productService;
        public ImportBaseInfoController(UserContextService userContext, IBuyerService personService, IWhProductService whProductService)
        {
            _userContext = userContext;
            _personService = personService;
            _productService = whProductService;
        }

        [HttpGet]
        public IActionResult ImportPersen()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportPersen(ImportBuyerDto dto)
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            ExcelImporter importer = new ExcelImporter();
            List<string> errorMessages;
            int customerId = 0;
            if (_userContext.CustomerId.HasValue)
                customerId = _userContext.CustomerId.Value;

            var persenData = importer.ImportPersenFromExcel(dto, _userContext.SellerId.Value, out errorMessages, customerId);
            if (errorMessages.Count > 0)
            {
                foreach (var er in errorMessages)
                {
                    ViewBag.msg += "\n" + er;
                }
                return View();
            }
            var result = await _personService.AddBulkBuyer(persenData);
            if (result.Success)
            {
                string retUrl = Url.Action("PersenList", "Persen", new { Area = "" });
                return RedirectToAction(retUrl);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ImportProducts()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportProducts(IFormFile ExcelFile)
        {
            clsResult result = new clsResult();
            result.ShowMessage = true;

            if (!_userContext.SellerId.HasValue)
                return NoContent();
            ExcelImporter importer = new ExcelImporter();
            List<string> errorMessages;

            var productsData = importer.ImportProductsFromExcel(ExcelFile, _userContext.SellerId.Value, out errorMessages);
            if (errorMessages.Count > 0)
            {
                foreach (var er in errorMessages)
                {
                    result.Message += "\n" + er;
                }
                return Json(result.ToJsonResult());
            }
            result = await _productService.CreateProductsInBulkAsync(productsData, _userContext.SellerId.Value);
            if (result.Success)
            {
                result.returnUrl = Url.Action("products", "product", new { Area = "Warehouse" });
                result.updateType = 1;
            }
            return Json(result.ToJsonResult());
        }
    }
}
