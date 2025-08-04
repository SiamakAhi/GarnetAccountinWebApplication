using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.AutoServiceAgency.Dto;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccDocCreatorService
    {
        Task<clsResult> CreateBankDocAsync(BankTransactionsCreateDocDto dto);
        Task<clsResult> CreatInvoiceDocAsync(List<Guid> InvoicesId, string username);
        Task<clsResult> CreateModiranReceptionDocAsync(SaveDetailsDto Reception);
        Task<clsResult> CreateLamaryReceptionDocAsync(SaveDetailsDto Reception);
        Task<clsResult> CreateMoadianDocAsync(MoadianCreateDocDto dto);

    }
}
