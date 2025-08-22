using ClosedXML.Excel;
using GarnetAccounting.Areas.Treasury.Dto;
using GarnetAccounting.Areas.Treasury.Models.Entities;
using GarnetAccounting.Areas.Treasury.TreasuryInterfaces;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Treasury.TreasuryServices
{
    public class TreBankImporterService : ITreBankImporterService
    {
        private readonly AppDbContext _db;

        public TreBankImporterService(AppDbContext db)
        {
            _db = db;
        }
        public SelectList Select_list_BankReportType()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "10", Text = "سامان" });
            items.Add(new SelectListItem { Value = "11", Text = "سامان (سدید)" });
            items.Add(new SelectListItem { Value = "20", Text = "تجارت" });
            items.Add(new SelectListItem { Value = "21", Text = "تجارت - اینترنت بانک" });
            items.Add(new SelectListItem { Value = "30", Text = "ملت" });
            items.Add(new SelectListItem { Value = "31", Text = "ملت 12" });
            items.Add(new SelectListItem { Value = "40", Text = "اقتصاد نوین" });
            items.Add(new SelectListItem { Value = "41", Text = "اقتصاد نوین (اینترنت بانک)" });
            items.Add(new SelectListItem { Value = "42", Text = "اقتصاد نوین (اینترنت بانک 2)" });
            items.Add(new SelectListItem { Value = "50", Text = "کشاورزی" });
            items.Add(new SelectListItem { Value = "60", Text = "رفاه (حساب جاری)" });
            items.Add(new SelectListItem { Value = "70", Text = "بانک شهر (اینترنت بانک )" });
            items.Add(new SelectListItem { Value = "80", Text = "پست بانک (اینتربانک)" });
            items.Add(new SelectListItem { Value = "90", Text = "بانک صادرات - سپهر" });
            items.Add(new SelectListItem { Value = "91", Text = "بانک صادرات" });
            items.Add(new SelectListItem { Value = "100", Text = "بانک سپه" });
            items.Add(new SelectListItem { Value = "102", Text = "بانک سپه (گردش سپرده جاری)" });
            items.Add(new SelectListItem { Value = "103", Text = "بانک سپه - اینترنت بانک" });
            items.Add(new SelectListItem { Value = "200", Text = "بانک ملی" });
            items.Add(new SelectListItem { Value = "300", Text = "پاسارگاد" });
            items.Add(new SelectListItem { Value = "400", Text = "پارسیان - اینترنت بانک (سپرده کوتاه مدت)" });

            return new SelectList(items, "Value", "Text");

        }
        public IQueryable<TreBankTransactionDto> GetAllTreBankTransactions(long sellerId, long accountId, DateTime fromDate, DateTime untilDate)
        {
            var query = _db.TreBankTransactions.AsQueryable();
            query = query
                .Where(c =>
                c.SellerId == sellerId
                && c.BankAccountId == accountId
                && (c.Date >= fromDate && c.Date <= untilDate));


            return query.Select(c => new TreBankTransactionDto
            {
                Id = c.Id,
                SellerId = c.SellerId,
                BankAccountId = c.BankAccountId,
                IsChecked = c.IsChecked,
                HasDoc = c.HasDoc,
                AccountHolderName = c.AccountHolderName,
                Row = c.Row,
                Date = c.Date,
                DocumentNumber = c.DocumentNumber,
                Debtor = c.Debtor,
                Creditor = c.Creditor,
                Balance = c.Balance,
                Branch = c.Branch,
                Time = c.Time,
                DocumentOrCheckNumber = c.DocumentOrCheckNumber,
                RelatedCustomerNumber = c.RelatedCustomerNumber,
                RelatedCustomerName = c.RelatedCustomerName,
                Description = c.Description,
                ReferenceId = c.ReferenceId,
                Note = c.Note,
                IBAN = c.IBAN,
                DepositorOrBeneficiary = c.DepositorOrBeneficiary,
                TransactionId = c.TransactionId,
                OperationCode = c.OperationCode,
                Operation = c.Operation
            });
        }
        public async Task<List<TreBankTransactionDto>> GetBankTransactionsAsync(BankReportFilterDto filter)
        {
            if (filter.AccountId == 0)
                return new List<TreBankTransactionDto>();

            var query = _db.TreBankTransactions.AsQueryable();
            query = query
                .Where(c =>
                c.SellerId == filter.SellerId
                && c.BankAccountId == filter.AccountId
                && (c.Date >= filter.FromDate && c.Date <= filter.ToDate));

            if (filter.HasDoc.HasValue)
                query = query.Where(n => n.HasDoc == filter.HasDoc);

            if (filter.IsChecked.HasValue)
                query = query.Where(n => n.IsChecked == filter.IsChecked);

            if (filter.Transactiontype == 1)
                query = query.Where(n => n.Debtor > 0);

            if (filter.Transactiontype == 2)
                query = query.Where(n => n.Creditor > 0);

            if (!string.IsNullOrEmpty(filter.Description))
            {
                if (filter.searchtype == 1)
                    query = query.Where(n => n.Description.Contains(filter.Description.Trim()) || n.Note.Contains(filter.Description.Trim()));
                else
                    query = query.Where(n => n.Description == filter.Description || n.Note == filter.Description);
            }


            var transactions = await query.Select(c => new TreBankTransactionDto
            {
                Id = c.Id,
                SellerId = c.SellerId,
                BankAccountId = c.BankAccountId,
                IsChecked = c.IsChecked,
                HasDoc = c.HasDoc,
                AccountHolderName = c.AccountHolderName,
                Row = c.Row,
                Date = c.Date,
                DocumentNumber = c.DocumentNumber,
                Debtor = c.Debtor,
                Creditor = c.Creditor,
                Balance = c.Balance,
                Branch = c.Branch,
                Time = c.Time,
                DocumentOrCheckNumber = c.DocumentOrCheckNumber,
                RelatedCustomerNumber = c.RelatedCustomerNumber,
                RelatedCustomerName = c.RelatedCustomerName,
                Description = c.Description,
                ReferenceId = c.ReferenceId,
                Note = c.Note,
                IBAN = c.IBAN,
                DepositorOrBeneficiary = c.DepositorOrBeneficiary,
                TransactionId = c.TransactionId,
                OperationCode = c.OperationCode,
                Operation = c.Operation
            }).ToListAsync();

            return transactions;

        }
        // BankReportType = 1?
        public async Task<clsResult> ImportSamanKotahModatAsync(BankImporterDto dto)
        {

            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();
            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();
                    var iban = worksheet.Cell(3, 1).GetValue<string>();

                    // تنظیم ردیف شروع به 8
                    int startRow = 8;

                    string batchNum = DateTime.Now.ToLong().ToString();
                    // شروع خواندن داده‌ها از ردیف 9 به بعد
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction
                            {
                                SellerId = dto.SellerId,
                                BankAccountId = dto.BankAccountId,

                                Row = row.Cell("A").GetValue<int>(),
                                Date = row.Cell("B").GetValue<string>().PersianToLatin(),
                                DocumentNumber = row.Cell("J").GetValue<string>(),
                                Debtor = row.Cell("F").GetValue<long>(),
                                Creditor = row.Cell("G").GetValue<long>(),
                                Balance = row.Cell("H").GetValue<long>(),
                                Branch = row.Cell("I").GetValue<string>(),
                                Time = row.Cell("G").GetValue<TimeSpan>(),
                                DocumentOrCheckNumber = row.Cell("K").GetValue<string>(),
                                Description = row.Cell("E").GetValue<string>(),
                                IBAN = iban,
                                Note = row.Cell("M").GetValue<string>(),
                                Operation = row.Cell("D").GetValue<string>(),
                                BatchNumber = batchNum,

                            };


                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }


                }
            }
            if (okTask)
            {
                await _db.TreBankTransactions.AddRangeAsync(BankTransactionList);
                try
                {
                    await _db.SaveChangesAsync();
                    result.Success = true;
                    result.Message = "ثبت گزارش بانکی با موفقیت انجام شد";
                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات پزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }

            }

            return result;
        }
        // Saman = 1
        public async Task<clsResult> ImportSamanAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 5;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("B").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("C").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Operation = row.Cell("D").GetValue<string?>();
                            transaction.Description = row.Cell("E").GetValue<string?>();
                            transaction.DocumentNumber = row.Cell("J").GetValue<string?>();

                            transaction.Debtor = row.Cell("F").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("G").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("H").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("I").GetValue<string?>();
                            transaction.BatchNumber = batchNum;
                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Bank Saman Sadid = 11
        public async Task<clsResult> ImportBankSamanSadidAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 1;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateTimeValue = row.Cell("B").GetValue<string>().Trim();
                            string dateValue = dateTimeValue.Substring(9, 10);
                            string timeValue = dateTimeValue.Substring(0, 8);
                            transaction.Date = dateValue.PersianToLatin();
                            transaction.Time = TimeSpan.TryParse(timeValue, out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Branch = row.Cell("I").GetValue<string?>(); // شعبه
                            transaction.DocumentNumber = row.Cell("C").GetValue<string?>(); // شماره سند
                            transaction.DocumentOrCheckNumber = row.Cell("K").GetValue<string?>(); // شماره سند
                            transaction.Description = row.Cell("E").GetValue<string?>(); // شرح
                            transaction.Note = row.Cell("L").GetValue<string?>(); // یادداشت

                            transaction.Operation = row.Cell("G").GetValue<string?>()?.Trim(); // عملیات
                            long amount = row.Cell("F").GetValue<long?>() ?? 0;
                            transaction.Debtor = transaction.Operation == "بستانکار" ? amount : 0; // واریز
                            transaction.Creditor = transaction.Operation == "بدهکار" ? amount : 0;  // برداشت
                            transaction.Balance = row.Cell("H").GetValue<long?>() ?? 0; // مانده
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // BankReportType = 2
        public async Task<clsResult> ImportTejaratAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();
                    var iban = (worksheet.Cell("L4").GetValue<string>() ?? "") +
                     (worksheet.Cell("K4").GetValue<string>() ?? "") +
                     (worksheet.Cell("L5").GetValue<string>() ?? "") +
                     (worksheet.Cell("K5").GetValue<string>() ?? "");

                    // تنظیم ردیف شروع به 7
                    int startRow = 5;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    // حذف تیتر ها
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction
                            {
                                SellerId = dto.SellerId,
                                BankAccountId = dto.BankAccountId,
                                Row = row.Cell("A").GetValue<int>(),
                                AccountHolderName = row.Cell("B").GetValue<string>(),
                                Date = row.Cell("D").GetValue<string>().PersianToLatin(),
                                DocumentNumber = row.Cell("O").GetValue<string>(),
                                Debtor = row.Cell("I").GetValue<long>(),
                                Creditor = row.Cell("H").GetValue<long>(),
                                Balance = row.Cell("J").GetValue<long>(),
                                Branch = row.Cell("G").GetValue<string>(),
                                Time = row.Cell("F").GetValue<TimeSpan>(),
                                Description = row.Cell("K").GetValue<string>(),
                                ReferenceId = row.Cell("T").GetValue<string>(),
                                TransactionId = row.Cell("Q").GetValue<string>(),
                                OperationCode = row.Cell("L").GetValue<string>(),
                                IBAN = iban,
                                Operation = row.Cell("M").GetValue<string>(),
                                BatchNumber = batchNum,
                            };


                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }
            }
            if (okTask)
            {
                await _db.TreBankTransactions.AddRangeAsync(BankTransactionList);
                try
                {
                    await _db.SaveChangesAsync();
                    result.Success = true;
                    result.Message = "ثبت گزارش بانکی با موفقیت انجام شد";
                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات پزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Bank Tejarat Internet Bank 21
        public async Task<clsResult> ImportTejaratInternetBankAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 12;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        int rowno = 1;
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("P").GetValue<string?>();
                            long accountNum = long.TryParse(strRow, out long bankNum) ? bankNum : 0;
                            if (accountNum == 0) continue;

                            transaction.Row = rowno;
                            rowno++;


                            string dateValue = row.Cell("O").GetValue<string>().Trim();
                            transaction.Date = dateValue.PersianToLatin();
                            string timeValue = row.Cell("N").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;
                            transaction.AccountHolderName = accountNum.ToString();
                            transaction.Branch = row.Cell("J").GetValue<string?>(); // شعبه
                            transaction.Operation = row.Cell("I").GetValue<string?>()?.Trim(); // عملیات
                            string? docNum = row.Cell("F").GetValue<string?>();
                            if (docNum == null || docNum == "0")
                                docNum = row.Cell("B").GetValue<string?>();
                            transaction.DocumentNumber = docNum; // شماره سند
                            transaction.Description = row.Cell("I").GetValue<string?>() + " " + row.Cell("H").GetValue<string?>() + " " + row.Cell("G").GetValue<string?>(); // شرح
                            transaction.Debtor = row.Cell("K").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("L").GetValue<long?>() ?? 0;  // برداشت
                            transaction.Balance = row.Cell("M").GetValue<long?>() ?? 0; // مانده
                            transaction.Note = row.Cell("I").GetValue<string?>(); // یادداشت
                            transaction.BatchNumber = batchNum;
                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // BankReportType = 3
        public async Task<clsResult> ImportMelatAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();
                    int startRow = 2;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {
                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("J").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("I").GetValue<string>().Trim();
                            string timeValue = row.Cell("H").GetValue<string>().Trim();
                            transaction.Date = dateValue.PersianToLatin();
                            transaction.Time = TimeSpan.TryParse(timeValue, out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Description = row.Cell("F").GetValue<string?>() + " " + row.Cell("E").GetValue<string?>();
                            transaction.Note = row.Cell("L").GetValue<string>().Trim();
                            transaction.DocumentNumber = row.Cell("D").GetValue<string?>();
                            transaction.Debtor = row.Cell("B").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("C").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("A").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("G").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"خطا در ردیف {row.RowNumber()}: {ex.Message}");
                            break; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }

            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }
                await _db.TreBankTransactions.AddRangeAsync(finalList);
                try
                {
                    await _db.SaveChangesAsync();
                    result.Success = true;
                    result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // BankReportType = 31
        public async Task<clsResult> ImportMelat12Async(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();
                    int startRow = 1;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {
                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("L").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("K").GetValue<string>().Trim();
                            string timeValue = row.Cell("J").GetValue<string>().Trim();
                            transaction.Date = dateValue.PersianToLatin();
                            transaction.Time = TimeSpan.TryParse(timeValue, out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Description = row.Cell("D").GetValue<string?>() + " " + row.Cell("E").GetValue<string?>();
                            transaction.DocumentNumber = row.Cell("F").GetValue<string?>();
                            transaction.Debtor = row.Cell("B").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("C").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("A").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("I").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"خطا در ردیف {row.RowNumber()}: {ex.Message}");
                            break; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }

            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }
                await _db.TreBankTransactions.AddRangeAsync(finalList);
                try
                {
                    await _db.SaveChangesAsync();
                    result.Success = true;
                    result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // BankReportType = 4
        public async Task<clsResult> ImportEghtesadNovinAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 15;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("AQ").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateTimeValue = row.Cell("AJ").GetValue<string>().Trim();
                            string dateValue = dateTimeValue.Substring(9, 10);
                            string timeValue = dateTimeValue.Substring(0, 8);
                            transaction.Date = dateValue.PersianToLatin();
                            transaction.Time = TimeSpan.TryParse(timeValue, out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.DocumentNumber = row.Cell("AG").GetValue<string?>();
                            transaction.Debtor = row.Cell("L").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("P").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("I").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("AN").GetValue<string?>();
                            transaction.Description = row.Cell("Y").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Eqtesad Internet Bank = 41
        public async Task<clsResult> ImportEghtesadInternetBankAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 5;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("B").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("C").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Operation = row.Cell("D").GetValue<string?>();
                            transaction.Description = row.Cell("E").GetValue<string?>();
                            transaction.DocumentNumber = row.Cell("J").GetValue<string?>();
                            transaction.Debtor = row.Cell("F").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("G").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("H").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("I").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Eqtesad Internet Bank = 42
        public async Task<clsResult> ImportEghtesadInternetBank2Async(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 5;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;
                            transaction.Description = row.Cell("B").GetValue<string?>();

                            string dateValue = row.Cell("C").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("D").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Operation = row.Cell("J").GetValue<string?>();
                            transaction.DocumentNumber = row.Cell("I").GetValue<string?>();
                            transaction.Debtor = row.Cell("E").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("F").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("G").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("H").GetValue<string?>();
                            transaction.Note = row.Cell("P").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // BankReportType = 5
        public async Task<clsResult> ImportKeshavarziAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    // تنظیم ردیف شروع به 7
                    int startRow = 7;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    // حذف تیتر ها
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction
                            {
                                SellerId = dto.SellerId,
                                BankAccountId = dto.BankAccountId,
                                Row = row.Cell("L").GetValue<int>(),
                                Date = row.Cell("K").GetValue<string>().PersianToLatin(),
                                Time = row.Cell("J").GetValue<TimeSpan>(),
                                DocumentNumber = row.Cell("I").GetValue<string>(),
                                Debtor = row.Cell("F").GetValue<long>(),
                                Creditor = row.Cell("G").GetValue<long>(),
                                Balance = row.Cell("E").GetValue<long>(),
                                Branch = row.Cell("H").GetValue<string>(),

                                Description = row.Cell("A").GetValue<string>(),
                                ReferenceId = row.Cell("B").GetValue<string>(),
                                Operation = row.Cell("D").GetValue<string>(),
                                BatchNumber = batchNum,
                            };


                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }
                await _db.TreBankTransactions.AddRangeAsync(finalList);
                try
                {
                    await _db.SaveChangesAsync();
                    result.Success = true;
                    result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Bank Refah jari = 6
        public async Task<clsResult> ImportRefahJariAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    // تنظیم ردیف شروع به 2
                    int startRow = 3;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    // حذف تیتر ها
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;
                            transaction.Row = row.Cell("M").GetValue<int?>() ?? 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("L").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("K").GetValue<string>().Substring(1, 5);
                            transaction.Time = TimeSpan.TryParse(timeValue, out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.DocumentNumber = row.Cell("H").GetValue<string?>();
                            transaction.Debtor = row.Cell("J").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("I").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("C").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("F").GetValue<string?>();
                            transaction.Description = row.Cell("D").GetValue<string?>();
                            transaction.ReferenceId = row.Cell("E").GetValue<string?>();
                            transaction.Operation = row.Cell("B").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // City Bank = 7
        public async Task<clsResult> ImportCityBankAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 5;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("C").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("D").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.DocumentNumber = row.Cell("I").GetValue<string?>();
                            transaction.Debtor = row.Cell("E").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("F").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("G").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("H").GetValue<string?>();
                            transaction.Description = row.Cell("B").GetValue<string?>();
                            transaction.ReferenceId = row.Cell("J").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Post Bank = 8
        public async Task<clsResult> ImportPostBankAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 5;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("B").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("C").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Operation = row.Cell("D").GetValue<string?>();
                            transaction.Description = row.Cell("E").GetValue<string?>();
                            transaction.DocumentNumber = row.Cell("J").GetValue<string?>();
                            transaction.Debtor = row.Cell("F").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("G").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("H").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("I").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Saderat Sepehr = 9
        public async Task<clsResult> ImportSaderat_SepehrAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 5;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {
                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("B").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("C").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Branch = row.Cell("D").GetValue<string?>(); // شعبه
                            transaction.Operation = row.Cell("E").GetValue<string?>(); // عملیات
                            transaction.DocumentNumber = row.Cell("F").GetValue<string?>(); // شماره سند
                            transaction.Description = row.Cell("G").GetValue<string?>(); // شرح

                            transaction.Debtor = row.Cell("H").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("I").GetValue<long?>() ?? 0; // برداشت

                            transaction.Balance = row.Cell("J").GetValue<long?>() ?? 0; // مانده
                            transaction.Note = row.Cell("K").GetValue<string?>(); // یادداشت
                            transaction.BatchNumber = batchNum;
                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Saderat Sepehr = 91
        public async Task<clsResult> ImportSaderatAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 3;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("B").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("C").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Description = row.Cell("D").GetValue<string?>()
                                + " " + row.Cell("K").GetValue<string?>()
                                + " " + row.Cell("M").GetValue<string?>();

                            transaction.Branch = row.Cell("E").GetValue<string?>(); // شعبه
                            transaction.Operation = row.Cell("S").GetValue<string?>(); // شماره پیگیری
                            transaction.DocumentNumber = row.Cell("G").GetValue<string?>() + " " + row.Cell("Z").GetValue<string?>(); // شماره سند
                            transaction.Debtor = row.Cell("I").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("H").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("J").GetValue<long?>() ?? 0; // مانده
                            transaction.Note = row.Cell("M").GetValue<string?>(); // یادداشت
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }

        // Bank Sepah = 101
        public async Task<clsResult> ImportSepahAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 1;
                    string batchNum = DateTime.Now.ToLong().ToString();

                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateTimeValue = row.Cell("H").GetValue<string>().Trim();
                            string dateValue = dateTimeValue.Substring(0, 10);
                            string timeValue = dateTimeValue.Substring(11, 5);
                            transaction.Date = dateValue.PersianToLatin();
                            transaction.Time = TimeSpan.TryParse(timeValue, out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.DocumentNumber = row.Cell("I").GetValue<string?>();
                            transaction.Debtor = row.Cell("D").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("E").GetValue<long?>() ?? 0; // برداشت
                            //transaction.Balance = row.Cell("I").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("B").GetValue<string?>();
                            transaction.Description = row.Cell("F").GetValue<string?>();
                            transaction.Note = row.Cell("M").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Bank Sepah = 102
        public async Task<clsResult> ImportSepahJariAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 1;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("D").GetValue<string>().Trim();
                            string timeValue = row.Cell("E").GetValue<string>().Trim();
                            transaction.Date = dateValue.PersianToLatin();
                            transaction.Time = TimeSpan.TryParse(timeValue, out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.DocumentNumber = row.Cell("I").GetValue<string?>();
                            transaction.Debtor = row.Cell("N").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("M").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("O").GetValue<long?>() ?? 0;
                            transaction.Branch = row.Cell("B").GetValue<string?>();
                            transaction.Description = row.Cell("L").GetValue<string?>();
                            transaction.Note = row.Cell("Q").GetValue<string?>();
                            transaction.DocumentOrCheckNumber = row.Cell("K").GetValue<string?>();
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue; // خروج از حلقه در صورت بروز خطا
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Saderat Internet Bank = 103
        public async Task<clsResult> ImportSepah_InternetBankAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 3;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {
                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("C").GetValue<string>().Trim();
                            transaction.Date = dateValue.Length > 10 ? dateValue.PersianToLatinRefah() : dateValue.PersianToLatin();

                            string timeValue = row.Cell("D").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Description = row.Cell("G").GetValue<string?>();

                            transaction.Branch = row.Cell("B").GetValue<string?>(); // شعبه
                            transaction.Operation = row.Cell("F").GetValue<string?>(); // شماره پیگیری
                            transaction.DocumentNumber = row.Cell("E").GetValue<string?>() + " " + row.Cell("Z").GetValue<string?>(); // شماره سند

                            transaction.Debtor = row.Cell("I").GetValue<long?>() ?? 0; // واریز
                            transaction.Creditor = row.Cell("H").GetValue<long?>() ?? 0; // برداشت
                            transaction.Balance = row.Cell("J").GetValue<long?>() ?? 0; // مانده
                            transaction.Note = row.Cell("K").GetValue<string?>(); // یادداشت
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }

        // Bank Melli = 201
        public async Task<clsResult> ImportBankMeliAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 2;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("B").GetValue<string>().Trim();
                            transaction.Date = dateValue.PersianToLatin();
                            string timeValue = row.Cell("C").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Branch = row.Cell("D").GetValue<string?>(); // شعبه
                            transaction.Operation = row.Cell("E").GetValue<string?>()?.Trim(); // عملیات
                            transaction.DocumentNumber = row.Cell("G").GetValue<string?>(); // شماره سند
                            transaction.Description = row.Cell("H").GetValue<string?>() + " " + row.Cell("I").GetValue<string?>() + " " + row.Cell("J").GetValue<string?>(); // شرح
                            long amount = row.Cell("F").GetValue<long?>() ?? 0;

                            transaction.Debtor = transaction.Operation == "واریز" ? amount : 0; // واریز
                            transaction.Creditor = transaction.Operation == "برداشت" ? amount : 0;  // برداشت
                            transaction.Balance = row.Cell("K").GetValue<long?>() ?? 0; // مانده
                            transaction.Note = row.Cell("I").GetValue<string?>(); // یادداشت
                            transaction.BatchNumber = batchNum;
                            transaction.BatchNumber = batchNum;
                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        // Bank Melli = 300
        public async Task<clsResult> ImportPasargadAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 2;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("D").GetValue<string>().Trim();
                            transaction.Date = dateValue.PersianToLatin();
                            string timeValue = row.Cell("E").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Debtor = row.Cell("K").GetValue<long?>() ?? 0; //---------------------------------- واریز
                            transaction.Creditor = row.Cell("J").GetValue<long?>() ?? 0; //-------------------------------- برداشت
                            transaction.Balance = row.Cell("L").GetValue<long?>() ?? 0; //--------------------------------- مانده

                            transaction.Branch = row.Cell("B").GetValue<string?>();  //------------------------------------ شعبه
                            transaction.Description = row.Cell("I").GetValue<string?>(); //-------------------------------- شرح
                            transaction.Note = row.Cell("M").GetValue<string?>(); //--------------------------------------- یادداشت
                            transaction.DocumentNumber = row.Cell("F").GetValue<string?>(); //----------------------------- شماره سند
                            transaction.DocumentOrCheckNumber = row.Cell("H").GetValue<string?>(); //---------------------- شماره چک
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        //

        // Bank Melli = 400
        public async Task<clsResult> ImportParsianAsync(BankImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.SellerId == 0)
            {
                result.Message = "لایسنس شناسایی نشد";
                return result;
            }
            if (dto.File == null || dto.File.Length <= 0)
            {
                result.Message = "فایل معتبر نیست";
                return result;
            }
            bool okTask = true;
            var BankTransactionList = new List<TreBankTransaction>();

            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed();

                    int startRow = 5;
                    string batchNum = DateTime.Now.ToLong().ToString();
                    foreach (var row in rows.Skip(startRow - 1))
                    {
                        try
                        {

                            var transaction = new TreBankTransaction();

                            transaction.SellerId = dto.SellerId;
                            transaction.BankAccountId = dto.BankAccountId;

                            string strRow = row.Cell("A").GetValue<string?>();
                            transaction.Row = int.TryParse(strRow, out int rownumber) ? rownumber : 0;
                            if (transaction.Row == 0) continue;

                            string dateValue = row.Cell("C").GetValue<string>().Trim();
                            transaction.Date = dateValue.PersianToLatin();
                            string timeValue = row.Cell("D").GetValue<string?>();
                            transaction.Time = TimeSpan.TryParse(timeValue.Trim(), out var parsedTime) ? parsedTime : TimeSpan.Zero;

                            transaction.Debtor = row.Cell("E").GetValue<long?>() ?? 0; //---------------------------------- واریز
                            transaction.Creditor = row.Cell("F").GetValue<long?>() ?? 0; //-------------------------------- برداشت
                            transaction.Balance = row.Cell("G").GetValue<long?>() ?? 0; //--------------------------------- مانده

                            transaction.Branch = row.Cell("J").GetValue<string?>();  //------------------------------------ شعبه
                            transaction.Description = row.Cell("B").GetValue<string?>(); //-------------------------------- شرح
                            transaction.Note = row.Cell("K").GetValue<string?>(); //--------------------------------------- یادداشت
                            transaction.DocumentNumber = row.Cell("H").GetValue<string?>(); //----------------------------- شماره سند
                            transaction.DocumentOrCheckNumber = row.Cell("I").GetValue<string?>(); //---------------------- شماره چک
                            transaction.BatchNumber = batchNum;

                            BankTransactionList.Add(transaction);
                        }
                        catch (Exception ex)
                        {
                            result.Message += $"خطا در ردیف {row.RowNumber()}: {ex.Message}";
                            okTask = false;
                            continue;
                        }
                    }
                }
            }
            if (okTask)
            {
                var existingData = await _db.TreBankTransactions.AsNoTracking().Where(n => n.SellerId == dto.SellerId && n.BankAccountId == dto.BankAccountId).ToListAsync();

                List<TreBankTransaction> finalList = new List<TreBankTransaction>();
                foreach (var x in BankTransactionList)
                {
                    if (!existingData.Where(n => n.Date == x.Date && n.DocumentNumber == x.DocumentNumber).Any())
                    { finalList.Add(x); }
                }


                try
                {
                    if (finalList.Count > 0)
                    {
                        await _db.TreBankTransactions.AddRangeAsync(finalList);
                        await _db.SaveChangesAsync();
                        result.Message = $"تعداد {finalList.Count} از {BankTransactionList.Count} رکورد موجود در فایل بارگذاری شد";
                    }
                    else
                    {
                        result.Message = $"رکورد جدیدی برای افزودن یافت نشد";
                    }

                    result.Success = true;

                }
                catch (Exception)
                {
                    result.Message = "خطایی در ثبت اطلاعات گزارش بانک رخ داده است فایل را بررسی کنید مجدد تلاش کنید";
                }
            }
            return result;
        }
        //
    }
}
