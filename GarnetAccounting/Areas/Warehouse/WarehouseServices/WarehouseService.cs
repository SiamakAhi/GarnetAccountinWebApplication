using GarnetAccounting.Areas.Warehouse.Models.Dtos;
using GarnetAccounting.Areas.Warehouse.Models.Entities;
using GarnetAccounting.Areas.Warehouse.WarehouseInterfaces;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Warehouse.WarehouseServices
{
    public class WarehouseService : IWarehouseService
    {
        private readonly AppDbContext _db;

        public WarehouseService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        // Get list of warehouses
        public async Task<List<WarehouseDto>> GetWarehousesAsync(long sellerId)
        {
            var result = await _db.Wh_Warehouses
                .Where(w => w.SellerId == sellerId)
                .GroupJoin(
                    _db.Acc_Coding_Moeins.Where(m => m.SellerId == sellerId),
                    w => w.MoeinId,
                    m => m.Id,
                    (w, moeins) => new { w, moeins }
                )
                .SelectMany(
                    x => x.moeins.DefaultIfEmpty(),
                    (x, m) => new { x.w, moein = m }
                )
                .GroupJoin(
                    _db.Acc_Coding_Tafsils.Where(t => t.SellerId == sellerId),
                    x => x.w.TafsilId,
                    t => t.Id,
                    (x, tafsils) => new { x.w, x.moein, tafsils }
                )
                .SelectMany(
                    x => x.tafsils.DefaultIfEmpty(),
                    (x, t) => new WarehouseDto
                    {
                        WarehouseId = x.w.WarehouseId,
                        SellerId = x.w.SellerId,
                        WarehouseCode = x.w.WarehouseCode,
                        WarehouseName = x.w.WarehouseName,
                        Address = x.w.Address,
                        Description = x.w.Description,
                        IsActive = x.w.IsActive,
                        MoeinId = x.w.MoeinId,
                        MoeinName = x.moein != null ? x.moein.MoeinName : "-",
                        TafsilId = x.w.TafsilId,
                        TafsilName = t != null ? t.Name : "-"
                    }
                )
                .ToListAsync();

            return result;
        }


        // Get SelectList of warehouses
        public async Task<SelectList> GetWarehousesSelectListAsync(long sellerId)
        {
            var warehouses = await _db.Wh_Warehouses
                .Where(w => w.SellerId == sellerId && w.IsActive)
                .Select(w => new { w.WarehouseId, w.WarehouseName })
                .ToListAsync();

            return new SelectList(warehouses, "WarehouseId", "WarehouseName");
        }

        // Create a new warehouse
        public async Task<clsResult> CreateWarehouseAsync(WarehouseDto warehouseDto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (warehouseDto.MoeinId == 0)
                warehouseDto.MoeinId = null;
            if (warehouseDto.TafsilId == 0)
                warehouseDto.TafsilId = null;

            var warehouse = new Wh_Warehouse
            {
                SellerId = warehouseDto.SellerId,
                WarehouseCode = warehouseDto.WarehouseCode,
                WarehouseName = warehouseDto.WarehouseName,
                Address = warehouseDto.Address,
                Description = warehouseDto.Description,
                IsActive = warehouseDto.IsActive,
                MoeinId = warehouseDto.MoeinId,
                TafsilId = warehouseDto.TafsilId

            };

            _db.Wh_Warehouses.Add(warehouse);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"انبار {warehouse.WarehouseName} با موفقیت ثبت شد";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + ex.Message;
            }

            return result;
        }

        // Update warehouse
        public async Task<clsResult> UpdateWarehouseAsync(WarehouseDto warehouseDto)
        {
            clsResult result = new clsResult();
            var warehouse = await _db.Wh_Warehouses.FindAsync(warehouseDto.WarehouseId);
            if (warehouse == null)
            {
                result.Success = false;
                result.Message = "انبار مورد نظر یافت نشد.";
                return result;
            }
            if (warehouseDto.MoeinId == 0)
                warehouseDto.MoeinId = null;
            if (warehouseDto.TafsilId == 0)
                warehouseDto.TafsilId = null;

            warehouse.WarehouseCode = warehouseDto.WarehouseCode;
            warehouse.WarehouseName = warehouseDto.WarehouseName;
            warehouse.Address = warehouseDto.Address;
            warehouse.Description = warehouseDto.Description;
            warehouse.IsActive = warehouseDto.IsActive;
            warehouse.MoeinId = warehouseDto.MoeinId;
            warehouse.TafsilId = warehouseDto.TafsilId;

            _db.Wh_Warehouses.Update(warehouse);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"انبار {warehouse.WarehouseName} با موفقیت ویرایش شد";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "خطایی در ویرایش اطلاعات رخ داده است. " + ex.Message;
            }

            return result;
        }

        // Delete warehouse
        public async Task<clsResult> DeleteWarehouseAsync(long warehouseId)
        {
            clsResult result = new clsResult();
            var warehouse = await _db.Wh_Warehouses.FindAsync(warehouseId);
            if (warehouse == null)
            {
                result.Success = false;
                result.Message = "انبار مورد نظر یافت نشد.";
                return result;
            }

            _db.Wh_Warehouses.Remove(warehouse);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"انبار {warehouse.WarehouseName} با موفقیت حذف شد";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "خطایی در حذف اطلاعات رخ داده است. " + ex.Message;
            }

            return result;
        }

        // Activate or deactivate warehouse
        public async Task<clsResult> SetWarehouseActiveStatusAsync(long warehouseId, bool isActive)
        {
            clsResult result = new clsResult();
            var warehouse = await _db.Wh_Warehouses.FindAsync(warehouseId);
            if (warehouse == null)
            {
                result.Success = false;
                result.Message = "انبار مورد نظر یافت نشد.";
                return result;
            }

            warehouse.IsActive = isActive;
            _db.Wh_Warehouses.Update(warehouse);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"وضعیت انبار {warehouse.WarehouseName} با موفقیت تغییر یافت";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "خطایی در تغییر وضعیت رخ داده است. " + ex.Message;
            }

            return result;
        }
    }
}
