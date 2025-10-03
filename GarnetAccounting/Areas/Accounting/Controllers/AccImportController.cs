using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    [Authorize(Roles = "AccountingManager,AccountingBoss,AccountingUser")]
    public class AccImportController : Controller
    {
        private readonly IAccImportService _importService;
        private readonly IGeneralService _gs;
        public AccImportController(IAccImportService service, IGeneralService gs)
        {
            _importService = service;
            _gs = gs;
        }

        public IActionResult ImportCustomCoding()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportCustomCoding(IFormFile file)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;


            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                return BadRequest(result);
            }
            long sellerId = userSett.ActiveSellerId.Value;
            var coding = await _importService.GetCodingFromExcelAsync(file, sellerId);
            return View();
        }


        public async Task<IActionResult> ImportDocs(IFormFile file)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;


            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            if (userSett == null || userSett?.ActiveSellerPeriod == null || userSett?.ActiveSellerId == null)
            {
                //result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                //return Json(result.ToJsonResult());
                return BadRequest(result);
            }

            long sellerId = userSett.ActiveSellerId.Value;
            int periodId = userSett.ActiveSellerPeriod.Value;

            var data = _importService.GetDocFromExl_General(file);
            //var artics = _importService.AssignDocumentNumbers(data, sellerId, periodId);
            result = await _importService.AddBulkDocsAsync(data, User.Identity.Name, sellerId, periodId);

            return View(data);
        }

    }
}
