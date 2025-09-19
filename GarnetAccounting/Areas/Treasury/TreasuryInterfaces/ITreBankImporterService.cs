using GarnetAccounting.Areas.Treasury.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Treasury.TreasuryInterfaces
{
    public interface ITreBankImporterService
    {
        SelectList Select_list_BankReportType();
        IQueryable<TreBankTransactionDto> GetAllTreBankTransactions(long sellerId, long accountId, DateTime fromDate, DateTime untilDate);
        Task<List<TreBankTransactionDto>> GetBankTransactionsAsync(BankReportFilterDto filter);
        Task<clsResult> ImportSamanKotahModatAsync(BankImporterDto dto);
        Task<clsResult> ImportSamanAsync(BankImporterDto dto);
        Task<clsResult> ImportBankSamanSadidAsync(BankImporterDto dto);
        Task<clsResult> ImportTejaratAsync(BankImporterDto dto);
        Task<clsResult> ImportTejaratInternetBankAsync(BankImporterDto dto);
        Task<clsResult> ImportBankTejarat3Async(BankImporterDto dto);
        Task<clsResult> ImportMelatAsync(BankImporterDto dto);
        Task<clsResult> ImportMelat12Async(BankImporterDto dto);
        Task<clsResult> ImportEghtesadNovinAsync(BankImporterDto dto);
        Task<clsResult> ImportEghtesadInternetBankAsync(BankImporterDto dto);
        Task<clsResult> ImportEghtesadInternetBank2Async(BankImporterDto dto);
        Task<clsResult> ImportKeshavarziAsync(BankImporterDto dto);
        Task<clsResult> ImportRefahJariAsync(BankImporterDto dto);
        Task<clsResult> ImportCityBankAsync(BankImporterDto dto);
        Task<clsResult> ImportPostBankAsync(BankImporterDto dto);
        Task<clsResult> ImportSaderat_SepehrAsync(BankImporterDto dto);
        Task<clsResult> ImportSaderatAsync(BankImporterDto dto);
        Task<clsResult> ImportSepahAsync(BankImporterDto dto);
        Task<clsResult> ImportSepahJariAsync(BankImporterDto dto);
        Task<clsResult> ImportBankMeliAsync(BankImporterDto dto);
        Task<clsResult> ImportBankMeli_InternetBankAsync(BankImporterDto dto);
        Task<clsResult> ImportBankMeli3Async(BankImporterDto dto);
        Task<clsResult> ImportSepah_InternetBankAsync(BankImporterDto dto);
        Task<clsResult> ImportPasargadAsync(BankImporterDto dto);
        Task<clsResult> ImportParsianAsync(BankImporterDto dto);
        Task<clsResult> ImportSinaAsync(BankImporterDto dto);
    }
}
