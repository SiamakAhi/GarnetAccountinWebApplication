using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Dto.BranchUserDto;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GarnetAccounting.Areas.Courier.Controllers
{
    [Area("Courier")]
    [Authorize(Roles = "BranchManager , CustomerOwner")]
    public class BranchUserController : Controller
    {
        private readonly ICuBranchService _branchService;
        private readonly IBranchUserService _branchUserService;
        private readonly IAppIdentityUserManager _UserManager;
        private readonly UserContextService _userContext;
        private readonly IPersonService _persen;

        public BranchUserController(ICuBranchService branchService
            , IBranchUserService branchUserService
            , IAppIdentityUserManager appIdentityUserManager
            , UserContextService userContextService
            , IPersonService personService)
        {
            _branchService = branchService;
            _branchUserService = branchUserService;
            _UserManager = appIdentityUserManager;
            _userContext = userContextService;
            _persen = personService;
        }

        [HttpGet]
        public IActionResult CreateBranchManagerUser(Guid id)
        {
            ViewBag.BranchId = id;
            ViewBag.sellerId = _userContext.SellerId;
            return PartialView("_CreateBranchManagerUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBranchManagerUser(AddBranchUserDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "شرکت فعالی یافت نشد";
                result.updateType = 1;
                return Json(result.ToJsonResult());
            }
            if (ModelState.IsValid)
            {
                dto.SellerId = _userContext.SellerId.Value;
                dto.CustomerId = _userContext.CustomerId;
                result = await _UserManager.AddBranchUserAccountAsync(dto);
                if (result.Success)
                {
                    result.Message = "کاربر شعبه با موقفیت ایجاد شد";
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
            }
            var errors = ModelState.Values.SelectMany(n => n.Errors).ToList();
            foreach (var er in errors)
            {
                result.Message += $"\n {er.ErrorMessage}";
            }

            return Json(result.ToJsonResult());
        }

        public async Task<IActionResult> BranchUser(BranchUserFilterDto filter)
        {
            var model = new ViewmodelBranchUser();
            if (filter != null)
                model.filter = filter;
            model.Users = await _branchUserService.GetBranchesUserAsync(model.filter);
            return View(model);
        }
    }
}
