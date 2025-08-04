using GarnetAccounting.Areas.AvaRasta.ViewModels;
using GarnetAccounting.Areas.CustomerArea.Dto;
using GarnetAccounting.ViewModels;

namespace GarnetAccounting.Areas.AvaRasta.Interfaces
{
    public interface ICostomerService
    {
        IQueryable<VmCustomer> GetCustomers(string name = "");
        Task<VmCustomer> GetVmCustomerByIdAsync(int Id);
        Task<ResultDto> AddCustomerAsync(VmCustomer n);
        Task<ResultDto> UpdateCustomerAsync(VmCustomer n);
        Task<ResultDto> DeleteCustomerAsync(int id);
        Task<List<VmCustomerUsers>> GetCustomerUsersAsync(int? CustomerId);
    }
}
