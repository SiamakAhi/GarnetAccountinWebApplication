using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.Courier.Dto.BranchUserDto;
using GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos;

namespace GarnetAccounting.Areas.Representatives.Dtos
{
    public class VmBranchUserPanel
    {
        public BillOfLadingFilterDto filter { get; set; } = new BillOfLadingFilterDto();
        public BranchDto Branch { get; set; }
        public VmBranchUser CurrentUser { get; set; }
        public Pagination<ViewBillOfLadings> BillsOut { get; set; }
        public Pagination<ViewBillOfLadings> BillsIn { get; set; }

    }
}
