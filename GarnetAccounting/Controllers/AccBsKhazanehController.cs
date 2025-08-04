using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GarnetAccounting.Controllers
{
    public class AccBsKhazanehController : Controller
    {
        private readonly IAccCodingService _coding;
        private readonly IGeneralService _gs;
        private readonly IAccBaseInfoService _baseInfo;

        public AccBsKhazanehController(
            IAccBaseInfoService baseInfo
            , IAccCodingService CodingService
            , IGeneralService GeneralService)
        {
            _baseInfo = baseInfo;
            _coding = CodingService;
            _gs = GeneralService;
        }
        public async Task<IActionResult> Banks()
        {
            var model = new BanksViewModel();
            model.Banks = await _baseInfo.BanksAsync();
            return View(model);
        }

        public IActionResult AddBank()
        {
            return PartialView("_AddBank");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBank(BankDto dto)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            clsResult result = new clsResult();
            result.Success = false;
            if (!sellerId.HasValue)
            {
                result.Message = "فروشنده فعال یافت نشد";
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                result = await _baseInfo.AddBankAsync(dto);
                if (result.Success)
                    result.returnUrl = Request.Headers["Referer"].ToString();
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();

            foreach (var er in errors)
            {
                result.Message += "\n " + er.ErrorMessage;
            }
            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> CreateBankTafsil(string name, int groupId)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
                return Ok();

            AutoAddTafsilDto dto = new AutoAddTafsilDto();
            dto.Name = name;
            dto.GroupId = groupId;
            dto.SellerId = sellerId.Value;

            return PartialView("_CreateBankTafsil", dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTafsil(int id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
            {
                result.Message = "فروشنده فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            result = await _baseInfo.CreateTafsilForBankAsync(id, sellerId.Value);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
            }
            return Json(result.ToJsonResult());
        }

        //Bank Accounts
        public async Task<IActionResult> BankAccounts()
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
                return Ok();
            var accounts = await _baseInfo.BankAccountsAsync(sellerId.Value);
            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> AddBankAccount()
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);
            if (!sellerId.HasValue)
                return Ok();

            ViewBag.Banks = await _baseInfo.SelectList_BanksAsync();
            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(sellerId);

            return PartialView("_AddBankAccount");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBankAccount(BankAccountDto dto)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            clsResult result = new clsResult();
            result.Success = false;
            if (!sellerId.HasValue)
            {
                result.Message = "فروشنده فعال یافت نشد";
                return Json(result.ToJsonResult());
            }
            dto.SellerId = sellerId.Value;
            if (ModelState.IsValid)
            {
                result = await _baseInfo.AddBankAccountAsync(dto);
                if (result.Success)
                    result.returnUrl = Request.Headers["Referer"].ToString();
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();

            foreach (var er in errors)
            {
                result.Message += "\n " + er.ErrorMessage;
            }
            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBankAccount(int id)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            var dto = await _baseInfo.GetBankAccountDtoAsync(id);
            ViewBag.Banks = await _baseInfo.SelectList_BanksAsync();
            ViewBag.Moeins = await _coding.SelectList_MoeinsAsync(sellerId);
            return PartialView("_UpdateBankAccount", dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBankAccount(BankAccountDto dto)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            clsResult result = new clsResult();
            result.Success = false;
            if (!sellerId.HasValue)
            {
                result.Message = "فروشنده فعال یافت نشد";
                return Json(result.ToJsonResult());
            }
            dto.SellerId = sellerId.Value;
            if (ModelState.IsValid)
            {
                result = await _baseInfo.UpdateBankAccountAsync(dto);
                if (result.Success)
                    result.returnUrl = Request.Headers["Referer"].ToString();
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();

            foreach (var er in errors)
            {
                result.Message += "\n " + er.ErrorMessage;
            }
            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> SetAccountBankTafsil(string name, int bankId)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            var model = new BankAccountTafsilDto();
            model.SellerId = sellerId.Value;
            model.Name = name;
            model.IsPerson = false;
            model.intGroupsId = new int[] { 4 };
            model.BankAccountId = bankId;

            ViewBag.Title = "تعیین تفصیلی برای حساب " + name;
            ViewBag.Groups = await _coding.SelectList_TafsilGroupsAsync(sellerId.Value);
            ViewBag.Tafsils = await _coding.SelectList_TafsilsAsync(sellerId);
            return PartialView("_SetAccountBankTafsil", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccountBankTafsil(BankAccountTafsilDto dto)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            clsResult result = new clsResult();
            result.Success = false;
            if (!sellerId.HasValue)
            {
                result.Message = "فروشنده فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            if (dto.intGroupsId.Length == 0 || dto.intGroupsId == null)
            {
                dto.intGroupsId = new int[] { 4 };
            }
            if (!dto.BankAccountId.HasValue)
            {
                result.Message = "شناسه حساب بانکی موردنظر یافت نشد. مجددا تلاش کنید";
                return Json(result.ToJsonResult());
            }
            dto.strGroupsId = JsonConvert.SerializeObject(dto.intGroupsId);
            dto.SellerId = sellerId.Value;
            if (ModelState.IsValid)
            {
                result = await _coding.CreateAccountBankTafsilAsync(dto);
                if (result.Success)
                    result.returnUrl = Request.Headers["Referer"].ToString();
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();

            foreach (var er in errors)
            {
                result.Message += "\n " + er.ErrorMessage;
            }
            return Json(result.ToJsonResult());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetAccountBankTafsil(BankAccountTafsilDto dto)
        {
            long? sellerId = await _gs.GetActiveSellerIdAsync(User.Identity.Name);

            clsResult result = new clsResult();
            result.Success = false;

            if (!dto.BankAccountId.HasValue)
            {
                result.Message = "شناسه حساب بانکی موردنظر یافت نشد. مجددا تلاش کنید";
                return Json(result.ToJsonResult());
            }
            if (dto.Id <= 0)
            {
                result.Message = "حساب تفصیلی موردنظر خود را از لیست موجود انتخاب نمایید";
                return Json(result.ToJsonResult());
            }

            result = await _baseInfo.SetTafsilToAccountAsync(dto.BankAccountId.Value, dto.Id);
            if (result.Success)
                result.returnUrl = Request.Headers["Referer"].ToString();

            return Json(result.ToJsonResult());
        }

        public IActionResult RemoveAccountTafsil(int id)
        {
            ViewBag.BankAccountId = id;
            return PartialView("_RemoveAccountTafsil");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAccountTafsilA(int BankId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result = await _baseInfo.RemoveTafsilFromAccountAsync(BankId);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                return Json(result.ToJsonResult());
            }
            result.Message = "خطایی در حین عملیات رخ داده است";
            return Json(result.ToJsonResult());
        }
    }
}
