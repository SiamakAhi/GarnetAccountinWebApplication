using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    [Authorize(Roles = "AccountingManager,AccountingBoss")]
    public class ElectronicBookManagerController : Controller
    {
        private readonly IAccEbookService _ebook;
        private readonly IAccExportService _export;
        private readonly IGeneralService _gs;

        public ElectronicBookManagerController(
            IAccExportService export
            , IGeneralService gs
            , IAccEbookService ebook)
        {
            _export = export;
            _gs = gs;
            _ebook = ebook;
        }

        public async Task<IActionResult> ElectronicBookFiles()
        {
            string userName = User.Identity.Name;
            var userSett = await _gs.GetUserSettingAsync(userName);
            if (userSett == null || userSett.ActiveSellerId == null || userSett.ActiveSellerPeriod == null) { return BadRequest("License not fine."); }
            long SellerId = userSett.ActiveSellerId.Value;
            int PeriodId = userSett.ActiveSellerPeriod.Value;
            EBookManagerDto model = new EBookManagerDto();
            return View(model);
        }


    }
}
