using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Models;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class CuHubService : ICuHubService
    {
        private readonly AppDbContext _db;
        private readonly UserContextService _userContextService;

        public CuHubService(AppDbContext context, UserContextService userContextService)
        {
            _db = context;
            _userContextService = userContextService;
        }

        /// <summary>
        /// SelectList of Hubs for a specific seller.
        /// </summary>
        public async Task<SelectList> SelectList_HubsAsync(long sellerId)
        {
            var hubs = await _db.Cu_Hubs
                .Where(h => h.SellerId == sellerId && h.IsActive)
                .Select(h => new { id = h.HubId, name = h.HubName })
                .ToListAsync();

            return new SelectList(hubs, "id", "name");
        }

        public async Task<List<HubDto>> GetHubsAsync(long sellerId)
        {
            if (_userContextService.SellerId == null)
                return new List<HubDto>();

            var data = await _db.Cu_Hubs
                .Where(h => h.SellerId == sellerId)
                .Select(h => new HubDto
                {
                    HubId = h.HubId,
                    SellerId = h.SellerId,
                    HubName = h.HubName,
                    CityId = h.CityId,
                    CityName = h.HubCity.PersianName,
                    Latitude = h.Latitude,
                    Longitude = h.Longitude,
                    HubAddress = h.HubAddress,
                    IsActive = h.IsActive
                })
                .OrderBy(h => h.HubName)
                .ToListAsync();

            return data;
        }

        /// <summary>
        /// Find Hub by Id.
        /// </summary>
        public async Task<Cu_Hub> FindByIdAsync(Guid id)
        {
            if (_userContextService.SellerId == null)
                return null;

            return await _db.Cu_Hubs
                .Include(h => h.HubCity)
                .FirstOrDefaultAsync(h => h.HubId == id);
        }

        /// <summary>
        /// Get Hub Details by Id.
        /// </summary>
        public async Task<HubDto> GetHubByIdAsync(Guid id)
        {
            if (_userContextService.SellerId == null)
                return null;

            var hub = await _db.Cu_Hubs
                .Include(h => h.HubCity)
                .FirstOrDefaultAsync(h => h.HubId == id);

            if (hub == null)
                return null;

            return new HubDto
            {
                HubId = hub.HubId,
                SellerId = hub.SellerId,
                HubName = hub.HubName,
                CityId = hub.CityId,
                CityName = hub.HubCity.PersianName,
                Latitude = hub.Latitude,
                Longitude = hub.Longitude,
                HubAddress = hub.HubAddress,
                IsActive = hub.IsActive
            };
        }

        /// <summary>
        /// Create a new Hub.
        /// </summary>
        public async Task<clsResult> CreateHubAsync(HubDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (_userContextService.SellerId == null)
            {
                result.Message = "شرکت فعالی یافت نشد.";
                return result;
            }

            if (dto == null || string.IsNullOrEmpty(dto.HubName))
            {
                result.Message = "اطلاعات بدرستی وارد نشده است.";
                return result;
            }

            if (await _db.Cu_Hubs.AnyAsync(h => h.SellerId == dto.SellerId && h.HubName == dto.HubName))
            {
                result.Message = "نام هاب تکراری است.";
                return result;
            }

            var hub = new Cu_Hub
            {
                HubId = Guid.NewGuid(),
                SellerId = dto.SellerId,
                HubName = dto.HubName,
                CityId = dto.CityId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                HubAddress = dto.HubAddress,
                IsActive = dto.IsActive
            };

            _db.Cu_Hubs.Add(hub);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "هاب جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        /// <summary>
        /// Update an existing Hub.
        /// </summary>
        public async Task<clsResult> UpdateHubAsync(HubDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null || dto.HubId == Guid.Empty)
            {
                result.Message = "اطلاعات بدرستی وارد نشده است.";
                return result;
            }

            var hub = await _db.Cu_Hubs.FindAsync(dto.HubId);

            if (hub == null)
            {
                result.Message = "هاب مورد نظر یافت نشد.";
                return result;
            }

            if (await _db.Cu_Hubs.AnyAsync(h => h.HubId != dto.HubId && h.SellerId == dto.SellerId && h.HubName == dto.HubName))
            {
                result.Message = "نام هاب تکراری است.";
                return result;
            }

            hub.HubName = dto.HubName;
            hub.CityId = dto.CityId;
            hub.Latitude = dto.Latitude;
            hub.Longitude = dto.Longitude;
            hub.HubAddress = dto.HubAddress;
            hub.IsActive = dto.IsActive;

            _db.Cu_Hubs.Update(hub);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات هاب با موفقیت بروزرسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان بروزرسانی اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        /// <summary>
        /// Delete a Hub.
        /// </summary>
        public async Task<clsResult> DeleteHubAsync(Guid id)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            var hub = await _db.Cu_Hubs.FindAsync(id);

            if (hub == null)
            {
                result.Message = "هاب مورد نظر یافت نشد.";
                return result;
            }

            _db.Cu_Hubs.Remove(hub);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "عملیات حذف هاب با موفقیت انجام شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان حذف اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }
    }
}