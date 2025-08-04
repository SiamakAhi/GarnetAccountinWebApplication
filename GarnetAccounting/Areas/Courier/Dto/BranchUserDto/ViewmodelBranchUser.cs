namespace GarnetAccounting.Areas.Courier.Dto.BranchUserDto
{
    public class ViewmodelBranchUser
    {
        public BranchUserFilterDto filter { get; set; } = new BranchUserFilterDto();
        public List<VmBranchUser> Users { get; set; }
    }
}
