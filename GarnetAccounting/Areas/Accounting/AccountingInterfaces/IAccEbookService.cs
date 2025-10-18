using GarnetAccounting.Areas.Accounting.Dto;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccEbookService
    {
        Task<bool> IsDocumentsBalancedAsync(long sellerId, int periodId, int minDocNumber, int maxDocNumber);
        Task<bool> IsDocumentsBalancedAsync(long sellerId, int periodId, DateTime mindate, DateTime maxdate);
        Task<bool> AreDocumentsDateOrderValidAsync(long sellerId, int periodId, int minDocNumber, int maxDocNumber);
        Task<bool> AreDocumentsDateOrderValidAsync(long sellerId, int periodId, DateTime mindate, DateTime maxdate);

        Task<List<EBookManagerDto>> GetEbookMetadata(long sellerId, int periodId);
        Task<EBookManagerDto> GetEbookAsync(EBookManagerDto dto);

    }
}
