using ClosedXML.Excel;
using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Areas.DataTransfer.Dto;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccImportService : IAccImportService
    {
        private readonly AppDbContext _db;
        private readonly IAccCodingService _coding;
        private readonly IAccOperationService _op;
        public AccImportService(AppDbContext db, IAccCodingService coding, IAccOperationService op)
        {
            _db = db;
            _coding = coding;
            _op = op;
        }
        public async Task<clsResult> GetCodingFromExcelAsync(IFormFile excelFile, long sellerId)
        {
            clsResult result = new clsResult();
            result.Success = false;

            List<ImportCodingDto> codingData = new List<ImportCodingDto>();
            List<Acc_Coding_Group> groups = new List<Acc_Coding_Group>();
            List<Acc_Coding_Kol> kols = new List<Acc_Coding_Kol>();
            List<Acc_Coding_Moein> moeins = new List<Acc_Coding_Moein>();


            if (excelFile == null || excelFile.Length == 0)
                return result;

            //Get data from Excel file
            if (excelFile.FileName.EndsWith("xlsx") || excelFile.FileName.EndsWith("xls"))
            {
                using (var stream = new MemoryStream())
                {
                    excelFile.CopyTo(stream);
                    stream.Position = 0;
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        int rowCount = worksheet.LastRowUsed().RowNumber();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            ImportCodingDto dto = new ImportCodingDto();

                            dto.MoeinCode = worksheet.Cell(row, 1).GetValue<string>();
                            dto.MoeinName = worksheet.Cell(row, 2).GetValue<string>();
                            dto.MoeinNature = worksheet.Cell(row, 3).GetValue<short>();

                            dto.KolCode = worksheet.Cell(row, 4).GetValue<string>();
                            dto.KolName = worksheet.Cell(row, 5).GetValue<string>();
                            dto.KolNature = worksheet.Cell(row, 6).GetValue<short>();

                            dto.GroupCode = worksheet.Cell(row, 7).GetValue<string>();
                            dto.GroupName = worksheet.Cell(row, 8).GetValue<string>();
                            dto.GroupType = worksheet.Cell(row, 9).GetValue<short>();
                            codingData.Add(dto);
                        }
                    }
                }

            }

            //Groups
            var GroupsData = codingData.DistinctBy(x => x.GroupCode).ToList();
            foreach (var x in GroupsData.OrderBy(n => n.GroupCode).ToList())
            {
                groups.Add(new Acc_Coding_Group
                {
                    AltGroupCode = x.GroupCode,
                    GroupName = x.GroupName,
                    TypeId = x.GroupType,
                    GroupCode = x.GroupCode,
                    IsEditable = true,
                    SellerId = sellerId
                });

            }
            _db.Acc_Coding_Groups.AddRange(groups);
            try
            {
                await _db.SaveChangesAsync();
                //Kols
                groups = _db.Acc_Coding_Groups.AsNoTracking().Where(n => n.SellerId == sellerId).ToList();
                var KolsData = codingData.DistinctBy(x => x.KolCode).ToList();
                foreach (var x in KolsData.OrderBy(n => n.GroupCode).ThenBy(n => n.KolCode).ToList())
                {
                    var kolGroup = groups.FirstOrDefault(n => n.GroupCode == x.GroupCode);
                    if (kolGroup == null) continue;
                    kols.Add(new Acc_Coding_Kol
                    {
                        GroupId = kolGroup.Id,
                        KolCode = x.KolCode,
                        KolName = x.KolName,
                        Nature = x.KolNature,
                        TypeId = x.GroupType,
                        IsEditable = true,
                        SellerId = sellerId,
                    });
                }
                _db.Acc_Coding_Kols.AddRange(kols);
                try
                {
                    await _db.SaveChangesAsync();
                    //Moeins
                    kols = _db.Acc_Coding_Kols.AsNoTracking().Where(n => n.SellerId == sellerId).ToList();
                    var MoeinsData = codingData.DistinctBy(n => n.MoeinCode).ToList();
                    foreach (var x in MoeinsData)
                    {
                        var kol = kols.FirstOrDefault(n => n.KolCode == x.KolCode);
                        if (kol == null) continue;
                        moeins.Add(new Acc_Coding_Moein
                        {
                            KolId = kol.Id,
                            MoeinCode = x.MoeinCode,
                            MoeinName = x.MoeinName,
                            Nature = x.MoeinNature,
                            IsCurrencyAccount = false,
                            IsEditable = true,
                            SellerId = sellerId,
                        });
                    }
                    _db.Acc_Coding_Moeins.AddRange(moeins);
                    try
                    {
                        await _db.SaveChangesAsync();
                        result.Success = true;
                        result.Message = ":کدینک حسابداری با موفقیت بارگذاری شد.";
                        return result;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                catch
                {
                    result.Message = "در بارگذاری اطلاعات حسابهای کل خطایی رخ داده است";
                    return result;
                }

            }
            catch
            {
                result.Message = "در بارگذاری اطلاعات گروهای حساب خطایی رخ داده است";
                return result;
            }

            return result;
        }
        public List<ImportDocDto> GetDocFromExl_Sepidar(IFormFile excelFile)
        {
            List<ImportDocDto> Artics = new List<ImportDocDto>();

            if (excelFile == null || excelFile.Length == 0)
                return Artics;

            if (excelFile.FileName.EndsWith("xlsx") || excelFile.FileName.EndsWith("xls"))
            {
                using (var stream = new MemoryStream())
                {
                    excelFile.CopyTo(stream);
                    stream.Position = 0;
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        int rowCount = worksheet.LastRowUsed().RowNumber();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            ImportDocDto dto = new ImportDocDto();

                            dto.RowNumber = worksheet.Cell(row, 1).GetValue<int>();
                            dto.DocNumber = worksheet.Cell(row, 2).GetValue<int>();
                            dto.OldDocnumber = worksheet.Cell(row, 2).GetValue<int>();
                            dto.PersianDate = worksheet.Cell(row, 3).GetValue<string>();
                            if (string.IsNullOrEmpty(dto.PersianDate))
                                continue;
                            dto.DocDate = dto.PersianDate.PersianToLatin();
                            var Desc = worksheet.Cell(row, 4).Value;

                            dto.Bed = worksheet.Cell(row, 5).GetValue<Int64>();
                            dto.Bes = worksheet.Cell(row, 6).GetValue<Int64>();

                            string[] kol = worksheet.Cell(row, 7).GetValue<string>().Split('-').ToArray();
                            if (kol.Length > 0)
                            {
                                dto.KolName = kol[1].Trim();
                                dto.KolCode = kol[0].Trim();
                            }

                            string[] moein = worksheet.Cell(row, 8).GetValue<string>().Split('-').ToArray();
                            if (moein.Length > 0)
                            {
                                dto.MoeinName = moein[1].Trim();
                                dto.MoeinCod = moein[0].Trim();
                            }
                            else
                                continue;

                            string[] tafsil = worksheet.Cell(row, 9).GetValue<string>().Split('-').ToArray();
                            if (tafsil.Length > 0)
                                dto.TafsilCode = tafsil[0].Trim();
                            if (tafsil.Length > 1)
                                dto.TafsilName = tafsil[1].Trim();


                            Artics.Add(dto);

                            var groupedDocs = Artics.GroupBy(doc => doc.OldDocnumber);

                            foreach (var group in groupedDocs)
                            {
                                // پیدا کردن اولین ردیفی که حساب تفصیلی دارد
                                var tafsilInfo = group.FirstOrDefault(doc => !string.IsNullOrEmpty(doc.TafsilName) && !string.IsNullOrEmpty(doc.TafsilCode));

                                if (tafsilInfo != null)
                                {
                                    // به‌روزرسانی همه ردیف‌های گروه با حساب تفصیلی پیدا شده
                                    foreach (var doc in group)
                                    {
                                        doc.TafsilName = tafsilInfo.TafsilName;
                                        doc.TafsilCode = tafsilInfo.TafsilCode;
                                    }
                                }
                            }

                        }
                    }
                }
            }

            return Artics;
        }
        public List<ImportDocDto> GetDocFromExl_General(IFormFile excelFile)
        {
            List<ImportDocDto> Artics = new List<ImportDocDto>();

            if (excelFile == null || excelFile.Length == 0)
                return Artics;

            if (excelFile.FileName.EndsWith("xlsx") || excelFile.FileName.EndsWith("xls"))
            {
                using (var stream = new MemoryStream())
                {
                    excelFile.CopyTo(stream);
                    stream.Position = 0;
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        int rowCount = worksheet.LastRowUsed().RowNumber();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            ImportDocDto dto = new ImportDocDto();

                            dto.RowNumber = worksheet.Cell(row, 1).GetValue<int>();
                            dto.DocNumber = worksheet.Cell(row, 2).GetValue<int>();
                            dto.OldDocnumber = worksheet.Cell(row, 3).GetValue<int>();
                            dto.PersianDate = worksheet.Cell(row, 4).GetValue<string>();
                            if (string.IsNullOrEmpty(dto.PersianDate))
                                continue;
                            dto.DocDate = dto.PersianDate.PersianToLatin();
                            dto.DocDescription = worksheet.Cell(row, 5).GetValue<string>();
                            dto.Description = worksheet.Cell(row, 6).GetValue<string>();
                            dto.Bed = worksheet.Cell(row, 7).GetValue<Int64>();
                            dto.Bes = worksheet.Cell(row, 8).GetValue<Int64>();

                            dto.KolName = worksheet.Cell(row, 9).GetValue<string>();
                            dto.KolCode = worksheet.Cell(row, 10).GetValue<string>();

                            dto.MoeinName = worksheet.Cell(row, 11).GetValue<string>();
                            dto.MoeinCod = worksheet.Cell(row, 12).GetValue<string>();
                            if (string.IsNullOrEmpty(dto.MoeinCod))
                                continue;
                            dto.TafsilName = worksheet.Cell(row, 13).GetValue<string>();
                            dto.TafsilName5 = worksheet.Cell(row, 14).GetValue<string>();
                            dto.TafsilName6 = worksheet.Cell(row, 15).GetValue<string>();

                            Artics.Add(dto);

                            //var groupedDocs = Artics.GroupBy(doc => doc.OldDocnumber);

                            //foreach (var group in groupedDocs)
                            //{
                            //    // پیدا کردن اولین ردیفی که حساب تفصیلی دارد
                            //    var tafsilInfo = group.FirstOrDefault(doc => !string.IsNullOrEmpty(doc.TafsilName) && !string.IsNullOrEmpty(doc.TafsilCode));

                            //    if (tafsilInfo != null)
                            //    {
                            //        // به‌روزرسانی همه ردیف‌های گروه با حساب تفصیلی پیدا شده
                            //        foreach (var doc in group)
                            //        {
                            //            doc.TafsilName = tafsilInfo.TafsilName;
                            //            doc.TafsilCode = tafsilInfo.TafsilCode;
                            //        }
                            //    }
                            //}

                        }
                    }
                }
            }

            return Artics;
        }
        public List<ImportDocDto> AssignDocumentNumbers(List<ImportDocDto> documents, long sellerId, int periodId)
        {
            if (documents == null || !documents.Any())
                return documents;

            // مرتب سازی لیست بر اساس تاریخ
            documents = documents.OrderBy(d => d.DocDate).ToList();

            var sellerDocs = _db.Acc_Documents.Where(n => n.SellerId == sellerId && n.PeriodId == periodId && n.IsDeleted == false)
                .Select(n => new { docNumber = n.DocNumber }).ToList();


            int docNumber = 1;
            if (sellerDocs.Count > 0)
                docNumber = sellerDocs.Max(n => n.docNumber) + 1;

            DateTime currentGroupStartDate = documents.First().DocDate;
            DateTime currentGroupEndDate = currentGroupStartDate.AddDays(4);

            foreach (var doc in documents)
            {
                // اگر تاریخ سند جاری خارج از بازه 5 روزه گروه فعلی باشد
                if (doc.DocDate > currentGroupEndDate)
                {
                    docNumber++;
                    currentGroupStartDate = doc.DocDate;
                    currentGroupEndDate = currentGroupStartDate.AddDays(4);
                }

                doc.DocNumber = docNumber;
            }

            // به‌روزرسانی شماره سند به بزرگترین تاریخ هر گروه
            var groupedDocuments = documents.GroupBy(d => d.DocNumber)
                                            .Select(g => new { DocNumber = g.Key, MaxDate = g.Max(d => d.DocDate) })
                                            .ToList();

            foreach (var group in groupedDocuments)
            {
                foreach (var doc in documents.Where(d => d.DocNumber == group.DocNumber))
                {
                    doc.DocDate = group.MaxDate;
                }
            }
            return documents;
        }
        public async Task<clsResult> AddBulkDocsAsync(List<ImportDocDto> documents, string userName, long sellerId, int peropdId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "";
            result.ShowMessage = true;

            var fp = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == peropdId);
            if (fp == null || fp.SellerId != sellerId)
            {
                result.Message = "دوره مالی معتبر نیست";
            }

            List<Acc_Document> docs = documents.GroupBy(d => new { d.DocNumber, d.OldDocnumber, d.DocDate }).Select(n => new Acc_Document
            {
                Id = Guid.NewGuid(),
                SellerId = sellerId,
                PeriodId = peropdId,
                AtfNumber = n.Key.OldDocnumber,
                DocNumber = n.Key.DocNumber,
                AutoDocNumber = n.Key.OldDocnumber,
                DocDate = n.Key.DocDate,
                TypeId = 1,
                CreateDate = DateTime.Now,
                CreatorUserName = userName,
                Description = n.Max(s => s.DocDescription),
                IsDeleted = false,
                StatusId = 1,
                SubsystemId = 1,
            }).ToList();

            _db.Acc_Documents.AddRange(docs);
            List<Acc_Article> Articles = new List<Acc_Article>();
            foreach (var doc in docs)
            {
                List<Acc_Article> docArts = new List<Acc_Article>();
                var artics = documents.Where(n => n.DocNumber == doc.DocNumber)
                    .OrderBy(n => n.TafsilName)
                    .ToList();
                int row = 1;
                foreach (var a in artics)
                {
                    Acc_Article art = new Acc_Article();
                    art.RowNumber = row;
                    art.Id = Guid.NewGuid();
                    art.DocId = doc.Id;
                    art.PeriodId = peropdId;
                    art.SellerId = sellerId;

                    int? mid = await _coding.GetMoeinIdByCodeAsync(a.MoeinCod, sellerId);
                    if (!mid.HasValue) continue;
                    art.MoeinId = mid.Value;
                    art.KolId = await _coding.GetKolIdByCodeAsync(a.KolCode, sellerId);
                    if (!string.IsNullOrEmpty(a.TafsilName))
                    {
                        art.Tafsil4Id = await _coding.CheckAddTafsilAsync(a.TafsilName, sellerId);
                        art.Tafsil4Name = a.TafsilName;
                    }
                    if (!string.IsNullOrEmpty(a.TafsilName5))
                    {
                        art.Tafsil5Id = await _coding.CheckAddTafsilAsync(a.TafsilName5, sellerId);
                        art.Tafsil5Name = a.TafsilName5;
                    }
                    if (!string.IsNullOrEmpty(a.TafsilName6))
                    {
                        art.Tafsil6Id = await _coding.CheckAddTafsilAsync(a.TafsilName6, sellerId);
                        art.Tafsil6Name = a.TafsilName6;
                    }

                    art.Comment = a.Description;

                    art.Bed = a.Bed;
                    art.Bes = a.Bes;
                    art.Amount = a.Bed > 0 ? a.Bed : a.Bes;

                    art.CreateDate = DateTime.UtcNow;
                    art.CreatorUserName = userName;
                    art.IsDeleted = false;

                    docArts.Add(art);
                    row++;
                }
                Articles.AddRange(docArts);
            }
            _db.Acc_Articles.AddRange(Articles);
            if (Convert.ToBoolean(await _db.SaveChangesAsync()))
            {
                result.Success = true;
                result.Message = "بارگذاری اسناد با موفقیت انجام شد.";
            }

            return result;
        }
        public async Task<clsResult> AddBulkKpDocsAsync(List<ImportSaleDocDto> documents, string userName, long sellerId, int peropdId, int? subsystemId = null)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.Message = "";
            result.ShowMessage = true;

            //------------------
            //  کنترل میکند که آبا بارنامه تکراری نباشد و تکراری نیز سند نخورد
            var BillOfLadingNos = documents.DistinctBy(n => new { n.BillId }).ToList();
            var BillOfLadingCount = BillOfLadingNos.Count();
            foreach (var BillOfLading in BillOfLadingNos)
            {
                long num = !string.IsNullOrEmpty(BillOfLading.BillOfLadingNumber) ? Convert.ToInt64(BillOfLading.BillOfLadingNumber) : 0;
                var checkBill = await _db.KPOldSystemSales.Where(n => n.SellerId == sellerId && n.NonSystemicBillOfLadingNumber == num).ToListAsync();
                var checkAccDocs = await _db.Acc_Articles.Include(n => n.Doc)
                        .Where(n => n.SellerId == sellerId && (!n.IsDeleted && !n.Doc.IsDeleted) && n.ArchiveCode != null && n.ArchiveCode == BillOfLading.BillOfLadingNumber)
                       .ToListAsync();

                if (checkBill.Count > 1 || checkAccDocs.Count > 0)
                {
                    string ErrorMessage = "";
                    var BillHasDoc = checkBill.Where(n => n.DocNumber != null).ToList();

                    if (BillHasDoc.Count > 0)
                    {
                        ErrorMessage += $"بارنامه {num} تکراری است. قبل از ثبت سند حسابداری اطلاعات را بررسی نمایید.";
                        string docnums = "";
                        foreach (var item in BillHasDoc)
                        {
                            docnums += $"- {item.DocNumber} -";
                        }
                        ErrorMessage += $"\n بنظر میرسد در سند(های) {docnums} دارای ثبت حسابداری باشد";
                        result.Message += $"\n {ErrorMessage}";
                    }
                    if (checkAccDocs != null)
                    {
                        ErrorMessage += $"\n  در سند شماره {checkAccDocs.FirstOrDefault()?.Doc.DocNumber} ثبت شده است";
                    }

                    var removList = documents.Where(n => n.BillOfLadingNumber == BillOfLading.BillOfLadingNumber).ToList();
                    var barnameh = await _db.KPOldSystemSales.SingleOrDefaultAsync(n => n.Id == BillOfLading.BillId);
                    if (barnameh != null)
                    {
                        barnameh.ErrorMessage = ErrorMessage;
                        if (checkAccDocs != null && checkAccDocs.Count == 1)
                        {
                            barnameh.DocNumber = checkAccDocs.FirstOrDefault()?.Doc.AutoDocNumber;
                            barnameh.DocId = checkAccDocs.FirstOrDefault()?.Doc.Id;
                            barnameh.ErrorMessage = string.Empty;
                        }

                        _db.KPOldSystemSales.Update(barnameh);
                        await _db.SaveChangesAsync();
                    }
                    foreach (var item in removList)
                    {
                        documents.Remove(item);
                    }
                }
            }
            //-----------------------------------------------------------------------
            if (documents.Count == 0)
            {
                result.Message = "سندی جهت ثبت وجود ندارد.";
                result.Success = false;
                return result;

            }
            var fp = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == peropdId);
            if (fp == null || fp.SellerId != sellerId)
            {
                result.Message = "دوره مالی معتبر نیست";
            }

            List<Acc_Document> docs = documents.GroupBy(d => d.DocDate).Select(n => new Acc_Document
            {
                Id = Guid.NewGuid(),
                SellerId = sellerId,
                PeriodId = peropdId,
                AtfNumber = 0,
                DocNumber = 0,
                AutoDocNumber = 0,
                DocDate = n.Max(d => d.DocDate),
                TypeId = 1,
                CreateDate = DateTime.Now,
                CreatorUserName = userName,
                Description = $"بابت فروش {n.Max(d => d.DocDate.LatinToPersian())}",
                IsDeleted = false,
                StatusId = 1,
                SubsystemId = subsystemId,

            }).ToList();

            _db.Acc_Documents.AddRange(docs);
            List<Acc_Article> Articles = new List<Acc_Article>();

            foreach (var doc in docs)
            {


                int docNumber = await _op.DocAutoNumberGeneratorAsync(sellerId, peropdId);
                doc.DocNumber = docNumber;
                doc.AutoDocNumber = docNumber;

                List<Acc_Article> docArts = new List<Acc_Article>();

                var artics = documents.Where(n => n.DocDate == doc.DocDate)
                    .OrderBy(n => n.BillOfLadingNumber)
                    .ThenByDescending(n => n.Bed).ThenByDescending(n => n.Bes).ToList();

                int row = 1;
                foreach (var a in artics)
                {
                    Acc_Article art = new Acc_Article();
                    art.RowNumber = row;
                    art.Id = Guid.NewGuid();
                    art.DocId = doc.Id;
                    art.PeriodId = peropdId;
                    art.SellerId = sellerId;

                    int? mid = await _coding.GetMoeinIdByCodeAsync(a.MoeinCod, sellerId);
                    if (!mid.HasValue) continue;
                    art.MoeinId = mid.Value;
                    art.KolId = await _coding.GetKolIdByCodeAsync(a.KolCode, sellerId);
                    if (!string.IsNullOrEmpty(a.Tafsil4Name))
                    {
                        art.Tafsil4Id = await _coding.CheckAddTafsilAsync(a.Tafsil4Name, sellerId);
                        art.Tafsil4Name = a.Tafsil4Name;
                    }
                    if (!string.IsNullOrEmpty(a.Tafsil5Name))
                    {
                        art.Tafsil5Id = await _coding.CheckAddTafsilAsync(a.Tafsil5Name, sellerId);
                        art.Tafsil5Name = a.Tafsil5Name;
                    }
                    if (!string.IsNullOrEmpty(a.Tafsil6Name))
                    {
                        art.Tafsil6Id = await _coding.CheckAddTafsilAsync(a.Tafsil6Name, sellerId);
                        art.Tafsil6Name = a.Tafsil6Name;
                    }
                    if (!string.IsNullOrEmpty(a.Tafsil7Name))
                    {
                        art.Tafsil7Id = await _coding.CheckAddTafsilAsync(a.Tafsil7Name, sellerId);
                        art.Tafsil7Name = a.Tafsil7Name;
                    }

                    art.Comment = a.Description;
                    art.ArchiveCode = a.BillOfLadingNumber;

                    art.Bed = a.Bed;
                    art.Bes = a.Bes;
                    art.Amount = a.Bed > 0 ? a.Bed : a.Bes;

                    art.CreateDate = DateTime.UtcNow;
                    art.CreatorUserName = userName;
                    art.IsDeleted = false;

                    docArts.Add(art);
                    row++;
                }
                Articles.AddRange(docArts);
            }
            _db.Acc_Articles.AddRange(Articles);

            try
            {
                await _db.SaveChangesAsync();
                var setDocNumber = Articles.DistinctBy(n => new { n.ArchiveCode }).ToList();
                foreach (var x in setDocNumber)
                {
                    long number = long.Parse(x.ArchiveCode);
                    var sale = await _db.KPOldSystemSales.FirstOrDefaultAsync(n => n.SellerId == sellerId && n.BillOfLadingNumber == number);
                    if (sale != null)
                    {
                        sale.DocId = x.DocId;
                        sale.DocNumber = x.Doc.AutoDocNumber;
                        _db.KPOldSystemSales.Update(sale);
                        await _db.SaveChangesAsync();
                    }
                }
                result.Success = true;
                result.Message = "ثبت اسناد حسابداری فروش با موفقیت انجام شد.";
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ثبت اسناد رخ داده است \n\n" + x.Message;
            }

            return result;
        }
    }
}

