using GarnetAccounting.Areas.Accounting.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccBaseInfoService
    {
        SelectList SelectList_DocTypes();
        SelectList SelectList_Subsystems();
        Task<SelectList> SelectList_PersonsAsync(long sellerId);


        //Bank
        Task<SelectList> SelectList_BanksAsync();
        Task<List<BankDto>> BanksAsync();
        Task<clsResult> AddBankAsync(BankDto dto);
        Task<clsResult> UpdateBankAsync(BankDto dto);
        Task<clsResult> CreateTafsilForBankAsync(int id, long sellerId);

        //Bank Account
        Task<SelectList> SelectList_BankAccountsAsync(long sellerId, bool onlyActive = true);
        Task<BankAccountDto?> GetBankAccountDtoAsync(int id);
        Task<List<BankAccountDto>> BankAccountsAsync(long sellerId);
        Task<clsResult> AddBankAccountAsync(BankAccountDto dto);
        Task<clsResult> UpdateBankAccountAsync(BankAccountDto dto);
        Task<clsResult> SetTafsilToAccountAsync(int accountId, long tafsilId);
        Task<clsResult> RemoveTafsilFromAccountAsync(int accountId);

    }
}
