using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos;
using GarnetAccounting.Areas.Accounting.Dto.PrintDto;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccountingReportService
    {

        Task<MinMaxDocNumberDto> GetMinMaxDocNumberAsync(long SellerId, int PeriodId);
        Task<ArticleAccountInfo> GetArticleAccountInfoAsync(DocArticleDto article);
        Task<bool> HasAccountInLevelAsync(long sellerId, int periodId, int moeinId, int tafsilLevel, long? tafsilId = null);
        Task<List<Report_BrowserDto>> Report_KolAsync(DocFilterDto filter);
        Task<List<Report_BrowserDto>> Report_MoeinAsync(DocFilterDto filter);
        Task<List<Report_BrowserDto>> Report_Tafsil4Async(DocFilterDto filter);
        Task<List<Report_BrowserDto>> Report_TafsilAsync(DocFilterDto filter);
        Task<List<DocArticleDto>> Report_BrowserArticlesAsync(DocFilterDto filter);
        Task<List<DocArticleDto>> Report_ArticlesAsync(DocFilterDto filter);
        Task<List<DocArticleDto>> GetArticlesAsync(DocFilterDto filter);
        IQueryable<DocArticleDto> GetArticlesQuery(DocFilterDto filter);
        Task<List<DocArticleDto>> GetSimpleArticlesAsync(DocFilterDto filter);
        //گردش حساب ها

        Task<List<VmTurnoverAccount>> TurnoverAccounts_TafsilAsync(DocFilterDto filter);
        Task<List<VmTurnoverAccount>> TurnoverAccounts_MoeinAsync(DocFilterDto filter);

        //
        Task<List<TarazAzmayeshiDto>> GetTrialBalanceAsync(DocFilterDto filter);
        Task<List<TarazAzmayeshiDto>> GetTrialBalance_6ColAsync(DocFilterDto filter);
        Task<List<RooznamehDto>> DafarRooznamehAsync(long sellerId, int periodId, int rowsInPage);
        Task<List<RooznamehDto>> DafarRooznamehByKolAsync(long sellerId, int periodId, int rowsInPage);
        Task<List<DaftarKolDto>> DaftarKolAsync(long sellerId, int periodId, int rowsInPage);
        Task<byte[]> GenerateTrialBalanceReportAsync(DocFilterDto filter);
        //Tafsil Reports
        Task<List<TafsilReportDto>> TafsilReportAsync(long sellerId
            , int period
            , long tafsilId
            , long? tafsil5Id
            , long? tafsil6Id
            , string? startDate = null
            , string? endDate = null
            , int[]? Moeins = null);
        Task<VmTafsilReport> PersonBalaceAsync(TafsilReportFilterDto filter);
        Task<VmTafsilReport> GetTafsilBalaceAsync(TafsilReportFilterDto filter);

        Task<VmTafsilReport> Tafsil4MoeinTurnoverAsync(TafsilReportFilterDto filter);

        Task<List<TrialBalancePrintDto>> GetTrialBalanceForPrintAsync(DocFilterDto filter);
        Task<List<TrialBalancePrintDto>> GetTrialBalance6ForPrintAsync(DocFilterDto filter);

        Task<List<BalanceDataDto>> GetBalance(long sellerId, int period, DateTime? date = null);
        Task<DocumentsInfo> GetDocumentsInfoAsync(long sellerId, int period);

        //--------------------------------
        Task<VmTafsilReport> GetTafsilGroupedTurnoverAsync(TafsilReportFilterDto filter);
        Task<VmTafsilReport> GetTafsilTurnoverAsync(TafsilReportFilterDto filter);

        Task<List<AccountsReport>> GetAccountsCascadingReportAsync(DocFilterDto filter);
    }
}
