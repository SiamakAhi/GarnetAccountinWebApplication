using GarnetAccounting.Areas.Support.Dtos;
using GarnetAccounting.ViewModels.IdentityViewModels;

namespace GarnetAccounting.Areas.Support.SuportInterfaces
{
    public interface ISupportService
    {
        Task<SmsResultDto> SendSmsToSupportAsync(string message);
        Task<VmUserInfo>? GetUserInfoAsync(string userName);
    }
}
