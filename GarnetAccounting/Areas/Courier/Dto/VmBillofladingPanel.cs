using GarnetAccounting.Areas.Courier.Dto.BranchUserDto;
using GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class VmBillofladingPanel
    {
        public BranchDto? Branch { get; set; }
        public VmBranchUser? CurrentUser { get; set; }
        public BillOfLadingDto BillOfLading { get; set; }
        public ConsigmentDto? Consigmen { get; set; }
    }
}
