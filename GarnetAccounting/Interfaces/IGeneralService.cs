using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.ViewModels.CommercialViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Interfaces
{
    public interface IGeneralService
    {
        Task<userSettingDto> GetUserSettingAsync(string username, Int64 SellerId, int? customerId);
        Task<userSettingDto> SetAndGetUserSettingAsync(string username, long sellerId, int? customerId, int? periodId);
        Task<userSettingDto> SetUserActivePeriodAsync(string username, int periodId, int? customerId);
        Task<userSettingDto> UserSettingAsync(string username);
        Task<SelectList> SelectList_GetCustomerSellersAsync(int customerId);
        Task<SelectList> SelectList_GetSellerPeriodsAsync(Int64 SellerId);
        Task<List<FinancePeriodsDto>> GetSellerPeriodsAsync(Int64 SellerId);
        Task<SelectList> SelectList_GetUserSellersAsync(string userName);
        Task<Int64?> GetActiveSellerIdAsync(string userName);
        Task<string> ActiveSellerName(string userName);
        Task<int?> GetActiveUserFinancePeriodIdAsync(string username);
        Task<userSettingDto> GetUserSettingAsync(string username);
        Task<userSettingDto> UpdateUserSettingAsync(userSettingDto dto);
        Task<Acc_FinancialPeriod?> GetFinancialPeriodAsync(int id);
        Task<SelectList?> SelectList_NextFinancialPeriodsAsync(int currentPeriodId);
    }
}
