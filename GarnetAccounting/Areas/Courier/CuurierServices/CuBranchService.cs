using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Models;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class CuBranchService : ICuBranchService
    {
        private readonly AppDbContext _db;
        private readonly UserContextService _userContextService;

        public CuBranchService(AppDbContext context
            , UserContextService userContextService)
        {
            _db = context;
            _userContextService = userContextService;
        }

        public async Task<SelectList> SelectList_RepresentativeBranchesAsync(Guid id)
        {
            var branches = await _db.Cu_Branch.Where(n => n.RepresentativeId == id)
                .Select(n => new { id = n.Id, name = n.BranchName })
                .ToListAsync();

            return new SelectList(branches, "id", "name");
        }
        public async Task<List<BranchDto>> GetBranchesAsync(long sellerId)
        {
            if (_userContextService.SellerId == null)
                return new List<BranchDto>();

            var data = await _db.Cu_Branch.Where(n => n.SellerId == sellerId)
                .Select(n => new BranchDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    BranchCode = n.BranchCode,
                    BranchName = n.BranchName,
                    Province = n.BranchCity.Province.PersianName,
                    CityId = n.CityId,
                    CityName = n.BranchCity.PersianName,
                    IsOwnership = n.IsOwnership,
                    StartDate = n.StartDate,
                    IsActive = n.IsActive,
                    CommissionPercentage = n.CommissionPercentage,
                    IsHub = n.IsHub,
                    Latitude = n.Latitude,
                    Longitude = n.Longitude,
                    BranchTypeId = n.BranchTypeId,
                    OpeningDate = n.OpeningDate,
                    RenewalDate = n.RenewalDate,
                    HubId = n.HubId,
                    IsBillOfLadingIssuer = n.IsBillOfLadingIssuer,
                    IsUrbanFleet = n.IsUrbanFleet,
                    IsIntercityFleet = n.IsIntercityFleet,
                    RepresentativeId = n.RepresentativeId,
                    Address = n.Address,
                    BranchManager = n.BranchManager,
                    BranchManagerPhoneNumber = n.BranchManagerPhoneNumber,
                    TafsilId = n.TafsilId,

                }).OrderBy(n => n.Province).ThenBy(n => n.CityName).ToListAsync();

            return data;
        }

        public async Task<BranchDto> FindBranchByIdAsync(Guid id)
        {
            if (_userContextService.SellerId == null)
                return new BranchDto();

            var n = await _db.Cu_Branch.Include(a => a.TafsilAccount)
                .Include(a => a.BranchCity).ThenInclude(a => a.Province).FirstOrDefaultAsync(n => n.Id == id);

            var branch = new BranchDto();
            branch.Id = n.Id;
            branch.SellerId = n.SellerId;
            branch.BranchCode = n.BranchCode;
            branch.BranchName = n.BranchName;
            branch.Province = n.BranchCity?.Province?.PersianName;
            branch.CityId = n.CityId;
            branch.CityName = n.BranchCity?.PersianName;
            branch.IsOwnership = n.IsOwnership;
            branch.StartDate = n.StartDate;
            branch.IsActive = n.IsActive;
            branch.CommissionPercentage = n.CommissionPercentage;
            branch.IsHub = n.IsHub;
            branch.Latitude = n.Latitude;
            branch.Longitude = n.Longitude;
            branch.BranchTypeId = n.BranchTypeId;
            branch.OpeningDate = n.OpeningDate;
            branch.RenewalDate = n.RenewalDate;
            branch.HubId = n.HubId;
            branch.Address = n.Address;
            branch.BranchManager = n.BranchManager;
            branch.BranchManagerPhoneNumber = n.BranchManagerPhoneNumber;
            branch.TafsilId = n.TafsilId;
            branch.TafsilName = n.TafsilAccount?.Name;
            branch.IsBillOfLadingIssuer = n.IsBillOfLadingIssuer;
            branch.IsUrbanFleet = n.IsUrbanFleet;
            branch.IsIntercityFleet = n.IsIntercityFleet;
            branch.RepresentativeId = n.RepresentativeId;
            branch.IsExternalBLIssuer = n.IsExternalBLIssuer;
            branch.IsInternalBLIssuer = n.IsInternalBLIssuer;
            branch.OldBranchName = n.OldBranchName;
            branch.OldDistRepName = n.OldDistRepName;
            return branch;
        }
        public async Task<clsResult> AddBranchAsync(BranchDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (_userContextService.SellerId == null)
            {
                result.Message = "شرکت فعالی یافت نشد";
                return result;
            }

            if (dto == null || string.IsNullOrEmpty(dto.Id.ToString()))
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }

            if (await _db.Cu_Branch.AnyAsync(n => n.BranchCode == dto.BranchCode))
            {
                result.Message = "کد شعبه تکراری است.";
                return result;
            }

            Cu_Branch branch = new Cu_Branch();
            branch.Id = dto.Id;
            branch.SellerId = _userContextService.SellerId.Value;
            branch.BranchCode = dto.BranchCode;
            branch.BranchName = dto.BranchName;
            branch.CityId = dto.CityId;
            branch.IsOwnership = dto.IsOwnership;
            branch.StartDate = dto.StartDate;
            branch.IsActive = dto.IsActive;
            branch.CommissionPercentage = dto.CommissionPercentage;
            branch.IsHub = dto.IsHub ?? false;
            branch.Latitude = dto.Latitude ?? 0;
            branch.Longitude = dto.Longitude ?? 0;
            branch.BranchTypeId = dto.BranchTypeId;
            branch.OpeningDate = dto.OpeningDate;
            branch.RenewalDate = dto.RenewalDate;
            branch.HubId = dto.HubId ?? null;
            branch.IsBillOfLadingIssuer = dto.IsBillOfLadingIssuer;
            branch.IsUrbanFleet = dto.IsUrbanFleet;
            branch.IsIntercityFleet = dto.IsIntercityFleet;
            branch.RepresentativeId = dto.RepresentativeId;
            branch.Address = dto.Address;
            branch.BranchManager = dto.BranchManager;
            branch.BranchManagerPhoneNumber = dto.BranchManagerPhoneNumber;
            branch.TafsilId = dto.TafsilId;
            branch.IsInternalBLIssuer = dto.IsInternalBLIssuer;
            branch.IsExternalBLIssuer = dto.IsExternalBLIssuer;
            branch.OldBranchName = dto.OldBranchName;
            branch.OldDistRepName = dto.OldDistRepName;
            _db.Cu_Branch.Add(branch);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "شعبه جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> UpdateBranchAsync(BranchDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null || string.IsNullOrEmpty(dto.Id.ToString()))
            {
                result.Message = "اطلاعات بدرستی وارد نشده است";
                return result;
            }

            var branch = await _db.Cu_Branch.FindAsync(dto.Id);
            if (branch == null)
            {
                result.Message = "اطلاعات شعبه یافت نشد";
                return result;
            }

            if (await _db.Cu_Branch.AnyAsync(n => n.Id != dto.Id && n.BranchCode == dto.BranchCode))
            {
                result.Message = "کد شعبه تکراری است.";
                return result;
            }

            branch.SellerId = dto.SellerId;
            branch.BranchCode = dto.BranchCode;
            branch.BranchName = dto.BranchName;
            branch.CityId = dto.CityId;
            branch.IsOwnership = dto.IsOwnership;
            branch.StartDate = dto.StartDate;
            branch.IsActive = dto.IsActive;
            branch.CommissionPercentage = dto.CommissionPercentage;
            branch.IsHub = dto.IsHub ?? false;
            branch.Latitude = dto.Latitude ?? 0;
            branch.Longitude = dto.Longitude ?? 0;
            branch.BranchTypeId = dto.BranchTypeId;
            branch.OpeningDate = dto.OpeningDate;
            branch.RenewalDate = dto.RenewalDate;
            branch.HubId = dto.HubId ?? Guid.Empty;
            branch.IsBillOfLadingIssuer = dto.IsBillOfLadingIssuer;
            branch.IsUrbanFleet = dto.IsUrbanFleet;
            branch.IsIntercityFleet = dto.IsIntercityFleet;
            branch.RepresentativeId = dto.RepresentativeId;
            branch.Address = dto.Address;
            branch.BranchManager = dto.BranchManager;
            branch.BranchManagerPhoneNumber = dto.BranchManagerPhoneNumber;
            branch.TafsilId = dto.TafsilId;
            branch.IsInternalBLIssuer = dto.IsInternalBLIssuer;
            branch.IsExternalBLIssuer = dto.IsExternalBLIssuer;
            branch.OldBranchName = dto.OldBranchName;
            branch.OldDistRepName = dto.OldDistRepName;
            _db.Cu_Branch.Update(branch);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات شعبه با موفقیت بروزسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> DeleteBranchAsync(Guid id)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            var branch = await _db.Cu_Branch.FindAsync(id);
            if (branch == null)
            {
                result.Message = "اطلاعات شعبه یافت نشد";
                return result;
            }

            _db.Cu_Branch.Remove(branch);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "عملیات حذف شعبه با موفقیت انجام شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان حذف اطلاعات رخ داده است. احتمالا شعبه موردنظر در سیستم دارای سوابقی می باشد.\n {ex.Message}";
            }

            return result;
        }
    }

}
