using Microsoft.AspNetCore.Mvc.Rendering;
using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.CommercialViewModel;

namespace GarnetAccounting.Interfaces.CommercialInterfaces
{
    public interface IBuyerService
    {
        Task<SelectList> SelectList_PartyType();
        Task<ResultDto> AddBuyer(BuyerAddDto dto);
        Task<long> AddBuyerAsync(BuyerAddDto dto);
        Task<ResultDto> AddBulkBuyer(List<BuyerAddDto> listDto);
        Task<ResultDto> UpdateBuyerAsync(BuyerUpdateDto dto);
        Task<BuyerUpdateDto> GetBuyerByIdAsync(Int64 id);
        IQueryable<VmBuyer> GetBuyers(Int64 SellerId, int customerId, string name = "", string NationalCode = "");
        Task<SelectList> SelectList_Buyers(Int64 sellerId);
        Task<bool> IsDupplicateNationalId(Int64 SellerId, string code);
        Task<ResultDto> DeleteBuyerAsync(Int64 id);
    }
}
