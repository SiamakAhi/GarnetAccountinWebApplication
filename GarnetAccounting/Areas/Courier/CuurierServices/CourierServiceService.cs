using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class CourierServiceService : ICourierServiceService
    {
        private readonly AppDbContext _db;

        public CourierServiceService(AppDbContext dbContext /*, UserContextService userContextService*/)
        {
            _db = dbContext;
            // _userContextService = userContextService;
        }

        //================================================================================== Service

        public async Task<SelectList> SelectList_ServicesAsync(long sellerId)
        {

            var services = await _db.Cu_Services
                .Where(s => s.SellerId == sellerId)
                .Select(s => new Cu_ServiceDto
                {
                    Id = s.Id,
                    ServiceName = s.ServiceName,
                })
                .OrderBy(s => s.ServiceName)
                .ToListAsync();

            return new SelectList(services, "Id", "ServiceName");
        }
        public SelectList ServiceRatingType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "cr", Text = "Courier" });
            list.Add(new SelectListItem { Value = "ia", Text = "IATA" });

            return new SelectList(list, "Value", "Text");

        }

        public async Task<List<Cu_ServiceDto>> GetServicesAsync(long sellerId)
        {

            var services = await _db.Cu_Services
                .Where(s => s.SellerId == sellerId)
                .Select(s => new Cu_ServiceDto
                {
                    Id = s.Id,
                    SellerId = s.SellerId,
                    ServiceCode = s.ServiceCode,
                    ServiceName = s.ServiceName,
                    ServiceName_En = s.ServiceName_En,
                    ServicePercentage = s.ServicePercentage,
                    ShipmentTypeCode = s.ShipmentTypeCode,
                    RatingType = s.RatingType,
                })
                .OrderBy(s => s.ServiceCode)
                .ToListAsync();

            return services;
        }


        public async Task<Cu_ServiceDto> FindServiceByIdAsync(int id)
        {
            var service = await _db.Cu_Services.FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
            {
                return null!;
            }

            var dto = new Cu_ServiceDto
            {
                Id = service.Id,
                SellerId = service.SellerId,
                ServiceCode = service.ServiceCode,
                ServiceName = service.ServiceName,
                ServiceName_En = service.ServiceName_En,
                ServicePercentage = service.ServicePercentage,
                ShipmentTypeCode = service.ShipmentTypeCode,
                RatingType = service.RatingType,
            };
            return dto;
        }
        public async Task<clsResult> AddServiceAsync(Cu_ServiceDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null)
            {
                result.Message = "اطلاعات به درستی وارد نشده است.";
                return result;
            }

            // بررسی عدم تکراری بودن کد سرویس (ServiceCode) برای همان SellerId
            bool isDuplicate = await _db.Cu_Services.AnyAsync(s =>
                s.ServiceCode == dto.ServiceCode && s.SellerId == dto.SellerId);
            if (isDuplicate)
            {
                result.Message = "کد سرویس تکراری است.";
                return result;
            }

            // ساخت موجودیت جدید
            var service = new Cu_Service
            {
                SellerId = dto.SellerId,
                ServiceCode = dto.ServiceCode,
                ServiceName = dto.ServiceName,
                ServiceName_En = dto.ServiceName_En,
                ServicePercentage = dto.ServicePercentage,
                ShipmentTypeCode = dto.ShipmentTypeCode,
                RatingType = dto.RatingType,
            };

            // درج در دیتابیس
            _db.Cu_Services.Add(service);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "سرویس جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> UpdateServiceAsync(Cu_ServiceDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null)
            {
                result.Message = "اطلاعات به درستی وارد نشده است.";
                return result;
            }

            // بازیابی سرویس از دیتابیس
            var service = await _db.Cu_Services.FindAsync(dto.Id);
            if (service == null)
            {
                result.Message = "سرویسی با این شناسه یافت نشد.";
                return result;
            }

            // بررسی عدم تکرار کد سرویس برای رکورد دیگری
            bool isDuplicate = await _db.Cu_Services.AnyAsync(s =>
                s.Id != dto.Id &&
                s.SellerId == dto.SellerId &&
                s.ServiceCode == dto.ServiceCode
            );
            if (isDuplicate)
            {
                result.Message = "کد سرویس تکراری است.";
                return result;
            }

            // ویرایش فیلدها
            service.SellerId = dto.SellerId;
            service.ServiceCode = dto.ServiceCode;
            service.ServiceName = dto.ServiceName;
            service.ServiceName_En = dto.ServiceName_En;
            service.ServicePercentage = dto.ServicePercentage;
            service.ShipmentTypeCode = dto.ShipmentTypeCode;
            service.RatingType = dto.RatingType;

            _db.Cu_Services.Update(service);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات سرویس با موفقیت به‌روزرسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان به‌روزرسانی اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> DeleteServiceAsync(int id)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            var service = await _db.Cu_Services.FindAsync(id);
            if (service == null)
            {
                result.Message = "سرویسی با این شناسه یافت نشد.";
                return result;
            }

            // در صورت وجود وابستگی‌های خاص (BillOfLadings یا ...) بهتر است قبل از حذف بررسی شوند.
            _db.Cu_Services.Remove(service);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "سرویس با موفقیت حذف شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان حذف سرویس رخ داده است.\n {ex.Message}";
            }

            return result;
        }

        //================================================================================== Route
        public async Task<List<RouteDto>> GetRoutesAsync(long sellerId)
        {
            var routes = await _db.Cu_Routes
                .Include(n => n.DestinationCity).ThenInclude(n => n.Province)
                .Include(n => n.OriginCity).ThenInclude(n => n.Province)
                .Include(n => n.Zone)
                .Where(r => r.SellerId == sellerId)
                .Select(r => new RouteDto
                {
                    RouteId = r.RouteId,
                    SellerId = r.SellerId,
                    RouteCode = r.RouteCode,
                    RouteName = r.RouteName,
                    RouteName_En = r.RouteName_En,
                    IsActive = r.IsActive,
                    IsTransit = r.IsTransit,
                    OriginCityId = r.OriginCityId,
                    DestinationCityId = r.DestinationCityId,
                    ZoneId = r.ZoneId,
                    ZoneName = r.Zone.Name,
                    OriginCity = r.OriginCity.Province.PersianName + " - " + r.OriginCity.PersianName,
                    DestinationCity = r.DestinationCity.Province.PersianName + " - " + r.DestinationCity.PersianName,
                })
                .OrderBy(r => r.RouteName)
                .ToListAsync();

            return routes;
        }
        public async Task<RouteDto> FindRouteByIdAsync(int id)
        {
            var route = await _db.Cu_Routes.FirstOrDefaultAsync(r => r.RouteId == id);
            if (route == null)
            {
                return null!;
            }

            var dto = new RouteDto
            {
                RouteId = route.RouteId,
                RouteCode = route.RouteCode,
                SellerId = route.SellerId,
                RouteName = route.RouteName,
                RouteName_En = route.RouteName_En,
                IsActive = route.IsActive,
                OriginCityId = route.OriginCityId,
                DestinationCityId = route.DestinationCityId,
                ZoneId = route.ZoneId,
                IsTransit = route.IsTransit,
            };
            return dto;
        }
        public async Task<clsResult> AddRouteAsync(RouteDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null)
            {
                result.Message = "اطلاعات به درستی وارد نشده است.";
                return result;
            }

            bool isDuplicate = await _db.Cu_Routes.AnyAsync(r =>
                r.RouteName == dto.RouteName && r.SellerId == dto.SellerId);
            if (isDuplicate)
            {
                result.Message = "نام مسیر تکراری است.";
                return result;
            }

            var route = new Cu_Route
            {
                RouteCode = dto.RouteCode,
                SellerId = dto.SellerId,
                RouteName = dto.RouteName,
                RouteName_En = dto.RouteName_En,
                IsActive = dto.IsActive,
                OriginCityId = dto.OriginCityId,
                DestinationCityId = dto.DestinationCityId,
                ZoneId = dto.ZoneId,
                IsTransit = dto.IsTransit,
            };

            _db.Cu_Routes.Add(route);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "مسیر جدید با موفقیت ثبت شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> UpdateRouteAsync(RouteDto dto)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            if (dto == null)
            {
                result.Message = "اطلاعات به درستی وارد نشده است.";
                return result;
            }

            var route = await _db.Cu_Routes.FindAsync(dto.RouteId);
            if (route == null)
            {
                result.Message = "مسیری با این شناسه یافت نشد.";
                return result;
            }

            bool isDuplicate = await _db.Cu_Routes.AnyAsync(r =>
                r.RouteId != dto.RouteId &&
                r.SellerId == dto.SellerId &&
                r.RouteName == dto.RouteName
            );
            if (isDuplicate)
            {
                result.Message = "نام مسیر تکراری است.";
                return result;
            }

            route.SellerId = dto.SellerId;
            route.RouteName = dto.RouteName;
            route.RouteName_En = dto.RouteName_En;
            route.IsActive = dto.IsActive;
            route.OriginCityId = dto.OriginCityId;
            route.DestinationCityId = dto.DestinationCityId;
            route.ZoneId = dto.ZoneId;
            route.RouteCode = dto.RouteCode;
            route.IsTransit = dto.IsTransit;

            _db.Cu_Routes.Update(route);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "اطلاعات مسیر با موفقیت به‌روزرسانی شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان به‌روزرسانی اطلاعات رخ داده است.\n {ex.Message}";
            }

            return result;
        }
        public async Task<clsResult> DeleteRouteAsync(int id)
        {
            clsResult result = new clsResult { Success = false, ShowMessage = true };

            var route = await _db.Cu_Routes.FindAsync(id);
            if (route == null)
            {
                result.Message = "مسیری با این شناسه یافت نشد.";
                return result;
            }

            _db.Cu_Routes.Remove(route);

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "مسیر با موفقیت حذف شد.";
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان حذف مسیر رخ داده است.\n {ex.Message}";
            }

            return result;
        }
        public async Task<List<RouteDto>> GetRoutesByCityAsync(long sellerId, int cityId)
        {
            var routes = await _db.Cu_Routes.Where(r => r.SellerId == sellerId && (r.OriginCityId == cityId || r.DestinationCityId == cityId))
                .Select(r => new RouteDto
                {
                    RouteId = r.RouteId,
                    SellerId = r.SellerId,
                    RouteName = r.RouteName,
                    RouteName_En = r.RouteName_En,
                    IsActive = r.IsActive,
                    OriginCityId = r.OriginCityId,
                    DestinationCityId = r.DestinationCityId,
                    ZoneId = r.ZoneId
                })
                .OrderBy(r => r.RouteName).ToListAsync();
            return routes;
        }

        public async Task<List<RouteDto>> GetRoutesByOriginCityAsync(long sellerId, int originCityId)
        {
            var routes = await _db.Cu_Routes.Where(r => r.SellerId == sellerId && r.OriginCityId == originCityId)
                .Select(r => new RouteDto
                {
                    RouteId = r.RouteId,
                    SellerId = r.SellerId,
                    RouteName = r.RouteName,
                    RouteName_En = r.RouteName_En,
                    IsActive = r.IsActive,
                    OriginCityId = r.OriginCityId,
                    DestinationCityId = r.DestinationCityId,
                    ZoneId = r.ZoneId
                })
                .OrderBy(r => r.RouteName).ToListAsync();
            return routes;
        }

        public async Task<SelectList> SelectList_RoutesByOriginCityAsync(long sellerId, int originCityId)
        {
            var routes = await _db.Cu_Routes.Where(r => r.SellerId == sellerId && r.OriginCityId == originCityId)
                .Select(r => new RouteDto
                {
                    RouteId = r.RouteId,
                    RouteName = r.RouteName,
                })
                .OrderBy(r => r.RouteName).ToListAsync();

            return new SelectList(routes, "RouteId", "RouteName");
        }

        //================================================================================== Packaging

        public async Task<SelectList> SelectList_PackagesAsync(long sellerId, bool forExport = false)
        {
            var routes = await _db.Cu_Packagings.Where(r => r.SellerId == sellerId && r.ForExport == forExport)
                .Select(r => new
                {
                    value = r.Id,
                    text = r.Name
                })
                .ToListAsync();

            return new SelectList(routes, "value", "text");
        }


    }
}
