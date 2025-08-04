using GarnetAccounting.Areas.Courier.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface ICourierServiceService
    {
        //========================================================== Service
        Task<SelectList> SelectList_ServicesAsync(long sellerId);
        SelectList ServiceRatingType();
        Task<List<Cu_ServiceDto>> GetServicesAsync(long sellerId);
        Task<Cu_ServiceDto> FindServiceByIdAsync(int id);
        Task<clsResult> AddServiceAsync(Cu_ServiceDto dto);
        Task<clsResult> UpdateServiceAsync(Cu_ServiceDto dto);
        Task<clsResult> DeleteServiceAsync(int id);


        //============================================================ Route
        Task<List<RouteDto>> GetRoutesAsync(long sellerId);
        Task<RouteDto> FindRouteByIdAsync(int id);
        Task<clsResult> AddRouteAsync(RouteDto dto);
        Task<clsResult> UpdateRouteAsync(RouteDto dto);
        Task<clsResult> DeleteRouteAsync(int id);
        Task<List<RouteDto>> GetRoutesByCityAsync(long sellerId, int cityId);
        Task<List<RouteDto>> GetRoutesByOriginCityAsync(long sellerId, int originCityId);
        Task<SelectList> SelectList_RoutesByOriginCityAsync(long sellerId, int originCityId);


        //============================================================= Packaging
        Task<SelectList> SelectList_PackagesAsync(long sellerId, bool forExport = false);



    }
}
