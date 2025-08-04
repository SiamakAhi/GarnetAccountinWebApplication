using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.DataTransfer.Dto;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccImportService
    {
        Task<clsResult> GetCodingFromExcelAsync(IFormFile excelFile, long sellerId);
        List<ImportDocDto> GetDocFromExl_Sepidar(IFormFile excelFile);
        List<ImportDocDto> GetDocFromExl_General(IFormFile excelFile);
        List<ImportDocDto> AssignDocumentNumbers(List<ImportDocDto> documents, long sellerId, int periodId);
        Task<clsResult> AddBulkDocsAsync(List<ImportDocDto> documents, string userName, long sellerId, int peropdId);
        Task<clsResult> AddBulkKpDocsAsync(List<ImportSaleDocDto> documents, string userName, long sellerId, int peropdId, int? subsystemId = null);
    }
}
