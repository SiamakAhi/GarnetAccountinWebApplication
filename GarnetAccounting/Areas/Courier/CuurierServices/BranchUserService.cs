using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto.BranchUserDto;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class BranchUserService : IBranchUserService
    {
        private readonly IAppIdentityUserManager _userManager;
        private readonly AppDbContext _db;

        public BranchUserService(IAppIdentityUserManager appIdentityUserManager, AppDbContext dbContex)
        {
            _userManager = appIdentityUserManager;
            _db = dbContex;
        }

        public async Task<List<VmBranchUser>> GetBranchesUserAsync(BranchUserFilterDto filter)
        {

            var query = _db.Cu_BranchUser
              .Include(n => n.Branch).ThenInclude(n => n.BranchCity)
                .Include(n => n.Branch).ThenInclude(n => n.BranchHub)
              .Include(n => n.IdentityUser)
              .AsQueryable();

            var result = await query.Select(n => new VmBranchUser
            {
                Id = n.Id,
                BranchId = n.BranchId,
                BranchName = n.Branch.BranchName,
                userName = n.userName,
                Name = n.FullName,
                BranchCityId = n.Branch.CityId.Value,
                BranchCityName = n.Branch.BranchCity.PersianName,
                Gender = n.IdentityUser.Gender,
                RegisterDate = n.IdentityUser.RegistrDate,
                BranchCode = n.Branch.BranchCode ?? "NoCode",
                BranchHubId = n.Branch.HubId,
                BranchHubName = n.Branch.BranchHub != null ? n.Branch.BranchHub.HubName : "تعیین نشده",

            }).ToListAsync();

            return result;
        }

        public async Task<VmBranchUser> GetBUserAsync(string userId)
        {
            var bu = await _db.Cu_BranchUser
                .Include(n => n.Branch).ThenInclude(n => n.BranchCity)
                .Include(n => n.Branch).ThenInclude(n => n.BranchHub)
                .Include(u => u.IdentityUser).FirstOrDefaultAsync(n => n.UserId == userId);
            VmBranchUser vmBranchUser = new VmBranchUser
            {
                Id = bu.Id,
                BranchId = bu.BranchId,
                BranchName = bu.Branch.BranchName,
                userName = bu.userName,
                Gender = bu.IdentityUser.Gender,
                Name = bu.FullName,
                BranchCityId = bu.Branch.CityId.Value,
                BranchCityName = bu.Branch.BranchCity.PersianName,
                RegisterDate = bu.IdentityUser.RegistrDate,
                BranchCode = bu.Branch.BranchCode ?? "NoCode",
                BranchHubId = bu.Branch.HubId,
                BranchHubName = bu.Branch.BranchHub?.HubName,
            };
            return vmBranchUser;
        }
        public async Task<VmBranchUser?> GetBUserByUsernameAsync(string userName)
        {
            var bu = await _db.Cu_BranchUser
                 .Include(n => n.Branch).ThenInclude(n => n.BranchCity)
                  .Include(n => n.Branch).ThenInclude(n => n.BranchHub)
                .Include(u => u.IdentityUser).FirstOrDefaultAsync(n => n.userName == userName);
            if (bu == null) return null;
            VmBranchUser vmBranchUser = new VmBranchUser
            {
                Id = bu.Id,
                BranchId = bu.BranchId,
                BranchName = bu.Branch.BranchName,
                userName = bu.userName,
                Gender = bu.IdentityUser.Gender,
                Name = bu.FullName,
                BranchCityId = bu.Branch.CityId.Value,
                BranchCityName = bu.Branch.BranchCity.PersianName,
                RegisterDate = bu.IdentityUser.RegistrDate,
                BranchCode = bu.Branch.BranchCode ?? "NoCode",
                BranchHubId = bu.Branch.HubId,
                BranchHubName = bu.Branch.BranchHub?.HubName,
            };
            return vmBranchUser;
        }
    }
}
