using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Models;
using GarnetAccounting.Models.Commercial;
using GarnetAccounting.ViewModels.CommercialViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly AppDbContext _db;
        private readonly IAppIdentityUserManager _userManager;

        public GeneralService(AppDbContext db, IAppIdentityUserManager userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<userSettingDto> GetUserSettingAsync(string username, long sellerId, int? customerId)
        {
            var setting = _db.UserSettings.FirstOrDefault(n => n.UserName == username);

            if (setting == null)
            {
                setting = new UserSetting
                {
                    UserName = username,
                    ActiveSellerId = sellerId,
                    CustomerId = customerId
                };

                await _db.UserSettings.AddAsync(setting);
            }
            else
            {
                setting.ActiveSellerId = sellerId;
                _db.UserSettings.Update(setting);
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا هنگام ذخیره تنظیمات کاربر: {ex.Message}");
                throw;
            }

            // ایجاد و بازگشت DTO
            return new userSettingDto
            {
                Id = setting.Id,
                UserName = setting.UserName,
                ActiveSellerId = setting.ActiveSellerId,
                CustomerId = setting.CustomerId,
                UserId = setting.UserId,
                ActiveSellerPeriod = setting.ActiveFinancePeriodId
            };
        }
        public async Task<userSettingDto> SetAndGetUserSettingAsync(string username, long sellerId, int? customerId, int? periodId)
        {
            var setting = _db.UserSettings.FirstOrDefault(n => n.UserName == username);

            if (setting == null)
            {
                setting = new UserSetting
                {
                    UserName = username,
                    ActiveSellerId = sellerId,
                    CustomerId = customerId,
                    ActiveFinancePeriodId = periodId,
                };

                await _db.UserSettings.AddAsync(setting);
            }
            else
            {
                setting.ActiveSellerId = sellerId;
                setting.ActiveFinancePeriodId = periodId;
                _db.UserSettings.Update(setting);
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا هنگام ذخیره تنظیمات کاربر: {ex.Message}");
                throw;
            }

            // ایجاد و بازگشت DTO
            return new userSettingDto
            {
                Id = setting.Id,
                UserName = setting.UserName,
                ActiveSellerId = setting.ActiveSellerId,
                CustomerId = setting.CustomerId,
                UserId = setting.UserId,
                ActiveSellerPeriod = setting.ActiveFinancePeriodId
            };
        }

        public async Task<userSettingDto> SetUserActivePeriodAsync(string username, int periodId, int? customerId)
        {
            var setting = _db.UserSettings.FirstOrDefault(n => n.UserName == username);

            if (setting == null)
            {
                setting = new UserSetting
                {
                    UserName = username,
                    CustomerId = customerId,
                    ActiveFinancePeriodId = periodId,

                };

                await _db.UserSettings.AddAsync(setting);
            }
            else
            {
                setting.ActiveFinancePeriodId = periodId;
                _db.UserSettings.Update(setting);
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا هنگام ذخیره تنظیمات کاربر: {ex.Message}");
                throw;
            }

            // ایجاد و بازگشت DTO
            return new userSettingDto
            {
                Id = setting.Id,
                UserName = setting.UserName,
                ActiveSellerId = setting.ActiveSellerId,
                CustomerId = setting.CustomerId,
                UserId = setting.UserId,
                ActiveSellerPeriod = setting.ActiveFinancePeriodId
            };
        }

        public async Task<userSettingDto> UserSettingAsync(string username)
        {
            var setting = await _db.UserSettings.FirstOrDefaultAsync(n => n.UserName == username);
            if (setting == null)
            {
                var user = _userManager.Users.SingleOrDefault(n => n.UserName == username);
                var sett = new UserSetting
                {
                    UserName = username,
                    ActiveSellerId = null,
                    ActiveFinancePeriodId = null,
                    AllowBuyerManagement = true,
                    AllowSaleManagement = true,
                    AllowSellerManagement = false,
                    AllowStuffManagement = true,
                    AllowUserManagement = false,
                    CustomerId = user.CustomerId,
                    DepartmentCode = user.DepartmentCode,
                    UserId = user.Id,
                };
                bool isOwner = await _userManager.IsInRoleAsync(user, "CustomerOwner");
                if (isOwner)
                {
                    sett.AllowUserManagement = true;
                    sett.AllowSellerManagement = true;
                }
                _db.UserSettings.Add(sett);
                if (Convert.ToBoolean((await _db.SaveChangesAsync())))
                {
                    setting = sett;
                }
            }

            return new userSettingDto
            {
                UserName = setting.UserName,
                ActiveSellerId = setting.ActiveSellerId,
                ActiveSellerPeriod = setting.ActiveFinancePeriodId,
                CustomerId = setting.CustomerId,
                Id = setting.Id,
                UserId = setting.UserId,
                AllowBuyerManagement = setting.AllowBuyerManagement,
                AllowSaleManagement = setting.AllowSaleManagement,
                AllowSellerManagement = setting.AllowSellerManagement,
                AllowStuffManagement = setting.AllowStuffManagement,
                AllowUserManagement = setting.AllowUserManagement,
                DepartmentCode = setting.DepartmentCode,
                BeranchId = setting.BranchId
            };
        }
        public async Task<SelectList> SelectList_GetCustomerSellersAsync(int customerId)
        {
            var data = await _db.parties
                .Where(n => n.CustomerId == customerId && n.Role == 1)
                .Select(n => new { id = n.Id, name = n.Name })
                .OrderBy(n => n.name).ToListAsync();

            return new SelectList(data, "id", "name");
        }
        public async Task<SelectList> SelectList_GetSellerPeriodsAsync(Int64 SellerId)
        {
            var data = await _db.Acc_FinancialPeriods
                .Where(n => n.SellerId == SellerId)
                .Select(n => new { id = n.Id, name = n.Name })
                .OrderBy(n => n.name).ToListAsync();

            return new SelectList(data, "id", "name");
        }
        public async Task<List<FinancePeriodsDto>> GetSellerPeriodsAsync(Int64 SellerId)
        {
            var data = await _db.Acc_FinancialPeriods
                .Where(n => n.SellerId == SellerId)
                .Select(n => new FinancePeriodsDto
                {
                    DefualtVatRate = n.DefualtVatRate,
                    Id = n.Id,
                    Name = n.Name,
                    SellerId = n.SellerId,
                    Description = n.Description,
                    EndDate = n.EndDate,
                    StartDate = n.StartDate,
                })
                .OrderBy(n => n.Name).ToListAsync();

            return data;
        }
        public async Task<SelectList> SelectList_GetUserSellersAsync(string userName)
        {
            Int64[] sellersId = _db.UserSellers.Where(n => userName == n.Username).Select(n => n.SellerId).ToArray();

            var data = await _db.parties.Where(n => sellersId.Contains(n.Id))
              .Select(s => new { id = s.Id, name = s.Name }).ToListAsync();

            return new SelectList(data, "id", "name");
        }

        public async Task<Int64?> GetActiveSellerIdAsync(string userName)
        {
            var setting = await _db.UserSettings.FirstOrDefaultAsync(n => n.UserName == userName);

            return setting?.ActiveSellerId;
        }

        public async Task<string> ActiveSellerName(string userName)
        {
            try
            {
                var sett = await _db.UserSettings.FirstOrDefaultAsync(n => n.UserName == userName);
                if (sett == null || sett.ActiveSellerId == null) return "تعریف نشده";
                var party = await _db.parties.FindAsync(sett.ActiveSellerId.Value);
                return party.Name;
            }
            catch (Exception)
            {
                return "نا مشخص";
            }

        }

        public async Task<int?> GetActiveUserFinancePeriodIdAsync(string username)
        {
            var sett = await _db.UserSettings.FirstOrDefaultAsync(n => n.UserName == username);
            if (sett == null) return null;
            return sett.ActiveFinancePeriodId;
        }

        public async Task<userSettingDto> UpdateUserSettingAsync(userSettingDto dto)
        {
            var set = await _db.UserSettings.Where(n => n.UserName == dto.UserName).FirstOrDefaultAsync();
            if (set == null)
                return null;

            set.UserName = dto.UserName;
            set.UserId = dto.UserId;
            set.ActiveFinancePeriodId = dto.ActiveSellerPeriod;
            set.ActiveSellerId = dto.ActiveSellerId;
            set.AllowSellerManagement = dto.AllowSellerManagement;
            set.AllowSaleManagement = dto.AllowSaleManagement;
            set.AllowStuffManagement = dto.AllowStuffManagement;
            set.AllowBuyerManagement = dto.AllowBuyerManagement;
            set.AllowUserManagement = dto.AllowUserManagement;
            set.BranchId = dto.BeranchId;
            set.DepartmentCode = dto.DepartmentCode;

            _db.UserSettings.Update(set);
            await _db.SaveChangesAsync();
            return dto;
        }

        public async Task<userSettingDto> GetUserSettingAsync(string username)
        {
            var setting = await _db.UserSettings.Where(n => n.UserName == username).FirstOrDefaultAsync();
            if (setting == null)
                return null;
            string periodName = "";
            string sellerName = "";
            if (setting.ActiveFinancePeriodId.HasValue)
            {
                var period = await _db.Acc_FinancialPeriods.FindAsync(setting.ActiveFinancePeriodId.Value);
                periodName = period?.Name;
            }
            if (setting.ActiveSellerId.HasValue)
            {
                var seller = await _db.parties.FindAsync(setting.ActiveSellerId.Value);
                sellerName = seller?.Name;
            }


            return new userSettingDto
            {
                UserName = setting.UserName,
                ActiveSellerId = setting.ActiveSellerId,
                ActiveSellerName = sellerName,
                ActiveSellerPeriod = setting.ActiveFinancePeriodId,
                ActiveSellerPeriodName = periodName,
                CustomerId = setting.CustomerId,
                Id = setting.Id,
                UserId = setting.UserId,
                AllowBuyerManagement = setting.AllowBuyerManagement,
                AllowSaleManagement = setting.AllowSaleManagement,
                AllowSellerManagement = setting.AllowSellerManagement,
                AllowStuffManagement = setting.AllowStuffManagement,
                AllowUserManagement = setting.AllowUserManagement,
            };
        }

        public async Task<Acc_FinancialPeriod?> GetFinancialPeriodAsync(int id)
        {
            var data = await _db.Acc_FinancialPeriods.FindAsync(id);
            return data;
        }

        public async Task<SelectList?> SelectList_NextFinancialPeriodsAsync(int currentPeriodId)
        {
            var currentPeriod = await _db.Acc_FinancialPeriods.FindAsync(currentPeriodId);
            if (currentPeriod == null)
                return null;

            var nextPeriods = await _db.Acc_FinancialPeriods
                .Where(n => n.SellerId == currentPeriod.SellerId && n.StartDate > currentPeriod.EndDate)
                .OrderBy(n => n.StartDate)
                .ToListAsync();

            return new SelectList(nextPeriods, "Id", "Name");
        }
    }
}
