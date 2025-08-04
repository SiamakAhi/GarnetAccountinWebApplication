using GarnetAccounting.Areas.CustomerArea.CustomerInterfases;
using GarnetAccounting.Areas.CustomerArea.Dto;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Models;
using GarnetAccounting.Models.Commercial;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.CommercialViewModel;
using GarnetAccounting.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.CustomerArea.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IGeneralService _gs;
        private readonly IAppIdentityUserManager _userManager;

        private readonly AppDbContext _db;
        public CustomerService(IGeneralService generalService
            , IAppIdentityUserManager userManager
            , AppDbContext DbContext)
        {
            _gs = generalService;
            _userManager = userManager;
            _db = DbContext;
        }

        public async Task<List<VmCustomerUsers>> GetCustomerUsersAsync(int? CustomerId)
        {
            List<VmCustomerUsers> lst = new List<VmCustomerUsers>();
            if (CustomerId != null)
            {

                var users = await _userManager.Users.Where(n => n.CustomerId == CustomerId).ToListAsync();

                foreach (var usr in users)
                {
                    var sett = _db.UserSettings.FirstOrDefault(n => n.UserName == usr.UserName);
                    var setting = new VmCustomerUsers();
                    setting.UserId = usr.Id;
                    setting.UserName = usr.UserName;
                    setting.FullName = usr.FName + " " + usr.Family;
                    setting.CreationDate = usr.RegistrDate.LatinToPersian();
                    setting.phoneNumber = usr.Mobile;
                    setting.email = usr.Email;
                    setting.Image = usr.Avatar;
                    setting.UserRolesArry = await _userManager.userArrayRolesDescAsync(usr.UserName);
                    setting.IsActive = usr.IsActive;
                    setting.Gender = usr.Gender;

                    if (sett != null)
                    {
                        setting.AllowBuyerManagement = sett.AllowBuyerManagement;
                        setting.AllowSaleManagement = sett.AllowSaleManagement;
                        setting.AllowSellerManagement = sett.AllowSellerManagement;
                        setting.AllowStuffManagement = sett.AllowStuffManagement;
                        setting.AllowUserManagement = sett.AllowUserManagement;
                    }
                    lst.Add(setting);
                }
            }

            return lst;
        }

        public async Task<int?> GetCustomerIdByUsernameAsync(string UserName)
        {
            return await _userManager.GetCustomerIdByUsername(UserName);
        }
        public int? GetCustomerIdByUsername(string UserName)
        {
            return _userManager.GetCustomerIdByUsername(UserName).Result;
        }
        public async Task<SelectList> Selectlist_CustomerSellersAsync(string UserName)
        {
            int? customerId = await _userManager.GetCustomerIdByUsername(UserName);
            return await _gs.SelectList_GetCustomerSellersAsync(customerId.Value);
        }
        public async Task<ResultDto> CreateCustomerUserAsync(AddCustomerUserDto dto)
        {
            VmRegisterUser user = new VmRegisterUser();
            user.Gender = dto.Gender;
            user.UserName = dto.UserName;
            user.Password = dto.Password;
            user.ConfirmPassword = dto.ConfirmPassword;
            user.LName = dto.LName;
            user.FName = dto.FName;
            user.Birthday = DateTime.Now.AddYears(-25);
            user.email = dto.email;
            user.Mobile = dto.Mobile;
            user.customerId = dto.customerId;
            user.Roles = new string[] { "CustomerUser" };
            var addUserResult = await _userManager.AddCustomerOwnerAccount(user);

            if (addUserResult.Succeeded)
            {
                try
                {
                    var userselersData = _db.UserSellers.Where(n => n.Username == user.UserName).ToList();

                    UserSetting setting = new UserSetting();

                    if (dto.SellersId != null)
                    {
                        List<UserSeller> userSellers = new List<UserSeller>();
                        for (int i = 0; i < dto.SellersId.Count(); i++)
                        {
                            if (userselersData == null || !(userselersData.Any(n => n.Username == user.UserName && n.SellerId == dto.SellersId[i])))
                            {
                                userSellers.Add(new UserSeller()
                                {
                                    CustomerId = dto.customerId,
                                    SellerId = dto.SellersId[i],
                                    Username = user.UserName,
                                });
                            }

                        }

                        await _db.UserSellers.AddRangeAsync(userSellers);

                        setting.ActiveSellerId = userSellers.FirstOrDefault().SellerId;
                    }

                    setting.CustomerId = dto.customerId;
                    setting.AllowSaleManagement = dto.AllowSaleManagement;
                    setting.AllowStuffManagement = dto.AllowStuffManagement;
                    setting.AllowBuyerManagement = dto.AllowBuyerManagement;
                    setting.AllowSellerManagement = dto.AllowSellerManagement;
                    setting.AllowUserManagement = dto.AllowUserManagement;
                    setting.UserName = user.UserName;
                    setting.UserId = user.Id;

                    bool hasSetting = _db.UserSettings.Where(n => n.UserName == setting.UserName).Any();
                    if (!hasSetting)
                        _db.UserSettings.Add(setting);
                    try
                    {
                        await _db.SaveChangesAsync();
                        return new ResultDto { Success = true, Message = "کابر با موفقیت ایجاد شد" };
                    }
                    catch (Exception x)
                    {
                        var err = x.Message;
                        var User = await _userManager.FindByNameAsync(user.UserName);
                        var removeUserAsync = await _userManager.DeleteAsync(User);
                        return new ResultDto { Success = false, Message = "در انجام عملیات خطایی رخ داده است" };
                    }
                }
                catch
                {
                    var User = await _userManager.FindByNameAsync(user.UserName);
                    var removeUserAsync = await _userManager.DeleteAsync(User);
                }

            }
            else
            {
                ResultDto res = new ResultDto();
                foreach (var error in addUserResult.Errors)
                {
                    res.Message += "<br> - " + error.Description;
                }
                res.Success = false;
                return res;

            }
            return new ResultDto { Success = false, Message = "در انجام عملیات خطایی رخ داده است" };
        }
        public async Task<UpdatePermissionDto> GetPermissionInfoAsync(string userName)
        {
            UpdatePermissionDto model = new UpdatePermissionDto();
            model.UserSetting = await _gs.UserSettingAsync(userName);
            var sellersUser = await _db.UserSellers.Where(n => n.Username == userName).ToListAsync();
            Int64[] sellersId = sellersUser.Select(n => n.SellerId).ToArray();
            var sellersInfo = _db.parties.Where(n => sellersId.Contains(n.Id)).AsQueryable();

            model.UserSellers = new List<UserSellerDto>();
            foreach (var x in sellersUser)
            {
                var dto = new UserSellerDto();
                var seller = sellersInfo.FirstOrDefault(n => n.Id == x.SellerId);
                dto.SellerName = seller?.Name;
                dto.UserName = x.Username;
                dto.Id = x.Id;
                dto.SellerId = x.SellerId;
                dto.CustomerId = seller?.CustomerId;

                model.UserSellers.Add(dto);
            }

            return model;
        }
        public async Task<ResultDto> UpdateUserSettingAsync(userSettingDto dto)
        {
            var userSetting = await _db.UserSettings.FindAsync(dto.Id);
            if (userSetting == null)
            {
                return new ResultDto { Success = false, Message = "اطلاعات یافت نشد" };
            }
            userSetting.AllowStuffManagement = dto.AllowStuffManagement;
            userSetting.AllowSellerManagement = dto.AllowSellerManagement;
            userSetting.AllowBuyerManagement = dto.AllowBuyerManagement;
            userSetting.AllowSaleManagement = dto.AllowSaleManagement;
            userSetting.AllowUserManagement = dto.AllowUserManagement;
            _db.UserSettings.Update(userSetting);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                return new ResultDto
                {
                    Success = true,
                    Message = "سطح دستری کاربر با موفقیت بروزرسانی شد."
                };
            }
            return new ResultDto { Success = false, Message = "خطایی در ذخیره اطلاعات رخ داده است." };
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ResultDto> AddSellerToUserAsync(UserSellerDto dto)
        {
            bool IsDupplicate = await _db.UserSellers.AnyAsync(n => n.Username == dto.UserName && n.SellerId == dto.SellerId);
            if (IsDupplicate)
            {
                return new ResultDto { Success = false, Message = "مشتری موردنظر پیش از این به کاربر اختصاص داده شده است" };
            }
            UserSeller userSeller = new UserSeller
            {
                CustomerId = dto.CustomerId,
                SellerId = dto.SellerId,
                Username = dto.UserName,
            };
            _db.UserSellers.Add(userSeller);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                return new ResultDto
                {
                    Success = true,
                    Message = "دسترسی کاربر به فروشنده موردنظر با موفقیت انجام شد."
                };
            }
            return new ResultDto { Success = false, Message = "خطایی در ذخیره اطلاعات رخ داده است." };
        }
        public async Task<ResultDto> RemoveSellerFromUserAsync(Int64 Id)
        {
            var userSeller = await _db.UserSellers.FindAsync(Id);
            if (userSeller == null)
            {
                return new ResultDto { Success = false, Message = "اطلاعات یافت نشد" };
            }
            Int64? userActiveSeller = await _gs.GetActiveSellerIdAsync(userSeller.Username);
            var allUserSeller = await _db.UserSellers.Where(n => n.Username == userSeller.Username && n.SellerId == userSeller.SellerId).ToListAsync();
            if (userActiveSeller == userSeller.SellerId)
            {
                var userSetting = await _db.UserSettings.FirstOrDefaultAsync(x => x.UserName == userSeller.Username);
                if (userSetting != null)
                {
                    userSetting.ActiveSellerId = null;
                    _db.UserSettings.Update(userSetting);
                }
            }

            _db.UserSellers.RemoveRange(allUserSeller);

            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                return new ResultDto
                {
                    Success = true,
                    Message = "عملیات با موفقیت انجام شد"
                };
            }
            return new ResultDto { Success = false, Message = "خطایی در ذخیره اطلاعات رخ داده است." };
        }
        public async Task<ResultDto> DelCustomerUserAsync(string userName)
        {
            var user = await _userManager.Users.Where(n => n.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ResultDto { Success = false, Message = "کاربر موردنظر یافت نشد" };
            }

            var IdentityRes = await _userManager.DeleteAsync(user);
            if (IdentityRes.Succeeded)
            {
                var setting = await _db.UserSettings.SingleOrDefaultAsync(n => n.UserName == userName);
                if (setting != null)
                    _db.UserSettings.Remove(setting);

                var usersellers = _db.UserSellers.Where(n => n.Username == userName).ToList();
                if (usersellers.Any())
                    _db.UserSellers.RemoveRange(usersellers);
                var save = await _db.SaveChangesAsync();

                return new ResultDto
                {
                    Success = true,
                    Message = "حذف کاربر با موفقیت انجام شد"
                };
            }
            return new ResultDto { Success = false, Message = "خطایی در عملیات حذف کاربر رخ داده است." };
        }
    }
}
