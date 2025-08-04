using GarnetAccounting.Areas.Courier.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface ICuPricingService
    {
        //انواع تٔاثیر بر نرخ
        Task<SelectList> SelectList_RateImpactTypeAsync();

        // دریافت لیست انواع تأثیر بر نرخ پایه
        Task<List<RateImpactTypeDto>> GetRateImpactTypesAsync();

        // دریافت لیست رنج‌های وزنی
        Task<List<RateWeightRangeDto>> GetRateWeightRangesAsync();

        // دریافت یک رنج وزنی بر اساس شناسه
        Task<RateWeightRangeDto> GetRateWeightRangeByIdAsync(int id);

        // دریافت لیست مناطق قیمت‌گذاری
        Task<List<RateZoneDto>> GetRateZonesAsync();

        Task<SelectList> SelectList_ConsigmentNatureAsync();
        // دریافت لیست ماهیت‌های محموله
        Task<List<ConsignmentNatureDto>> GetConsignmentNaturesAsync();
        Task<clsResult> AddConsigmentNatureAsync(ConsignmentNatureDto dto);
        Task<clsResult> UpdateConsigmentNatureAsync(ConsignmentNatureDto dto);
        Task<clsResult> DeleteConsigmentNatureAsync(int id);

        // دریافت لیست آیتم‌های هزینه
        Task<List<CostItemDto>> GetCostItemsAsync();

        // افزودن یک رنج وزنی جدید
        Task<clsResult> AddRateWeightRangeAsync(RateWeightRangeDto dto);

        // بروزرسانی یک رنج وزنی
        Task<clsResult> UpdateRateWeightRangeAsync(RateWeightRangeDto dto);

        // حذف یک رنج وزنی
        Task<clsResult> DeleteRateWeightRangeAsync(int id);

        Task<CostItemDto> GetCostItemByIdAsync(int id);

        // افزودن یک آیتم هزینه جدید
        Task<clsResult> AddCostItemAsync(CostItemDto dto);

        // بروزرسانی یک آیتم هزینه
        Task<clsResult> UpdateCostItemAsync(CostItemDto dto);

        // حذف یک آیتم هزینه
        Task<clsResult> DeleteCostItemAsync(int id);

        Task<RateImpactTypeDto> GetRateImpactTypeByIdAsync(short id);
        Task<RateZoneDto> GetRateZoneByIdAsync(int id);
        Task<ConsignmentNatureDto> GetConsignmentNatureByIdAsync(short id);

    }
}