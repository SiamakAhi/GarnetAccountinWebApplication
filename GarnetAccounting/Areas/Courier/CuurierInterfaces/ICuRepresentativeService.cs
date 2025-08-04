using GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface ICuRepresentativeService
    {
        Task<SelectList> SelectList_OldSys_RepresentativeAsync(long sellerId);
        IQueryable<RepresentativeRate> OldSys_RepresentativeRates(RepFilterDto filter);


        //---------------------------------------------------------------------------
        Task<SelectList> SelectList_RepresentativeAsync(long sellerId);
        Task<SelectList> SelectList_RepresentativeNameAsync(long sellerId);
        Task<List<RepresentativeDto>> GetRepresentativesAsync(long sellerId);
        Task<clsResult> AddRepresentativeAsync(RepresentativeDto dto);
        Task<clsResult> UpdateRepresentativeAsync(RepresentativeDto dto);
        Task<clsResult> DeleteRepresentativeAsync(Guid id);

    }
}
