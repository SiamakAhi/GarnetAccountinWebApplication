using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Models.Identity;
using GarnetAccounting.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Controllers
{
    [Authorize]
    public class usermanagerController : Controller
    {
        private readonly IAppIdentityUserManager _usermanager;
        private readonly IAppRoleManager _rolemanager;
        private readonly SignInManager<AppIdentityUser> _SignIn;

        public usermanagerController(IAppIdentityUserManager usermanager, IAppRoleManager rolemanager, SignInManager<AppIdentityUser> signIn)
        {
            _usermanager = usermanager;
            _rolemanager = rolemanager;
            _SignIn = signIn;
        }


        //[HttpGet]
        //public IActionResult AppUsers(string ReturnUrl)
        //{
        //    var users = _usermanager.GetAllUsersWithRolesAsync().ToList();

        //    return View(users);
        //}


        [HttpGet]

        public IActionResult roles()
        {
            var roles = _rolemanager.RolesViewModelList().ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddNewRole()
        {
            AppRolViewModel model = new AppRolViewModel();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewRole(AppRolViewModel model)
        {
            clsResult result = new clsResult();

            if (ModelState.IsValid)
            {
                AppRole role = new AppRole
                {
                    Name = model.Name,
                    Description = model.Description,
                };

                var res = await _rolemanager.CreateAsync(role);
                if (res.Succeeded)
                {
                    result.Success = true;
                    result.Message = "عملیات با موفقیت انجام شد";
                    result.returnUrl = Url.Action("Roles", "usermanager", new { Area = "" });

                }
                else
                {
                    result.Success = false;
                    result.Message = "عملیات با شکست مواجه شد. ممکن است اطلاعات را کامل یا بدرستی وارد نکرده باشید";
                }

            }
            else
            {
                result.Success = false;
                result.Message = "عملیات با شکست مواجه شد. ممکن است اطلاعات را کامل یا بدرستی وارد نکرده باشید";
            }

            return Json(result.ToJsonResult());
        }


        [HttpGet]
        public IActionResult EditRole(string Id)
        {
            AppRolViewModel model = _rolemanager.RolesViewModelList().SingleOrDefault(x => x.Id == Id);
            return PartialView("_EditRole", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(AppRolViewModel model)
        {
            clsResult result = new clsResult();

            if (ModelState.IsValid)
            {
                AppRole role = await _rolemanager.FindByIdAsync(model.Id);
                role.Name = model.Name;
                role.Description = model.Description;

                var res = await _rolemanager.UpdateAsync(role);
                if (res.Succeeded)
                {
                    result.Success = true;
                    result.Message = "عملیات با موفقیت انجام شد";
                    result.returnUrl = Url.Action("Roles", "usermanager", new { Area = "" });

                }
                else
                {
                    result.Success = false;
                    result.Message = "عملیات با شکست مواجه شد. ممکن است اطلاعات را کامل یا بدرستی وارد نکرده باشید";
                }

            }
            else
            {
                result.Success = false;
                result.Message = "عملیات با شکست مواجه شد. ممکن است اطلاعات را کامل یا بدرستی وارد نکرده باشید";
            }

            return Json(result.ToJsonResult());
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Loginn(string? ReturnUrl)
        {

            if (_SignIn.IsSignedIn(User))
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                    return RedirectToAction(ReturnUrl);
                else
                    return RedirectToAction("Index", "Home", new { Area = "" });
            }

            ViewData["ReturnUrl"] = ReturnUrl;

            return View();
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult Login(string? ReturnUrl)
        //{

        //    if (_SignIn.IsSignedIn(User))
        //    {
        //        //if (!string.IsNullOrEmpty(ReturnUrl))
        //        //    return RedirectToAction(ReturnUrl);
        //        //else
        //        return RedirectToAction("Index", "Home", new { Area = "" });
        //    }

        //    //ViewData["ReturnUrl"] = ReturnUrl;

        //    return View();
        //}
        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(VmLogin vm, string? ReturnUrl = null)
        //{
        //    if (_SignIn.IsSignedIn(User))
        //        return RedirectToAction("Index");

        //    if (ModelState.IsValid)
        //    {
        //        var result = await _SignIn.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, false);

        //        if (result.Succeeded)
        //        {

        //            if (!string.IsNullOrEmpty(ReturnUrl))
        //                return RedirectToAction(ReturnUrl);
        //            else
        //                return RedirectToAction("Index", "Home", new { Area = "" }); ;
        //        }

        //        ModelState.AddModelError("", "نام کاربری یا کلمه عبور اشتباه است");
        //    }

        //    return View();
        //}

        //......................................


        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("_Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(VmRegisterUser user)
        {
            ViewBag.rolse = new SelectList(_rolemanager.Roles, "Name", "Description");
            clsResult result = new clsResult();
            if (user.personId == null)
            {
                result.Success = false;
                result.Message = "انتخاب شخص الزامیست";
                return Json(result.ToJsonResult());
            }

            if (ModelState.IsValid)
            {
                var res = await _usermanager.AddEmployeeAsync(user);
                if (res.Succeeded)
                {
                    result.Success = true;
                    result.Message = "عملیات با موفقیت انجام شد";
                    result.returnUrl = Request.Headers["Referer"].ToString(); // Url.Action("AppUsers", "usermanager", new { Area = "" });

                    return Json(result.ToJsonResult());
                }

            }

            result.Success = false;
            result.Message = "عملیات با شکست مواجه شد. ممکن است اطلاعات را کامل یا بدرستی وارد نکرده باشید";
            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            clsResult result = new clsResult();
            var user = await _usermanager.FindByIdAsync(id);
            if (user != null)
            {
                var res = await _usermanager.DeleteAsync(user);

                if (res.Succeeded)
                {
                    result.Success = true;
                    result.Message = "عملیات با موفقیت انجام شد";
                    result.returnUrl = Url.Action("AppUsers", "usermanager", new { Area = "" });

                    return Json(result.ToJsonResult());
                }
            }

            result.Success = false;
            result.Message = "عملیات با شکست مواجه شد. ممکن است این کاربر در بانک اظلاعاتی دارای سابقه باشد";
            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string id)
        {
            ViewBag.rolse = new SelectList(_rolemanager.Roles, "Name", "Description");
            AppIdentityUser user = await _usermanager.FindByIdAsync(id);
            VmUpdateProfile model = new VmUpdateProfile()
            {

                Email = user.Email,
                FirstName = user.FName,
                LastName = user.Family,
                Id = user.Id,
                PhoneNumber = user.Mobile,
                userName = user.UserName,
                BirthDate = user.Birthday,
                Gender = user.Gender,
                Image = user.Avatar,

            };
            model.StrBirthDate = user.Birthday < DateTime.Now.AddYears(-90) ? DateTime.Now.AddYears(-30).LatinToPersian() : user.Birthday.LatinToPersian();
            return PartialView("_UpdateUser", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(VmUpdateProfile user)
        {
            clsResult result = new clsResult();

            if (ModelState.IsValid)
            {
                var res = await _usermanager.UpdateProfile(user);
                if (res.Succeeded)
                {
                    result.Success = true;
                    result.Message = "عملیات با موفقیت انجام شد";
                    result.returnUrl = Url.Action("AppUsers", "usermanager", new { Area = "" });

                    return Json(result.ToJsonResult());
                }
            }

            result.Success = false;
            result.Message = "عملیات با شکست مواجه شد. ممکن است اطلاعات را کامل یا بدرستی وارد نکرده باشید";
            return Json(result.ToJsonResult());
        }


        public async Task<IActionResult> SignOut()
        {
            await _SignIn.SignOutAsync();

            return RedirectToAction("Login");
        }

        //...................... User Profile 
        public async Task<IActionResult> UserProfile(string? message = null)
        {
            if (message != null)
            {
                ViewBag.msg = message;
            }
            VmUserProfile model = new VmUserProfile();
            model.UpdateProfile = await _usermanager.GetUserVmAsync(User.Identity.Name);
            model.UserRoles = await _usermanager.GetUserRolesByNameAsync(User.Identity.Name);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserProfile()
        {
            AppIdentityUser user = await _usermanager.FindByNameAsync(User.Identity.Name);

            VmUpdateProfile model = new VmUpdateProfile()
            {
                Email = user.Email,
                FirstName = user.FName,
                LastName = user.Family,
                Id = user.Id,
                PhoneNumber = user.Mobile,
                userName = user.UserName,
                BirthDate = user.Birthday,
                Gender = user.Gender,
                Image = user.Avatar,

            };
            model.StrBirthDate = user.Birthday < DateTime.Now.AddYears(-90) ? DateTime.Now.AddYears(-30).LatinToPersian() : user.Birthday.LatinToPersian();
            return PartialView("_UpdateUserProfile", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserProfile(VmUpdateProfile user)
        {
            clsResult result = new clsResult();
            user.BirthDate = user.StrBirthDate?.PersianToLatin();
            if (ModelState.IsValid)
            {
                var res = await _usermanager.UpdateProfile(user);
                if (res.Succeeded)
                {
                    return RedirectToAction("UserProfile");
                }
            }

            return RedirectToAction("UserProfile");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(VmChangePass model)
        {
            var user = await _usermanager.FindByNameAsync(model.UserName);
            string msg = "";
            if (ModelState.IsValid)
            {
                var res = await _usermanager.RemovePasswordAsync(user);
                if (res.Succeeded)
                {
                    string token = await _usermanager.GeneratePasswordResetTokenAsync(user);
                    await _usermanager.ResetPasswordAsync(user, token, model.NewPassword);
                    msg = "بروزرسانی رمز عبور شما با موفقیت انجام شد";
                    return RedirectToAction("UserProfile", new { message = msg });
                }
            }
            msg = "مشکلی در تغییر کلمه عبور پیش آمده است. لطفاً مجددا سعی کنید";
            return RedirectToAction("UserProfile", new { message = msg });
        }


        //[HttpGet]
        //public async Task<IActionResult> CreateUser(int personId)
        //{
        //    var person = await _person.FindByIdAsync(personId);

        //    VmRegisterUser model = new VmRegisterUser();
        //    model.FName = person.FirstName;
        //    model.LName = person.LastName;
        //    model.email = person.Email;
        //    model.Mobile = person.PhoneNumber;
        //    model.personId = personId;
        //    model.Gender = 1;

        //    return PartialView("_CreateUser", model);
        //}

        public async Task<IActionResult> GetUserRole(string userName)
        {
            var model = new UserRoleManagerDto();
            model.CurrentUserRolse = await _usermanager.userArrayRolesAsync(userName);
            model.username = userName;
            ViewBag.Roles = _rolemanager.SelectList_Roles();

            return PartialView("_GetUserRole", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRoles(UserRoleManagerDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            result.updateType = 1;

            var user = _usermanager.Users.Where(n => n.UserName == dto.username).FirstOrDefault();

            string[] oldRoles = await _usermanager.userArrayRolesAsync(dto.username);

            var removeRoles = await _usermanager.RemoveFromRolesAsync(user, oldRoles);
            if (removeRoles.Succeeded)
            {
                var idResult = await _usermanager.AddToRolesAsync(user, dto.CurrentUserRolse);
                if (idResult.Succeeded)
                {
                    result.Success = true;
                    result.Message = "نقش های کاربر با موفقیت بروزرسانی شد";

                    result.returnUrl = Request.Headers["Referer"].ToString();
                    return Json(result.ToJsonResult());
                }
                else
                {
                    foreach (var e in idResult.Errors)
                    {
                        result.Message += "\n " + e.Description;
                    }
                }
            }
            else
            {
                foreach (var er in removeRoles.Errors)
                {
                    result.Message += "\n " + er.Description;
                }
            }

            return Json(result.ToJsonResult());
        }

    }
}
