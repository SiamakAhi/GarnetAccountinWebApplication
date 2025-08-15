using GarnetAccounting.Areas.Commercial.ComercialInterfaces;
using GarnetAccounting.Areas.Commercial.Dtos;
using GarnetAccounting.Areas.Warehouse.Dto;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Classes;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.PartyDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Commercial.Controllers
{
    [Area("Commercial")]
    [Authorize]
    public class buyController : Controller
    {

        private readonly IPersonService _person;
        private readonly UserContextService _userContext;
        private readonly IInvoiceService _invoice;
        private readonly IWhProductService _product;
        private readonly IGeneralService _gs;

        public buyController(IPersonService personService
            , UserContextService userContextService
            , IInvoiceService invoiceService
            , IWhProductService productService
            , IGeneralService generalService
            )
        {
            _person = personService;
            _userContext = userContextService;
            _invoice = invoiceService;
            _product = productService;
            _gs = generalService;
        }

        public IActionResult vendors(PersonFilterDto filter)
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            filter.SellerId = _userContext.SellerId.Value;
            filter.isVendor = true;
            filter.PageSize = 100;
            var dataQuery = _person.PersenAsQuery(filter);
            PersonListDto model = new PersonListDto();
            model.filter = filter;
            model.persen = Pagination<PersonDto>.Create(dataQuery, filter.CurrentPage, filter.PageSize);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> addVendor()
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            ViewBag.customerId = _userContext.CustomerId.Value;
            ViewBag.SellerId = _userContext.SellerId.Value;
            ViewBag.PartyTypes = await _person.SelectList_PartyTypeAsync();
            return PartialView("_addVendor");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addVendor(PersonDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            string userName = User.Identity.Name;

            if (string.IsNullOrEmpty(userName) || !_userContext.SellerId.HasValue)
            {
                result.Message = "شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                result = await _person.AddPersonAsync(dto);
                if (result.Success)
                {
                    result.Success = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.ShowMessage = true;
                    return Json(result.ToJsonResult());
                }
            }
            var modelErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message = "\n " + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());

        }

        //Invoice
        public async Task<IActionResult> Invoices(InvoiceFilterDto filter)
        {
            if (!_userContext.SellerId.HasValue)
                return NotFound();
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = _userContext.PeriodId;
            filter.Invoicetype = 1;

            VmInvoices model = new VmInvoices();
            model.filter = filter;

            var invoices = _invoice.GetInvoices(filter);
            var paginatedInvoices = Pagination<InvoiceHeaderDto>.Create(invoices, filter.CurrentPage, filter.PageSize);
            model.Invoices = paginatedInvoices;
            ViewBag.vendors = await _person.SelectList_PersenListAsync(_userContext.SellerId.Value, true, null);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatebuyInvoice()
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            ViewBag.vendors = await _person.SelectList_PersenListAsync(_userContext.SellerId.Value, true, null);
            ViewBag.SettelmentType = _invoice.SelectList_SettelmentType();
            return PartialView("_CreatebuyInvoice");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatebuyInvoice(InvoiceHeaderDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }
            dto.SellerId = _userContext.SellerId.Value;
            dto.FinancePeriodId = _userContext.PeriodId;
            dto.InvoiceSubject = 1;
            dto.CreatorUserId = User.Identity.Name;
            dto.InvoiceDate = dto.strInvoiceDate.PersianToLatin();

            if (ModelState.IsValid)
            {
                result = await _invoice.CreateInvoiceHeaderAsync(dto);
                if (result.Success)
                {
                    result.returnUrl = Url.Action("buyInvoice", "buy", new { Area = "Commercial", id = dto.Id });
                    return Json(result.ToJsonResult());
                }
            }

            // در صورت وجود خطا در ModelState
            var modelErrors = ModelState.Values.SelectMany(e => e.Errors).ToList();
            foreach (var error in modelErrors)
            {
                result.Message += "<br>" + error.ErrorMessage;
            }

            return Json(result.ToJsonResult());
        }

        //
        public async Task<IActionResult> buyInvoice(Guid id)
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            var invoice = await _invoice.GetInvoiceByIdAsync(id);

            ViewBag.vendors = await _person.SelectList_PersenListAsync(_userContext.SellerId.Value, true, null);
            ViewBag.SettelmentType = _invoice.SelectList_SettelmentType();
            ViewBag.products = await _product.SelectList_ProductsAsync(new ProductFilter() { SellerId = _userContext.SellerId.Value });

            return View(invoice);
        }

        //---------------------------------------------------
        [HttpGet]
        public IActionResult bulkCreateBuyInvoice()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> bulkCreateBuyInvoice(CreateBulkInvoiceDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            ExcelImporter importer = new ExcelImporter();
            var data = importer.ReadInvoicesFromAtiranExcel(dto.ExcelFile);
            if (data.Errors.Count > 0)
            {
                foreach (var er in data.Errors)
                {
                    result.Message += $"<br> {er.Code} - {er.Error}";
                }
                result.Success = false;
                result.ShowMessage = true;
                return Json(result.ToJsonResult());
            }

            var dataToAdd = await _invoice.PrepareInvoiceToCreate_AtiranAsync(data, 1);
            result = await _invoice.CreateInvoiceInBulkAsync(dataToAdd);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.Success = true;
            }

            return Json(result.ToJsonResult());
        }



    }
}
