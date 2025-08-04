using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.DataProtection;

namespace GarnetAccounting.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserContextMiddleware(RequestDelegate next, IDataProtectionProvider dataProtectionProvider, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _dataProtectionProvider = dataProtectionProvider;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context, UserContextService userContext)
        {
            var protector = _dataProtectionProvider.CreateProtector("CookieProtection");

            // بررسی اینکه آیا کاربر وارد شده است
            if (context.User.Identity?.IsAuthenticated ?? false)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {

                    var userSettingsService = scope.ServiceProvider.GetRequiredService<IGeneralService>(); // دریافت سرویس Scoped
                    var periodData = scope.ServiceProvider.GetRequiredService<IAccCodingService>();
                    var BranchService = scope.ServiceProvider.GetRequiredService<IBranchUserService>();

                    var userManager = scope.ServiceProvider.GetRequiredService<IAppIdentityUserManager>();
                    var user = userManager.UserInfo(context.User.Identity.Name);

                    // خواندن کوکی‌ها
                    var encryptedSeller = context.Request.Cookies["currentSeller"];
                    var encryptedPeriod = context.Request.Cookies["currentPeriod"];
                    var encryptedCustomer = context.Request.Cookies["currentCustomer"];
                    string? periodStartDate = context.Request.Cookies["periodStartDate"];
                    string? periodEndtDate = context.Request.Cookies["periodEndDate"];

                    // ذخیره در کوکی
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        IsEssential = true,
                        Expires = DateTime.Now.AddDays(1)
                    };
                    try
                    {
                        if (!string.IsNullOrEmpty(encryptedSeller))
                        {
                            userContext.SellerId = long.Parse(protector.Unprotect(encryptedSeller));
                        }
                        else
                        {
                            var userSettings = await userSettingsService.UserSettingAsync(context.User.Identity.Name);
                            if (userSettings != null && userSettings.ActiveSellerId.HasValue)
                            {
                                userContext.SellerId = userSettings.ActiveSellerId;

                                // ذخیره در کوکی
                                context.Response.Cookies.Append("currentSeller", protector.Protect(userContext.SellerId.Value.ToString()), cookieOptions);
                            }
                        }

                        if (!string.IsNullOrEmpty(encryptedPeriod))
                        {
                            userContext.PeriodId = int.Parse(protector.Unprotect(encryptedPeriod));

                        }
                        else
                        {
                            var userSettings = await userSettingsService.UserSettingAsync(context.User.Identity.Name);
                            if (userSettings != null && userSettings.ActiveSellerPeriod.HasValue)
                            {
                                userContext.PeriodId = userSettings.ActiveSellerPeriod;
                                context.Response.Cookies.Append("currentPeriod", protector.Protect(userContext.PeriodId.Value.ToString()), cookieOptions);

                                //start and end date

                            }
                        }
                        if (!string.IsNullOrEmpty(periodStartDate))
                        {
                            userContext.FinancPeriodStartDate = periodStartDate;
                            userContext.FinancPeriodEndDate = periodEndtDate;
                        }
                        else
                        {
                            if (userContext.PeriodId.HasValue)
                            {
                                var priod = await periodData.GetFinanceDtoAsync(userContext.PeriodId.Value);
                                context.Response.Cookies.Append("periodStartDate", priod.StartDate.LatinToPersian(), cookieOptions);
                                context.Response.Cookies.Append("periodEndDate", priod.EndDate.LatinToPersian(), cookieOptions);

                            }
                        }

                        if (!string.IsNullOrEmpty(encryptedCustomer))
                        {
                            userContext.CustomerId = int.Parse(protector.Unprotect(encryptedCustomer));
                        }
                        else
                        {
                            var userSettings = await userSettingsService.UserSettingAsync(context.User.Identity.Name);
                            if (userSettings != null && userSettings.CustomerId.HasValue)
                            {
                                userContext.CustomerId = userSettings.CustomerId;
                                // ذخیره در کوکی
                                context.Response.Cookies.Append("currentCustomer", protector.Protect(userContext.CustomerId.Value.ToString()), cookieOptions);
                            }
                        }
                        //
                        if (user.DepartmentCode == 101)
                        {
                            //Branch User Check

                            var departmentCode = context.Request.Cookies["department"];
                            var bId = context.Request.Cookies["bi"];
                            //
                            if (departmentCode == null)
                            {
                                var userSettings = await userSettingsService.UserSettingAsync(context.User.Identity.Name);
                                var branchUser = await BranchService.GetBUserAsync(user.Id);

                                if (userSettings.DepartmentCode == 0 || userSettings.DepartmentCode == null)
                                {
                                    userSettings.DepartmentCode = 101;
                                    userSettings.BeranchId = branchUser.BranchId;
                                    userSettings = await userSettingsService.UpdateUserSettingAsync(userSettings);
                                }
                                context.Response.Cookies.Append("department", user.DepartmentCode.ToString(), cookieOptions);

                                if (!string.IsNullOrEmpty(bId))
                                {
                                    userContext.BranchId = Guid.Parse(bId);
                                }
                                else
                                {
                                    context.Response.Cookies.Append("bi", branchUser.BranchId.ToString(), cookieOptions);
                                }
                            }
                            else
                            {
                                userContext.DepartmentCode = short.Parse(departmentCode);
                                userContext.BranchId = Guid.Parse(bId);

                            }
                        }

                    }
                    catch
                    {
                        // در صورت بروز خطا، مقادیر را به null تنظیم کنید
                        userContext.SellerId = null;
                        userContext.PeriodId = null;
                        userContext.CustomerId = null;
                        userContext.FinancPeriodStartDate = null;
                        userContext.FinancPeriodEndDate = null;
                        userContext.DepartmentCode = null;
                        userContext.BranchId = null;

                    }
                }
            }

            // ادامه‌ی درخواست
            await _next(context);
        }
    }
}
