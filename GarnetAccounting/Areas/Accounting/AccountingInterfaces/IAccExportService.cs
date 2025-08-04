using GarnetAccounting.Areas.Accounting.Dto;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccExportService
    {
        Task<byte[]> Export_browserKolAsync(DocFilterDto filter);
        Task<byte[]> Export_browserMoeinAsync(DocFilterDto filter);
        Task<byte[]> Export_browserTafsilAsync(DocFilterDto filter);
        Task<byte[]> Export_DocArticlesAsync(DocFilterDto filter);
    }
}
