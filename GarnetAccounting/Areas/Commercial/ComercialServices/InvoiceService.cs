using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Commercial.ComercialInterfaces;
using GarnetAccounting.Areas.Commercial.Dtos;
using GarnetAccounting.Areas.Commercial.Models.Entities;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Models;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Commercial.ComercialServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _db;
        private readonly UserContextService _userContext;
        private readonly IWhProductService _productService;
        private readonly IPersonService _personService;

        public InvoiceService(AppDbContext dbcontext, UserContextService userContext, IWhProductService productService, IPersonService personService)
        {
            _db = dbcontext;
            _userContext = userContext;
            _productService = productService;
            _personService = personService;
        }
        public SelectList SelectList_SettelmentType()
        {
            var settlementTypes = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "نقد" },
        new SelectListItem { Value = "2", Text = "نسیه" },
        new SelectListItem { Value = "3", Text = "اعتباری" },
        new SelectListItem { Value = "4", Text = "نقد و نسیه" }
    };

            return new SelectList(settlementTypes, "Value", "Text");
        }
        public IQueryable<InvoiceHeaderDto> GetInvoices(InvoiceFilterDto filter)
        {
            var query = _db.Invoices
                .AsNoTracking()
                .Include(i => i.InvoiceItems)
                .Include(i => i.InvoiceParty) // برای دریافت نام طرف حساب
                .Where(x =>
                      x.InvoiceType == filter.Invoicetype
                   && x.SellerId == filter.SellerId)
                .AsQueryable();

            // فیلترها
            if (filter.PeriodId.HasValue)
            {
                query = query.Where(i => i.FinancePeriodId == filter.PeriodId.Value);
            }
            if (!string.IsNullOrEmpty(filter.InvoiceNumber))
            {
                query = query.Where(i => i.InvoiceNumber.Contains(filter.InvoiceNumber));
            }

            if (!string.IsNullOrEmpty(filter.srtFromDate))
            {
                DateTime startDate = filter.srtFromDate.PersianToLatin();
                query = query.Where(i => i.InvoiceDate >= startDate);
            }

            if (!string.IsNullOrEmpty(filter.srtToDate))
            {
                DateTime endDate = filter.srtToDate.PersianToLatin();
                query = query.Where(i => i.InvoiceDate <= endDate);
            }

            if (filter.Party.HasValue)
            {
                query = query.Where(i => i.PartyId == filter.Party.Value);
            }

            if (!string.IsNullOrEmpty(filter.Remark))
            {
                query = query.Where(i => i.Remarks.Contains(filter.Remark));
            }

            if (!string.IsNullOrEmpty(filter.Status))
            {
                query = query.Where(i => i.status.ToString() == filter.Status);
            }
            if (filter.Taged)
                query = query.Where(n => n.flag == true);

            // ایجاد DTO با اطلاعات کامل
            var invoices = query.Select(i => new InvoiceHeaderDto
            {
                Id = i.Id,
                SellerId = i.SellerId,
                FinancePeriodId = i.FinancePeriodId,
                InvoiceType = i.InvoiceType,
                InvoiceSubject = i.InvoiceSubject,
                InvoiceNumber = i.InvoiceNumber,
                TaxInvoiceNumber = i.TaxInvoiceNumber,
                InvoiceAutoNumber = i.InvoiceAutoNumber,
                SequenceNumber = i.SequenceNumber,
                ArchiveRef = i.ArchiveRef,
                InvoiceDate = i.InvoiceDate,
                PartyId = i.PartyId,
                PartyName = i.InvoiceParty.Name, // نام طرف حساب
                Remarks = i.Remarks,
                status = i.status,
                CreationTime = i.CreationTime,
                CreatorUserId = i.CreatorUserId,
                LastUpdate = i.LastUpdate,
                EditorUserId = i.EditorUserId,
                SettlementTypeName = i.SettlementTypeId.com_ToSettelmentTypeName(),
                SettlementTypeId = i.SettlementTypeId,
                taged = i.flag,
                // محاسبه جمع مبالغ فاکتور
                TotalNoTaxable = i.InvoiceItems.Where(x => x.VatRate == 0).Sum(item => item.PriceAfterDiscount),
                TotalTaxable = i.InvoiceItems.Where(x => x.VatRate > 0).Sum(item => item.PriceAfterDiscount),
                TotalPriceBeforDiscount = i.InvoiceItems.Sum(item => item.PriceBeForDescount),
                TotalDiscount = i.InvoiceItems.Sum(item => item.Discount),
                TotalPriceAfterDiscount = i.InvoiceItems.Sum(item => item.PriceAfterDiscount),
                TotalVatPrice = i.InvoiceItems.Sum(item => item.VatPrice),
                TotalFinalPrice = i.InvoiceItems.Sum(item => item.FinalPrice)
            })
            .OrderBy(i => i.InvoiceDate).ThenBy(n => n.InvoiceNumber)
            .AsQueryable();

            if (filter.WithVat.HasValue)
            {
                if (filter.WithVat.Value)
                    invoices = invoices.Where(n => n.TotalTaxable > 0);
                else if (!filter.WithVat.Value)
                    invoices = invoices.Where(n => n.TotalTaxable == 0);
            }

            return invoices;
        }

        public async Task<List<InvoiceHeaderDto>> GetSelectedInvoicesAsync(Guid[] ids)
        {
            var query = _db.Invoices
                .AsNoTracking()
                .Include(i => i.InvoiceItems)
                .Include(i => i.InvoiceParty) // برای دریافت نام طرف حساب
                .Where(x => ids.Contains(x.Id)).AsQueryable();

            // ایجاد DTO با اطلاعات کامل
            var invoices = await query.Select(i => new InvoiceHeaderDto
            {
                Id = i.Id,
                SellerId = i.SellerId,
                FinancePeriodId = i.FinancePeriodId,
                InvoiceType = i.InvoiceType,
                InvoiceSubject = i.InvoiceSubject,
                InvoiceNumber = i.InvoiceNumber,
                TaxInvoiceNumber = i.TaxInvoiceNumber,
                InvoiceAutoNumber = i.InvoiceAutoNumber,
                SequenceNumber = i.SequenceNumber,
                ArchiveRef = i.ArchiveRef,
                InvoiceDate = i.InvoiceDate,
                PartyId = i.PartyId,
                PartyName = i.InvoiceParty.Name, // نام طرف حساب
                Remarks = i.Remarks,
                status = i.status,
                CreationTime = i.CreationTime,
                CreatorUserId = i.CreatorUserId,
                LastUpdate = i.LastUpdate,
                EditorUserId = i.EditorUserId,
                SettlementTypeName = i.SettlementTypeId.com_ToSettelmentTypeName(),
                SettlementTypeId = i.SettlementTypeId,
                taged = i.flag,
                // محاسبه جمع مبالغ فاکتور
                TotalNoTaxable = i.InvoiceItems.Where(x => x.VatRate == 0).Sum(item => item.PriceAfterDiscount),
                TotalTaxable = i.InvoiceItems.Where(x => x.VatRate > 0).Sum(item => item.PriceAfterDiscount),
                TotalPriceBeforDiscount = i.InvoiceItems.Sum(item => item.PriceBeForDescount),
                TotalDiscount = i.InvoiceItems.Sum(item => item.Discount),
                TotalPriceAfterDiscount = i.InvoiceItems.Sum(item => item.PriceAfterDiscount),
                TotalVatPrice = i.InvoiceItems.Sum(item => item.VatPrice),
                TotalFinalPrice = i.InvoiceItems.Sum(item => item.FinalPrice)
            })
            .OrderBy(i => i.InvoiceDate).ThenBy(n => n.InvoiceNumber)
            .ToListAsync();

            return invoices;
        }
        public async Task<clsResult> DeleteDuplacatedInvoicesAsync(Guid[] ids)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            try
            {
                var invoices = _db.Invoices
                    .Where(x => ids.Contains(x.Id))
                    .ToList();

                var groupedInvoices = invoices
                    .GroupBy(i => i.InvoiceNumber)
                    .Select(g =>
                    {
                        var firstInvoice = g.First();
                        var duplicates = g.Skip(1).ToList();
                        return new { FirstInvoice = firstInvoice, Duplicates = duplicates };
                    })
                    .ToList();

                foreach (var group in groupedInvoices)
                {
                    _db.Invoices.RemoveRange(group.Duplicates);
                }

                await _db.SaveChangesAsync();

                result.Success = true;
                result.Message = "حذف فاکتورهای تکراری با موفقیت انجام شد";
                result.updateType = 1;
            }
            catch (Exception ex)
            {
                result.Message = "خطایی در انجام عملیات رخ داده است: " + ex.Message;
            }

            return result;
        }
        public async Task<string> GenerateSaleInvoiceNumberAsync(long sellerId)
        {
            int maxSequenceNumber = await _db.Invoices
                .Where(i => i.SellerId == sellerId)
                .MaxAsync(i => (int?)i.SequenceNumber) ?? 0;
            int nextSequenceNumber = maxSequenceNumber + 1;
            string invoiceNumber = $"{sellerId}2{nextSequenceNumber}";

            return invoiceNumber;
        }
        private async Task<bool> IsDupplicateInvoiceNumberAsync(long sellerId, string invoiceNumber)
        {
            return await _db.Invoices
                .AnyAsync(i => i.SellerId == sellerId && i.InvoiceNumber == invoiceNumber);
        }

        public async Task<clsResult> CreateInvoiceHeaderAsync(InvoiceHeaderDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (await IsDupplicateInvoiceNumberAsync(dto.SellerId.Value, dto.InvoiceNumber))
            {
                result.Message = "شماره فاکتور تکراری است";
                return result;
            }
            // بررسی دوره مالی
            var period = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == dto.FinancePeriodId.Value);
            if (period == null || period.SellerId != dto.SellerId || dto.InvoiceDate < period.StartDate || dto.InvoiceDate > period.EndDate)
            {
                result.Message = $"تاریخ فاکتور خارج از بازه دوره مالی {period?.Name} است";
                return result;
            }

            // تولید شماره فاکتور
            string invoiceNumber = dto.InvoiceNumber;
            int ssequNo = 0;
            if (dto.InvoiceType == 2)
            {
                invoiceNumber = await GenerateSaleInvoiceNumberAsync(dto.SellerId.Value);
                ssequNo = int.Parse(invoiceNumber.Substring(dto.SellerId.Value.ToString().Length + 1));
            }



            // ایجاد هدر فاکتور
            var invoice = new com_Invoice
            {
                Id = dto.Id,
                SellerId = dto.SellerId.Value,
                FinancePeriodId = dto.FinancePeriodId,
                InvoiceType = dto.InvoiceType,
                InvoiceSubject = dto.InvoiceSubject,
                InvoiceNumber = invoiceNumber,
                SequenceNumber = ssequNo,
                InvoiceDate = dto.InvoiceDate.Value,
                PartyId = dto.PartyId,
                SettlementTypeId = dto.SettlementTypeId.Value,
                Remarks = dto.Remarks,
                status = 1, // وضعیت اولیه به‌عنوان "جدید"
                CreationTime = DateTime.Now,
                CreatorUserId = dto.CreatorUserId,
            };
            _db.Invoices.Add(invoice);

            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "فاکتور فروش با موفقیت ایجاد شد";
                    result.updateType = 1;

                }
            }
            catch (Exception ex)
            {
                result.Message = "خطایی در زمان ایجاد فاکتور رخ داده است: " + ex.Message;
            }

            return result;
        }
        public async Task<clsResult> UpdateInvoiceHeaderAsync(InvoiceHeaderDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            if (await _db.Invoices.AnyAsync(n => n.Id != dto.Id && n.InvoiceNumber == dto.InvoiceNumber))
            {
                result.Message = "شماره فاکتور تکراری است";
                return result;
            }

            // بررسی دوره مالی
            var period = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == dto.FinancePeriodId.Value);
            if (period == null || period.SellerId != dto.SellerId || dto.InvoiceDate < period.StartDate || dto.InvoiceDate > period.EndDate)
            {
                result.Message = $"تاریخ فاکتور خارج از بازه دوره مالی {period?.Name} است";
                return result;
            }

            var invoice = await _db.Invoices.FindAsync(dto.Id);
            if (invoice == null)
            {
                result.Message = $"اطلاعات فاکتور یافت نشد";
                return result;
            }
            // ایجاد هدر فاکتور
            if (!string.IsNullOrEmpty(dto.strInvoiceDate))
                invoice.InvoiceDate = dto.strInvoiceDate.PersianToLatin();

            invoice.PartyId = dto.PartyId;
            invoice.InvoiceType = dto.InvoiceType;
            invoice.SettlementTypeId = dto.SettlementTypeId.Value;
            invoice.Remarks = dto.Remarks;
            invoice.LastUpdate = DateTime.Now;
            invoice.EditorUserId = dto.EditorUserId;

            _db.Invoices.Update(invoice);

            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "فاکتور فروش با موفقیت ویرایش شد";
                    result.updateType = 1;

                }
            }
            catch (Exception ex)
            {
                result.Message = "خطایی در زمان ایجاد فاکتور رخ داده است: " + ex.Message;
            }

            return result;
        }
        public async Task<InvoiceDto> GetInvoiceByIdAsync(Guid invoiceId)
        {
            // یافتن هدر فاکتور به همراه آیتم‌ها
            var invoice = await _db.Invoices
                .AsNoTracking()
                .Include(i => i.InvoiceItems)
                .Include(i => i.InvoiceParty) // برای دریافت اطلاعات طرف حساب
                .Where(i => i.Id == invoiceId)
                .Select(i => new InvoiceDto
                {
                    InvoiceHeader = new InvoiceHeaderDto
                    {
                        Id = i.Id,
                        SellerId = i.SellerId,
                        FinancePeriodId = i.FinancePeriodId,
                        InvoiceType = i.InvoiceType,
                        InvoiceSubject = i.InvoiceSubject,
                        InvoiceNumber = i.InvoiceNumber,
                        TaxInvoiceNumber = i.TaxInvoiceNumber,
                        InvoiceAutoNumber = i.InvoiceAutoNumber,
                        SequenceNumber = i.SequenceNumber,
                        ArchiveRef = i.ArchiveRef,
                        InvoiceDate = i.InvoiceDate,
                        PartyId = i.PartyId,
                        PartyName = i.InvoiceParty.Name, // نام طرف حساب
                        Remarks = i.Remarks,
                        status = i.status,
                        CreationTime = i.CreationTime,
                        CreatorUserId = i.CreatorUserId,
                        LastUpdate = i.LastUpdate,
                        EditorUserId = i.EditorUserId,
                        SettlementTypeName = i.SettlementTypeId.ToString(), // یا متدی برای ترجمه وضعیت
                        SettlementTypeId = i.SettlementTypeId,

                        TotalPriceBeforDiscount = i.InvoiceItems.Sum(item => item.PriceBeForDescount),
                        TotalDiscount = i.InvoiceItems.Sum(item => item.Discount),
                        TotalPriceAfterDiscount = i.InvoiceItems.Sum(item => item.PriceAfterDiscount),
                        TotalVatPrice = i.InvoiceItems.Sum(item => item.VatPrice),
                        TotalFinalPrice = i.InvoiceItems.Sum(item => item.FinalPrice)
                    },
                    Items = i.InvoiceItems.Select(item => new InvoiceItemDto
                    {
                        Id = item.Id,
                        InvoiceId = item.InvoiceId,
                        ProductId = item.ProductId,
                        ProductName = item.Product.ProductName, // دریافت نام محصول
                        PakageUnitName = item.Product.PakageUnit.UnitName,
                        BaseUnitName = item.Product.BaseUnit.UnitName,
                        QuantityInPakageUnit = item.QuantityInPakageUnit,
                        QuantityInBaseUnit = item.QuantityInBaseUnit,
                        QuantityInPerPakage = item.QuantityInPerPakage,
                        TotalQuantity = item.TotalQuantity,
                        UnitPrice = item.UnitPrice,
                        PriceBeForDescount = item.PriceBeForDescount,
                        Discount = item.Discount,
                        PriceAfterDiscount = item.PriceAfterDiscount,
                        VatRate = item.VatRate,
                        VatPrice = item.VatPrice,
                        FinalPrice = item.FinalPrice,
                        Remarks = item.Remarks,
                        CreationTime = item.CreationTime,
                        CreatorUserId = item.CreatorUserId,
                        LastUpdate = item.LastUpdate,
                        EditorUserId = item.EditorUserId
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return invoice;
        }

        //Item
        public async Task<clsResult> AddInvoiceItemAsync(InvoiceItemDto itemDto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            // بررسی معتبر بودن شناسه فاکتور
            var invoice = await _db.Invoices
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == itemDto.InvoiceId);

            if (invoice == null)
            {
                result.Message = "فاکتور یافت نشد";
                return result;
            }

            // افزودن آیتم به فاکتور
            var invoiceItem = new com_InvoiceItem
            {
                Id = itemDto.Id,
                InvoiceId = itemDto.InvoiceId,
                ProductId = itemDto.ProductId,
                QuantityInPakageUnit = itemDto.QuantityInPakageUnit,
                QuantityInBaseUnit = itemDto.QuantityInBaseUnit,
                QuantityInPerPakage = itemDto.QuantityInPerPakage,
                TotalQuantity = itemDto.TotalQuantity,
                UnitPrice = itemDto.UnitPrice,
                PriceBeForDescount = itemDto.PriceBeForDescount,
                Discount = itemDto.Discount,
                PriceAfterDiscount = itemDto.PriceAfterDiscount,
                VatRate = itemDto.VatRate,
                VatPrice = itemDto.VatPrice,
                FinalPrice = itemDto.FinalPrice,
                Remarks = itemDto.Remarks,
                CreationTime = DateTime.Now,
                CreatorUserId = itemDto.CreatorUserId
            };

            _db.InvoiceItems.Add(invoiceItem);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "آیتم فاکتور با موفقیت افزوده شد";
                    result.updateType = 1;
                }
            }
            catch (Exception ex)
            {
                result.Message = "خطایی در زمان افزودن آیتم به فاکتور رخ داده است: " + ex.Message;
            }

            return result;
        }
        public async Task<InvoiceItemDto> GetInvoiceItemByIdAsync(Guid id)
        {
            InvoiceItemDto dto = new InvoiceItemDto();

            var item = await _db.InvoiceItems
                .Include(n => n.Product)
                .ThenInclude(n => n.BaseUnit)
                .Include(n => n.Product.PakageUnit)
                .SingleOrDefaultAsync(n => n.Id == id);

            dto.Id = item.Id;
            dto.InvoiceId = item.InvoiceId;
            dto.ProductId = item.ProductId;
            dto.ProductName = item.Product.ProductName; // دریافت نام محصول
            dto.PakageUnitName = item.Product.PakageUnit.UnitName;
            dto.BaseUnitName = item.Product.BaseUnit.UnitName;
            dto.QuantityInPakageUnit = item.QuantityInPakageUnit;
            dto.QuantityInBaseUnit = item.QuantityInBaseUnit;
            dto.QuantityInPerPakage = item.QuantityInPerPakage;
            dto.TotalQuantity = item.TotalQuantity;
            dto.UnitPrice = item.UnitPrice;
            dto.PriceBeForDescount = item.PriceBeForDescount;
            dto.Discount = item.Discount;
            dto.PriceAfterDiscount = item.PriceAfterDiscount;
            dto.VatRate = item.VatRate;
            dto.VatPrice = item.VatPrice;
            dto.FinalPrice = item.FinalPrice;
            dto.Remarks = item.Remarks;
            dto.CreationTime = item.CreationTime;
            dto.CreatorUserId = item.CreatorUserId;
            dto.LastUpdate = item.LastUpdate;
            dto.EditorUserId = item.EditorUserId;

            return dto;
        }
        public async Task<clsResult> updateInvoiceItemAsync(InvoiceItemDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            var item = await _db.InvoiceItems.FindAsync(dto.Id);
            if (item == null)
            {
                result.Message = "آیتم موردنظر یافت نشد";
                return result;
            }

            item.InvoiceId = dto.InvoiceId;
            item.ProductId = dto.ProductId;
            item.QuantityInPakageUnit = dto.QuantityInPakageUnit;
            item.QuantityInBaseUnit = dto.QuantityInBaseUnit;
            item.QuantityInPerPakage = dto.QuantityInPerPakage;
            item.TotalQuantity = dto.TotalQuantity;
            item.UnitPrice = dto.UnitPrice;
            item.PriceBeForDescount = dto.PriceBeForDescount;
            item.Discount = dto.Discount;
            item.PriceAfterDiscount = dto.PriceAfterDiscount;
            item.VatRate = dto.VatRate;
            item.VatPrice = dto.VatPrice;
            item.FinalPrice = dto.FinalPrice;
            item.Remarks = dto.Remarks;
            item.LastUpdate = dto.LastUpdate;
            item.EditorUserId = dto.EditorUserId;
            _db.InvoiceItems.Update(item);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.updateType = 1;
                result.Message = "بروزسانی اطلاعات با موفقیت انجام شد";

            }
            catch (Exception x)
            {
                result.Message = "در ذخیره اطلاعات خطایی رخ داده است \n" + x.Message;
            }

            return result;
        }
        public async Task<clsResult> DeleteInvoiceItemAsync(Guid Id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            var item = await _db.InvoiceItems.FindAsync(Id);
            if (item == null)
            {
                result.Message = "آیتم موردنظر یافت نشد";
                return result;
            }

            _db.InvoiceItems.Remove(item);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.updateType = 1;
                result.Message = "حذف اطلاعات با موفقیت انجام شد";

            }
            catch (Exception x)
            {
                result.Message = "در حذف اطلاعات خطایی رخ داده است \n" + x.Message;
            }

            return result;
        }
        public async Task<clsResult> DeleteInvoiceAsync(Guid Id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            var item = await _db.Invoices.FindAsync(Id);
            if (item == null)
            {
                result.Message = "آیتم موردنظر یافت نشد";
                return result;
            }

            var items = await _db.InvoiceItems.Where(n => n.InvoiceId == Id).ToListAsync();
            _db.InvoiceItems.RemoveRange(items);
            _db.Invoices.Remove(item);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.updateType = 1;
                result.Message = "حذف اطلاعات با موفقیت انجام شد";

            }
            catch (Exception x)
            {
                result.Message = "در حذف اطلاعات خطایی رخ داده است \n" + x.Message;
            }

            return result;
        }
        //Create Invoice In Bulk
        public async Task<List<CreateIncoiceDto>> PrepareInvoiceToCreate_AtiranAsync(InvoiceImportDto_Atiran rawData, short Invoicetype)
        {
            List<CreateIncoiceDto> invoices = new List<CreateIncoiceDto>();

            // Check if SellerId exists
            if (!_userContext.SellerId.HasValue) return invoices;

            long sellerId = _userContext.SellerId.Value;
            int? periodId = _userContext.PeriodId;

            var GroupdedByNumber = rawData.Items.GroupBy(n => n.InvoiceNumer)
                .Select(n => new
                {
                    InvoiceNumber = n.Key,
                    personName = n.Max(a => a.personName),
                    personCode = n.Max(a => a.personCode),
                    personNational = n.Max(a => a.personNationalId),
                    personEccoCode = n.Max(a => a.personEcconomicCode),
                    invcDate = n.Max(a => a.InvoiceDate),
                }).ToList();

            foreach (var x in GroupdedByNumber)
            {
                try
                {
                    bool allowToAdd = true;
                    CreateIncoiceDto invoice = new CreateIncoiceDto();
                    InvoiceHeaderDto header = new InvoiceHeaderDto();
                    List<InvoiceItemDto> itemsdto = new List<InvoiceItemDto>();

                    var items = rawData.Items.Where(n => n.InvoiceNumer == x.InvoiceNumber).ToList();

                    // Setup header
                    header.Id = Guid.NewGuid();
                    header.SellerId = sellerId;
                    header.InvoiceSubject = 1;
                    header.FinancePeriodId = periodId;
                    header.InvoiceType = Invoicetype;
                    header.InvoiceDate = x.invcDate;
                    header.ArchiveRef = x.InvoiceNumber;
                    header.InvoiceNumber = x.InvoiceNumber;
                    header.status = 0;
                    header.SettlementTypeId = 1;
                    header.PartyId = await _personService.GetOrCreatePersonIdAsync(x.personName, x.personCode, x.personNational, x.personEccoCode);
                    header.CreationTime = DateTime.Now;
                    header.CreatorUserId = "system";

                    // Add Items
                    foreach (var i in items)
                    {
                        var product = await _productService.GetOrCreateProductByNameAsync(i.ProductName, i.ProductCode, i.QtyBaseUnitInPakage, sellerId);

                        if (product == null)
                        {
                            allowToAdd = false;
                            break;
                        }

                        InvoiceItemDto itemdto = new InvoiceItemDto();
                        itemdto.Id = Guid.NewGuid();
                        itemdto.InvoiceId = header.Id;
                        itemdto.ProductId = product.Id;
                        itemdto.QuantityInPakageUnit = i.QtyPakage;
                        itemdto.QuantityInBaseUnit = i.QtyBase;
                        itemdto.QuantityInPerPakage = i.QtyBaseUnitInPakage;
                        itemdto.TotalQuantity = (i.QtyBaseUnitInPakage * i.QtyPakage) + i.QtyBase;
                        itemdto.UnitPrice = i.Fee;
                        itemdto.PriceBeForDescount = i.PriceBeforeDiscount;
                        itemdto.Discount = i.Discount;
                        itemdto.PriceAfterDiscount = i.PriceAfterDiscount;
                        itemdto.VatRate = i.VatRate;
                        itemdto.VatPrice = i.VatPrice;
                        itemdto.FinalPrice = i.FinalPrice;
                        itemdto.CreationTime = DateTime.Now;
                        itemdto.CreatorUserId = "system";

                        itemsdto.Add(itemdto);
                    }

                    if (!allowToAdd)
                    {
                        continue;
                    }

                    invoice.Header = header;
                    invoice.Item.AddRange(itemsdto);
                    invoices.Add(invoice);

                }
                catch
                {
                    continue;
                }
            }
            return invoices;
        }
        public async Task<clsResult> CreateInvoiceInBulkAsync(List<CreateIncoiceDto> invoices)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            List<com_Invoice> headers = new List<com_Invoice>();
            List<com_InvoiceItem> items = new List<com_InvoiceItem>();

            foreach (var i in invoices)
            {

                if (await _db.Invoices.AnyAsync(n => n.InvoiceType == i.Header.InvoiceType && n.SellerId == i.Header.SellerId.Value && n.InvoiceNumber == i.Header.InvoiceNumber && n.FinancePeriodId == i.Header.FinancePeriodId))
                    continue;

                var invoice = new com_Invoice();
                invoice.Id = i.Header.Id;
                invoice.SellerId = i.Header.SellerId.Value;
                invoice.FinancePeriodId = i.Header.FinancePeriodId;
                invoice.InvoiceType = i.Header.InvoiceType;
                invoice.InvoiceSubject = i.Header.InvoiceSubject;
                invoice.InvoiceNumber = i.Header.InvoiceNumber;
                invoice.SequenceNumber = 0;
                invoice.InvoiceAutoNumber = 0;
                invoice.InvoiceDate = i.Header.InvoiceDate.Value;
                invoice.PartyId = i.Header.PartyId;
                invoice.SettlementTypeId = i.Header.SettlementTypeId.Value;
                invoice.Remarks = i.Header.Remarks;
                invoice.status = 1;
                invoice.CreationTime = DateTime.Now;
                invoice.CreatorUserId = i.Header.CreatorUserId;

                headers.Add(invoice);

                foreach (var itemDto in i.Item)
                {
                    var invoiceItem = new com_InvoiceItem
                    {
                        Id = itemDto.Id,
                        InvoiceId = itemDto.InvoiceId,
                        ProductId = itemDto.ProductId,
                        QuantityInPakageUnit = itemDto.QuantityInPakageUnit,
                        QuantityInBaseUnit = itemDto.QuantityInBaseUnit,
                        QuantityInPerPakage = itemDto.QuantityInPerPakage,
                        TotalQuantity = itemDto.TotalQuantity,
                        UnitPrice = itemDto.UnitPrice,
                        PriceBeForDescount = itemDto.PriceBeForDescount,
                        Discount = itemDto.Discount,
                        PriceAfterDiscount = itemDto.PriceAfterDiscount,
                        VatRate = itemDto.VatRate,
                        VatPrice = itemDto.VatPrice,
                        FinalPrice = itemDto.FinalPrice,
                        Remarks = itemDto.Remarks,
                        CreationTime = DateTime.Now,
                        CreatorUserId = itemDto.CreatorUserId
                    };
                    items.Add(invoiceItem);
                }
            }
            await _db.Invoices.AddRangeAsync(headers);
            await _db.InvoiceItems.AddRangeAsync(items);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.updateType = 1;
                result.Message = "عملیات ورود اطلاعات گروهی فاکتورها با موفقیت انجام شد";

            }
            catch (Exception x)
            {
                result.Message = "در ایجاد 'گروهی صورتحساب خطایی رخ داده است \n" + x.Message;
            }

            return result;

        }
        public async Task<List<InvoiceDto>> GetInvoicesFuulDataAsync(Guid[] ids)
        {
            List<InvoiceDto> invoices = new List<InvoiceDto>();

            for (int i = 0; i < ids.Length; i++)
            {
                InvoiceDto invoice = await GetInvoiceByIdAsync(ids[i]);
                invoice.sellerInfo = await _personService.GetPersonDtoAsync(invoice.InvoiceHeader.SellerId.Value);
                invoice.buyerInfo = await _personService.GetPersonDtoAsync(invoice.InvoiceHeader.PartyId);

                invoices.Add(invoice);
            }

            return invoices;
        }
        public async Task<clsResult> TagInvoicesAsync(Guid[] ids)
        {
            clsResult result = new clsResult
            {
                Success = false,
                ShowMessage = true
            };

            // Check if the ids array is empty
            if (!ids.Any())
            {
                return result;
            }

            try
            {
                // Update the invoices
                var invoice = await _db.Invoices
                    .Where(n => ids.Contains(n.Id))
                    .ExecuteUpdateAsync(n => n.SetProperty(x => x.flag, true));

                result.Success = true;
                result.Message = "فاکتورهای انتخاب شده، نشان شدند.";
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the update
                result.Message = $"خطا در نشان کردن فاکتورها: {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> UnTagInvoicesAsync(Guid[] ids)
        {
            clsResult result = new clsResult
            {
                Success = false,
                ShowMessage = true
            };

            // Check if the ids array is empty
            if (!ids.Any())
            {
                return result;
            }

            try
            {
                // Update the invoices
                var invoice = await _db.Invoices
                    .Where(n => ids.Contains(n.Id))
                    .ExecuteUpdateAsync(n => n.SetProperty(x => x.flag, false));

                result.Success = true;
                result.Message = "فاکتورهای انتخاب شده، نشان شدند.";
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the update
                result.Message = $"خطا در نشان کردن فاکتورها: {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> TagTogglerInvoicesAsync(Guid id)
        {
            clsResult result = new clsResult
            {
                Success = false,
                ShowMessage = true
            };

            try
            {
                var invoice = await _db.Invoices
                    .FindAsync(id);

                invoice.flag = !invoice.flag;
                _db.Invoices.Update(invoice);
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "فاکتور موردنظر حذف نشان شد";
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the update
                result.Message = $"خطا در نشان کردن فاکتورها: {ex.Message}";
            }

            return result;
        }

        // Copy Invoices
        public async Task<clsResult> CopyInvoicesAsync(CoppyInvoiceSettingDto filter)
        {
            clsResult result = new clsResult();
            result.Message = "فاکتوری برای کپی دریافت نشد";

            if (filter.InvoicesId.Count == 0)
                return result;

            List<com_Invoice> newInvoices = new List<com_Invoice>();
            List<com_InvoiceItem> items = new List<com_InvoiceItem>();

            var period = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == filter.PeriodId);
            if (period == null)
            {
                result.Message = "دوره مالی فعال شناسایی نشد.";
                return result;
            }
            DateTime maxDate = period.EndDate;
            DateTime minDate = period.StartDate;

            foreach (var id in filter.InvoicesId)
            {
                var query = _db.InvoiceItems.Include(n => n.Invoice)
                    .Where(n => n.InvoiceId == id).AsQueryable();
                if (filter.TaxableType == 2)
                    query = query.Where(n => n.VatPrice > 0);

                if (filter.TaxableType == 3)
                    query = query.Where(n => n.VatPrice == 0);

                var invoiceItems = await query.ToListAsync();

                if (invoiceItems == null || invoiceItems.Count == 0)
                    continue;

                com_Invoice newInvc = new com_Invoice();
                var oldInvoice = invoiceItems?.FirstOrDefault()?.Invoice;
                newInvc = oldInvoice;
                newInvc.Id = Guid.NewGuid();

                //Set Date
                if (filter.GenerateDataType == 2)
                {
                    DateTime invcDate = oldInvoice.InvoiceDate.AddMonths(1);
                    if (invcDate > maxDate)
                        invcDate = maxDate;

                    newInvc.InvoiceDate = invcDate;
                }
                else if (filter.GenerateDataType == 3)
                {
                    DateTime invcDate = oldInvoice.InvoiceDate.AddMonths(-1);
                    if (invcDate < minDate)
                        invcDate = minDate;

                    newInvc.InvoiceDate = invcDate;
                }
                else if (filter.GenerateDataType == 4)
                {
                    int dayCount = filter.VarDay;
                    DateTime invcDate = oldInvoice.InvoiceDate.AddDays(dayCount);
                    if (invcDate < minDate)
                        invcDate = minDate;
                    else if (invcDate > maxDate)
                        invcDate = maxDate;

                    newInvc.InvoiceDate = invcDate;
                }
                else if (filter.GenerateDataType == 5)
                {
                    if (string.IsNullOrEmpty(filter.strInvoiceDate))
                    {
                        result.Message = "تاریخ فاکتور را مشخص نکرده اید";
                        result.Success = false;
                        result.ShowMessage = true;
                        return result;
                    }

                    newInvc.InvoiceDate = filter.strInvoiceDate.PersianToLatin();
                }

                newInvc.InvoiceNumber = "66" + oldInvoice.InvoiceNumber;
                newInvc.CreationTime = DateTime.Now;
                newInvc.CreatorUserId = "system";
                newInvoices.Add(newInvc);

                foreach (var item in invoiceItems)
                {
                    com_InvoiceItem newItem = new com_InvoiceItem();
                    newItem = item;
                    newItem.Id = Guid.NewGuid();
                    newItem.InvoiceId = newInvc.Id;
                    items.Add(newItem);
                }
            }

            if (newInvoices.Count > 0)
            {
                await _db.Invoices.AddRangeAsync(newInvoices);
                await _db.InvoiceItems.AddRangeAsync(items);
                try
                {
                    await _db.SaveChangesAsync();
                    result.Success = true;
                    result.Message = "عملیات کپی از فاکتور بدرستی انجام شد";
                }
                catch (Exception)
                {
                    result.Message = "در عملیات کپی فاکتورهای خطایی رخ داده است";
                }
            }
            return result;
        }

        //Invoice Reports
        public async Task<List<InvoiceHeaderDto>> GetInvoicesGroupedByCustomer(InvoiceFilterDto filter)
        {
            var query = await GetInvoices(filter).ToListAsync();
            var grouped = query.GroupBy(n => n.PartyId).Select(n => new InvoiceHeaderDto
            {
                PartyId = n.Key,
                PartyName = n.Max(a => a.PartyName),
                FinancePeriodId = n.Max(z => z.FinancePeriodId),
                SellerId = n.Max(h => h.SellerId),
                TotalPriceBeforDiscount = n.Sum(a => a.TotalPriceBeforDiscount),
                TotalDiscount = n.Sum(a => a.TotalDiscount),
                TotalPriceAfterDiscount = n.Sum(a => a.TotalPriceAfterDiscount),
                TotalNoTaxable = n.Sum(a => a.TotalNoTaxable),
                TotalTaxable = n.Sum(a => a.TotalTaxable),
                TotalVatPrice = n.Sum(a => a.TotalVatPrice),
                TotalFinalPrice = n.Sum(a => a.TotalFinalPrice),
                InviceQty = n.Count(),
            }).ToList();

            return grouped;
        }

        public async Task<SaleTotalReportDto> GetTotalReportAsync(SaleTotalReportDto dto)
        {

            var filter = dto.filter;

            var query = _db.InvoiceItems
                .AsNoTracking()
                .Include(i => i.Invoice)
                .Where(x =>
                      x.Invoice.InvoiceType == filter.Invoicetype
                   && x.Invoice.SellerId == filter.SellerId)
                .AsQueryable();

            // فیلترها
            if (filter.PeriodId.HasValue)
            {
                query = query.Where(i => i.Invoice.FinancePeriodId == filter.PeriodId.Value);
            }
            if (!string.IsNullOrEmpty(filter.InvoiceNumber))
            {
                query = query.Where(i => i.Invoice.InvoiceNumber.Contains(filter.InvoiceNumber));
            }

            if (!string.IsNullOrEmpty(filter.srtFromDate))
            {
                DateTime startDate = filter.srtFromDate.PersianToLatin();
                query = query.Where(i => i.Invoice.InvoiceDate >= startDate);
            }

            if (!string.IsNullOrEmpty(filter.srtToDate))
            {
                DateTime endDate = filter.srtToDate.PersianToLatin();
                query = query.Where(i => i.Invoice.InvoiceDate <= endDate);
            }

            if (filter.Party.HasValue)
            {
                query = query.Where(i => i.Invoice.PartyId == filter.Party.Value);
            }

            if (!string.IsNullOrEmpty(filter.Remark))
            {
                query = query.Where(i => i.Invoice.Remarks.Contains(filter.Remark));
            }

            if (!string.IsNullOrEmpty(filter.Status))
            {
                query = query.Where(i => i.Invoice.status.ToString() == filter.Status);
            }
            if (filter.Taged)
                query = query.Where(n => n.Invoice.flag == true);

            dto.InvoiceQty = await query.GroupBy(n => n.Invoice.InvoiceNumber).CountAsync();
            dto.PriceBeforDiscount = (long)await query.SumAsync(n => n.PriceBeForDescount);
            dto.Discount = (long)await query.SumAsync(n => n.Discount);
            dto.PriceAfterDiscount = (long)await query.SumAsync(n => n.PriceAfterDiscount);
            dto.Taxable = (long)await query.Where(n => n.VatPrice > 0).SumAsync(n => n.PriceAfterDiscount);
            dto.NoTaxable = (long)await query.Where(n => n.VatPrice <= 0).SumAsync(n => n.PriceAfterDiscount);
            dto.VarPrice = (long)await query.SumAsync(n => n.VatPrice);
            dto.TotalFinalPrice = (long)await query.SumAsync(n => n.FinalPrice);


            return dto;
        }

        //===================================== For Doc Creator
        public IQueryable<InvoiceHeaderDto> GetInvoicesForCreateDoc(DocCreatorFilterDto filter)
        {
            var query = _db.Invoices
                .AsNoTracking()
                .Include(i => i.InvoiceItems)
                .Include(i => i.InvoiceParty)
                .Where(x =>
                      x.InvoiceType == filter.DocType.Value
                   && x.SellerId == filter.SellerId
                   && x.DocId == null)
                .AsQueryable();

            // فیلترها
            if (filter.PeriodId.HasValue)
            {
                query = query.Where(i => i.FinancePeriodId == filter.PeriodId.Value);
            }

            if (!string.IsNullOrEmpty(filter.srtFromDate))
            {
                DateTime startDate = filter.srtFromDate.PersianToLatin();
                query = query.Where(i => i.InvoiceDate >= startDate);
            }

            if (!string.IsNullOrEmpty(filter.srtToDate))
            {
                DateTime endDate = filter.srtToDate.PersianToLatin();
                query = query.Where(i => i.InvoiceDate <= endDate);
            }

            if (filter.Taged)
                query = query.Where(n => n.flag == true);

            // ایجاد DTO با اطلاعات کامل
            var invoices = query.Select(i => new InvoiceHeaderDto
            {
                Id = i.Id,
                SellerId = i.SellerId,
                FinancePeriodId = i.FinancePeriodId,
                InvoiceType = i.InvoiceType,
                InvoiceSubject = i.InvoiceSubject,
                InvoiceNumber = i.InvoiceNumber,
                TaxInvoiceNumber = i.TaxInvoiceNumber,
                InvoiceAutoNumber = i.InvoiceAutoNumber,
                SequenceNumber = i.SequenceNumber,
                ArchiveRef = i.ArchiveRef,
                InvoiceDate = i.InvoiceDate,
                PartyId = i.PartyId,
                PartyTafsilId = i.InvoiceParty.TafsilId,
                PartyName = i.InvoiceParty.Name, // نام طرف حساب
                Remarks = i.Remarks,
                status = i.status,
                CreationTime = i.CreationTime,
                CreatorUserId = i.CreatorUserId,
                LastUpdate = i.LastUpdate,
                EditorUserId = i.EditorUserId,
                SettlementTypeName = i.SettlementTypeId.com_ToSettelmentTypeName(),
                SettlementTypeId = i.SettlementTypeId,
                taged = i.flag,
                // محاسبه جمع مبالغ فاکتور
                TotalNoTaxable = i.InvoiceItems.Where(x => x.VatRate == 0).Sum(item => item.PriceAfterDiscount),
                TotalTaxable = i.InvoiceItems.Where(x => x.VatRate > 0).Sum(item => item.PriceAfterDiscount),
                TotalPriceBeforDiscount = i.InvoiceItems.Sum(item => item.PriceBeForDescount),
                TotalDiscount = i.InvoiceItems.Sum(item => item.Discount),
                TotalPriceAfterDiscount = i.InvoiceItems.Sum(item => item.PriceAfterDiscount),
                TotalVatPrice = i.InvoiceItems.Sum(item => item.VatPrice),
                TotalFinalPrice = i.InvoiceItems.Sum(item => item.FinalPrice)
            })
            .OrderBy(i => i.InvoiceDate).ThenBy(n => n.InvoiceNumber)
            .AsQueryable();

            return invoices;
        }
        public async Task<List<InvoiceHeaderDto>> GetInvoicesToDtoByIdAsync(List<Guid> Ids)
        {
            var query = _db.Invoices
                .AsNoTracking()
                .Include(i => i.InvoiceItems)
                .Include(i => i.InvoiceParty) // برای دریافت نام طرف حساب
                .Where(x => Ids.Contains(x.Id)).AsQueryable();

            // ایجاد DTO با اطلاعات کامل
            var invoices = await query.Select(i => new InvoiceHeaderDto
            {
                Id = i.Id,
                SellerId = i.SellerId,
                FinancePeriodId = i.FinancePeriodId,
                InvoiceType = i.InvoiceType,
                InvoiceSubject = i.InvoiceSubject,
                InvoiceNumber = i.InvoiceNumber,
                TaxInvoiceNumber = i.TaxInvoiceNumber,
                InvoiceAutoNumber = i.InvoiceAutoNumber,
                SequenceNumber = i.SequenceNumber,
                ArchiveRef = i.ArchiveRef,
                InvoiceDate = i.InvoiceDate,
                PartyId = i.PartyId,
                PartyTafsilId = i.InvoiceParty.TafsilId,
                PartyName = i.InvoiceParty.Name, // نام طرف حساب
                Remarks = i.Remarks,
                status = i.status,
                CreationTime = i.CreationTime,
                CreatorUserId = i.CreatorUserId,
                LastUpdate = i.LastUpdate,
                EditorUserId = i.EditorUserId,
                SettlementTypeName = i.SettlementTypeId.com_ToSettelmentTypeName(),
                SettlementTypeId = i.SettlementTypeId,
                taged = i.flag,
                // محاسبه جمع مبالغ فاکتور
                TotalNoTaxable = i.InvoiceItems.Where(x => x.VatRate == 0).Sum(item => item.PriceAfterDiscount),
                TotalTaxable = i.InvoiceItems.Where(x => x.VatRate > 0).Sum(item => item.PriceAfterDiscount),
                TotalPriceBeforDiscount = i.InvoiceItems.Sum(item => item.PriceBeForDescount),
                TotalDiscount = i.InvoiceItems.Sum(item => item.Discount),
                TotalPriceAfterDiscount = i.InvoiceItems.Sum(item => item.PriceAfterDiscount),
                TotalVatPrice = i.InvoiceItems.Sum(item => item.VatPrice),
                TotalFinalPrice = i.InvoiceItems.Sum(item => item.FinalPrice)
            })
            .OrderBy(i => i.InvoiceDate).ThenBy(n => n.InvoiceNumber)
            .ToListAsync();

            return invoices;
        }

        public async Task<List<InvoiceDetailsDto>> SaleReportByStufAsync(InvoiceFilterDto filter)
        {
            if (filter.Party == 0)
                filter.Party = null;

            var query = _db.InvoiceItems
                .Include(n => n.Invoice)
                .ThenInclude(n => n.InvoiceParty)
                .Include(n => n.Product)
                .ThenInclude(n => n.ProductUnits)
                .Where(n =>
                n.Invoice.SellerId == filter.SellerId
                ).AsQueryable();

            if (filter.Party != null)
                query = query.Where(n => n.Invoice.PartyId == filter.Party);
            if (!string.IsNullOrEmpty(filter.srtFromDate))
            {
                DateTime fromDate = filter.srtFromDate.PersianToLatin();
                query = query.Where(n => n.Invoice.InvoiceDate >= fromDate);
            }
            if (!string.IsNullOrEmpty(filter.srtToDate))
            {
                DateTime toDate = filter.srtToDate.PersianToLatin();
                query = query.Where(n => n.Invoice.InvoiceDate <= toDate);
            }

            if (filter.PeriodId.HasValue)
                query = query.Where(n => n.Invoice.FinancePeriodId == filter.PeriodId);
            if (filter.Taged)
                query = query.Where(n => n.Invoice.flag == true);


            var report = await query.GroupBy(x => x.ProductId).Select(x => new InvoiceDetailsDto
            {
                ProductOrServiceId = x.Key,
                ProductOrServiceName = x.Max(n => n.Product.ProductName),
                unitId = x.Max(n => n.Product.BaseUnitId),
                UnitCountName = x.Max(n => n.Product.BaseUnit.UnitName),
                Amount = x.Sum(n => n.TotalQuantity),
                Price = x.Sum(n => n.PriceBeForDescount),
                DiscountPrice = x.Sum(n => n.Discount),
                TotalPriceAfterDiscount = x.Sum(n => n.PriceAfterDiscount),
                VatRate = x.Max(n => n.VatRate),
                VatPrice = x.Sum(n => n.VatPrice),
                FinalPrice = x.Sum(n => n.FinalPrice),

            }).ToListAsync();

            return report;

        }

        public async Task<List<InvoiceDetailsDto>> GetSalesDetailsAsync(InvoiceFilterDto filter)
        {
            if (filter.Party == 0)
                filter.Party = null;

            var query = _db.InvoiceItems
                .Include(n => n.Invoice)
                .ThenInclude(n => n.InvoiceParty)
                .Include(n => n.Product)
                .ThenInclude(n => n.ProductUnits)
                .Where(n =>
                n.Invoice.SellerId == filter.SellerId
                && n.Invoice.InvoiceType == filter.Invoicetype
                ).AsQueryable();

            if (filter.Party != null)
                query = query.Where(n => n.Invoice.PartyId == filter.Party);
            if (!string.IsNullOrEmpty(filter.srtFromDate))
            {
                DateTime fromDate = filter.srtFromDate.PersianToLatin();
                query = query.Where(n => n.Invoice.InvoiceDate >= fromDate);
            }
            if (!string.IsNullOrEmpty(filter.srtToDate))
            {
                DateTime toDate = filter.srtToDate.PersianToLatin();
                query = query.Where(n => n.Invoice.InvoiceDate <= toDate);
            }

            if (filter.PeriodId.HasValue)
                query = query.Where(n => n.Invoice.FinancePeriodId == filter.PeriodId);
            if (filter.Taged)
                query = query.Where(n => n.Invoice.flag == true);


            var report = await query.Select(x => new InvoiceDetailsDto
            {
                InvoiceNumber = x.Invoice.InvoiceNumber,
                InvoicePersianDate = x.Invoice.InvoiceDate.LatinToPersian(),
                ProductOrServiceId = x.ProductId,
                ProductOrServiceName = x.Product.ProductName,
                unitId = x.Product.BaseUnitId,
                UnitCountName = x.Product.BaseUnit.UnitName,
                Amount = x.TotalQuantity,
                UnitPrice = x.UnitPrice,
                Price = x.PriceBeForDescount,
                DiscountPrice = x.Discount,
                TotalPriceAfterDiscount = x.PriceAfterDiscount,
                VatRate = x.VatRate,
                VatPrice = x.VatPrice,
                FinalPrice = x.FinalPrice,
                Buyer = x.Invoice.InvoiceParty.Name,
                stuffUID = x.Product.UniqueId,

            }).ToListAsync();

            return report;

        }
    }
}