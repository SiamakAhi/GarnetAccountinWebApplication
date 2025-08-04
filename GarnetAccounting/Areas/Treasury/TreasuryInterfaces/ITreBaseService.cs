using GarnetAccounting.Areas.Accounting.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Treasury.TreasuryInterfaces
{
    public interface ITreBaseService
    {
        Task<SelectList> SelectList_BanksAsync();
        Task<SelectList> SelectList_BankAccountsAsync();
        Task<List<BankAccountDto>> GetBankAccountsByBankIdAsync(int bankId);
    }
}
