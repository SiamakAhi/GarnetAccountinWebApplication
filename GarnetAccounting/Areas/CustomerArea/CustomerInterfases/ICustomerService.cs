using Microsoft.AspNetCore.Mvc.Rendering;
using GarnetAccounting.Areas.CustomerArea.Dto;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.CommercialViewModel;

namespace GarnetAccounting.Areas.CustomerArea.CustomerInterfases
{
    public interface ICustomerService
    {
        Task<List<VmCustomerUsers>> GetCustomerUsersAsync(int? CustomerId);
        Task<int?> GetCustomerIdByUsernameAsync(string UserName);
        int? GetCustomerIdByUsername(string UserName);
        Task<SelectList> Selectlist_CustomerSellersAsync(string UserName);
        Task<ResultDto> CreateCustomerUserAsync(AddCustomerUserDto dto);
        Task<UpdatePermissionDto> GetPermissionInfoAsync(string userName);
        Task<ResultDto> UpdateUserSettingAsync(userSettingDto dto);
        Task<ResultDto> AddSellerToUserAsync(UserSellerDto dto);
        Task<ResultDto> RemoveSellerFromUserAsync(Int64 Id);
        Task<ResultDto> DelCustomerUserAsync(string userName);
    }
}
