using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Commercial.ComercialInterfaces;
using GarnetAccounting.Areas.Commercial.Dtos;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.CommercialInterfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    [Authorize(Roles = "AccountingManager,AccountingBoss,AccountingUser")]
    public class DocCreatorController : Controller
    {
        private readonly IAccImportService _importService;
        private readonly IGeneralService _gs;
        private readonly IAccSettingService _sett;
        private readonly IInvoiceService _saleService;
        private readonly UserContextService _userContext;
        private readonly IBuyerService _buyerService;
        private readonly IPersonService _person;
        private readonly IAccDocCreatorService _docCreator;
        private readonly IAccCodingService _coding;

        public DocCreatorController(IAccImportService service
            , IGeneralService gs
            , IAccSettingService accSettingService
            , IInvoiceService saleService
            , UserContextService userContext
            , IBuyerService buyerService
            , IPersonService person
            , IAccDocCreatorService docCreator,
              IAccCodingService coding)
        {
            _importService = service;
            _gs = gs;
            _sett = accSettingService;
            _saleService = saleService;
            _userContext = userContext;
            _buyerService = buyerService;
            _person = person;
            _docCreator = docCreator;
            _coding = coding;
        }

        public async Task<IActionResult> GetData(DocCreatorFilterDto filter)
        {
            if (!_userContext.SellerId.HasValue)
                return NotFound();
            VmDocCreator model = new VmDocCreator();
            model.filter = new DocCreatorFilterDto();
            if (filter.DocType.HasValue)
            {
                filter.SellerId = _userContext.SellerId.Value;
                filter.PeriodId = _userContext.PeriodId;
                filter.DocType = filter.DocType;

                model.filter = filter;
                var invoices = _saleService.GetInvoicesForCreateDoc(filter);
                var paginatedInvoices = Pagination<InvoiceHeaderDto>.Create(invoices, filter.CurrentPage, filter.PageSize);
                model.Invoices = paginatedInvoices;
            }

            ViewBag.buyers = await _buyerService.SelectList_Buyers(_userContext.SellerId.Value);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoiceDoc(List<Guid> items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            if (items?.Count == 0)
            {
                result.Message = "اطلاعاتی جهت ثبت سند درسافت نشد";
                return Json(result.ToJsonResult());
            }

            result = await _docCreator.CreatInvoiceDocAsync(items, User.Identity.Name);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
            }

            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> CreatetransactionDoc(List<long> items, int transactionsType, int BankId)
        {
            if (!_userContext.SellerId.HasValue || !_userContext.PeriodId.HasValue) return NoContent();

            BankTransactionsCreateDocDto model = new BankTransactionsCreateDocDto();
            model.SellerId = _userContext.SellerId.Value;
            model.PeriodId = _userContext.PeriodId.Value;
            model.TransactionsId = items;
            model.TransactionsType = transactionsType;
            model.BankAccountId = BankId;

            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(model.SellerId);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(model.SellerId);

            return PartialView("_CreatetransactionDoc", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatetBankReportDoc(BankTransactionsCreateDocDto model)
        {
            if (!_userContext.SellerId.HasValue || !_userContext.PeriodId.HasValue) return NoContent();

            clsResult result = new clsResult();
            result.Success = false;

            if (ModelState.IsValid)
            {
                result = await _docCreator.CreateBankDocAsync(model);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var er in errors)
            {
                result.Message += "\n" + er.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        //----------------- Moadian ---------------------------------- Moadian ------------------------------------- Moadian -------------

        [HttpPost]
        public async Task<IActionResult> CreatetMoadianDoc(List<long> items, short transactionsType)
        {
            if (!_userContext.SellerId.HasValue || !_userContext.PeriodId.HasValue) return NoContent();

            MoadianCreateDocDto model = new MoadianCreateDocDto();
            model.SellerId = _userContext.SellerId.Value;
            model.PeriodId = _userContext.PeriodId.Value;
            model.TransactionsId = items;
            model.transactionsType = transactionsType;

            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(model.SellerId);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(model.SellerId);

            return PartialView("_CreatetMoadianDoc", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertMoadianDoc(MoadianCreateDocDto model)
        {
            if (!_userContext.SellerId.HasValue || !_userContext.PeriodId.HasValue) return NoContent();

            clsResult result = new clsResult();
            result.Success = false;

            if (ModelState.IsValid)
            {
                result = await _docCreator.CreateMoadianDocAsync(model);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.updateType = 1;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }

            var errors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var er in errors)
            {
                result.Message += "<br>" + er.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }
    }
}
