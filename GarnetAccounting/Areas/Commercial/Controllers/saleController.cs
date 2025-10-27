using ClosedXML.Excel;
using GarnetAccounting.Areas.Commercial.ComercialInterfaces;
using GarnetAccounting.Areas.Commercial.Dtos;
using GarnetAccounting.Areas.Warehouse.Dto;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Classes;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.CommercialInterfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Commercial.Controllers
{
    [Area("Commercial")]
    [Authorize]
    public class saleController : Controller
    {
        private readonly IInvoiceService _saleService;
        private readonly UserContextService _userContext;
        private readonly IBuyerService _buyerService;
        private readonly IWhProductService _product;
        private readonly IPersonService _person;
        private readonly IGeneralService _gs;
        public saleController(IInvoiceService saleInvoiceService
            , UserContextService userContextService
            , IBuyerService buyerService
            , IWhProductService whProductService
            , IPersonService personService,
              IGeneralService gs)
        {
            _saleService = saleInvoiceService;
            _userContext = userContextService;
            _buyerService = buyerService;
            _product = whProductService;
            _person = personService;
            _gs = gs;
        }

        public async Task<IActionResult> saleInvoices(InvoiceFilterDto filter)
        {
            if (!_userContext.SellerId.HasValue)
                return NotFound();
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = _userContext.PeriodId;
            filter.Invoicetype = 2;

            VmInvoices model = new VmInvoices();
            model.filter = filter;

            var invoices = _saleService.GetInvoices(filter);
            var paginatedInvoices = Pagination<InvoiceHeaderDto>.Create(invoices, filter.CurrentPage, filter.PageSize);
            model.Invoices = paginatedInvoices;
            ViewBag.buyers = await _buyerService.SelectList_Buyers(_userContext.SellerId.Value);

            return View(model);
        }

        public async Task<IActionResult> saleReports(InvoiceFilterDto filter)
        {
            if (!_userContext.SellerId.HasValue)
                return NotFound();
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = _userContext.PeriodId;
            filter.Invoicetype = 2;

            VmInvoices model = new VmInvoices();
            model.filter = filter;

            var invoices = await _saleService.GetInvoicesGroupedByCustomer(filter);
            var paginatedInvoices = Pagination<InvoiceHeaderDto>.Create(invoices.AsQueryable(), filter.CurrentPage, filter.PageSize);
            model.Invoices = paginatedInvoices;
            ViewBag.buyers = await _buyerService.SelectList_Buyers(_userContext.SellerId.Value);
            return View(model);
        }

        public async Task<IActionResult> saleTotalReport(SaleTotalReportDto dto)
        {
            if (!_userContext.SellerId.HasValue)
                return NotFound();
            dto.filter.SellerId = _userContext.SellerId.Value;
            dto.filter.PeriodId = _userContext.PeriodId;
            dto.filter.Invoicetype = 2;

            if (dto.filter.FromBody)
                dto = await _saleService.GetTotalReportAsync(dto);
            ViewBag.buyers = await _buyerService.SelectList_Buyers(_userContext.SellerId.Value);
            dto.filter.FromBody = true;
            return View(dto);
        }

        [HttpGet]
        public IActionResult comSaleInvoices(InvoiceFilterDto filter)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateSaleInvoice()
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            ViewBag.InvoiceNumber = await _saleService.GenerateSaleInvoiceNumberAsync(_userContext.SellerId.Value);
            ViewBag.Buyers = await _buyerService.SelectList_Buyers(_userContext.SellerId.Value);
            ViewBag.SettelmentType = _saleService.SelectList_SettelmentType();
            return PartialView("_CreateSaleInvoice");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSaleInvoice(InvoiceHeaderDto dto)
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
                result = await _saleService.CreateInvoiceHeaderAsync(dto);

                if (result.Success)
                {
                    result.ShowMessage = false;
                    result.updateType = 1;
                    result.returnUrl = Url.Action("invoice", "sale", new { Area = "Commercial", id = dto.Id });
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

        public async Task<IActionResult> invoice(Guid id)
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            var invoice = await _saleService.GetInvoiceByIdAsync(id);

            ViewBag.Buyers = await _buyerService.SelectList_Buyers(_userContext.SellerId.Value);
            ViewBag.SettelmentType = _saleService.SelectList_SettelmentType();
            ViewBag.products = await _product.SelectList_ProductsAsync(new ProductFilter() { SellerId = _userContext.SellerId.Value });

            return View(invoice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHeaderInvoice(InvoiceDto model)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (!_userContext.SellerId.HasValue) return NoContent();
            model.InvoiceHeader.EditorUserId = User.Identity.Name;
            model.InvoiceHeader.LastUpdate = DateTime.Now;
            model.InvoiceHeader.SellerId = _userContext.SellerId.Value;

            result = await _saleService.UpdateInvoiceHeaderAsync(model.InvoiceHeader);
            if (result.Success)
            {
                result.ShowMessage = false;
                result.updateType = 1;
                result.returnUrl = Url.Action("invoice", "sale", new { Area = "Commercial", id = model.InvoiceHeader.Id });
                return Json(result.ToJsonResult());

            }

            return Json(result.ToJsonResult());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addInvoiceItem(InvoiceDto model)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (!_userContext.SellerId.HasValue) return NoContent();
            model.dto.CreatorUserId = User.Identity.Name;
            model.dto.CreationTime = DateTime.Now;

            result = await _saleService.AddInvoiceItemAsync(model.dto);
            if (result.Success)
            {
                result.ShowMessage = false;
                result.updateType = 1;
                result.returnUrl = Url.Action("invoice", "sale", new { Area = "Commercial", id = model.dto.InvoiceId });
                return Json(result.ToJsonResult());

            }

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> EditInvoiceItem(Guid id)
        {

            var item = await _saleService.GetInvoiceItemByIdAsync(id);
            if (item == null || !_userContext.SellerId.HasValue) return NoContent();
            ViewBag.products = await _product.SelectList_ProductsAsync(new ProductFilter() { SellerId = _userContext.SellerId.Value });

            return PartialView("_EditInvoiceItem", item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInvoiceItem(InvoiceItemDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (!_userContext.SellerId.HasValue) return NoContent();
            dto.EditorUserId = User.Identity.Name;
            dto.LastUpdate = DateTime.Now;

            result = await _saleService.updateInvoiceItemAsync(dto);
            if (result.Success)
            {
                result.ShowMessage = false;
                result.updateType = 1;
                result.returnUrl = Url.Action("invoice", "sale", new { Area = "Commercial", id = dto.InvoiceId });
                return Json(result.ToJsonResult());

            }

            return Json(result.ToJsonResult());
        }

        //
        [HttpPost]
        public async Task<IActionResult> DeleteInvoiceItem(Guid itemId)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _saleService.DeleteInvoiceItemAsync(itemId);

            if (result.Success)
            {
                result.ShowMessage = false;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }
        [HttpPost]
        public async Task<IActionResult> DeleteInvoice(Guid itemId)
        {
            clsResult result = new clsResult();
            result.Success = true;
            result.updateType = 1;
            result.ShowMessage = true;

            if (_userContext.SellerId == null || _userContext.PeriodId == null)
            {
                result.Message = "شرکت فعال و یا سال مالی انتخاب نشده است.";
                return Json(result.ToJsonResult());
            }

            result = await _saleService.DeleteInvoiceAsync(itemId);

            if (result.Success)
            {
                result.ShowMessage = false;
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }

            return Json(result.ToJsonResult());
        }


        [HttpGet]
        public async Task<IActionResult> GetProductInfo(long id)
        {
            var product = await _product.GetProductByIdAsync(id);
            if (product == null)
                return NoContent();
            return Json(product);
        }

        [HttpGet]
        public IActionResult bulkCreateInvoice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> bulkCreateInvoice(IFormFile ExcelFile, short template)
        {
            clsResult result = new clsResult();
            result.Success = false;
            ExcelImporter importer = new ExcelImporter();
            var data = new InvoiceImportDto_Atiran();
            if (template == 404)
                data = importer.ReadInvoicesFromAtiran404Excel(ExcelFile);
            else
                data = importer.ReadInvoicesFromAtiranExcel(ExcelFile);

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

            var dataToAdd = await _saleService.PrepareInvoiceToCreate_AtiranAsync(data, 2);
            result = await _saleService.CreateInvoiceInBulkAsync(dataToAdd);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.Success = true;
            }

            return Json(result.ToJsonResult());
        }
        public async Task<IActionResult> print_Invoice(Guid id)
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();
            var invoice = await _saleService.GetInvoiceByIdAsync(id);
            invoice.buyerInfo = await _person.GetPersonDtoAsync(invoice.InvoiceHeader.PartyId);
            invoice.sellerInfo = await _person.GetPersonDtoAsync(invoice.InvoiceHeader.SellerId.Value);
            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> print_Invoices(Guid[] items)
        {
            var invoices = await _saleService.GetInvoicesFuulDataAsync(items);
            return View(invoices);
        }

        public async Task<IActionResult> print_saleInvoices(InvoiceFilterDto filter)
        {
            if (!_userContext.SellerId.HasValue)
                return NotFound();
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = _userContext.PeriodId;

            VmInvoices model = new VmInvoices();
            model.filter = filter;

            var invoices = _saleService.GetInvoices(filter);
            var paginatedInvoices = Pagination<InvoiceHeaderDto>.Create(invoices, filter.CurrentPage, filter.PageSize);
            model.Invoices = paginatedInvoices;

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);

            string startDate = !string.IsNullOrEmpty(filter.srtFromDate) ? filter.srtFromDate.PersianToLatin().LatinToPersian() : " ابتدای دوره ";
            string endDate = !string.IsNullOrEmpty(filter.srtToDate) ? filter.srtToDate.PersianToLatin().LatinToPersian() : " ...";

            ViewBag.ReportDate = $" از {startDate} لغایت {endDate}";
            ViewBag.SellerName = userSett.ActiveSellerName;
            ViewBag.PeriodName = userSett.ActiveSellerPeriodName;
            ViewBag.ReportTitle = "گزارش فروش";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TagInvoices(Guid[] items)
        {
            var result = await _saleService.TagInvoicesAsync(items);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }
            return Json(result.ToJsonResult());
        }
        [HttpPost]
        public async Task<IActionResult> UnTagInvoices(Guid[] items)
        {
            var result = await _saleService.UnTagInvoicesAsync(items);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }
            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> PrintSelectedInvoice(Guid[] items)
        {
            if (!_userContext.SellerId.HasValue)
                return NotFound();
            var model = await _saleService.GetSelectedInvoicesAsync(items);

            var userSett = await _gs.GetUserSettingAsync(User.Identity.Name);
            ViewBag.SellerName = userSett.ActiveSellerName;
            ViewBag.PeriodName = userSett.ActiveSellerPeriodName;
            ViewBag.ReportTitle = "گزارش فروش";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TagInvoice(Guid id)
        {
            var result = await _saleService.TagTogglerInvoicesAsync(id);
            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }
            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public IActionResult CopySelectedInvoices(Guid[] items)
        {
            if (!_userContext.SellerId.HasValue)
                return NoContent();

            CoppyInvoiceSettingDto model = new CoppyInvoiceSettingDto();
            model.InvoicesId = items.ToList();
            model.SellerId = _userContext.SellerId.Value;
            model.PeriodId = _userContext.PeriodId.Value;

            return PartialView("_CopySelectedInvoices", model);
        }
        [HttpPost]
        public async Task<IActionResult> deleteDuplacated(Guid[] items)
        {
            var result = await _saleService.DeleteDuplacatedInvoicesAsync(items);

            if (result.Success)
            {
                result.updateType = 1;
                result.returnUrl = Request.Headers["Referer"].ToString();
            }
            return Json(result.ToJsonResult());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSelectedInvoices(CoppyInvoiceSettingDto model)
        {
            clsResult result = new clsResult();
            if (model.InvoicesId.Count == 0)
            {
                result.Message = "اطلاعاتی برای کپی کردن دریافت نشد";
                result.ShowMessage = true;
                return Json(result.ToJsonResult());
            }

            result = await _saleService.CopyInvoicesAsync(model);
            result.updateType = 1;
            result.returnUrl = Request.Headers["Referer"].ToString();

            return Json(result.ToJsonResult());
        }


        [HttpGet]
        public async Task<IActionResult> StufSaleExcelReport(InvoiceFilterDto filter)
        {
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = _userContext.PeriodId;
            // دریافت لیست فاکتورها
            var invoices = await _saleService.SaleReportByStufAsync(filter);

            // ایجاد فایل Excel با ClosedXML
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Invoices");

                // تنظیم جهت نوشتار به راست-به-چپ
                worksheet.RightToLeft = true;

                // افزودن سرستون‌ها
                worksheet.Cell(1, 1).Value = "ردیف";
                worksheet.Cell(1, 2).Value = "شناسه کالا/خدمت";
                worksheet.Cell(1, 3).Value = "نام کالا/خدمت";
                worksheet.Cell(1, 4).Value = "واحد اندازه‌گیری";
                worksheet.Cell(1, 5).Value = "مقدار فروخته شده";
                worksheet.Cell(1, 6).Value = "جمع مبلغ فروش قبل از تخفیف";
                worksheet.Cell(1, 7).Value = "جمع تخفیفات";
                worksheet.Cell(1, 8).Value = "جمع مبلغ فروش بعد از تخفیف";
                worksheet.Cell(1, 9).Value = "نرخ ارزش افزوده";
                worksheet.Cell(1, 10).Value = "جمع مبلغ ارزش افزوده";
                worksheet.Cell(1, 11).Value = "جمع خالص فروش بعلاوه ارزش افزوده";

                // تنظیم استایل سرستون‌ها
                var headerRange = worksheet.Range(1, 1, 1, 11);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                int row = 2;
                decimal totalAmount = 0;
                decimal totalPrice = 0;
                decimal totalDiscountPrice = 0;
                decimal totalTotalPriceAfterDiscount = 0;
                decimal totalVatRate = 0;
                decimal totalVatPrice = 0;
                decimal totalFinalPrice = 0;

                // وارد کردن داده‌ها به ورک‌شیت
                foreach (var invoice in invoices)
                {
                    worksheet.Cell(row, 1).Value = row - 1; // ردیف
                    worksheet.Cell(row, 2).Value = invoice.stuffUID;
                    worksheet.Cell(row, 3).Value = invoice.ProductOrServiceName;
                    worksheet.Cell(row, 4).Value = invoice.UnitCountName;
                    worksheet.Cell(row, 5).Value = Math.Round(invoice.Amount, 1); // Round to one decimal place
                    worksheet.Cell(row, 6).Value = Math.Round(invoice.Price);
                    worksheet.Cell(row, 7).Value = Math.Round(invoice.DiscountPrice);
                    worksheet.Cell(row, 8).Value = Math.Round(invoice.TotalPriceAfterDiscount);
                    worksheet.Cell(row, 9).Value = Math.Round(invoice.VatRate.Value);
                    worksheet.Cell(row, 10).Value = Math.Round(invoice.VatPrice);
                    worksheet.Cell(row, 11).Value = Math.Round(invoice.FinalPrice);

                    // تنظیم فرمت عددی
                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0.0"; // Number with separator and one decimal
                    for (int col = 6; col <= 11; col++)
                    {
                        worksheet.Cell(row, col).Style.NumberFormat.Format = "#,##0"; // Number with separator
                    }

                    // تنظیم مرزهای سلول‌ها
                    var dataRange = worksheet.Range(row, 1, row, 11);
                    dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    // محاسبه جمع ستون‌ها
                    totalAmount += Math.Round(invoice.Amount, 1);
                    totalPrice += Math.Round(invoice.Price);
                    totalDiscountPrice += Math.Round(invoice.DiscountPrice);
                    totalTotalPriceAfterDiscount += Math.Round(invoice.TotalPriceAfterDiscount);
                    totalVatRate += Math.Round(invoice.VatRate.Value);
                    totalVatPrice += Math.Round(invoice.VatPrice);
                    totalFinalPrice += Math.Round(invoice.FinalPrice);

                    row++;
                }

                // اضافه کردن جمع کل
                worksheet.Cell(row, 1).Value = "جمع";
                worksheet.Cell(row, 1).Style.Font.Bold = true;

                worksheet.Cell(row, 5).Value = totalAmount;
                worksheet.Cell(row, 6).Value = totalPrice;
                worksheet.Cell(row, 7).Value = totalDiscountPrice;
                worksheet.Cell(row, 8).Value = totalTotalPriceAfterDiscount;
                worksheet.Cell(row, 9).Value = totalVatRate;
                worksheet.Cell(row, 10).Value = totalVatPrice;
                worksheet.Cell(row, 11).Value = totalFinalPrice;

                // تنظیم فرمت عددی برای جمع کل
                worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0.0"; // Number with separator and one decimal
                for (int col = 6; col <= 11; col++)
                {
                    worksheet.Cell(row, col).Style.NumberFormat.Format = "#,##0"; // Number with separator
                }

                // تنظیم استایل جمع کل
                var totalRange = worksheet.Range(row, 1, row, 11);
                totalRange.Style.Font.Bold = true;
                totalRange.Style.Fill.BackgroundColor = XLColor.LightGreen;
                totalRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                totalRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // تنظیم خودکار عرض ستون‌ها
                worksheet.Columns().AdjustToContents();

                // ذخیره فایل Excel به صورت بایت‌آرایه
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var excelData = stream.ToArray();

                    // بازگشت فایل اکسل به صورت FileContentResult برای دانلود
                    return new FileContentResult(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "Garnet Invoices list.xlsx"
                    };
                }
            }

        }

        [HttpGet]
        public async Task<IActionResult> SalesDetailsExcelReport(InvoiceFilterDto filter)
        {
            filter.SellerId = _userContext.SellerId.Value;
            filter.PeriodId = _userContext.PeriodId;
            // دریافت لیست فاکتورها
            var invoices = await _saleService.GetSalesDetailsAsync(filter);

            // ایجاد فایل Excel با ClosedXML
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Invoices");

                // تنظیم جهت نوشتار به راست-به-چپ
                worksheet.RightToLeft = true;

                // افزودن سرستون‌ها
                worksheet.Cell(1, 1).Value = "ردیف";
                worksheet.Cell(1, 2).Value = "شماره فاکتور";
                worksheet.Cell(1, 3).Value = "تاریخ";
                worksheet.Cell(1, 4).Value = "مشتری";
                worksheet.Cell(1, 5).Value = "محصول";
                worksheet.Cell(1, 6).Value = "شناسه کالا";
                worksheet.Cell(1, 7).Value = "فی";
                worksheet.Cell(1, 8).Value = "مقدار";
                worksheet.Cell(1, 9).Value = "جمع مبلغ";
                worksheet.Cell(1, 10).Value = "تخفیف";
                worksheet.Cell(1, 11).Value = "مبلغ پس از تخفیف";
                worksheet.Cell(1, 12).Value = "نرخ ارزش افزوده";
                worksheet.Cell(1, 13).Value = "مبلغ ارزش افزوده";
                worksheet.Cell(1, 14).Value = "قابل پرداخت";

                // تنظیم استایل سرستون‌ها
                var headerRange = worksheet.Range(1, 1, 1, 14);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                int row = 2;
                decimal totalAmount = 0;
                decimal totalPrice = 0;
                decimal totalDiscountPrice = 0;
                decimal totalTotalPriceAfterDiscount = 0;
                decimal totalVatRate = 0;
                decimal totalVatPrice = 0;
                decimal totalFinalPrice = 0;

                // وارد کردن داده‌ها به ورک‌شیت
                foreach (var invoice in invoices)
                {
                    worksheet.Cell(row, 1).Value = row - 1; // ردیف
                    worksheet.Cell(row, 2).Value = invoice.InvoiceNumber;
                    worksheet.Cell(row, 3).Value = invoice.InvoicePersianDate;
                    worksheet.Cell(row, 4).Value = invoice.Buyer;
                    worksheet.Cell(row, 5).Value = invoice.ProductOrServiceName;
                    worksheet.Cell(row, 6).Value = invoice.stuffUID;
                    worksheet.Cell(row, 7).Value = Math.Round(invoice.UnitPrice);
                    worksheet.Cell(row, 8).Value = Math.Round(invoice.Amount, 1);
                    worksheet.Cell(row, 9).Value = Math.Round(invoice.Price);
                    worksheet.Cell(row, 10).Value = Math.Round(invoice.DiscountPrice);
                    worksheet.Cell(row, 11).Value = Math.Round(invoice.TotalPriceAfterDiscount);
                    worksheet.Cell(row, 12).Value = Math.Round(invoice.VatRate ?? 0);
                    worksheet.Cell(row, 13).Value = Math.Round(invoice.VatPrice);
                    worksheet.Cell(row, 14).Value = Math.Round(invoice.FinalPrice);

                    // تنظیم فرمت عددی
                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0.0"; // Number with separator and one decimal
                    for (int col = 6; col <= 14; col++)
                    {
                        worksheet.Cell(row, col).Style.NumberFormat.Format = "#,##0"; // Number with separator
                    }

                    // تنظیم مرزهای سلول‌ها
                    var dataRange = worksheet.Range(row, 1, row, 14);
                    dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    // محاسبه جمع ستون‌ها
                    totalAmount += Math.Round(invoice.Amount, 1);
                    totalPrice += Math.Round(invoice.Price);
                    totalDiscountPrice += Math.Round(invoice.DiscountPrice);
                    totalTotalPriceAfterDiscount += Math.Round(invoice.TotalPriceAfterDiscount);
                    totalVatRate += Math.Round(invoice.VatRate.Value);
                    totalVatPrice += Math.Round(invoice.VatPrice);
                    totalFinalPrice += Math.Round(invoice.FinalPrice);

                    row++;
                }

                // اضافه کردن جمع کل
                worksheet.Cell(row, 1).Value = "جمع";
                worksheet.Cell(row, 1).Style.Font.Bold = true;

                worksheet.Cell(row, 8).Value = totalAmount;
                worksheet.Cell(row, 9).Value = totalPrice;
                worksheet.Cell(row, 10).Value = totalDiscountPrice;
                worksheet.Cell(row, 11).Value = totalTotalPriceAfterDiscount;
                worksheet.Cell(row, 12).Value = totalVatRate;
                worksheet.Cell(row, 13).Value = totalVatPrice;
                worksheet.Cell(row, 14).Value = totalFinalPrice;

                // تنظیم فرمت عددی برای جمع کل
                worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0.0"; // Number with separator and one decimal
                for (int col = 6; col <= 14; col++)
                {
                    worksheet.Cell(row, col).Style.NumberFormat.Format = "#,##0"; // Number with separator
                }

                // تنظیم استایل جمع کل
                var totalRange = worksheet.Range(row, 1, row, 14);
                totalRange.Style.Font.Bold = true;
                totalRange.Style.Fill.BackgroundColor = XLColor.LightGreen;
                totalRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                totalRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // تنظیم خودکار عرض ستون‌ها
                worksheet.Columns().AdjustToContents();

                // ذخیره فایل Excel به صورت بایت‌آرایه
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var excelData = stream.ToArray();

                    // بازگشت فایل اکسل به صورت FileContentResult برای دانلود
                    return new FileContentResult(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "Garnet Invoices list.xlsx"
                    };
                }
            }

        }
    }
}
