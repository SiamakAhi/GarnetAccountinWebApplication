using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccEndOfPeriodService
    {
        Task<FinancePeriodsDto> FinancePeriodDto(int id);
        Task<SelectList> SelectList_GroupAccountsAsync(long sellerId, Int16 typeid);
        Task<SelectList> SelectList_TemporaryAccounts_KolAsync(long sellerId);
        Task<SelectList> SelectList_TemporaryAccounts_MoeinAsync(long sellerId);
        Task<SelectList> SelectList_PermanentAccounts_KolAsync(long sellerId);
        Task<SelectList> SelectList_PermanentAccounts_MoeinAsync(long sellerId);
        Task<SelectList> SelectList_AllAccounts_MoeinAsync(long sellerId);
        //
        bool IsDocumentBalanced(Acc_Document document);
        Task<bool> AreDocumentsBalancedAsync(long sellerId, int periodId);
        Task<bool> AreDocumentDatesInOrderAsync(long sellerId, int periodId);
        Task<clsResult> AreAllDocumentsApprovedAsync(long sellerId, int periodId);
        Task<clsResult> UpdateDocumentStatusAsync(Guid documentId, short newStatusId, string userName);
        Task<clsResult> UpdateDocumentsStatusAsync(Guid[] documentIds, short newStatusId, string userName);
        Task<clsResult> FinalizeAllDocumentsAsync(long sellerId, int periodId, string userName);
        //
        Task<List<DocArticleDto>> CloseTemporaryPreviewAsync(EndOfPeriodSettings dto);
        Task<List<DocArticleDto>> CloseTemporaryPreviewOldAsync(EndOfPeriodSettings dto);
        Task<List<DocArticleDto>> ClosePermanentAccountsPreviewAsync(EndOfPeriodSettings dto);
        Task<clsResult> ClosePermanentAccountsAsync(EndOfPeriodSettings dto);

        //-----------------------------
        Task<clsResult> ClosingTemproryAccountsAsync(EndOfPeriodSettings dto);

        Task<clsResult> OpeningDodPreviewAsync(long sellerId, int periodId);
        Task<clsResult> InsertOpeningDocAsync(InsertOpeningDocDto dto);
    }
}
