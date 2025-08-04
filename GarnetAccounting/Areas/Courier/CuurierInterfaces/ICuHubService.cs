using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface ICuHubService
    {
        Task<SelectList> SelectList_HubsAsync(long sellerId);
        Task<List<HubDto>> GetHubsAsync(long sellerId);
        Task<Cu_Hub> FindByIdAsync(Guid id);
        Task<HubDto> GetHubByIdAsync(Guid id);
        Task<clsResult> CreateHubAsync(HubDto dto);
        Task<clsResult> UpdateHubAsync(HubDto dto);
        Task<clsResult> DeleteHubAsync(Guid id);
    }
}