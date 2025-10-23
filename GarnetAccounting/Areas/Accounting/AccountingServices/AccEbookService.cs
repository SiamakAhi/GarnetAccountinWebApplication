using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccEbookService : IAccEbookService
    {
        private readonly AppDbContext _db;

        public AccEbookService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsDocumentsBalancedAsync(long sellerId, int periodId, int minDocNumber, int maxDocNumber)
        {
            var documents = await _db.Acc_Documents.Include(d => d.DocArticles)
                .Where(d =>
                d.StatusId == sellerId
                && d.PeriodId == periodId
                && d.IsDeleted == false
                && (d.DocNumber >= minDocNumber && d.DocNumber <= maxDocNumber))
                .ToListAsync();
            foreach (var document in documents)
            {
                long totalBed = document.DocArticles.Sum(a => a.Bed);
                long totalBes = document.DocArticles.Sum(a => a.Bes);

                if (totalBed != totalBes)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> IsDocumentsBalancedAsync(long sellerId, int periodId, DateTime mindate, DateTime maxdate)
        {
            var documents = await _db.Acc_Documents.Include(d => d.DocArticles)
                .Where(d =>
                d.StatusId == sellerId
                && d.PeriodId == periodId
                && d.IsDeleted == false
                && (d.DocDate.Date >= mindate && d.DocDate.Date <= maxdate))
                .ToListAsync();
            foreach (var document in documents)
            {
                long totalBed = document.DocArticles.Sum(a => a.Bed);
                long totalBes = document.DocArticles.Sum(a => a.Bes);

                if (totalBed != totalBes)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> AreDocumentsDateOrderValidAsync(long sellerId, int periodId, int minDocNumber, int maxDocNumber)
        {
            var documents = await _db.Acc_Documents
                .Where(d =>
                d.StatusId == sellerId
                && d.PeriodId == periodId
                && d.IsDeleted == false
                && (d.DocNumber >= minDocNumber && d.DocNumber <= maxDocNumber))
                .Select(n => new { date = n.DocDate, number = n.DocNumber }).OrderBy(n => n.number)
                .ToListAsync();

            if (documents.Count <= 1)
                return true;

            for (int i = 1; i < documents.Count; i++)
            {
                var prev = documents[i - 1];
                var current = documents[i];

                if (current.date < prev.date)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> AreDocumentsDateOrderValidAsync(long sellerId, int periodId, DateTime mindate, DateTime maxdate)
        {
            var documents = await _db.Acc_Documents
                .Where(d =>
                d.StatusId == sellerId
                && d.PeriodId == periodId
                && d.IsDeleted == false
                && (d.DocDate.Date >= mindate && d.DocDate.Date <= maxdate))
                .Select(n => new { date = n.DocDate, number = n.DocNumber }).OrderBy(n => n.number)
                .ToListAsync();

            if (documents.Count <= 1)
                return true;

            for (int i = 1; i < documents.Count; i++)
            {
                var prev = documents[i - 1];
                var current = documents[i];

                if (current.date < prev.date)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<List<EBookManagerDto>> GetEbookMetadata(long sellerId, int periodId)
        {
            var e = await _db.Acc_ElectronicBookMetaData
                .Where(n => n.SellerId == sellerId && n.PeriodId == periodId && !n.IsDeleted)
                .Select(n => new EBookManagerDto
                {
                    Id = n.Id,
                    PeriodId = periodId,
                    SellerId = n.SellerId,
                    FromDate = n.FromDate,
                    ToDate = n.ToDate,
                    MinDocNumber = n.MinDocNumber,
                    MaxDocNumber = n.MaxDocNumber,
                    CreateAt = n.CreateAt,
                    CreateBy = n.CreateBy,
                    IsDeleted = n.IsDeleted,
                    IsDeletedAt = n.IsDeletedAt,
                    IsDeletedBy = n.IsDeletedBy,
                    IsSent = n.IsSent
                }).OrderBy(n => n.MinDocNumber).ToListAsync();
            return e;
        }

        public async Task<EBookManagerDto> GetEbookAsync(EBookManagerDto dto)
        {
            if (!string.IsNullOrEmpty(dto.strFromDate))
                dto.FromDate = dto.strFromDate.PersianToLatin();

            if (!string.IsNullOrEmpty(dto.strToDate))
                dto.ToDate = dto.strToDate.PersianToLatin();

            if (dto.ByDate)
            {
                bool checkOrderedByDate = await AreDocumentsDateOrderValidAsync(dto.SellerId, dto.PeriodId, dto.FromDate, dto.ToDate);
                if (!checkOrderedByDate)
                {
                    dto.Successed = false;
                    dto.Message = "در بازه انتخابی، ترتیب اسناد نادرست است";
                    return dto;
                }

                bool checkBalanced = await IsDocumentsBalancedAsync(dto.SellerId, dto.PeriodId, dto.FromDate, dto.ToDate);
                if (!checkBalanced)
                {
                    dto.Successed = false;
                    dto.Message = "در بازه انتخابی، برخی اسناد تراز نیستند";
                    return dto;
                }
            }
            else
            {
                bool checkOrderedByDate = await AreDocumentsDateOrderValidAsync(dto.SellerId, dto.PeriodId, dto.MinDocNumber, dto.MaxDocNumber);
                if (!checkOrderedByDate)
                {
                    dto.Successed = false;
                    dto.Message = "در بازه انتخابی، ترتیب اسناد نادرست است";
                    return dto;
                }

                bool checkBalanced = await IsDocumentsBalancedAsync(dto.SellerId, dto.PeriodId, dto.MinDocNumber, dto.MaxDocNumber);
                if (!checkBalanced)
                {
                    dto.Successed = false;
                    dto.Message = "در بازه انتخابی، برخی اسناد تراز نیستند";
                    return dto;
                }
            }

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n =>
                   n.Doc.SellerId == dto.SellerId
                && n.Doc.PeriodId == dto.PeriodId
                && (!n.IsDeleted && !n.Doc.IsDeleted))
                .AsQueryable();

            if (dto.ByDate)
                query = query.Where(n => n.Doc.DocDate.Date >= dto.FromDate.Date && n.Doc.DocDate.Date <= dto.ToDate.Date);
            else
                query = query.Where(n => n.Doc.DocNumber >= dto.MinDocNumber && n.Doc.DocNumber <= dto.MaxDocNumber);



            List<ElectronicBookDto> data = new List<ElectronicBookDto>();
            var queryData = await query.ToListAsync();
            foreach (var x in queryData)
            {
                ElectronicBookDto a = new ElectronicBookDto();
                a.Row = x.Doc.DocNumber;
                a.ArtRow = x.RowNumber;
                a.docDate = x.Doc.DocDate.LatinToPersian();
                a.KolCode = x.Moein.MoeinKol.KolCode;
                a.KolName = x.Moein.MoeinKol.KolName;
                if (dto.InsertMoein)
                {
                    a.MoeinCode = x.Moein.MoeinCode;
                    a.MoeinName = x.Moein.MoeinName;
                }
                if (dto.InsertDescription)
                    a.Description = x.Comment;
                a.Bed = x.Bed;
                a.Bes = x.Bes;

                data.Add(a);
            }

            dto.eBooks = data.OrderBy(n => n.Row).ThenBy(n => n.ArtRow).ToList();
            dto.Successed = true;
            return dto;

        }

        public async Task<EBookManagerDto> AddMetaDataAsync(EBookManagerDto dto)
        {
            if (dto.eBooks == null || dto.eBooks.Count == 0)
            {
                dto.Successed = false;
                dto.Message = "اطلاعاتی برای ارسال وجود ندارد";
                return dto;
            }

            string minDate = dto.eBooks.Min(n => n.docDate) ?? "";
            string maxDate = dto.eBooks.Max(n => n.docDate) ?? "";


            Acc_ElectronicBookMetaData e = new Acc_ElectronicBookMetaData();
            e.SellerId = dto.SellerId;
            e.PeriodId = dto.PeriodId;
            e.FromDate = minDate.PersianToLatin();
            e.ToDate = maxDate.PersianToLatin();
            e.MinDocNumber = dto.eBooks.Min(n => n.Row);
            e.MaxDocNumber = dto.eBooks.Max(n => n.Row);
            e.IsDeleted = false;
            e.CreateAt = DateTime.Now;
            e.CreateBy = dto.CreateBy;
            e.IsSent = dto.IsSent;
            e.IsDeletedBy = "";

            _db.Acc_ElectronicBookMetaData.Add(e);
            await _db.SaveChangesAsync();

            var docs = _db.Acc_Documents
                .Where(n => n.SellerId == dto.SellerId && n.PeriodId == dto.PeriodId && !n.IsDeleted && (n.DocNumber >= e.MinDocNumber && n.DocNumber <= e.MaxDocNumber))
                .ExecuteUpdateAsync(u =>
                u.SetProperty(p => p.eBookFileId, e.Id)
                );

            var arts = _db.Acc_Articles
               .Where(n => n.Doc.SellerId == dto.SellerId && n.Doc.PeriodId == dto.PeriodId && (!n.Doc.IsDeleted && !n.IsDeleted) && (n.Doc.DocNumber >= e.MinDocNumber && n.Doc.DocNumber <= e.MaxDocNumber))
               .ExecuteUpdateAsync(u =>
               u.SetProperty(p => p.eBookFileId, e.Id)
               );

            try
            {
                await _db.SaveChangesAsync();
                dto.Successed = true;

            }
            catch (Exception)
            {
                dto.Successed = false;
                dto.Message = "اطلاعاتی برای ارسال وجود ندارد";
            }

            return dto;

        }

    }
}
