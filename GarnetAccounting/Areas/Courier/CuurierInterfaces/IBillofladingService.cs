using GarnetAccounting.Areas.Courier.Dto;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface IBillofladingService
    {
        Task<string> GenerateBillNumberAsync(long sellerId, string branchCode);
        Task<clsResult> CreateNewBillOfLadingAsync(BillOfLadingDto_Header dto);
        Task<BillOfLadingDto> GetBillOfLadingDtoAsync(Guid id);
        IQueryable<ViewBillOfLadings> GetBillsAsQuery(BillOfLadingFilterDto filter);
    }
}
