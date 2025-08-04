using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Models;
using GarnetAccounting.Models.Commercial;
using GarnetAccounting.Models.Identity;
using GarnetAccounting.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GarnetAccounting.Services.Identity
{
    public class AppIdentityUserManager : UserManager<AppIdentityUser>, IAppIdentityUserManager
    {
        private readonly PersianIdentityError _errors;
        private readonly ILookupNormalizer _lookupNormalizer;
        private readonly ILogger<AppIdentityUserManager> _logger;
        private readonly IOptions<IdentityOptions> _option;
        private readonly IPasswordHasher<AppIdentityUser> _passwordHasher;
        private readonly IEnumerable<IPasswordValidator<AppIdentityUser>> _passwordValidator;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserStore<AppIdentityUser> _userStore;
        private readonly IEnumerable<IUserValidator<AppIdentityUser>> _userValidators;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _db;

        public AppIdentityUserManager(
            PersianIdentityError errors,
        ILookupNormalizer lookupNormalizer,
        ILogger<AppIdentityUserManager> logger,
        IOptions<IdentityOptions> option,
        IPasswordHasher<AppIdentityUser> passwordHasher,
        IEnumerable<IPasswordValidator<AppIdentityUser>> passwordValidator,
        IServiceProvider serviceProvider,
        IUserStore<AppIdentityUser> userStore,
        IEnumerable<IUserValidator<AppIdentityUser>> userValidators,
        SignInManager<AppIdentityUser> signInManager,
        IWebHostEnvironment env,
        AppDbContext db
            )
            : base(userStore, option, passwordHasher, userValidators, passwordValidator, lookupNormalizer, errors, serviceProvider, logger)
        {

            _errors = errors;
            _lookupNormalizer = lookupNormalizer;
            _logger = logger;
            _option = option;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _serviceProvider = serviceProvider;
            _userStore = userStore;
            _userValidators = userValidators;
            _signInManager = signInManager;
            _env = env;
            _db = db;
        }

        public async Task<List<AppIdentityUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }
        public IQueryable<UserViewModel> GetAllUsersWithRolesAsync()
        {
            return Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FName,
                LastName = user.Family,
                BirthDate = user.Birthday,
                IsActive = user.IsActive,
                LastVisitDateTime = user.LastVisitDate,
                Image = user.Avatar,
                RegisterDate = user.RegistrDate,
                Gender = user.Gender,
            }).AsQueryable();
        }
        public async Task<string[]> GetUserRolesAsync(AppIdentityUser user)
        {
            string[] roles;
            var usr = await Users.Include(n => n.Roles).FirstOrDefaultAsync(n => n.Id == user.Id);
            roles = usr.Roles.Select(r => r.Name).ToArray();
            return roles;
        }
        public string[] GetUserRoles(string UserId)
        {
            string[] roles;
            roles = Users.Include(n => n.Roles).FirstOrDefault(n => n.Id == UserId).Roles.Select(r => r.Name).ToArray();
            return roles;
        }

        public async Task<List<UserRoleViewModel>> GetUserRolesVmAsync(string userName)
        {
            var userRoles = new List<UserRoleViewModel>();
            var user = await Users.FirstOrDefaultAsync(n => n.UserName == userName);
            if (user != null)
            {
                var roles = await GetUserRolesByNameAsync(user.UserName);
                foreach (var role in roles)
                {
                    userRoles.Add(new UserRoleViewModel
                    {
                        RoleName = role.Name,
                        userName = user.UserName,
                        RoleDescription = role.Description,
                        RoleId = role.RoleId,
                        UserId = user.Id,
                    });
                }
            }
            return userRoles;
        }

        public UserViewModel UserInfo(string UserName)
        {
            var user = Users.Include(r => r.Roles).SingleOrDefault(u => u.UserName == UserName);
            UserViewModel vm = new UserViewModel
            {
                FirstName = user.FName,
                LastName = user.Family,
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                BirthDate = user.Birthday,
                Gender = user.Gender,
                Image = user.Avatar,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                CustomerId = user.CustomerId,
                DepartmentCode = user.DepartmentCode,
                RegisterDate = user.RegistrDate,
                LastVisitDateTime = user.LastVisitDate,
            };

            if (user.Roles != null)
            {
                string[] RolesID = user.Roles.Select(n => n.Id).ToArray();

                vm.Roles = _db.Roles.Where(n => RolesID.Contains(n.Id)).Select(n => new AppRolViewModel
                {
                    Id = n.Id,
                    Name = n.Name,
                    Description = n.Description
                }).ToList();
            }

            return vm;
        }
        public async Task<string> GetFullNameAsync(AppIdentityUser user)
        {
            await Task.CompletedTask;
            return user.FName + " " + user.Family;
        }
        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            var user = await Users.SingleOrDefaultAsync(n => n.Id == userId);
            return user.FName + " " + user.Family;
        }
        public string GetFullName()
        {
            var user = new AppIdentityUser();
            return user.FName + " " + user.Family;
        }
        public async Task<int> GetActiveUsersCount()
        {
            return await Users.Where(n => n.IsActive == true).CountAsync();
        }
        public string NormalizeKey(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> AddEmployeeAsync(VmRegisterUser emp)
        {
            AppIdentityUser user = new AppIdentityUser();
            // user.Id = Guid.NewGuid().ToString();
            user.FName = emp.FName;
            user.Family = emp.LName;
            user.PhoneNumber = emp.Mobile;
            user.Email = emp.email;
            user.UserName = emp.UserName;
            user.IsActive = true;
            user.RegistrDate = DateTime.Now;
            user.PhoneNumberConfirmed = false;
            user.EmailConfirmed = false;
            user.Gender = 1;
            user.AccessFailedCount = 0;
            user.Avatar = "";
            user.Mobile = emp.Mobile;
            user.Birthday = emp.Birthday != null ? emp.Birthday.Value : DateTime.Now.AddYears(-24);

            var result = new IdentityResult();
            try
            {
                result = await CreateAsync(user, emp.Password);
            }
            catch (Exception msg)
            {
                var xx = result;
                var res = msg.Message;
            }

            if (emp.Roles?.Length > 0 && result.Succeeded)
            {
                foreach (var r in emp.Roles)
                {
                    try
                    {
                        await AddToRoleAsync(user, r);
                    }
                    catch (Exception msg)
                    {
                        var res = msg.Message;
                    }
                    await AddToRoleAsync(user, r);
                }
            }

            return result;
        }
        public async Task<IdentityResult> AddCustomerOwnerAccount(VmRegisterUser emp)
        {
            AppIdentityUser user = new AppIdentityUser();
            user.FName = emp.FName;
            user.Family = emp.LName;
            user.PhoneNumber = emp.Mobile;
            user.Email = emp.email;
            user.UserName = emp.UserName;
            user.IsActive = true;
            user.RegistrDate = DateTime.Now;
            user.PhoneNumberConfirmed = false;
            user.EmailConfirmed = false;
            user.Gender = emp.Gender.Value;
            user.AccessFailedCount = 0;
            user.Avatar = "";
            user.Mobile = emp.Mobile;
            user.Birthday = emp.Birthday.Value;
            user.CustomerId = emp.customerId;

            var result = new IdentityResult();
            try
            {
                result = await CreateAsync(user, emp.Password);
            }
            catch (Exception msg)
            {
                var xx = result;
                var res = msg.Message;
            }

            if (emp.Roles?.Length > 0 && result.Succeeded)
            {
                foreach (var r in emp.Roles)
                {
                    try
                    {
                        await AddToRoleAsync(user, r);
                    }
                    catch (Exception msg)
                    {

                        var res = msg.Message;
                    }
                    await AddToRoleAsync(user, r);
                }
            }

            var userSetting = _db.UserSettings.FirstOrDefaultAsync(n => n.UserName == user.UserName);
            if (userSetting == null)
            {
                UserSetting setting = new UserSetting
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    CustomerId = user.CustomerId,

                };
                _db.UserSettings.Add(setting);
                await _db.SaveChangesAsync();
            }

            return result;
        }
        //--------------------------------------------
        public async Task<clsResult> AddBranchUserAccountAsync(AddBranchUserDto emp)
        {
            clsResult result = new clsResult();
            AppIdentityUser user = new AppIdentityUser();
            user.FName = emp.FName;
            user.Family = emp.LName;
            user.PhoneNumber = emp.Mobile;
            user.Email = emp.email;
            user.UserName = emp.UserName;
            user.IsActive = true;
            user.RegistrDate = DateTime.Now;
            user.PhoneNumberConfirmed = true;
            user.EmailConfirmed = false;
            user.Gender = emp.Gender.Value;
            user.AccessFailedCount = 0;
            user.Avatar = "";
            user.Mobile = emp.Mobile;
            user.Birthday = DateTime.Now.AddYears(-30);
            user.DepartmentCode = emp.DepartmentCode;

            var IdentityResult = new IdentityResult();
            try
            {
                IdentityResult = await CreateAsync(user, emp.Password);
            }
            catch (Exception msg)
            {

                result.Message = msg.Message;
                return result;
            }

            if (emp.Roles?.Length > 0 && IdentityResult.Succeeded)
            {
                foreach (var r in emp.Roles)
                {
                    try
                    {
                        await AddToRoleAsync(user, r);
                    }
                    catch (Exception msg)
                    {
                        result.Message = "در مدیریت نقش ها خطایی رخ داده است ";
                        result.Message += msg.Message;
                    }
                }
            }

            Cu_BranchUser branchUser = new Cu_BranchUser();
            branchUser.BranchId = emp.BranchId;
            branchUser.sellerId = emp.SellerId;
            branchUser.Id = emp.DepartmentUserId;
            branchUser.IsSupervisor = emp.IsSupervisor;
            branchUser.UserId = user.Id;
            branchUser.userName = user.UserName;
            branchUser.FullName = emp.FName + " " + emp.LName;
            _db.Cu_BranchUser.Add(branchUser);

            var userSetting = await _db.UserSettings.FirstOrDefaultAsync(n => n.UserName == user.UserName);
            if (userSetting == null)
            {
                UserSetting setting = new UserSetting
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    CustomerId = user.CustomerId,
                    ActiveSellerId = emp.SellerId,
                    BranchId = emp.BranchId,
                };
                try
                {
                    _db.UserSettings.Add(setting);
                    await _db.SaveChangesAsync();

                    result.Success = true;
                    result.Message = "عملیات با موفقیت انجام شد";
                    result.updateType = 1;
                }
                catch (Exception)
                {
                    result.Message += "\n  در ثبت اطلاعات کاربر در شعبه، خطایی رخ داده است";
                }
            }
            else
            {
                userSetting.UserId = user.Id;
                userSetting.UserName = user.UserName;
                userSetting.CustomerId = user.CustomerId;
                userSetting.ActiveSellerId = emp.SellerId;
                userSetting.BranchId = emp.BranchId;
                _db.UserSettings.Update(userSetting);
                await _db.SaveChangesAsync();
            }
            return result;
        }
        //----------------------------------------------
        public async Task<IdentityResult> UpdateProfile(VmUpdateProfile model)
        {
            // Save User Avatar
            string fileName = "";
            string SavePath = "";
            if (model.ImageFile != null)
            {
                string FileExtension = Path.GetExtension(model.ImageFile.FileName);
                fileName = string.Concat("Avatar_", model.userName, FileExtension);
                SavePath = $"{_env.WebRootPath}/img/avatars/{fileName}";

                using (var stm = new FileStream(SavePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stm);
                }
            }

            AppIdentityUser user = await Users.Where(u => u.Id == model.Id).SingleOrDefaultAsync();

            user.Email = model.Email;
            try
            {
                user.Birthday = model.BirthDate.Value;
            }
            catch
            {
            }

            user.Family = model.LastName;
            user.FName = model.FirstName;
            user.Gender = model.Gender.Value;
            user.Mobile = model.Mobile;
            user.PhoneNumber = model.PhoneNumber;
            if (fileName != "")
                user.Avatar = fileName;
            var result = await UpdateUserAsync(user);
            return result;
        }
        public async Task<VmUpdateProfile> GetUserVmAsync(string UserName)
        {
            AppIdentityUser u = await Users.Where(n => n.UserName == UserName).SingleOrDefaultAsync();
            VmUpdateProfile vm = new VmUpdateProfile();
            vm.BirthDate = u.Birthday;
            try { vm.StrBirthDate = u.Birthday.LatinToPersianForDatepicker(); } catch { }
            vm.Email = u.Email;
            vm.FirstName = u.FName;
            vm.Gender = u.Gender;
            vm.Id = u.Id;
            vm.Image = u.Avatar;
            vm.LastName = u.Family;
            vm.PhoneNumber = u.PhoneNumber;
            vm.userName = u.UserName;
            vm.Mobile = u.Mobile;
            vm.RegisterDate = u.RegistrDate;

            return vm;
        }
        public async Task<VmRegisterUser> GetUserForEditAsync(string id)
        {
            AppIdentityUser u = await Users.Where(n => n.Id == id).SingleOrDefaultAsync();
            VmRegisterUser vm = new VmRegisterUser();
            vm.email = u.Email;
            vm.FName = u.FName;
            vm.LName = u.Family;
            vm.Id = u.Id;
            vm.Mobile = u.Mobile;
            vm.UserName = u.UserName;
            // vm.Roles = u.Roles.Select(n => n.Role.Name).ToArray();
            return vm;
        }
        public async Task<List<VmRole>> GetUserRolesByNameAsync(string userName)
        {
            var user = await Users.Include(n => n.Roles).SingleOrDefaultAsync(n => n.UserName == userName);

            List<VmRole> roles = new List<VmRole>();
            if (user != null)
            {
                var Roles = await _db.UserRoles
               .Where(n => n.UserId == user.Id)
               .Join(_db.Roles, n => n.RoleId, r => r.Id, (n, r) => new VmRole
               {
                   RoleId = n.RoleId,
                   Description = r.Description,
                   Name = r.Name,
               }).ToListAsync();

                roles.AddRange(Roles);
            }


            return roles;
        }
        public async Task<int?> GetCustomerIdByUsername(string username)
        {
            var n = await Users.SingleOrDefaultAsync(n => n.UserName == username);
            int? id = n?.CustomerId;
            return id;
        }
        public async Task<string[]> userArrayRolesAsync(string username)
        {
            var roles = await GetUserRolesByNameAsync(username);
            string[] roleArray = roles.Select(n => n.Name).ToArray();
            return roleArray;
        }
        public async Task<string[]?> userArrayRolesDescAsync(string username)
        {
            var roles = await GetUserRolesByNameAsync(username);
            foreach (var role in roles)
            {

                if (string.IsNullOrEmpty(role.Description))
                    role.Description = role.Name;
            }
            string[] roleArray = roles.Select(n => n.Description).ToArray();

            return roleArray;
        }
        public async Task<short?> GetUserDepartmentCodeAsync(string username)
        {
            var user = await Users.SingleOrDefaultAsync(n => n.UserName == username);
            return user?.DepartmentCode;
        }
    }
}
