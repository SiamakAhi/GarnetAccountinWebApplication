using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Treasury.TreasuryInterfaces;
using GarnetAccounting.Models;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Treasury.TreasuryServices
{
    public class TreBaseService : ITreBaseService
    {

        private readonly AppDbContext _db;
        private readonly UserContextService _user;
        long? _sellerId;

        public TreBaseService(AppDbContext dbContext, UserContextService userContextService)
        {
            _db = dbContext;
            _user = userContextService;

            _sellerId = _user.SellerId;
        }

        public async Task<SelectList> SelectList_BanksAsync()
        {
            var banks = await _db.Banks
               .Select(n => new { id = n.Id, name = n.Name }).ToListAsync();

            return new SelectList(banks, "id", "name");
        }

        public async Task<SelectList> SelectList_BankAccountsAsync()
        {
            var banks = await _db.BankAccounts.Where(n => n.SellerId == _sellerId.Value)
               .Select(n => new { id = n.Id, name = "بانک " + n.Bank.Name + " - " + n.AccountName + "-" + n.AccountNumber }).ToListAsync();

            return new SelectList(banks, "id", "name");
        }
        public async Task<List<BankAccountDto>> GetBankAccountsByBankIdAsync(int bankId)
        {
            var accounts = await _db.BankAccounts.Include(n => n.Bank).Where(n => n.BankId == bankId && n.SellerId == _sellerId.Value)
               .Select(n => new BankAccountDto
               {
                   Id = n.Id,
                   AccountName = n.Bank.Name + " - " + n.AccountNumber + " به نام " + n.AccountName
               }).ToListAsync();

            return accounts;
        }
    }
}
