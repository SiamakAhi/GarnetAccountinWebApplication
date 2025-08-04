using GarnetAccounting.Areas.DataTransfer.Dto;
using GarnetAccounting.Areas.DataTransfer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.DataTransfer.DataTransferInterfaces
{
    public interface IKPDataTransferService
    {
        Task<clsResult> ImportFromExcelAsync(IFormFile ExcelFile, long sellerId);
        Task<SelectList> SelectList_DestinationRepresentativesAsync(long sellerId);
        Task<SelectList> SelectList_AgencyAsync(long sellerId);
        Task<List<KPOldSystemSaleReport>> GetSalesAsync(SaleFilterDto filter);
        Task<KPOldSystemSaleReport?> FindBillofladdingByIdAsync(long Id);
        Task<bool> isDupplicateBillOfLandingAsync(long landingNulmber, long sellerId);
        Task<List<ImportSaleDocDto>> PrepareSalesForAccountingAsync(long[] ids, long sellerId);
        Task<clsResult> DeleteBillOfLandingsAsync(long[] ids);
        //
        Task<VmBillOfLandingMonitor> BillsMonitorAsync(long sellerId);
        Task<clsResult> SetErrorAsync(long id, string error);
        Task<List<KPOldSystemSaleReport>> GetSalesErrorsAsync(SaleFilterDto filter);
        //
        Task<List<KPOldSystemSaleReport>> GetIncomingBillsOfLadingAsync(SaleFilterDto filter);
        IQueryable<KPOldSystemSaleReport> GetIncomingBillsOfLadings(SaleFilterDto filter);
        Task<List<KPOldSystemSaleReport>> GetOutgoingBillsOfLadingAsync(SaleFilterDto filter);
        Task<List<ExportMoadianDto>> GetBillofladingByListIdAsync(List<long> Ids);
    }
}
