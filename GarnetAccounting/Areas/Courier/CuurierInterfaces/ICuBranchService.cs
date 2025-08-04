using GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface ICuBranchService
    {
        Task<SelectList> SelectList_RepresentativeBranchesAsync(Guid id);
        Task<List<BranchDto>> GetBranchesAsync(long sellerId);
        Task<BranchDto> FindBranchByIdAsync(Guid id);
        Task<clsResult> AddBranchAsync(BranchDto dto);
        Task<clsResult> UpdateBranchAsync(BranchDto dto);
        Task<clsResult> DeleteBranchAsync(Guid id);

    }
}
