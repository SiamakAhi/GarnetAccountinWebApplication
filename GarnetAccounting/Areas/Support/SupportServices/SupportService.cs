using Microsoft.EntityFrameworkCore;
using GarnetAccounting.Areas.Support.Dtos;
using GarnetAccounting.Areas.Support.SuportInterfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Models;
using GarnetAccounting.ViewModels.IdentityViewModels;

namespace GarnetAccounting.Areas.Support.SupportServices
{
    public class SupportService : ISupportService
    {
        private readonly ISmsSenderService _smsSender;
        private readonly IAppIdentityUserManager _userManager;
        private readonly AppDbContext _db;

        public SupportService(ISmsSenderService smsSender, AppDbContext DbContext, IAppIdentityUserManager userManager)
        {
            _smsSender = smsSender;
            _db = DbContext;
            _userManager = userManager;
        }

        public async Task<SmsResultDto> SendSmsToSupportAsync(string message)
        {
            var result = await _smsSender.Send_KavenegarAsync("09161114954", message);
            return new() { Status = result.Status, StatusText = result.StatusText };
        }
        public async Task<VmUserInfo>? GetUserInfoAsync(string userName)
        {
            var user = _userManager.UserInfo(userName);

            if (user == null)
                return null;

            VmUserInfo info = new()
            {
                UserName = userName,
                CustomerId = user.CustomerId,
                UserFullName = user.FirstName + " " + user.LastName,
            };
            if (user.CustomerId != null && user.CustomerId > 0)
            {
                var cus = await _db.Customers.SingleOrDefaultAsync(n => n.Id == user.CustomerId.Value);
                info.CustomerName = cus.Title;
            }
            return info;
        }
    }
}
