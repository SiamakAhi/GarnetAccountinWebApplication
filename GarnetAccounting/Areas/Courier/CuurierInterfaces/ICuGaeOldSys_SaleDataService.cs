using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Areas.DataTransfer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface ICuGaeOldSys_SaleDataService
    {
        Task<SelectList> SelectList_BillOfLadingGroupAsync(long sellerId);
        Task<SelectList> SelectList_DestinationRepresentativesAsync(long sellerId);
        Task<SelectList> SelectList_AgencyAsync(long sellerId);

        //...........................................................................
        Task<List<KPOldSystemSaleReport>> GetSalesAsync(SaleFilterDto filter);
        IQueryable<KPOldSystemSaleReport> GetSalesAsQuery(SaleFilterDto filter);

        IQueryable<CuOld_SaleReportGrouped> DailyReport(SaleFilterDto filter);
        IQueryable<CuOld_SaleReportGrouped> DailyReportByRepresentative(SaleFilterDto filter);
        IQueryable<CuOld_SaleReportGrouped> RepresentativeReportAsync(SaleFilterDto filter);

    }
}
