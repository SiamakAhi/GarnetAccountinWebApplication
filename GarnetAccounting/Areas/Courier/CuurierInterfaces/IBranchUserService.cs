using GarnetAccounting.Areas.Courier.Dto.BranchUserDto;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface IBranchUserService
    {
        Task<List<VmBranchUser>> GetBranchesUserAsync(BranchUserFilterDto filter);
        Task<VmBranchUser> GetBUserAsync(string userId);
        Task<VmBranchUser?> GetBUserByUsernameAsync(string userName);
    }
}
