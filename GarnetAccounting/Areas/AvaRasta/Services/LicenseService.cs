using GarnetAccounting.Areas.AvaRasta.Dto;
using GarnetAccounting.Areas.AvaRasta.Interfaces;
using GarnetAccounting.Areas.AvaRasta.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.AvaRasta.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly AppDbContext _db;

        public LicenseService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<clsResult> AddLicenseAsync(AddLicenseDto dto)
        {
            var result = new clsResult();

            var license = new License
            {
                CustomerId = dto.CustomerId,
                ModuleId = dto.ModuleId,
                PurchaseDate = dto.PurchaseDate,
                ExpirationDate = dto.ExpirationDate
            };

            _db.Licenses.Add(license);
            await _db.SaveChangesAsync();

            result.Success = true;
            result.Message = "License added successfully";
            result.ShowMessage = true;
            return result;
        }

        public async Task<List<License>> GetLicensesByCustomerIdAsync(int customerId)
        {
            return await _db.Licenses.Where(l => l.CustomerId == customerId).ToListAsync();
        }

        public async Task<clsResult> RemoveLicenseAsync(Guid licenseId)
        {
            var result = new clsResult();
            var license = await _db.Licenses.FindAsync(licenseId);
            if (license == null)
            {
                result.Success = false;
                result.Message = "License not found";
                result.ShowMessage = true;
                return result;
            }

            _db.Licenses.Remove(license);
            await _db.SaveChangesAsync();

            result.Success = true;
            result.Message = "License removed successfully";
            result.ShowMessage = true;
            return result;
        }
    }
}
