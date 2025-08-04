using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Treasury.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccBaseInfoService : IAccBaseInfoService
    {
        private readonly AppDbContext _db;
        private readonly IAccCodingService _coding;
        public AccBaseInfoService(AppDbContext db
            , IAccCodingService coding)
        {
            _db = db;
            _coding = coding;
        }

        public SelectList SelectList_DocTypes()
        {
            List<SelectListDto> types = new List<SelectListDto>();
            types.Add(new SelectListDto() { Id = 1, Name = "یادداشت" });
            types.Add(new SelectListDto() { Id = 2, Name = "موقت" });
            types.Add(new SelectListDto() { Id = 3, Name = "دائم" });

            return new SelectList(types, "Id", "Name");
        }
        public SelectList SelectList_Subsystems()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Accounting", "حسابداری");
            lst.Add("Khazaneh", "خزانه");
            lst.Add("Sale", "فروش");
            lst.Add("Buy", "خرید");
            lst.Add("Warehouse", "انبار");
            lst.Add("Contract", "قراردادها");
            lst.ToList();
            return new SelectList(lst, "Key", "Value");

        }
        public async Task<SelectList> SelectList_PersonsAsync(long sellerId)
        {
            var lst = await _db.parties.Where(n => n.uyer_SellerId == sellerId)
                .Select(n => new { id = n.Id, name = n.Name + " " + n.NationalId })
                .OrderBy(n => n.name)
                .ToListAsync();

            return new SelectList(lst, "id", "name");
        }

        //Bank
        public async Task<SelectList> SelectList_BanksAsync()
        {
            var lst = await _db.Banks
                .Select(n => new { id = n.Id, name = n.Name })
                .OrderBy(n => n.name)
                .ToListAsync();

            return new SelectList(lst, "id", "name");

        }
        public async Task<List<BankDto>> BanksAsync()
        {
            var banks = await _db.Banks.Select(n => new BankDto
            {
                Id = n.Id,
                Name = n.Name,
                TafsilCode = n.TafsilCode,
                TafsilId = n.TafsilId,

            }).OrderBy(n => n.Name).ToListAsync();
            return banks;
        }
        public async Task<clsResult> AddBankAsync(BankDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (dto == null)
            {
                result.Message = "اطلاعاتی یافت نشد";
                return result;
            }
            if (_db.Banks.Any(n => n.Name == dto.Name))
            {
                result.Message = $"بانک {dto.Name} در لیست بانک ها وجود دارد";
                return result;
            }

            kh_Bank bank = new kh_Bank();
            bank.Name = dto.Name;

            _db.Banks.Add(bank);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> UpdateBankAsync(BankDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (_db.Banks.Where(n => n.Id != dto.Id).Any(n => n.Name == dto.Name))
            {
                result.Message = $"بانک {dto.Name} در لیست بانک ها وجود دارد";
                return result;
            }

            kh_Bank bank = await _db.Banks.FindAsync(dto.Id);
            if (bank == null)
            {
                result.Message = "اطلاعاتی یافت نشد";
                return result;
            }
            bank.Name = dto.Name;
            _db.Banks.Update(bank);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است. " + x.Message;
            }

            return result;
        }
        public async Task<clsResult> CreateTafsilForBankAsync(int id, long sellerId)
        {
            clsResult result = new clsResult();
            result.Success = false;

            var bank = await _db.Banks.FindAsync(id);

            if (bank == null)
            {
                result.Message = $"داده ای یافت نشد";
                return result;
            }
            AutoAddTafsilDto dto = new AutoAddTafsilDto();
            dto.SellerId = sellerId;
            dto.GroupId = 2;
            dto.Name = bank.Name;
            var addResult = await _coding.AutoAddTafsilAsync(dto);
            if (addResult.Success)
            {
                bank.TafsilId = addResult.TafsilId;
                bank.TafsilCode = addResult.TafsilCode;
                _db.Banks.Update(bank);

                try
                {
                    _db.SaveChanges();
                    result.Success = true;
                    result.Message = $"عملیات با موفقیت انجام شد";
                    result.updateType = 1;

                }
                catch (Exception x)
                {
                    result.Success = false;
                    result.Message = "خطایی در انجام عملیات رخ داده است. " + x.Message;
                }

            }
            result.Success = addResult.Success;
            result.Message = addResult.Message;

            return result;

        }

        //Bank Account
        public async Task<SelectList> SelectList_BankAccountsAsync(long sellerId, bool onlyActive = true)
        {
            var data = await _db.BankAccounts.Where(n =>
            n.SellerId == sellerId
            && (onlyActive == true ? n.IsActive == true : n.Id > 0)
            ).Select(n => new
            {
                id = n.Id,
                name = n.AccountName + " " + n.AccountNumber
            }).ToListAsync();

            return new SelectList(data, "id", "name");
        }
        public async Task<BankAccountDto?> GetBankAccountDtoAsync(int id)
        {
            var accont = await _db.BankAccounts.SingleOrDefaultAsync(n => n.Id == id);
            if (accont == null)
                return null;

            BankAccountDto dto = new BankAccountDto();

            dto.AccountName = accont.AccountName;
            dto.AccountNumber = accont.AccountNumber;
            dto.AccountType = accont.AccountType;
            dto.BankAddress = accont.BankAddress;
            dto.BankId = accont.BankId;
            dto.BranchCode = accont.BranchCode;
            dto.CardNumber = accont.CardNumber;
            dto.cvvt = accont.cvvt;
            dto.IsActive = accont.IsActive;
            dto.SHABA = accont.SHABA;
            dto.CardDate = accont.CardDate;
            dto.MoeinId = accont.MoeinId;
            return dto;

        }
        public async Task<List<BankAccountDto>> BankAccountsAsync(long sellerId)
        {
            var data = await _db.BankAccounts.Where(n => n.SellerId == sellerId)
                .Select(n => new BankAccountDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    AccountName = n.AccountName,
                    AccountNumber = n.AccountNumber,
                    AccountType = n.AccountType,
                    SHABA = n.SHABA,
                    BankId = n.BankId,
                    BankAddress = n.BankAddress,
                    BankName = n.Bank.Name,
                    BranchCode = n.BranchCode,
                    CardDate = n.CardDate,
                    CardNumber = n.CardNumber,
                    cvvt = n.cvvt,
                    IsActive = n.IsActive,
                    TafsilCode = n.TafsilCode,
                    TafsilId = n.TafsilId,
                    MoeinId = n.MoeinId,

                }).ToListAsync();

            return data;
        }
        public async Task<clsResult> AddBankAccountAsync(BankAccountDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            kh_BankAccount x = new kh_BankAccount
            {
                AccountName = dto.AccountName,
                AccountNumber = dto.AccountNumber,
                AccountType = dto.AccountType,
                BankAddress = dto.BankAddress,
                BankId = dto.BankId,
                BranchCode = dto.BranchCode,
                CardNumber = dto.CardNumber,
                cvvt = dto.cvvt,
                IsActive = dto.IsActive,
                SellerId = dto.SellerId,
                SHABA = dto.SHABA,
                CardDate = dto.CardDate,
                MoeinId = dto.MoeinId,
            };

            _db.BankAccounts.Add(x);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception er)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n. " + er.Message;
            }

            return result;
        }
        public async Task<clsResult> UpdateBankAccountAsync(BankAccountDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            kh_BankAccount? x = await _db.BankAccounts.FindAsync(dto.Id);
            if (x == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }

            x.AccountName = dto.AccountName;
            x.AccountNumber = dto.AccountNumber;
            x.AccountType = dto.AccountType;
            x.BankAddress = dto.BankAddress;
            x.BankId = dto.BankId;
            x.BranchCode = dto.BranchCode;
            x.CardNumber = dto.CardNumber;
            x.cvvt = dto.cvvt;
            x.IsActive = dto.IsActive;
            x.SHABA = dto.SHABA;
            x.CardDate = dto.CardDate;
            x.MoeinId = dto.MoeinId;
            _db.BankAccounts.Update(x);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception er)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n. " + er.Message;
            }

            return result;
        }
        public async Task<clsResult> SetTafsilToAccountAsync(int accountId, long tafsilId)
        {
            clsResult result = new clsResult();
            result.Success = false;

            kh_BankAccount? x = await _db.BankAccounts.FindAsync(accountId);
            if (x == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }
            var tafsil = await _coding.FindTafsilAsync(tafsilId);
            x.TafsilCode = tafsil.Code;
            x.TafsilId = tafsilId;


            _db.BankAccounts.Update(x);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception er)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n. " + er.Message;
            }

            return result;
        }
        public async Task<clsResult> RemoveTafsilFromAccountAsync(int accountId)
        {
            clsResult result = new clsResult();
            result.Success = false;

            kh_BankAccount? x = await _db.BankAccounts.FindAsync(accountId);
            if (x == null)
            {
                result.Message = "اطلاعات موردنظر یافت نشد";
                return result;
            }
            x.TafsilCode = null;
            x.TafsilId = null;


            _db.BankAccounts.Update(x);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = $"عملیات با موفقیت انجام شد";
            }
            catch (Exception er)
            {
                result.Success = false;
                result.Message = "خطایی در ثبت اطلاعات رخ داده است \n. " + er.Message;
            }

            return result;
        }
    }
}
