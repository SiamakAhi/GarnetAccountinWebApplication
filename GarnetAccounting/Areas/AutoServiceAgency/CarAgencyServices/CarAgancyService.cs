using ClosedXML.Excel;
using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.AutoServiceAgency.CarAgencyInterfaces;
using GarnetAccounting.Areas.AutoServiceAgency.Dto;
using GarnetAccounting.Areas.AutoServiceAgency.Models.Enities;
using GarnetAccounting.Areas.AutoServiceAgency.Models.Entities;
using GarnetAccounting.Models;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.AutoServiceAgency.CarAgencyServices
{
    public class CarAgancyService : ICarAgancyInterface
    {
        private readonly AppDbContext _db;
        private readonly UserContextService _userContext;
        private readonly IAccDocCreatorService _docCreator;
        public CarAgancyService(AppDbContext db, UserContextService userContextService, IAccDocCreatorService docCreatorService)
        {
            _db = db;
            _userContext = userContextService;
            _docCreator = docCreatorService;
        }

        // Contractors
        public async Task<SelectList> SelectList_ContractorsAsync(long SellerId)
        {
            var contractors = await _db.Asa_Contractors.Where(n => n.SellerId == SellerId && n.IsActive)
                .Select(n => new { Id = n.Id, Name = n.Name }).OrderBy(n => n.Name).ToListAsync();

            return new SelectList(contractors, "Id", "Name");
        }

        public async Task<List<ContractorDto>> GetContractorsAsync(long SellerId)
        {
            var contractors = await _db.Asa_Contractors.AsNoTracking()
                .Include(n => n.ContractorTafsil).Where(n => n.SellerId == SellerId)
                .Select(n => new ContractorDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    Name = n.Name,
                    SharePercentage = n.SharePercentage,
                    SkillArea = n.SkillArea,
                    IsActive = n.IsActive,
                    TafsilId = n.TafsilId,
                    TafsilName = n.ContractorTafsil.Name ?? "",
                    CreatedDate = DateTime.Now,
                    CreatedBy = n.CreatedBy,
                }).OrderBy(n => n.Name).ToListAsync();
            return contractors;
        }
        public async Task<ContractorDto> GetContractorByIdAsync(int id)
        {
            var contractor = await _db.Asa_Contractors.AsNoTracking()
                .Include(n => n.ContractorTafsil)
                .Where(n => n.Id == id)
                .Select(n => new ContractorDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    Name = n.Name,
                    SharePercentage = n.SharePercentage,
                    SkillArea = n.SkillArea,
                    IsActive = n.IsActive,
                    TafsilId = n.TafsilId,
                    TafsilName = n.ContractorTafsil.Name ?? "",
                    CreatedDate = n.CreatedDate,
                    CreatedBy = n.CreatedBy
                }).FirstOrDefaultAsync();

            return contractor;
        }
        public async Task<clsResult> AddContractorAsync(ContractorDto dto)
        {
            clsResult result = new clsResult();

            bool IsDuplicate = await _db.Asa_Contractors.Where(n => n.SellerId == dto.SellerId && n.Name == dto.Name).AnyAsync();
            if (IsDuplicate)
            {
                result.Message = $"پیمانکار با نام {dto.Name} قبلا ثبت شده است ";
                return result;
            }
            asa_Contractor newContractor = new asa_Contractor();
            newContractor.SellerId = dto.SellerId.Value;
            newContractor.Name = dto.Name;
            newContractor.SkillArea = dto.SkillArea;
            newContractor.IsActive = dto.IsActive;
            newContractor.SharePercentage = dto.SharePercentage;
            newContractor.TafsilId = dto.TafsilId;
            newContractor.CreatedBy = dto.CreatedBy;
            newContractor.CreatedDate = DateTime.Now;

            _db.Asa_Contractors.Add(newContractor);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات پیمانکار جدید با موفقیت ثبت شد";

            }
            catch
            {
                result.Message = "در عملیات ثبت اطلاعات پیمانکار جدید خطایی رخ داده است";
                result.Success = false;
            }

            return result;
        }

        public async Task<clsResult> UpdateContractorAsync(ContractorDto dto)
        {
            clsResult result = new clsResult();

            bool IsDuplicate = await _db.Asa_Contractors
                .Where(n => n.SellerId == dto.SellerId && n.Name == dto.Name && n.Id != dto.Id)
                .AnyAsync();

            if (IsDuplicate)
            {
                result.Message = $"پیمانکار با نام {dto.Name} قبلا ثبت شده است ";
                return result;
            }
            asa_Contractor theContractor = await _db.Asa_Contractors.FindAsync(dto.Id);
            if (theContractor == null)
            {
                result.Message = $"اطلاعات پیمانکار با نام {dto.Name} در بانک اطلاعاتی یافت نشد ";
                return result;
            }
            theContractor.SellerId = dto.SellerId.Value;
            theContractor.Name = dto.Name;
            theContractor.SkillArea = dto.SkillArea;
            theContractor.IsActive = dto.IsActive;
            theContractor.SharePercentage = dto.SharePercentage;
            theContractor.TafsilId = dto.TafsilId;

            _db.Asa_Contractors.Update(theContractor);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "بروزرسانی اطلاعات پیمانکار با موفقیت انجام شد";

            }
            catch
            {
                result.Message = "در عملیات بروزرسانی اطلاعات پیمانکار خطایی رخ داده است";
                result.Success = false;
            }

            return result;
        }


        // Service
        public async Task<SelectList> SelectList_ServicesAsync(long SellerId)
        {
            var services = await _db.Asa_Services
                .Where(n => n.SellerId == SellerId && n.IsActive)
                .Select(n => new { Id = n.Id, Name = n.ServiceName })
                .OrderBy(n => n.Name)
                .ToListAsync();

            return new SelectList(services, "Id", "Name");
        }
        public async Task<List<ServiceDto>> GetServicesAsync(long SellerId)
        {
            var services = await _db.Asa_Services.AsNoTracking()
                .Where(n => n.SellerId == SellerId)
                .Select(n => new ServiceDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    ServiceName = n.ServiceName,
                    MoeinId = n.MoeinId,
                    TafsilId = n.TafsilId,
                    IsActive = n.IsActive
                }).OrderBy(n => n.ServiceName).ToListAsync();

            return services;
        }
        public async Task<ServiceDto> GetServiceByIdAsync(int id)
        {
            var service = await _db.Asa_Services
                .Where(s => s.Id == id)
                .Select(s => new ServiceDto
                {
                    Id = s.Id,
                    SellerId = s.SellerId,
                    ServiceName = s.ServiceName,
                    MoeinId = s.MoeinId,
                    TafsilId = s.TafsilId,
                    IsActive = s.IsActive
                })
                .FirstOrDefaultAsync();

            return service;
        }
        public async Task<clsResult> AddServiceAsync(ServiceDto dto)
        {
            clsResult result = new clsResult();

            bool isDuplicate = await _db.Asa_Services
                .AnyAsync(n => n.SellerId == dto.SellerId && n.ServiceName == dto.ServiceName);

            if (isDuplicate)
            {
                result.Message = $"سرویس با نام {dto.ServiceName} قبلاً ثبت شده است.";
                return result;
            }

            asa_Services newService = new asa_Services
            {
                SellerId = dto.SellerId,
                ServiceName = dto.ServiceName,
                MoeinId = dto.MoeinId,
                TafsilId = dto.TafsilId,
                IsActive = dto.IsActive
            };

            _db.Asa_Services.Add(newService);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات سرویس جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = "در عملیات ثبت اطلاعات سرویس جدید خطایی رخ داده است.";
                result.Success = false;
                // Log Exception: ex.ToString()
            }

            return result;
        }
        public async Task<clsResult> UpdateServiceAsync(ServiceDto dto)
        {
            clsResult result = new clsResult();

            var existingService = await _db.Asa_Services
                .FirstOrDefaultAsync(n => n.Id == dto.Id && n.SellerId == dto.SellerId);

            if (existingService == null)
            {
                result.Message = "سرویس مورد نظر یافت نشد.";
                return result;
            }

            bool isDuplicate = await _db.Asa_Services
                .AnyAsync(n => n.SellerId == dto.SellerId && n.ServiceName == dto.ServiceName && n.Id != dto.Id);

            if (isDuplicate)
            {
                result.Message = $"سرویس با نام {dto.ServiceName} قبلاً ثبت شده است.";
                return result;
            }

            existingService.ServiceName = dto.ServiceName;
            existingService.MoeinId = dto.MoeinId;
            existingService.TafsilId = dto.TafsilId;
            existingService.IsActive = dto.IsActive;

            _db.Asa_Services.Update(existingService);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات سرویس با موفقیت بروزرسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = "در عملیات بروزرسانی اطلاعات سرویس خطایی رخ داده است.";
                result.Success = false;
                // Log Exception: ex.ToString()
            }

            return result;
        }

        // Setting
        public async Task<ReceptionAccountingSettingDto> GetOrCreateSettingsAsync(long sellerId)
        {
            var setting = await _db.Asa_Settings
                .Where(s => s.SellerId == sellerId)
                .Select(s => new ReceptionAccountingSettingDto
                {
                    Id = s.Id,
                    SellerId = s.SellerId,
                    SaleMoeinId = s.SaleMoeinId,
                    SaleFreeTafsilId = s.SaleFreeTafsilId,
                    SaleWarrantyTfsilId = s.SaleWarrantyTfsilId,
                    NonCommercialCreditorMoeinId = s.NonCommercialCreditorMoeinId,
                    CommercialDebtorMoeinId = s.CommercialDebtorMoeinId,
                    SaleVatMoeinId = s.SaleVatMoeinId,
                    FreeTafsiltId = s.FreeTafsiltId,
                    WarrantyTafsilId = s.WarrantyTafsilId,
                    BankMoeinId = s.BankMoeinId,
                    BankTafsilId = s.BankTafsilId,
                    DebtorTafsil4Free = s.DebtorTafsil4Free,
                    DebtorTafsil4Warranty = s.DebtorTafsil4Warranty
                })
                .FirstOrDefaultAsync();

            // اگر تنظیمات وجود نداشت، ایجاد کن
            if (setting == null)
            {
                var newSetting = new asa_Setting
                {
                    SellerId = sellerId,
                    SaleMoeinId = null,
                    SaleFreeTafsilId = null,
                    SaleWarrantyTfsilId = null,
                    NonCommercialCreditorMoeinId = null,
                    CommercialDebtorMoeinId = null,
                    SaleVatMoeinId = null,
                    FreeTafsiltId = null,
                    WarrantyTafsilId = null
                };

                _db.Asa_Settings.Add(newSetting);
                await _db.SaveChangesAsync();

                setting = new ReceptionAccountingSettingDto
                {
                    Id = newSetting.Id,
                    SellerId = newSetting.SellerId,
                    SaleMoeinId = newSetting.SaleMoeinId,
                    SaleFreeTafsilId = newSetting.SaleFreeTafsilId,
                    SaleWarrantyTfsilId = newSetting.SaleWarrantyTfsilId,
                    NonCommercialCreditorMoeinId = newSetting.NonCommercialCreditorMoeinId,
                    CommercialDebtorMoeinId = newSetting.CommercialDebtorMoeinId,
                    SaleVatMoeinId = newSetting.SaleVatMoeinId,
                    FreeTafsiltId = newSetting.FreeTafsiltId,
                    WarrantyTafsilId = newSetting.WarrantyTafsilId,
                    DebtorTafsil4Warranty = newSetting.DebtorTafsil4Warranty,
                    DebtorTafsil4Free = newSetting.DebtorTafsil4Free,
                    BankMoeinId = newSetting.BankMoeinId,
                    BankTafsilId = newSetting.BankTafsilId,
                };
            }

            return setting;
        }
        public async Task<clsResult> UpdateSettingsAsync(ReceptionAccountingSettingDto dto)
        {
            clsResult result = new clsResult();

            if (dto == null || dto.SellerId <= 0)
            {
                result.Message = "اطلاعات نامعتبر است.";
                result.Success = false;
                return result;
            }

            var existingSetting = await _db.Asa_Settings
                .FirstOrDefaultAsync(s => s.SellerId == dto.SellerId);

            if (existingSetting == null)
            {
                result.Message = "تنظیمات مربوط به این فروشنده یافت نشد.";
                result.Success = false;
                return result;
            }

            // به روزرسانی فیلدها
            existingSetting.SaleMoeinId = dto.SaleMoeinId;
            existingSetting.SaleFreeTafsilId = dto.SaleFreeTafsilId;
            existingSetting.SaleWarrantyTfsilId = dto.SaleWarrantyTfsilId;
            existingSetting.NonCommercialCreditorMoeinId = dto.NonCommercialCreditorMoeinId;
            existingSetting.CommercialDebtorMoeinId = dto.CommercialDebtorMoeinId;
            existingSetting.SaleVatMoeinId = dto.SaleVatMoeinId;
            existingSetting.FreeTafsiltId = dto.FreeTafsiltId;
            existingSetting.WarrantyTafsilId = dto.WarrantyTafsilId;
            existingSetting.BankMoeinId = dto.BankMoeinId;
            existingSetting.BankTafsilId = dto.BankTafsilId;
            existingSetting.DebtorTafsil4Warranty = dto.DebtorTafsil4Warranty;
            existingSetting.DebtorTafsil4Free = dto.DebtorTafsil4Free;

            _db.Asa_Settings.Update(existingSetting);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "تنظیمات با موفقیت به روزرسانی شد.";
                result.updateType = 1;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "خطایی در ذخیره تنظیمات رخ داده است.";
                // Log Exception: ex.ToString()
            }

            return result;
        }


        // Reception
        public async Task<clsResult> CarRec_ImportReception_ModiranAsync(ImpodtReceptionDto dto)
        {
            var receptionList = new List<asa_Reception>();
            var result = new clsResult();
            string bachNumber = DateTime.Now.ToLong().ToString();
            try
            {
                if (dto.ExcelFile == null || dto.ExcelFile.Length <= 0)
                    throw new ArgumentException("فایل معتبر نیست");

                using (var stream = new MemoryStream())
                {
                    await dto.ExcelFile.CopyToAsync(stream);
                    using (var workbook = new XLWorkbook(stream))
                    {

                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RowsUsed();
                        foreach (var row in rows.Skip(1))
                        {

                            var locationValue = row.Cell("K").GetValue<string>().Trim();
                            var TypeValue = row.Cell("M").GetValue<string>().Trim();
                            var reception = new asa_Reception
                            {
                                ReceptionNumber = row.Cell("A").GetValue<string>(),
                                CustomerFullName = $"{row.Cell("B").GetValue<string>()} {row.Cell("C").GetValue<string>()}",
                                AgentCode = row.Cell("D").GetValue<string>(),
                                KilometersAtReception = int.TryParse(row.Cell("E").GetValue<string>(), out var km) ? km : 0,
                                // تاریخ شمسی به میلادی
                                ReceptionDate = row.Cell("F").GetValue<string>().PersianToLatin(),
                                Code = row.Cell("G").GetValue<string>(),
                                LogisticsCode = row.Cell("H").GetValue<string>(),
                                Name = row.Cell("I").GetValue<string>(),
                                Price = long.TryParse(row.Cell("J").GetValue<string>().Replace(",", ""), out var price) ? price : 0,
                                // استفاده از مقدار متنی برای کد و نام محل
                                LocationCode = locationValue == "گارانتی" ? (short)1 : locationValue == "مشتری" ? (short)2 : (short)0,
                                LocationName = locationValue,
                                Quantity = int.TryParse(row.Cell("L").GetValue<string>(), out var qty) ? qty : 0,

                                // استفاده از مقدار متنی برای کد و نام محل
                                TypeCode = TypeValue == "قطعه" ? (short)1 : (short)2,
                                TypeName = TypeValue,
                                InvoiceCode = row.Cell("N").GetValue<string>(),
                                InvoiceDate = row.Cell("O").GetValue<string>().PersianToLatin(),
                                Campaign = bool.TryParse(row.Cell("Q").GetValue<string>(), out var campaign) && campaign,
                                CreateDate = DateTime.Now,
                                SellerId = dto.SellerId,
                                Brand = dto.Brand,
                                CtreateBy = dto.UserCreator,
                                BachRefrense = bachNumber,
                                PeriodId = dto.PeriodId,
                            };

                            receptionList.Add(reception);
                        }
                    }
                }

                List<asa_Reception> finalList = new List<asa_Reception>();
                DateTime minDate = receptionList.Select(m => m.ReceptionDate).Min();
                DateTime maxDate = receptionList.Select(m => m.ReceptionDate).Max();
                var storedData = await _db.Asa_Receptions.AsNoTracking()
                    .Where(n => n.SellerId == dto.SellerId
                    && n.ReceptionDate >= minDate && n.ReceptionDate <= maxDate).ToListAsync();
                foreach (var rec in receptionList)
                {
                    if (!storedData.Where(n => n.ReceptionNumber == rec.ReceptionNumber).Any())
                        finalList.Add(rec);
                }
                // ذخیره‌سازی داده‌ها
                if (finalList.Count > 0)
                {
                    await _db.Asa_Receptions.AddRangeAsync(finalList);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    result.Success = false;
                    result.Message = "اطلاعات وارد شده تکراری است.";
                    result.ShowMessage = true;
                    return result;
                }

                // تنظیم مقدارهای موفقیت
                result.Success = true;
                result.Message = "اطلاعات با موفقیت وارد شد.";
                result.ShowMessage = true;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"خطای کلی: {ex.Message}";
                result.ShowMessage = true;
            }

            return result;
        }

        public async Task<clsResult> CarRec_ImportReception_LamariAsync(ImpodtReceptionDto dto)
        {
            var receptionList = new List<asa_Reception>();
            var result = new clsResult();
            string bachNumber = DateTime.Now.ToLong().ToString();
            try
            {
                if (dto.ExcelFile == null || dto.ExcelFile.Length <= 0)
                    throw new ArgumentException("فایل معتبر نیست");

                using (var stream = new MemoryStream())
                {
                    await dto.ExcelFile.CopyToAsync(stream);
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RowsUsed();
                        foreach (var row in rows.Skip(1))
                        {

                            string strRow = row.Cell("AK").GetValue<string>().Trim();
                            int intRow = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (intRow == 0)
                                continue;

                            var locationValue = row.Cell("S").GetValue<string>().Trim();
                            var TypeValue = row.Cell("AD").GetValue<string>().Trim();

                            var reception = new asa_Reception();
                            reception.ReceptionNumber = row.Cell("AJ").GetValue<string>();
                            reception.CustomerFullName = "";
                            reception.AgentCode = row.Cell("AB").GetValue<string>();
                            reception.KilometersAtReception = int.TryParse(row.Cell("C").GetValue<string>(), out var km) ? km : 0;
                            reception.ReceptionDate = row.Cell("AG").GetValue<string>().PersianToLatin();
                            reception.Code = row.Cell("AC").GetValue<string>();
                            reception.LogisticsCode = row.Cell("AH").GetValue<string>();
                            reception.Name = row.Cell("AA").GetValue<string>();
                            reception.Price = long.TryParse(row.Cell("T").GetValue<string>().Replace(",", ""), out var price) ? price : 0;
                            reception.LocationCode = locationValue == "WARRANTY" ? (short)1 : (short)2;
                            reception.LocationName = locationValue;
                            reception.Quantity = 1;
                            reception.TypeCode = TypeValue == "Item" ? (short)1 : (short)2;
                            reception.TypeName = TypeValue;
                            reception.InvoiceCode = row.Cell("S").GetValue<string>();
                            reception.InvoiceDate = row.Cell("F").GetValue<string>().PersianToLatin();
                            reception.Campaign = false;
                            reception.CreateDate = DateTime.Now;
                            reception.SellerId = dto.SellerId;
                            reception.Brand = dto.Brand;
                            reception.CtreateBy = dto.UserCreator;
                            reception.BachRefrense = bachNumber;
                            reception.PeriodId = dto.PeriodId;

                            if (reception.Price > 0)
                                receptionList.Add(reception);
                        }
                    }
                }

                List<asa_Reception> finalList = new List<asa_Reception>();
                DateTime minDate = receptionList.Select(m => m.ReceptionDate).Min();
                DateTime maxDate = receptionList.Select(m => m.ReceptionDate).Max();
                var storedData = await _db.Asa_Receptions.AsNoTracking()
                    .Where(n => n.SellerId == dto.SellerId
                    && n.ReceptionDate >= minDate && n.ReceptionDate <= maxDate).ToListAsync();
                foreach (var rec in receptionList)
                {
                    if (!storedData.Where(n => n.ReceptionNumber == rec.ReceptionNumber).Any())
                        finalList.Add(rec);
                }
                // ذخیره‌سازی داده‌ها
                if (finalList.Count > 0)
                {
                    await _db.Asa_Receptions.AddRangeAsync(finalList);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    result.Success = false;
                    result.Message = "اطلاعات وارد شده تکراری است.";
                    result.ShowMessage = true;
                    return result;
                }

                // تنظیم مقدارهای موفقیت
                result.Success = true;
                result.Message = "اطلاعات با موفقیت وارد شد.";
                result.ShowMessage = true;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"خطای کلی: {ex.Message}";
                result.ShowMessage = true;
            }

            return result;
        }

        public async Task<List<ReciptionDto>> CarRec_GetReceptionInfosAsync(ReciptionFilterDto filter)
        {
            var query = _db.Asa_Receptions.AsNoTracking().Where(n => n.SellerId == filter.SellerId);

            // فیلتر بر اساس شماره پذیرش
            if (!string.IsNullOrEmpty(filter.ReceptionNumber))
            {
                query = query.Where(r => r.ReceptionNumber.EndsWith(filter.ReceptionNumber));
            }

            // فیلتر بر اساس آیدی تکنسین
            if (filter.TechnicianId.HasValue)
            {
                query = query.Where(r => r.ContractorId == filter.TechnicianId.Value);
            }

            // فیلتر بر اساس کد محل
            if (filter.LocationCode.HasValue)
            {
                query = query.Where(r => r.LocationCode == filter.LocationCode.Value);
            }

            // فیلتر بر اساس کد نوع
            if (filter.TypeCode.HasValue)
            {
                query = query.Where(r => r.TypeCode == filter.TypeCode.Value);
            }

            // فیلتر بر اساس نام مشتری
            if (!string.IsNullOrEmpty(filter.CustomerFullName))
            {
                query = query.Where(r => r.CustomerFullName.Contains(filter.CustomerFullName));
            }

            // فیلتر بر اساس تاریخ پذیرش (شروع)
            if (filter.FromDate.HasValue)
            {
                query = query.Where(r => r.ReceptionDate >= filter.FromDate.Value);
            }

            // فیلتر بر اساس تاریخ پذیرش (پایان)
            if (filter.ToDate.HasValue)
            {
                query = query.Where(r => r.ReceptionDate <= filter.ToDate.Value);
            }

            var data = await query
        .Select(receptionInfo => new ReciptionDto
        {
            Id = receptionInfo.Id,
            ReceptionNumber = receptionInfo.ReceptionNumber,
            CustomerFullName = receptionInfo.CustomerFullName,
            AgentCode = receptionInfo.AgentCode,
            KilometersAtReception = receptionInfo.KilometersAtReception,
            ReceptionDate = receptionInfo.ReceptionDate,
            Code = receptionInfo.Code,
            LogisticsCode = receptionInfo.LogisticsCode,
            Name = receptionInfo.Name,
            Price = receptionInfo.Price,
            LocationCode = receptionInfo.LocationCode,
            LocationName = receptionInfo.LocationName,
            Quantity = receptionInfo.Quantity,
            TypeCode = receptionInfo.TypeCode,
            TypeName = receptionInfo.TypeName,
            InvoiceCode = receptionInfo.InvoiceCode,
            InvoiceDate = receptionInfo.InvoiceDate,
            Campaign = receptionInfo.Campaign,
            Brand = receptionInfo.Brand,
            ContractorId = receptionInfo.ContractorId,
            DocNum = receptionInfo.DocNum,
            PeriodId = receptionInfo.PeriodId,
            ArchiveNum = receptionInfo.ArchiveNum,
            PersonRate = receptionInfo.PersonRate,
            HasDocument = receptionInfo.HasDocument,
            SellerId = receptionInfo.SellerId,
            TotalGhate = receptionInfo.TotalGhate,
            TotalOjrat = receptionInfo.TotalOjrat,
            ExtraPrice = receptionInfo.ExtraPrice,
            CreateDate = receptionInfo.CreateDate,
            CtreateBy = receptionInfo.CtreateBy,
            BachRefrense = receptionInfo.BachRefrense,
            ContractorShareMoney = receptionInfo.ContractorShareMoney,
            ContractorPersentage = receptionInfo.ContractorPersentage,
            lamariElectricContractor = receptionInfo.lamariElectricContractorId,
            lamariElectricShareAmount = receptionInfo.lamariElectricShareAmount,
            lamariMechanicContractor = receptionInfo.lamariMechanicContractorId,
            lamariMechanicShareAmount = receptionInfo.lamariMechanicShareAmount,
            ServiceId = receptionInfo.ServiceId,

        }).OrderBy(n => n.ReceptionNumber).ToListAsync();

            return data;
        }

        public async Task<List<ReceptionHeaderDto>> CarRec_GetReceptionHeaderAsync(ReciptionFilterDto filter)
        {
            var query = _db.Asa_Receptions.AsNoTracking().Where(n => n.SellerId == filter.SellerId);

            if (filter.JustNonDoc)
                query = query.Where(n => !n.DocNum.HasValue);

            // فیلتر بر اساس شماره پذیرش
            if (!string.IsNullOrEmpty(filter.ReceptionNumber))
            {
                query = query.Where(r => r.ReceptionNumber.EndsWith(filter.ReceptionNumber));
            }

            // فیلتر بر اساس آیدی تکنسین
            if (filter.TechnicianId.HasValue)
            {
                query = query.Where(r => r.ContractorId == filter.TechnicianId.Value);
            }

            // فیلتر بر اساس کد محل
            if (filter.LocationCode.HasValue)
            {
                query = query.Where(r => r.LocationCode == filter.LocationCode.Value);
            }

            // فیلتر بر اساس کد نوع
            if (filter.TypeCode.HasValue)
            {
                query = query.Where(r => r.TypeCode == filter.TypeCode.Value);
            }

            // فیلتر بر اساس نام مشتری
            if (!string.IsNullOrEmpty(filter.CustomerFullName))
            {
                query = query.Where(r => r.CustomerFullName.Contains(filter.CustomerFullName));
            }

            // فیلتر بر اساس تاریخ پذیرش (شروع)
            if (filter.FromDate.HasValue)
            {
                query = query.Where(r => r.ReceptionDate >= filter.FromDate.Value);
            }

            // فیلتر بر اساس تاریخ پذیرش (پایان)
            if (filter.ToDate.HasValue)
            {
                query = query.Where(r => r.ReceptionDate <= filter.ToDate.Value);
            }

            var data = await query.GroupBy(n => n.ReceptionNumber).Select(n => new ReceptionHeaderDto
            {
                ReceptionNumber = n.Key,
                CustomerName = n.FirstOrDefault().CustomerFullName,
                Date = n.FirstOrDefault().ReceptionDate,
                InvoiceNumber = n.FirstOrDefault().InvoiceCode,
                InvoiceDate = n.FirstOrDefault().InvoiceDate,
                IncommType = n.FirstOrDefault().LocationName,

                SaleFreeAmount = n.Where(n => n.TypeCode == 1 && n.LocationCode == 2).Sum(n => n.Price * n.Quantity) ?? 0,
                ojratFreeTotal = n.Where(n => n.TypeCode == 2 && n.LocationCode == 2).Sum(n => n.Price) ?? 0,

                SaleWarrantyAmount = n.Where(n => n.TypeCode == 1 && n.LocationCode == 1).Sum(n => n.Price * n.Quantity) ?? 0,
                ojratWarrantyTotal = n.Where(n => n.TypeCode == 2 && n.LocationCode == 1).Sum(n => n.Price) ?? 0,

                AgancyCode = n.FirstOrDefault().AgentCode,
                BachNumber = n.FirstOrDefault().BachRefrense,
                HasDoc = n.FirstOrDefault().HasDocument ?? false,
                DocNum = n.FirstOrDefault().DocNum,
                ArchiveNumber = n.FirstOrDefault().ArchiveNum,

            }).OrderBy(n => n.ReceptionNumber).ToListAsync();

            return data;
        }

        public async Task<List<ReciptionDto>> GetReceptionItemsByNumbersAsync(long sellerId, string ReceptionNumber)
        {
            var query = _db.Asa_Receptions.AsNoTracking()
                .Include(n => n.Contractor)
                .Include(n => n.MechanicContractor)
                .Include(n => n.ElectricContractor)
                .Where(n => n.SellerId == sellerId && n.ReceptionNumber == ReceptionNumber);

            var data = await query
        .Select(receptionInfo => new ReciptionDto
        {
            Id = receptionInfo.Id,
            ReceptionNumber = receptionInfo.ReceptionNumber,
            CustomerFullName = receptionInfo.CustomerFullName,
            AgentCode = receptionInfo.AgentCode,
            KilometersAtReception = receptionInfo.KilometersAtReception,
            ReceptionDate = receptionInfo.ReceptionDate,
            Code = receptionInfo.Code,
            LogisticsCode = receptionInfo.LogisticsCode,
            Name = receptionInfo.Name,
            Price = receptionInfo.Price,
            LocationCode = receptionInfo.LocationCode,
            LocationName = receptionInfo.LocationName,
            Quantity = receptionInfo.Quantity,
            TypeCode = receptionInfo.TypeCode,
            TypeName = receptionInfo.TypeName,
            InvoiceCode = receptionInfo.InvoiceCode,
            InvoiceDate = receptionInfo.InvoiceDate,
            Campaign = receptionInfo.Campaign,
            Brand = receptionInfo.Brand,
            ContractorId = receptionInfo.ContractorId,
            DocNum = receptionInfo.DocNum,
            PeriodId = receptionInfo.PeriodId,
            ArchiveNum = receptionInfo.ArchiveNum,
            PersonRate = receptionInfo.PersonRate,
            HasDocument = receptionInfo.HasDocument,
            SellerId = receptionInfo.SellerId,
            TotalGhate = receptionInfo.TotalGhate,
            TotalOjrat = receptionInfo.TotalOjrat,
            ExtraPrice = receptionInfo.ExtraPrice,
            CreateDate = receptionInfo.CreateDate,
            CtreateBy = receptionInfo.CtreateBy,
            BachRefrense = receptionInfo.BachRefrense,
            lamariElectricContractor = receptionInfo.lamariElectricContractorId,
            lamariElectricShareAmount = receptionInfo.lamariElectricShareAmount,
            lamariMechanicContractor = receptionInfo.lamariMechanicContractorId,
            lamariMechanicShareAmount = receptionInfo.lamariMechanicShareAmount,
            ServiceId = receptionInfo.ServiceId,
            LamariServiceId = receptionInfo.LamariServiceId,
            LamariserviceName = receptionInfo.LamariService != null ? receptionInfo.LamariService.ServiceName : ""

        }).OrderBy(n => n.ReceptionNumber).ToListAsync();

            return data;
        }

        public async Task<clsResult> UpdateReceptionDetailsAsync(List<ReciptionDto> items)
        {
            var result = new clsResult();

            // دریافت شماره پلاک از اولین آیتم
            var licensePlate = items.FirstOrDefault()?.LicensePlate;

            // بروزرسانی تمام آیتم‌های پذیرش با شماره پلاک یکسان
            var receptionNumbers = items.Select(x => x.ReceptionNumber).Distinct();
            foreach (var receptionNumber in receptionNumbers)
            {
                var receptions = await _db.Asa_Receptions
                    .Where(x => x.ReceptionNumber == receptionNumber)
                    .ToListAsync();

                foreach (var reception in receptions)
                {
                    reception.LicensePlate = licensePlate;
                }
            }

            // بروزرسانی اطلاعات پیمانکار و سرویس برای آیتم‌های اجرت
            foreach (var item in items.Where(x => x.TypeCode == 2))
            {
                var reception = await _db.Asa_Receptions.FindAsync(item.Id);
                if (reception != null)
                {
                    reception.ContractorId = item.ContractorId;
                    reception.ServiceId = item.ServiceId;

                    if (item.ContractorId.HasValue)
                    {
                        var contractor = await _db.Asa_Contractors.FindAsync(item.ContractorId.Value);
                        if (contractor != null)
                        {
                            reception.ContractorPersentage = (float)contractor.SharePercentage;
                            reception.ContractorShareMoney = (long)((item.Price ?? 0) * contractor.SharePercentage / 100);
                        }
                    }
                }
            }
            try
            {
                await _db.SaveChangesAsync();

                long sellerId = items.FirstOrDefault().SellerId;
                string recNumber = items.FirstOrDefault().ReceptionNumber;
                var reception = await GetSaveDetailsDtoAsync(sellerId, recNumber);
                if (reception != null)
                {

                    result = await _docCreator.CreateModiranReceptionDocAsync(reception);
                    if (result.Success)
                    {
                        var updateRec = await _db.Asa_Receptions.Where(n => n.SellerId == sellerId && n.ReceptionNumber == recNumber).ToListAsync();
                        foreach (var x in updateRec)
                        {
                            x.ArchiveNum = result.ArchiveCode;
                            x.DocNum = result.DocNumber;
                            x.HasDocument = true;
                        }
                        _db.Asa_Receptions.UpdateRange(updateRec);
                        await _db.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                result.Message = "خطا در بروزرسانی اطلاعات: " + ex.Message;
            }

            return result;
        }
        public async Task<clsResult> UpdateLamariReceptionAsync(List<ReciptionDto> items)
        {
            var result = new clsResult();

            // دریافت شماره پلاک از اولین آیتم
            var licensePlate = items.FirstOrDefault()?.LicensePlate;

            // بروزرسانی تمام آیتم‌های پذیرش با شماره پلاک یکسان
            var receptionNumbers = items.Select(x => x.ReceptionNumber).Distinct();
            foreach (var receptionNumber in receptionNumbers)
            {
                var receptions = await _db.Asa_Receptions
                    .Where(x => x.ReceptionNumber == receptionNumber)
                    .ToListAsync();

                foreach (var reception in receptions)
                {
                    reception.LicensePlate = licensePlate;
                }
            }

            // بروزرسانی اطلاعات پیمانکار و سرویس برای آیتم‌های اجرت
            foreach (var item in items.Where(x => x.TypeCode == 2))
            {
                var reception = await _db.Asa_Receptions.FindAsync(item.Id);
                if (reception != null)
                {
                    if (item.LamariServiceId != null)
                    {
                        var lamariService = await _db.Asa_LamariServices.FindAsync(item.LamariServiceId);
                        reception.ServiceType = lamariService.ServiceType;
                    }
                    reception.LamariServiceId = item.LamariServiceId;
                    reception.ContractorId = item.ContractorId;
                    reception.ContractorPersentage = item.ContractorPersentage;
                    reception.ContractorShareMoney = item.ContractorShareMoney;

                    reception.lamariMechanicContractorId = item.lamariMechanicContractor;
                    reception.lamariMechanicShareAmount = item.lamariMechanicShareAmount;

                    reception.lamariElectricContractorId = item.lamariElectricContractor;
                    reception.lamariElectricShareAmount = item.lamariElectricShareAmount;
                }
            }
            try
            {
                await _db.SaveChangesAsync();

                long sellerId = items.FirstOrDefault().SellerId;
                string recNumber = items.FirstOrDefault().ReceptionNumber;
                var reception = await GetSaveDetailsDtoAsync(sellerId, recNumber);
                if (reception != null)
                {

                    result = await _docCreator.CreateLamaryReceptionDocAsync(reception);
                    if (result.Success)
                    {
                        var updateRec = await _db.Asa_Receptions.Where(n => n.SellerId == sellerId && n.ReceptionNumber == recNumber).ToListAsync();
                        foreach (var x in updateRec)
                        {
                            x.ArchiveNum = result.ArchiveCode;
                            x.DocNum = result.DocNumber;
                            x.HasDocument = true;
                        }
                        _db.Asa_Receptions.UpdateRange(updateRec);
                        await _db.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                result.Message = "خطا در بروزرسانی اطلاعات: " + ex.Message;
            }

            return result;
        }
        public async Task<SaveDetailsDto> GetSaveDetailsDtoAsync(long sellerId, string ReceptionNumber)
        {
            var data = await _db.Asa_Receptions
                .AsNoTracking().Where(n => n.SellerId == sellerId && n.ReceptionNumber == ReceptionNumber)
                .ToListAsync();

            var dto = new SaveDetailsDto();
            dto.Items = new List<SaveDetails_ItemDto>();
            if (!data.Any())
                return dto;

            var f = data.FirstOrDefault();
            dto.ReceptionNember = f.ReceptionNumber;
            dto.ReceptionDate = f.ReceptionDate.LatinToPersian();
            dto.InvoiceDate = f.InvoiceDate;
            dto.Date = f.ReceptionDate;
            dto.CarNumber = f.LicensePlate ?? "";
            dto.ClientName = f.CustomerFullName;
            dto.IncomeType = f.LocationName;
            dto.IncomeTypeCode = f.TypeCode ?? 0;
            dto.Brand = f.Brand;

            dto.TotalSaleAmount = data.Where(n => n.TypeCode == 1).Sum(n => n.Price * n.Quantity.Value) ?? 0;
            dto.TotalOjratAmount = data.Where(n => n.TypeCode == 2).Sum(n => n.Price) ?? 0;
            dto.VatAmount = (dto.TotalOjratAmount + dto.TotalSaleAmount) * 10 / 100;

            dto.SaleFreeAmount = data.Where(n => n.TypeCode == 1 && n.LocationCode == 2).Sum(n => n.Price * n.Quantity.Value) ?? 0;
            dto.ojratFreeTotal = data.Where(n => n.TypeCode != 1 && n.LocationCode == 2).Sum(n => n.Price) ?? 0;
            dto.SaleWarrantyAmount = data.Where(n => n.TypeCode == 1 && n.LocationCode == 1).Sum(n => n.Price * n.Quantity.Value) ?? 0;
            dto.ojratWarrantyTotal = data.Where(n => n.TypeCode != 1 && n.LocationCode == 1).Sum(n => n.Price) ?? 0;

            var ojrat = data.Where(n => n.TypeCode == 2);
            if (ojrat.Any())
            {
                foreach (var x in ojrat)
                {
                    SaveDetails_ItemDto item = new SaveDetails_ItemDto();
                    item.Id = x.Id;
                    item.ItemName = x.Name;
                    item.ContractorId = x.ContractorId;
                    item.LamaryServiceType = x.ServiceType;
                    item.ServiceId = x.ServiceId;
                    item.LamariServiceId = x.LamariServiceId;
                    item.ContractorShareAmount = x.ContractorShareMoney;
                    item.Percentage = x.ContractorPersentage;
                    item.Price = x.Price ?? 0;
                    item.Income = x.LocationName;
                    item.IncomeCode = x.LocationCode;
                    item.lamariElectricContractor = x.lamariElectricContractorId;
                    item.lamariElectricShareAmount = x.lamariElectricShareAmount;
                    item.lamariMechanicContractor = x.lamariMechanicContractorId;
                    item.lamariMechanicShareAmount = x.lamariMechanicShareAmount;
                    item.lamaristrMechanicShareAmount = x.lamariMechanicShareAmount.ToPrice();
                    item.lamaristrElectricShareAmount = x.lamariElectricShareAmount.ToPrice();
                    dto.Items.Add(item);
                }
            }

            return dto;

        }

        // =========================== Lamari Services
        public async Task<SelectList> SelectList_LamariServicesAsync(long SellerId)
        {
            var services = await _db.Asa_LamariServices
                .Where(n => n.SellerId == SellerId && n.IsActive)
                .Select(n => new { Id = n.Id, Name = n.ServiceName })
                .OrderBy(n => n.Name)
                .ToListAsync();

            return new SelectList(services, "Id", "Name");
        }
        public async Task<List<LamariServiceDto>> GetLamariServicesAsync(long SellerId)
        {
            var services = await _db.Asa_LamariServices.AsNoTracking()
                .Where(n => n.SellerId == SellerId)
                .Select(n => new LamariServiceDto
                {
                    Id = n.Id,
                    ServiceType = n.ServiceType,
                    SellerId = n.SellerId,
                    ServiceName = n.ServiceName,
                    AgencySharePercentage = n.AgencySharePercentage,
                    ElectricianShareAmount = n.ElectricianShareAmount,
                    MechanicShareAmount = n.MechanicShareAmount,
                    MoeinId = n.MoeinId,
                    TafsilId = n.TafsilId,
                    IsActive = n.IsActive
                }).OrderBy(n => n.ServiceName).ToListAsync();

            return services;
        }
        public async Task<LamariServiceDto> GetLamariServiceByIdAsync(int id)
        {
            var service = await _db.Asa_LamariServices
                .Where(s => s.Id == id)
                .Select(n => new LamariServiceDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    ServiceType = n.ServiceType,
                    ServiceName = n.ServiceName,
                    AgencySharePercentage = n.AgencySharePercentage,
                    ElectricianShareAmount = n.ElectricianShareAmount,
                    MechanicShareAmount = n.MechanicShareAmount,
                    MoeinId = n.MoeinId,
                    TafsilId = n.TafsilId,
                    IsActive = n.IsActive
                })
                .FirstOrDefaultAsync();

            return service;
        }
        public async Task<clsResult> AddLamriServiceAsync(LamariServiceDto dto)
        {
            clsResult result = new clsResult();

            bool isDuplicate = await _db.Asa_LamariServices
                .AnyAsync(n => n.SellerId == dto.SellerId && n.ServiceName == dto.ServiceName);

            if (isDuplicate)
            {
                result.Message = $"سرویس با نام {dto.ServiceName} قبلاً ثبت شده است.";
                return result;
            }

            asa_LamariService newService = new asa_LamariService
            {
                SellerId = dto.SellerId,
                ServiceType = dto.ServiceType,
                ServiceName = dto.ServiceName,
                MoeinId = dto.MoeinId,
                TafsilId = dto.TafsilId,
                IsActive = dto.IsActive,
                MechanicShareAmount = dto.MechanicShareAmount,
                AgencySharePercentage = dto.AgencySharePercentage,
                ElectricianShareAmount = dto.ElectricianShareAmount,
            };

            _db.Asa_LamariServices.Add(newService);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات سرویس جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = "در عملیات ثبت اطلاعات سرویس جدید خطایی رخ داده است.";
                result.Success = false;
                // Log Exception: ex.ToString()
            }

            return result;
        }
        public async Task<clsResult> UpdateLamariServiceAsync(LamariServiceDto dto)
        {
            clsResult result = new clsResult();

            var existingService = await _db.Asa_LamariServices
                .FirstOrDefaultAsync(n => n.Id == dto.Id && n.SellerId == dto.SellerId);

            if (existingService == null)
            {
                result.Message = "سرویس مورد نظر یافت نشد.";
                return result;
            }

            bool isDuplicate = await _db.Asa_LamariServices
                .AnyAsync(n => n.SellerId == dto.SellerId && n.ServiceName == dto.ServiceName && n.Id != dto.Id);

            if (isDuplicate)
            {
                result.Message = $"سرویس با نام {dto.ServiceName} قبلاً ثبت شده است.";
                return result;
            }

            existingService.ServiceType = dto.ServiceType;
            existingService.ServiceName = dto.ServiceName;
            existingService.MoeinId = dto.MoeinId;
            existingService.TafsilId = dto.TafsilId;
            existingService.IsActive = dto.IsActive;
            existingService.MechanicShareAmount = dto.MechanicShareAmount;
            existingService.ElectricianShareAmount = dto.ElectricianShareAmount;
            existingService.AgencySharePercentage = dto.AgencySharePercentage;

            _db.Asa_LamariServices.Update(existingService);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات سرویس با موفقیت بروزرسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = "در عملیات بروزرسانی اطلاعات سرویس خطایی رخ داده است.";
                result.Success = false;
                // Log Exception: ex.ToString()
            }

            return result;
        }

    }
}
