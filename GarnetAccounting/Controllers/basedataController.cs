using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Controllers
{
    [Authorize]
    public class basedataController : Controller
    {
        private readonly IAccGetBaseDataService _baseData;
        private readonly UserContextService _userContext;
        public basedataController(IAccGetBaseDataService baseDataService, UserContextService userContext)
        {
            _baseData = baseDataService;
            _userContext = userContext;
        }

        [HttpPost]
        public async Task<IActionResult> GetTafsilByGroup(int? groupId)
        {
            if (_userContext.SellerId == null) return Ok();
            var kols = await _baseData.GetTafsilByTafsilGroupAsync(_userContext.SellerId.Value, groupId);
            return Json(kols);
        }
        [HttpPost]
        public async Task<IActionResult> GetKolsByTafsil(List<long>? items)
        {
            if (_userContext.SellerId == null) return Ok();
            var kols = await _baseData.GetUsedKolsByTafsilAsync(_userContext.SellerId.Value, items);
            return Json(kols);
        }
        [HttpPost]
        public async Task<IActionResult> GetKolsCascading(List<long>? tafsils, List<int>? groups)
        {
            if (_userContext.SellerId == null) return Ok();

            var kols = await _baseData.GetCascadingUsedKolsAsync(_userContext.SellerId.Value, groups, tafsils);
            return Json(kols);
        }

        [HttpPost]
        public async Task<IActionResult> GetMoeinsByKolAndTafsil(List<long>? tafsils, List<int>? kols)
        {
            if (_userContext.SellerId == null) return Ok();
            var moeins = await _baseData.GetUsedMoeinsByKolAndTafsilAsync(_userContext.SellerId.Value, tafsils, kols);
            return Json(moeins);
        }
        [HttpPost]
        public async Task<IActionResult> GetMoeinsByKols(List<int>? items)
        {
            if (_userContext.SellerId == null) return Ok();
            var moeins = await _baseData.GetUsedMoeinsByKolsAsync(_userContext.SellerId.Value, items);
            return Json(moeins);
        }

        [HttpPost]
        public async Task<IActionResult> GetTafsil5ByTafsil4(List<long?> tafsils)
        {
            if (_userContext.SellerId == null) return Ok();

            var tafsil5 = await _baseData.GetTafsil5Async(_userContext.SellerId.Value, tafsils);
            return Json(tafsil5);
        }
    }
}
