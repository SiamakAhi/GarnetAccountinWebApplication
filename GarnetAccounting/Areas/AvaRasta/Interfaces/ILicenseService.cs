using GarnetAccounting.Areas.AvaRasta.Dto;
using GarnetAccounting.Areas.AvaRasta.Models.Entities;

namespace GarnetAccounting.Areas.AvaRasta.Interfaces
{
    public interface ILicenseService
    {
        Task<clsResult> AddLicenseAsync(AddLicenseDto dto);
        Task<List<License>> GetLicensesByCustomerIdAsync(int customerId);
        Task<clsResult> RemoveLicenseAsync(Guid licenseId);
    }
}
