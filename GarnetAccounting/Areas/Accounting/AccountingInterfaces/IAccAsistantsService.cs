using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Dto.Moadian;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccAsistantsService
    {
        SelectList SelectList_MoadianInvoiceStatuses();
        IQueryable<Acc_MoadianReport> GetMoadianReport(MoadianReportFilterDto filter);
        Task<clsResult> InsertMoadianReportAsync(long sellerId, List<Acc_MoadianReport> report);
        Task<clsResult> insertMoadianReportFromExcelAsync(MoadianImporterDto dto);
        Task<List<Acc_MoadianReport>> ReadMoadianReportFromExcelAsync(IFormFile file);
        Task<clsResult> DeleteMoadianAsync(List<long> Id);
        Task<List<GarnetMoadianOutput>> ReadGarnetMoadianFromExcelAsync(IFormFile file);
        Task<BulkDocDto> PreparingToCreateSaleMoadianDocAsync(List<Acc_MoadianReport> report, bool appendToDoc, long sellerId, int periodId, string currentUser);
        Task<BulkDocDto> PreparingToCreateBuyMoadianDocAsync(List<Acc_MoadianReport> report, bool appendToDoc, long sellerId, int periodId, string currentUser);
        Task<BulkDocDto> PreparingToCreateGarnetMoadianDocAsync(List<GarnetMoadianOutput> report, bool isSale, long sellerId, int periodId, string currentUser);
        Task<BulkDocDto> PreparingToCreateGarnetMoadianDailyDocAsync(List<GarnetMoadianOutput> report, bool isSale, long sellerId, int periodId, string currentUser);
        Task<clsResult> InsertBulkDocsAsync(BulkDocDto dto);
        Task<clsResult> BankTransactionSaveAsCheckedAsync(List<long> items);
        Task<clsResult> DeleteBankTransactionAsync(List<long> items);
        Task<BankTransactionEditDto> GetBankTransactionByIdAsync(long Id);
        Task<clsResult> BankEditTransactionUserCommentAsync(BankTransactionEditDto dto);
        Task<clsResult> BankTransactionCheckTogleAsync(long Id);
        Task<List<MoadianExportDto>> CreateMoadianReport(DocFilterDto filter);
        Task<clsResult> ConvertAccountsAsync(ConvertAccountsDto dto);
        Task<clsResult> MergeDocDaytodayAsync(long sellerId, int periodId);

    }
}
