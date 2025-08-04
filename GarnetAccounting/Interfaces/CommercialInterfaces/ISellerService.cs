using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.CommercialViewModel;
using GarnetAccounting.ViewModels.PartyDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Interfaces.CommercialInterfaces
{
    public interface ISellerService
    {
        Task<ResultDto> AddSeller(AddSellerDto dto);
        Task<List<VmSeller>> GetCustomerSellers(int customerId);
        Task<UpdateSellerDto> GetSellerByIdAsync(Int64 SellerId);
        Task<ResultDto> UpdateSeller(UpdateSellerDto dto);
        Task<clsResult> SetOthersSellerInfoAsync(UpdateSellerDto dto);
        Task<SelectList> SelectList_CustomerSellers(int? customerId);
        Task<TaxpayerInfoDto> GetTaxPayerInfoAsync(long sellerId);
        Task<clsResult> UpdateTaxPayerInoAsync(TaxpayerInfoDto dto);
    }
}
