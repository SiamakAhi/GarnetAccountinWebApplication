using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Models;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class CuPricingService : ICuPricingService
    {
        private readonly AppDbContext _db;
        private readonly UserContextService _userContextService;

        public CuPricingService(AppDbContext context, UserContextService userContextService)
        {
            _db = context;
            _userContextService = userContextService;
        }

        public async Task<SelectList> SelectList_RateImpactTypeAsync()
        {
            var lst = await _db.Cu_RateImpactTypes.Select(n => new { id = n.Id, name = n.Name }).ToListAsync();
            return new SelectList(lst, "id", "name");
        }

        // دریافت لیست انواع تأثیر بر نرخ پایه
        public async Task<List<RateImpactTypeDto>> GetRateImpactTypesAsync()
        {
            if (_userContextService.SellerId == null)
                return new List<RateImpactTypeDto>();

            var data = await _db.Cu_RateImpactTypes
                .Select(n => new RateImpactTypeDto
                {
                    Id = n.Id,
                    RateImpactTypeCode = n.RateImpactTypeCode,
                    Name = n.Name,
                    Description = n.Description
                })
                .ToListAsync();

            return data;
        }




        // دریافت لیست رنج‌های وزنی
        public async Task<List<RateWeightRangeDto>> GetRateWeightRangesAsync()
        {
            if (_userContextService.SellerId == null)
                return new List<RateWeightRangeDto>();

            var data = await _db.Cu_RateWeightRanges
                .Select(n => new RateWeightRangeDto
                {
                    Id = n.Id,
                    StartWeight = n.StartWeight,
                    EndWeight = n.EndWeight,
                    WeightFactorPercent = n.Courier_WeightFactorPercent
                })
                .ToListAsync();

            return data;
        }

        // دریافت یک رنج وزنی بر اساس شناسه
        public async Task<RateWeightRangeDto> GetRateWeightRangeByIdAsync(int id)
        {
            if (_userContextService.SellerId == null)
                return null;

            var rateWeightRange = await _db.Cu_RateWeightRanges
                .FirstOrDefaultAsync(n => n.Id == id);

            if (rateWeightRange == null)
                return null;

            return new RateWeightRangeDto
            {
                Id = rateWeightRange.Id,
                StartWeight = rateWeightRange.StartWeight,
                EndWeight = rateWeightRange.EndWeight,
                WeightFactorPercent = rateWeightRange.Courier_WeightFactorPercent
            };
        }

        // دریافت لیست مناطق قیمت‌گذاری
        public async Task<List<RateZoneDto>> GetRateZonesAsync()
        {
            if (_userContextService.SellerId == null)
                return new List<RateZoneDto>();

            var data = await _db.Cu_RateZones
                .Select(n => new RateZoneDto
                {
                    ZoneId = n.ZoneId,
                    SellerId = n.SellerId,
                    Name = n.Name,
                    IsSatellite = n.IsSatellite
                })
                .ToListAsync();

            return data;
        }



        //================================================================================== Nature


        public async Task<SelectList> SelectList_ConsigmentNatureAsync()
        {
            if (_userContextService.SellerId == null)
                return null;

            var data = await _db.Cu_ConsignmentNatures.Where(n => n.SellerId == _userContextService.SellerId.Value)
               .Select(n => new
               {
                   id = n.Id,
                   name = n.Name,
               }).ToListAsync();

            return new SelectList(data, "id", "name");
        }
        // دریافت لیست ماهیت‌های محموله
        public async Task<List<ConsignmentNatureDto>> GetConsignmentNaturesAsync()
        {
            if (_userContextService.SellerId == null)
                return new List<ConsignmentNatureDto>();

            var data = await _db.Cu_ConsignmentNatures
                .Select(n => new ConsignmentNatureDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    Name = n.Name,
                    Code = n.Code,
                    IsAirTransportable = n.IsAirTransportable,
                    IsGroundTransportable = n.IsGroundTransportable,
                    RateImpactTypeId = n.RateImpactTypeId,
                    RateImpactValue = n.RateImpactValue
                })
                .ToListAsync();

            return data;
        }

        public async Task<clsResult> AddConsigmentNatureAsync(ConsignmentNatureDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (_userContextService.SellerId == null)
            {
                result.Message = "شرکت فعالی یافت نشد";
                return result;
            }

            if (dto == null)
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }

            var nature = new Cu_ConsignmentNature
            {
                Code = dto.Code,
                Name = dto.Name,
                IsAirTransportable = dto.IsAirTransportable,
                IsGroundTransportable = dto.IsGroundTransportable,
                RateImpactTypeId = dto.RateImpactTypeId,
                RateImpactValue = dto.RateImpactValue,
                SellerId = dto.SellerId,
            };

            _db.Cu_ConsignmentNatures.Add(nature);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "ماهیت جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        public async Task<clsResult> UpdateConsigmentNatureAsync(ConsignmentNatureDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null || dto.Id == 0)
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }

            var nature = await _db.Cu_ConsignmentNatures.FindAsync(dto.Id);
            if (nature == null)
            {
                result.Message = "رنج وزنی یافت نشد";
                return result;
            }

            nature.Name = dto.Name;
            nature.IsAirTransportable = dto.IsAirTransportable;
            nature.IsGroundTransportable = dto.IsGroundTransportable;
            nature.RateImpactTypeId = dto.RateImpactTypeId;
            nature.RateImpactValue = dto.RateImpactValue;
            nature.SellerId = dto.SellerId;

            _db.Cu_ConsignmentNatures.Update(nature);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات ماهیت با موفقیت بروزسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان بروزرسانی اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> DeleteConsigmentNatureAsync(int id)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            var nature = await _db.Cu_ConsignmentNatures.FindAsync(id);
            if (nature == null)
            {
                result.Message = "ماهیت یافت نشد";
                return result;
            }

            _db.Cu_ConsignmentNatures.Remove(nature);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "ماهیت با موفقیت حذف شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان حذف اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }


        //================================================================================== WeightRange

        // افزودن یک رنج وزنی جدید
        public async Task<clsResult> AddRateWeightRangeAsync(RateWeightRangeDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (_userContextService.SellerId == null)
            {
                result.Message = "شرکت فعالی یافت نشد";
                return result;
            }

            if (dto == null)
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }

            var rateWeightRange = new Cu_RateWeightRange
            {
                StartWeight = dto.StartWeight,
                EndWeight = dto.EndWeight,
                Courier_WeightFactorPercent = dto.WeightFactorPercent
            };

            _db.Cu_RateWeightRanges.Add(rateWeightRange);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "رنج وزنی جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        // بروزرسانی یک رنج وزنی
        public async Task<clsResult> UpdateRateWeightRangeAsync(RateWeightRangeDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null || dto.Id == 0)
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }

            var rateWeightRange = await _db.Cu_RateWeightRanges.FindAsync(dto.Id);
            if (rateWeightRange == null)
            {
                result.Message = "رنج وزنی یافت نشد";
                return result;
            }

            rateWeightRange.StartWeight = dto.StartWeight;
            rateWeightRange.EndWeight = dto.EndWeight;
            rateWeightRange.Courier_WeightFactorPercent = dto.WeightFactorPercent;

            _db.Cu_RateWeightRanges.Update(rateWeightRange);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات رنج وزنی با موفقیت بروزسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان بروزرسانی اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        // حذف یک رنج وزنی
        public async Task<clsResult> DeleteRateWeightRangeAsync(int id)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            var rateWeightRange = await _db.Cu_RateWeightRanges.FindAsync(id);
            if (rateWeightRange == null)
            {
                result.Message = "رنج وزنی یافت نشد";
                return result;
            }

            _db.Cu_RateWeightRanges.Remove(rateWeightRange);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "رنج وزنی با موفقیت حذف شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان حذف اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        //================================================================================== Cost Item

        // دریافت لیست آیتم‌های هزینه
        public async Task<List<CostItemDto>> GetCostItemsAsync()
        {
            if (_userContextService.SellerId == null)
                return new List<CostItemDto>();

            var data = await _db.Cu_BillOfLadingCostItems
                .Select(n => new CostItemDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    CostCode = n.CostCode,
                    Description = n.Description,
                    RateImpactTypeCode = n.RateImpactTypeCode,
                    Amount = n.Amount,
                    ForBillOfLading = n.ForBillOfLading,
                    ForConsignment = n.ForConsignment,
                    IsAutoAdded = n.IsAutoAdded,
                    AccountMoeinId = n.AccountMoeinId,
                    AccountTafsilId = n.AccountTafsilId,
                    RateImpactTypeName = n.RateImpactTypeCode.ToRateImpactType(),
                })
                .ToListAsync();

            return data;
        }

        public async Task<CostItemDto> GetCostItemByIdAsync(int id)
        {
            if (_userContextService.SellerId == null)
                return null;

            var costItem = await _db.Cu_BillOfLadingCostItems
                .FirstOrDefaultAsync(n => n.Id == id);

            if (costItem == null)
                return null;

            var costItemDto = new CostItemDto();
            costItemDto.Id = costItem.Id;
            costItemDto.SellerId = costItem.SellerId;
            costItemDto.CostCode = costItem.CostCode;
            costItemDto.Description = costItem.Description;
            costItemDto.RateImpactTypeCode = costItem.RateImpactTypeCode;
            costItemDto.Amount = costItem.Amount;
            costItemDto.ForBillOfLading = costItem.ForBillOfLading;
            costItemDto.ForConsignment = costItem.ForConsignment;
            costItemDto.IsAutoAdded = costItem.IsAutoAdded;
            costItemDto.AccountMoeinId = costItem.AccountMoeinId;
            costItemDto.AccountTafsilId = costItem.AccountTafsilId;

            return costItemDto;
        }

        // افزودن یک آیتم هزینه جدید
        public async Task<clsResult> AddCostItemAsync(CostItemDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (_userContextService.SellerId == null)
            {
                result.Message = "شرکت فعالی یافت نشد";
                return result;
            }

            if (dto == null)
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }

            var costItem = new Cu_BillOfLadingCostItem
            {
                SellerId = _userContextService.SellerId.Value,
                CostCode = dto.CostCode,
                Description = dto.Description,
                RateImpactTypeCode = dto.RateImpactTypeCode,
                Amount = dto.Amount,
                ForBillOfLading = dto.ForBillOfLading,
                ForConsignment = dto.ForConsignment,
                IsAutoAdded = dto.IsAutoAdded,
                AccountMoeinId = dto.AccountMoeinId,
                AccountTafsilId = dto.AccountTafsilId
            };

            _db.Cu_BillOfLadingCostItems.Add(costItem);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "آیتم هزینه جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        // بروزرسانی یک آیتم هزینه
        public async Task<clsResult> UpdateCostItemAsync(CostItemDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null || dto.Id == 0)
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }

            var costItem = await _db.Cu_BillOfLadingCostItems.FindAsync(dto.Id);
            if (costItem == null)
            {
                result.Message = "آیتم هزینه یافت نشد";
                return result;
            }

            costItem.CostCode = dto.CostCode;
            costItem.Description = dto.Description;
            costItem.RateImpactTypeCode = dto.RateImpactTypeCode;
            costItem.Amount = dto.Amount;
            costItem.ForBillOfLading = dto.ForBillOfLading;
            costItem.ForConsignment = dto.ForConsignment;
            costItem.IsAutoAdded = dto.IsAutoAdded;
            costItem.AccountMoeinId = dto.AccountMoeinId;
            costItem.AccountTafsilId = dto.AccountTafsilId;

            _db.Cu_BillOfLadingCostItems.Update(costItem);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات آیتم هزینه با موفقیت بروزسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان بروزرسانی اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        // حذف یک آیتم هزینه
        public async Task<clsResult> DeleteCostItemAsync(int id)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            var costItem = await _db.Cu_BillOfLadingCostItems.FindAsync(id);
            if (costItem == null)
            {
                result.Message = "آیتم هزینه یافت نشد";
                return result;
            }

            _db.Cu_BillOfLadingCostItems.Remove(costItem);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "آیتم هزینه با موفقیت حذف شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان حذف اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        public async Task<RateImpactTypeDto> GetRateImpactTypeByIdAsync(short id)
        {
            if (_userContextService.SellerId == null)
                return null;

            var rateImpactType = await _db.Cu_RateImpactTypes
                .FirstOrDefaultAsync(n => n.Id == id);

            if (rateImpactType == null)
                return null;

            var rateImpactTypeDto = new RateImpactTypeDto();
            rateImpactTypeDto.Id = rateImpactType.Id;
            rateImpactTypeDto.RateImpactTypeCode = rateImpactType.RateImpactTypeCode;
            rateImpactTypeDto.Name = rateImpactType.Name;
            rateImpactTypeDto.Description = rateImpactType.Description;

            return rateImpactTypeDto;
        }

        public async Task<RateZoneDto> GetRateZoneByIdAsync(int id)
        {
            if (_userContextService.SellerId == null)
                return null;

            var rateZone = await _db.Cu_RateZones
                .FirstOrDefaultAsync(n => n.ZoneId == id);

            if (rateZone == null)
                return null;

            var rateZoneDto = new RateZoneDto();
            rateZoneDto.ZoneId = rateZone.ZoneId;
            rateZoneDto.SellerId = rateZone.SellerId;
            rateZoneDto.Name = rateZone.Name;
            rateZoneDto.IsSatellite = rateZone.IsSatellite;

            return rateZoneDto;
        }

        public async Task<ConsignmentNatureDto> GetConsignmentNatureByIdAsync(short id)
        {
            if (_userContextService.SellerId == null)
                return null;

            var consignmentNature = await _db.Cu_ConsignmentNatures
                .FirstOrDefaultAsync(n => n.Id == id);

            if (consignmentNature == null)
                return null;

            var consignmentNatureDto = new ConsignmentNatureDto();
            consignmentNatureDto.Id = consignmentNature.Id;
            consignmentNatureDto.SellerId = consignmentNature.SellerId;
            consignmentNatureDto.Name = consignmentNature.Name;
            consignmentNatureDto.Code = consignmentNature.Code;
            consignmentNatureDto.IsAirTransportable = consignmentNature.IsAirTransportable;
            consignmentNatureDto.IsGroundTransportable = consignmentNature.IsGroundTransportable;
            consignmentNatureDto.RateImpactTypeId = consignmentNature.RateImpactTypeId;
            consignmentNatureDto.RateImpactValue = consignmentNature.RateImpactValue;

            return consignmentNatureDto;
        }





    }
}