using ClosedXML.Excel;
using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos;
using GarnetAccounting.Areas.Accounting.Dto.PrintDto;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccountingReportService : IAccountingReportService
    {
        private readonly AppDbContext _db;
        private readonly IAccCodingService _coding;
        private readonly IAccGetBaseDataService _baseData;
        public AccountingReportService(AppDbContext dbContext, IAccCodingService coding, IAccGetBaseDataService baseData)
        {
            _coding = coding;
            _db = dbContext;
            _baseData = baseData;
        }

        public async Task<MinMaxDocNumberDto> GetMinMaxDocNumberAsync(long SellerId, int PeriodId)
        {
            var data = _db.Acc_Documents.AsNoTracking().Include(n => n.DocPeriod)
                .Where(n => n.SellerId == SellerId && n.PeriodId == PeriodId && !n.IsDeleted)
                .AsQueryable();
            MinMaxDocNumberDto dto = new MinMaxDocNumberDto();
            dto.MinNumber = await data.MinAsync(n => n.DocNumber);
            dto.MaxNumber = await data.MaxAsync(n => n.DocNumber);
            dto.MinDate = await data.MinAsync(n => n.DocDate);
            dto.MaxDate = await data.MaxAsync(n => n.DocDate);

            return dto;

        }

        // معین های دارای گردش در دوره مالی جاری
        public async Task<bool> HasAccountInLevelAsync(long sellerId, int periodId, int moeinId, int tafsilLevel, long? tafsilId = null)
        {
            var query = _db.Acc_Articles
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == sellerId
                && n.Doc.PeriodId == periodId
                && n.MoeinId == moeinId
                ).AsQueryable();

            if (tafsilLevel == 5 && tafsilId.HasValue)
            {
                query = query.Where(n => n.Tafsil4Id == tafsilId);
            }
            if (tafsilLevel == 6 && tafsilId.HasValue)
            {
                query = query.Where(n => n.Tafsil5Id == tafsilId);
            }
            if (tafsilLevel == 7 && tafsilId.HasValue)
            {
                query = query.Where(n => n.Tafsil6Id == tafsilId);
            }
            if (tafsilLevel == 8 && tafsilId.HasValue)
            {
                query = query.Where(n => n.Tafsil7Id == tafsilId);
            }

            bool result = false;
            switch (tafsilLevel)
            {
                case 4:
                    result = await query.Where(n => n.Tafsil4Id != null).AnyAsync();
                    break;
                case 5:
                    result = await query.Where(n => n.Tafsil5Id != null).AnyAsync();
                    break;
                case 6:
                    result = await query.Where(n => n.Tafsil6Id != null).AnyAsync();
                    break;
                case 7:
                    result = await query.Where(n => n.Tafsil7Id != null).AnyAsync();
                    break;
                case 8:
                    result = await query.Where(n => n.Tafsil8Id != null).AnyAsync();
                    break;
                default:
                    break;
            }

            return result;
        }
        public async Task<int[]?> ActiveMoeinsIdAsync(long sellerId, int periodId)
        {
            var data = await _db.Acc_Articles.
                Where(n => n.SellerId == sellerId && n.PeriodId == periodId)
                .Select(n => n.MoeinId).Distinct()
                .ToListAsync();
            if (data.Count > 0)
            {
                return data.ToArray();
            }
            return null;
        }
        public async Task<List<Report_BrowserDto>> ActiveKolAccountsAsync(long sellerId, int periodId)
        {
            var data = await _db.Acc_Articles
                .AsNoTracking()
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.SellerId == sellerId && n.PeriodId == periodId)
                .Select(n => new Report_BrowserDto
                {
                    Id = n.Moein.KolId,
                    Description = n.Moein.MoeinKol.KolName,
                    KolCode = n.Moein.MoeinKol.KolCode,
                    KolName = n.Moein.MoeinKol.KolName,
                    Nature = n.Moein.MoeinKol.Nature,
                    SellerId = sellerId,
                    TypeId = n.Moein.MoeinKol.TypeId,
                    Bed = 0,
                    Bes = 0,
                    Mandeh = 0,
                }).Distinct().OrderBy(n => n.KolCode).ToListAsync();

            return data;
        }
        public async Task<ArticleAccountInfo> GetArticleAccountInfoAsync(DocArticleDto article)
        {
            var art = await _db.Acc_Articles.AsNoTracking().Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
             .SingleOrDefaultAsync(n => n.Id == article.Id);

            ArticleAccountInfo info = new ArticleAccountInfo();
            info.KolId = art.Moein.KolId;
            info.KolName = art.Moein.MoeinKol.KolName;
            info.MoeinId = art.MoeinId;
            info.MoeinName = art.Moein.MoeinName;
            info.Tafsil4Id = art.Tafsil4Id;
            info.Tafsil5Id = art.Tafsil5Id;
            info.Tafsil6Id = art.Tafsil6Id;
            info.Tafsil7Id = art.Tafsil7Id;
            info.Tafsil8Id = art.Tafsil8Id;
            info.Tafsil4Name = art.Tafsil4Name;
            info.Tafsil5Name = art.Tafsil5Name;
            info.Tafsil6Name = art.Tafsil6Name;
            info.Tafsil7Name = art.Tafsil7Name;
            info.Tafsil8Name = art.Tafsil8Name;

            return info;
        }

        public async Task<List<Report_BrowserDto>> Report_KolAsync(DocFilterDto filter)
        {
            // پیش‌پردازش تاریخ‌ها خارج از کوئری
            DateTime? startDate = null;
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                startDate = filter.strStartDate.PersianToLatin().Date;
            }

            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                endDate = filter.strEndDate.PersianToLatin().Date;
            }

            var baseQuery = _db.Acc_Articles.AsNoTracking()
                .Where(a => a.IsDeleted == false
                           && a.Doc.IsDeleted == false
                           && a.Doc.SellerId == filter.SellerId
                           && a.Doc.PeriodId == filter.PeriodId);

            // اعمال فیلترها
            if (filter.docType != null)
                baseQuery = baseQuery.Where(n => n.Doc.StatusId == filter.docType.Value);

            if (startDate.HasValue)
                baseQuery = baseQuery.Where(n => n.Doc.DocDate >= startDate.Value);

            if (endDate.HasValue)
                baseQuery = baseQuery.Where(n => n.Doc.DocDate <= endDate.Value);

            if (filter.FromDocNumer > 0)
                baseQuery = baseQuery.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);

            if (filter.ToDocNumer > 0)
                baseQuery = baseQuery.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);

            if (!string.IsNullOrEmpty(filter.Description))
                baseQuery = baseQuery.Where(n => n.Doc.Description.Contains(filter.Description));

            // کوئری اصلی با projection بهینه
            var result = await baseQuery
                .GroupBy(n => n.Moein.KolId)
                .Select(g => new
                {
                    KolId = g.Key,
                    KolCode = g.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = g.Max(x => x.Moein.MoeinKol.KolName),
                    Description = g.Max(x => x.Moein.MoeinKol.Description),
                    Nature = g.Max(x => x.Moein.MoeinKol.Nature),
                    TypeId = g.Max(x => x.Moein.MoeinKol.TypeId),
                    SellerId = g.Max(s => s.SellerId),
                    GroupPeriority = g.Min(x => x.Moein.MoeinKol.KolGroup.Order),
                    TotalBed = g.Sum(s => s.Bed),
                    TotalBes = g.Sum(s => s.Bes)
                })
                .OrderBy(x => x.GroupPeriority)
                .ThenBy(x => x.KolCode)
                .ToListAsync();

            // تبدیل به DTO در حافظه
            return result.Select(x =>
            {
                var mandeh = Math.Abs(x.TotalBed - x.TotalBes);
                return new Report_BrowserDto
                {
                    Id = x.KolId,
                    KolId = x.KolId,
                    KolCode = x.KolCode,
                    KolName = x.KolName,
                    Description = x.Description,
                    Nature = x.Nature,
                    TypeId = x.TypeId,
                    SellerId = x.SellerId,
                    GroupPeriority = x.GroupPeriority,
                    Bed = x.TotalBed,
                    Bes = x.TotalBes,
                    Mandeh = mandeh,
                    MandehNature = x.TotalBed > x.TotalBes ? (short)1 :
                                  (x.TotalBed < x.TotalBes ? (short)2 : (short)3),
                    MandehBed = x.TotalBed > x.TotalBes ? x.TotalBed - x.TotalBes : 0,
                    MandehBes = x.TotalBes > x.TotalBed ? x.TotalBes - x.TotalBed : 0
                };
            }).ToList();
        }
        public async Task<List<Report_BrowserDto>> Report_KolAsync_Old(DocFilterDto filter)
        {
            List<Report_BrowserDto> accounts = new List<Report_BrowserDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n => n.IsDeleted == false && n.Doc.IsDeleted == false && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId)
                .AsQueryable();

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            accounts = await query.GroupBy(n => n.Moein.KolId)
                .Select(n => new Report_BrowserDto
                {
                    Id = n.Key,
                    KolId = n.Key,
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.MoeinKol.Nature),
                    TypeId = n.Max(x => x.Moein.MoeinKol.TypeId),
                    SellerId = n.Max(s => s.SellerId),
                    GroupPeriority = n.FirstOrDefault().Moein.MoeinKol.KolGroup.Order,
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed - s.Bes) : n.Sum(s => s.Bes - s.Bed),
                    MandehNature = Convert.ToInt16((n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? 1 : ((n.Sum(s => s.Bed) < n.Sum(s => s.Bes)) ? 2 : 3)),
                    MandehBed = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed - s.Bes) : 0,
                    MandehBes = (n.Sum(s => s.Bes) > n.Sum(s => s.Bed)) ? n.Sum(s => s.Bes - s.Bed) : 0,

                }).OrderBy(n => n.GroupPeriority).ThenBy(n => n.KolCode)
                .ToListAsync();

            return accounts;
        }


        public async Task<List<Report_BrowserDto>> Report_MoeinAsync(DocFilterDto filter)
        {
            List<Report_BrowserDto> accounts = new List<Report_BrowserDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.Doc.PeriodId == filter.PeriodId
                && (filter.KolId == null ? n.MoeinId > 0 : n.Moein.KolId == filter.KolId))
                 .OrderBy(n => n.Moein.MoeinKol.KolGroup.Order).ThenBy(n => n.Moein.MoeinKol.KolCode)
                .ThenBy(n => n.Moein.MoeinCode).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber)
                .AsQueryable();

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            accounts = await query.GroupBy(n => n.MoeinId)
                .Select(n => new Report_BrowserDto
                {
                    KolId = n.Max(z => z.KolId),
                    MoeinId = n.Key,
                    GroupId = n.Max(x => x.Moein.MoeinKol.GroupId),
                    GroupName = n.Max(x => x.Moein.MoeinKol.KolGroup.GroupName),
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    MoeinCode = n.Max(x => x.Moein.MoeinCode),
                    MoeinName = n.Max(x => x.Moein.MoeinName),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.Nature),
                    SellerId = n.Max(s => s.SellerId),
                    Id = n.Max(x => x.Moein.KolId),

                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed) - n.Sum(s => s.Bes) : n.Sum(s => s.Bes) - n.Sum(s => s.Bed),
                    MandehNature = Convert.ToInt16((n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? 1 : ((n.Sum(s => s.Bed) < n.Sum(s => s.Bes)) ? 2 : 3)),
                    MandehBed = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed - s.Bes) : 0,
                    MandehBes = (n.Sum(s => s.Bes) > n.Sum(s => s.Bed)) ? n.Sum(s => s.Bes - s.Bed) : 0,

                }).ToListAsync();

            return accounts;
        }
        public async Task<List<Report_BrowserDto>> Report_Tafsil4Async(DocFilterDto filter)
        {
            List<Report_BrowserDto> accounts = new List<Report_BrowserDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.Doc.PeriodId == filter.PeriodId
                && (filter.targetId == null ? n.MoeinId > 0 : n.MoeinId == filter.targetId))
                .OrderBy(n => n.Moein.MoeinKol.KolGroup.Order).ThenBy(n => n.Moein.MoeinKol.KolCode)
                .ThenBy(n => n.Moein.MoeinCode).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber)
               .AsQueryable();

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            accounts = await query.GroupBy(n => n.Tafsil4Id)
                .Select(n => new Report_BrowserDto
                {
                    Tafsil4Id = n.Key,
                    Tafsil4Name = n.Max(x => x.Tafsil4Name),
                    MoeinId = n.Max(x => x.MoeinId),
                    GroupId = n.Max(x => x.Moein.MoeinKol.GroupId),
                    GroupName = n.Max(x => x.Moein.MoeinKol.KolGroup.GroupName),
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    MoeinCode = n.Max(x => x.Moein.MoeinCode),
                    MoeinName = n.Max(x => x.Moein.MoeinName),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.Nature),
                    SellerId = n.Max(s => s.SellerId),
                    Id = n.Max(x => x.Moein.KolId),
                    KolId = n.Max(x => x.Moein.KolId),

                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed) - n.Sum(s => s.Bes) : n.Sum(s => s.Bes) - n.Sum(s => s.Bed),
                    MandehNature = Convert.ToInt16((n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? 1 : ((n.Sum(s => s.Bed) < n.Sum(s => s.Bes)) ? 2 : 3)),
                    MandehBed = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed - s.Bes) : 0,
                    MandehBes = (n.Sum(s => s.Bes) > n.Sum(s => s.Bed)) ? n.Sum(s => s.Bes - s.Bed) : 0,

                }).ToListAsync();

            return accounts;
        }
        public async Task<List<Report_BrowserDto>> Report_TafsilAsync(DocFilterDto filter)
        {
            List<Report_BrowserDto> accounts = new List<Report_BrowserDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.Doc.PeriodId == filter.PeriodId
                && (filter.MoeinId == null ? n.MoeinId > 0 : n.MoeinId == filter.MoeinId))
                  .OrderBy(n => n.Moein.MoeinKol.KolGroup.Order).ThenBy(n => n.Moein.MoeinKol.KolCode)
                .ThenBy(n => n.Moein.MoeinCode).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber)
                .AsQueryable();


            switch (filter.ReportLevel.Value)
            {
                case 5:
                    query = query.Where(n => n.Tafsil4Id == filter.CurrentTafsilId);
                    break;
                case 6:
                    query = query.Where(n => n.Tafsil4Id == filter.tafsil4Id && n.Tafsil5Id == filter.CurrentTafsilId);
                    break;
                case 7:
                    query = query.Where(n =>
                    n.Tafsil4Id == filter.tafsil4Id
                    && n.Tafsil5Id == filter.tafsil5Id
                    && n.Tafsil6Id == filter.CurrentTafsilId);
                    break;
                case 8:
                    query = query.Where(n =>
                    n.Tafsil4Id == filter.tafsil4Id
                    && n.Tafsil5Id == filter.tafsil5Id
                    && n.Tafsil6Id == filter.tafsil6Id
                   && n.Tafsil7Id == filter.CurrentTafsilId);
                    break;

                default:
                    break;
            }

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            accounts = await query.GroupBy(n =>
            filter.ReportLevel == 4 ? n.Tafsil4Id : (filter.ReportLevel == 5 ? n.Tafsil5Id : (filter.ReportLevel == 6 ? n.Tafsil6Id : (filter.ReportLevel == 7 ? n.Tafsil7Id : n.Tafsil8Id)))
                ).Select(n => new Report_BrowserDto
                {
                    CurrentTafsilId = n.Key,
                    Tafsil4Id = n.Max(x => x.Tafsil4Id),
                    Tafsil4Name = n.Max(x => x.Tafsil4Name),

                    Tafsil5Id = n.Max(x => x.Tafsil5Id),
                    Tafsil5Name = n.Max(x => x.Tafsil5Name),

                    Tafsil6Id = n.Max(x => x.Tafsil6Id),
                    Tafsil6Name = n.Max(x => x.Tafsil6Name),

                    Tafsil7Id = n.Max(x => x.Tafsil7Id),
                    Tafsil7Name = n.Max(x => x.Tafsil7Name),

                    Tafsil8Id = n.Max(x => x.Tafsil8Id),
                    Tafsil8Name = n.Max(x => x.Tafsil5Name),

                    MoeinId = n.Max(x => x.MoeinId),
                    GroupId = n.Max(x => x.Moein.MoeinKol.GroupId),
                    GroupName = n.Max(x => x.Moein.MoeinKol.KolGroup.GroupName),
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    MoeinCode = n.Max(x => x.Moein.MoeinCode),
                    MoeinName = n.Max(x => x.Moein.MoeinName),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.Nature),
                    SellerId = n.Max(s => s.SellerId),
                    Id = n.Max(x => x.Moein.KolId),

                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed) - n.Sum(s => s.Bes) : n.Sum(s => s.Bes) - n.Sum(s => s.Bed),
                    MandehNature = Convert.ToInt16((n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? 1 : ((n.Sum(s => s.Bed) < n.Sum(s => s.Bes)) ? 2 : 3)),
                    MandehBed = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed - s.Bes) : 0,
                    MandehBes = (n.Sum(s => s.Bes) > n.Sum(s => s.Bed)) ? n.Sum(s => s.Bes - s.Bed) : 0,

                }).ToListAsync();

            return accounts;
        }
        public async Task<List<DocArticleDto>> Report_BrowserArticlesAsync(DocFilterDto filter)
        {
            List<DocArticleDto> accounts = new List<DocArticleDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.PeriodId == filter.PeriodId
                 )
                  .OrderBy(n => n.Moein.MoeinKol.KolGroup.Order).ThenBy(n => n.Moein.MoeinKol.KolCode)
                .ThenBy(n => n.Moein.MoeinCode).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber)
                .AsQueryable();

            if (filter.MoeinId != null)
            {
                query = query.Where(n => n.MoeinId == filter.MoeinId);
            }
            switch (filter.ReportLevel)
            {
                case 5:
                    query = query.Where(n => n.Tafsil4Id == filter.CurrentTafsilId);
                    break;
                case 6:
                    query = query.Where(n =>
                    n.Tafsil4Id == filter.tafsil4Id
                    && n.Tafsil5Id == filter.CurrentTafsilId);
                    break;
                case 7:
                    query = query.Where(n =>
                    n.Tafsil4Id == filter.tafsil4Id
                    && n.Tafsil5Id == filter.tafsil5Id
                    && n.Tafsil6Id == filter.CurrentTafsilId);
                    break;
                case 8:
                    query = query.Where(n =>
                    n.Tafsil4Id == filter.tafsil4Id
                    && n.Tafsil5Id == filter.tafsil5Id
                    && n.Tafsil6Id == filter.tafsil6Id
                    && n.Tafsil7Id == filter.CurrentTafsilId);
                    break;
                default:
                    break;
            }

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                DateTime startdate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= startdate);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                DateTime endDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= endDate);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description) || n.Comment.Contains(filter.Description));

            if (filter.MoeinIds != null && filter.MoeinIds.Count > 0)
                query = query.Where(n => filter.MoeinIds.Contains(n.MoeinId));

            var list = await query.OrderBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber).ThenBy(n => n.RowNumber).ToListAsync();

            long totalBed = 0;
            long totalBes = 0;

            foreach (var n in list)
            {
                DocArticleDto art = new DocArticleDto();
                art.Id = n.Id;
                art.DocId = n.DocId;
                art.DocDate = n.Doc.DocDate;
                art.strDocDate = n.Doc.DocDate.LatinToPersian();
                art.DocNumber = n.Doc.DocNumber;
                art.Atf = n.Doc.AtfNumber;
                art.AutoNumber = n.Doc.AutoDocNumber;
                art.PeriodId = n.Doc.PeriodId;
                art.SellerId = n.SellerId;
                art.RowNumber = n.RowNumber;
                art.Comment = n.Comment;
                art.ArchiveCode = n.ArchiveCode;
                art.GroupName = n.Moein.MoeinKol.KolGroup.GroupName;
                art.KolId = n.KolId;
                art.KolName = n.Moein.MoeinKol.KolName;
                art.MoeinId = n.MoeinId;
                art.MoeinCode = n.Moein.MoeinCode;
                art.MoeinName = n.Moein.MoeinName;
                art.Tafsil4Id = n.Tafsil4Id;
                art.Tafsil4Name = n.Tafsil4Name;
                art.Tafsil5Id = n.Tafsil5Id;
                art.Tafsil5Name = n.Tafsil5Name;
                art.Tafsil6Id = n.Tafsil6Id;
                art.Tafsil6Name = n.Tafsil6Name;
                art.Tafsil7Id = n.Tafsil7Id;
                art.Tafsil7Name = n.Tafsil7Name;
                art.Tafsil8Id = n.Tafsil8Id;
                art.Tafsil8Name = n.Tafsil8Name;

                art.CreateDate = n.CreateDate;
                art.CreatorUserName = n.CreatorUserName;
                art.LastUpdateDate = n.LastUpdateDate;
                art.EditorUserName = n.EditorUserName;

                art.Bed = n.Bed;
                art.Bes = n.Bes;
                art.Amount = n.Amount;

                totalBed += n.Bed;
                totalBes += n.Bes;
                art.BedBalance = totalBed > totalBes ? totalBed - totalBes : 0;
                art.BesBalance = totalBed < totalBes ? totalBes - totalBed : 0;

                accounts.Add(art);

            }

            return accounts;
        }
        public async Task<List<DocArticleDto>> Report_ArticlesAsync(DocFilterDto filter)
        {
            List<DocArticleDto> accounts = new List<DocArticleDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.PeriodId == filter.PeriodId
                 ).OrderBy(n => n.Doc.DocNumber)
                .AsQueryable();

            if (filter.targetId != null)
            {
                query = query.Where(n => n.MoeinId == filter.targetId);
            }
            if (filter.tafsilId != null)
            {
                query = query.Where(n => n.Tafsil4Id == filter.tafsilId);
            }
            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description) || n.Comment.Contains(filter.Description));

            if (filter.MoeinIds != null && filter.MoeinIds.Count > 0)
                query = query.Where(n => filter.MoeinIds.Contains(n.MoeinId));

            accounts = await query
                .Select(n => new DocArticleDto
                {
                    Id = n.Id,
                    DocId = n.DocId,
                    DocDate = n.Doc.DocDate,
                    DocNumber = n.Doc.DocNumber,
                    Atf = n.Doc.AtfNumber,
                    AutoNumber = n.Doc.AutoDocNumber,
                    PeriodId = n.Doc.PeriodId,
                    SellerId = n.SellerId,

                    RowNumber = n.RowNumber,
                    Comment = n.Comment,
                    ArchiveCode = n.ArchiveCode,

                    GroupName = n.Moein.MoeinKol.KolGroup.GroupName,
                    KolId = n.KolId,
                    KolName = n.Moein.MoeinKol.KolName,
                    MoeinId = n.MoeinId,
                    MoeinCode = n.Moein.MoeinCode,
                    MoeinName = n.Moein.MoeinName,

                    Tafsil4Id = n.Tafsil4Id,
                    Tafsil4Name = n.Tafsil4Name,
                    Tafsil5Id = n.Tafsil5Id,
                    Tafsil5Name = n.Tafsil5Name,
                    Tafsil6Id = n.Tafsil6Id,
                    Tafsil6Name = n.Tafsil6Name,
                    Tafsil7Id = n.Tafsil7Id,
                    Tafsil7Name = n.Tafsil7Name,
                    Tafsil8Id = n.Tafsil8Id,
                    Tafsil8Name = n.Tafsil8Name,

                    Bed = n.Bed,
                    Bes = n.Bes,
                    Amount = n.Amount,

                    CreateDate = n.CreateDate,
                    CreatorUserName = n.CreatorUserName,
                    LastUpdateDate = n.LastUpdateDate,
                    EditorUserName = n.EditorUserName,

                }).OrderBy(n => n.DocNumber).ToListAsync();

            return accounts;
        }
        public async Task<List<DocArticleDto>> GetArticlesAsync(DocFilterDto filter)
        {
            List<DocArticleDto> accounts = new List<DocArticleDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.PeriodId == filter.PeriodId
                 ).OrderBy(n => n.Doc.DocNumber)
                .AsQueryable();

            if (filter.KolId != null)
            {
                query = query.Where(n => n.Moein.KolId == filter.KolId);
            }
            if (filter.MoeinIds?.Count > 0)
            {
                query = query.Where(n => filter.MoeinIds.Contains(n.MoeinId));
            }
            if (filter.Tafsil4Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil4Ids.Contains(n.Tafsil4Id));
            }
            if (filter.Tafsil5Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil5Ids.Contains(n.Tafsil5Id));
            }
            if (filter.Tafsil6Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil6Ids.Contains(n.Tafsil6Id));
            }
            if (filter.Tafsil7Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil7Ids.Contains(n.Tafsil7Id));
            }
            if (filter.Tafsil8Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil8Ids.Contains(n.Tafsil8Id));
            }

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description) || n.Comment.Contains(filter.Description));

            if (!string.IsNullOrEmpty(filter.strAmount))
            {
                try
                {
                    long amount = Convert.ToInt64(filter.strAmount.Replace(",", ""));
                    if (amount > 0)
                    {
                        if (filter.SearchAmountFild == 3)
                            query = query.Where(n => n.Bed == amount || n.Bes == amount);
                        else if (filter.SearchAmountFild == 1)
                            query = query.Where(n => n.Bed == amount);
                        else if (filter.SearchAmountFild == 2)
                            query = query.Where(n => n.Bes == amount);
                    }
                }
                catch { }

            }

            accounts = await query
                .Select(n => new DocArticleDto
                {
                    Id = n.Id,
                    DocId = n.DocId,
                    DocTypeId = n.Doc.TypeId,
                    DocDate = n.Doc.DocDate,
                    strDocDate = n.Doc.DocDate.LatinToPersian(),
                    DocNumber = n.Doc.DocNumber,
                    Atf = n.Doc.AtfNumber,
                    AutoNumber = n.Doc.AutoDocNumber,
                    PeriodId = n.Doc.PeriodId,
                    SellerId = n.SellerId,

                    RowNumber = n.RowNumber,
                    Comment = n.Comment,
                    ArchiveCode = n.ArchiveCode,

                    GroupName = n.Moein.MoeinKol.KolGroup.GroupName,
                    KolId = n.KolId,
                    KolName = n.Moein.MoeinKol.KolName,
                    KolCode = n.Moein.MoeinKol.KolCode,
                    MoeinId = n.MoeinId,
                    MoeinCode = n.Moein.MoeinCode,
                    MoeinName = n.Moein.MoeinName,

                    Tafsil4Id = n.Tafsil4Id,
                    Tafsil4Name = n.Tafsil4Name,
                    Tafsil5Id = n.Tafsil5Id,
                    Tafsil5Name = n.Tafsil5Name,
                    Tafsil6Id = n.Tafsil6Id,
                    Tafsil6Name = n.Tafsil6Name,
                    Tafsil7Id = n.Tafsil7Id,
                    Tafsil7Name = n.Tafsil7Name,
                    Tafsil8Id = n.Tafsil8Id,
                    Tafsil8Name = n.Tafsil8Name,

                    Bed = n.Bed,
                    Bes = n.Bes,
                    Amount = n.Amount,

                    CreateDate = n.CreateDate,
                    CreatorUserName = n.CreatorUserName,
                    LastUpdateDate = n.LastUpdateDate,
                    EditorUserName = n.EditorUserName,

                }).OrderBy(n => n.DocNumber).ToListAsync();

            return accounts;
        }
        public IQueryable<DocArticleDto> GetArticlesQuery(DocFilterDto filter)
        {
            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.PeriodId == filter.PeriodId
                 ).OrderBy(n => n.Doc.DocNumber)
                .AsQueryable();

            if (filter.KolId != null)
            {
                query = query.Where(n => n.Moein.KolId == filter.KolId);
            }

            if (filter.KolsId?.Count > 0)
                query = query.Where(n => filter.KolsId.Contains(n.Moein.KolId));

            if (filter.MoeinIds?.Count > 0)
            {
                query = query.Where(n => filter.MoeinIds.Contains(n.MoeinId));
            }
            if (filter.Tafsil4Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil4Ids.Contains(n.Tafsil4Id));
            }
            if (filter.Tafsil5Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil5Ids.Contains(n.Tafsil5Id));
            }
            if (filter.Tafsil6Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil6Ids.Contains(n.Tafsil6Id));
            }
            if (filter.Tafsil7Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil7Ids.Contains(n.Tafsil7Id));
            }
            if (filter.Tafsil8Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil8Ids.Contains(n.Tafsil8Id));
            }

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description) || n.Comment.Contains(filter.Description));

            if (!string.IsNullOrEmpty(filter.strAmount))
            {
                try
                {
                    long amount = Convert.ToInt64(filter.strAmount.Replace(",", ""));
                    if (amount > 0)
                    {
                        if (filter.SearchAmountFild == 3)
                            query = query.Where(n => n.Bed == amount || n.Bes == amount);
                        else if (filter.SearchAmountFild == 1)
                            query = query.Where(n => n.Bed == amount);
                        else if (filter.SearchAmountFild == 2)
                            query = query.Where(n => n.Bes == amount);
                    }
                }
                catch { }

            }

            return query.Select(n => new DocArticleDto
            {
                Id = n.Id,
                DocId = n.DocId,
                DocTypeId = n.Doc.TypeId,
                DocDate = n.Doc.DocDate,
                DocNumber = n.Doc.DocNumber,
                PeriodId = n.Doc.PeriodId,
                SellerId = n.SellerId,

                RowNumber = n.RowNumber,
                Comment = n.Comment,
                ArchiveCode = n.ArchiveCode,

                GroupName = n.Moein.MoeinKol.KolGroup.GroupName,
                KolId = n.KolId,
                KolName = n.Moein.MoeinKol.KolName,
                MoeinId = n.MoeinId,
                MoeinCode = n.Moein.MoeinCode,
                MoeinName = n.Moein.MoeinName,

                Tafsil4Id = n.Tafsil4Id,
                Tafsil4Name = n.Tafsil4Name,
                Tafsil5Id = n.Tafsil5Id,
                Tafsil5Name = n.Tafsil5Name,
                Tafsil6Id = n.Tafsil6Id,
                Tafsil6Name = n.Tafsil6Name,
                Tafsil7Id = n.Tafsil7Id,
                Tafsil7Name = n.Tafsil7Name,
                Tafsil8Id = n.Tafsil8Id,
                Tafsil8Name = n.Tafsil8Name,

                Bed = n.Bed,
                Bes = n.Bes,
                Amount = n.Amount,

                CreateDate = n.CreateDate,
                CreatorUserName = n.CreatorUserName,
                LastUpdateDate = n.LastUpdateDate,
                EditorUserName = n.EditorUserName,

            }).OrderBy(n => n.DocNumber).AsQueryable();
        }
        public async Task<List<DocArticleDto>> GetSimpleArticlesAsync(DocFilterDto filter)
        {
            List<DocArticleDto> accounts = new List<DocArticleDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.PeriodId == filter.PeriodId
                 )
                .AsQueryable();

            if (filter.KolId != null)
            {
                query = query.Where(n => n.Moein.KolId == filter.KolId);
            }
            if (filter.MoeinIds?.Count > 0)
            {
                query = query.Where(n => filter.MoeinIds.Contains(n.MoeinId));
            }
            if (filter.Tafsil4Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil4Ids.Contains(n.Tafsil4Id));
            }
            if (filter.Tafsil5Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil5Ids.Contains(n.Tafsil5Id));
            }
            if (filter.Tafsil6Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil6Ids.Contains(n.Tafsil6Id));
            }
            if (filter.Tafsil7Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil7Ids.Contains(n.Tafsil7Id));
            }
            if (filter.Tafsil8Ids?.Count > 0)
            {
                query = query.Where(n => filter.Tafsil8Ids.Contains(n.Tafsil8Id));
            }

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description) || n.Comment.Contains(filter.Description));

            if (!string.IsNullOrEmpty(filter.strAmount))
            {
                try
                {
                    long amount = Convert.ToInt64(filter.strAmount.Replace(",", ""));
                    if (amount > 0)
                    {
                        if (filter.SearchAmountFild == 3)
                            query = query.Where(n => n.Bed == amount || n.Bes == amount);
                        else if (filter.SearchAmountFild == 1)
                            query = query.Where(n => n.Bed == amount);
                        else if (filter.SearchAmountFild == 2)
                            query = query.Where(n => n.Bes == amount);
                    }
                }
                catch { }

            }

            accounts = await query
                .Select(n => new DocArticleDto
                {
                    DocDate = n.Doc.DocDate,
                    DocNumber = n.Doc.DocNumber,

                    RowNumber = n.RowNumber,
                    Comment = n.Comment,
                    strBed = n.Doc.DocDate.LatinToPersian(),
                    KolName = n.Moein.MoeinKol.KolName,
                    KolCode = n.Moein.MoeinKol.KolCode,
                    MoeinCode = n.Moein.MoeinCode,
                    MoeinName = n.Moein.MoeinName,

                    Tafsil4Id = n.Tafsil4Id.HasValue ? n.Tafsil4Id : 369,
                    Tafsil4Name = string.IsNullOrEmpty(n.Tafsil4Name) ? n.Moein.MoeinName : n.Tafsil4Name,

                    Bed = n.Bed,
                    Bes = n.Bes,
                    Amount = n.Amount,

                }).OrderBy(n => n.DocDate).ThenBy(n => n.DocNumber).ThenBy(n => n.RowNumber).ToListAsync();

            return accounts;
        }
        //گزارش کردش حساب ها
        public async Task<List<VmTurnoverAccount>> TurnoverAccounts_TafsilAsync(DocFilterDto filter)
        {
            List<VmTurnoverAccount> report = new List<VmTurnoverAccount>();
            var allData = GetArticlesQuery(filter);
            var kols = await _baseData.GetUsedKolsAsync(filter.SellerId);

            foreach (var kol in kols)
            {
                var artsBykol = allData.Where(n => n.KolId == kol.Id);
                List<int> moeinsId = await artsBykol.Select(n => n.MoeinId).Distinct().ToListAsync();
                foreach (var moein in moeinsId)
                {

                    var artbyMoein = artsBykol.Where(n => n.MoeinId == moein);
                    List<long?> tIds = await artbyMoein.Select(n => n.Tafsil4Id).Distinct().ToListAsync();
                    foreach (var tid in tIds)
                    {

                        var articles = await artbyMoein.Where(n => n.Tafsil4Id == tid).ToListAsync();
                        var headerData = articles.FirstOrDefault();

                        var n = new VmTurnoverAccount();
                        n.AccountCode = headerData?.MoeinCode + "-" + headerData?.Tafsil4Id;
                        n.KolName = headerData?.KolName;
                        n.MoeinName = headerData?.MoeinName;
                        n.TafsilName = headerData?.Tafsil4Name;
                        n.TotalBed = articles.Sum(n => n.Bed);
                        n.TotalBes = articles.Sum(n => n.Bes);

                        long totalBed = 0;
                        long totalBes = 0;
                        long balance = 0;
                        string status = "-";

                        List<TurnoverAccount_Items> items = new List<TurnoverAccount_Items>();
                        foreach (var x in articles)
                        {
                            TurnoverAccount_Items item = new TurnoverAccount_Items();
                            item.DocNumber = x.DocNumber.Value;
                            item.DocDate = x.DocDate.Value;
                            item.Description = x.Comment;
                            item.Bed = x.Bed;
                            item.Bes = x.Bes;

                            totalBed += x.Bed;
                            totalBes += x.Bes;
                            if (totalBed > totalBes)
                            {
                                status = "بد";
                                balance = totalBed - totalBes;
                                item.Balance = balance;
                            }
                            else if (totalBes > totalBed)
                            {
                                status = "بس";
                                balance = totalBes - totalBed;
                                item.Balance = balance;
                            }
                            else
                            {
                                status = "-";
                                balance = totalBes - totalBed;
                                item.Balance = balance;
                            }

                            items.Add(item);
                        }
                        n.Items.AddRange(items);
                        report.Add(n);
                    }
                }
            }

            return report;
        }
        public async Task<List<VmTurnoverAccount>> TurnoverAccounts_MoeinAsync(DocFilterDto filter)
        {
            List<VmTurnoverAccount> report = new List<VmTurnoverAccount>();
            var allData = GetArticlesQuery(filter);
            var kols = await _baseData.GetUsedKolsAsync(filter.SellerId);

            foreach (var kol in kols)
            {
                var artsBykol = allData.Where(n => n.KolId == kol.Id);
                List<int> moeinsId = await artsBykol.Select(n => n.MoeinId).Distinct().ToListAsync();
                foreach (var moein in moeinsId)
                {

                    var articles = await artsBykol.Where(n => n.MoeinId == moein).ToListAsync();
                    var headerData = articles.FirstOrDefault();

                    var n = new VmTurnoverAccount();
                    n.AccountCode = headerData?.MoeinCode + "-" + headerData?.Tafsil4Id;
                    n.KolName = headerData?.KolName;
                    n.MoeinName = headerData?.MoeinName;
                    n.TotalBed = articles.Sum(n => n.Bed);
                    n.TotalBes = articles.Sum(n => n.Bes);

                    long totalBed = 0;
                    long totalBes = 0;
                    long balance = 0;
                    string status = "-";

                    List<TurnoverAccount_Items> items = new List<TurnoverAccount_Items>();
                    foreach (var x in articles)
                    {
                        TurnoverAccount_Items item = new TurnoverAccount_Items();
                        item.DocNumber = x.DocNumber.Value;
                        item.DocDate = x.DocDate.Value;
                        item.Description = x.Comment;
                        item.Bed = x.Bed;
                        item.Bes = x.Bes;

                        totalBed += x.Bed;
                        totalBes += x.Bes;
                        if (totalBed > totalBes)
                        {
                            status = "بد";
                            balance = totalBed - totalBes;
                            item.Balance = balance;
                        }
                        else if (totalBes > totalBed)
                        {
                            status = "بس";
                            balance = totalBes - totalBed;
                            item.Balance = balance;
                        }
                        else
                        {
                            status = "-";
                            balance = totalBes - totalBed;
                            item.Balance = balance;
                        }

                        items.Add(item);
                    }
                    n.Items.AddRange(items);
                    report.Add(n);
                }
            }

            return report;
        }
        //تراز آزمایشی
        public async Task<List<TarazAzmayeshiDto>> GetTrialBalanceAsync(DocFilterDto filter)
        {
            var accounts = new List<Report_BrowserDto>();
            if (filter.ReportLevel == null)
                filter.ReportLevel = 1;
            if (filter.ReportLevel == 1)
                accounts = await Report_KolAsync(filter);
            else if (filter.ReportLevel == 2)
                accounts = await Report_MoeinAsync(filter);
            else if (filter.ReportLevel == 3)
                accounts = await GetTafsil4TrialBalanceAsync(filter);

            List<TarazAzmayeshiDto> trialBalanceList = new List<TarazAzmayeshiDto>();

            foreach (var account in accounts)
            {
                var tarazAzmayeshiDto = new TarazAzmayeshiDto
                {
                    BalanceLevel = filter.ReportLevel,
                    AccountKolName = account.KolName,
                    AccountKolCode = account.KolCode,
                    AccountMoeinName = account.MoeinName,
                    AccountMoeinCode = account.MoeinCode,
                    AccountTafsil4Name = account.Tafsil4Name,
                    AccountNature = account.Nature,
                    TotalBed = account.Bed,
                    TotalBes = account.Bes,
                    MandehBed = account.MandehNature == 1 ? account.Mandeh : 0,
                    MandehBes = account.MandehNature == 2 ? account.Mandeh : 0,
                    AvalDoreh = 0,
                    PayanDore = 0,
                    BalanceNature = account.MandehNature,
                    IsMatch = (account.MandehNature != account.Nature && account.Nature != 3) ? false : true,
                };

                trialBalanceList.Add(tarazAzmayeshiDto);
            }

            return trialBalanceList;
        }
        public async Task<List<TarazAzmayeshiDto>> GetTrialBalance_6ColAsync(DocFilterDto filter)
        {
            if (filter.ReportLevel == 1 || filter.ReportLevel == null)
                return await GetTrialBalanceKol_6Col_Async(filter);
            else if (filter.ReportLevel == 2)
                return await GetTrialBalanceMoein_6Col_Async(filter);
            else
                return await GetTrialBalanceTafsil_6Col_Async(filter);
        }
        public async Task<List<TrialBalancePrintDto>> GetTrialBalanceForPrintAsync(DocFilterDto filter)
        {
            var accounts = new List<Report_BrowserDto>();
            if (filter.ReportLevel == null)
                filter.ReportLevel = 1;
            if (filter.ReportLevel == 1)
                accounts = await Report_KolAsync(filter);
            else if (filter.ReportLevel == 2)
                accounts = await Report_MoeinAsync(filter);
            else if (filter.ReportLevel == 3)
                accounts = await GetTafsil4TrialBalanceAsync(filter);

            List<TrialBalancePrintDto> trialBalanceList = new List<TrialBalancePrintDto>();

            foreach (var account in accounts)
            {
                TrialBalancePrintDto x = new TrialBalancePrintDto();
                x.AccountName = account.KolCode + "-" + account.KolName;
                x.AccountNature = account.Nature.AccToNatureName();
                if (filter.ReportLevel == 2)
                    x.AccountName = account.MoeinCode + "-" + account.MoeinName;
                if (filter.ReportLevel == 3)
                    x.AccountName = account.MoeinCode + "-" + account.MoeinName;
                if (filter.ReportLevel == 3 && !string.IsNullOrEmpty(account.Tafsil4Name))
                    x.AccountName += "-" + account.Tafsil4Name;
                x.TotalBed = account.Bed;
                x.TotalBes = account.Bes;
                x.MandehBed = account.MandehNature == 1 ? account.Mandeh : 0;
                x.MandehBes = account.MandehNature == 2 ? account.Mandeh : 0;
                x.KolName = account.KolName;
                x.MoeinName = account.MoeinName;
                x.TafsilName = account.Tafsil4Name;
                trialBalanceList.Add(x);
            }

            return trialBalanceList;
        }
        public async Task<List<TarazAzmayeshiDto>> GetTrialBalanceKol_6Col_Async(DocFilterDto filter)
        {
            var trialBalanceList = new List<TarazAzmayeshiDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n => !n.IsDeleted && !n.Doc.IsDeleted && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId);

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);

            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }

            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);

            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            // حساب‌های اول دوره
            var startAccounts = await query
                .Where(n => n.Doc.DocNumber == 1 || n.Doc.TypeId == 2)
                .GroupBy(n => n.Moein.KolId)
                .Select(n => new Report_BrowserDto
                {
                    Id = n.Key,
                    KolId = n.Key,
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.MoeinKol.Nature),
                    TypeId = n.Max(x => x.Moein.MoeinKol.TypeId),
                    SellerId = n.Max(s => s.SellerId),
                    KolNature = n.Max(x => x.Moein.MoeinKol.Nature),
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = Math.Abs(n.Sum(s => s.Bed - s.Bes)),
                    MandehNature = Convert.ToInt16(n.Sum(s => s.Bed) > n.Sum(s => s.Bes) ? 1 : n.Sum(s => s.Bed) < n.Sum(s => s.Bes) ? 2 : 3)
                })
                .OrderBy(n => n.KolCode)
                .ToListAsync();

            // حساب‌های طی دوره
            var periodAccounts = await query
                .Where(n => n.Doc.DocNumber > 1 && n.Doc.TypeId != 2)
                .GroupBy(n => n.Moein.KolId)
                .Select(n => new Report_BrowserDto
                {
                    Id = n.Key,
                    KolId = n.Key,
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.MoeinKol.Nature),
                    TypeId = n.Max(x => x.Moein.MoeinKol.TypeId),
                    SellerId = n.Max(s => s.SellerId),
                    KolNature = n.Max(x => x.Moein.MoeinKol.Nature),
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = Math.Abs(n.Sum(s => s.Bed - s.Bes)),
                    MandehNature = Convert.ToInt16(n.Sum(s => s.Bed) > n.Sum(s => s.Bes) ? 1 : n.Sum(s => s.Bed) < n.Sum(s => s.Bes) ? 2 : 3)
                })
                .OrderBy(n => n.KolCode)
                .ToListAsync();

            // تبدیل لیست‌ها به دیکشنری برای دسترسی سریع‌تر
            var startAccountsDict = startAccounts.ToDictionary(a => a.KolId);
            var periodAccountsDict = periodAccounts.ToDictionary(a => a.KolId);

            // ترکیب حساب‌های گردش طی دوره با حساب‌های اول دوره
            foreach (var account in periodAccounts)
            {
                var tarazAzmayeshiDto = new TarazAzmayeshiDto
                {
                    BalanceLevel = filter.ReportLevel,
                    AccountKolName = account.KolName,
                    AccountKolCode = account.KolCode,
                    AccountMoeinName = account.MoeinName,
                    AccountMoeinCode = account.MoeinCode,
                    AccountTafsil4Name = account.Tafsil4Name,
                    AccountNature = account.Nature,
                    TotalBed = account.Bed,
                    TotalBes = account.Bes,
                    MandehBed = account.MandehNature == 1 ? account.Mandeh : 0,
                    MandehBes = account.MandehNature == 2 ? account.Mandeh : 0,
                    AvalDoreh = 0,
                    PayanDore = 0,
                    BalanceNature = account.MandehNature,
                    IsMatch = (account.MandehNature != account.Nature && account.Nature != 3) ? false : true,
                    StartBed = 0,
                    StartBes = 0
                };

                if (startAccountsDict.TryGetValue(account.KolId, out var startAccount))
                {
                    tarazAzmayeshiDto.StartBed = startAccount.Bed;
                    tarazAzmayeshiDto.StartBes = startAccount.Bes;

                    long totalBed = tarazAzmayeshiDto.StartBed + tarazAzmayeshiDto.TotalBed;
                    long totalBes = tarazAzmayeshiDto.StartBes + tarazAzmayeshiDto.TotalBes;
                    long mandeh = totalBed > totalBes ? totalBed - totalBes : totalBes - totalBed;
                    tarazAzmayeshiDto.BalanceNature = totalBed > totalBes ? (Int16)1 : (totalBes > totalBed ? (Int16)2 : (Int16)3);
                    tarazAzmayeshiDto.MandehBed = tarazAzmayeshiDto.BalanceNature == 1 ? mandeh : 0;
                    tarazAzmayeshiDto.MandehBes = tarazAzmayeshiDto.BalanceNature == 2 ? mandeh : 0;
                }

                trialBalanceList.Add(tarazAzmayeshiDto);
            }

            // اضافه کردن حساب‌هایی که در طی دوره گردش نداشته‌اند
            foreach (var account in startAccounts)
            {
                if (!periodAccountsDict.ContainsKey(account.KolId))
                {
                    var tarazAzmayeshiDto = new TarazAzmayeshiDto
                    {
                        BalanceLevel = filter.ReportLevel,
                        AccountKolName = account.KolName,
                        AccountKolCode = account.KolCode,
                        AccountMoeinName = account.MoeinName,
                        AccountMoeinCode = account.MoeinCode,
                        AccountTafsil4Name = account.Tafsil4Name,
                        AccountNature = account.Nature,
                        TotalBed = 0,
                        TotalBes = 0,
                        MandehBed = account.MandehNature == 1 ? account.Mandeh : 0,
                        MandehBes = account.MandehNature == 2 ? account.Mandeh : 0,
                        AvalDoreh = 0,
                        PayanDore = 0,
                        BalanceNature = account.MandehNature,
                        IsMatch = (account.MandehNature != account.Nature && account.Nature != 3) ? false : true,
                        StartBed = account.Bed,
                        StartBes = account.Bes
                    };

                    trialBalanceList.Add(tarazAzmayeshiDto);
                }
            }

            return trialBalanceList.OrderBy(n => n.AccountKolCode).ToList();
        }
        public async Task<List<TarazAzmayeshiDto>> GetTrialBalanceMoein_6Col_Async(DocFilterDto filter)
        {
            var trialBalanceList = new List<TarazAzmayeshiDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n => !n.IsDeleted && !n.Doc.IsDeleted && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId);

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);

            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }

            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);

            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            // حساب‌های اول دوره
            var startAccounts = await query
                .Where(n => n.Doc.DocNumber == 1 || n.Doc.TypeId == 2)
                .GroupBy(n => n.MoeinId)
                .Select(n => new Report_BrowserDto
                {
                    Id = n.Key,
                    KolId = n.Max(x => x.Moein.KolId),
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    MoeinId = n.Key,
                    MoeinCode = n.Max(x => x.Moein.MoeinCode),
                    MoeinName = n.Max(x => x.Moein.MoeinName),
                    MoeinNature = n.Max(x => x.Moein.Nature),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.Nature),
                    TypeId = n.Max(x => x.Moein.MoeinKol.TypeId),
                    SellerId = n.Max(s => s.SellerId),
                    KolNature = n.Max(x => x.Moein.MoeinKol.Nature),
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = Math.Abs(n.Sum(s => s.Bed - s.Bes)),
                    MandehNature = Convert.ToInt16(n.Sum(s => s.Bed) > n.Sum(s => s.Bes) ? 1 : n.Sum(s => s.Bed) < n.Sum(s => s.Bes) ? 2 : 3)

                })
                .OrderBy(n => n.MoeinCode)
                .ToListAsync();

            // حساب‌های طی دوره
            var periodAccounts = await query
                .Where(n => n.Doc.DocNumber > 1 && n.Doc.TypeId != 2)
                .GroupBy(n => n.MoeinId)
                .Select(n => new Report_BrowserDto
                {
                    Id = n.Key,
                    KolId = n.Max(x => x.Moein.KolId),
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    MoeinId = n.Key,
                    MoeinCode = n.Max(x => x.Moein.MoeinCode),
                    MoeinName = n.Max(x => x.Moein.MoeinName),
                    MoeinNature = n.Max(x => x.Moein.Nature),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.Nature),
                    TypeId = n.Max(x => x.Moein.MoeinKol.TypeId),
                    SellerId = n.Max(s => s.SellerId),
                    KolNature = n.Max(x => x.Moein.MoeinKol.Nature),
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = Math.Abs(n.Sum(s => s.Bed - s.Bes)),
                    MandehNature = Convert.ToInt16(n.Sum(s => s.Bed) > n.Sum(s => s.Bes) ? 1 : n.Sum(s => s.Bed) < n.Sum(s => s.Bes) ? 2 : 3)

                })
                .OrderBy(n => n.MoeinCode)
                .ToListAsync();

            // تبدیل لیست‌ها به دیکشنری برای دسترسی سریع‌تر
            var startAccountsDict = startAccounts.ToDictionary(a => a.MoeinId);
            var periodAccountsDict = periodAccounts.ToDictionary(a => a.MoeinId);

            // ترکیب حساب‌های گردش طی دوره با حساب‌های اول دوره
            foreach (var account in periodAccounts)
            {
                var tarazAzmayeshiDto = new TarazAzmayeshiDto
                {
                    BalanceLevel = filter.ReportLevel,
                    AccountKolName = account.KolName,
                    AccountKolCode = account.KolCode,
                    AccountMoeinName = account.MoeinName,
                    AccountMoeinCode = account.MoeinCode,
                    AccountTafsil4Name = account.Tafsil4Name,
                    AccountNature = account.Nature,
                    TotalBed = account.Bed,
                    TotalBes = account.Bes,
                    MandehBed = account.MandehNature == 1 ? account.Mandeh : 0,
                    MandehBes = account.MandehNature == 2 ? account.Mandeh : 0,
                    AvalDoreh = 0,
                    PayanDore = 0,
                    BalanceNature = account.MandehNature,
                    IsMatch = (account.MandehNature != account.Nature && account.Nature != 3) ? false : true,
                    StartBed = 0,
                    StartBes = 0
                };

                if (startAccountsDict.TryGetValue(account.MoeinId, out var startAccount))
                {
                    tarazAzmayeshiDto.StartBed = startAccount.Bed;
                    tarazAzmayeshiDto.StartBes = startAccount.Bes;


                    long totalBed = tarazAzmayeshiDto.StartBed + tarazAzmayeshiDto.TotalBed;
                    long totalBes = tarazAzmayeshiDto.StartBes + tarazAzmayeshiDto.TotalBes;
                    long mandeh = totalBed > totalBes ? totalBed - totalBes : totalBes - totalBed;
                    tarazAzmayeshiDto.BalanceNature = totalBed > totalBes ? (Int16)1 : (totalBes > totalBed ? (Int16)2 : (Int16)3);
                    tarazAzmayeshiDto.MandehBed = tarazAzmayeshiDto.BalanceNature == 1 ? mandeh : 0;
                    tarazAzmayeshiDto.MandehBes = tarazAzmayeshiDto.BalanceNature == 2 ? mandeh : 0;
                }

                trialBalanceList.Add(tarazAzmayeshiDto);
            }

            // اضافه کردن حساب‌هایی که در طی دوره گردش نداشته‌اند
            foreach (var account in startAccounts)
            {
                if (!periodAccountsDict.ContainsKey(account.MoeinId))
                {
                    var tarazAzmayeshiDto = new TarazAzmayeshiDto
                    {
                        BalanceLevel = filter.ReportLevel,
                        AccountKolName = account.KolName,
                        AccountKolCode = account.KolCode,
                        AccountMoeinName = account.MoeinName,
                        AccountMoeinCode = account.MoeinCode,
                        AccountTafsil4Name = account.Tafsil4Name,
                        AccountNature = account.Nature,
                        TotalBed = 0,
                        TotalBes = 0,
                        MandehBed = account.MandehNature == 1 ? account.Mandeh : 0,
                        MandehBes = account.MandehNature == 2 ? account.Mandeh : 0,
                        AvalDoreh = 0,
                        PayanDore = 0,
                        BalanceNature = account.MandehNature,
                        IsMatch = (account.MandehNature != account.Nature && account.Nature != 3) ? false : true,
                        StartBed = account.Bed,
                        StartBes = account.Bes
                    };

                    trialBalanceList.Add(tarazAzmayeshiDto);
                }
            }

            return trialBalanceList.OrderBy(n => n.AccountMoeinCode).ToList();
        }
        public async Task<List<TarazAzmayeshiDto>> GetTrialBalanceTafsil_6Col_Async(DocFilterDto filter)
        {
            var trialBalanceList = new List<TarazAzmayeshiDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n => !n.IsDeleted && !n.Doc.IsDeleted && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId);

            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);

            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }

            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);

            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            // حساب‌های اول دوره
            var startAccounts = await query
                .Where(n => n.Doc.DocNumber == 1 || n.Doc.TypeId == 2)
                .GroupBy(n => new { n.MoeinId, n.Tafsil4Id })
                .Select(n => new Report_BrowserDto
                {
                    KolId = n.Max(x => x.Moein.KolId),
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    MoeinId = n.Key.MoeinId,
                    MoeinCode = n.Max(x => x.Moein.MoeinCode),
                    MoeinName = n.Max(x => x.Moein.MoeinName),
                    MoeinNature = n.Max(x => x.Moein.Nature),
                    Tafsil4Id = n.Key.Tafsil4Id,
                    Tafsil4Name = n.Max(x => x.Tafsil4Name),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.Nature),
                    TypeId = n.Max(x => x.Moein.MoeinKol.TypeId),
                    SellerId = n.Max(s => s.SellerId),
                    KolNature = n.Max(x => x.Moein.MoeinKol.Nature),
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = Math.Abs(n.Sum(s => s.Bed - s.Bes)),
                    MandehNature = (short)(n.Sum(s => s.Bed) > n.Sum(s => s.Bes) ? 1 :
                                    n.Sum(s => s.Bed) < n.Sum(s => s.Bes) ? 2 : 3)

                })
                .OrderBy(n => n.MoeinCode)
                .ToListAsync();

            // حساب‌های طی دوره
            var periodAccounts = await query
                .Where(n => n.Doc.DocNumber > 1 && n.Doc.TypeId != 2)
                .GroupBy(n => new { n.MoeinId, n.Tafsil4Id })
                .Select(n => new Report_BrowserDto
                {
                    KolId = n.Max(x => x.Moein.KolId),
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    MoeinId = n.Key.MoeinId,
                    MoeinCode = n.Max(x => x.Moein.MoeinCode),
                    MoeinName = n.Max(x => x.Moein.MoeinName),
                    MoeinNature = n.Max(x => x.Moein.Nature),
                    Tafsil4Id = n.Key.Tafsil4Id,
                    Tafsil4Name = n.Max(x => x.Tafsil4Name),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.Nature),
                    TypeId = n.Max(x => x.Moein.MoeinKol.TypeId),
                    SellerId = n.Max(s => s.SellerId),
                    KolNature = n.Max(x => x.Moein.MoeinKol.Nature),
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = Math.Abs(n.Sum(s => s.Bed - s.Bes)),
                    MandehNature = (short)(n.Sum(s => s.Bed) > n.Sum(s => s.Bes) ? 1 :
                                    n.Sum(s => s.Bed) < n.Sum(s => s.Bes) ? 2 : 3)

                })
                .OrderBy(n => n.MoeinCode)
                .ToListAsync();

            // تبدیل لیست‌ها به دیکشنری با کلید ترکیبی
            var startAccountsDict = startAccounts.ToDictionary(a => (a.MoeinId, a.Tafsil4Id));
            var periodAccountsDict = periodAccounts.ToDictionary(a => (a.MoeinId, a.Tafsil4Id));

            // ترکیب حساب‌های گردش طی دوره با حساب‌های اول دوره
            foreach (var account in periodAccounts)
            {
                var tarazAzmayeshiDto = new TarazAzmayeshiDto
                {
                    BalanceLevel = filter.ReportLevel,
                    AccountKolName = account.KolName,
                    AccountKolCode = account.KolCode,
                    AccountMoeinName = account.MoeinName,
                    AccountMoeinCode = account.MoeinCode,
                    AccountTafsil4Name = account.Tafsil4Name,
                    AccountNature = account.Nature,
                    TotalBed = account.Bed,
                    TotalBes = account.Bes,
                    MandehBed = account.MandehNature == 1 ? account.Mandeh : 0,
                    MandehBes = account.MandehNature == 2 ? account.Mandeh : 0,
                    AvalDoreh = 0,
                    PayanDore = 0,
                    BalanceNature = account.MandehNature,
                    IsMatch = (account.MandehNature != account.Nature && account.Nature != 3) ? false : true,
                    StartBed = 0,
                    StartBes = 0
                };

                if (startAccountsDict.TryGetValue((account.MoeinId, account.Tafsil4Id), out var startAccount))
                {
                    tarazAzmayeshiDto.StartBed = startAccount.Bed;
                    tarazAzmayeshiDto.StartBes = startAccount.Bes;


                    long totalBed = tarazAzmayeshiDto.StartBed + tarazAzmayeshiDto.TotalBed;
                    long totalBes = tarazAzmayeshiDto.StartBes + tarazAzmayeshiDto.TotalBes;
                    long mandeh = totalBed > totalBes ? totalBed - totalBes : totalBes - totalBed;
                    tarazAzmayeshiDto.BalanceNature = totalBed > totalBes ? (Int16)1 : (totalBes > totalBed ? (Int16)2 : (Int16)3);
                    tarazAzmayeshiDto.MandehBed = tarazAzmayeshiDto.BalanceNature == 1 ? mandeh : 0;
                    tarazAzmayeshiDto.MandehBes = tarazAzmayeshiDto.BalanceNature == 2 ? mandeh : 0;
                }

                trialBalanceList.Add(tarazAzmayeshiDto);
            }

            // اضافه کردن حساب‌هایی که در طی دوره گردش نداشته‌اند
            foreach (var account in startAccounts)
            {
                if (!periodAccountsDict.ContainsKey((account.MoeinId, account.Tafsil4Id)))
                {
                    var tarazAzmayeshiDto = new TarazAzmayeshiDto
                    {
                        BalanceLevel = filter.ReportLevel,
                        AccountKolName = account.KolName,
                        AccountKolCode = account.KolCode,
                        AccountMoeinName = account.MoeinName,
                        AccountMoeinCode = account.MoeinCode,
                        AccountTafsil4Name = account.Tafsil4Name,
                        AccountNature = account.Nature,
                        TotalBed = 0,
                        TotalBes = 0,
                        MandehBed = account.MandehNature == 1 ? account.Mandeh : 0,
                        MandehBes = account.MandehNature == 2 ? account.Mandeh : 0,
                        AvalDoreh = 0,
                        PayanDore = 0,
                        BalanceNature = account.MandehNature,
                        IsMatch = (account.MandehNature != account.Nature && account.Nature != 3) ? false : true,
                        StartBed = account.Bed,
                        StartBes = account.Bes
                    };

                    trialBalanceList.Add(tarazAzmayeshiDto);
                }
            }

            return trialBalanceList.OrderBy(n => n.AccountMoeinCode).ToList();
        }
        public async Task<List<TrialBalancePrintDto>> GetTrialBalance6ForPrintAsync(DocFilterDto filter)
        {
            List<TrialBalancePrintDto> trialBalanceList = new List<TrialBalancePrintDto>();
            List<TarazAzmayeshiDto> report = new List<TarazAzmayeshiDto>();
            if (filter.ReportLevel == 1 || filter.ReportLevel == null)
                report = await GetTrialBalanceKol_6Col_Async(filter);
            else if (filter.ReportLevel == 2)
                report = await GetTrialBalanceMoein_6Col_Async(filter);
            else if (filter.ReportLevel == 3)
                report = await GetTrialBalanceTafsil_6Col_Async(filter);
            else
                return trialBalanceList;

            foreach (var account in report)
            {
                TrialBalancePrintDto x = new TrialBalancePrintDto();
                x.TotalBed = account.TotalBed;
                x.TotalBes = account.TotalBes;
                x.MandehBed = account.MandehBed;
                x.MandehBes = account.MandehBes;
                x.KolName = account.AccountKolName;
                x.MoeinName = account.AccountMoeinName;
                x.TafsilName = account.AccountTafsil4Name;
                x.StartBed = account.StartBed;
                x.StartBes = account.StartBes;
                trialBalanceList.Add(x);
            }

            return trialBalanceList;
        }
        public async Task<List<Report_BrowserDto>> GetTafsil4TrialBalanceAsync(DocFilterDto filter)
        {
            List<Report_BrowserDto> accounts = new List<Report_BrowserDto>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.PeriodId == filter.PeriodId
                ).AsQueryable();


            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            accounts = await query.GroupBy(n => new { n.Tafsil4Id, n.MoeinId })
                .Select(n => new Report_BrowserDto
                {
                    Tafsil4Id = n.Key.Tafsil4Id,
                    Tafsil4Name = n.Max(x => x.Tafsil4Name),
                    MoeinId = n.Key.MoeinId,
                    GroupId = n.Max(x => x.Moein.MoeinKol.GroupId),
                    GroupName = n.Max(x => x.Moein.MoeinKol.KolGroup.GroupName),
                    KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                    KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                    MoeinCode = n.Max(x => x.Moein.MoeinCode),
                    MoeinName = n.Max(x => x.Moein.MoeinName),
                    Description = n.Max(x => x.Moein.MoeinKol.Description),
                    Nature = n.Max(x => x.Moein.Nature),
                    SellerId = n.Max(s => s.SellerId),
                    Id = n.Max(x => x.Moein.KolId),

                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    Mandeh = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed) - n.Sum(s => s.Bes) : n.Sum(s => s.Bes) - n.Sum(s => s.Bed),
                    MandehNature = Convert.ToInt16((n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? 1 : ((n.Sum(s => s.Bed) < n.Sum(s => s.Bes)) ? 2 : 3))

                }).ToListAsync();

            return accounts;
        }

        // دفتر روزنامه
        public async Task<List<RooznamehDto>> DafarRooznamehAsync(long sellerId, int periodId, int rowsInPage)
        {
            List<RooznamehDto> FinalArticles = new List<RooznamehDto>();
            List<RooznamehDto> lst = new List<RooznamehDto>();
            rowsInPage = rowsInPage + 1;
            // دریافت لیست اسناد
            var articles = await _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc).Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.Doc.SellerId == sellerId && n.Doc.PeriodId == periodId
                    && n.IsDeleted == false && n.Doc.IsDeleted == false)
                .Select(n => new RooznamehDto
                {
                    ArtNo = n.RowNumber,
                    DocDate = n.Doc.DocDate.LatinToPersian(),
                    DocNumber = n.Doc.DocNumber,
                    Description = n.Moein.MoeinKol.KolName,
                    KolCode = n.Moein.MoeinKol.KolCode,
                    MoeinCode = n.Moein.MoeinCode,
                    Bed = n.Bed,
                    Bes = n.Bes
                }).OrderBy(n => n.DocNumber).ToListAsync();

            List<int> docNumbers = articles.Select(n => n.DocNumber).Distinct().ToList();
            foreach (var docNumber in docNumbers)
            {
                var doc = articles.Where(n => n.DocNumber == docNumber && n.Bed > 0).OrderByDescending(n => n.Bed).ToList();
                var bedRows = doc.GroupBy(n => n.KolCode).Select(n => new RooznamehDto
                {
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    KolCode = n.Key,
                    MoeinCode = n.Max(s => s.MoeinCode),
                    Description = n.Max(s => s.Description),
                    DocDate = n.Max(s => s.DocDate),
                    DocNumber = n.Max(s => s.DocNumber)
                }).ToList();
                FinalArticles.AddRange(bedRows);

                var docBes = articles.Where(n => n.DocNumber == docNumber && n.Bes > 0).OrderByDescending(n => n.Bes).ToList();
                var besRows = docBes.GroupBy(n => n.KolCode).Select(n => new RooznamehDto
                {
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    KolCode = n.Key,
                    MoeinCode = n.Max(s => s.MoeinCode),
                    Description = n.Max(s => s.Description),
                    DocDate = n.Max(s => s.DocDate),
                    DocNumber = n.Max(s => s.DocNumber)
                }).ToList();
                FinalArticles.AddRange(besRows);

                FinalArticles.Add(new RooznamehDto
                {
                    Bed = 0,
                    Bes = 0
                });
            }

            // تقسیم بندی اسناد به صفحات
            int currentPage = 1;
            int rowNumber = 1;
            long totalBed = 0;
            long totalBes = 0;

            for (int i = 0; i < FinalArticles.Count; i += rowsInPage - 2)
            {
                // ایجاد ردیف جمع صفحه قبل
                if (currentPage > 1)
                {
                    lst.Add(new RooznamehDto
                    {
                        Pno = currentPage,
                        Description = $"منقول از صفحه {currentPage - 1}",
                        Bed = totalBed,
                        Bes = totalBes
                    });
                }

                // اضافه کردن ردیف‌های اسناد به صفحه
                for (int j = i; j < i + rowsInPage - 2 && j < FinalArticles.Count; j++)
                {
                    var article = FinalArticles[j];

                    // اگر ردیف خالی است و اولین ردیف صفحه بعدی است، از اضافه کردن آن صرف نظر کنید
                    if (j == i && article.Bed == 0 && article.Bes == 0)
                    {
                        continue;
                    }

                    article.Pno = currentPage;
                    article.ArtNo = rowNumber;
                    lst.Add(article);
                    totalBed += article.Bed;
                    totalBes += article.Bes;

                    rowNumber++;
                }

                // ایجاد ردیف جمع کل صفحه
                lst.Add(new RooznamehDto
                {
                    Pno = currentPage,
                    Description = $"جمع صفحه {currentPage}",
                    Bed = totalBed,
                    Bes = totalBes
                });

                currentPage++;
            }

            return lst;
        }
        public async Task<List<RooznamehDto>> DafarRooznamehByKolAsync(long sellerId, int periodId, int rowsInPage)
        {
            List<RooznamehDto> FinalArticles = new List<RooznamehDto>();
            List<RooznamehDto> lst = new List<RooznamehDto>();
            rowsInPage = rowsInPage + 1;
            // دریافت لیست اسناد
            var articles = await _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc).Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.Doc.SellerId == sellerId && n.Doc.PeriodId == periodId
                    && n.IsDeleted == false && n.Doc.IsDeleted == false)
                .Select(n => new RooznamehDto
                {
                    ArtNo = n.RowNumber,
                    DocDate = n.Doc.DocDate.LatinToPersian(),
                    DocNumber = n.Doc.DocNumber,
                    Description = n.Moein.MoeinKol.KolName,
                    KolCode = n.Moein.MoeinKol.KolCode,
                    MoeinCode = n.Moein.MoeinCode,
                    Bed = n.Bed,
                    Bes = n.Bes
                }).OrderBy(n => n.DocNumber).ToListAsync();

            List<int> docNumbers = articles.Select(n => n.DocNumber).Distinct().ToList();
            foreach (var docNumber in docNumbers)
            {
                var doc = articles.Where(n => n.DocNumber == docNumber && n.Bed > 0).OrderByDescending(n => n.Bed).ToList();
                var bedRows = doc.GroupBy(n => n.KolCode).Select(n => new RooznamehDto
                {
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    KolCode = n.Key,
                    MoeinCode = n.Max(s => s.MoeinCode),
                    Description = n.Max(s => s.Description),
                    DocDate = n.Max(s => s.DocDate),
                    DocNumber = n.Max(s => s.DocNumber)
                }).ToList();
                FinalArticles.AddRange(bedRows);

                var docBes = articles.Where(n => n.DocNumber == docNumber && n.Bes > 0).OrderByDescending(n => n.Bes).ToList();
                var besRows = docBes.GroupBy(n => n.KolCode).Select(n => new RooznamehDto
                {
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    KolCode = n.Key,
                    MoeinCode = n.Max(s => s.MoeinCode),
                    Description = n.Max(s => s.Description),
                    DocDate = n.Max(s => s.DocDate),
                    DocNumber = n.Max(s => s.DocNumber)
                }).ToList();
                FinalArticles.AddRange(besRows);

                FinalArticles.Add(new RooznamehDto
                {
                    Bed = 0,
                    Bes = 0
                });
            }

            // تقسیم بندی اسناد به صفحات
            int currentPage = 1;
            int rowNumber = 1;
            long totalBed = 0;
            long totalBes = 0;

            for (int i = 0; i < FinalArticles.Count; i += rowsInPage - 2)
            {
                // ایجاد ردیف جمع صفحه قبل
                if (currentPage > 1)
                {
                    lst.Add(new RooznamehDto
                    {
                        Pno = currentPage,
                        Description = $"منقول از صفحه {currentPage - 1}",
                        Bed = totalBed,
                        Bes = totalBes
                    });
                }

                // اضافه کردن ردیف‌های اسناد به صفحه
                for (int j = i; j < i + rowsInPage - 2 && j < FinalArticles.Count; j++)
                {
                    var article = FinalArticles[j];

                    // اگر ردیف خالی است و اولین ردیف صفحه بعدی است، از اضافه کردن آن صرف نظر کنید
                    if (j == i && article.Bed == 0 && article.Bes == 0)
                    {
                        continue;
                    }

                    article.Pno = currentPage;
                    article.ArtNo = rowNumber;
                    lst.Add(article);
                    totalBed += article.Bed;
                    totalBes += article.Bes;

                    rowNumber++;
                }

                // ایجاد ردیف جمع کل صفحه
                lst.Add(new RooznamehDto
                {
                    Pno = currentPage,
                    Description = $"جمع صفحه {currentPage}",
                    Bed = totalBed,
                    Bes = totalBes
                });

                currentPage++;
            }

            return lst;
        }
        //دفتر کل
        public async Task<List<DaftarKolDto>> DaftarKolAsync(long sellerId, int periodId, int rowsInPage)
        {
            List<DaftarKolDto> finalArticles = new List<DaftarKolDto>();

            // دریافت لیست اسناد
            var articles = await _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc).Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.Doc.SellerId == sellerId && n.Doc.PeriodId == periodId
                    && n.IsDeleted == false && !n.Doc.IsDeleted)
                .Select(n => new DaftarKolDto
                {
                    DocDate = n.Doc.DocDate.LatinToPersian(),
                    DocNumber = n.Doc.DocNumber,
                    Description = "طبق شرح دفتر روزنامه",
                    KolCode = n.Moein.MoeinKol.KolCode,
                    KolName = n.Moein.MoeinKol.KolName,
                    Bed = n.Bed,
                    Bes = n.Bes
                }).OrderBy(n => n.DocNumber).ToListAsync();

            List<int> docNumbers = articles.Select(n => n.DocNumber).Distinct().ToList();
            foreach (var docNumber in docNumbers)
            {
                var doc = articles.Where(n => n.DocNumber == docNumber && n.Bed > 0).OrderByDescending(n => n.Bed).ToList();
                var bedRows = doc.GroupBy(n => n.KolCode).Select(n => new DaftarKolDto
                {
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    KolName = n.Max(s => s.KolName),
                    KolCode = n.Key,
                    Description = n.Max(s => s.Description),
                    DocDate = n.Max(s => s.DocDate),
                    DocNumber = n.Max(s => s.DocNumber)
                }).ToList();
                finalArticles.AddRange(bedRows);

                var docBes = articles.Where(n => n.DocNumber == docNumber && n.Bes > 0).OrderByDescending(n => n.Bes).ToList();
                var besRows = docBes.GroupBy(n => n.KolCode).Select(n => new DaftarKolDto
                {
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    KolName = n.Max(s => s.KolName),
                    KolCode = n.Key,
                    Description = n.Max(s => s.Description),
                    DocDate = n.Max(s => s.DocDate),
                    DocNumber = n.Max(s => s.DocNumber)
                }).ToList();
                finalArticles.AddRange(besRows);
            }

            finalArticles = finalArticles.OrderBy(n => n.KolCode).ThenBy(n => n.DocNumber).ToList();
            List<string> kolCodes = finalArticles.Select(n => n.KolCode).Distinct().ToList<string>();

            List<DaftarKolDto> result = new List<DaftarKolDto>();
            int currentPageNumber = 1;
            int currentRowNumber = 1;

            foreach (var kolCode in kolCodes)
            {
                var kols = finalArticles.Where(n => n.KolCode == kolCode).ToList();
                long kolsBed = 0;
                long kolsBes = 0;
                List<DaftarKolDto> currentPage = new List<DaftarKolDto>();
                int accountPageNumber = 1;

                foreach (var x in kols)
                {
                    kolsBed += x.Bed;
                    kolsBes += x.Bes;
                    if (kolsBed > kolsBes)
                    {
                        x.Mandeh = kolsBed - kolsBes;
                        x.Tashkhis = "بدهکار";
                    }
                    else if (kolsBes > kolsBed)
                    {
                        x.Mandeh = kolsBes - kolsBed;
                        x.Tashkhis = "بستانکار";
                    }
                    else
                    {
                        x.Mandeh = kolsBed - kolsBes;
                    }

                    x.PageNumber = currentPageNumber;
                    x.RowNumber = currentRowNumber++;
                    x.AccountPageNumber = accountPageNumber;
                    currentPage.Add(x);

                    if (currentPage.Count == rowsInPage)
                    {
                        AddPageSummary(currentPage, kolsBed, kolsBes, currentPageNumber, currentRowNumber++, accountPageNumber);
                        result.AddRange(currentPage);
                        currentPage = new List<DaftarKolDto>
                {
                    CreatePageContinuation(kolsBed, kolsBes, currentPageNumber + 1, currentRowNumber++, accountPageNumber + 1, kolCode, kols.First().KolName)
                };
                        currentPageNumber++;
                        accountPageNumber++;
                    }
                }

                if (currentPage.Any())
                {
                    AddPageSummary(currentPage, kolsBed, kolsBes, currentPageNumber, currentRowNumber++, accountPageNumber);
                    result.AddRange(currentPage);
                    currentPageNumber++;
                    accountPageNumber++;
                }
            }

            return result.OrderBy(n => n.KolCode).ThenBy(n => n.RowNumber).ToList();
        }
        private void AddPageSummary(List<DaftarKolDto> page, long kolsBed, long kolsBes, int currentPageNumber, int currentRowNumber, int accountPageNumber)
        {
            var summary = new DaftarKolDto
            {
                Description = "جمع صفحه",
                Bed = kolsBed,
                Bes = kolsBes,
                Mandeh = kolsBed > kolsBes ? kolsBed - kolsBes : kolsBes - kolsBed,
                Tashkhis = kolsBed > kolsBes ? "بدهکار" : "بستانکار",
                PageNumber = currentPageNumber,
                RowNumber = currentRowNumber,
                AccountPageNumber = accountPageNumber,
                KolCode = page?.FirstOrDefault()?.KolCode,
                KolName = page?.FirstOrDefault()?.KolName
            };
            page.Add(summary);
        }
        private DaftarKolDto CreatePageContinuation(long kolsBed, long kolsBes, int nextPageNumber, int nextRowNumber, int nextAccountPageNumber, string kolCode, string kolName)
        {
            return new DaftarKolDto
            {
                Description = "انتقال از صفحه قبل",
                Bed = kolsBed,
                Bes = kolsBes,
                Mandeh = kolsBed > kolsBes ? kolsBed - kolsBes : kolsBes - kolsBed,
                Tashkhis = kolsBed > kolsBes ? "بدهکار" : "بستانکار",
                PageNumber = nextPageNumber,
                RowNumber = nextRowNumber,
                AccountPageNumber = nextAccountPageNumber,
                KolCode = kolCode,
                KolName = kolName
            };
        }

        //Export To Excel
        public async Task<byte[]> GenerateTrialBalanceReportAsync(DocFilterDto filter)
        {
            var trialBalanceList = await GetTrialBalanceAsync(filter);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("تراز آزمایشی");

                // تنظیمات راست به چپ
                worksheet.RightToLeft = true;

                string reportTitle = "تراز آزمایشی در سطح ";
                if (filter.ReportLevel == 1)
                    reportTitle += "کـل";
                else if (filter.ReportLevel == 2)
                    reportTitle += "معین";
                else if (filter.ReportLevel == 3)
                    reportTitle += "تفصیل";
                // Report title
                worksheet.Cell(1, 1).Value = reportTitle;
                var titleRange = worksheet.Range("A1:G1");
                titleRange.Merge().Style.Font.SetBold().Font.FontSize = 16;
                titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Report date
                worksheet.Cell(2, 1).Value = $"تاریخ دریافت گزارش : {DateTime.Now.LatinToPersian()}";
                var dateRange = worksheet.Range("A2:G2");
                dateRange.Merge().Style.Font.SetBold().Font.FontSize = 12;
                dateRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                // Header
                var headers = new[] { "کد حساب", "نام حساب", "ماهیت حساب", "بدهکار", "بستانکار", "مانده بدهکار", "مانده بستانکار" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cell(4, i + 1).Value = headers[i];
                    worksheet.Cell(4, i + 1).Style.Font.SetBold();
                    worksheet.Cell(4, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(4, i + 1).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Cell(4, i + 1).Style.Font.FontColor = XLColor.White;
                }

                // Data
                for (int i = 0; i < trialBalanceList.Count; i++)
                {

                    var item = trialBalanceList[i];

                    string accountCode = item.BalanceLevel == 1 ? item.AccountKolCode : item.AccountMoeinCode;
                    string accountName = item.BalanceLevel == 1 ? item.AccountKolName : item.AccountMoeinName;
                    if (item.BalanceLevel == 3)
                    {
                        if (!string.IsNullOrEmpty(item.AccountTafsil4Name))
                            accountName += " - " + item.AccountTafsil4Name;
                        else
                            accountName += " (بدون تفصیل)";
                    }
                    worksheet.Cell(i + 5, 1).Value = accountCode;
                    worksheet.Cell(i + 5, 2).Value = accountName;
                    worksheet.Cell(i + 5, 3).Value = item.AccountNature.AccToNatureName();
                    worksheet.Cell(i + 5, 4).Value = item.TotalBed;
                    worksheet.Cell(i + 5, 5).Value = item.TotalBes;
                    worksheet.Cell(i + 5, 6).Value = item.MandehBed;
                    worksheet.Cell(i + 5, 7).Value = item.MandehBes;

                    // تنظیم فرمت جداکننده هزارگان برای سلول‌های عددی
                    worksheet.Cell(i + 5, 4).Style.NumberFormat.SetFormat("#,##0");
                    worksheet.Cell(i + 5, 5).Style.NumberFormat.SetFormat("#,##0");
                    worksheet.Cell(i + 5, 6).Style.NumberFormat.SetFormat("#,##0");
                    worksheet.Cell(i + 5, 7).Style.NumberFormat.SetFormat("#,##0");

                    // رنگ ردیف‌های فرد
                    if (i % 2 == 0)
                    {
                        worksheet.Row(i + 5).Style.Fill.BackgroundColor = XLColor.AliceBlue;
                    }
                }


                // تنظیم عرض ستون‌ها
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.ToArray();
                }
            }
        }
        public async Task<byte[]> GenerateArticlesReportAsync(DocFilterDto filter)
        {
            var articles = await Report_ArticlesAsync(filter);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("گزارش دفتر روزنامه");

                // تنظیمات راست به چپ
                worksheet.RightToLeft = true;

                string reportTitle = "تراز آزمایشی در سطح ";
                // Report title
                worksheet.Cell(1, 1).Value = reportTitle;
                var titleRange = worksheet.Range("A1:G1");
                titleRange.Merge().Style.Font.SetBold().Font.FontSize = 16;
                titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Report date
                worksheet.Cell(2, 1).Value = $"تاریخ دریافت گزارش : {DateTime.Now.LatinToPersian()}";
                var dateRange = worksheet.Range("A2:G2");
                dateRange.Merge().Style.Font.SetBold().Font.FontSize = 12;
                dateRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                // Header
                var headers = new[] { "شماره سند", "تاریخ سند", "کد حساب", "نام حساب", "شـرح", "تفصیل 4", "تفصیل 5", "تفصیل 6", "بدهکار", "بستانکار" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cell(4, i + 1).Value = headers[i];
                    worksheet.Cell(4, i + 1).Style.Font.SetBold();
                    worksheet.Cell(4, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(4, i + 1).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Cell(4, i + 1).Style.Font.FontColor = XLColor.White;
                }

                // Data
                for (int i = 0; i < articles.Count; i++)
                {

                    var item = articles[i];

                    worksheet.Cell(i + 5, 1).Value = item.DocNumber;
                    worksheet.Cell(i + 5, 2).Value = item.DocDate.Value.LatinToPersian();
                    worksheet.Cell(i + 5, 3).Value = item.MoeinCode;
                    worksheet.Cell(i + 5, 4).Value = item.MoeinName;
                    worksheet.Cell(i + 5, 5).Value = item.Comment;
                    worksheet.Cell(i + 5, 6).Value = item.Tafsil4Name;
                    worksheet.Cell(i + 5, 7).Value = item.Tafsil5Name;
                    worksheet.Cell(i + 5, 8).Value = item.Tafsil6Name;
                    worksheet.Cell(i + 5, 9).Value = item.Bed;
                    worksheet.Cell(i + 5, 10).Value = item.Bes;

                    // تنظیم فرمت جداکننده هزارگان برای سلول‌های عددی
                    worksheet.Cell(i + 5, 9).Style.NumberFormat.SetFormat("#,##0");
                    worksheet.Cell(i + 5, 10).Style.NumberFormat.SetFormat("#,##0");

                    // رنگ ردیف‌های فرد
                    if (i % 2 == 0)
                    {
                        worksheet.Row(i + 5).Style.Fill.BackgroundColor = XLColor.AliceBlue;
                    }
                }

                // تنظیم عرض ستون‌ها
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.ToArray();
                }
            }
        }
        public async Task<byte[]> GenerateMoeinTrialBalanceAsync(DocFilterDto filter)
        {
            var trialBalanceList = await GetTrialBalanceAsync(filter);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("تراز آزمایشی");

                // تنظیمات راست به چپ
                worksheet.RightToLeft = true;

                // Report title
                worksheet.Cell(1, 1).Value = "تراز آزمایشی";
                var titleRange = worksheet.Range("A1:K1");
                titleRange.Merge().Style.Font.SetBold().Font.FontSize = 16;
                titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Report date
                worksheet.Cell(2, 1).Value = $"Date: {DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("fa-IR"))}";
                var dateRange = worksheet.Range("A2:K2");
                dateRange.Merge().Style.Font.SetBold().Font.FontSize = 12;
                dateRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Header
                worksheet.Cell(4, 2).Value = "حساب کل";
                worksheet.Cell(4, 3).Value = "کد حساب کل";
                worksheet.Cell(4, 4).Value = "نام حساب معین";
                worksheet.Cell(4, 5).Value = "کد حساب معین";
                worksheet.Cell(4, 6).Value = "نام تفصیل 4";
                worksheet.Cell(4, 7).Value = "ماهیت حساب";
                worksheet.Cell(4, 8).Value = "بدهکار";
                worksheet.Cell(4, 9).Value = "بستانکار";
                worksheet.Cell(4, 10).Value = "مانده بدهکار";
                worksheet.Cell(4, 11).Value = "مانده بستانکار";


                // Data
                for (int i = 0; i < trialBalanceList.Count; i++)
                {
                    var item = trialBalanceList[i];
                    //worksheet.Cell(i + 5, 1).Value = item.BalanceLevel;
                    worksheet.Cell(i + 5, 2).Value = item.AccountKolName;
                    worksheet.Cell(i + 5, 3).Value = item.AccountKolCode;
                    worksheet.Cell(i + 5, 4).Value = item.AccountMoeinName;
                    worksheet.Cell(i + 5, 5).Value = item.AccountMoeinCode;
                    worksheet.Cell(i + 5, 6).Value = item.AccountTafsil4Name;
                    worksheet.Cell(i + 5, 7).Value = item.AccountNature;
                    worksheet.Cell(i + 5, 8).Value = item.TotalBed;
                    worksheet.Cell(i + 5, 9).Value = item.TotalBes;
                    worksheet.Cell(i + 5, 10).Value = item.MandehBed;
                    worksheet.Cell(i + 5, 11).Value = item.MandehBes;

                    // تنظیم فرمت جداکننده هزارگان برای سلول‌های عددی
                    worksheet.Cell(i + 5, 8).Style.NumberFormat.SetFormat("#,##0");
                    worksheet.Cell(i + 5, 9).Style.NumberFormat.SetFormat("#,##0");
                    worksheet.Cell(i + 5, 10).Style.NumberFormat.SetFormat("#,##0");
                    worksheet.Cell(i + 5, 11).Style.NumberFormat.SetFormat("#,##0");
                }

                worksheet.Columns().AdjustToContents();


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.ToArray();
                }
            }
        }

        //------------------------
        // Tafsil Report
        public async Task<List<TafsilReportDto>> TafsilReportAsync(long sellerId
            , int period
            , long tafsilId
            , long? tafsil5Id
            , long? tafsil6Id
            , string? startDate = null
            , string? endDate = null
            , int[]? Moeins = null)
        {
            List<TafsilReportDto> lst = new List<TafsilReportDto>();
            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Moein)
                .Include(n => n.Doc)
                .Where(n =>
                !n.Doc.IsDeleted && !n.IsDeleted
               && n.Doc.SellerId == sellerId && n.Doc.PeriodId == period
                && n.Tafsil4Id == tafsilId)
                .OrderBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber).ThenBy(n => n.RowNumber)
                .AsQueryable();

            if (Moeins.Length > 0)
                query = query.Where(n => Moeins.Contains(n.MoeinId));
            if (tafsil5Id.HasValue)
                query = query.Where(n => n.Tafsil5Id == tafsil5Id && n.Tafsil5Id != null);
            if (tafsil6Id.HasValue)
                query = query.Where(n => n.Tafsil6Id == tafsil6Id && n.Tafsil6Id != null);
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime stdate = startDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= stdate);
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime EndDate = endDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= EndDate);
            }

            List<int> accounts = query.Select(n => n.MoeinId).Distinct().ToList<int>();
            foreach (var moein in accounts)
            {
                var arts = await query.Where(n => n.MoeinId == moein).ToListAsync();
                long bed = 0;
                long bes = 0;
                long mandeh = 0;
                foreach (var x in arts)
                {
                    TafsilReportDto dto = new TafsilReportDto();
                    dto.ArtId = x.Id;
                    dto.DocId = x.DocId;
                    dto.DocNumber = x.Doc.DocNumber;
                    dto.DocDate = x.Doc.DocDate;
                    dto.RowNumber = x.RowNumber;

                    dto.MoeinId = x.MoeinId;
                    dto.MoeinCode = x.Moein.MoeinCode;
                    dto.MoeinName = x.Moein.MoeinName;
                    dto.Comment = x.Comment;

                    dto.Tafsil4Id = x.Tafsil4Id;
                    dto.Tafsil4Name = x.Tafsil4Name;
                    dto.Tafsil5Id = x.Tafsil5Id;
                    dto.Tafsil5Name = x.Tafsil5Name;
                    dto.Tafsil6Id = x.Tafsil6Id;
                    dto.Tafsil7Id = x.Tafsil7Id;

                    dto.Bed = x.Bed;
                    bed += x.Bed;
                    dto.Bes = x.Bes;
                    bes += x.Bes;
                    if (bed > bes)
                    {
                        mandeh = bed - bes;
                        dto.BalaceNature = 1;
                    }
                    else if (bes > bed)
                    {
                        mandeh = bes - bed;
                        dto.BalaceNature = 2;
                    }
                    else
                    {
                        mandeh = bed - bes;
                        dto.BalaceNature = 0;
                    }
                    dto.Balace = mandeh;
                    lst.Add(dto);
                }
            }

            return lst;
        }

        public async Task<VmTafsilReport> PersonBalaceAsync(TafsilReportFilterDto filter)
        {
            VmTafsilReport result = new VmTafsilReport();

            if (filter.TafsilGroup != null && (filter.Tafsil4Ids == null || filter.Tafsil4Ids?.Count == 0))
                filter.Tafsil4Ids = await _baseData.GetGroupTafsilIdsAsync(filter.SellerId, filter.TafsilGroup);

            List<TafsilReportDto> lst = new List<TafsilReportDto>();
            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Moein)
                .ThenInclude(n => n.MoeinKol)
                .Include(n => n.Doc)
                .Where(n =>
                  n.Doc.IsDeleted == false && n.IsDeleted == false
               && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId
                && filter.Tafsil4Ids.Contains(n.Tafsil4Id.Value))
                .AsQueryable();

            if (filter.Kols?.Count > 0)
                query = query.Where(n => filter.Kols.Contains(n.Moein.KolId));

            if (filter.Moeins?.Count > 0)
                query = query.Where(n => filter.Moeins.Contains(n.MoeinId));
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                DateTime stdate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= stdate);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                DateTime EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= EndDate);
            }
            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            query = query.OrderBy(n => n.KolId).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber).ThenBy(n => n.RowNumber).AsQueryable();

            long bed = 0;
            long bes = 0;
            long mandeh = 0;
            foreach (var x in query)
            {
                TafsilReportDto dto = new TafsilReportDto();
                dto.ArtId = x.Id;
                dto.DocId = x.DocId;
                dto.DocNumber = x.Doc.DocNumber;
                dto.DocDate = x.Doc.DocDate;
                dto.strDocDate = x.Doc.DocDate.LatinToPersian();
                dto.RowNumber = x.RowNumber;

                dto.KolCode = x.Moein?.MoeinKol?.KolCode;
                dto.KolName = x.Moein?.MoeinKol?.KolName;
                dto.MoeinId = x.MoeinId;
                dto.MoeinCode = x.Moein?.MoeinCode;
                dto.MoeinName = x.Moein?.MoeinName;
                dto.Comment = x.Comment;

                dto.Tafsil4Id = x.Tafsil4Id;
                dto.Tafsil4Name = x.Tafsil4Name;
                dto.Tafsil5Id = x.Tafsil5Id;
                dto.Tafsil5Name = x.Tafsil5Name;
                dto.Tafsil6Id = x.Tafsil6Id;
                dto.Tafsil7Id = x.Tafsil7Id;

                dto.Bed = x.Bed;
                bed += x.Bed;
                dto.Bes = x.Bes;
                bes += x.Bes;
                if (bed > bes)
                {
                    mandeh = bed - bes;
                    dto.BalaceNature = 1;
                }
                else if (bes > bed)
                {
                    mandeh = bes - bed;
                    dto.BalaceNature = 2;
                }
                else
                {
                    mandeh = bed - bes;
                    dto.BalaceNature = 0;
                }
                dto.Balace = mandeh;
                lst.Add(dto);
            }

            result.filter = filter;
            result.Report = lst;

            return result;
        }
        public async Task<VmTafsilReport> GetTafsilBalaceAsync(TafsilReportFilterDto filter)
        {
            VmTafsilReport result = new VmTafsilReport();

            if (filter.TafsilGroup.HasValue)
            {
                if (filter.ReportLevel == 4 && !filter.Tafsil4Ids.Any())
                    filter.Tafsil4Ids = await _baseData.GetGroupTafsilIdsAsync(filter.SellerId, filter.TafsilGroup);
                else if (filter.ReportLevel == 5 && !filter.Tafsil5Ids.Any())
                    filter.Tafsil5Ids = await _baseData.GetGroupTafsilIdsAsync(filter.SellerId, filter.TafsilGroup);
            }

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Moein)
                .ThenInclude(n => n.MoeinKol)
                .Include(n => n.Doc)
                .Where(n =>
                  n.Doc.IsDeleted == false && n.IsDeleted == false
                 && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId)
                .AsQueryable();

            if (filter.Tafsil4Ids.Any())
                query = query.Where(n => filter.Tafsil4Ids.Contains(n.Tafsil4Id.Value));

            if (filter.Tafsil5Ids.Any())
                query = query.Where(n => filter.Tafsil5Ids.Contains(n.Tafsil5Id.Value));

            if (filter.Kols?.Count > 0)
                query = query.Where(n => filter.Kols.Contains(n.Moein.KolId));

            if (filter.Moeins?.Count > 0)
                query = query.Where(n => filter.Moeins.Contains(n.MoeinId));
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                DateTime stdate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= stdate);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                DateTime EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= EndDate);
            }
            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            query = query.OrderBy(n => n.KolId).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber).ThenBy(n => n.RowNumber).AsQueryable();


            var data = await query.GroupBy(n => filter.ReportLevel == 4 ? n.Tafsil4Id : n.Tafsil5Id).Select(n => new
            {
                tafsilId = n.Key,
                tafsilName = filter.ReportLevel == 4 ? n.FirstOrDefault().Tafsil4Name : n.FirstOrDefault().Tafsil5Name,
                totalBed = n.Sum(s => s.Bed),
                totalBes = n.Sum(s => s.Bes),
                report = n.OrderBy(x => x.Doc.DocDate).ThenBy(x => x.Doc.DocNumber).ThenBy(x => x.RowNumber).Select(x => new TafsilReportDto
                {
                    ArtId = x.Id,
                    DocId = x.DocId,
                    DocNumber = x.Doc.DocNumber,
                    RowNumber = x.RowNumber,
                    DocDate = x.Doc.DocDate,
                    strDocDate = x.Doc.DocDate.LatinToPersian(),
                    KolCode = x.Moein.MoeinKol.KolCode,
                    KolName = x.Moein.MoeinKol.KolName,
                    MoeinId = x.MoeinId,
                    MoeinCode = x.Moein.MoeinCode,
                    MoeinName = x.Moein.MoeinName,
                    Comment = x.Comment,
                    Tafsil4Id = x.Tafsil4Id,
                    Tafsil5Id = x.Tafsil5Id,
                    Tafsil6Id = x.Tafsil6Id,
                    Tafsil7Id = x.Tafsil7Id,
                    Tafsil8Id = x.Tafsil8Id,
                    Tafsil4Name = x.Tafsil4Name,
                    Tafsil5Name = x.Tafsil5Name,
                    Tafsil6Name = x.Tafsil6Name,
                    Tafsil7Name = x.Tafsil7Name,
                    Tafsil8Name = x.Tafsil8Name,
                    Bed = x.Bed,
                    Bes = x.Bes,

                }).ToList()

            }).ToListAsync();

            List<TafsilReportDto> lst = new List<TafsilReportDto>();

            foreach (var tafsil in data)
            {
                long bed = 0;
                long bes = 0;
                long mandeh = 0;
                if (filter.ShowJustWithValue)
                {
                    if (tafsil.totalBed == tafsil.totalBes)
                        continue;
                }

                foreach (var x in tafsil.report.OrderBy(s => s.DocDate).ThenBy(s => s.DocNumber).ThenBy(z => z.RowNumber).ToList())
                {
                    TafsilReportDto dto = new TafsilReportDto();
                    dto.ArtId = x.ArtId;
                    dto.DocId = x.DocId;
                    dto.DocNumber = x.DocNumber;
                    dto.DocDate = x.DocDate;
                    dto.strDocDate = x.strDocDate;
                    dto.RowNumber = x.RowNumber;

                    dto.KolCode = x.KolCode;
                    dto.KolName = x.KolName;
                    dto.MoeinId = x.MoeinId;
                    dto.MoeinCode = x.MoeinCode;
                    dto.MoeinName = x.MoeinName;
                    dto.Comment = x.Comment;

                    dto.Tafsil4Id = x.Tafsil4Id;
                    dto.Tafsil4Name = x.Tafsil4Name;
                    dto.Tafsil5Id = x.Tafsil5Id;
                    dto.Tafsil5Name = x.Tafsil5Name;
                    dto.Tafsil6Id = x.Tafsil6Id;
                    dto.Tafsil7Id = x.Tafsil7Id;

                    dto.Bed = x.Bed;
                    bed += x.Bed;
                    dto.Bes = x.Bes;
                    bes += x.Bes;
                    if (bed > bes)
                    {
                        mandeh = bed - bes;
                        dto.BalaceNature = 1;
                        dto.BalaceNatureName = "بدهکار";
                    }
                    else if (bes > bed)
                    {
                        mandeh = bes - bed;
                        dto.BalaceNature = 2;
                        dto.BalaceNatureName = "بستانکار";
                    }
                    else
                    {
                        mandeh = bed - bes;
                        dto.BalaceNature = 0;
                        dto.BalaceNatureName = "";
                    }
                    dto.Balace = mandeh;
                    lst.Add(dto);
                }
            }

            result.filter = filter;
            result.Report = lst;

            return result;
        }
        public async Task<VmTafsilReport> Tafsil4MoeinTurnoverAsync(TafsilReportFilterDto filter)
        {
            VmTafsilReport result = new VmTafsilReport();
            if (filter.TafsilGroup != null && (filter.Tafsil4Ids == null || filter.Tafsil4Ids?.Count == 0))
                filter.Tafsil4Ids = await _baseData.GetGroupTafsilIdsAsync(filter.SellerId, filter.TafsilGroup);

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Moein)
                .ThenInclude(n => n.MoeinKol)
                .Include(n => n.Doc)
                .Where(n =>
                    n.Doc.IsDeleted == false && n.IsDeleted == false
                    && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId
                    && filter.Tafsil4Ids.Contains(n.Tafsil4Id.Value))
                .OrderBy(n => n.KolId).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber).ThenBy(n => n.RowNumber)
                .AsQueryable();

            if (filter.Kols?.Count > 0)
                query = query.Where(n => filter.Kols.Contains(n.Moein.KolId));

            if (filter.Moeins?.Count > 0)
                query = query.Where(n => filter.Moeins.Contains(n.MoeinId));
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                DateTime stdate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= stdate);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                DateTime EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= EndDate);
            }
            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            var data = await query.GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new TafsilReportDto
            {
                MoeinId = n.Key.MoeinId,
                MoeinCode = n.Max(x => x.Moein.MoeinCode),
                MoeinName = n.Max(x => x.Moein.MoeinCode),

                KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                KolName = n.Max(x => x.Moein.MoeinKol.KolName),

                Tafsil4Id = n.Key.Tafsil4Id,
                Tafsil4Name = n.Max(x => x.Tafsil4Name),
                Bed = n.Sum(x => x.Bed),
                Bes = n.Sum(x => x.Bes),
                Balace = (n.Sum(x => x.Bed) > n.Sum(x => x.Bes)) ? n.Sum(x => x.Bed - x.Bes) : n.Sum(x => x.Bes - x.Bed),
                BalaceNature = (n.Sum(x => x.Bed) > n.Sum(x => x.Bes)) ? (Int16)1 : ((n.Sum(x => x.Bes) > n.Sum(x => x.Bed)) ? (Int16)2 : (Int16)3),

            }).ToListAsync();

            result.filter = filter;
            result.Report = data;

            return result;
        }
        //
        public async Task<List<TafsilTurnoverPrintDto>> TafsilTurnoverAsync(TafsilReportFilterDto filter)
        {
            List<TafsilTurnoverPrintDto> lst = new List<TafsilTurnoverPrintDto>();
            var report = await PersonBalaceAsync(filter);

            return lst;
        }
        public async Task<List<BalanceDataDto>> GetBalance(long sellerId, int period, DateTime? date = null)
        {
            var query = _db.Acc_Articles
                 .AsNoTracking()
                 .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
            n.Doc.SellerId == sellerId
            && n.PeriodId == period
            && n.Doc.TypeId != 3
            && n.Moein.MoeinKol.KolGroup.TypeId == 1
            && !n.IsDeleted && !n.Doc.IsDeleted).AsQueryable();

            if (date != null)
                query = query.Where(n => n.Doc.DocDate <= date);

            //دارایی ها
            var balanceData = await query
               .GroupBy(n => n.Moein.KolId)
               .Select(n => new BalanceDataDto
               {
                   GroupType = n.Max(a => a.Moein.MoeinKol.KolGroup.GroupType),
                   GroupTypeName = n.Max(a => a.Moein.MoeinKol.KolGroup.GroupType).AccToGroupType(),
                   GroupName = n.Max(a => a.Moein.MoeinKol.KolGroup.GroupName),
                   GroupId = n.Max(a => a.Moein.MoeinKol.GroupId),
                   KolId = n.Key,
                   KolName = n.Max(a => a.Moein.MoeinKol.KolName),
                   KolCode = n.Max(a => a.Moein.MoeinKol.KolCode),
                   Bed = n.Sum(a => a.Bed),
                   Bes = n.Sum(a => a.Bes),
                   Mandeh = n.Max(s => s.Moein.Nature) == 1 ? n.Sum(s => s.Bed - s.Bes) : n.Sum(s => s.Bes - s.Bed)
               }).OrderBy(n => n.GroupType).ThenBy(n => n.KolCode).ToListAsync();

            return balanceData;

        }
        public async Task<DocumentsInfo> GetDocumentsInfoAsync(long sellerId, int period)
        {
            DocumentsInfo info = new DocumentsInfo();
            var checkSellerData = await _db.Acc_FinancialPeriods.AnyAsync(n => n.SellerId == sellerId && n.Id == period);
            if (!checkSellerData)
                return info;

            var docs = _db.Acc_Documents.Where(n =>
                n.SellerId == sellerId &&
                n.PeriodId == period &&
                !n.IsDeleted)
                .OrderBy(n => n.DocNumber)
                .AsQueryable();
            if (docs.Count() == 0 || docs == null)
                return info;

            long bed = await docs.SelectMany(n => n.DocArticles.Where(a => !a.IsDeleted))
                .SumAsync(a => a.Bed);
            long bes = await docs.SelectMany(n => n.DocArticles.Where(a => !a.IsDeleted))
                .SumAsync(a => a.Bes);

            info.DocumentCount = await docs.CountAsync();
            info.LastDocumentNumber = await docs.Select(n => n.DocNumber).MaxAsync();
            info.LatestDate = await docs.Select(n => n.DocDate).MaxAsync();
            info.TotalDebit = bed;
            info.TotalCredit = bes;

            bool isSorted = true;
            var checklist = await docs.Select(n => new { n.DocDate, n.DocNumber }).ToListAsync();
            for (int i = 0; i < checklist.Count; i++)
            {
                if (checklist[i].DocNumber != i + 1 || (i > 0 && checklist[i].DocDate < checklist[i - 1].DocDate))
                {
                    isSorted = false;
                    break;
                }
            }
            info.IsSorted = isSorted;

            return info;
        }
        //  گزارش مانده حساب تفیلی
        public async Task<VmTafsilReport> GetTafsilGroupedTurnoverAsync(TafsilReportFilterDto filter)
        {
            VmTafsilReport result = new VmTafsilReport();
            if (filter.TafsilGroup != null && (filter.Tafsil4Ids == null || filter.Tafsil4Ids?.Count == 0))
                filter.Tafsil4Ids = await _baseData.GetGroupTafsilIdsAsync(filter.SellerId, filter.TafsilGroup);

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Moein)
                .ThenInclude(n => n.MoeinKol)
                .Include(n => n.Doc)
                .Where(n =>
                    n.Doc.IsDeleted == false && n.IsDeleted == false
                    && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId
                    && filter.Tafsil4Ids.Contains(n.Tafsil4Id.Value))
                .OrderBy(n => n.KolId).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber).ThenBy(n => n.RowNumber)
                .AsQueryable();

            if (filter.Kols?.Count > 0)
                query = query.Where(n => filter.Kols.Contains(n.Moein.KolId));

            if (filter.Moeins?.Count > 0)
                query = query.Where(n => filter.Moeins.Contains(n.MoeinId));
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                DateTime stdate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= stdate);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                DateTime EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= EndDate);
            }

            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            var data = await query.GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new TafsilReportDto
            {
                MoeinId = n.Key.MoeinId,
                MoeinCode = n.Max(x => x.Moein.MoeinCode),
                MoeinName = n.Max(x => x.Moein.MoeinCode),

                KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                KolName = n.Max(x => x.Moein.MoeinKol.KolName),

                Tafsil4Id = n.Key.Tafsil4Id,
                Tafsil4Name = n.Max(x => x.Tafsil4Name),
                Bed = n.Sum(x => x.Bed),
                Bes = n.Sum(x => x.Bes),
                Balace = (n.Sum(x => x.Bed) > n.Sum(x => x.Bes)) ? n.Sum(x => x.Bed - x.Bes) : n.Sum(x => x.Bes - x.Bed),
                BalaceNature = (n.Sum(x => x.Bed) > n.Sum(x => x.Bes)) ? (Int16)1 : ((n.Sum(x => x.Bes) > n.Sum(x => x.Bed)) ? (Int16)2 : (Int16)3),

            }).OrderBy(n => n.Tafsil4Id).ThenByDescending(n => n.Bed).ToListAsync();

            List<TafsilReportDto> report = new List<TafsilReportDto>();
            foreach (var t in filter.Tafsil4Ids)
            {
                var tafsilTurnover = data.Where(n => n.Tafsil4Id == t).ToList();
                if (tafsilTurnover.Count > 0)
                {
                    foreach (var x in tafsilTurnover)
                    {
                        TafsilReportDto i = new TafsilReportDto();
                        i.Tafsil4Id = x.Tafsil4Id;
                        i.Tafsil4Name = x.Tafsil4Name;
                        i.KolCode = x.KolCode;
                        i.KolName = x.KolName;
                        i.MoeinId = x.MoeinId;
                        i.MoeinCode = x.MoeinCode;
                        i.MoeinName = x.MoeinName;
                        i.Bed = x.Bed;
                        i.Bes = x.Bes;
                        i.Balace = x.Balace;
                        i.BalaceNature = x.BalaceNature;
                        i.Comment = x.Comment;
                        i.cssClassName = "reportItem";

                        report.Add(i);
                    }

                    TafsilReportDto total = new TafsilReportDto();
                    total.Tafsil4Id = tafsilTurnover.FirstOrDefault()?.Tafsil4Id;
                    total.Tafsil4Name = tafsilTurnover.FirstOrDefault()?.Tafsil4Name;
                    total.KolCode = tafsilTurnover.FirstOrDefault()?.KolCode;
                    total.KolName = tafsilTurnover.FirstOrDefault()?.KolName;
                    total.MoeinId = tafsilTurnover.FirstOrDefault().MoeinId;
                    total.MoeinCode = tafsilTurnover.FirstOrDefault()?.MoeinCode;
                    total.MoeinName = tafsilTurnover.FirstOrDefault()?.MoeinName;
                    total.Bed = tafsilTurnover.Sum(n => n.Bed);
                    total.Bes = tafsilTurnover.Sum(n => n.Bes);
                    total.Balace = total.Bed > total.Bes ? total.Bed - total.Bes : total.Bes - total.Bed;
                    total.BalaceNature = total.Bed > total.Bes ? (short)1 : (total.Bed < total.Bes ? (short)2 : (short)3);
                    total.Comment = "";
                    total.cssClassName = "reportItemTotal";
                    report.Add(total);
                }


            }
            result.filter = filter;
            result.Report = report;

            return result;
        }
        public async Task<VmTafsilReport> GetTafsilTurnoverAsync(TafsilReportFilterDto filter)
        {
            VmTafsilReport result = new VmTafsilReport();
            if (filter.TafsilGroup != null && (filter.Tafsil4Ids == null || filter.Tafsil4Ids?.Count == 0))
                filter.Tafsil4Ids = await _baseData.GetGroupTafsilIdsAsync(filter.SellerId, filter.TafsilGroup);

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Moein)
                .ThenInclude(n => n.MoeinKol)
                .Include(n => n.Doc)
                .Where(n =>
                    n.Doc.IsDeleted == false && n.IsDeleted == false
                    && n.Doc.SellerId == filter.SellerId && n.Doc.PeriodId == filter.PeriodId
                    && filter.Tafsil4Ids.Contains(n.Tafsil4Id.Value))
                .OrderBy(n => n.KolId).ThenBy(n => n.Doc.DocDate).ThenBy(n => n.Doc.DocNumber).ThenBy(n => n.RowNumber)
                .AsQueryable();

            if (filter.Kols?.Count > 0)
                query = query.Where(n => filter.Kols.Contains(n.Moein.KolId));

            if (filter.Moeins?.Count > 0)
                query = query.Where(n => filter.Moeins.Contains(n.MoeinId));
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                DateTime stdate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= stdate);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                DateTime EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= EndDate);
            }

            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            var data = await query.GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new TafsilReportDto
            {
                MoeinId = n.Key.MoeinId,
                MoeinCode = n.Max(x => x.Moein.MoeinCode),
                MoeinName = n.Max(x => x.Moein.MoeinCode),

                KolCode = n.Max(x => x.Moein.MoeinKol.KolCode),
                KolName = n.Max(x => x.Moein.MoeinKol.KolName),

                Tafsil4Id = n.Key.Tafsil4Id,
                Tafsil4Name = n.Max(x => x.Tafsil4Name),
                Bed = n.Sum(x => x.Bed),
                Bes = n.Sum(x => x.Bes),
                Balace = (n.Sum(x => x.Bed) > n.Sum(x => x.Bes)) ? n.Sum(x => x.Bed - x.Bes) : n.Sum(x => x.Bes - x.Bed),
                BalaceNature = (n.Sum(x => x.Bed) > n.Sum(x => x.Bes)) ? (Int16)1 : ((n.Sum(x => x.Bes) > n.Sum(x => x.Bed)) ? (Int16)2 : (Int16)3),

            }).OrderBy(n => n.Tafsil4Id).ThenByDescending(n => n.Bed).ToListAsync();


            //تنظیم تاریخ گزارش
            //دوره مالی
            var financialPeriod = await _db.Acc_FinancialPeriods.FindAsync(filter.PeriodId);
            DateTime pStartDate = financialPeriod.StartDate;
            DateTime pEndDate = DateTime.Now;
            if (!string.IsNullOrEmpty(filter.strStartDate))
                pStartDate = filter.strEndDate.PersianToLatin();
            if (!string.IsNullOrEmpty(filter.strEndDate))
                pEndDate = filter.strEndDate.PersianToLatin();



            var report = await query.GroupBy(n => n.Tafsil4Id)
       .Select(n => new CustomerCreditableTurnover
       {
           TafsilId = n.Key ?? 0,
           TafsilName = n.Max(x => x.Tafsil4Name),
           FromDate = pStartDate,
           UntilDate = pEndDate,
           Bed = n.Sum(x => x.Bed),
           Bes = n.Sum(x => x.Bes),
           BedQty = n.Count(x => x.Bed > 0),
           BesQty = n.Count(x => x.Bes > 0),
           LastBedDate = n.Where(x => x.Bed > 0).OrderByDescending(x => x.Doc.DocDate).Select(x => x.Doc.DocDate).FirstOrDefault(),
           LastBesDate = n.Where(x => x.Bes > 0).OrderByDescending(x => x.Doc.DocDate).Select(x => x.Doc.DocDate).FirstOrDefault(),
           Balance = n.Sum(x => x.Bed) - n.Sum(x => x.Bes),
           BalanceNature = n.Sum(x => x.Bed) > n.Sum(x => x.Bes) ? (short)1 : (n.Sum(x => x.Bes) > n.Sum(x => x.Bed) ? (short)2 : (short)3)
       }).ToListAsync();
            result.filter = filter;
            result.CreditableCustomerReport = report;

            return result;
        }


        // Accounts Report TreeView type
        public async Task<List<AccountsReport>> GetAccountsCascadingReportAsync(DocFilterDto filter)
        {
            List<AccountsReport> accounts = new List<AccountsReport>();

            var query = _db.Acc_Articles.AsNoTracking()
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n =>
                (n.IsDeleted == false && n.Doc.IsDeleted == false)
                && n.Doc.SellerId == filter.SellerId
                && n.PeriodId == filter.PeriodId)
                .OrderBy(n => n.Moein.MoeinKol.KolGroup.Order).ThenBy(n => n.Moein.MoeinKol.KolCode).ThenBy(n => n.Moein.MoeinCode)
                .AsQueryable();


            if (filter.docType != null)
                query = query.Where(n => n.Doc.StatusId == filter.docType.Value);
            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                filter.StartDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate >= filter.StartDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                filter.EndDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.Doc.DocDate <= filter.EndDate.Value.Date);
            }
            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.Doc.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(n => n.Doc.Description.Contains(filter.Description));

            if (filter.ProjectId.HasValue)
                query = query.Where(n => n.ProjectId == filter.ProjectId);

            accounts = await query.GroupBy(n => n.KolId)
                .Select(n => new AccountsReport
                {
                    AccountId = n.Key.Value,
                    ReportLevel = 2,
                    AccountCode = n.FirstOrDefault().Moein.MoeinKol.KolCode,
                    AccountName = n.FirstOrDefault().Moein.MoeinKol.KolName,
                    Bed = n.Sum(s => s.Bed),
                    Bes = n.Sum(s => s.Bes),
                    BalanceBed = (n.Sum(s => s.Bed) > n.Sum(s => s.Bes)) ? n.Sum(s => s.Bed - s.Bes) : 0,
                    BalanceBes = (n.Sum(s => s.Bes) > n.Sum(s => s.Bed)) ? n.Sum(s => s.Bes - s.Bed) : 0,
                    Moeins = n.GroupBy(k => k.MoeinId).Select(m => new MoeinReport
                    {
                        AccountId = m.Key,
                        ReportLevel = 3,
                        AccountCode = m.FirstOrDefault().Moein.MoeinCode,
                        AccountName = m.FirstOrDefault().Moein.MoeinName,
                        Bed = m.Sum(s => s.Bed),
                        Bes = m.Sum(s => s.Bes),
                        BalanceBed = (m.Sum(s => s.Bed) > m.Sum(s => s.Bes)) ? m.Sum(s => s.Bed - s.Bes) : 0,
                        BalanceBes = (m.Sum(s => s.Bes) > m.Sum(s => s.Bed)) ? m.Sum(s => s.Bes - s.Bed) : 0,
                        Tafsils = m.GroupBy(t => t.Tafsil4Id).Select(t => new TafsilsReport
                        {
                            AccountId = t.Key,
                            ReportLevel = 4,
                            AccountCode = t.FirstOrDefault() != null ? t.FirstOrDefault().Tafsil4Id != null ? t.FirstOrDefault().Tafsil4Id.Value.ToString() : "" : "",
                            AccountName = t.FirstOrDefault().Tafsil4Name,
                            Bed = t.Sum(s => s.Bed),
                            Bes = t.Sum(s => s.Bes),
                            BalanceBed = (t.Sum(s => s.Bed) > t.Sum(s => s.Bes)) ? t.Sum(s => s.Bed - s.Bes) : 0,
                            BalanceBes = (t.Sum(s => s.Bes) > t.Sum(s => s.Bed)) ? t.Sum(s => s.Bes - s.Bed) : 0,
                        }).ToList(),
                    }).ToList(),

                }).OrderBy(n => n.AccountCode).ToListAsync();

            return accounts;
        }
    }
}
