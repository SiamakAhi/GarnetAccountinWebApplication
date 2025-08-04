using ClosedXML.Excel;
using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Dto.Moadian;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccAsistantsService : IAccAsistantsService
    {
        private readonly AppDbContext _db;
        public readonly IAccOperationService _accService;
        private readonly IAccCodingService _coding;

        public AccAsistantsService(AppDbContext appDbContext, IAccOperationService accService, IAccCodingService codingService)
        {
            _db = appDbContext;
            _accService = accService;
            _coding = codingService;
        }

        public SelectList SelectList_MoadianInvoiceStatuses()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>
            {
                new SelectListItem{Value="1", Text="تایید شده"},
                new SelectListItem{Value="2", Text="تایید سیستمی"},
                new SelectListItem{Value="3", Text="در انتظار واکنش"},
                new SelectListItem{Value="4", Text="عدم نیاز به واکنش"},
                new SelectListItem{Value="5", Text="باطل شده"},
                new SelectListItem{Value="6", Text="رد شده"},
                new SelectListItem{Value="7", Text="عدم امکان واکنش"}
            };

            return new SelectList(selectListItems, "Value", "Text");
        }
        public IQueryable<Acc_MoadianReport> GetMoadianReport(MoadianReportFilterDto filter)
        {
            var query = _db.Acc_ModianReports
                .Where(n => n.SellerId == filter.SellerId && n.ReportType == filter.Transactiontype)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.strFromDate))
            {
                DateTime startDate = filter.strFromDate.PersianToLatin();
                query = query.Where(n => n.IssueDate >= startDate);
            }
            if (!string.IsNullOrEmpty(filter.strToDate))
            {
                DateTime EndDate = filter.strToDate.PersianToLatin();
                query = query.Where(n => n.IssueDate <= EndDate);
            }
            if (!string.IsNullOrEmpty(filter.InvoiceType))
                query = query.Where(n => n.InvoiceType == filter.InvoiceType);

            if (filter.BatchNumber.HasValue)
                query = query.Where(n => n.Batchnuber == filter.BatchNumber);

            if (filter.InvoiceSubject != null && filter.InvoiceSubject.Length > 0)
                query = query.Where(n => filter.InvoiceSubject.Contains(n.InvoiceSubjectCode));

            if (filter.InvoiceStatuse != null && filter.InvoiceStatuse.Length > 0)
                query = query.Where(n => filter.InvoiceStatuse.Contains(n.InvoiceStatusCode));

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.BuyerName.Contains(filter.Description) || n.BuyerTradeName.Contains(filter.Description));

            if (filter.Ischecked.HasValue)
                query = query.Where(n => n.IsChecked == filter.Ischecked);

            return query;
        }
        public async Task<clsResult> InsertMoadianReportAsync(long sellerId, List<Acc_MoadianReport> report)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطلاعاتی برای درج در دیتابیس یافت نشد";
            result.ShowMessage = true;

            if (report.Count == 0) return result;

            foreach (var x in report)
            {

                var Item = new Acc_MoadianReport();
                var existItem = await _db.Acc_ModianReports.Where(n => n.TaxNumber == x.TaxNumber).FirstOrDefaultAsync();
                if (existItem != null)
                    Item = existItem;

                Item.SellerId = sellerId;
                Item.InvoiceType = x.InvoiceType;
                Item.InvoicePattern = x.InvoicePattern;
                Item.InvoiceSubject = x.InvoiceSubject;
                Item.TaxNumber = x.TaxNumber;
                Item.TotalInvoiceAmount = x.TotalInvoiceAmount;
                Item.VAT = x.VAT;
                Item.InvoiceStatus = x.InvoiceStatus;
                Item.IssueDate = x.IssueDate;
                Item.FolderInsertDate = x.FolderInsertDate;
                Item.BuyerIdentity = x.BuyerIdentity;
                Item.BuyerEconomicNumber = x.BuyerEconomicNumber;
                Item.SellerBranch = x.SellerBranch;
                Item.BuyerName = x.BuyerName;
                Item.BuyerTradeName = x.BuyerTradeName;
                Item.BuyerPersonType = x.BuyerPersonType;
                Item.SellerContractNumber = x.SellerContractNumber;
                Item.SubscriptionNumber = x.SubscriptionNumber;
                Item.FlightType = x.FlightType;
                Item.ContractorContractNumber = x.ContractorContractNumber;
                Item.SettlementMethod = x.SettlementMethod;
                Item.YearAndPeriod = x.YearAndPeriod;
                Item.LimitStatus = x.LimitStatus;
                Item.AccountingStatus = x.AccountingStatus;
                Item.InvoiceAmountWithoutVAT = x.InvoiceAmountWithoutVAT;
                Item.ReferringInvoiceIssueDate = x.ReferringInvoiceIssueDate;
                Item.NonAccountingStatusDate = x.NonAccountingStatusDate;
                Item.ReferenceInvoiceTaxNumber = x.ReferenceInvoiceTaxNumber;
                Item.InvoiceSettlementBalance = x.InvoiceSettlementBalance;
                if (existItem != null)
                    _db.Acc_ModianReports.Update(Item);
                else
                    _db.Acc_ModianReports.Add(Item);
            }

            await _db.SaveChangesAsync();
            result.Success = true;
            result.Message = "اطلاعات با موفقیت ذخیره شد";
            return result;
        }
        public async Task<clsResult> insertMoadianReportFromExcelAsync(MoadianImporterDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            var reportList = new List<Acc_MoadianReport>();

            using (var stream = new MemoryStream())
            {

                await dto.File.CopyToAsync(stream);
                var FinancialPeriod = await _db.Acc_FinancialPeriods.FirstOrDefaultAsync(n => n.Id == dto.PeriodId);
                if (FinancialPeriod == null)
                {
                    result.Success = false;
                    result.Message = "سال مالی فعال شناسایی نشد";
                    return result;
                }

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.First();
                    var rows = worksheet.RowsUsed().Skip(1);
                    long batch = DateTime.Now.ToLong();
                    int rowNumber = 1;
                    foreach (var row in rows)
                    {

                        var report = new Acc_MoadianReport();
                        report.ReportType = dto.ReportType;
                        report.InvoiceType = row.Cell(1).GetString();
                        report.InvoicePattern = row.Cell(2).GetString();
                        report.InvoiceSubject = row.Cell(3).GetString();
                        report.InvoiceSubjectCode = report.InvoiceSubject.Moadian_IncoiceSubjectToCode() ?? 0;

                        report.TaxNumber = row.Cell(5).GetString();
                        bool checkDuplacate = await _db.Acc_ModianReports
                            .Where(n => n.SellerId == dto.SellerId && n.ReportType == dto.ReportType && n.TaxNumber == report.TaxNumber).AnyAsync();
                        if (checkDuplacate)
                            continue;
                        bool checkDuplacateInTheBatch = reportList.Where(n => n.TaxNumber == report.TaxNumber).Any();
                        if (checkDuplacateInTheBatch)
                            continue;

                        report.TotalInvoiceAmount = row.Cell(6).GetValue<long?>() ?? 0;
                        if (report.TotalInvoiceAmount == 0)
                            continue;
                        report.VAT = row.Cell(7).GetValue<long?>() ?? 0;
                        report.InvoiceStatus = row.Cell(8).GetString();
                        report.InvoiceStatusCode = report.InvoiceStatus.Moadian_IncoiceStatusToCode() ?? 0;
                        string strDate = row.Cell(9).GetString().Substring(0, 10);
                        report.IssueDate = strDate.mdToMiladiDate();
                        if (report.IssueDate < FinancialPeriod.StartDate || report.IssueDate > FinancialPeriod.EndDate)
                        {
                            result.Message = $"تاریخ صورتحساب در ردیف  {rowNumber} خارج از بازه دوره مالی {FinancialPeriod.Name} است.";
                            return result;
                        }
                        string strFolderInsertDate = row.Cell(10).GetString().Substring(0, 10);
                        report.FolderInsertDate = strFolderInsertDate.mdToMiladiDate();
                        report.BuyerIdentity = row.Cell(11).GetString();
                        report.BuyerEconomicNumber = row.Cell(12).GetString();
                        report.SellerBranch = row.Cell(13).GetString();
                        report.BuyerName = row.Cell(14).GetString();
                        report.BuyerTradeName = row.Cell(15).GetString();
                        report.BuyerPersonType = row.Cell(16).GetString();
                        report.SellerContractNumber = row.Cell(17).GetString();
                        report.SubscriptionNumber = row.Cell(18).GetString();
                        report.FlightType = row.Cell(19).GetString();
                        report.ContractorContractNumber = row.Cell(20).GetString();
                        report.SettlementMethod = row.Cell(21).GetString();
                        report.YearAndPeriod = row.Cell(22).GetString();
                        report.LimitStatus = row.Cell(25).GetString();
                        report.AccountingStatus = row.Cell(26).GetString();
                        report.InvoiceAmountWithoutVAT = report.TotalInvoiceAmount - report.VAT;
                        report.ReferringInvoiceIssueDate = null;
                        report.NonAccountingStatusDate = null;
                        report.ReferenceInvoiceTaxNumber = row.Cell(30).GetString();
                        report.InvoiceSettlementBalance = row.Cell(32).GetValue<long?>() ?? 0;

                        report.IsSaleInvoice = dto.ReportType > 1 ? true : false;
                        report.Batchnuber = batch;
                        report.SellerId = dto.SellerId;

                        rowNumber++;
                        reportList.Add(report);
                    }
                }
            }

            if (reportList.Count > 0)
            {
                _db.Acc_ModianReports.AddRange(reportList);
                try
                {
                    await _db.SaveChangesAsync();
                    result.Success = true;
                    result.Message = $"اطلاعات با موفقیت انجام شد. </br> تعداد {reportList.Count} ردیف در بانک اطلاعاتی ذخیره شد.";
                }
                catch (Exception x)
                {
                    result.Success = false;
                    result.Message = $"در عملیات قبت اطلاعات خطایی رخ داده است </br> {x.Message}";
                }
            }
            else
            {
                result.Success = false;
                result.Message = "ردیف تایید شده ای برای ثبت مشاهده نشد.";
            }

            return result;
        }

        public async Task<List<Acc_MoadianReport>> ReadMoadianReportFromExcelAsync(IFormFile file)
        {
            var reportList = new List<Acc_MoadianReport>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.First();
                    var rows = worksheet.RowsUsed().Skip(1); // Skip header row

                    foreach (var row in rows)
                    {
                        var report = new Acc_MoadianReport();

                        report.InvoiceType = row.Cell(1).GetString();
                        report.InvoicePattern = row.Cell(2).GetString();
                        report.InvoiceSubject = row.Cell(3).GetString();
                        if (report.InvoiceSubject == "ابطالی")
                            continue;
                        report.TaxNumber = row.Cell(5).GetString();
                        report.TotalInvoiceAmount = row.Cell(6).GetValue<long?>() ?? 0;
                        if (report.TotalInvoiceAmount == 0)
                            continue;
                        report.VAT = row.Cell(7).GetValue<long?>() ?? 0;
                        report.InvoiceStatus = row.Cell(8).GetString();
                        if (report.InvoiceType.Trim() == "اول" && report.InvoiceStatus.Trim() != "تایید شده" && report.InvoiceStatus.Trim() != "تایید سیستمی")
                            continue;
                        if (report.InvoiceType.Trim() == "دوم" && report.InvoiceStatus.Trim() != "عدم نیاز به واکنش")
                            continue;
                        string strDate = row.Cell(9).GetString().Substring(0, 10);
                        report.IssueDate = strDate.mdToMiladiDate();
                        string strFolderInsertDate = row.Cell(10).GetString().Substring(0, 10);
                        report.FolderInsertDate = strFolderInsertDate.mdToMiladiDate();
                        report.BuyerIdentity = row.Cell(11).GetString();
                        report.BuyerEconomicNumber = row.Cell(12).GetString();
                        report.SellerBranch = row.Cell(13).GetString();
                        report.BuyerName = row.Cell(14).GetString();
                        report.BuyerTradeName = row.Cell(15).GetString();
                        report.BuyerPersonType = row.Cell(16).GetString();
                        report.SellerContractNumber = row.Cell(17).GetString();
                        report.SubscriptionNumber = row.Cell(18).GetString();
                        report.FlightType = row.Cell(19).GetString();
                        report.ContractorContractNumber = row.Cell(20).GetString();
                        report.SettlementMethod = row.Cell(21).GetString();
                        report.YearAndPeriod = row.Cell(22).GetString();
                        report.LimitStatus = row.Cell(25).GetString();
                        report.AccountingStatus = row.Cell(26).GetString();
                        report.InvoiceAmountWithoutVAT = report.TotalInvoiceAmount - report.VAT;
                        report.ReferringInvoiceIssueDate = null;
                        report.NonAccountingStatusDate = null;
                        report.ReferenceInvoiceTaxNumber = row.Cell(30).GetString();
                        report.InvoiceSettlementBalance = row.Cell(32).GetValue<long?>() ?? 0;

                        reportList.Add(report);
                    }
                }
            }

            return reportList;
        }

        public async Task<List<GarnetMoadianOutput>> ReadGarnetMoadianFromExcelAsync(IFormFile file)
        {
            var reportList = new List<GarnetMoadianOutput>();

            using (var stream = new MemoryStream())
            {
                // Copy the uploaded file to a memory stream
                await file.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    // Get the first worksheet
                    var worksheet = workbook.Worksheets.First();
                    var rows = worksheet.RowsUsed().Skip(1); // Skip header row

                    foreach (var row in rows)
                    {
                        var report = new GarnetMoadianOutput();

                        // Map Excel columns to GarnetMoadianOutput properties
                        report.Identifier = row.Cell(1).GetString();
                        report.TaxCode = row.Cell(2).GetString();
                        string strDate = row.Cell(3).GetString()?.Substring(0, 10);
                        report.Date = !string.IsNullOrEmpty(strDate) ? strDate.mdToMiladiDate() : (DateTime?)null;
                        report.InvoiceNumber = row.Cell(4).GetString();
                        report.InvoicePattern = row.Cell(5).GetString();
                        report.InvoiceSubject = row.Cell(6).GetString();
                        report.ReferenceInvoiceTaxCode = row.Cell(7).GetString();
                        report.InvoiceType = row.Cell(8).GetString();
                        report.BuyerName = row.Cell(9).GetString();
                        report.Description = row.Cell(10).GetString();
                        report.CashPayment = row.Cell(11).GetValue<long?>();
                        report.AmountBeforeDiscount = row.Cell(12).GetValue<long?>();
                        report.TotalDiscount = row.Cell(13).GetValue<long?>();
                        report.AmountAfterDiscount = row.Cell(14).GetValue<long?>();
                        report.TotalTax = row.Cell(15).GetValue<long?>();
                        report.OtherCharges = row.Cell(16).GetValue<long?>();
                        report.TotalAmount = row.Cell(17).GetValue<long?>();
                        report.BuyerPostalCode = row.Cell(18).GetString();
                        report.BuyerEconomicCode = row.Cell(19).GetString();
                        report.BuyerNationalCode = row.Cell(20).GetString();
                        report.Status = row.Cell(21).GetString();
                        report.SettlementType = row.Cell(22).GetString();

                        // Add the mapped object to the list
                        reportList.Add(report);
                    }
                }
            }

            return reportList;
        }

        public async Task<BulkDocDto> PreparingToCreateSaleMoadianDocAsync(List<Acc_MoadianReport> report, bool appendToDoc, long sellerId, int periodId, string currentUser)
        {
            BulkDocDto docs = new BulkDocDto();

            var AccSetting = await _db.Acc_Settings.Where(n => n.SellerId == sellerId).FirstOrDefaultAsync();
            if (AccSetting == null)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "ابتدا از منوی تنظیمات حسابداری، سرفصل های مربوط به فروش را مشخص کنید.";
                return docs;
            }

            int saleMoeinId = AccSetting.saleMoeinId ?? 0;
            int DebtorMoeinId = AccSetting.salePartyMoeinId ?? 0;
            int SaleVatMoeinId = AccSetting.SaleVatMoeinId ?? 0;
            if (saleMoeinId == 0 || DebtorMoeinId == 0)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "ابتدا از منوی تنظیمات حسابداری، سرفصل های مربوط به فروش را تنظیم کنید.";
                return docs;
            }
            if (report.Where(n => n.VAT > 0).Any() && SaleVatMoeinId == 0)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "ابتدا از منوی تنظیمات حسابداری، سرفصل مربوط به ارزش افزوده فروش را تنظیم کنید.";
                return docs;
            }

            var DebtorMoein = await _db.Acc_Coding_Moeins.SingleOrDefaultAsync(n => n.Id == DebtorMoeinId);
            if (DebtorMoein == null)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "حساب معین بدهکاران تجاری (دریافتنی های تجاری) یافت نشد";
                return docs;
            }
            var saleMoein = await _db.Acc_Coding_Moeins.SingleOrDefaultAsync(n => n.Id == saleMoeinId);
            if (saleMoein == null)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "حساب معین فروش/درآمد یافت نشد";
                return docs;
            }
            var VatMoein = await _db.Acc_Coding_Moeins.SingleOrDefaultAsync(n => n.Id == SaleVatMoeinId);
            if (VatMoein == null)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "حساب معین ارزش افزوده فروش یافت نشد";
                return docs;
            }

            int atf = await _accService.DocAutoNumberGeneratorAsync(sellerId, periodId);
            int docNo = await _accService.DocNumberGeneratorAsync(sellerId, periodId);
            foreach (var x in report.OrderBy(d => d.IssueDate).ToList())
            {

                long? tafsilId = null;
                string? tafsilName = null;
                if (!string.IsNullOrEmpty(x.BuyerName))
                {
                    tafsilId = await _coding.CheckAddTafsilAsync(x.BuyerName, sellerId);
                    tafsilName = x.BuyerName;
                }
                Acc_Document header = new Acc_Document();
                int row = 1;
                if (appendToDoc)
                {
                    var DocInDate = await _db.Acc_Documents.Include(b => b.DocArticles)
                        .Where(n =>
                    n.SellerId == sellerId
                    && n.PeriodId == periodId
                    && !n.IsDeleted
                    && n.DocDate.Date == x.IssueDate.Date)
                        .FirstOrDefaultAsync();

                    if (DocInDate != null)
                    {
                        header = DocInDate;
                        row = DocInDate.DocArticles.Count + 1;
                    }
                    else
                    {
                        var newDoc = docs.Headers.Where(n => n.DocDate == x.IssueDate).FirstOrDefault();
                        if (newDoc != null)
                        {
                            header = newDoc;
                            row = docs.Articles.Where(n => n.DocId == newDoc.Id).Count() + 1;
                        }
                        else
                        {
                            header.Id = Guid.NewGuid();
                            header.SellerId = sellerId;
                            header.PeriodId = periodId;
                            header.AtfNumber = atf;
                            header.AutoDocNumber = atf;
                            header.DocNumber = docNo;
                            header.DocDate = x.IssueDate;
                            header.Description = "بابت منظور نمودن مدارک مثبته ضمیمه سند حسابداری";
                            header.IsDeleted = false;
                            header.CreatorUserName = currentUser;
                            header.CreateDate = DateTime.Now;
                            atf++;
                            docNo++;

                            docs.Headers.Add(header);
                        }

                    }
                }
                else
                {
                    header.Id = Guid.NewGuid();
                    header.SellerId = sellerId;
                    header.PeriodId = periodId;
                    header.AtfNumber = atf;
                    header.AutoDocNumber = atf;
                    header.DocNumber = docNo;
                    header.DocDate = x.IssueDate;
                    header.Description = "بابت منظور نمودن مدارک مثبته ضمیمه سند حسابداری";
                    header.IsDeleted = false;
                    header.CreatorUserName = currentUser;
                    header.CreateDate = DateTime.Now;

                    atf++;
                    docNo++;

                    docs.Headers.Add(header);
                }


                //Articles

                //بدهکاران تجاری
                Acc_Article debtor = new Acc_Article();
                debtor.Id = Guid.NewGuid();
                debtor.SellerId = sellerId;
                debtor.PeriodId = periodId;
                debtor.DocId = header.Id;
                debtor.KolId = DebtorMoein.KolId;
                debtor.MoeinId = DebtorMoeinId;
                debtor.Amount = x.TotalInvoiceAmount;
                debtor.Bed = x.TotalInvoiceAmount;
                debtor.Bes = 0;
                debtor.Tafsil4Id = tafsilId;
                debtor.Tafsil4Name = tafsilName;
                debtor.ArchiveCode = x.TaxNumber;
                debtor.RowNumber = row;
                debtor.IsDeleted = false;
                debtor.CreatorUserName = currentUser;
                debtor.CreateDate = DateTime.Now;
                debtor.Comment = $"بابت صورتحساب فروش شماره {x.TaxNumber}";
                debtor.AccountantRemark = DateTime.Now.LatinToPersian();
                row++;
                docs.Articles.Add(debtor);

                // فروش
                Acc_Article sale = new Acc_Article();
                sale.Id = Guid.NewGuid();
                sale.SellerId = sellerId;
                sale.PeriodId = periodId;
                sale.DocId = header.Id;
                sale.KolId = saleMoein.KolId;
                sale.MoeinId = saleMoein.Id;
                sale.Amount = x.InvoiceAmountWithoutVAT;
                sale.Bed = 0;
                sale.Bes = x.InvoiceAmountWithoutVAT;
                sale.Tafsil4Id = tafsilId;
                sale.Tafsil4Name = tafsilName;
                sale.ArchiveCode = x.TaxNumber;
                sale.RowNumber = row;
                sale.IsDeleted = false;
                sale.CreatorUserName = currentUser;
                sale.CreateDate = DateTime.Now;
                sale.Comment = $"بابت صورتحساب فروش تاریخ : {header.DocDate.LatinToPersian()} خریدار : {x.BuyerName}";
                sale.AccountantRemark = DateTime.Now.LatinToPersian();
                row++;
                docs.Articles.Add(sale);

                if (x.VAT > 0)
                {
                    // ارزش افزوده فروش 
                    Acc_Article Vat = new Acc_Article();
                    Vat.Id = Guid.NewGuid();
                    Vat.SellerId = sellerId;
                    Vat.PeriodId = periodId;
                    Vat.DocId = header.Id;
                    Vat.KolId = VatMoein.KolId;
                    Vat.MoeinId = VatMoein.Id;
                    Vat.Amount = x.VAT;
                    Vat.Bed = 0;
                    Vat.Bes = x.VAT;
                    Vat.ArchiveCode = x.TaxNumber;
                    Vat.RowNumber = row;
                    Vat.ArchiveCode = x.TaxNumber;
                    Vat.IsDeleted = false;
                    Vat.CreatorUserName = currentUser;
                    Vat.CreateDate = DateTime.Now;
                    Vat.AccountantRemark = DateTime.Now.LatinToPersian();
                    if (AccSetting.SetTafsilForSaleVat)
                    {
                        Vat.Tafsil4Name = tafsilName;
                        Vat.Tafsil4Id = tafsilId;
                    }
                    row++;
                    docs.Articles.Add(Vat);
                }
            }

            docs.Success = true;
            return docs;
        }

        public async Task<BulkDocDto> PreparingToCreateBuyMoadianDocAsync(List<Acc_MoadianReport> report, bool appendToDoc, long sellerId, int periodId, string currentUser)
        {
            BulkDocDto docs = new BulkDocDto();

            var AccSetting = await _db.Acc_Settings.Where(n => n.SellerId == sellerId).FirstOrDefaultAsync();
            if (AccSetting == null)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "ابتدا از منوی تنظیمات حسابداری، سرفصل های مربوط به خرید را مشخص کنید.";
                return docs;
            }

            int buyMoeinId = AccSetting.BuyMoeinId ?? 0;
            int BesMoeinId = AccSetting.BuyPartyMoeinId ?? 0;
            int buyVatMoeinId = AccSetting.BuyVatMoeinId ?? 0;
            if (buyMoeinId == 0 || BesMoeinId == 0)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "ابتدا از منوی تنظیمات حسابداری، سرفصل های مربوط به خرید را تنظیم کنید.";
                return docs;
            }
            if (report.Where(n => n.VAT > 0).Any() && buyVatMoeinId == 0)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "ابتدا از منوی تنظیمات حسابداری، سرفصل مربوط به ارزش افزوده خرید را تنظیم کنید.";
                return docs;
            }

            var BesMoein = await _db.Acc_Coding_Moeins.SingleOrDefaultAsync(n => n.Id == BesMoeinId);
            if (BesMoein == null)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "حساب معین بستانکاران تجاری (پرداختنی های تجاری) یافت نشد";
                return docs;
            }
            var buyMoein = await _db.Acc_Coding_Moeins.SingleOrDefaultAsync(n => n.Id == buyMoeinId);
            if (buyMoein == null)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "حساب معین خرید یافت نشد";
                return docs;
            }
            var VatMoein = await _db.Acc_Coding_Moeins.SingleOrDefaultAsync(n => n.Id == buyVatMoeinId);
            if (VatMoein == null)
            {
                docs.Headers = new List<Acc_Document>();
                docs.Articles = new List<Acc_Article>();
                docs.Message = "حساب معین ارزش افزوده خرید یافت نشد";
                return docs;
            }

            int atf = await _accService.DocAutoNumberGeneratorAsync(sellerId, periodId);
            int docNo = await _accService.DocNumberGeneratorAsync(sellerId, periodId);
            foreach (var x in report.OrderBy(d => d.IssueDate).ToList())
            {

                long? tafsilId = null;
                string? tafsilName = null;
                if (!string.IsNullOrEmpty(x.BuyerName))
                {
                    tafsilId = await _coding.CheckAddTafsilAsync(x.BuyerName, sellerId);
                    tafsilName = x.BuyerName;
                }
                Acc_Document header = new Acc_Document();
                int row = 1; // شماره ردیف ارتیکل
                if (appendToDoc)
                {
                    var DocInDate = await _db.Acc_Documents.Include(b => b.DocArticles)
                        .Where(n =>
                    n.SellerId == sellerId
                    && n.PeriodId == periodId
                    && !n.IsDeleted
                    && n.DocDate.Date == x.IssueDate.Date)
                        .FirstOrDefaultAsync();

                    if (DocInDate != null)
                    {
                        header = DocInDate;
                        row = DocInDate.DocArticles.Count + 1;
                    }
                    else
                    {
                        var newDoc = docs.Headers.Where(n => n.DocDate == x.IssueDate).FirstOrDefault();
                        if (newDoc != null)
                        {
                            header = newDoc;
                            row = docs.Articles.Where(n => n.DocId == newDoc.Id).Count() + 1;
                        }
                        else
                        {
                            header.Id = Guid.NewGuid();
                            header.SellerId = sellerId;
                            header.PeriodId = periodId;
                            header.AtfNumber = atf;
                            header.AutoDocNumber = atf;
                            header.DocNumber = docNo;
                            header.DocDate = x.IssueDate;
                            header.Description = "بابت منظور نمودن مدارک مثبته ضمیمه سند حسابداری";
                            header.IsDeleted = false;
                            header.CreatorUserName = currentUser;
                            header.CreateDate = DateTime.Now;
                            atf++;
                            docNo++;

                            docs.Headers.Add(header);
                        }

                    }
                }
                else
                {
                    header.Id = Guid.NewGuid();
                    header.SellerId = sellerId;
                    header.PeriodId = periodId;
                    header.AtfNumber = atf;
                    header.AutoDocNumber = atf;
                    header.DocNumber = docNo;
                    header.DocDate = x.IssueDate;
                    header.Description = "بابت منظور نمودن مدارک مثبته ضمیمه سند حسابداری";
                    header.IsDeleted = false;
                    header.CreatorUserName = currentUser;
                    header.CreateDate = DateTime.Now;

                    atf++;
                    docNo++;

                    docs.Headers.Add(header);
                }


                //Articles
                // خرید
                Acc_Article buy = new Acc_Article();
                buy.Id = Guid.NewGuid();
                buy.SellerId = sellerId;
                buy.PeriodId = periodId;
                buy.DocId = header.Id;
                buy.KolId = buyMoein.KolId;
                buy.MoeinId = buyMoein.Id;
                buy.Amount = x.InvoiceAmountWithoutVAT;
                buy.Bed = x.InvoiceAmountWithoutVAT;
                buy.Bes = 0;
                buy.Tafsil4Id = tafsilId;
                buy.Tafsil4Name = tafsilName;
                buy.ArchiveCode = x.TaxNumber;
                buy.RowNumber = row;
                buy.IsDeleted = false;
                buy.CreatorUserName = currentUser;
                buy.CreateDate = DateTime.Now;
                buy.Comment = $"بابت صورتحساب خرید تاریخ : {header.DocDate.LatinToPersian()} فروشنده : {x.BuyerName}";
                buy.AccountantRemark = DateTime.Now.LatinToPersian();
                row++;
                docs.Articles.Add(buy);

                if (x.VAT > 0)
                {
                    // ارزش افزوده خرید 
                    Acc_Article Vat = new Acc_Article();
                    Vat.Id = Guid.NewGuid();
                    Vat.SellerId = sellerId;
                    Vat.PeriodId = periodId;
                    Vat.DocId = header.Id;
                    Vat.KolId = VatMoein.KolId;
                    Vat.MoeinId = VatMoein.Id;
                    Vat.Amount = x.VAT;
                    Vat.Bed = x.VAT;
                    Vat.Bes = 0;
                    Vat.ArchiveCode = x.TaxNumber;
                    Vat.RowNumber = row;
                    Vat.ArchiveCode = x.TaxNumber;
                    Vat.IsDeleted = false;
                    Vat.CreatorUserName = currentUser;
                    Vat.CreateDate = DateTime.Now;
                    Vat.AccountantRemark = DateTime.Now.LatinToPersian();
                    if (AccSetting.SetTafsilForBuyVat)
                    {
                        Vat.Tafsil4Name = tafsilName;
                        Vat.Tafsil4Id = tafsilId;
                    }
                    row++;
                    docs.Articles.Add(Vat);
                }

                //بستانکاران تجاری
                Acc_Article bestankar = new Acc_Article();
                bestankar.Id = Guid.NewGuid();
                bestankar.SellerId = sellerId;
                bestankar.PeriodId = periodId;
                bestankar.DocId = header.Id;
                bestankar.KolId = BesMoein.KolId;
                bestankar.MoeinId = BesMoein.Id;
                bestankar.Amount = x.TotalInvoiceAmount;
                bestankar.Bed = 0;
                bestankar.Bes = x.TotalInvoiceAmount;
                bestankar.Tafsil4Id = tafsilId;
                bestankar.Tafsil4Name = tafsilName;
                bestankar.ArchiveCode = x.TaxNumber;
                bestankar.RowNumber = row;
                bestankar.IsDeleted = false;
                bestankar.CreatorUserName = currentUser;
                bestankar.CreateDate = DateTime.Now;
                bestankar.Comment = $"بابت صورتحساب خرید شماره {x.TaxNumber} تاریخ : {header.DocDate.LatinToPersian()}";
                bestankar.AccountantRemark = DateTime.Now.LatinToPersian();
                row++;
                docs.Articles.Add(bestankar);



            }
            docs.Success = true;
            return docs;
        }

        public async Task<BulkDocDto> PreparingToCreateGarnetMoadianDocAsync(List<GarnetMoadianOutput> report, bool isSale, long sellerId, int periodId, string currentUser)
        {
            BulkDocDto docs = new BulkDocDto();

            //بدهکاران تجاری  2060001
            //فروش 6010001
            //6030001 نخفیفات نقدی فروش 
            //بدهی تحقق یافته ( ارزش افزوده فروش )5030006
            //بدهی تحقق نیافته ( ارزش افزوده فروش ) 5030007


            var saleMoein = await _db.Acc_Coding_Moeins.AsNoTracking().Where(n => n.SellerId == sellerId && n.MoeinCode == "6010001").FirstOrDefaultAsync();
            var DebtorMoein = await _db.Acc_Coding_Moeins.AsNoTracking().Where(n => n.SellerId == sellerId && n.MoeinCode == "2060001").FirstOrDefaultAsync();
            var VatNaghdiMoein = await _db.Acc_Coding_Moeins.AsNoTracking().Where(n => n.SellerId == sellerId && n.MoeinCode == "5030006").FirstOrDefaultAsync();
            var VatNesiyehiMoein = await _db.Acc_Coding_Moeins.AsNoTracking().Where(n => n.SellerId == sellerId && n.MoeinCode == "5030007").FirstOrDefaultAsync();
            var SaleDiscountMoein = await _db.Acc_Coding_Moeins.AsNoTracking().Where(n => n.SellerId == sellerId && n.MoeinCode == "6030001").FirstOrDefaultAsync();

            int atf = await _accService.DocAutoNumberGeneratorAsync(sellerId, periodId);
            int docNo = await _accService.DocNumberGeneratorAsync(sellerId, periodId);
            foreach (var x in report.OrderBy(d => d.Date).ToList())
            {

                long? tafsilId = null;
                string? tafsilName = null;
                if (!string.IsNullOrEmpty(x.BuyerName))
                {
                    tafsilId = await _coding.CheckAddTafsilAsync(x.BuyerName, sellerId);
                    tafsilName = x.BuyerName;
                }

                Acc_Document header = new Acc_Document();
                header.Id = Guid.NewGuid();
                header.SellerId = sellerId;
                header.PeriodId = periodId;
                header.AtfNumber = atf;
                header.AutoDocNumber = atf;
                header.DocNumber = docNo;
                header.DocDate = x.Date.Value;
                header.Description = "بابت منظور نمودن مدارک مثبته ضمیمه سند حسابداری";
                header.IsDeleted = false;
                header.CreatorUserName = currentUser;
                header.CreateDate = DateTime.Now;

                atf++;
                docNo++;
                docs.Headers.Add(header);

                //Articles
                int row = 1;
                //بدهکاران تجاری
                Acc_Article debtor = new Acc_Article();
                debtor.Id = Guid.NewGuid();
                debtor.DocId = header.Id;
                debtor.KolId = DebtorMoein.KolId;
                debtor.MoeinId = DebtorMoein.Id;
                debtor.Amount = x.TotalAmount.Value;
                debtor.Bed = x.TotalAmount.Value;
                debtor.Bes = 0;
                debtor.Tafsil4Id = tafsilId;
                debtor.Tafsil4Name = tafsilName;
                debtor.ArchiveCode = x.TaxCode;
                debtor.RowNumber = row;
                debtor.IsDeleted = false;
                debtor.CreatorUserName = currentUser;
                debtor.CreateDate = DateTime.Now;
                row++;
                docs.Articles.Add(debtor);

                // فروش
                Acc_Article sale = new Acc_Article();
                sale.Id = Guid.NewGuid();
                sale.DocId = header.Id;
                sale.KolId = saleMoein.KolId;
                sale.MoeinId = saleMoein.Id;
                sale.Amount = x.AmountBeforeDiscount.Value;
                sale.Bed = 0;
                sale.Bes = x.AmountBeforeDiscount.Value;
                sale.Tafsil4Id = tafsilId;
                sale.Tafsil4Name = tafsilName;
                sale.ArchiveCode = x.TaxCode;
                sale.RowNumber = row;
                sale.IsDeleted = false;
                sale.CreatorUserName = currentUser;
                sale.CreateDate = DateTime.Now;
                row++;
                docs.Articles.Add(sale);

                if (x.TotalDiscount > 0)
                {
                    // تخفیفات نقدی فروش 
                    Acc_Article saleDiscount = new Acc_Article();
                    saleDiscount.Id = Guid.NewGuid();
                    saleDiscount.DocId = header.Id;
                    saleDiscount.KolId = VatNaghdiMoein.KolId;
                    saleDiscount.MoeinId = VatNaghdiMoein.Id;
                    saleDiscount.Amount = x.TotalDiscount.Value;
                    saleDiscount.Bed = x.TotalDiscount.Value;
                    saleDiscount.Bes = 0;
                    saleDiscount.ArchiveCode = x.TaxCode;
                    saleDiscount.RowNumber = row;
                    saleDiscount.IsDeleted = false;
                    saleDiscount.CreatorUserName = currentUser;
                    saleDiscount.CreateDate = DateTime.Now;
                    row++;
                    docs.Articles.Add(saleDiscount);
                }

                if (x.SettlementType == "نقدی")
                {
                    // اعتبار تحقق یافته 
                    Acc_Article Vat = new Acc_Article();
                    Vat.Id = Guid.NewGuid();
                    Vat.DocId = header.Id;
                    Vat.KolId = VatNaghdiMoein.KolId;
                    Vat.MoeinId = VatNaghdiMoein.Id;
                    Vat.Amount = x.TotalTax.Value;
                    Vat.Bed = 0;
                    Vat.Bes = x.TotalTax.Value;
                    Vat.ArchiveCode = x.TaxCode;
                    Vat.RowNumber = row;
                    Vat.IsDeleted = false;
                    Vat.CreatorUserName = currentUser;
                    Vat.CreateDate = DateTime.Now;
                    row++;
                    docs.Articles.Add(Vat);
                }
                else
                {
                    // اعتبار تحقق نیافته 
                    Acc_Article Vat = new Acc_Article();
                    Vat.Id = Guid.NewGuid();
                    Vat.DocId = header.Id;
                    Vat.KolId = VatNesiyehiMoein.KolId;
                    Vat.MoeinId = VatNesiyehiMoein.Id;
                    Vat.Tafsil4Id = tafsilId;
                    Vat.Tafsil4Name = tafsilName;
                    Vat.Amount = x.TotalTax.Value;
                    Vat.Bed = 0;
                    Vat.Bes = x.TotalTax.Value;
                    Vat.ArchiveCode = x.TaxCode;
                    Vat.RowNumber = row;
                    Vat.IsDeleted = false;
                    Vat.CreatorUserName = currentUser;
                    Vat.CreateDate = DateTime.Now;
                    row++;
                    docs.Articles.Add(Vat);
                }
            }

            return docs;

        }
        public async Task<BulkDocDto> PreparingToCreateGarnetMoadianDailyDocAsync(List<GarnetMoadianOutput> report, bool isSale, long sellerId, int periodId, string currentUser)
        {
            BulkDocDto docs = new BulkDocDto();

            var saleMoein = await _db.Acc_Coding_Moeins.AsNoTracking()
                .FirstOrDefaultAsync(n => n.SellerId == sellerId && n.MoeinCode == "6010001");
            var DebtorMoein = await _db.Acc_Coding_Moeins.AsNoTracking()
                .FirstOrDefaultAsync(n => n.SellerId == sellerId && n.MoeinCode == "2060001");
            var VatNaghdiMoein = await _db.Acc_Coding_Moeins.AsNoTracking()
                .FirstOrDefaultAsync(n => n.SellerId == sellerId && n.MoeinCode == "5030006");
            var VatNesiyehiMoein = await _db.Acc_Coding_Moeins.AsNoTracking()
                .FirstOrDefaultAsync(n => n.SellerId == sellerId && n.MoeinCode == "5030007");
            var SaleDiscountMoein = await _db.Acc_Coding_Moeins.AsNoTracking()
                .FirstOrDefaultAsync(n => n.SellerId == sellerId && n.MoeinCode == "6030001");

            int atf = await _accService.DocAutoNumberGeneratorAsync(sellerId, periodId);
            int docNo = await _accService.DocNumberGeneratorAsync(sellerId, periodId);

            // گروه‌بندی فاکتورها بر اساس تاریخ
            var groupedReports = report.Where(x => x.Date.HasValue)
                                       .GroupBy(x => x.Date.Value.Date)
                                       .OrderBy(g => g.Key);

            foreach (var group in groupedReports)
            {
                var date = group.Key;
                var header = new Acc_Document
                {
                    Id = Guid.NewGuid(),
                    SellerId = sellerId,
                    PeriodId = periodId,
                    AtfNumber = atf,
                    AutoDocNumber = atf,
                    DocNumber = docNo,
                    DocDate = date,
                    Description = "بابت منظور نمودن مدارک مثبته ضمیمه سند حسابداری",
                    IsDeleted = false,
                    CreatorUserName = currentUser,
                    CreateDate = DateTime.Now
                };

                atf++;
                docNo++;
                docs.Headers.Add(header);

                int row = 1;

                foreach (var x in group)
                {
                    long? tafsilId = null;
                    string? tafsilName = null;
                    if (!string.IsNullOrEmpty(x.BuyerName))
                    {
                        tafsilId = await _coding.CheckAddTafsilAsync(x.BuyerName, sellerId);
                        tafsilName = x.BuyerName;
                    }

                    // بدهکاران تجاری
                    var debtor = new Acc_Article
                    {
                        Id = Guid.NewGuid(),
                        DocId = header.Id,
                        KolId = DebtorMoein.KolId,
                        MoeinId = DebtorMoein.Id,
                        Amount = x.TotalAmount ?? 0,
                        Bed = x.TotalAmount ?? 0,
                        Bes = 0,
                        Tafsil4Id = tafsilId,
                        Tafsil4Name = tafsilName,
                        ArchiveCode = x.TaxCode,
                        RowNumber = row++,
                        IsDeleted = false,
                        CreatorUserName = currentUser,
                        CreateDate = DateTime.Now
                    };
                    docs.Articles.Add(debtor);

                    // فروش
                    var sale = new Acc_Article
                    {
                        Id = Guid.NewGuid(),
                        DocId = header.Id,
                        KolId = saleMoein.KolId,
                        MoeinId = saleMoein.Id,
                        Amount = x.AmountBeforeDiscount ?? 0,
                        Bed = 0,
                        Bes = x.AmountBeforeDiscount ?? 0,
                        Tafsil4Id = tafsilId,
                        Tafsil4Name = tafsilName,
                        ArchiveCode = x.TaxCode,
                        RowNumber = row++,
                        IsDeleted = false,
                        CreatorUserName = currentUser,
                        CreateDate = DateTime.Now
                    };
                    docs.Articles.Add(sale);

                    if (x.TotalDiscount > 0)
                    {
                        // تخفیفات نقدی فروش
                        var saleDiscount = new Acc_Article
                        {
                            Id = Guid.NewGuid(),
                            DocId = header.Id,
                            KolId = SaleDiscountMoein.KolId,
                            MoeinId = SaleDiscountMoein.Id,
                            Amount = x.TotalDiscount ?? 0,
                            Bed = x.TotalDiscount ?? 0,
                            Bes = 0,
                            ArchiveCode = x.TaxCode,
                            RowNumber = row++,
                            IsDeleted = false,
                            CreatorUserName = currentUser,
                            CreateDate = DateTime.Now
                        };
                        docs.Articles.Add(saleDiscount);
                    }

                    // مالیات بر ارزش افزوده
                    var vatMoein = x.SettlementType == "نقدی" ? VatNaghdiMoein : VatNesiyehiMoein;
                    var vat = new Acc_Article
                    {
                        Id = Guid.NewGuid(),
                        DocId = header.Id,
                        KolId = vatMoein.KolId,
                        MoeinId = vatMoein.Id,
                        Amount = x.TotalTax ?? 0,
                        Bed = 0,
                        Bes = x.TotalTax ?? 0,
                        Tafsil4Id = tafsilId,
                        Tafsil4Name = tafsilName,
                        ArchiveCode = x.TaxCode,
                        RowNumber = row++,
                        IsDeleted = false,
                        CreatorUserName = currentUser,
                        CreateDate = DateTime.Now
                    };
                    docs.Articles.Add(vat);
                }
            }

            return docs;
        }

        public async Task<clsResult> DeleteMoadianAsync(List<long> items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطلاعاتی یافت نشد";
            result.ShowMessage = true;

            if (items?.Count == 0)
                return result;

            var transaction = await _db.Acc_ModianReports.Where(n => items.Contains(n.Id)).ToListAsync();
            if (transaction.Count == 0)
                return result;
            _db.Acc_ModianReports.RemoveRange(transaction);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "حذف اطلاعات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = $"خطایی در عملیات ثبت اطلاعات بوجود آمده است . <br> {x.Message}";
            }

            return result;
        }


        public async Task<clsResult> InsertBulkDocsAsync(BulkDocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطلاعاتی برای درج در دیتابیس یافت نشد";
            result.ShowMessage = true;

            if (dto.Articles.Count == 0) return result;

            _db.Acc_Documents.AddRange(dto.Headers);
            _db.Acc_Articles.AddRange(dto.Articles);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "ثبت اسناد با موفقیت انجام شد . برای مشاهده اسناد به صفحه لیست اسناد بروید";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = $"خطایی در عملیات ثبت اسناد بوجود آمده است . \n {x.Message}";
            }

            return result;
        }

        public async Task<clsResult> BankTransactionSaveAsCheckedAsync(List<long> items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطلاعاتی یافت نشد";
            result.ShowMessage = true;

            if (items?.Count == 0)
                return result;

            var transaction = await _db.TreBankTransactions.Where(n => items.Contains(n.Id)).ToListAsync();
            if (transaction.Count == 0)
                return result;
            foreach (var x in transaction)
            {
                x.IsChecked = true;
                x.HasDoc = true;

            }
            _db.TreBankTransactions.UpdateRange(transaction);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = $"خطایی در عملیات ثبت اطلاعات بوجود آمده است . \n {x.Message}";
            }

            return result;
        }

        public async Task<BankTransactionEditDto> GetBankTransactionByIdAsync(long Id)
        {
            BankTransactionEditDto dto = new BankTransactionEditDto();
            var t = await _db.TreBankTransactions.FindAsync(Id);
            if (t == null)
                return null;

            dto.Id = t.Id;
            dto.TransactionNumber = t.DocumentNumber;
            dto.Description = t.Description;
            dto.TransactionDate = t.Date?.LatinToPersian() ?? "";
            dto.Comment = t.Note;

            return dto;
        }

        public async Task<clsResult> BankEditTransactionUserCommentAsync(BankTransactionEditDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطلاعاتی یافت نشد";
            result.ShowMessage = true;

            if (dto.Id == 0)
                return result;

            var transaction = await _db.TreBankTransactions.FindAsync(dto.Id);
            if (transaction == null)
                return result;

            transaction.Note = dto.Comment;
            _db.TreBankTransactions.Update(transaction);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = $"خطایی در عملیات ثبت اطلاعات بوجود آمده است . \n {x.Message}";
            }

            return result;
        }

        public async Task<clsResult> BankTransactionCheckTogleAsync(long Id)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطلاعاتی یافت نشد";
            result.ShowMessage = true;

            var transaction = await _db.TreBankTransactions.FindAsync(Id);
            if (transaction == null)
                return result;
            if (transaction.IsChecked)
            {
                transaction.IsChecked = false;
                transaction.HasDoc = false;
            }
            else
            {
                transaction.IsChecked = true;
                transaction.HasDoc = true;
            }


            _db.TreBankTransactions.UpdateRange(transaction);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = $"خطایی در عملیات ثبت اطلاعات بوجود آمده است . \n {x.Message}";
            }

            return result;
        }

        public async Task<clsResult> DeleteBankTransactionAsync(List<long> items)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "اطلاعاتی یافت نشد";
            result.ShowMessage = true;

            if (items?.Count == 0)
                return result;

            var transaction = await _db.TreBankTransactions.Where(n => items.Contains(n.Id)).ToListAsync();
            if (transaction.Count == 0)
                return result;
            _db.TreBankTransactions.RemoveRange(transaction);
            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "حذف اطلاعات با موفقیت انجام شد";
            }
            catch (Exception x)
            {
                result.Success = false;
                result.Message = $"خطایی در عملیات حذف اطلاعات بوجود آمده است . \n {x.Message}";
            }

            return result;
        }

        // Moadian Export
        public async Task<List<MoadianExportDto>> CreateMoadianReport(DocFilterDto filter)
        {
            var moeinSale = await _db.Acc_Settings.Where(n => n.SellerId == filter.SellerId).FirstOrDefaultAsync();
            if (moeinSale == null || !moeinSale.saleMoeinId.HasValue) return new List<MoadianExportDto>();

            List<MoadianExportDto> invoices = new List<MoadianExportDto>();

            var query = _db.Acc_Articles.AsNoTracking().Include(n => n.Doc)
               .Where(n => (!n.IsDeleted && !n.Doc.IsDeleted)
                           && n.SellerId == filter.SellerId
                           && n.Doc.PeriodId == filter.PeriodId
                           && n.MoeinId == moeinSale.saleMoeinId).AsQueryable();

            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                DateTime date = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                DateTime date = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= date);
            }

            var groupedArts = await query.GroupBy(n => new { n.Doc.DocDate, n.Tafsil4Id }).Select(n => new
            {
                date = n.Key.DocDate,
                tafsilId = n.Key.Tafsil4Id,
                bes = n.Sum(s => s.Bes),

            }).OrderBy(n => n.date).ToListAsync();

            int seq = 1;
            foreach (var x in groupedArts)
            {
                string num = x.date.LatinToPersian().Substring(5).Replace("/", "") + seq.ToString("000");

                MoadianExportDto n = new MoadianExportDto();
                n.InvoiceNumber = num;
                n.AccountingInvoiceCode = num;

                n.InvoiceDate = x.date.LatinToPersian();
                n.InvoiceType = x.tafsilId == 13931 ? (short)1 : (short)2;
                n.BasePrice = x.bes;
                n.BuyerType = x.tafsilId == 13931 ? (short)1 : (short)2;
                seq++;

                invoices.Add(n);
            }

            return invoices;

        }


        public async Task<clsResult> ConvertAccountsAsync(ConvertAccountsDto dto)
        {

            clsResult result = new clsResult();
            result.Success = false;

            var hasdata = await _db.Acc_Articles.Where(n => dto.ArticleIds.Contains(n.Id)).AnyAsync();
            if (!hasdata)
            {
                result.Message = "اطلاعاتی یافت نشد";
                return result;
            }


            if (dto.NewMoeinId.HasValue && dto.NewMoeinId > 0)
            {
                var moein = await _db.Acc_Coding_Moeins.Where(n => n.Id == dto.NewMoeinId).FirstOrDefaultAsync();
                if (moein == null)
                {
                    result.Message = "اطلاعات حساب معین یافت نشد";
                    return result;
                }

                int resultUpdate = await _db.Acc_Articles.Where(n => dto.ArticleIds.Contains(n.Id))
                    .ExecuteUpdateAsync(n =>
                    n.SetProperty(a => a.MoeinId, dto.NewMoeinId)
                    .SetProperty(a => a.KolId, moein.KolId)
                   );
            }


            //=============================================================================================
            //========================================================================== Tafsil 4 Updating
            //=============================================================================================
            //حساب تفصیل 4 برای آرتیکل های انتخاب شده حذف شون
            if (dto.Tafsil4Action == 2)
            {
                int resultUpdate2 = await _db.Acc_Articles
                         .Where(n => dto.ArticleIds.Contains(n.Id))
                         .ExecuteUpdateAsync(n =>
                             n.SetProperty(a => a.Tafsil4Id, (long?)null)
                              .SetProperty(a => a.Tafsil4Name, (string?)null));
            }
            //حساب تفصیل 4 به حساب انتخاب شده زیر تغییر کنند
            else if (dto.Tafsil4Action == 3)
            {
                if (!dto.Tafsil4Id.HasValue)
                {
                    result.Message = "حساب تفصیلی 4 را انتخاب نکردید";
                    return result;
                }
                var tafsil4 = await _db.Acc_Coding_Tafsils.Where(n => n.Id == dto.Tafsil4Id.Value).FirstOrDefaultAsync();
                if (tafsil4 == null)
                {
                    result.Message = "اطلاعات حساب تفصیلی 4 یافت نشد";
                    return result;
                }
                int resultUpdate3 = await _db.Acc_Articles
                        .Where(n => dto.ArticleIds.Contains(n.Id))
                        .ExecuteUpdateAsync(n =>
                            n.SetProperty(a => a.Tafsil4Id, tafsil4.Id)
                             .SetProperty(a => a.Tafsil4Name, tafsil4.Name));

            }

            //=============================================================================================
            //========================================================================== Tafsil 5 Updating
            //=============================================================================================

            //حساب تفصیل 4 برای آرتیکل های انتخاب شده حذف شون
            if (dto.Tafsil5Action == 2)
            {
                int resultUpdate4 = await _db.Acc_Articles
                         .Where(n => dto.ArticleIds.Contains(n.Id))
                         .ExecuteUpdateAsync(n =>
                             n.SetProperty(a => a.Tafsil5Id, (long?)null)
                              .SetProperty(a => a.Tafsil5Name, (string?)null));
            }


            //حساب تفصیل 5 به حساب انتخاب شده زیر تغییر کنند
            else if (dto.Tafsil5Action == 3)
            {
                if (!dto.Tafsil5Id.HasValue)
                {
                    result.Message = "حساب تفصیلی 5 را انتخاب نکردید";
                    return result;
                }
                var tafsil5 = await _db.Acc_Coding_Tafsils.Where(n => n.Id == dto.Tafsil5Id.Value).FirstOrDefaultAsync();
                if (tafsil5 == null)
                {
                    result.Message = "اطلاعات حساب تفصیلی 5 یافت نشد";
                    return result;
                }
                int resultUpdate3 = await _db.Acc_Articles
                        .Where(n => dto.ArticleIds.Contains(n.Id))
                        .ExecuteUpdateAsync(n =>
                            n.SetProperty(a => a.Tafsil5Id, tafsil5.Id)
                             .SetProperty(a => a.Tafsil5Name, tafsil5.Name));

            }

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "عملیات تبدیل حساب ها با موفقیت انجام شد";

            }
            catch
            {
                result.Success = false;
                result.Message = "خطایی در ذخیره اطلاعات رخ داده است";
            }
            return result;
        }
    }
}
