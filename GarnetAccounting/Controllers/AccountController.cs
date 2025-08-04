using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Models.Identity;
using GarnetAccounting.Services;
using GarnetAccounting.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppIdentityUserManager _usermanager;
        private readonly IAppRoleManager _rolemanager;
        private readonly SignInManager<AppIdentityUser> _SignIn;
        private readonly IGeneralService _gs;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly UserContextService _contextService;
        private readonly IAccSettingService _accSettings;


        public AccountController(IAppIdentityUserManager usermanager
            , IAppRoleManager rolemanager
            , SignInManager<AppIdentityUser> signIn
            , IGeneralService generalService
            , IDataProtectionProvider dataProtectionProvider
            , UserContextService userContextService
            , IAccSettingService accSettings)
        {
            _usermanager = usermanager;
            _rolemanager = rolemanager;
            _SignIn = signIn;
            _gs = generalService;
            _dataProtectionProvider = dataProtectionProvider;
            _contextService = userContextService;
            _accSettings = accSettings;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? ReturnUrl = null)
        {
            // اگر کاربر قبلاً وارد شده است، به صفحه اصلی هدایت شود
            if (_SignIn.IsSignedIn(User))
            {
                // هدایت به آدرس ReturnUrl اگر مشخص شده است، در غیر این صورت به صفحه اصلی
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            // ذخیره ReturnUrl در ViewData برای استفاده در فرم لاگین
            ViewData["ReturnUrl"] = ReturnUrl;

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(VmLogin vm, string? ReturnUrl = null)
        {
            var protector = _dataProtectionProvider.CreateProtector("CookieProtection");
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Delete("currentSeller");
            Response.Cookies.Delete("currentPeriod");
            Response.Cookies.Delete("currentCustomer");
            Response.Cookies.Delete("periodStartDate");
            Response.Cookies.Delete("periodEndDate");
            Response.Cookies.Delete("department");
            Response.Cookies.Delete("bi");

            if (_SignIn.IsSignedIn(User))
            {
                await SetUserContextAndCookies(User.Identity.Name, protector, cookieOptions);
                long? sellerId = _contextService.SellerId;
                //دریافت تنظیمات حسابداری
                if (sellerId.HasValue)
                    await _accSettings.GetSettingAsync(sellerId.Value);

                    return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var result = await _SignIn.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    await SetUserContextAndCookies(User.Identity.Name, protector, cookieOptions);
                    long? sellerId = _contextService.SellerId;
                    //دریافت تنظیمات حسابداری
                    if (sellerId.HasValue)
                        await _accSettings.GetSettingAsync(sellerId.Value);
                    //if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    //{
                    //    return Redirect(ReturnUrl);
                    //}
                    var testname = User?.Identity.Name;

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "نام کاربری یا کلمه عبور اشتباه است");
            }

            ViewData["ReturnUrl"] = ReturnUrl;
            return View(vm);
        }

        private async Task SetUserContextAndCookies(string username, IDataProtector protector, CookieOptions cookieOptions)
        {
            // دریافت و تنظیم CustomerId
            int? cusId = await _usermanager.GetCustomerIdByUsername(username);
            Response.Cookies.Delete("currentCustomer");
            if (cusId.HasValue)
            {
                var encryptedCus = protector.Protect(cusId.Value.ToString());
                Response.Cookies.Append("currentCustomer", encryptedCus, cookieOptions);
                _contextService.SetCustomerId(cusId.Value); // تنظیم در UserContextService
            }

            // دریافت و تنظیم User Settings
            var userSetting = await _gs.UserSettingAsync(username);
            Response.Cookies.Delete("currentSeller");
            Response.Cookies.Delete("currentPeriod");
            Response.Cookies.Delete("currentCustomer");
            Response.Cookies.Delete("periodStartDate");
            Response.Cookies.Delete("periodEndDate");
            Response.Cookies.Delete("department");
            Response.Cookies.Delete("bi");

            if (userSetting != null)
            {
                long? currentSeller = userSetting.ActiveSellerId;
                int? currentPeriod = userSetting.ActiveSellerPeriod;
                Guid? beranchId = userSetting.BeranchId;
                short? departmentId = userSetting.DepartmentCode;

                if (currentSeller.HasValue)
                {
                    var encryptedSeller = protector.Protect(currentSeller.Value.ToString());
                    Response.Cookies.Append("currentSeller", encryptedSeller, cookieOptions);
                    _contextService.SetSellerId(currentSeller.Value); // تنظیم در UserContextService
                }

                if (currentPeriod.HasValue)
                {
                    var encryptedPeriod = protector.Protect(currentPeriod.Value.ToString());
                    Response.Cookies.Append("currentPeriod", encryptedPeriod, cookieOptions);
                    _contextService.SetPeriodId(currentPeriod.Value); // تنظیم در UserContextService
                }
                if (departmentId.HasValue)
                {
                    Response.Cookies.Append("department", departmentId.Value.ToString(), cookieOptions);
                    _contextService.SetDepartmentCode(departmentId.Value);
                }
                if (beranchId.HasValue)
                {
                    Response.Cookies.Append("bi", beranchId.ToString(), cookieOptions);
                    _contextService.SetBranchId(beranchId.Value);
                }
            }
        }


        public async Task<IActionResult> SignOut()
        {
            // حذف کوکی‌ها
            Response.Cookies.Delete("currentSeller");
            Response.Cookies.Delete("currentPeriod");
            Response.Cookies.Delete("currentCustomer");
            Response.Cookies.Delete("periodStartDate");
            Response.Cookies.Delete("periodEndDate");
            Response.Cookies.Delete("department");
            Response.Cookies.Delete("bi");
            await _SignIn.SignOutAsync();
            return RedirectToAction("Login", "Account", new { Area = "" });
        }
    }
}
