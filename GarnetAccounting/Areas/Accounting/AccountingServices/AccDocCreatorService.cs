using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Areas.AutoServiceAgency.Dto;
using GarnetAccounting.Areas.Commercial.ComercialInterfaces;
using GarnetAccounting.Areas.Commercial.Models.Entities;
using GarnetAccounting.Models;
using GarnetAccounting.Services;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccDocCreatorService : IAccDocCreatorService
    {
        private readonly AppDbContext _db;
        private readonly IAccOperationService _docServic;
        private readonly UserContextService _userContext;
        private readonly IAccCodingService _codingService;
        private readonly IInvoiceService _invoiceService;
        long? _sellerId = null;
        int? _periodId = null;

        public AccDocCreatorService(AppDbContext dbContext
            , IAccOperationService accOperationService
            , UserContextService userContextService
            , IAccCodingService codingService
            , IInvoiceService invoiceService)
        {
            _db = dbContext;
            _userContext = userContextService;
            _docServic = accOperationService;
            _codingService = codingService;
            _invoiceService = invoiceService;
            _sellerId = _userContext.SellerId;
            _periodId = _userContext.PeriodId;
        }
        private clsResult CheckAccountingSettings(Acc_Setting? settings, bool sale, bool buy)
        {
            clsResult result = new clsResult();
            result.Success = true;
            if (settings == null)
                result.Success = false;

            result.Message = "جهت ثبت اسناد خرید و فروش لازم است ابتدا از طریق منوی تنظیمات حسابداری، تنظیمات مربوط به حساب ها را اعمال نمائید.  \n \n \n";
            if (sale)
            {
                if (settings.saleDiscountMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب تحفیفات فروش مشخص نشده است.";
                    result.Success = false;
                }
                if (settings.saleMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب فروش مشخص نشده است.";
                    result.Success = !false;
                }
                if (settings.ReturnToSaleMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب برگشت از فروش مشخص نشده است.";
                    result.Success = false;
                }
                if (settings.SaleVatMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب ارزش افزوده فروش مشخص نشده است.";
                    result.Success = false;
                }
                if (settings.salePartyMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب دریافتنی تجاری (بدهکاران تجاری) مشخص نشده است .";
                    result.Success = false;
                }
            }
            if (buy)
            {
                if (settings.BuyDiscountMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب تحفیفات خرید مشخص نشده است.";
                    result.Success = false;
                }
                if (settings.BuyMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب خرید مشخص نشده است.";
                    result.Success = !false;
                }
                if (settings.ReturnToBuyMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب برگشت از خرید مشخص نشده است.";
                    result.Success = false;
                }
                if (settings.BuyVatMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب ارزش افزوده خرید مشخص نشده است.";
                    result.Success = false;
                }
                if (settings.BuyPartyMoeinId == null)
                {
                    result.Message += "\n در تنظیمات حسابداری، حساب پرداختنی تجاری (بستانکاران تجاری) مشخص نشده است .";
                    result.Success = false;
                }
            }

            return result;

        }
        public async Task<clsResult> CreateBankDocAltAsync(BankTransactionsCreateDocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطاعاتی یافت نشد";

            if (!_sellerId.HasValue)
            {
                result.Message = "شرکت فعال یافت نشد";
                return result;
            }
            if (dto == null) return result;

            if (dto.PeriodId == 0)
            {
                result.Message = "سال مالی فعال یافت نشد";
                return result;
            }
            if (!dto.TransactionsId.Any())
            {
                result.Message = "اطلاعاتی برای ثبت وجود ندارد";
                return result;
            }

            // دریافت اطلاعات و اعتبارسنحی حساب بانکی
            var bankAccount = await _db.BankAccounts.Include(m => m.Moein).SingleOrDefaultAsync(n => n.Id == dto.BankAccountId);
            if (bankAccount == null)
            {
                result.Message = "اطلاعات حساب بانکی یافت نشد";
                return result;
            }
            if (!bankAccount.MoeinId.HasValue)
            {
                result.Message = "حساب معین بانک تعریف نشده است";
                return result;
            }
            if (!bankAccount.TafsilId.HasValue)
            {
                result.Message = "تفصیل حساب بانکی یافت نشد";
                return result;
            }
            if (dto.TransactionsType == 0)
            {
                result.Message = "نوع تراکنش (واریز-برداشت) مشخص نیست";
                return result;
            }
            //=============================================================

            var transactions = await _db.TreBankTransactions
                .Where(n => dto.TransactionsId.Contains(n.Id))
                .OrderBy(n => n.Row).ToListAsync();
            if (transactions == null) return result;

            //--
            List<Acc_Document> Docs = new List<Acc_Document>();
            List<Acc_Article> Articles = new List<Acc_Article>();

            //---- گروه بندی تراکنش ها بر اساس تاریخ تراکنش
            var dateGrouped = transactions.GroupBy(n => n.Date).Select(n => new
            {
                date = n.Key,
                transaction = n

            }).ToList();

            int docAutonumber = await _docServic.DocAutoNumberGeneratorAsync(dto.SellerId, dto.PeriodId);
            int docNumber = await _docServic.DocNumberGeneratorAsync(dto.SellerId, dto.PeriodId);
            int rownumber = 1;
            foreach (var x in dateGrouped)
            {
                //======= Doc Header
                Acc_Document doc = new Acc_Document();
                if (!dto.CreateNewDoc)
                {
                    var query = _db.Acc_Documents.Include(n => n.DocArticles)
                      .Where(n => !n.IsDeleted && n.SellerId == dto.SellerId && n.PeriodId == dto.PeriodId && n.DocDate.Date == x.date.Value.Date).
                    AsQueryable();

                    if (!string.IsNullOrEmpty(dto.DocSelector))
                        query = query.Where(n => n.Description == dto.DocSelector);

                    var docInDate = await query.FirstOrDefaultAsync();

                    if (docInDate != null)
                    {
                        doc = docInDate;
                        if (docInDate.DocArticles != null && docInDate.DocArticles.Any())
                            rownumber = docInDate.DocArticles.Select(m => m.RowNumber).Max() + 1;
                        else
                            rownumber = 1;
                    }

                    else
                    {
                        doc.Id = Guid.NewGuid();
                        doc.SellerId = dto.SellerId;
                        doc.PeriodId = dto.PeriodId;
                        doc.DocDate = x.date.Value;
                        doc.AtfNumber = docAutonumber;
                        doc.AutoDocNumber = docAutonumber;
                        doc.DocNumber = docNumber;
                        doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                        if (!string.IsNullOrEmpty(dto.DocSelector))
                            doc.Description = dto.DocSelector;
                        doc.StatusId = 1;
                        doc.SubsystemId = 2;
                        doc.CreateDate = DateTime.Now;
                        doc.CreatorUserName = dto.UserName;
                        doc.IsDeleted = false;
                        doc.TypeId = 1;
                        Docs.Add(doc);

                        docAutonumber++;
                        docNumber++;
                    }
                }
                else
                {
                    doc.Id = Guid.NewGuid();
                    doc.SellerId = dto.SellerId;
                    doc.PeriodId = dto.PeriodId;
                    doc.DocDate = x.date.Value;
                    doc.AtfNumber = docAutonumber;
                    doc.AutoDocNumber = docAutonumber;
                    doc.DocNumber = docNumber;
                    doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                    if (!string.IsNullOrEmpty(dto.DocSelector))
                        doc.Description = dto.DocSelector;
                    doc.StatusId = 1;
                    doc.SubsystemId = 2;
                    doc.CreateDate = DateTime.Now;
                    doc.CreatorUserName = dto.UserName;
                    doc.IsDeleted = false;
                    doc.TypeId = 1;
                    Docs.Add(doc);

                    docAutonumber++;
                    docNumber++;
                }

                //== End Create Doc Heade =========================================
                var moein = await _db.Acc_Coding_Moeins.FindAsync(dto.MoeinId);

                Acc_Coding_Tafsil tafsil4 = new Acc_Coding_Tafsil();
                if (dto.Tafsil4Id.HasValue)
                    tafsil4 = await _db.Acc_Coding_Tafsils.FindAsync(dto.Tafsil4Id.Value);

                Acc_Coding_Tafsil tafsil5 = new Acc_Coding_Tafsil();
                if (dto.Tafsil5Id.HasValue)
                    tafsil5 = await _db.Acc_Coding_Tafsils.FindAsync(dto.Tafsil5Id.Value);

                Acc_Coding_Tafsil bankTafsil = new Acc_Coding_Tafsil();
                if (bankAccount.TafsilId.HasValue)
                    bankTafsil = await _db.Acc_Coding_Tafsils.FindAsync(bankAccount.TafsilId.Value);

                // ردیف بانک
                // واریزی
                long totalBed = x.transaction.Sum(n => n.Debtor) ?? 0;
                long totalBes = x.transaction.Sum(n => n.Creditor) ?? 0;

                Acc_Article BankArt = new Acc_Article();
                BankArt.Id = Guid.NewGuid();
                BankArt.SellerId = dto.SellerId;
                BankArt.DocId = doc.Id;
                BankArt.PeriodId = doc.PeriodId;
                BankArt.KolId = bankAccount?.Moein?.KolId;
                BankArt.MoeinId = bankAccount.MoeinId.Value;
                BankArt.Tafsil4Id = bankTafsil?.Id;
                BankArt.Tafsil4Name = bankTafsil?.Name;
                BankArt.Amount = totalBed > 0 ? totalBed : totalBes;
                BankArt.Comment = dto.Descriptions;
                BankArt.CreateDate = DateTime.Now;
                BankArt.CreatorUserName = dto.UserName;
                BankArt.IsDeleted = false;


                if (dto.Grouped)
                {
                    // طرف دم سند
                    Acc_Article Side2Art = new Acc_Article();
                    Side2Art.Id = Guid.NewGuid();
                    Side2Art.SellerId = dto.SellerId;
                    Side2Art.DocId = doc.Id;
                    Side2Art.PeriodId = doc.PeriodId;
                    Side2Art.KolId = moein?.KolId;
                    Side2Art.MoeinId = moein.Id;
                    Side2Art.Tafsil4Id = tafsil4?.Id;
                    Side2Art.Tafsil4Name = tafsil4?.Name;
                    Side2Art.Tafsil5Id = tafsil5?.Id;
                    Side2Art.Tafsil5Name = tafsil5?.Name;
                    Side2Art.Amount = totalBed > 0 ? totalBed : totalBes;
                    Side2Art.Comment = dto.Descriptions;
                    Side2Art.CreateDate = DateTime.Now;
                    Side2Art.CreatorUserName = dto.UserName;
                    Side2Art.IsDeleted = false;


                    if (dto.TransactionsType == 1)
                    {
                        BankArt.Bed = BankArt.Amount;
                        BankArt.Bes = 0;
                        BankArt.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(BankArt);

                        // -------
                        Side2Art.Bed = 0;
                        Side2Art.Bes = Side2Art.Amount;
                        Side2Art.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(Side2Art);

                    }
                    else if (dto.TransactionsType == 2)
                    {
                        // -------
                        Side2Art.Bed = Side2Art.Amount;
                        Side2Art.Bes = 0;
                        Side2Art.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(Side2Art);

                        //-------
                        BankArt.Bed = 0;
                        BankArt.Bes = BankArt.Amount;
                        BankArt.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(BankArt);

                    }
                }
                else
                {
                    if (dto.TransactionsType == 1)
                    {
                        BankArt.Bed = BankArt.Amount;
                        BankArt.Bes = 0;
                        BankArt.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(BankArt);
                    }

                    foreach (var a in x.transaction)
                    {
                        long bed = a.Debtor ?? 0;
                        long bes = a.Creditor ?? 0;

                        Acc_Article Side2Art = new Acc_Article();
                        Side2Art.Id = Guid.NewGuid();
                        Side2Art.SellerId = dto.SellerId;
                        Side2Art.DocId = doc.Id;
                        Side2Art.PeriodId = doc.PeriodId;
                        Side2Art.KolId = moein.KolId;
                        Side2Art.MoeinId = moein.Id;
                        Side2Art.Tafsil4Id = tafsil4?.Id;
                        Side2Art.Tafsil4Name = tafsil4?.Name;
                        Side2Art.Tafsil5Id = tafsil5?.Id;
                        Side2Art.Tafsil5Name = tafsil5?.Name;
                        Side2Art.Comment = dto.Descriptions;
                        Side2Art.CreateDate = DateTime.Now;
                        Side2Art.CreatorUserName = dto.UserName;
                        Side2Art.IsDeleted = false;

                        if (dto.AppendBankDescription)
                            Side2Art.Comment += " " + a.Description;
                        if (dto.InsertTrackingNumber)
                            Side2Art.ArchiveCode = a.DocumentNumber;

                        if (dto.TransactionsType == 1)
                        {
                            // -------
                            Side2Art.Amount = a.Debtor ?? 0;
                            Side2Art.Bed = 0;
                            Side2Art.Bes = a.Debtor ?? 0;
                            Side2Art.RowNumber = rownumber;
                            rownumber++;
                            Articles.Add(Side2Art);
                        }
                        else if (dto.TransactionsType == 2)
                        {
                            // -------
                            Side2Art.Amount = a.Creditor ?? 0;
                            Side2Art.Bed = a.Creditor ?? 0;
                            Side2Art.Bes = 0;
                            Side2Art.RowNumber = rownumber;
                            rownumber++;
                            Articles.Add(Side2Art);
                        }
                    }
                    if (dto.TransactionsType == 2)
                    {
                        BankArt.Bed = 0;
                        BankArt.Bes = BankArt.Amount;
                        BankArt.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(BankArt);
                    }
                }
            }

            try
            {
                _db.Acc_Documents.AddRange(Docs);
                _db.Acc_Articles.AddRange(Articles);
                foreach (var t in transactions)
                {
                    t.IsChecked = true;
                    t.HasDoc = true;
                }
                _db.TreBankTransactions.UpdateRange(transactions);

                await _db.SaveChangesAsync();

                result.Message = "ثبت اسناد حسابداری با موفقیت انجام شد";
                result.Success = true;
            }
            catch (Exception x)
            {
                result.Message = "خطایی در ثبت اطلاعات رخ داده است";
                result.Message += "\n\n" + x.Message;
            }

            return result;
        }
        public async Task<clsResult> CreateBankDocAsync(BankTransactionsCreateDocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطاعاتی یافت نشد";

            if (!_sellerId.HasValue)
            {
                result.Message = "شرکت فعال یافت نشد";
                return result;
            }
            if (dto == null) return result;

            if (dto.PeriodId == 0)
            {
                result.Message = "سال مالی فعال یافت نشد";
                return result;
            }
            if (!dto.TransactionsId.Any())
            {
                result.Message = "اطلاعاتی برای ثبت وجود ندارد";
                return result;
            }

            // دریافت اطلاعات و اعتبارسنحی حساب بانکی
            var bankAccount = await _db.BankAccounts.Include(m => m.Moein).SingleOrDefaultAsync(n => n.Id == dto.BankAccountId);
            if (bankAccount == null)
            {
                result.Message = "اطلاعات حساب بانکی یافت نشد";
                return result;
            }
            if (!bankAccount.MoeinId.HasValue)
            {
                result.Message = "حساب معین بانک تعریف نشده است";
                return result;
            }
            if (!bankAccount.TafsilId.HasValue)
            {
                result.Message = "تفصیل حساب بانکی یافت نشد";
                return result;
            }
            if (dto.TransactionsType == 0)
            {
                result.Message = "نوع تراکنش (واریز-برداشت) مشخص نیست";
                return result;
            }
            //=============================================================

            var transactions = await _db.TreBankTransactions
                .Where(n => dto.TransactionsId.Contains(n.Id))
                .OrderBy(n => n.Row).ToListAsync();
            if (transactions == null) return result;

            //--
            List<Acc_Document> Docs = new List<Acc_Document>();
            List<Acc_Article> Articles = new List<Acc_Article>();

            //---- گروه بندی تراکنش ها بر اساس تاریخ تراکنش
            var dateGrouped = transactions.GroupBy(n => n.Date).Select(n => new
            {
                date = n.Key,
                transaction = n

            }).ToList();

            int docAutonumber = await _docServic.DocAutoNumberGeneratorAsync(dto.SellerId, dto.PeriodId);
            int docNumber = await _docServic.DocNumberGeneratorAsync(dto.SellerId, dto.PeriodId);
            int rownumber = 1;
            foreach (var x in dateGrouped.OrderBy(n => n.date).ToList())
            {
                //======= Doc Header
                Acc_Document doc = new Acc_Document();
                if (!dto.CreateNewDoc)
                {
                    var query = _db.Acc_Documents.Include(n => n.DocArticles)
                      .Where(n => !n.IsDeleted && n.SellerId == dto.SellerId && n.PeriodId == dto.PeriodId && n.DocDate.Date == x.date.Value.Date).
                    AsQueryable();

                    if (!string.IsNullOrEmpty(dto.DocSelector))
                        query = query.Where(n => n.Description == dto.DocSelector);

                    var docInDate = await query.FirstOrDefaultAsync();

                    if (docInDate != null)
                    {
                        doc = docInDate;
                        if (docInDate.DocArticles != null && docInDate.DocArticles.Any())
                            rownumber = docInDate.DocArticles.Select(m => m.RowNumber).Max() + 1;
                        else
                            rownumber = 1;
                    }

                    else
                    {
                        doc.Id = Guid.NewGuid();
                        doc.SellerId = dto.SellerId;
                        doc.PeriodId = dto.PeriodId;
                        doc.DocDate = x.date.Value;
                        doc.AtfNumber = docAutonumber;
                        doc.AutoDocNumber = docAutonumber;
                        doc.DocNumber = docNumber;
                        doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                        if (!string.IsNullOrEmpty(dto.DocSelector))
                            doc.Description = dto.DocSelector;
                        doc.StatusId = 1;
                        doc.SubsystemId = 2;
                        doc.CreateDate = DateTime.Now;
                        doc.CreatorUserName = dto.UserName;
                        doc.IsDeleted = false;
                        doc.TypeId = 1;
                        Docs.Add(doc);

                        docAutonumber++;
                        docNumber++;
                    }
                }
                else
                {
                    doc.Id = Guid.NewGuid();
                    doc.SellerId = dto.SellerId;
                    doc.PeriodId = dto.PeriodId;
                    doc.DocDate = x.date.Value;
                    doc.AtfNumber = docAutonumber;
                    doc.AutoDocNumber = docAutonumber;
                    doc.DocNumber = docNumber;
                    doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                    if (!string.IsNullOrEmpty(dto.DocSelector))
                        doc.Description = dto.DocSelector;
                    doc.StatusId = 1;
                    doc.SubsystemId = 2;
                    doc.CreateDate = DateTime.Now;
                    doc.CreatorUserName = dto.UserName;
                    doc.IsDeleted = false;
                    doc.TypeId = 1;
                    Docs.Add(doc);

                    docAutonumber++;
                    docNumber++;
                }

                //== End Create Doc Heade =========================================
                var moein = await _db.Acc_Coding_Moeins.FindAsync(dto.MoeinId);

                Acc_Coding_Tafsil? tafsil4 = null;
                if (dto.Tafsil4Id.HasValue)
                    tafsil4 = await _db.Acc_Coding_Tafsils.FindAsync(dto.Tafsil4Id.Value);

                Acc_Coding_Tafsil? tafsil5 = null;
                if (dto.Tafsil5Id.HasValue)
                    tafsil5 = await _db.Acc_Coding_Tafsils.FindAsync(dto.Tafsil5Id.Value);

                Acc_Coding_Tafsil? bankTafsil = null;
                if (bankAccount.TafsilId.HasValue)
                    bankTafsil = await _db.Acc_Coding_Tafsils.FindAsync(bankAccount.TafsilId.Value);



                if (dto.Grouped)
                {
                    // ردیف بانک
                    // واریزی
                    long totalBed = x.transaction.Sum(n => n.Debtor) ?? 0;
                    long totalBes = x.transaction.Sum(n => n.Creditor) ?? 0;

                    Acc_Article BankArt = new Acc_Article();
                    BankArt.Id = Guid.NewGuid();
                    BankArt.SellerId = dto.SellerId;
                    BankArt.DocId = doc.Id;
                    BankArt.PeriodId = doc.PeriodId;
                    BankArt.KolId = bankAccount?.Moein?.KolId;
                    BankArt.MoeinId = bankAccount.MoeinId.Value;
                    BankArt.Tafsil4Id = bankTafsil?.Id;
                    BankArt.Tafsil4Name = bankTafsil?.Name;
                    BankArt.Amount = totalBed > 0 ? totalBed : totalBes;
                    BankArt.Comment = dto.Descriptions;
                    BankArt.CreateDate = DateTime.Now;
                    BankArt.CreatorUserName = dto.UserName;
                    BankArt.IsDeleted = false;


                    // طرف دم سند
                    Acc_Article Side2Art = new Acc_Article();
                    Side2Art.Id = Guid.NewGuid();
                    Side2Art.SellerId = dto.SellerId;
                    Side2Art.DocId = doc.Id;
                    Side2Art.PeriodId = doc.PeriodId;
                    Side2Art.KolId = moein?.KolId;
                    Side2Art.MoeinId = moein.Id;
                    Side2Art.Tafsil4Id = tafsil4?.Id;
                    Side2Art.Tafsil4Name = tafsil4?.Name;
                    Side2Art.Tafsil5Id = tafsil5?.Id;
                    Side2Art.Tafsil5Name = tafsil5?.Name;
                    Side2Art.Amount = totalBed > 0 ? totalBed : totalBes;
                    Side2Art.Comment = dto.Descriptions;
                    Side2Art.CreateDate = DateTime.Now;
                    Side2Art.CreatorUserName = dto.UserName;
                    Side2Art.IsDeleted = false;

                    if (dto.TransactionsType == 1)
                    {
                        BankArt.Bed = BankArt.Amount;
                        BankArt.Bes = 0;
                        BankArt.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(BankArt);

                        // -------
                        Side2Art.Bed = 0;
                        Side2Art.Bes = Side2Art.Amount;
                        Side2Art.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(Side2Art);

                    }
                    else if (dto.TransactionsType == 2)
                    {
                        // -------
                        Side2Art.Bed = Side2Art.Amount;
                        Side2Art.Bes = 0;
                        Side2Art.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(Side2Art);

                        //-------
                        BankArt.Bed = 0;
                        BankArt.Bes = BankArt.Amount;
                        BankArt.RowNumber = rownumber;
                        rownumber++;
                        Articles.Add(BankArt);

                    }
                }
                else // ثبت سند به تفکیک تراکنش
                {

                    foreach (var a in x.transaction)
                    {
                        long bed = a.Debtor ?? 0;
                        long bes = a.Creditor ?? 0;

                        // بانک
                        Acc_Article BankArt = new Acc_Article();
                        BankArt.Id = Guid.NewGuid();
                        BankArt.SellerId = dto.SellerId;
                        BankArt.DocId = doc.Id;
                        BankArt.PeriodId = doc.PeriodId;
                        BankArt.KolId = bankAccount?.Moein?.KolId;
                        BankArt.MoeinId = bankAccount.MoeinId.Value;
                        BankArt.Tafsil4Id = bankTafsil?.Id;
                        BankArt.Tafsil4Name = bankTafsil?.Name;
                        BankArt.Tafsil5Id = tafsil5 != null ? tafsil5?.Id : BankArt.Tafsil5Id = null;
                        BankArt.Tafsil5Name = tafsil5 != null ? tafsil5?.Name : BankArt.Tafsil5Name = null;
                        BankArt.Amount = bed > 0 ? bed : bes;
                        BankArt.Comment = dto.Descriptions;
                        BankArt.CreateDate = DateTime.Now;
                        BankArt.CreatorUserName = dto.UserName;
                        BankArt.IsDeleted = false;
                        BankArt.BatchNumber = a.BatchNumber;
                        BankArt.BankTransactionId = a.Id;
                        //طرف دوم
                        Acc_Article Side2Art = new Acc_Article();
                        Side2Art.Id = Guid.NewGuid();
                        Side2Art.SellerId = dto.SellerId;
                        Side2Art.DocId = doc.Id;
                        Side2Art.PeriodId = doc.PeriodId;
                        Side2Art.KolId = moein.KolId;
                        Side2Art.MoeinId = moein.Id;
                        Side2Art.Tafsil4Id = tafsil4?.Id;
                        Side2Art.Tafsil4Name = tafsil4?.Name;
                        Side2Art.Tafsil5Id = tafsil5 != null ? tafsil5?.Id : Side2Art.Tafsil5Id = null;
                        Side2Art.Tafsil5Name = tafsil5 != null ? tafsil5?.Name : Side2Art.Tafsil5Name = null;
                        Side2Art.Comment = dto.Descriptions;
                        Side2Art.CreateDate = DateTime.Now;
                        Side2Art.CreatorUserName = dto.UserName;
                        Side2Art.IsDeleted = false;
                        Side2Art.BatchNumber = a.BatchNumber;
                        Side2Art.BankTransactionId = a.Id;

                        if (dto.AppendBankDescription)
                        {
                            BankArt.Comment += " " + a.Description;
                            Side2Art.Comment += " " + a.Description;
                        }
                        if (dto.InsertTrackingNumber)
                        {
                            BankArt.ArchiveCode = a.DocumentNumber;
                            Side2Art.ArchiveCode = a.DocumentNumber;
                        }

                        if (dto.TransactionsType == 1) // واریز
                        {
                            BankArt.Bed = bed;
                            BankArt.Bes = bes;
                            BankArt.RowNumber = rownumber;
                            rownumber++;
                            Articles.Add(BankArt);

                            Side2Art.Amount = a.Debtor ?? 0;
                            Side2Art.Bed = 0;
                            Side2Art.Bes = a.Debtor ?? 0;
                            Side2Art.RowNumber = rownumber;
                            rownumber++;
                            Articles.Add(Side2Art);

                        }
                        else // برداشت
                        {
                            Side2Art.Amount = a.Creditor ?? 0;
                            Side2Art.Bed = a.Creditor ?? 0;
                            Side2Art.Bes = 0;
                            Side2Art.RowNumber = rownumber;
                            rownumber++;
                            Articles.Add(Side2Art);

                            BankArt.Bed = bed;
                            BankArt.Bes = bes;
                            BankArt.RowNumber = rownumber;
                            rownumber++;
                            Articles.Add(BankArt);
                        }

                    }
                }
            }

            try
            {
                _db.Acc_Documents.AddRange(Docs);
                _db.Acc_Articles.AddRange(Articles);
                foreach (var t in transactions)
                {
                    t.IsChecked = true;
                    t.HasDoc = true;

                }
                _db.TreBankTransactions.UpdateRange(transactions);

                await _db.SaveChangesAsync();

                result.Message = "ثبت اسناد حسابداری با موفقیت انجام شد";
                result.Success = true;
            }
            catch (Exception x)
            {
                result.Message = "خطایی در ثبت اطلاعات رخ داده است";
                result.Message += "\n\n" + x.Message;
            }

            return result;
        }
        public async Task<clsResult> CreatInvoiceDocAsync(List<Guid> InvoicesId, string username)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطاعاتی یافت نشد";

            if (InvoicesId.Count == 0)
                return result;
            if (!_sellerId.HasValue)
            {
                result.Message = "شرکت فعال یافت نشد";
                return result;
            }
            var accSett = await _db.Acc_Settings.Where(n => n.SellerId == _sellerId.Value).FirstOrDefaultAsync();
            var invoices = await _invoiceService.GetInvoicesToDtoByIdAsync(InvoicesId);
            if (invoices.Count == 0) return result;

            var checkSettings = CheckAccountingSettings(accSett, invoices.Where(n => n.InvoiceType == 2).Any(), invoices.Where(n => n.InvoiceType == 1).Any());
            if (!checkSettings.Success)
                return checkSettings;



            var invoicesGrouped = invoices.GroupBy(n => new { n.InvoiceType, n.InvoiceDate })
               .Select(n => new
               {
                   date = n.Key.InvoiceDate,
                   BuyInvoices = n.Where(n => n.InvoiceType == 1).ToList(),
                   saleInvoices = n.Where(n => n.InvoiceType == 2).ToList(),
               }).ToList();

            List<Acc_Document> Docs = new List<Acc_Document>();
            List<Acc_Article> Articles = new List<Acc_Article>();
            List<InvoiceDocInfo> invoiceDocInfos = new List<InvoiceDocInfo>();

            int docAutonumber = await _docServic.DocAutoNumberGeneratorAsync(_sellerId.Value, _periodId.Value);
            int docNumber = await _docServic.DocNumberGeneratorAsync(_sellerId.Value, _periodId.Value);

            foreach (var x in invoicesGrouped)
            {

                //========================================================================================
                //================================== ثبت فاکتورهای خرید  =================================
                //========================================================================================
                if (x.BuyInvoices.Count > 0)
                {

                    Acc_Document doc = new Acc_Document();
                    doc.Id = Guid.NewGuid();
                    doc.SellerId = _sellerId.Value;
                    doc.PeriodId = _periodId.Value;
                    doc.DocDate = x.date.Value;
                    doc.AtfNumber = docAutonumber;
                    doc.AutoDocNumber = docAutonumber;
                    doc.DocNumber = docNumber;
                    doc.Description = $"بابت ثبت حسابداری فاکتورهای خرید در تاریخ {x.date.Value.LatinToPersian()}";
                    doc.StatusId = 1;
                    doc.SubsystemId = 2;
                    doc.CreateDate = DateTime.Now;
                    doc.CreatorUserName = username;
                    doc.IsDeleted = false;
                    doc.TypeId = 1;
                    Docs.Add(doc);

                    docAutonumber++;
                    docNumber++;
                    int row = 1;
                    foreach (var a in x.BuyInvoices)
                    {


                        //آرتیکل خرید
                        Acc_Article buyArticle = new Acc_Article();
                        buyArticle.Id = Guid.NewGuid();
                        buyArticle.InvoiceId = a.Id;
                        buyArticle.DocId = doc.Id;
                        buyArticle.PeriodId = _periodId.Value;
                        buyArticle.KolId = await _codingService.GetKolIdByMoeinIdAsync(accSett.BuyMoeinId.Value);
                        buyArticle.MoeinId = accSett.BuyMoeinId.Value;
                        buyArticle.Amount = (long)a.TotalPriceBeforDiscount.Value;
                        buyArticle.Bed = (long)a.TotalPriceBeforDiscount.Value;
                        buyArticle.Bes = 0;
                        buyArticle.Comment = $"بابت فاکتور خرید شماره {a.InvoiceNumber} - {a.PartyName} در تاریخ {a.InvoiceDate.Value.LatinToPersian()}";
                        buyArticle.ArchiveCode = a.InvoiceNumber;
                        buyArticle.CreateDate = DateTime.Now;
                        buyArticle.CreatorUserName = username;
                        buyArticle.IsDeleted = false;
                        buyArticle.RowNumber = row;
                        buyArticle.AccountantRemark = a.Id.ToString();
                        buyArticle.ArchiveCode = a.InvoiceNumber;
                        row++;
                        Articles.Add(buyArticle);

                        if (a.TotalVatPrice > 0)
                        {
                            //ارزش افزوده خرید 
                            Acc_Article buyVatArticle = new Acc_Article();
                            buyVatArticle.Id = Guid.NewGuid();
                            buyVatArticle.InvoiceId = a.Id;
                            buyVatArticle.DocId = doc.Id;
                            buyVatArticle.PeriodId = _periodId.Value;
                            buyVatArticle.KolId = await _codingService.GetKolIdByMoeinIdAsync(accSett.BuyVatMoeinId.Value);
                            buyVatArticle.MoeinId = accSett.BuyVatMoeinId.Value;
                            buyVatArticle.Amount = (long)a.TotalVatPrice.Value;
                            buyVatArticle.Bed = (long)a.TotalVatPrice.Value;
                            buyVatArticle.Bes = 0;
                            buyVatArticle.Comment = $"بابت ارزش افزوده فاکتور خرید شماره {a.InvoiceNumber} - {a.PartyName} در تاریخ {a.InvoiceDate.Value.LatinToPersian()}";
                            buyVatArticle.ArchiveCode = a.InvoiceNumber;
                            buyVatArticle.CreateDate = DateTime.Now;
                            buyVatArticle.CreatorUserName = username;
                            buyVatArticle.IsDeleted = false;
                            buyVatArticle.RowNumber = row;
                            buyVatArticle.AccountantRemark = a.Id.ToString();
                            buyVatArticle.ArchiveCode = a.InvoiceNumber;
                            row++;
                            Articles.Add(buyVatArticle);
                        }
                        if (a.TotalDiscount > 0)
                        {
                            //تخفیفات نقدی خرید
                            Acc_Article buyDiscountArticle = new Acc_Article();
                            buyDiscountArticle.Id = Guid.NewGuid();
                            buyDiscountArticle.InvoiceId = a.Id;
                            buyDiscountArticle.DocId = doc.Id;
                            buyDiscountArticle.PeriodId = _periodId.Value;
                            buyDiscountArticle.KolId = await _codingService.GetKolIdByMoeinIdAsync(accSett.BuyDiscountMoeinId.Value);
                            buyDiscountArticle.MoeinId = accSett.BuyDiscountMoeinId.Value;
                            buyDiscountArticle.Amount = (long)a.TotalDiscount.Value;
                            buyDiscountArticle.Bed = 0;
                            buyDiscountArticle.Bes = (long)a.TotalDiscount.Value;
                            buyDiscountArticle.Comment = $"بابت تخفیفات نقدی فاکتور خرید شماره {a.InvoiceNumber} - {a.PartyName} در تاریخ {a.InvoiceDate.Value.LatinToPersian()}";
                            buyDiscountArticle.ArchiveCode = a.InvoiceNumber;
                            buyDiscountArticle.CreateDate = DateTime.Now;
                            buyDiscountArticle.CreatorUserName = username;
                            buyDiscountArticle.IsDeleted = false;
                            buyDiscountArticle.RowNumber = row;
                            buyDiscountArticle.AccountantRemark = a.Id.ToString();
                            buyDiscountArticle.ArchiveCode = a.InvoiceNumber;
                            row++;
                            Articles.Add(buyDiscountArticle);
                        }

                        //بستانکاران تجاری
                        Acc_Article creditor = new Acc_Article();
                        creditor.Id = Guid.NewGuid();
                        creditor.InvoiceId = a.Id;
                        creditor.DocId = doc.Id;
                        creditor.PeriodId = _periodId.Value;
                        creditor.KolId = await _codingService.GetKolIdByMoeinIdAsync(accSett.BuyPartyMoeinId.Value);
                        creditor.MoeinId = accSett.BuyPartyMoeinId.Value;
                        creditor.Amount = (long)a.TotalFinalPrice.Value;
                        creditor.Bed = 0;
                        creditor.Bes = (long)a.TotalFinalPrice.Value;
                        creditor.Comment = $"بابت فاکتور خرید شماره {a.InvoiceNumber} - {a.PartyName} در تاریخ {a.InvoiceDate.Value.LatinToPersian()}";
                        creditor.ArchiveCode = a.InvoiceNumber;
                        creditor.Tafsil4Id = a.PartyTafsilId;
                        creditor.Tafsil4Name = a.PartyName;
                        creditor.CreateDate = DateTime.Now;
                        creditor.CreatorUserName = username;
                        creditor.IsDeleted = false;
                        creditor.RowNumber = row;

                        creditor.AccountantRemark = a.Id.ToString();
                        creditor.ArchiveCode = a.InvoiceNumber;

                        row++;
                        Articles.Add(creditor);

                        InvoiceDocInfo docInfo = new InvoiceDocInfo();
                        docInfo.InvoiceId = a.Id;
                        docInfo.DocId = doc.Id;
                        docInfo.DocAutoNumber = doc.AutoDocNumber;
                        invoiceDocInfos.Add(docInfo);
                    }
                }

                //========================================================================================
                //================================== ثبت فاکتورهای فروش  =================================
                //========================================================================================
                if (x.saleInvoices.Count > 0)
                {
                    Acc_Document saledoc = new Acc_Document();
                    saledoc.Id = Guid.NewGuid();
                    saledoc.SellerId = _sellerId.Value;
                    saledoc.PeriodId = _periodId.Value;
                    saledoc.DocDate = x.date.Value;
                    saledoc.AtfNumber = docAutonumber;
                    saledoc.AutoDocNumber = docAutonumber;
                    saledoc.DocNumber = docNumber;
                    saledoc.Description = $"بابت ثبت حسابداری فاکتورهای فروش در تاریخ {x.date.Value.LatinToPersian()}";
                    saledoc.StatusId = 1;
                    saledoc.SubsystemId = 3;
                    saledoc.CreateDate = DateTime.Now;
                    saledoc.CreatorUserName = username;
                    saledoc.IsDeleted = false;
                    saledoc.TypeId = 1;
                    Docs.Add(saledoc);


                    docAutonumber++;
                    docNumber++;
                    int row = 1;
                    foreach (var a in x.saleInvoices)
                    {


                        //آرتیکل فروش
                        Acc_Article saleArticle = new Acc_Article();
                        saleArticle.Id = Guid.NewGuid();
                        saleArticle.InvoiceId = a.Id;
                        saleArticle.DocId = saledoc.Id;
                        saleArticle.PeriodId = _periodId.Value;
                        saleArticle.KolId = await _codingService.GetKolIdByMoeinIdAsync(accSett.saleMoeinId.Value);
                        saleArticle.MoeinId = accSett.saleMoeinId.Value;
                        saleArticle.Amount = (long)a.TotalPriceBeforDiscount.Value;
                        saleArticle.Bed = 0;
                        saleArticle.Bes = (long)a.TotalPriceBeforDiscount.Value;
                        saleArticle.Comment = $"بابت فاکتور فروش شماره {a.InvoiceNumber} - {a.PartyName} در تاریخ {a.InvoiceDate.Value.LatinToPersian()}";
                        saleArticle.ArchiveCode = a.InvoiceNumber;
                        saleArticle.CreateDate = DateTime.Now;
                        saleArticle.CreatorUserName = username;
                        saleArticle.IsDeleted = false;
                        saleArticle.RowNumber = row;
                        saleArticle.AccountantRemark = a.Id.ToString();
                        saleArticle.ArchiveCode = a.InvoiceNumber;
                        row++;
                        Articles.Add(saleArticle);

                        if (a.TotalVatPrice > 0)
                        {
                            //ارزش افزوده فروش 
                            Acc_Article saleVatArticle = new Acc_Article();
                            saleVatArticle.Id = Guid.NewGuid();
                            saleVatArticle.InvoiceId = a.Id;
                            saleVatArticle.DocId = saledoc.Id;
                            saleVatArticle.PeriodId = _periodId.Value;
                            saleVatArticle.KolId = await _codingService.GetKolIdByMoeinIdAsync(accSett.SaleVatMoeinId.Value);
                            saleVatArticle.MoeinId = accSett.SaleVatMoeinId.Value;
                            saleVatArticle.Amount = (long)a.TotalVatPrice.Value;
                            saleVatArticle.Bed = 0;
                            saleVatArticle.Bes = (long)a.TotalVatPrice.Value;
                            saleVatArticle.Comment = $"بابت ارزش افزوده فاکتور فروش شماره {a.InvoiceNumber} - {a.PartyName} در تاریخ {a.InvoiceDate.Value.LatinToPersian()}";
                            saleVatArticle.ArchiveCode = a.InvoiceNumber;
                            saleVatArticle.CreateDate = DateTime.Now;
                            saleVatArticle.CreatorUserName = username;
                            saleVatArticle.IsDeleted = false;
                            saleVatArticle.RowNumber = row;
                            saleVatArticle.AccountantRemark = a.Id.ToString();
                            saleVatArticle.ArchiveCode = a.InvoiceNumber;
                            row++;
                            Articles.Add(saleVatArticle);
                        }
                        if (a.TotalDiscount > 0)
                        {
                            //تخفیفات نقدی فروش
                            Acc_Article saleDiscountArticle = new Acc_Article();
                            saleDiscountArticle.Id = Guid.NewGuid();
                            saleDiscountArticle.InvoiceId = a.Id;
                            saleDiscountArticle.DocId = saledoc.Id;
                            saleDiscountArticle.PeriodId = _periodId.Value;
                            saleDiscountArticle.KolId = await _codingService.GetKolIdByMoeinIdAsync(accSett.saleDiscountMoeinId.Value);
                            saleDiscountArticle.MoeinId = accSett.saleDiscountMoeinId.Value;
                            saleDiscountArticle.Amount = (long)a.TotalDiscount.Value;
                            saleDiscountArticle.Bed = (long)a.TotalDiscount.Value;
                            saleDiscountArticle.Bes = 0;
                            saleDiscountArticle.Comment = $"بابت تخفیفات نقدی فاکتور فروش شماره {a.InvoiceNumber} - {a.PartyName} در تاریخ {a.InvoiceDate.Value.LatinToPersian()}";
                            saleDiscountArticle.ArchiveCode = a.InvoiceNumber;
                            saleDiscountArticle.CreateDate = DateTime.Now;
                            saleDiscountArticle.CreatorUserName = username;
                            saleDiscountArticle.IsDeleted = false;
                            saleDiscountArticle.RowNumber = row;
                            saleDiscountArticle.AccountantRemark = a.Id.ToString();
                            saleDiscountArticle.ArchiveCode = a.InvoiceNumber;
                            row++;
                            Articles.Add(saleDiscountArticle);
                        }

                        //بدهکاران تجاری
                        Acc_Article debtor = new Acc_Article();
                        debtor.Id = Guid.NewGuid();
                        debtor.InvoiceId = a.Id;
                        debtor.DocId = saledoc.Id;
                        debtor.PeriodId = _periodId.Value;
                        debtor.KolId = await _codingService.GetKolIdByMoeinIdAsync(accSett.salePartyMoeinId.Value);
                        debtor.MoeinId = accSett.salePartyMoeinId.Value;
                        debtor.Amount = (long)a.TotalFinalPrice.Value;
                        debtor.Bed = (long)a.TotalFinalPrice.Value;
                        debtor.Bes = 0;
                        debtor.Comment = $"بابت فاکتور فروش شماره {a.InvoiceNumber} - {a.PartyName} در تاریخ {a.InvoiceDate.Value.LatinToPersian()}";
                        debtor.ArchiveCode = a.InvoiceNumber;
                        debtor.Tafsil4Id = a.PartyTafsilId;
                        debtor.Tafsil4Name = a.PartyName;
                        debtor.CreateDate = DateTime.Now;
                        debtor.CreatorUserName = username;
                        debtor.IsDeleted = false;
                        debtor.RowNumber = row;
                        debtor.AccountantRemark = a.Id.ToString();
                        debtor.ArchiveCode = a.InvoiceNumber;
                        row++;
                        Articles.Add(debtor);

                        InvoiceDocInfo docInfo = new InvoiceDocInfo();
                        docInfo.InvoiceId = a.Id;
                        docInfo.DocId = saledoc.Id;
                        docInfo.DocAutoNumber = saledoc.AutoDocNumber;
                        invoiceDocInfos.Add(docInfo);

                    }
                }
            }

            //========================================================================================
            //================================== افزودن اسناد حسابداری  =============================
            //========================================================================================

            _db.Acc_Documents.AddRange(Docs);
            _db.Acc_Articles.AddRange(Articles);
            try
            {
                List<com_Invoice> invoicesToUpdate = new List<com_Invoice>();
                foreach (var x in invoiceDocInfos)
                {
                    var invoice = await _db.Invoices.FindAsync(x.InvoiceId);
                    invoice.DocId = x.DocId;
                    invoice.DocAutoNo = x.DocAutoNumber;
                    invoicesToUpdate.Add(invoice);
                }
                _db.Invoices.UpdateRange(invoicesToUpdate);

                await _db.SaveChangesAsync();
                result.Message = "اسناد حسابداری خرید/فروش با موفقیت انجام شد.";
                result.Success = true;
                result.updateType = 1;

            }
            catch (Exception x)
            {
                result.Message = "در ثبت اطلاعات یک خطای سیستمی رخ داده است";
            }

            return result;
        }
        public async Task<clsResult> CreateModiranReceptionDocAsync(SaveDetailsDto Reception)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (Reception == null)
            {
                result.Message = "آیتمی برای ثبت وجود ندارد";
                return result;
            }
            if (!_userContext.PeriodId.HasValue)
            {
                result.Message = "اطلاعات سال مالی یافت نشد";
                return result;
            }
            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "اطلاعات شرکت فعال یافت نشد";
                return result;
            }
            long sellerId = _userContext.SellerId.Value;
            int periodId = _userContext.PeriodId.Value;
            var period = await _db.Acc_FinancialPeriods.FindAsync(periodId);
            if (period == null)
            {
                result.Message = "اطلاعات دوره مالی یافت نشد";
                return result;
            }
            var accountingSetting = await _db.Asa_Settings.Where(n => n.SellerId == sellerId).FirstOrDefaultAsync();
            if (accountingSetting == null)
            {
                result.Message = "تنظیمات حسابداری پذیرش انجام نشده";
                return result;
            }

            var hasDoc = await _db.Acc_Articles.Where(n => (!n.Doc.IsDeleted || !n.IsDeleted) && n.AccountantRemark == Reception.ReceptionNember).ToListAsync();
            if (hasDoc.Any())
            {
                var art = hasDoc.FirstOrDefault();
                result.Message = $"پذیرش {Reception.ReceptionNember} پیش از این با شماره بایگانی {art.ArchiveCode} ثبت شده است.";
                return result;
            }

            long totalAmount = Reception.TotalOjratAmount + Reception.TotalSaleAmount;
            long vatAmount = (long)(totalAmount * (period.DefualtVatRate ?? 0) / 100);

            Acc_Document doc = new Acc_Document();
            List<Acc_Article> articles = new List<Acc_Article>();

            // 0 ====> Doc Header
            int docAutonumber = await _docServic.DocAutoNumberGeneratorAsync(sellerId, periodId);
            int docNumber = await _docServic.DocNumberGeneratorAsync(sellerId, periodId);
            string ArchiveCode = "";
            int rownumber = 1;
            if (!Reception.CreateNewDoc)
            {
                var query = _db.Acc_Documents.Include(n => n.DocArticles)
                .Where(n => !n.IsDeleted && n.SellerId == sellerId && n.DocDate.Date.Date == Reception.InvoiceDate.Date).
                AsQueryable();
                var docInDate = await query.FirstOrDefaultAsync();

                if (docInDate != null)
                {
                    doc = docInDate;
                    ArchiveCode = await _docServic.GetDocArchiveNumber(doc.Id);
                    rownumber = docInDate.DocArticles != null && docInDate.DocArticles.Count() > 0 ? docInDate.DocArticles.Where(n => !n.IsDeleted).Max(n => n.RowNumber) + 1 : 1;
                }
                else
                {
                    doc.Id = Guid.NewGuid();
                    doc.SellerId = sellerId;
                    doc.PeriodId = periodId;
                    doc.DocDate = Reception.InvoiceDate;
                    doc.AtfNumber = docAutonumber;
                    doc.AutoDocNumber = docAutonumber;
                    doc.DocNumber = docNumber;
                    doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                    doc.StatusId = 1;
                    doc.SubsystemId = 2;
                    doc.CreateDate = DateTime.Now;
                    doc.CreatorUserName = "system";
                    doc.IsDeleted = false;
                    doc.TypeId = 1;
                    doc.SubsystemRef = DateTime.Now.ToLong();

                    _db.Acc_Documents.Add(doc);

                    ArchiveCode = $"{docNumber}/001";
                }
            }
            else
            {
                doc.Id = Guid.NewGuid();
                doc.SellerId = sellerId;
                doc.PeriodId = periodId;
                doc.DocDate = Reception.Date;
                doc.AtfNumber = docAutonumber;
                doc.AutoDocNumber = docAutonumber;
                doc.DocNumber = docNumber;
                doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                doc.StatusId = 1;
                doc.SubsystemId = 2;
                doc.CreateDate = DateTime.Now;
                doc.CreatorUserName = "system";
                doc.IsDeleted = false;
                doc.TypeId = 1;
                doc.SubsystemRef = DateTime.Now.ToLong();

                _db.Acc_Documents.Add(doc);

                ArchiveCode = $"{docNumber}/001";
            }

            //== End Create Doc Header =========================================
            //General

            string comment = $"{Reception.ClientName} - {Reception.CarNumber} - پ {Reception.ReceptionNember} - {Reception.Brand}";

            //  1 =============================================================> Commercial Bebtor Article / Bank

            if (Reception.WarrantyTotalPrice > 0)
            {
                Acc_Coding_Moein DebtorMoein = new Acc_Coding_Moein();
                Acc_Coding_Tafsil DebtorTafsil = new Acc_Coding_Tafsil();
                if (!accountingSetting.CommercialDebtorMoeinId.HasValue || !accountingSetting.DebtorTafsil4Warranty.HasValue)
                {
                    result.Message = "در بخش تنظیمات، حساب معین بدهکاران تجاری یا تفیل آن  مشحص نشده است.";
                    return result;
                }
                DebtorMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.CommercialDebtorMoeinId.Value);
                DebtorTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.DebtorTafsil4Warranty);

                Acc_Article DebtorArt = new Acc_Article();
                DebtorArt.Id = Guid.NewGuid();
                DebtorArt.SellerId = sellerId;
                DebtorArt.DocId = doc.Id;
                DebtorArt.PeriodId = doc.PeriodId;
                DebtorArt.KolId = DebtorMoein.KolId;
                DebtorArt.MoeinId = DebtorMoein.Id;
                DebtorArt.Tafsil4Id = DebtorTafsil != null ? DebtorTafsil.Id : null;
                DebtorArt.Tafsil4Name = DebtorTafsil != null ? DebtorTafsil.Name : null;
                DebtorArt.Comment = comment;
                DebtorArt.CreateDate = DateTime.Now;
                DebtorArt.CreatorUserName = "system";
                DebtorArt.ArchiveCode = ArchiveCode;
                DebtorArt.IsDeleted = false;
                DebtorArt.AccountantRemark = Reception.ReceptionNember;
                DebtorArt.RowNumber = rownumber;

                DebtorArt.Bed = Reception.WarrantyTotalPrice;
                DebtorArt.Bes = 0;

                articles.Add(DebtorArt);
                rownumber++;
            }

            if (Reception.FreeTotalPrice > 0)
            {
                Acc_Coding_Moein DebtorMoein = new Acc_Coding_Moein();
                Acc_Coding_Tafsil DebtorTafsil = new Acc_Coding_Tafsil();

                if (!accountingSetting.BankMoeinId.HasValue)
                {
                    result.Message = "در بخش تنظیمات، حساب معین بانک ها مشحص نشده است.";
                    return result;
                }
                DebtorMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.BankMoeinId.Value);

                long? bankTafsil = Reception.BankTafsilId.HasValue ? Reception.BankTafsilId.Value : accountingSetting.BankTafsilId;
                if (!bankTafsil.HasValue)
                {
                    result.Message = "حساب تفصیل بانک مشخص نشده است";
                    return result;
                }
                DebtorTafsil = await _db.Acc_Coding_Tafsils.FindAsync(bankTafsil.Value);

                Acc_Article DebtorArt = new Acc_Article();
                DebtorArt.Id = Guid.NewGuid();
                DebtorArt.SellerId = sellerId;
                DebtorArt.DocId = doc.Id;
                DebtorArt.PeriodId = doc.PeriodId;
                DebtorArt.KolId = DebtorMoein.KolId;
                DebtorArt.MoeinId = DebtorMoein.Id;
                DebtorArt.Tafsil4Id = DebtorTafsil != null ? DebtorTafsil.Id : null;
                DebtorArt.Tafsil4Name = DebtorTafsil != null ? DebtorTafsil.Name : null;
                DebtorArt.Comment = comment;
                DebtorArt.CreateDate = DateTime.Now;
                DebtorArt.CreatorUserName = "system";
                DebtorArt.ArchiveCode = ArchiveCode;
                DebtorArt.IsDeleted = false;
                DebtorArt.AccountantRemark = Reception.ReceptionNember;
                DebtorArt.RowNumber = rownumber;

                DebtorArt.Bed = Reception.FreeTotalPrice;
                DebtorArt.Bes = 0;

                articles.Add(DebtorArt);
                rownumber++;
            }


            //=====================================================================================
            // 2 ===========================================================================> Sale

            Acc_Coding_Moein SaleMoein = new Acc_Coding_Moein();
            if (!accountingSetting.SaleMoeinId.HasValue)
            {
                result.Message = "در تنظیمات حسابداری پذیرش، حساب معین فروش مشخص نشده است";
                return result;
            }
            SaleMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.SaleMoeinId.Value);


            if (Reception.SaleWarrantyAmount > 0)
            {
                Acc_Coding_Tafsil SaleTafsil = new Acc_Coding_Tafsil();
                if (!accountingSetting.SaleWarrantyTfsilId.HasValue)
                {
                    result.Message = "در تنظیمات حسابداری پذیرش، تفصیل فروش قطعه گارانتی مشخص نشده است";
                    return result;
                }
                SaleTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.SaleWarrantyTfsilId.Value);

                Acc_Article Sale = new Acc_Article();
                Sale.Id = Guid.NewGuid();
                Sale.SellerId = sellerId;
                Sale.DocId = doc.Id;
                Sale.PeriodId = doc.PeriodId;
                Sale.KolId = SaleMoein.KolId;
                Sale.MoeinId = SaleMoein.Id;
                Sale.Tafsil4Id = SaleTafsil != null ? SaleTafsil.Id : null;
                Sale.Tafsil4Name = SaleTafsil != null ? SaleTafsil.Name : null;
                Sale.Comment = comment;
                Sale.CreateDate = DateTime.Now;
                Sale.CreatorUserName = "system";
                Sale.ArchiveCode = ArchiveCode;
                Sale.IsDeleted = false;
                Sale.AccountantRemark = Reception.ReceptionNember;
                Sale.RowNumber = rownumber;

                Sale.Bed = 0;
                Sale.Bes = Reception.SaleWarrantyAmount;

                articles.Add(Sale);
                rownumber++;

            }
            if (Reception.SaleFreeAmount > 0)
            {
                Acc_Coding_Tafsil SaleTafsil = new Acc_Coding_Tafsil();
                if (!accountingSetting.SaleFreeTafsilId.HasValue)
                {
                    result.Message = "در تنظیمات حسابداری پذیرش، تفصیل فروش قطعه آزاد مشخص نشده است";
                    return result;
                }
                SaleTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.SaleFreeTafsilId.Value);

                Acc_Article Sale = new Acc_Article();
                Sale.Id = Guid.NewGuid();
                Sale.SellerId = sellerId;
                Sale.DocId = doc.Id;
                Sale.PeriodId = doc.PeriodId;
                Sale.KolId = SaleMoein.KolId;
                Sale.MoeinId = SaleMoein.Id;
                Sale.Tafsil4Id = SaleTafsil != null ? SaleTafsil.Id : null;
                Sale.Tafsil4Name = SaleTafsil != null ? SaleTafsil.Name : null;
                Sale.Comment = comment;
                Sale.CreateDate = DateTime.Now;
                Sale.CreatorUserName = "system";
                Sale.ArchiveCode = ArchiveCode;
                Sale.IsDeleted = false;
                Sale.AccountantRemark = Reception.ReceptionNember;
                Sale.RowNumber = rownumber;

                Sale.Bed = 0;
                Sale.Bes = Reception.SaleFreeAmount;

                articles.Add(Sale);
                rownumber++;
            }

            //   =====================================================================================
            // 3 ===========================================================================> Ojrat

            if (!accountingSetting.NonCommercialCreditorMoeinId.HasValue)
            {
                result.Message = "در تنظیمات حسابداری پذیرش، حساب معین 'بستانکاران غیرتجاری' مشخص نشده است";
                return result;
            }
            var nonCommercialCreditorMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.NonCommercialCreditorMoeinId.Value);

            //  گارانتی
            if (Reception.Items.Where(n => n.IncomeCode == 1).Any())
            {
                if (!accountingSetting.WarrantyTafsilId.HasValue)
                {
                    result.Message = "در تنظیمات حسابداری پذیرش، تفصیل درآمد گارانتی مشخص نشده است";
                    return result;
                }
                var warrantyTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.WarrantyTafsilId.Value);

                var itemGrouped = Reception.Items.Where(n => n.IncomeCode == 1).GroupBy(n => new { n.ContractorId, n.ServiceId })
                   .Select(n => new SaveDetails_ItemDto
                   {
                       ContractorId = n.Key.ContractorId,
                       ServiceId = n.Key.ServiceId,
                       ItemName = n.FirstOrDefault().ItemName,
                       ContractorShareAmount = n.Sum(s => s.ContractorShareAmount),
                       Price = n.Sum(s => s.Price)
                   }).OrderByDescending(n => n.Price).ToList();

                foreach (var item in itemGrouped)
                {
                    Acc_Coding_Tafsil contractorTafsil = new Acc_Coding_Tafsil();
                    if (!item.ServiceId.HasValue)
                    {
                        result.Message = $" در ردیف {item.ItemName} نوع سرویس شناسایی نشد";
                        return result;
                    }
                    var service = await _db.Asa_Services.FindAsync(item.ServiceId.Value);
                    if (service == null)
                    {
                        result.Message = $" در ردیف {item.ItemName} نوع سرویس شناسایی نشد";
                        return result;
                    }
                    Acc_Coding_Moein IncomMoein = await _db.Acc_Coding_Moeins.FindAsync(service.MoeinId);

                    if (!item.ContractorId.HasValue)
                    {
                        result.Message = $" در ردیف {item.ItemName} حساب تفصیلی پیمانکار شناسایی نشد";
                        return result;
                    }
                    var contractor = await _db.Asa_Contractors.Include(n => n.ContractorTafsil).SingleOrDefaultAsync(n => n.Id == item.ContractorId.Value);

                    if (item.ContractorShareAmount > 0)
                    {
                        // بستانکاری پیمانکار
                        Acc_Article ContractorBesArt = new Acc_Article();
                        ContractorBesArt.Id = Guid.NewGuid();
                        ContractorBesArt.SellerId = sellerId;
                        ContractorBesArt.DocId = doc.Id;
                        ContractorBesArt.PeriodId = doc.PeriodId;
                        ContractorBesArt.KolId = nonCommercialCreditorMoein.KolId;
                        ContractorBesArt.MoeinId = nonCommercialCreditorMoein.Id;
                        ContractorBesArt.Tafsil4Id = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Id : null;
                        ContractorBesArt.Tafsil4Name = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Name : null;
                        ContractorBesArt.Tafsil5Id = warrantyTafsil != null ? warrantyTafsil.Id : null;
                        ContractorBesArt.Tafsil5Name = warrantyTafsil != null ? warrantyTafsil.Name : null;
                        ContractorBesArt.Comment = comment;
                        ContractorBesArt.CreateDate = DateTime.Now;
                        ContractorBesArt.CreatorUserName = "system";
                        ContractorBesArt.ArchiveCode = ArchiveCode;
                        ContractorBesArt.IsDeleted = false;
                        ContractorBesArt.AccountantRemark = Reception.ReceptionNember;
                        ContractorBesArt.RowNumber = rownumber;

                        ContractorBesArt.Bed = 0;
                        ContractorBesArt.Bes = item.ContractorShareAmount;

                        articles.Add(ContractorBesArt);
                        rownumber++;
                    }


                    // درآمد حاصل از حدمات
                    Acc_Article IncomArt = new Acc_Article();
                    IncomArt.Id = Guid.NewGuid();
                    IncomArt.SellerId = sellerId;
                    IncomArt.DocId = doc.Id;
                    IncomArt.PeriodId = doc.PeriodId;
                    IncomArt.KolId = IncomMoein.KolId;
                    IncomArt.MoeinId = IncomMoein.Id;
                    IncomArt.Tafsil4Id = warrantyTafsil != null ? warrantyTafsil.Id : null;
                    IncomArt.Tafsil4Name = warrantyTafsil != null ? warrantyTafsil.Name : null;
                    IncomArt.Tafsil5Id = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Id : null;
                    IncomArt.Tafsil5Name = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Name : null;
                    IncomArt.Comment = comment;
                    IncomArt.CreateDate = DateTime.Now;
                    IncomArt.CreatorUserName = "system";
                    IncomArt.ArchiveCode = ArchiveCode;
                    IncomArt.IsDeleted = false;
                    IncomArt.AccountantRemark = Reception.ReceptionNember;
                    IncomArt.RowNumber = rownumber;

                    IncomArt.Bed = 0;
                    IncomArt.Bes = item.Price - item.ContractorShareAmount;

                    articles.Add(IncomArt);
                    rownumber++;

                }

            }
            //آزاد
            if (Reception.Items.Where(n => n.IncomeCode == 2).Any())
            {
                if (!accountingSetting.FreeTafsiltId.HasValue)
                {
                    result.Message = "در تنظیمات حسابداری پذیرش، تفصیل درآمد آزاد مشخص نشده است";
                    return result;
                }
                var IncomTypeTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.FreeTafsiltId.Value);

                var itemGrouped = Reception.Items.Where(n => n.IncomeCode == 2).GroupBy(n => new { n.ContractorId, n.ServiceId })
                    .Select(n => new SaveDetails_ItemDto
                    {
                        ContractorId = n.Key.ContractorId,
                        ServiceId = n.Key.ServiceId,
                        ItemName = n.FirstOrDefault().ItemName,
                        ContractorShareAmount = n.Sum(s => s.ContractorShareAmount),
                        Price = n.Sum(s => s.Price)
                    }).OrderByDescending(n => n.Price).ToList();

                foreach (var item in itemGrouped)
                {

                    Acc_Coding_Tafsil contractorTafsil = new Acc_Coding_Tafsil();
                    if (!item.ServiceId.HasValue)
                    {
                        result.Message = $" در ردیف {item.ItemName} نوع سرویس شناسایی نشد";
                        return result;
                    }
                    var service = await _db.Asa_Services.FindAsync(item.ServiceId.Value);
                    if (service == null)
                    {
                        result.Message = $" در ردیف {item.ItemName} نوع سرویس شناسایی نشد";
                        return result;
                    }
                    Acc_Coding_Moein IncomMoein = await _db.Acc_Coding_Moeins.FindAsync(service.MoeinId);

                    if (!item.ContractorId.HasValue)
                    {
                        result.Message = $" در ردیف {item.ItemName} حساب تفصیلی پیمانکار شناسایی نشد";
                        return result;
                    }
                    var contractor = await _db.Asa_Contractors.Include(n => n.ContractorTafsil).SingleOrDefaultAsync(n => n.Id == item.ContractorId.Value);

                    if (item.ContractorShareAmount > 0)
                    {
                        // بستانکاری پیمانکار
                        Acc_Article ContractorBesArt = new Acc_Article();
                        ContractorBesArt.Id = Guid.NewGuid();
                        ContractorBesArt.SellerId = sellerId;
                        ContractorBesArt.DocId = doc.Id;
                        ContractorBesArt.PeriodId = doc.PeriodId;
                        ContractorBesArt.KolId = nonCommercialCreditorMoein.KolId;
                        ContractorBesArt.MoeinId = nonCommercialCreditorMoein.Id;
                        ContractorBesArt.Tafsil4Id = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Id : null;
                        ContractorBesArt.Tafsil4Name = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Name : null;
                        ContractorBesArt.Tafsil5Id = IncomTypeTafsil != null ? IncomTypeTafsil.Id : null;
                        ContractorBesArt.Tafsil5Name = IncomTypeTafsil != null ? IncomTypeTafsil.Name : null;
                        ContractorBesArt.Comment = comment;
                        ContractorBesArt.CreateDate = DateTime.Now;
                        ContractorBesArt.CreatorUserName = "system";
                        ContractorBesArt.ArchiveCode = ArchiveCode;
                        ContractorBesArt.IsDeleted = false;
                        ContractorBesArt.AccountantRemark = Reception.ReceptionNember;
                        ContractorBesArt.RowNumber = rownumber;

                        ContractorBesArt.Bed = 0;
                        ContractorBesArt.Bes = item.ContractorShareAmount;

                        articles.Add(ContractorBesArt);
                        rownumber++;
                    }


                    // درآمد حاصل از حدمات
                    Acc_Article IncomArt = new Acc_Article();
                    IncomArt.Id = Guid.NewGuid();
                    IncomArt.SellerId = sellerId;
                    IncomArt.DocId = doc.Id;
                    IncomArt.PeriodId = doc.PeriodId;
                    IncomArt.KolId = IncomMoein.KolId;
                    IncomArt.MoeinId = IncomMoein.Id;
                    IncomArt.Tafsil4Id = IncomTypeTafsil != null ? IncomTypeTafsil.Id : null;
                    IncomArt.Tafsil4Name = IncomTypeTafsil != null ? IncomTypeTafsil.Name : null;
                    IncomArt.Tafsil5Id = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Id : null;
                    IncomArt.Tafsil5Name = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Name : null;
                    IncomArt.Comment = comment;
                    IncomArt.CreateDate = DateTime.Now;
                    IncomArt.CreatorUserName = "system";
                    IncomArt.ArchiveCode = ArchiveCode;
                    IncomArt.IsDeleted = false;
                    IncomArt.AccountantRemark = Reception.ReceptionNember;
                    IncomArt.RowNumber = rownumber;

                    IncomArt.Bed = 0;
                    IncomArt.Bes = item.Price - item.ContractorShareAmount;

                    articles.Add(IncomArt);
                    rownumber++;

                }
            }

            //   =====================================================================================
            // 4 ===========================================================================> VAT

            if (!accountingSetting.SaleVatMoeinId.HasValue)
            {
                result.Message = "در تنظیمات حسابداری پذیرش، معین ارزش افزوده فروش مشخص نشده است";
                return result;
            }
            var vatMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.SaleVatMoeinId.Value);

            if (Reception.VatWarranty > 0)
            {
                Acc_Article vat = new Acc_Article();
                vat.Id = Guid.NewGuid();
                vat.SellerId = sellerId;
                vat.DocId = doc.Id;
                vat.PeriodId = doc.PeriodId;
                vat.KolId = vatMoein.KolId;
                vat.MoeinId = vatMoein.Id;
                vat.Comment = comment;
                vat.CreateDate = DateTime.Now;
                vat.CreatorUserName = "system";
                vat.ArchiveCode = ArchiveCode;
                vat.IsDeleted = false;
                vat.AccountantRemark = Reception.ReceptionNember;
                vat.RowNumber = rownumber;

                vat.Bed = 0;
                vat.Bes = Reception.VatWarranty;
                articles.Add(vat);

            }
            if (Reception.VatFree > 0)
            {
                Acc_Article vat = new Acc_Article();
                vat.Id = Guid.NewGuid();
                vat.SellerId = sellerId;
                vat.DocId = doc.Id;
                vat.PeriodId = doc.PeriodId;
                vat.KolId = vatMoein.KolId;
                vat.MoeinId = vatMoein.Id;
                vat.Comment = comment;
                vat.CreateDate = DateTime.Now;
                vat.CreatorUserName = "system";
                vat.ArchiveCode = ArchiveCode;
                vat.IsDeleted = false;
                vat.AccountantRemark = Reception.ReceptionNember;
                vat.RowNumber = rownumber;

                vat.Bed = 0;
                vat.Bes = Reception.VatFree;
                articles.Add(vat);

            }

            //-----------------------------------------------

            try
            {
                _db.Acc_Articles.AddRange(articles);
                await _db.SaveChangesAsync();
                result.Id = doc.Id;
                result.DocNumber = doc.DocNumber;
                result.ArchiveCode = ArchiveCode;
                result.Success = true;
                result.Message = $"ثبت سند حسابداری پذیرش {Reception.ReceptionNember} با موفقیت انجام شد";
                result.Message += $"</br> <h4>شماره بایگانی :  {ArchiveCode}</h4> ";
            }
            catch
            {
                result.Success = false;
                result.Message = "خطایی در ثبت سند حسابداری ره داده است";

            }
            return result;
        }
        public async Task<clsResult> CreateLamaryReceptionDocAsync(SaveDetailsDto Reception)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (Reception == null)
            {
                result.Message = "آیتمی برای ثبت وجود ندارد";
                return result;
            }
            if (!_userContext.PeriodId.HasValue)
            {
                result.Message = "اطلاعات سال مالی یافت نشد";
                return result;
            }
            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "اطلاعات شرکت فعال یافت نشد";
                return result;
            }
            long sellerId = _userContext.SellerId.Value;
            int periodId = _userContext.PeriodId.Value;
            var period = await _db.Acc_FinancialPeriods.FindAsync(periodId);
            if (period == null)
            {
                result.Message = "اطلاعات دوره مالی یافت نشد";
                return result;
            }
            var accountingSetting = await _db.Asa_Settings.Where(n => n.SellerId == sellerId).FirstOrDefaultAsync();
            if (accountingSetting == null)
            {
                result.Message = "تنظیمات حسابداری پذیرش انجام نشده";
                return result;
            }

            var hasDoc = await _db.Acc_Articles.Where(n => (!n.Doc.IsDeleted && !n.IsDeleted) && n.AccountantRemark == Reception.ReceptionNember).ToListAsync();
            if (hasDoc.Any())
            {
                var art = hasDoc.FirstOrDefault();
                result.Message = $"پذیرش {Reception.ReceptionNember} پیش از این با شماره بایگانی {art.ArchiveCode} ثبت شده است.";
                return result;
            }

            long totalAmount = Reception.TotalOjratAmount + Reception.TotalSaleAmount;
            long vatAmount = (long)(totalAmount * (period.DefualtVatRate ?? 0) / 100);

            Acc_Document doc = new Acc_Document();
            List<Acc_Article> articles = new List<Acc_Article>();

            // 0 ====> Doc Header
            int docAutonumber = await _docServic.DocAutoNumberGeneratorAsync(sellerId, periodId);
            int docNumber = await _docServic.DocNumberGeneratorAsync(sellerId, periodId);
            string ArchiveCode = "";
            int rownumber = 1;
            if (!Reception.CreateNewDoc)
            {
                var query = _db.Acc_Documents.Include(n => n.DocArticles)
                .Where(n => !n.IsDeleted && n.SellerId == sellerId && n.DocDate.Date.Date == Reception.InvoiceDate.Date).
                AsQueryable();
                var docInDate = await query.FirstOrDefaultAsync();

                if (docInDate != null)
                {
                    doc = docInDate;
                    ArchiveCode = await _docServic.GetDocArchiveNumber(doc.Id);
                    rownumber = docInDate.DocArticles != null && docInDate.DocArticles.Count() > 0 ? docInDate.DocArticles.Where(n => !n.IsDeleted).Max(n => n.RowNumber) + 1 : 1;
                }
                else
                {
                    doc.Id = Guid.NewGuid();
                    doc.SellerId = sellerId;
                    doc.PeriodId = periodId;
                    doc.DocDate = Reception.InvoiceDate;
                    doc.AtfNumber = docAutonumber;
                    doc.AutoDocNumber = docAutonumber;
                    doc.DocNumber = docNumber;
                    doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                    doc.StatusId = 1;
                    doc.SubsystemId = 2;
                    doc.CreateDate = DateTime.Now;
                    doc.CreatorUserName = "system";
                    doc.IsDeleted = false;
                    doc.TypeId = 1;
                    doc.SubsystemRef = DateTime.Now.ToLong();

                    _db.Acc_Documents.Add(doc);

                    ArchiveCode = $"{docNumber}/001";
                }
            }
            else
            {
                doc.Id = Guid.NewGuid();
                doc.SellerId = sellerId;
                doc.PeriodId = periodId;
                doc.DocDate = Reception.Date;
                doc.AtfNumber = docAutonumber;
                doc.AutoDocNumber = docAutonumber;
                doc.DocNumber = docNumber;
                doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                doc.StatusId = 1;
                doc.SubsystemId = 2;
                doc.CreateDate = DateTime.Now;
                doc.CreatorUserName = "system";
                doc.IsDeleted = false;
                doc.TypeId = 1;
                doc.SubsystemRef = DateTime.Now.ToLong();

                _db.Acc_Documents.Add(doc);

                ArchiveCode = $"{docNumber}/001";
            }

            //== End Create Doc Header =========================================
            //General

            string comment = $"{Reception.ClientName} - {Reception.CarNumber} - پ {Reception.ReceptionNember} - {Reception.Brand}";

            //  1 =============================================================> Commercial Bebtor Article / Bank

            if (Reception.WarrantyTotalPrice > 0)
            {
                Acc_Coding_Moein DebtorMoein = new Acc_Coding_Moein();
                Acc_Coding_Tafsil DebtorTafsil = new Acc_Coding_Tafsil();
                if (!accountingSetting.CommercialDebtorMoeinId.HasValue || !accountingSetting.DebtorTafsil4Warranty.HasValue)
                {
                    result.Message = "در بخش تنظیمات، حساب معین بدهکاران تجاری یا تفیل آن  مشحص نشده است.";
                    return result;
                }
                DebtorMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.CommercialDebtorMoeinId.Value);
                DebtorTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.DebtorTafsil4Warranty);

                Acc_Article DebtorArt = new Acc_Article();
                DebtorArt.Id = Guid.NewGuid();
                DebtorArt.SellerId = sellerId;
                DebtorArt.DocId = doc.Id;
                DebtorArt.PeriodId = doc.PeriodId;
                DebtorArt.KolId = DebtorMoein.KolId;
                DebtorArt.MoeinId = DebtorMoein.Id;
                DebtorArt.Tafsil4Id = DebtorTafsil != null ? DebtorTafsil.Id : null;
                DebtorArt.Tafsil4Name = DebtorTafsil != null ? DebtorTafsil.Name : null;
                DebtorArt.Comment = comment;
                DebtorArt.CreateDate = DateTime.Now;
                DebtorArt.CreatorUserName = "system";
                DebtorArt.ArchiveCode = ArchiveCode;
                DebtorArt.IsDeleted = false;
                DebtorArt.AccountantRemark = Reception.ReceptionNember;
                DebtorArt.RowNumber = rownumber;

                DebtorArt.Bed = Reception.WarrantyTotalPrice;
                DebtorArt.Bes = 0;

                articles.Add(DebtorArt);
                rownumber++;
            }

            if (Reception.FreeTotalPrice > 0)
            {
                Acc_Coding_Moein DebtorMoein = new Acc_Coding_Moein();
                Acc_Coding_Tafsil DebtorTafsil = new Acc_Coding_Tafsil();

                if (!accountingSetting.BankMoeinId.HasValue)
                {
                    result.Message = "در بخش تنظیمات، حساب معین بانک ها مشحص نشده است.";
                    return result;
                }
                DebtorMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.BankMoeinId.Value);

                long? bankTafsil = Reception.BankTafsilId.HasValue ? Reception.BankTafsilId.Value : accountingSetting.BankTafsilId;
                if (!bankTafsil.HasValue)
                {
                    result.Message = "حساب تفصیل بانک مشخص نشده است";
                    return result;
                }
                DebtorTafsil = await _db.Acc_Coding_Tafsils.FindAsync(bankTafsil.Value);

                Acc_Article DebtorArt = new Acc_Article();
                DebtorArt.Id = Guid.NewGuid();
                DebtorArt.SellerId = sellerId;
                DebtorArt.DocId = doc.Id;
                DebtorArt.PeriodId = doc.PeriodId;
                DebtorArt.KolId = DebtorMoein.KolId;
                DebtorArt.MoeinId = DebtorMoein.Id;
                DebtorArt.Tafsil4Id = DebtorTafsil != null ? DebtorTafsil.Id : null;
                DebtorArt.Tafsil4Name = DebtorTafsil != null ? DebtorTafsil.Name : null;
                DebtorArt.Comment = comment;
                DebtorArt.CreateDate = DateTime.Now;
                DebtorArt.CreatorUserName = "system";
                DebtorArt.ArchiveCode = ArchiveCode;
                DebtorArt.IsDeleted = false;
                DebtorArt.AccountantRemark = Reception.ReceptionNember;
                DebtorArt.RowNumber = rownumber;

                DebtorArt.Bed = Reception.FreeTotalPrice;
                DebtorArt.Bes = 0;

                articles.Add(DebtorArt);
                rownumber++;
            }


            //=====================================================================================
            // 2 ===========================================================================> Sale

            Acc_Coding_Moein SaleMoein = new Acc_Coding_Moein();
            if (!accountingSetting.SaleMoeinId.HasValue)
            {
                result.Message = "در تنظیمات حسابداری پذیرش، حساب معین فروش مشخص نشده است";
                return result;
            }
            SaleMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.SaleMoeinId.Value);


            if (Reception.SaleWarrantyAmount > 0)
            {
                Acc_Coding_Tafsil SaleTafsil = new Acc_Coding_Tafsil();
                if (!accountingSetting.SaleWarrantyTfsilId.HasValue)
                {
                    result.Message = "در تنظیمات حسابداری پذیرش، تفصیل فروش قطعه گارانتی مشخص نشده است";
                    return result;
                }
                SaleTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.SaleWarrantyTfsilId.Value);

                Acc_Article Sale = new Acc_Article();
                Sale.Id = Guid.NewGuid();
                Sale.SellerId = sellerId;
                Sale.DocId = doc.Id;
                Sale.PeriodId = doc.PeriodId;
                Sale.KolId = SaleMoein.KolId;
                Sale.MoeinId = SaleMoein.Id;
                Sale.Tafsil4Id = SaleTafsil != null ? SaleTafsil.Id : null;
                Sale.Tafsil4Name = SaleTafsil != null ? SaleTafsil.Name : null;
                Sale.Comment = comment;
                Sale.CreateDate = DateTime.Now;
                Sale.CreatorUserName = "system";
                Sale.ArchiveCode = ArchiveCode;
                Sale.IsDeleted = false;
                Sale.AccountantRemark = Reception.ReceptionNember;
                Sale.RowNumber = rownumber;

                Sale.Bed = 0;
                Sale.Bes = Reception.SaleWarrantyAmount;

                articles.Add(Sale);
                rownumber++;

            }
            if (Reception.SaleFreeAmount > 0)
            {
                Acc_Coding_Tafsil SaleTafsil = new Acc_Coding_Tafsil();
                if (!accountingSetting.SaleFreeTafsilId.HasValue)
                {
                    result.Message = "در تنظیمات حسابداری پذیرش، تفصیل فروش قطعه آزاد مشخص نشده است";
                    return result;
                }
                SaleTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.SaleFreeTafsilId.Value);

                Acc_Article Sale = new Acc_Article();
                Sale.Id = Guid.NewGuid();
                Sale.SellerId = sellerId;
                Sale.DocId = doc.Id;
                Sale.PeriodId = doc.PeriodId;
                Sale.KolId = SaleMoein.KolId;
                Sale.MoeinId = SaleMoein.Id;
                Sale.Tafsil4Id = SaleTafsil != null ? SaleTafsil.Id : null;
                Sale.Tafsil4Name = SaleTafsil != null ? SaleTafsil.Name : null;
                Sale.Comment = comment;
                Sale.CreateDate = DateTime.Now;
                Sale.CreatorUserName = "system";
                Sale.ArchiveCode = ArchiveCode;
                Sale.IsDeleted = false;
                Sale.AccountantRemark = Reception.ReceptionNember;
                Sale.RowNumber = rownumber;

                Sale.Bed = 0;
                Sale.Bes = Reception.SaleFreeAmount;

                articles.Add(Sale);
                rownumber++;
            }


            //   =====================================================================================
            // 3 ===========================================================================> Ojrat

            if (!accountingSetting.NonCommercialCreditorMoeinId.HasValue)
            {
                result.Message = "در تنظیمات حسابداری پذیرش، حساب معین 'بستانکاران غیرتجاری' مشخص نشده است";
                return result;
            }
            var nonCommercialCreditorMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.NonCommercialCreditorMoeinId.Value);
            //var warrantyTafsil = await _db.Acc_Coding_Tafsils.FindAsync(accountingSetting.WarrantyTafsilId.Value);

            var ServiceType1 = Reception.Items.Where(n => n.LamaryServiceType == 1).ToList();
            var ServiceType2 = Reception.Items.Where(n => n.LamaryServiceType != 1).ToList();

            //==============================================================================
            // ...................  خدمات مربوط به سرویس دوره ای ...........................
            // =============================================================================
            if (ServiceType1 != null && ServiceType1.Any())
            {
                var groupType1 = ServiceType1.GroupBy(n => n.LamariServiceId)
              .Select(n => new SaveDetails_ItemDto
              {
                  LamariServiceId = n.Key,
                  ItemName = n.FirstOrDefault().ItemName,
                  lamariElectricShareAmount = n.Sum(s => s.lamariElectricShareAmount),
                  lamariMechanicShareAmount = n.Sum(s => s.lamariMechanicShareAmount),
                  lamariElectricContractor = n.FirstOrDefault().lamariElectricContractor,
                  lamariMechanicContractor = n.FirstOrDefault().lamariMechanicContractor,
                  Price = n.Sum(s => s.Price)
              }).OrderByDescending(n => n.Price).ToList();

                foreach (var item in groupType1)
                {
                    var service = await _db.Asa_LamariServices.FindAsync(item.LamariServiceId.Value);
                    if (service == null)
                    {
                        result.Message = $" در ردیف {item.ItemName} نوع سرویس شناسایی نشد";
                        return result;
                    }
                    Acc_Coding_Moein IncomMoein = await _db.Acc_Coding_Moeins.FindAsync(service.MoeinId);

                    if (item.lamariMechanicShareAmount > 0)
                    {
                        if (!item.lamariMechanicContractor.HasValue)
                        {
                            result.Message = $" در ردیف {item.ItemName} حساب تفصیلی مکانیک شناسایی نشد";
                            return result;
                        }
                        var mechanic = await _db.Asa_Contractors.Include(n => n.ContractorTafsil).SingleOrDefaultAsync(n => n.Id == item.lamariMechanicContractor.Value);

                        Acc_Article IncomMechanichArt = new Acc_Article();
                        IncomMechanichArt.Id = Guid.NewGuid();
                        IncomMechanichArt.SellerId = sellerId;
                        IncomMechanichArt.DocId = doc.Id;
                        IncomMechanichArt.PeriodId = doc.PeriodId;
                        IncomMechanichArt.KolId = IncomMoein.KolId;
                        IncomMechanichArt.MoeinId = IncomMoein.Id;
                        IncomMechanichArt.Tafsil4Id = mechanic != null ? mechanic.Id : null;
                        IncomMechanichArt.Tafsil4Name = mechanic != null ? mechanic.Name : null;
                        IncomMechanichArt.Comment = comment;
                        IncomMechanichArt.CreateDate = DateTime.Now;
                        IncomMechanichArt.CreatorUserName = "system";
                        IncomMechanichArt.ArchiveCode = ArchiveCode;
                        IncomMechanichArt.IsDeleted = false;
                        IncomMechanichArt.AccountantRemark = Reception.ReceptionNember;
                        IncomMechanichArt.RowNumber = rownumber;

                        IncomMechanichArt.Bed = 0;
                        IncomMechanichArt.Bes = item.lamariMechanicShareAmount;

                        articles.Add(IncomMechanichArt);
                        rownumber++;

                        //---------------------------------

                        Acc_Article mechanicCreatArt = new Acc_Article();
                        mechanicCreatArt.Id = Guid.NewGuid();
                        mechanicCreatArt.SellerId = sellerId;
                        mechanicCreatArt.DocId = doc.Id;
                        mechanicCreatArt.PeriodId = doc.PeriodId;
                        mechanicCreatArt.KolId = nonCommercialCreditorMoein.KolId;
                        mechanicCreatArt.MoeinId = nonCommercialCreditorMoein.Id;
                        mechanicCreatArt.Tafsil4Id = mechanic != null ? mechanic.Id : null;
                        mechanicCreatArt.Tafsil4Name = mechanic != null ? mechanic.Name : null;
                        mechanicCreatArt.Comment = comment;
                        mechanicCreatArt.CreateDate = DateTime.Now;
                        mechanicCreatArt.CreatorUserName = "system";
                        mechanicCreatArt.ArchiveCode = ArchiveCode;
                        mechanicCreatArt.IsDeleted = false;
                        mechanicCreatArt.AccountantRemark = Reception.ReceptionNember;
                        mechanicCreatArt.RowNumber = rownumber;

                        mechanicCreatArt.Bed = 0;
                        mechanicCreatArt.Bes = item.lamariMechanicShareAmount;

                        articles.Add(mechanicCreatArt);
                        rownumber++;
                    }

                    // === برقکار
                    if (item.lamariElectricShareAmount > 0)
                    {
                        if (!item.lamariElectricContractor.HasValue)
                        {
                            result.Message = $" در ردیف {item.ItemName} حساب تفصیلی برقکار شناسایی نشد";
                            return result;
                        }
                        var electrictafsil = await _db.Asa_Contractors.Include(n => n.ContractorTafsil).SingleOrDefaultAsync(n => n.Id == item.lamariElectricContractor.Value);

                        Acc_Article IncomElectricArt = new Acc_Article();
                        IncomElectricArt.Id = Guid.NewGuid();
                        IncomElectricArt.SellerId = sellerId;
                        IncomElectricArt.DocId = doc.Id;
                        IncomElectricArt.PeriodId = doc.PeriodId;
                        IncomElectricArt.KolId = IncomMoein.KolId;
                        IncomElectricArt.MoeinId = IncomMoein.Id;
                        IncomElectricArt.Tafsil4Id = electrictafsil != null ? electrictafsil.Id : null;
                        IncomElectricArt.Tafsil4Name = electrictafsil != null ? electrictafsil.Name : null;
                        IncomElectricArt.Comment = comment;
                        IncomElectricArt.CreateDate = DateTime.Now;
                        IncomElectricArt.CreatorUserName = "system";
                        IncomElectricArt.ArchiveCode = ArchiveCode;
                        IncomElectricArt.IsDeleted = false;
                        IncomElectricArt.AccountantRemark = Reception.ReceptionNember;
                        IncomElectricArt.RowNumber = rownumber;

                        IncomElectricArt.Bed = 0;
                        IncomElectricArt.Bes = item.lamariElectricShareAmount;

                        articles.Add(IncomElectricArt);
                        rownumber++;

                        //---------------------------------

                        Acc_Article ElectricCreatArt = new Acc_Article();
                        ElectricCreatArt.Id = Guid.NewGuid();
                        ElectricCreatArt.SellerId = sellerId;
                        ElectricCreatArt.DocId = doc.Id;
                        ElectricCreatArt.PeriodId = doc.PeriodId;
                        ElectricCreatArt.KolId = nonCommercialCreditorMoein.KolId;
                        ElectricCreatArt.MoeinId = nonCommercialCreditorMoein.Id;
                        ElectricCreatArt.Tafsil4Id = electrictafsil != null ? electrictafsil.Id : null;
                        ElectricCreatArt.Tafsil4Name = electrictafsil != null ? electrictafsil.Name : null;
                        ElectricCreatArt.Comment = comment;
                        ElectricCreatArt.CreateDate = DateTime.Now;
                        ElectricCreatArt.CreatorUserName = "system";
                        ElectricCreatArt.ArchiveCode = ArchiveCode;
                        ElectricCreatArt.IsDeleted = false;
                        ElectricCreatArt.AccountantRemark = Reception.ReceptionNember;
                        ElectricCreatArt.RowNumber = rownumber;

                        ElectricCreatArt.Bed = 0;
                        ElectricCreatArt.Bes = item.lamariElectricShareAmount;

                        articles.Add(ElectricCreatArt);
                        rownumber++;
                    }

                }

            }

            //==============================================================================
            // ............................... سایر خدمات ................................
            // =============================================================================

            if (ServiceType2 != null && ServiceType2.Any())
            {
                var itemGrouped = ServiceType2
                                 .GroupBy(n => n.ContractorId)
                                 .Select(n => new SaveDetails_ItemDto
                                 {
                                     ContractorId = n.Key,
                                     LamariServiceId = n.FirstOrDefault().LamariServiceId,
                                     ItemName = n.FirstOrDefault().ItemName,
                                     ContractorShareAmount = n.Sum(s => s.ContractorShareAmount),
                                     Price = n.Sum(s => s.Price)
                                 }).OrderByDescending(n => n.Price).ToList();

                foreach (var item in itemGrouped)
                {
                    if (!item.LamariServiceId.HasValue)
                    {
                        result.Message = $" در ردیف {item.ItemName} نوع سرویس شناسایی نشد";
                        return result;
                    }
                    var service = await _db.Asa_LamariServices.FindAsync(item.LamariServiceId.Value);
                    if (service == null)
                    {
                        result.Message = $" در ردیف {item.ItemName} نوع سرویس شناسایی نشد";
                        return result;
                    }
                    Acc_Coding_Moein IncomMoein = await _db.Acc_Coding_Moeins.FindAsync(service.MoeinId);

                    if (item.ContractorShareAmount > 0)
                    {

                        if (!item.ContractorId.HasValue)
                        {
                            result.Message = $" در ردیف {item.ItemName} حساب تفصیلی پیمانکار شناسایی نشد";
                            return result;
                        }
                        var contractor = await _db.Asa_Contractors.Include(n => n.ContractorTafsil).SingleOrDefaultAsync(n => n.Id == item.ContractorId.Value);

                        // بستانکاری پیمانکار
                        Acc_Article ContractorBesArt = new Acc_Article();
                        ContractorBesArt.Id = Guid.NewGuid();
                        ContractorBesArt.SellerId = sellerId;
                        ContractorBesArt.DocId = doc.Id;
                        ContractorBesArt.PeriodId = doc.PeriodId;
                        ContractorBesArt.KolId = nonCommercialCreditorMoein.KolId;
                        ContractorBesArt.MoeinId = nonCommercialCreditorMoein.Id;
                        ContractorBesArt.Tafsil4Id = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Id : null;
                        ContractorBesArt.Tafsil4Name = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Name : null;
                        ContractorBesArt.Comment = comment;
                        ContractorBesArt.CreateDate = DateTime.Now;
                        ContractorBesArt.CreatorUserName = "system";
                        ContractorBesArt.ArchiveCode = ArchiveCode;
                        ContractorBesArt.IsDeleted = false;
                        ContractorBesArt.AccountantRemark = Reception.ReceptionNember;
                        ContractorBesArt.RowNumber = rownumber;

                        ContractorBesArt.Bed = 0;
                        ContractorBesArt.Bes = item.ContractorShareAmount;

                        articles.Add(ContractorBesArt);
                        rownumber++;


                        // درآمد حاصل از حدمات
                        Acc_Article IncomArt = new Acc_Article();
                        IncomArt.Id = Guid.NewGuid();
                        IncomArt.SellerId = sellerId;
                        IncomArt.DocId = doc.Id;
                        IncomArt.PeriodId = doc.PeriodId;
                        IncomArt.KolId = IncomMoein.KolId;
                        IncomArt.MoeinId = IncomMoein.Id;
                        IncomArt.Tafsil4Id = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Id : null;
                        IncomArt.Tafsil4Name = contractor.ContractorTafsil != null ? contractor.ContractorTafsil.Name : null;
                        IncomArt.Comment = comment;
                        IncomArt.CreateDate = DateTime.Now;
                        IncomArt.CreatorUserName = "system";
                        IncomArt.ArchiveCode = ArchiveCode;
                        IncomArt.IsDeleted = false;
                        IncomArt.AccountantRemark = Reception.ReceptionNember;
                        IncomArt.RowNumber = rownumber;

                        IncomArt.Bed = 0;
                        IncomArt.Bes = item.Price - item.ContractorShareAmount;

                        articles.Add(IncomArt);
                        rownumber++;
                    }
                }
            }


            //   =====================================================================================
            // 4 ================================================================================> VAT

            if (!accountingSetting.SaleVatMoeinId.HasValue)
            {
                result.Message = "در تنظیمات حسابداری پذیرش، معین ارزش افزوده فروش مشخص نشده است";
                return result;
            }
            var vatMoein = await _db.Acc_Coding_Moeins.FindAsync(accountingSetting.SaleVatMoeinId.Value);

            if (Reception.VatAmount > 0)
            {
                Acc_Article vat = new Acc_Article();
                vat.Id = Guid.NewGuid();
                vat.SellerId = sellerId;
                vat.DocId = doc.Id;
                vat.PeriodId = doc.PeriodId;
                vat.KolId = vatMoein.KolId;
                vat.MoeinId = vatMoein.Id;
                vat.Comment = comment;
                vat.CreateDate = DateTime.Now;
                vat.CreatorUserName = "system";
                vat.ArchiveCode = ArchiveCode;
                vat.IsDeleted = false;
                vat.AccountantRemark = Reception.ReceptionNember;
                vat.RowNumber = rownumber;

                vat.Bed = 0;
                vat.Bes = Reception.VatAmount;
                articles.Add(vat);

            }
            //-----------------------------------------------

            try
            {
                _db.Acc_Articles.AddRange(articles);
                await _db.SaveChangesAsync();
                result.Id = doc.Id;
                result.DocNumber = doc.DocNumber;
                result.ArchiveCode = ArchiveCode;
                result.Success = true;
                result.Message = $"ثبت سند حسابداری پذیرش {Reception.ReceptionNember} با موفقیت انجام شد";
                result.Message += $"</br> <h4>شماره بایگانی :  {ArchiveCode}</h4> ";
            }
            catch
            {
                result.Success = false;
                result.Message = "خطایی در ثبت سند حسابداری ره داده است";

            }
            return result;
        }
        private class InvoiceDocInfo
        {
            public Guid InvoiceId { get; set; }
            public Guid DocId { get; set; }
            public int DocAutoNumber { get; set; }
        }


        //=============== Modaian ==========================================================================================================
        //==================================================== Modaian =====================================================================
        //================================================================================== Modaian =======================================
        public async Task<clsResult> CreateMoadianDocAsync(MoadianCreateDocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطاعاتی یافت نشد";

            if (!_sellerId.HasValue)
            {
                result.Message = "شرکت فعال یافت نشد";
                return result;
            }
            if (dto == null) return result;

            if (dto.PeriodId == 0)
            {
                result.Message = "سال مالی فعال یافت نشد";
                return result;
            }
            if (!dto.TransactionsId.Any())
            {
                result.Message = "اطلاعاتی برای ثبت وجود ندارد";
                return result;
            }

            // Debtor Account Info
            var BedMoein = await _db.Acc_Coding_Moeins.Where(n => n.Id == dto.BedMoeinId).FirstOrDefaultAsync();
            if (BedMoein == null)
            {
                result.Message = "اطلاعات حساب طرف بدهکار سند یافت نشد";
                return result;
            }
            // Credator Account Info
            var BesMoein = await _db.Acc_Coding_Moeins.Where(n => n.Id == dto.BesMoeinId).FirstOrDefaultAsync();
            if (BesMoein == null)
            {
                result.Message = "اطلاعات حساب طرف بدهکار سند یافت نشد";
                return result;
            }

            if (dto.transactionsType == 0)
            {
                result.Message = "نوع اطلاعات (خرید/فروش) مشخص نیست";
                return result;
            }
            //=============================================================

            var transactions = await _db.Acc_ModianReports
                .Where(n => dto.TransactionsId.Contains(n.Id))
                .OrderBy(n => n.IssueDate).ToListAsync();
            if (transactions == null) return result;

            //--
            List<Acc_Document> Docs = new List<Acc_Document>();
            List<Acc_Article> Articles = new List<Acc_Article>();

            //---- گروه بندی تراکنش ها بر اساس تاریخ تراکنش
            var dateGrouped = transactions.GroupBy(n => new { n.IssueDate, n.TaxNumber }).Select(n => new
            {
                date = n.Key.IssueDate,
                TaxNumber = n.Key.TaxNumber,
                batchNumber = n.FirstOrDefault().Batchnuber.ToString() ?? "",
                transaction = n

            }).ToList();

            int docAutonumber = await _docServic.DocAutoNumberGeneratorAsync(dto.SellerId, dto.PeriodId);
            int docNumber = await _docServic.DocNumberGeneratorAsync(dto.SellerId, dto.PeriodId);
            int rownumber = 1;
            foreach (var x in dateGrouped)
            {
                var hasDoc = await _db.Acc_Articles.AsNoTracking().Include(n => n.Doc)
                    .Where(n => n.Doc.SellerId == dto.SellerId && (!n.IsDeleted && !n.Doc.IsDeleted) && n.ArchiveCode == x.TaxNumber).FirstOrDefaultAsync();
                if (hasDoc != null)
                {
                    result.Message = $"صورتحساب {x.TaxNumber} در سند {hasDoc.Doc.DocNumber} ردیف {hasDoc.RowNumber} دارای ثبت حسابداری می باشد ";
                    return result;
                }
                //======= Doc Header
                Acc_Document doc = new Acc_Document();
                if (!dto.CreateNewDoc)
                {
                    var query = _db.Acc_Documents.Include(n => n.DocArticles)
                      .Where(n => !n.IsDeleted && n.SellerId == dto.SellerId && n.PeriodId == dto.PeriodId && n.DocDate.Date == x.date.Date).
                    AsQueryable();

                    if (!string.IsNullOrEmpty(dto.DocSelector))
                        query = query.Where(n => n.Description == dto.DocSelector);

                    var docInDate = await query.FirstOrDefaultAsync();

                    if (docInDate != null)
                    {
                        doc = docInDate;
                        if (docInDate.DocArticles != null && docInDate.DocArticles.Any())
                            rownumber = docInDate.DocArticles.Select(m => m.RowNumber).Max() + 1;
                        else
                            rownumber = 1;
                    }
                    else
                    {
                        doc.Id = Guid.NewGuid();
                        doc.SellerId = dto.SellerId;
                        doc.PeriodId = dto.PeriodId;
                        doc.DocDate = x.date;
                        doc.AtfNumber = docAutonumber;
                        doc.AutoDocNumber = docAutonumber;
                        doc.DocNumber = docNumber;
                        doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                        if (!string.IsNullOrEmpty(dto.DocSelector))
                            doc.Description = dto.DocSelector;
                        doc.StatusId = 1;
                        doc.SubsystemId = 2;
                        doc.CreateDate = DateTime.Now;
                        doc.CreatorUserName = dto.UserName;
                        doc.IsDeleted = false;
                        doc.TypeId = 1;
                        Docs.Add(doc);

                        docAutonumber++;
                        docNumber++;
                    }

                    var docinquee = Docs.Where(n => n.DocDate.Date == x.date.Date).FirstOrDefault();
                    if (docinquee != null)
                    {
                        if (Articles.Where(n => n.DocId == docinquee.Id).Any())
                            rownumber = Articles.Where(n => n.DocId == docinquee.Id).Select(m => m.RowNumber).Max() + 1;
                    }

                }
                else
                {
                    doc.Id = Guid.NewGuid();
                    doc.SellerId = dto.SellerId;
                    doc.PeriodId = dto.PeriodId;
                    doc.DocDate = x.date;
                    doc.AtfNumber = docAutonumber;
                    doc.AutoDocNumber = docAutonumber;
                    doc.DocNumber = docNumber;
                    doc.Description = $"بابت ثبت حسابداری اسناد ضمیمه";
                    if (!string.IsNullOrEmpty(dto.DocSelector))
                        doc.Description = dto.DocSelector;
                    doc.StatusId = 1;
                    doc.SubsystemId = 2;
                    doc.CreateDate = DateTime.Now;
                    doc.CreatorUserName = dto.UserName;
                    doc.IsDeleted = false;
                    doc.TypeId = 1;
                    Docs.Add(doc);

                    docAutonumber++;
                    docNumber++;
                }

                //== End Create Doc Header =========================================

                //--- 
                Acc_Coding_Tafsil? Bedtafsil4 = new Acc_Coding_Tafsil();
                if (dto.BedTafsil4Id.HasValue)
                    Bedtafsil4 = await _db.Acc_Coding_Tafsils.FindAsync(dto.BedTafsil4Id.Value);
                Acc_Coding_Tafsil? Bedtafsil5 = new Acc_Coding_Tafsil();
                if (dto.BedTafsil5Id.HasValue)
                    Bedtafsil5 = await _db.Acc_Coding_Tafsils.FindAsync(dto.BedTafsil5Id.Value);

                //----
                Acc_Coding_Tafsil? Bestafsil4 = new Acc_Coding_Tafsil();
                if (dto.BesTafsil4Id.HasValue)
                    Bestafsil4 = await _db.Acc_Coding_Tafsils.FindAsync(dto.BesTafsil4Id.Value);
                Acc_Coding_Tafsil? Bestafsil5 = new Acc_Coding_Tafsil();
                if (dto.BesTafsil5Id.HasValue)
                    Bestafsil5 = await _db.Acc_Coding_Tafsils.FindAsync(dto.BesTafsil5Id.Value);

                //--- مبلغ 
                long totalAmount = x.transaction.Sum(n => n.InvoiceAmountWithoutVAT);
                long totalVat = x.transaction.Sum(n => n.VAT);


                Acc_Article VatArticle = new Acc_Article();
                if (totalVat > 0)
                {
                    var accountingSettings = await _db.Acc_Settings.Where(n => n.SellerId == dto.SellerId).FirstOrDefaultAsync();

                    if (dto.transactionsType == 1 && !accountingSettings.BuyVatMoeinId.HasValue)
                    {
                        result.Message = "در تنظیمات حسابداری، حساب معین ارزش افزوده خرید تنظیم نشده است.";
                        return result;
                    }
                    else if (dto.transactionsType > 1 && !accountingSettings.SaleVatMoeinId.HasValue)
                    {
                        result.Message = "در تنظیمات حسابداری، حساب معین ارزش افزوده فروش تنظیم نشده است.";
                        return result;
                    }

                    int moeinId = dto.transactionsType == 1 ? accountingSettings.BuyVatMoeinId.Value : accountingSettings.SaleVatMoeinId.Value;
                    var VatAccount = await _db.Acc_Coding_Moeins.FirstOrDefaultAsync(n => n.Id == moeinId);

                    Acc_Coding_Tafsil? vatTafsil = new Acc_Coding_Tafsil();
                    vatTafsil = dto.transactionsType == 1 ? Bedtafsil4 : Bestafsil4;

                    VatArticle.Id = Guid.NewGuid();
                    VatArticle.SellerId = dto.SellerId;
                    VatArticle.DocId = doc.Id;
                    VatArticle.PeriodId = doc.PeriodId;
                    VatArticle.KolId = VatAccount.KolId;
                    VatArticle.MoeinId = VatAccount.Id;
                    VatArticle.Tafsil4Id = vatTafsil != null ? vatTafsil.Id : null;
                    VatArticle.Tafsil4Name = vatTafsil != null ? vatTafsil.Name : null;
                    VatArticle.Amount = totalVat;
                    VatArticle.Comment = dto.Descriptions;
                    VatArticle.CreateDate = DateTime.Now;
                    VatArticle.CreatorUserName = dto.UserName;
                    VatArticle.ArchiveCode = x.TaxNumber;
                    VatArticle.AccountantRemark = x.batchNumber;
                    VatArticle.IsDeleted = false;

                }

                //-- طرف بدهکار سند
                Acc_Article BedArt = new Acc_Article();
                BedArt.Id = Guid.NewGuid();
                BedArt.SellerId = dto.SellerId;
                BedArt.DocId = doc.Id;
                BedArt.PeriodId = doc.PeriodId;
                BedArt.RowNumber = rownumber;
                BedArt.KolId = BedMoein.KolId;
                BedArt.MoeinId = BedMoein.Id;
                BedArt.Tafsil4Id = Bedtafsil4 != null ? Bedtafsil4.Id : null;
                BedArt.Tafsil4Name = Bedtafsil4 != null ? Bedtafsil4.Name : null;
                BedArt.Tafsil5Id = Bedtafsil5 != null ? Bedtafsil5.Id : null;
                BedArt.Tafsil5Name = Bedtafsil5 != null ? Bedtafsil5.Name : null;
                BedArt.Bed = dto.transactionsType == 1 ? totalAmount : totalAmount + totalVat;
                BedArt.Bes = 0;
                BedArt.Amount = dto.transactionsType == 1 ? totalAmount : totalAmount + totalVat;
                BedArt.Comment = dto.Descriptions;
                BedArt.CreateDate = DateTime.Now;
                BedArt.CreatorUserName = dto.UserName;
                BedArt.IsDeleted = false;
                BedArt.ArchiveCode = x.TaxNumber;
                BedArt.AccountantRemark = x.batchNumber;
                Articles.Add(BedArt);
                rownumber++;

                //-- ارزش افزوده خرید
                if (dto.transactionsType == 1 && totalVat > 0)
                {
                    VatArticle.RowNumber = rownumber;
                    VatArticle.Bed = totalVat;
                    VatArticle.Bes = 0;

                    Articles.Add(VatArticle);
                    rownumber++;
                }

                //-- طرف بستانکار سند
                Acc_Article BesArt = new Acc_Article();
                BesArt.Id = Guid.NewGuid();
                BesArt.SellerId = dto.SellerId;
                BesArt.DocId = doc.Id;
                BesArt.PeriodId = doc.PeriodId;
                BesArt.RowNumber = rownumber;
                BesArt.KolId = BesMoein.KolId;
                BesArt.MoeinId = BesMoein.Id;
                BesArt.Tafsil4Id = Bestafsil4 != null ? Bestafsil4.Id : null;
                BesArt.Tafsil4Name = Bestafsil4 != null ? Bestafsil4.Name : null;
                BesArt.Tafsil5Id = Bestafsil5 != null ? Bestafsil5.Id : null;
                BesArt.Tafsil5Name = Bestafsil5 != null ? Bestafsil5.Name : null;
                BesArt.Bed = 0;
                BesArt.Bes = dto.transactionsType > 1 ? totalAmount : totalAmount + totalVat;
                BesArt.Amount = dto.transactionsType > 1 ? totalAmount : totalAmount + totalVat;
                BesArt.Comment = dto.Descriptions;
                BesArt.CreateDate = DateTime.Now;
                BesArt.CreatorUserName = dto.UserName;
                BesArt.IsDeleted = false;
                BesArt.ArchiveCode = x.TaxNumber;
                BesArt.AccountantRemark = x.batchNumber;
                Articles.Add(BesArt);
                rownumber++;

                //-- ارزش افزوده خرید
                if (dto.transactionsType > 1 && totalVat > 0)
                {
                    VatArticle.RowNumber = rownumber;
                    VatArticle.Bed = 0;
                    VatArticle.Bes = totalVat;

                    Articles.Add(VatArticle);
                    rownumber++;
                }

            }

            try
            {
                _db.Acc_Documents.AddRange(Docs);
                _db.Acc_Articles.AddRange(Articles);
                foreach (var t in transactions)
                {
                    t.IsChecked = true;
                    t.HasAccountingDoc = true;
                }
                _db.Acc_ModianReports.UpdateRange(transactions);

                await _db.SaveChangesAsync();

                result.Message = "ثبت اسناد حسابداری با موفقیت انجام شد";
                result.Success = true;
            }
            catch (Exception er)
            {
                result.Message = "خطایی در ثبت اطلاعات رخ داده است";
                result.Message += "<br><br>" + er.Message;
            }

            return result;
        }
    }

}
