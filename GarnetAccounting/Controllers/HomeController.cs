using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.AvaRasta.Interfaces;
using GarnetAccounting.Areas.AvaRasta.ViewModels;
using GarnetAccounting.Areas.DataTransfer.DataTransferInterfaces;
using GarnetAccounting.Areas.DataTransfer.Dto;
using GarnetAccounting.Areas.Projects.ProjectInterfaces;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Services;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.CommercialViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GarnetAccounting.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAppIdentityUserManager _userManager;
        private readonly IGeneralService _gs;
        private readonly ICostomerService _customerService;
        private readonly IKPDataTransferService _oldSystemData;
        private readonly IAccountingReportService _accReport;
        private readonly IAccCodingService _coding;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IAccGetBaseDataService _baseData;
        private readonly UserContextService _contextService;
        private readonly IAccSettingService _accSettings;
        private readonly IUISettingsService _ui;
        private readonly IConProjectService _project;

        public HomeController(IAppIdentityUserManager userManager
            , IGeneralService generalService
            , ICostomerService costomerService
            , IKPDataTransferService dataTranferService
            , IAccountingReportService accReport
            , IAccCodingService coding
            , IDataProtectionProvider dataProtectionProvider
            , IAccGetBaseDataService baseDataService
            , UserContextService userContextService,
               IAccSettingService accSettings
            , IUISettingsService uiSetting
            , IConProjectService project)
        {
            _userManager = userManager;
            _gs = generalService;
            _customerService = costomerService;
            _oldSystemData = dataTranferService;
            _accReport = accReport;
            _coding = coding;
            _dataProtectionProvider = dataProtectionProvider;
            _baseData = baseDataService;
            _contextService = userContextService;
            _accSettings = accSettings;
            _ui = uiSetting;
            _project = project;
        }

        public async Task<IActionResult> Index()
        {
            if (_contextService.DepartmentCode == 101)
            {
                return RedirectToAction("Index", "Kp", new { Area = "Representatives" });
            }

            HomePageViewModel model = new();
            model.BillsMonitor = new VmBillOfLandingMonitor();
            model.CustomerInfo = new VmCustomer();
            model.SellerDashboardData = new VmSellerDashboard();
            model.DocumentsInfo = new Areas.Accounting.Dto.DocumentsInfo();
            int? cusId = _contextService.CustomerId;
            if (_contextService.CustomerId == null)
                cusId = await _userManager.GetCustomerIdByUsername(User.Identity.Name);

            if (cusId.HasValue)
            {
                if (User.IsInRole("CustomerOwner"))
                    ViewBag.Sellers = await _gs.SelectList_GetCustomerSellersAsync(cusId.Value);
                else
                    ViewBag.Sellers = await _gs.SelectList_GetUserSellersAsync(User.Identity.Name);

                long? sellerId = _contextService.SellerId;
                int? periodId = _contextService.PeriodId;

                ViewBag.ActiveSellerId = sellerId;

                if (sellerId.HasValue)
                {
                    ViewBag.FinancePeriods = await _gs.SelectList_GetSellerPeriodsAsync(sellerId.Value);
                    ViewBag.ActiveSellerPeriod = periodId;

                    ViewBag.Kols = await _baseData.SelectList_UsedKolsByTafsilAsync(sellerId.Value, null);
                    ViewBag.Moeins = await _baseData.SelectListUsedMoeinsByKolsAsync(sellerId.Value, null);
                    ViewBag.Tafsils = await _baseData.SelectList_UsageTafsilsAsync(sellerId.Value);
                    ViewBag.Tafsils5 = await _baseData.SelectList_UsageTafsils5Async(sellerId.Value);
                    ViewBag.Tafsils6 = await _baseData.SelectList_UsageTafsils6Async(sellerId.Value);
                    ViewBag.TafsilGroups = await _baseData.SelectList_TafsilGroupAsync(sellerId.Value);
                    ViewBag.Projects = await _project.SelectList_ProjectsAsync(sellerId.Value);
                }

                model.CustomerInfo = await _customerService.GetVmCustomerByIdAsync(cusId.Value);
                if (sellerId.HasValue)
                    model.BillsMonitor = await _oldSystemData.BillsMonitorAsync(sellerId.Value);

                if (periodId.HasValue && sellerId.HasValue)
                    model.DocumentsInfo = await _accReport.GetDocumentsInfoAsync(sellerId.Value, periodId.Value);
            }
            return View(model);
        }

        public async Task<IActionResult> Dashboard_Accounting()
        {
            HomePageViewModel model = new();
            model.SellerDashboardData = new VmSellerDashboard();

            int? cusId = await _userManager.GetCustomerIdByUsername(User.Identity.Name);
            if (User.IsInRole("CustomerOwner") || User.IsInRole("CustomerUser"))
            {
                if (cusId.HasValue)
                {
                    if (User.IsInRole("CustomerOwner"))
                        ViewBag.Sellers = await _gs.SelectList_GetCustomerSellersAsync(cusId.Value);
                    else
                        ViewBag.Sellers = await _gs.SelectList_GetUserSellersAsync(User.Identity.Name);

                    long? sellerId = _contextService.SellerId;
                    ViewBag.ActiveSellerId = sellerId;

                    if (sellerId.HasValue)
                    {
                        ViewBag.FinancePeriods = await _gs.SelectList_GetSellerPeriodsAsync(sellerId.Value);
                        ViewBag.ActiveSellerPeriod = _contextService.PeriodId;
                    }

                    model.CustomerInfo = await _customerService.GetVmCustomerByIdAsync(cusId.Value);
                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> SettActiveSeller()
        {
            string currentUser = User.Identity.Name;
            int? cusId = await _userManager.GetCustomerIdByUsername(currentUser);
            if (cusId.HasValue)
            {
                if (User.IsInRole("CustomerOwner"))
                    ViewBag.Sellers = await _gs.SelectList_GetCustomerSellersAsync(cusId.Value);
                else
                    ViewBag.Sellers = await _gs.SelectList_GetUserSellersAsync(currentUser);

                long? activeSellerId = await _gs.GetActiveSellerIdAsync(currentUser);
                ViewBag.ActiveSellerId = activeSellerId;

                if (activeSellerId != null && activeSellerId > 0)
                {
                    ViewBag.FinancePeriods = await _gs.SelectList_GetSellerPeriodsAsync(activeSellerId.Value);
                    ViewBag.ActiveSellerPeriod = await _gs.GetActiveUserFinancePeriodIdAsync(currentUser);
                }
            }
            return PartialView("_SettActiveSeller");
        }
        public async Task<IActionResult> GetSellerPeriods(long? sellerId)
        {
            if (sellerId == null)
            {
                return Json(new List<FinancePeriodsDto>());
            }
            var periods = await _gs.GetSellerPeriodsAsync(sellerId.Value);
            return Json(periods);
        }

        [HttpPost]
        public async Task<IActionResult> SettActiveSeller(Int64 sellerId)
        {
            int? customerId = await _userManager.GetCustomerIdByUsername(User.Identity.Name);
            var sett = await _gs.GetUserSettingAsync(User.Identity.Name, sellerId, customerId);

            var protector = _dataProtectionProvider.CreateProtector("CookieProtection");
            Response.Cookies.Delete("currentSeller");
            Response.Cookies.Delete("currentPeriod");
            if (sett.ActiveSellerPeriod.HasValue && sett.ActiveSellerId.HasValue)
            {
                // رمزنگاری مقادیر کوکی‌ها
                var encryptedSeller = protector.Protect(sett.ActiveSellerId.Value.ToString());
                var encryptedPeriod = protector.Protect(sett.ActiveSellerPeriod.Value.ToString());

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    Expires = DateTime.Now.AddDays(1)
                };

                // نوشتن کوکی‌های رمزنگاری‌شده
                Response.Cookies.Append("currentSeller", encryptedSeller, cookieOptions);
                Response.Cookies.Append("currentPeriod", encryptedPeriod, cookieOptions);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetAndGetActiveSeller(long sellerId, int? periodId)
        {
            int? customerId = await _userManager.GetCustomerIdByUsername(User.Identity.Name);
            var sett = await _gs.SetAndGetUserSettingAsync(User.Identity.Name, sellerId, customerId, periodId);
            Response.Cookies.Delete("currentSeller");
            Response.Cookies.Delete("currentPeriod");
            Response.Cookies.Delete("currentCustomer");
            Response.Cookies.Delete("periodStartDate");
            Response.Cookies.Delete("periodEndDate");
            if (sett != null)
            {
                // ایجاد محافظ برای رمزنگاری
                var protector = _dataProtectionProvider.CreateProtector("CookieProtection");
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(1)
                };

                // تنظیم و به‌روزرسانی کوکی‌ها و UserContextService
                if (sett.ActiveSellerId.HasValue)
                {
                    _contextService.SetSellerId(sett.ActiveSellerId.Value);
                    Response.Cookies.Append("currentSeller", protector.Protect(sett.ActiveSellerId.Value.ToString()), cookieOptions);
                    var accSett = await _accSettings.GetSettingAsync(sett.ActiveSellerId.Value);
                }

                if (sett.ActiveSellerPeriod.HasValue)
                {
                    _contextService.SetPeriodId(sett.ActiveSellerPeriod.Value);
                    Response.Cookies.Append("currentPeriod", protector.Protect(sett.ActiveSellerPeriod.Value.ToString()), cookieOptions);
                }

                if (customerId.HasValue)
                {
                    _contextService.SetCustomerId(customerId.Value);
                    Response.Cookies.Append("currentCustomer", protector.Protect(customerId.Value.ToString()), cookieOptions);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SettActivePeriod(int periodId)
        {
            int? customerId = await _userManager.GetCustomerIdByUsername(User.Identity.Name);

            var sett = await _gs.SetUserActivePeriodAsync(User.Identity.Name, periodId, customerId);
            return RedirectToAction("Index");
        }
        public IActionResult SendSmsSupport()
        {
            return PartialView("_SendSmsSupport");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MessageSender(string message)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi("36704B2F6550656D77635667653046524173494C4265315A69766848687352685375714F38656F486D366B3D");
            var resu = api.Send("09161114954", "2000500666", "تست یام انجام شد لغو11");
            var result = api.VerifyLookup("09161114954", "25987", "Avalogincode");
            return View();
        }

        [HttpGet]
        public IActionResult KeepSessionAlive()
        {
            return NoContent();
        }

        public async Task<IActionResult> uitoggle(int id)
        {
            var ui = await _ui.ThemeTogglerAsync(id);
            string jsonUi = JsonConvert.SerializeObject(ui);
            return Json(jsonUi);
        }
    }
}