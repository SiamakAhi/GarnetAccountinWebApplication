using GarnetAccounting.Areas.Warehouse.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Warehouse.WarehouseInterfaces
{
    public interface IWarehouseService
    {
        Task<List<WarehouseDto>> GetWarehousesAsync(long sellerId);
        Task<SelectList> GetWarehousesSelectListAsync(long sellerId);
        Task<clsResult> CreateWarehouseAsync(WarehouseDto warehouseDto);
        Task<clsResult> UpdateWarehouseAsync(WarehouseDto warehouseDto);
        Task<clsResult> DeleteWarehouseAsync(long warehouseId);
        Task<clsResult> SetWarehouseActiveStatusAsync(long warehouseId, bool isActive);
    }
}
