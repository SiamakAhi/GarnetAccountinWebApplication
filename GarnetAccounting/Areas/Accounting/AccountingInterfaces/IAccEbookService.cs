using GarnetAccounting.Areas.Accounting.Dto;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccEbookService
    {
        Task<bool> IsDocumentsBalancedAsync(long sellerId, int periodId, int minDocNumber, int maxDocNumber);
        Task<bool> AreDocumentsDateOrderValidAsync(long sellerId, int periodId, int minDocNumber, int maxDocNumber);
        Task<List<EBookManagerDto>> GetEbookMetadata(long sellerId, int periodId);

    }
}
