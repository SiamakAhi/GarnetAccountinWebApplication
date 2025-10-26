using ClosedXML.Excel;
using EFCore.BulkExtensions;
using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Accounting.Dto.PrintDto;
using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccOperationService : IAccOperationService
    {
        private readonly AppDbContext _db;
        private readonly IAccCodingService _coding;
        public AccOperationService(AppDbContext dbContext, IAccCodingService coding)
        {
            _coding = coding;
            _db = dbContext;
        }

        public async Task<int> DocNumberGeneratorAsync(long SellerId, int PeriodId)
        {
            var lastNumber = await _db.Acc_Documents
                .Where(n =>
                n.SellerId == SellerId
                && n.PeriodId == PeriodId
                && n.IsDeleted == false).Select(n => n.DocNumber).ToListAsync();
            if (lastNumber.Count == 0)
                return 2;

            return lastNumber.Max() + 1;
        }
        public async Task<int> DocAutoNumberGeneratorAsync(long SellerId, int PeriodId)
        {
            var query = _db.Acc_Documents
                 .Where(n =>
                 n.SellerId == SellerId
                 && n.PeriodId == PeriodId).AsQueryable();

            int lastNumber = 0;
            if (query.Count() > 0)
                lastNumber = await query.MaxAsync(n => n.AutoDocNumber);

            return lastNumber + 1;

        }
        public async Task<bool> IsDupplicateDocNumberAsync(long SellerId, int PeriodId, int number)
        {
            return await _db.Acc_Documents
                   .AnyAsync(n =>
                   n.SellerId == SellerId
                   && n.PeriodId == PeriodId
                   && !n.IsDeleted
                   && n.DocNumber == number);
        }
        public async Task<bool> IsDupplicateDocNumberAsync(DocDto dto)
        {
            return await _db.Acc_Documents
                   .AnyAsync(n =>
                   n.Id != dto.Id
                   && n.SellerId == dto.SellerId
                   && n.PeriodId == dto.PeriodId
                   && !n.IsDeleted
                   && n.DocNumber == dto.DocNumber);
        }
        public async Task<string> GetDocArchiveNumber(long SellerId, int PeriodId, int DocNumber)
        {
            var archive = await _db.Acc_ArchiveNumbers
                .Where(n => n.SellerId == SellerId && n.PeriodId == PeriodId && n.DocAutonumber == DocNumber)
                .FirstOrDefaultAsync();

            string NewArchiveNumer = "";
            if (archive == null)
            {
                Acc_ArchiveNumber newarchive = new Acc_ArchiveNumber
                {
                    SellerId = SellerId,
                    PeriodId = PeriodId,
                    DocAutonumber = DocNumber,
                    Counter = 1
                };
                _db.Acc_ArchiveNumbers.Add(newarchive);
                NewArchiveNumer = $"{newarchive.DocAutonumber.ToString("000")}/{newarchive.Counter.ToString("000")}";
            }
            else
            {
                archive.Counter++;
                _db.Acc_ArchiveNumbers.Update(archive);
                NewArchiveNumer = $"{archive.DocAutonumber.ToString("000")}/{archive.Counter.ToString("000")}";
            }
            try
            {
                await _db.SaveChangesAsync();
                return NewArchiveNumer;
            }
            catch
            {
                return "";
            }

        }

        public async Task<string> GetDocArchiveNumber(Guid docId)
        {

            string ArchiveCode = "";
            var doc = await _db.Acc_Documents.FindAsync(docId);
            var arts = await _db.Acc_Articles
                .Where(n => n.DocId == docId && !n.IsDeleted).Select(n => new { code = n.ArchiveCode, num = n.Doc.DocNumber })
                .OrderByDescending(n => n.code).FirstOrDefaultAsync();
            if (arts == null)
            {

                ArchiveCode = $"{doc.DocNumber}/001";
                return ArchiveCode;
            }
            try
            {
                int sequ = Convert.ToInt32(arts.code.Split("/")[1]) + 1;

                string NewArchiveNumer = $"{arts.num}/{sequ.ToString("000")}";
                return NewArchiveNumer;
            }
            catch
            {
                return ArchiveCode = $"{doc.DocNumber}/001";
            }
        }

        public async Task<SelectList> Selectlist_Dosc(long sellerId, int periodId)
        {
            var docs = await _db.Acc_Documents.Where(n => !n.IsDeleted && n.SellerId == sellerId && n.PeriodId == periodId)
                .Select(n => new { Id = n.Id, number = n.DocNumber, Name = n.DocNumber + " - " + n.DocDate.LatinToPersian() })
                .OrderBy(n => n.number).ToListAsync();

            return new SelectList(docs, "Id", "Name");
        }
        //.....................................................................................
        public async Task<List<DocDto>> GetDocsAsync(DocFilterDto filter)
        {
            var query = _db.Acc_Documents
                .AsNoTracking()
                .Include(n => n.DocArticles.Where(a => !a.IsDeleted))
                    .ThenInclude(n => n.Moein)
                .Where(x =>
                    x.SellerId == filter.SellerId &&
                    x.PeriodId == filter.PeriodId &&
                    !x.IsDeleted)
                .AsQueryable();

            if (filter.docType != null)
            {
                query = query.Where(n => n.StatusId == filter.docType.Value);
            }

            if (filter.StartDate != null)
            {
                query = query.Where(n => n.DocDate.Date >= filter.StartDate.Value.Date);
            }

            if (filter.EndDate != null)
            {
                query = query.Where(n => n.DocDate <= filter.EndDate.Value.Date);
            }

            if (filter.FromDocNumer > 0)
            {
                query = query.Where(n => n.DocNumber >= filter.FromDocNumer);
            }

            if (filter.ToDocNumer > 0)
            {
                query = query.Where(n => n.DocNumber <= filter.ToDocNumer);
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(n =>
                    n.Description.Contains(filter.Description) ||
                    n.DocArticles.Any(a => a.ArchiveCode.Contains(filter.Description)));
            }


            var docs = await query.Select(n => new DocDto
            {
                Id = n.Id,
                SellerId = n.SellerId,
                PeriodId = n.PeriodId,
                TypeId = n.TypeId,
                AtfNumber = n.AtfNumber,
                AutoDocNumber = n.AutoDocNumber,
                DocNumber = n.DocNumber,
                DocDate = n.DocDate,
                Description = n.Description,

                Bed = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bed),
                Bes = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bes),
                Ekhtelaf = n.DocArticles.Sum(s => s.Bed - s.Bes),

                SubsystemId = n.SubsystemId,
                SubsystemRef = n.SubsystemRef,
                StatusId = n.StatusId,
                IsDeleted = n.IsDeleted,
                LastUpdateDate = n.LastUpdateDate,
                EditorUserName = n.EditorUserName,
                CreatorUserName = n.CreatorUserName,
                CreateDate = n.CreateDate,

                Articles = n.DocArticles.Where(a => !a.IsDeleted).Select(a => new DocArticleDto
                {
                    Id = a.Id,
                    DocId = a.DocId,
                    PeriodId = a.PeriodId,
                    DocNumber = a.Doc.DocNumber,
                    DocDate = a.Doc.DocDate,
                    KolId = a.KolId,
                    KolName = a.Moein.MoeinKol.KolName,
                    MoeinCode = a.Moein.MoeinCode,
                    MoeinName = a.Moein.MoeinName,
                    Tafsil4Id = a.Tafsil4Id,
                    Tafsil4Name = a.Tafsil4Name,
                    Tafsil5Id = a.Tafsil5Id,
                    Tafsil5Name = a.Tafsil5Name,
                    Tafsil6Id = a.Tafsil6Id,
                    Tafsil6Name = a.Tafsil6Name,
                    Tafsil7Id = a.Tafsil7Id,
                    Tafsil7Name = a.Tafsil7Name,
                    Tafsil8Id = a.Tafsil8Id,
                    Tafsil8Name = a.Tafsil8Name,
                    MoeinId = a.MoeinId,
                    RowNumber = a.RowNumber,
                    Comment = a.Comment,
                    ArchiveCode = a.ArchiveCode,
                    SellerId = a.SellerId,
                    Amount = a.Amount,
                    Bed = a.Bed,
                    Bes = a.Bes,
                    CreatorUserName = a.CreatorUserName,
                    CreateDate = a.CreateDate,
                    LastUpdateDate = a.LastUpdateDate,
                    EditorUserName = a.EditorUserName,
                    IsDeleted = a.IsDeleted,
                    ProjectId = a.ProjectId,
                    ProjectName = a.Project == null ? null : a.Project.ProjectName,
                    BatchNumber = a.AccountantRemark,
                }).ToList(),

            }).OrderBy(n => n.DocNumber)
            .ToListAsync();

            return docs;
        }
        public IQueryable<DocDto> GetDocs(DocFilterDto filter)
        {

            var query = _db.Acc_Documents
                .AsNoTracking()
                .Include(n => n.DocArticles.Where(a => !a.IsDeleted))
                    .ThenInclude(n => n.Moein)
                .Where(x =>
                    x.SellerId == filter.SellerId &&
                    x.PeriodId == filter.PeriodId &&
                    !x.IsDeleted)
                .AsQueryable();

            if (filter.docType != null)
            {
                query = query.Where(n => n.StatusId == filter.docType.Value);
            }

            if (!string.IsNullOrEmpty(filter.strStartDate))
            {
                var startDate = filter.strStartDate.PersianToLatin();
                query = query.Where(n => n.DocDate >= startDate);
            }

            if (!string.IsNullOrEmpty(filter.strEndDate))
            {
                var endDate = filter.strEndDate.PersianToLatin();
                query = query.Where(n => n.DocDate <= endDate);
            }

            if (filter.FromDocNumer > 0)
            {
                query = query.Where(n => n.DocNumber >= filter.FromDocNumer);
            }

            if (filter.ToDocNumer > 0)
            {
                query = query.Where(n => n.DocNumber <= filter.ToDocNumer);
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(n =>
                    n.Description.Contains(filter.Description) ||
                    n.DocArticles.Any(a => a.ArchiveCode.Contains(filter.Description)));
            }

            var docs = query.Select(n => new DocDto
            {
                Id = n.Id,
                SellerId = n.SellerId,
                PeriodId = n.PeriodId,
                TypeId = n.TypeId,
                AtfNumber = n.AtfNumber,
                AutoDocNumber = n.AutoDocNumber,
                DocNumber = n.DocNumber,
                DocDate = n.DocDate,
                Description = n.Description,

                Bed = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bed),
                Bes = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bes),
                Ekhtelaf = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bed - s.Bes),

                SubsystemId = n.SubsystemId,
                SubsystemRef = n.SubsystemRef,
                StatusId = n.StatusId,
                IsDeleted = n.IsDeleted,
                LastUpdateDate = n.LastUpdateDate,
                EditorUserName = n.EditorUserName,
                CreatorUserName = n.CreatorUserName,
                CreateDate = n.CreateDate,

                Articles = n.DocArticles.Where(a => !a.IsDeleted).Select(a => new DocArticleDto
                {
                    Id = a.Id,
                    DocId = a.DocId,
                    PeriodId = a.PeriodId,
                    KolId = a.KolId,
                    KolName = a.Moein.MoeinKol.KolName,
                    MoeinCode = a.Moein.MoeinCode,
                    MoeinName = a.Moein.MoeinName,
                    Tafsil4Id = a.Tafsil4Id,
                    Tafsil4Name = a.Tafsil4Name,
                    Tafsil5Id = a.Tafsil5Id,
                    Tafsil5Name = a.Tafsil5Name,
                    Tafsil6Id = a.Tafsil6Id,
                    Tafsil6Name = a.Tafsil6Name,
                    Tafsil7Id = a.Tafsil7Id,
                    Tafsil7Name = a.Tafsil7Name,
                    Tafsil8Id = a.Tafsil8Id,
                    Tafsil8Name = a.Tafsil8Name,
                    MoeinId = a.MoeinId,
                    RowNumber = a.RowNumber,
                    Comment = a.Comment,
                    ArchiveCode = a.ArchiveCode,
                    SellerId = a.SellerId,
                    Amount = a.Amount,
                    Bed = a.Bed,
                    Bes = a.Bes,
                    CreatorUserName = a.CreatorUserName,
                    CreateDate = a.CreateDate,
                    LastUpdateDate = a.LastUpdateDate,
                    EditorUserName = a.EditorUserName,
                    IsDeleted = a.IsDeleted,
                    ProjectId = a.ProjectId,
                    ProjectName = a.Project == null ? null : a.Project.ProjectName,
                    BatchNumber = a.AccountantRemark,
                }).ToList(),

            }).OrderBy(n => n.DocNumber)
            .AsQueryable();

            return docs;
        }
        public async Task<List<DocDto>> GetDeletedDocsAsync(DocFilterDto filter)
        {
            var query = _db.Acc_Documents
                 .AsNoTracking()
                .Include(n => n.DocArticles).ThenInclude(n => n.Moein)
                .Where(x =>
                x.SellerId == filter.SellerId
                && x.PeriodId == filter.PeriodId
                && x.IsDeleted == true).AsQueryable();

            if (filter.docType != null)
                query = query.Where(n => n.StatusId == filter.docType.Value);
            if (filter.StartDate != null)
            {
                filter.StartDate = filter.StartDate.Value.ToMiladiDate();
                query = query.Where(n => n.DocDate >= filter.StartDate.Value.Date);
            }

            if (filter.EndDate != null)
            {
                filter.EndDate = filter.EndDate.Value.ToMiladiDate();
                query = query.Where(n => n.DocDate <= filter.EndDate.Value.Date);
            }

            if (filter.FromDocNumer > 0)
                query = query.Where(n => n.DocNumber >= filter.FromDocNumer);
            if (filter.ToDocNumer > 0)
                query = query.Where(n => n.DocNumber <= filter.ToDocNumer);
            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(n => n.Description.Contains(filter.Description) ||
                                         n.DocArticles.Any(a => a.ArchiveCode.Contains(filter.Description)));
            }


            var docs = await query.Select(n => new DocDto
            {
                Id = n.Id,
                SellerId = n.SellerId,
                PeriodId = n.PeriodId,
                TypeId = n.TypeId,
                AtfNumber = n.AtfNumber,
                AutoDocNumber = n.AutoDocNumber,
                DocNumber = n.DocNumber,
                DocDate = n.DocDate,
                Description = n.Description,

                Bed = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bed),
                Bes = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bes),
                Ekhtelaf = n.DocArticles.Sum(s => s.Bed) - n.DocArticles.Sum(s => s.Bes),

                SubsystemId = n.SubsystemId,
                SubsystemRef = n.SubsystemRef,
                StatusId = n.StatusId,
                IsDeleted = n.IsDeleted,
                DeletedDate = n.DeletedDate,
                DeleteUser = n.DeleteUserName,
                LastUpdateDate = n.LastUpdateDate,
                EditorUserName = n.EditorUserName,
                CreatorUserName = n.CreatorUserName,
                CreateDate = n.CreateDate,
            }).OrderBy(n => n.DocNumber).ToListAsync();

            return docs;
        }
        public async Task<Acc_Document?> FindDocAsync(Guid id)
        {
            var docs = await _db.Acc_Documents.FindAsync(id);
            if (docs == null)
                return null;
            return docs;
        }
        public async Task<DocDto> GetDocDtoAsync(Guid id)
        {
            var doc = await _db.Acc_Documents
                 .AsNoTracking()
                 .Include(n => n.DocArticles)
                .Where(n => n.Id == id).Select(n => new DocDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    PeriodId = n.PeriodId,
                    TypeId = n.TypeId,
                    AtfNumber = n.AtfNumber,
                    AutoDocNumber = n.AutoDocNumber,
                    DocNumber = n.DocNumber,
                    DocDate = n.DocDate,
                    Description = n.Description,

                    Bed = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bed),
                    Bes = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bes),
                    Ekhtelaf = n.DocArticles.Sum(s => s.Bed) - n.DocArticles.Sum(s => s.Bes),

                    SubsystemId = n.SubsystemId,
                    SubsystemRef = n.SubsystemRef,
                    StatusId = n.StatusId,
                    IsDeleted = n.IsDeleted,
                    LastUpdateDate = n.LastUpdateDate,
                    EditorUserName = n.EditorUserName,
                    CreatorUserName = n.CreatorUserName,
                    CreateDate = n.CreateDate,
                    Articles = n.DocArticles.Where(a => a.IsDeleted == false).Select(a => new DocArticleDto
                    {
                        Id = a.Id,
                        DocId = a.DocId,
                        PeriodId = a.PeriodId,
                        KolId = a.KolId,
                        KolName = a.Moein.MoeinKol.KolName,
                        MoeinCode = a.Moein.MoeinCode,
                        MoeinName = a.Moein.MoeinName,
                        Tafsil4Id = a.Tafsil4Id,
                        Tafsil4Name = a.Tafsil4Name,
                        Tafsil5Id = a.Tafsil5Id,
                        Tafsil5Name = a.Tafsil5Name,
                        Tafsil6Id = a.Tafsil6Id,
                        Tafsil6Name = a.Tafsil6Name,
                        Tafsil7Id = a.Tafsil7Id,
                        Tafsil7Name = a.Tafsil7Name,
                        Tafsil8Id = a.Tafsil8Id,
                        Tafsil8Name = a.Tafsil8Name,
                        MoeinId = a.MoeinId,
                        RowNumber = a.RowNumber,
                        Comment = a.Comment,
                        ArchiveCode = a.ArchiveCode,
                        Amount = a.Amount,
                        Bed = a.Bed,
                        Bes = a.Bes,

                        CreatorUserName = a.CreatorUserName,
                        CreateDate = a.CreateDate,
                        LastUpdateDate = a.LastUpdateDate,
                        EditorUserName = a.EditorUserName,
                        IsDeleted = a.IsDeleted,
                        ProjectId = a.ProjectId,
                        ProjectName = a.Project == null ? null : a.Project.ProjectName,
                        BatchNumber = a.AccountantRemark,

                    }).OrderBy(a => a.RowNumber).ToList(),

                }).SingleOrDefaultAsync();

            return doc;

        }

        public async Task<DocDto> GetLastUserDocAsync(long sellerId, int periodId, string username)
        {
            var doc = await _db.Acc_Documents
                 .AsNoTracking()
                 .Include(n => n.DocArticles)
                .Where(n => n.SellerId == sellerId
                && n.PeriodId == periodId
                && !n.IsDeleted
                && n.CreatorUserName == username)
                .Select(n => new DocDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    PeriodId = n.PeriodId,
                    TypeId = n.TypeId,
                    AtfNumber = n.AtfNumber,
                    AutoDocNumber = n.AutoDocNumber,
                    DocNumber = n.DocNumber,
                    DocDate = n.DocDate,
                    Description = n.Description,

                    Bed = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bed),
                    Bes = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bes),
                    Ekhtelaf = n.DocArticles.Sum(s => s.Bed) - n.DocArticles.Sum(s => s.Bes),

                    SubsystemId = n.SubsystemId,
                    SubsystemRef = n.SubsystemRef,
                    StatusId = n.StatusId,
                    IsDeleted = n.IsDeleted,
                    LastUpdateDate = n.LastUpdateDate,
                    EditorUserName = n.EditorUserName,
                    CreatorUserName = n.CreatorUserName,
                    CreateDate = n.CreateDate,
                    Articles = n.DocArticles.Where(a => a.IsDeleted == false).Select(a => new DocArticleDto
                    {
                        Id = a.Id,
                        DocId = a.DocId,
                        PeriodId = a.PeriodId,
                        KolId = a.KolId,
                        KolName = a.Moein.MoeinKol.KolName,
                        MoeinCode = a.Moein.MoeinCode,
                        MoeinName = a.Moein.MoeinName,
                        Tafsil4Id = a.Tafsil4Id,
                        Tafsil4Name = a.Tafsil4Name,
                        Tafsil5Id = a.Tafsil5Id,
                        Tafsil5Name = a.Tafsil5Name,
                        Tafsil6Id = a.Tafsil6Id,
                        Tafsil6Name = a.Tafsil6Name,
                        Tafsil7Id = a.Tafsil7Id,
                        Tafsil7Name = a.Tafsil7Name,
                        Tafsil8Id = a.Tafsil8Id,
                        Tafsil8Name = a.Tafsil8Name,
                        MoeinId = a.MoeinId,
                        RowNumber = a.RowNumber,
                        Comment = a.Comment,
                        ArchiveCode = a.ArchiveCode,
                        Amount = a.Amount,
                        Bed = a.Bed,
                        Bes = a.Bes,

                        CreatorUserName = a.CreatorUserName,
                        CreateDate = a.CreateDate,
                        LastUpdateDate = a.LastUpdateDate,
                        EditorUserName = a.EditorUserName,
                        IsDeleted = a.IsDeleted,

                        ProjectId = a.ProjectId,
                        ProjectName = a.Project == null ? null : a.Project.ProjectName,
                        BatchNumber = a.AccountantRemark,

                    }).OrderBy(a => a.RowNumber).ToList(),

                }).OrderBy(n => n.CreateDate).LastOrDefaultAsync();

            return doc;

        }
        public async Task<DocDto> GetDocWithNumberAsync(long sellerId, int priodId, int docNumber)
        {
            int maxnumber = await _db.Acc_Documents.AsNoTracking()
               .Where(n => !n.IsDeleted && n.SellerId == sellerId && n.PeriodId == priodId)
               .Select(n => n.DocNumber).MaxAsync<int>();

            if (docNumber <= 0)
                docNumber = 1;
            if (docNumber > maxnumber)
                docNumber = maxnumber;

            var doc = await _db.Acc_Documents
                 .AsNoTracking()
                 .Include(n => n.DocArticles)
                .Where(n => n.DocNumber == docNumber && n.SellerId == sellerId && n.PeriodId == priodId && !n.IsDeleted)
                .Select(n => new DocDto
                {
                    Id = n.Id,
                    SellerId = n.SellerId,
                    PeriodId = n.PeriodId,
                    TypeId = n.TypeId,
                    AtfNumber = n.AtfNumber,
                    AutoDocNumber = n.AutoDocNumber,
                    DocNumber = n.DocNumber,
                    DocDate = n.DocDate,
                    Description = n.Description,

                    Bed = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bed),
                    Bes = n.DocArticles.Where(x => !x.IsDeleted).Sum(s => s.Bes),
                    Ekhtelaf = n.DocArticles.Sum(s => s.Bed) - n.DocArticles.Sum(s => s.Bes),

                    SubsystemId = n.SubsystemId,
                    SubsystemRef = n.SubsystemRef,
                    StatusId = n.StatusId,
                    IsDeleted = n.IsDeleted,
                    LastUpdateDate = n.LastUpdateDate,
                    EditorUserName = n.EditorUserName,
                    CreatorUserName = n.CreatorUserName,
                    CreateDate = n.CreateDate,
                    Articles = n.DocArticles.Where(a => a.IsDeleted == false).Select(a => new DocArticleDto
                    {
                        Id = a.Id,
                        DocId = a.DocId,
                        PeriodId = a.PeriodId,
                        KolId = a.KolId,
                        KolName = a.Moein.MoeinKol.KolName,
                        MoeinCode = a.Moein.MoeinCode,
                        MoeinName = a.Moein.MoeinName,
                        Tafsil4Id = a.Tafsil4Id,
                        Tafsil4Name = a.Tafsil4Name,
                        Tafsil5Id = a.Tafsil5Id,
                        Tafsil5Name = a.Tafsil5Name,
                        Tafsil6Id = a.Tafsil6Id,
                        Tafsil6Name = a.Tafsil6Name,
                        Tafsil7Id = a.Tafsil7Id,
                        Tafsil7Name = a.Tafsil7Name,
                        Tafsil8Id = a.Tafsil8Id,
                        Tafsil8Name = a.Tafsil8Name,
                        MoeinId = a.MoeinId,
                        RowNumber = a.RowNumber,
                        Comment = a.Comment,
                        ArchiveCode = a.ArchiveCode,
                        Amount = a.Amount,
                        Bed = a.Bed,
                        Bes = a.Bes,

                        CreatorUserName = a.CreatorUserName,
                        CreateDate = a.CreateDate,
                        LastUpdateDate = a.LastUpdateDate,
                        EditorUserName = a.EditorUserName,
                        IsDeleted = a.IsDeleted,

                        ProjectId = a.ProjectId,
                        ProjectName = a.Project == null ? null : a.Project.ProjectName,
                        BatchNumber = a.AccountantRemark,

                    }).OrderBy(a => a.RowNumber).ToList(),

                }).FirstOrDefaultAsync();

            return doc;

        }
        public async Task<clsResult> CreateDocHeaderAsync(DocDto_AddNew dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            if (!dto.SellerId.HasValue || !dto.PeriodId.HasValue)
            {
                result.Message = "اطلاعات دوره مالی یا شرکت فعال، بدرستی انتخاب نشده است";
                return result;
            }
            //برای سندهای ادغامی، شماره تکراری کنترل نمیشه.
            if (dto.TypeId != 100)
            {
                if (await IsDupplicateDocNumberAsync(dto.SellerId.Value, dto.PeriodId.Value, dto.DocNumber))
                {
                    result.Message = "شماره سند تکراری است";
                    return result;
                }
            }
            else
                dto.TypeId = 1; //نوع سند به سند روزانه باید تغییر کند

            var period = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == dto.PeriodId.Value);
            if (period == null)
            {
                result.Message = "اطلاعات مربوط به دوره مالی یافت نشد";
                return result;
            }
            if (dto.DocDate > period.EndDate || dto.DocDate < period.StartDate)
            {
                result.Message = $"تاریخ سند خارج از بازه دوره مالی {period.Name} است";
                return result;
            }
            if (period.SellerId != dto.SellerId)
            {
                result.Message = $"دوره مالی انتخاب شده معتبر نمی باشد، از فرم مربوط به انتخاب دوره مالی، دوره موردنظرتان را مجددا انتخاب کنید";
                return result;
            }

            Acc_Document doc = new Acc_Document();
            doc.Id = dto.Id;
            doc.AutoDocNumber = await DocAutoNumberGeneratorAsync(dto.SellerId.Value, dto.PeriodId.Value);
            doc.DocNumber = dto.DocNumber;
            doc.AtfNumber = dto.AtfNumber == null ? 0 : dto.AtfNumber.Value;
            doc.DocDate = dto.DocDate.Value;
            doc.Description = dto.Description;
            doc.TypeId = dto.TypeId;
            doc.StatusId = 1;
            doc.CreateDate = DateTime.Now;
            doc.CreatorUserName = dto.CreatorUserName;

            doc.SellerId = dto.SellerId.Value;
            doc.PeriodId = dto.PeriodId.Value;

            _db.Acc_Documents.Add(doc);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "سند حسابداری با موفقیت ایجاد شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ایجاد سند رخ داده است. /n" + x.Message;
                return result;
            }
            return result;
        }
        public async Task<clsResult> SaveDraftDocHeaderAsync(DocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (await IsDupplicateDocNumberAsync(dto))
            {
                result.Message = "شماره سند تکراری است";
                return result;
            }
            var period = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == dto.PeriodId);
            if (period == null)
            {
                result.Message = "اطلاعات مربوط به دوره مالی یافت نشد";
                return result;
            }
            if (dto.DocDate > period.EndDate || dto.DocDate < period.StartDate)
            {
                result.Message = $"تاریخ سند خارج از بازه دوره مالی {period.Name} است";
                return result;
            }

            Acc_Document? doc = await _db.Acc_Documents.FindAsync(dto.Id);
            if (doc == null) { return result; }

            doc.DocNumber = dto.DocNumber;
            doc.AtfNumber = dto.AtfNumber;
            doc.DocDate = dto.DocDate.Value;
            doc.Description = dto.Description;
            doc.TypeId = dto.TypeId;
            doc.StatusId = 1;
            doc.LastUpdateDate = DateTime.Now;
            doc.EditorUserName = dto.EditorUserName;

            doc.SellerId = dto.SellerId;
            doc.PeriodId = dto.PeriodId;
            doc.StatusId = dto.StatusId;

            _db.Acc_Documents.Update(doc);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "سند حسابداری با موفقیت ذخیره شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ذخیره سند رخ داده است. /n" + x.Message;
                return result;
            }
            return result;
        }
        public async Task<clsResult> DeleteDocAsync(Guid Id, string username)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            Acc_Document? doc = await _db.Acc_Documents.FindAsync(Id);
            if (doc == null)
            {
                result.Message = "سند موردنظر یافت نشد";
                return result;
            }

            doc.IsDeleted = true;
            doc.DeleteUserName = username;
            doc.DeletedDate = DateTime.Now;

            var articles = await _db.Acc_Articles
                .Where(n => n.DocId == Id).ToListAsync();
            foreach (var item in articles)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                item.DeleteUserName = username;
            }

            _db.Acc_Documents.Update(doc);
            _db.Acc_Articles.UpdateRange(articles);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    if (doc.SubsystemId == 2)
                    {
                        int updatedRecords = await _db.KPOldSystemSales
                            .Where(n => n.DocId == Id)
                            .ExecuteUpdateAsync(s => s
                            .SetProperty(n => n.DocId, (Guid?)null)
                            .SetProperty(n => n.DocNumber, (int?)null));
                        await _db.SaveChangesAsync();

                    }
                    result.Success = true;
                    result.Message = "سند حسابداری با موفقیت حذف شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان حذف سند رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> DeleteDocsAsync(Guid[] Ids, string username)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            List<Acc_Document> docs = await _db.Acc_Documents.Where(n => Ids.Contains(n.Id)).ToListAsync();
            List<Acc_Article> articles = new List<Acc_Article>();

            foreach (var doc in docs)
            {
                doc.IsDeleted = true;
                doc.DeleteUserName = username;
                doc.DeletedDate = DateTime.Now;

                var docaArticles = await _db.Acc_Articles
               .Where(n => n.DocId == doc.Id).ToListAsync();
                foreach (var item in articles)
                {
                    item.IsDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.DeleteUserName = username;
                }
                articles.AddRange(docaArticles);
                // حذف شماره سند در سیستم فروش
                if (doc.SubsystemId == 2)
                {
                    int updatedRecords = await _db.KPOldSystemSales
                        .Where(n => n.DocId == doc.Id)
                        .ExecuteUpdateAsync(s => s
                        .SetProperty(n => n.DocId, (Guid?)null)
                        .SetProperty(n => n.DocNumber, (int?)null));
                    await _db.SaveChangesAsync();
                }
            }

            _db.Acc_Documents.UpdateRange(docs);
            _db.Acc_Articles.UpdateRange(articles);

            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "حذف اسناد حسابداری با موفقیت انجام شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان حذف اسناد رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> BulkDeleteDocumentsAsync(Guid[] ids, string username)
        {
            clsResult result = new clsResult
            {
                Success = false,
                ShowMessage = true
            };

            var docs = await _db.Acc_Documents.Where(n => ids.Contains(n.Id)).ToListAsync();
            var articles = new List<Acc_Article>();

            foreach (var doc in docs)
            {
                doc.IsDeleted = true;
                doc.DeleteUserName = username;
                doc.DeletedDate = DateTime.Now;

                var docArticles = await _db.Acc_Articles.Where(n => n.DocId == doc.Id).ToListAsync();
                foreach (var item in docArticles)
                {
                    item.IsDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.DeleteUserName = username;
                }
                articles.AddRange(docArticles);

                // حذف شماره سند در سیستم فروش
                if (doc.SubsystemId == 2)
                {
                    int updatedRecords = await _db.KPOldSystemSales
                        .Where(n => n.DocId == doc.Id)
                        .ExecuteUpdateAsync(s => s
                        .SetProperty(n => n.DocId, (Guid?)null)
                        .SetProperty(n => n.DocNumber, (int?)null));
                    await _db.SaveChangesAsync();
                }
            }

            try
            {
                await _db.BulkUpdateAsync(docs);
                await _db.BulkUpdateAsync(articles);

                result.Success = true;
                result.Message = "حذف اسناد حسابداری با موفقیت انجام شد";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان حذف اسناد رخ داده است.\n{ex.Message}";
                return result;
            }
        }

        public async Task<clsResult> DeleteAllPeriodDocAsync(long sellerId, int periodId)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            List<Acc_Document> docs = await _db.Acc_Documents.Where(n => n.SellerId == sellerId && n.PeriodId == periodId).ToListAsync();
            List<Acc_Article> Arts = await _db.Acc_Articles.Where(n => n.Doc.SellerId == sellerId && n.Doc.PeriodId == periodId).ToListAsync();


            _db.Acc_Articles.RemoveRange(Arts);
            _db.Acc_Documents.RemoveRange(docs);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "حذف گروهی اسناد با موفقیت انجام شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان حذف اسناد رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> UndeleteDocAsync(Guid Id, string username)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;

            Acc_Document? doc = await _db.Acc_Documents.FindAsync(Id);
            if (doc == null)
            {
                result.Message = "سند موردنظر یافت نشد";
                return result;
            }

            doc.IsDeleted = false;
            doc.DeleteUserName = null;
            doc.DeletedDate = null;
            doc.LastUpdateDate = DateTime.Now;
            doc.EditorUserName = username;

            var articles = await _db.Acc_Articles
                .Where(n => n.DocId == Id).ToListAsync();
            foreach (var item in articles)
            {
                item.IsDeleted = false;
                item.DeletedDate = null;
                item.DeleteUserName = null;
                item.LastUpdateDate = DateTime.Now;
                item.EditorUserName = username;
            }

            _db.Acc_Documents.Update(doc);
            _db.Acc_Articles.UpdateRange(articles);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    if (doc.SubsystemId == 2)
                    {
                        int updatedRecords = await _db.KPOldSystemSales
                            .Where(n => n.DocId == Id)
                            .ExecuteUpdateAsync(s => s
                            .SetProperty(n => n.DocId, (Guid?)null)
                            .SetProperty(n => n.DocNumber, (int?)null));
                        await _db.SaveChangesAsync();

                    }
                    result.Success = true;
                    result.Message = "بازیابی سند حسابداری با موفقیت انجام شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان بازیابی سند رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }

        //Article
        public async Task<clsResult> AddArticleAsync(DocArticleDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.Bed == 0 && dto.Bes == 0)
            {
                result.Message = "یکی از فیلدهای بدهکار یا بستانکار باید دارای مقدار بالای صفر باشد.";
                return result;
            }
            if (dto.Bed > 0)
            {
                if (dto.Bes > 0)
                {
                    result.Message = "مقادیر مربوط به بدهکار و بستانکار، نمی تواند همزمان دارای مقدار بالاتر از صفر باشند";
                    return result;
                }
            }
            if (dto.Bes > 0)
            {
                if (dto.Bed > 0)
                {
                    result.Message = "مقادیر مربوط به بدهکار و بستانکار، نمی تواند همزمان دارای مقدار بالاتر از صفر باشند";
                    return result;
                }
            }
            if (string.IsNullOrEmpty(dto.Id.ToString()))
            {
                result.ShowMessage = false;
                return result;
            }

            Acc_Article article = new Acc_Article();
            article.Id = dto.Id;
            article.DocId = dto.DocId;
            article.SellerId = dto.SellerId;
            article.PeriodId = dto.PeriodId;

            article.RowNumber = dto.RowNumber;

            article.KolId = await _coding.GetKolIdByMoeinIdAsync(dto.MoeinId);
            article.MoeinId = dto.MoeinId;
            article.Tafsil4Id = dto.Tafsil4Id == 0 ? null : dto.Tafsil4Id;
            article.Tafsil5Id = dto.Tafsil5Id == 0 ? null : dto.Tafsil5Id;
            article.Tafsil6Id = dto.Tafsil6Id == 0 ? null : dto.Tafsil6Id;
            article.Tafsil7Id = dto.Tafsil7Id == 0 ? null : dto.Tafsil7Id;
            article.Tafsil8Id = dto.Tafsil8Id == 0 ? null : dto.Tafsil8Id;
            article.Tafsil4Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil4Id);
            article.Tafsil5Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil5Id);
            article.Tafsil6Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil6Id);
            article.Tafsil7Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil7Id);
            article.Tafsil8Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil8Id);

            article.Bes = dto.Bes;
            article.Bed = dto.Bed;
            article.Amount = dto.Bed > 0 ? dto.Bed : dto.Bes;
            article.Comment = dto.Comment;
            article.ArchiveCode = dto.ArchiveCode;

            article.CreatorUserName = dto.CreatorUserName;
            article.CreateDate = DateTime.Now;
            article.IsDeleted = false;
            article.ProjectId = dto.ProjectId;

            _db.Acc_Articles.Add(article);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "آرتیکل سند با موفقیت ثبت شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ایجاد آرتیکل سند رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> AddRangeArticlesAsync(List<DocArticleDto> arts)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (arts.Count == 0)
            {
                result.Message = "اطلاعاتی جهت ثبت وجود ندارد.";
                return result;
            }
            List<Acc_Article> addList = new List<Acc_Article>();
            foreach (var dto in arts)
            {
                if (dto.Bed == 0 && dto.Bes == 0)
                {
                    result.Message = "یکی از فیلدهای بدهکار یا بستانکار باید دارای مقدار بالای صفر باشد.";
                    return result;
                }
                if (dto.Bed > 0 && dto.Bes > 0)
                {
                    result.Message = "مقادیر مربوط به بدهکار و بستانکار، نمی تواند همزمان دارای مقدار بالاتر از صفر باشند";
                    return result;
                }

                if (string.IsNullOrEmpty(dto.Id.ToString()))
                {
                    result.ShowMessage = false;
                    return result;
                }

                Acc_Article article = new Acc_Article();
                article.Id = dto.Id;
                article.DocId = dto.DocId;
                article.SellerId = dto.SellerId;
                article.PeriodId = dto.PeriodId;

                article.RowNumber = dto.RowNumber;

                article.KolId = await _coding.GetKolIdByMoeinIdAsync(dto.MoeinId);
                article.MoeinId = dto.MoeinId;
                article.Tafsil4Id = dto.Tafsil4Id == 0 ? null : dto.Tafsil4Id;
                article.Tafsil5Id = dto.Tafsil5Id == 0 ? null : dto.Tafsil5Id;
                article.Tafsil6Id = dto.Tafsil6Id == 0 ? null : dto.Tafsil6Id;
                article.Tafsil7Id = dto.Tafsil7Id == 0 ? null : dto.Tafsil7Id;
                article.Tafsil8Id = dto.Tafsil8Id == 0 ? null : dto.Tafsil8Id;
                article.Tafsil4Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil4Id);
                article.Tafsil5Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil5Id);
                article.Tafsil6Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil6Id);
                article.Tafsil7Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil7Id);
                article.Tafsil8Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil8Id);

                article.Bes = dto.Bes;
                article.Bed = dto.Bed;
                article.Amount = dto.Bed > 0 ? dto.Bed : dto.Bes;
                article.Comment = dto.Comment;
                article.ArchiveCode = dto.ArchiveCode;

                article.CreatorUserName = dto.CreatorUserName;
                article.CreateDate = DateTime.Now;
                article.IsDeleted = false;

                article.ProjectId = dto.ProjectId;

                addList.Add(article);

            }


            _db.Acc_Articles.AddRange(addList);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "عملیات با موفقیت انجام شد.";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ایجاد آرتیکل سند رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> UpdateArticleAsync(DocArticleDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (dto.Bed == 0 && dto.Bes == 0)
            {
                result.Message = "یکی از فیلدهای بدهکار یا بستانکار باید دارای مقدار بالای صفر باشد.";
                return result;
            }
            if (dto.Bed > 0)
            {
                if (dto.Bes > 0)
                {
                    result.Message = "مقادیر مربوط به بدهکار و بستانکار، نمی تواند همزمان دارای مقدار بالاتر از صفر باشند";
                    return result;
                }
            }
            if (dto.Bes > 0)
            {
                if (dto.Bed > 0)
                {
                    result.Message = "مقادیر مربوط به بدهکار و بستانکار، نمی تواند همزمان دارای مقدار بالاتر از صفر باشند";
                    return result;
                }
            }

            Acc_Article? article = await _db.Acc_Articles.SingleOrDefaultAsync(n => n.Id == dto.Id);
            if (article == null)
            {
                result.Message = "آرتیکل موردنظر یافت نشد";
                return result;
            }
            article.DocId = dto.DocId;
            article.SellerId = dto.SellerId;
            article.PeriodId = dto.PeriodId;

            article.RowNumber = dto.RowNumber;

            article.KolId = await _coding.GetKolIdByMoeinIdAsync(dto.MoeinId);
            article.MoeinId = dto.MoeinId;
            article.Tafsil4Id = dto.Tafsil4Id == 0 ? null : dto.Tafsil4Id;
            article.Tafsil5Id = dto.Tafsil5Id == 0 ? null : dto.Tafsil5Id;
            article.Tafsil6Id = dto.Tafsil6Id == 0 ? null : dto.Tafsil6Id;
            article.Tafsil7Id = dto.Tafsil7Id == 0 ? null : dto.Tafsil7Id;
            article.Tafsil8Id = dto.Tafsil8Id == 0 ? null : dto.Tafsil8Id;
            article.Tafsil4Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil4Id);
            article.Tafsil5Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil5Id);
            article.Tafsil6Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil6Id);
            article.Tafsil7Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil7Id);
            article.Tafsil8Name = await _coding.GetTafsilNameByIdAsync(dto.Tafsil8Id);

            article.Bes = dto.Bes;
            article.Bed = dto.Bed;
            article.Amount = dto.Bed > 0 ? dto.Bed : dto.Bes;
            article.Comment = dto.Comment;
            article.ArchiveCode = dto.ArchiveCode;

            article.EditorUserName = dto.EditorUserName;
            article.LastUpdateDate = DateTime.Now;
            article.IsDeleted = false;

            article.ProjectId = dto.ProjectId;

            _db.Acc_Articles.Update(article);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "آرتیکل موردنظر با موفقیت ذخیره شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ویرایش آرتیکل سند رخ داده است. \n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<DocArticleDto> GetDocArticleDtoAsync(Guid Id)
        {
            var article = await _db.Acc_Articles
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Include(n => n.Project)
                .Where(n => n.IsDeleted == false && n.Id == Id)
                .SingleOrDefaultAsync();
            if (article == null) return null;

            DocArticleDto articleDto = new DocArticleDto();
            articleDto.Id = article.Id;
            articleDto.DocId = article.DocId;
            articleDto.PeriodId = article.PeriodId;
            articleDto.KolId = article.KolId;
            articleDto.KolName = article.Moein.MoeinKol.KolName;
            articleDto.MoeinCode = article.Moein.MoeinCode;
            articleDto.MoeinName = article.Moein.MoeinName;
            articleDto.Tafsil4Id = article.Tafsil4Id;
            articleDto.Tafsil4Name = article.Tafsil4Name;
            articleDto.Tafsil5Id = article.Tafsil5Id;
            articleDto.Tafsil5Name = article.Tafsil5Name;
            articleDto.Tafsil6Id = article.Tafsil6Id;
            articleDto.Tafsil6Name = article.Tafsil6Name;
            articleDto.Tafsil7Id = article.Tafsil7Id;
            articleDto.Tafsil7Name = article.Tafsil7Name;
            articleDto.Tafsil8Id = article.Tafsil8Id;
            articleDto.Tafsil8Name = article.Tafsil8Name;
            articleDto.MoeinId = article.MoeinId;
            articleDto.RowNumber = article.RowNumber;
            articleDto.Comment = article.Comment;
            articleDto.Amount = article.Amount;
            articleDto.Bed = article.Bed;
            articleDto.strBed = article.Bed.ToPrice();
            articleDto.Bes = article.Bes;
            articleDto.strBes = article.Bes.ToPrice();
            articleDto.CreatorUserName = article.CreatorUserName;
            articleDto.CreateDate = article.CreateDate;
            articleDto.LastUpdateDate = article.LastUpdateDate;
            articleDto.EditorUserName = article.EditorUserName;
            articleDto.IsDeleted = article.IsDeleted;
            articleDto.ProjectId = article.ProjectId;
            articleDto.ProjectName = article.Project?.ProjectName;
            articleDto.ArchiveCode = article.ArchiveCode;

            return articleDto;
        }
        public async Task<List<DocArticleDto>> GetArticlesDtoByIdAsync(List<Guid> Id)
        {
            var articles = await _db.Acc_Articles
                .Include(n => n.Doc)
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Include(n => n.Project)
                .Where(n => n.IsDeleted == false & Id.Contains(n.Id))
                .Select(article => new DocArticleDto
                {
                    Id = article.Id,
                    DocId = article.DocId,
                    PeriodId = article.PeriodId,
                    DocNumber = article.Doc.DocNumber,
                    DocDate = article.Doc.DocDate,
                    KolId = article.KolId,
                    KolName = article.Moein.MoeinKol.KolName,
                    MoeinCode = article.Moein.MoeinCode,
                    MoeinName = article.Moein.MoeinName,
                    Tafsil4Id = article.Tafsil4Id,
                    Tafsil4Name = article.Tafsil4Name,
                    Tafsil5Id = article.Tafsil5Id,
                    Tafsil5Name = article.Tafsil5Name,
                    Tafsil6Id = article.Tafsil6Id,
                    Tafsil6Name = article.Tafsil6Name,
                    Tafsil7Id = article.Tafsil7Id,
                    Tafsil7Name = article.Tafsil7Name,
                    Tafsil8Id = article.Tafsil8Id,
                    Tafsil8Name = article.Tafsil8Name,
                    MoeinId = article.MoeinId,
                    RowNumber = article.RowNumber,
                    Comment = article.Comment,
                    Amount = article.Amount,
                    Bed = article.Bed,
                    strBed = article.Bed.ToPrice(),
                    Bes = article.Bes,
                    strBes = article.Bes.ToPrice(),
                    CreatorUserName = article.CreatorUserName,
                    CreateDate = article.CreateDate,
                    LastUpdateDate = article.LastUpdateDate,
                    EditorUserName = article.EditorUserName,
                    IsDeleted = article.IsDeleted,
                    ProjectId = article.ProjectId,
                    ProjectName = article.Project != null ? article.Project.ProjectName : "",
                    ArchiveCode = article.ArchiveCode,
                    BatchNumber = article.AccountantRemark,

                }).ToListAsync();

            return articles;
        }
        public async Task<clsResult> DeleteDocArticleAsync(Guid Id)
        {
            clsResult result = new clsResult();
            result.Success = false;

            var article = await _db.Acc_Articles
                 .Where(n => n.IsDeleted == false && n.Id == Id)
                 .SingleOrDefaultAsync();
            if (article == null)
            {
                result.Message = "آرتیکل موردنظر یافت نشد";
                return result;
            }
            var doc = await _db.Acc_Documents.SingleOrDefaultAsync(n => n.Id == article.DocId);
            if (doc == null)
            {
                result.Message = "سند مربوط به آرتیکل موردنظر یافت نشد";
                return result;
            }
            if (doc.StatusId == 3)
            {
                result.Message = "در اسناد قطعی شده امکان ویرایش وجود ندارد";
                return result;
            }
            doc.StatusId = 1;

            _db.Acc_Documents.Update(doc);
            _db.Acc_Articles.Remove(article);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    var articles = await _db.Acc_Articles.Where(n => n.DocId == article.DocId)
                        .Select(n => new { n.Id, rowNumber = n.RowNumber }).OrderBy(n => n.rowNumber).ToListAsync();
                    if (articles.Count > 0)
                    {
                        List<SetNumberDto> renumberList = new List<SetNumberDto>();
                        int row = 1;
                        foreach (var item in articles)
                        {
                            renumberList.Add(new SetNumberDto { Id = item.Id, RowNumber = row });
                            row++;
                        }

                        await ArticlesRenumberRowsAsync(renumberList);
                    }

                    result.Success = true;
                    result.Message = "آرتیکل موردنظر با موفقیت حذف شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان حذف آرتیکل سند رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> DeleteDocArticlesAsync(List<Guid> Ids, string userName)
        {
            clsResult result = new clsResult();
            result.Success = false;

            var article = await _db.Acc_Articles
                 .Where(n => n.IsDeleted == false && Ids.Contains(n.Id))
                 .ExecuteUpdateAsync(n =>
                 n.SetProperty(z => z.IsDeleted, true)
                 .SetProperty(z => z.DeleteUserName, userName)
                 .SetProperty(z => z.DeletedDate, DateTime.Now));

            try
            {
                await _db.SaveChangesAsync();
                result.Success = true;
                result.Message = "ردیف (های) انتخاب شده با موفقیت حف شد";
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان حذف آرتیکل سند رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> ArticlesRenumberRows(List<SetNumberDto> articlesList, string editorUserName)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (articlesList.Count == 0)
            {
                result.Message = "اطلاعاتی جهت ویرایش دریافت نشد";
                return result;
            }

            List<Acc_Article> articles = new List<Acc_Article>();
            foreach (var art in articlesList)
            {
                Acc_Article? article = await _db.Acc_Articles.SingleOrDefaultAsync(n => n.Id == art.Id);
                if (article != null)
                {
                    article.RowNumber = art.RowNumber;
                    article.EditorUserName = editorUserName;
                    article.LastUpdateDate = DateTime.Now;
                    articles.Add(article);
                }
            }

            _db.Acc_Articles.UpdateRange(articles);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "مرتب سازی ردیف های سند با موفقیت انجام شد";
                    result.ShowMessage = true;
                    result.updateType = 1;

                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان مرتب سازی آرتیکل های سند رخ داده است. \n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> ArticlesRenumberRowsAsync(List<SetNumberDto> articlesList)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (articlesList.Count == 0)
            {
                result.Message = "اطلاعاتی جهت ویرایش دریافت نشد";
                return result;
            }

            List<Acc_Article> articles = new List<Acc_Article>();
            foreach (var art in articlesList)
            {
                Acc_Article? article = await _db.Acc_Articles.SingleOrDefaultAsync(n => n.Id == art.Id);
                if (article != null)
                {
                    article.RowNumber = art.RowNumber;
                    articles.Add(article);
                }
            }

            _db.Acc_Articles.UpdateRange(articles);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "مرتب سازی ردیف های سند با موفقیت انجام شد";
                    result.ShowMessage = true;
                    result.updateType = 1;

                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان مرتب سازی آرتیکل های سند رخ داده است. \n" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> MoveToOtherDocAsyncAsync(MoveArticlesToDocDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;

            var articles = await _db.Acc_Articles.Where(n => dto.ArticlesId.Contains(n.Id)).ToListAsync();
            if (!articles.Any())
            {
                result.Message = "داده ای برای انتقال یافت نشد";
                return result;
            }
            Guid currentDocId = articles.FirstOrDefault().DocId;
            if (currentDocId == dto.DocId)
            {
                result.Message = "نمی توانید ردیف ها را به سند جاری منتقل کنید";
                return result;
            }
            var doc = await _db.Acc_Documents.Include(n => n.DocArticles).SingleOrDefaultAsync(n => n.Id == dto.DocId);
            if (doc == null)
            {
                result.Message = "سند مقصد یافت نشد";
                return result;
            }

            int row = doc.DocArticles.Count + 1;
            foreach (var item in articles)
            {
                item.DocId = doc.Id;
                item.LastUpdateDate = DateTime.Now;
                item.EditorUserName = dto.userName;
                row++;
            }

            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = "انتقال اطلاعات با موفقیت انجام شد";
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی رخ داده است. /n" + x.Message;
                return result;
            }

            return result;
        }
        //
        public async Task<MoeinStatusDto> GetMoeinStatusByIdAsync(int moeinId, long sellerId, int periodId)
        {
            var arts = _db.Acc_Articles
                .Include(m => m.Moein).ThenInclude(n => n.MoeinKol).ThenInclude(n => n.KolGroup)
                .Where(n => n.MoeinId == moeinId && n.Doc.SellerId == sellerId && n.Doc.PeriodId == periodId)
                .Select(n => new
                {
                    moeinId = n.MoeinId,
                    bed = n.Bed,
                    bes = n.Bes,
                    kolName = n.Moein.MoeinKol.KolName,
                    groupName = n.Moein.MoeinKol.KolGroup.GroupName,
                    nature = n.Moein.Nature,
                }).AsQueryable();

            var moeinInfo = await _db.Acc_Coding_Moeins
              .Include(n => n.MoeinKol)
              .ThenInclude(n => n.KolGroup)
              .SingleOrDefaultAsync(n => n.Id == moeinId);

            long bed = await arts.SumAsync(n => n.bed);
            long bes = await arts.SumAsync(n => n.bes);


            MoeinStatusDto dto = new MoeinStatusDto();
            dto.Id = moeinId;
            dto.KolName = moeinInfo.MoeinKol.KolName;
            dto.GroupName = moeinInfo.MoeinKol.KolGroup.GroupName;
            dto.AccountNature = moeinInfo.Nature;

            dto.StatusNature = (Int16)(bed > bes ? 1 : (bed < bes ? 2 : 0));
            dto.Mande = (bed > bes) ? bed - bes : bes - bed;

            return dto;

        }
        public async Task<bool> DocsSortingAsync(long sellerId, int periodId, Guid[] DocsId, int startNumber)
        {
            var allDocs = _db.Acc_Documents.Where(n => n.SellerId == sellerId && n.PeriodId == periodId).AsQueryable();

            int docnumber = startNumber;
            //docnumber = 1;
            for (int i = 0; i < DocsId.Length; i++)
            {
                var doc = await allDocs.SingleOrDefaultAsync(n => n.Id == DocsId[i]);
                if (doc != null)
                    doc.DocNumber = docnumber;
                docnumber++;
            }
            _db.Acc_Documents.UpdateRange(allDocs);
            return Convert.ToBoolean(await _db.SaveChangesAsync());
        }

        public async Task<clsResult> InsertSystemicDocAsync(List<DocArticleDto> articles, string docDesc, short docType, int periodId)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (articles.Count == 0)
            {
                result.Message = "اطلاعاتی برای ثبت سند وجود ندارد.";
                return result;
            }
            long totalBed = articles.Sum(n => n.Bed);
            long totalBes = articles.Sum(n => n.Bes);
            if (totalBed != totalBes)
            {
                result.Message = "سند تراز نیست.";
                return result;
            }

            var art = articles.FirstOrDefault();
            if (_db.Acc_Articles.Any(n => n.Id == art.Id))
            {
                result.Message = "سند قبلا ثبت شده است.";
                return result;
            }
            var fp = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == periodId);
            if (fp == null)
            {
                result.Message = "اطلاعات مربوط به دوره مالی یافت نشد";
                return result;
            }
            if (art.DocDate > fp.EndDate || art.DocDate < fp.StartDate)
            {
                result.Message = $"تاریخ سند خارج از بازه دوره مالی {fp.Name} است";
                return result;
            }
            if (fp.SellerId != art.SellerId)
            {
                result.Message = $"دوره مالی انتخاب شده معتبر نمی باشد، از فرم مربوط به انتخاب دوره مالی، دوره موردنظرتان را مجددا انتخاب کنید";
                return result;
            }
            Acc_Document doc = new Acc_Document();
            doc.Id = art.DocId;
            doc.SellerId = art.SellerId.Value;
            doc.PeriodId = art.PeriodId;
            doc.TypeId = docType;
            doc.Description = docDesc;
            doc.AutoDocNumber = await DocAutoNumberGeneratorAsync(art.SellerId.Value, art.PeriodId);
            doc.DocNumber = await DocNumberGeneratorAsync(art.SellerId.Value, art.PeriodId);
            doc.DocDate = fp.EndDate;
            doc.CreateDate = DateTime.Now;
            doc.CreatorUserName = art.CreatorUserName;
            doc.IsDeleted = false;
            _db.Acc_Documents.Add(doc);

            List<Acc_Article> Articles = new List<Acc_Article>();
            int rowNumber = 1;
            foreach (var x in articles)
            {
                Acc_Article a = new Acc_Article();
                a.RowNumber = rowNumber;
                rowNumber++;

                a.Id = x.Id;
                a.DocId = doc.Id;
                a.SellerId = x.SellerId;
                a.PeriodId = x.PeriodId;

                a.KolId = x.KolId;
                a.MoeinId = x.MoeinId;
                a.Tafsil4Id = x.Tafsil4Id;
                a.Tafsil4Name = x.Tafsil4Name;
                a.Comment = x.Comment;

                a.Amount = x.Amount;
                a.Bed = x.Bed;
                a.Bes = x.Bes;

                a.CreatorUserName = art.CreatorUserName;
                a.CreateDate = DateTime.Now;
                a.IsDeleted = false;
                a.ProjectId = x.ProjectId;
                Articles.Add(a);
            }

            _db.Acc_Articles.AddRange(Articles);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = $" سند {docDesc} با موفقیت ثبت شد";
                    result.ShowMessage = true;
                    result.updateType = 1;
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ثبت سند رخ داده است. <br>" + x.Message;
                return result;
            }

            return result;
        }

        public async Task<clsResult> InsertSystemicOpeningDocAsync(List<DocArticleDto> articles, string docDesc, short docType)
        {
            clsResult result = new clsResult();
            result.Success = false;

            if (articles.Count == 0)
            {
                result.Message = "اطلاعاتی برای ثبت سند وجود ندارد.";
                return result;
            }
            long totalBed = articles.Sum(n => n.Bed);
            long totalBes = articles.Sum(n => n.Bes);
            if (totalBed != totalBes)
            {
                result.Message = "سند تراز نیست.";
                return result;
            }

            var art = articles.FirstOrDefault();
            if (_db.Acc_Articles.Any(n => n.Id == art.Id))
            {
                result.Message = "سند قبلا ثبت شده است.";
                return result;
            }
            var fp = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == art.PeriodId);
            if (fp == null)
            {
                result.Message = "اطلاعات مربوط به دوره مالی یافت نشد";
                return result;
            }
            if (fp.SellerId != art.SellerId)
            {
                result.Message = $"دوره مالی انتخاب شده معتبر نمی باشد، از فرم مربوط به انتخاب دوره مالی، دوره موردنظرتان را مجددا انتخاب کنید";
                return result;
            }
            Acc_Document doc = new Acc_Document();
            doc.Id = art.DocId;
            doc.SellerId = art.SellerId.Value;
            doc.PeriodId = art.PeriodId;
            doc.TypeId = docType;
            doc.Description = docDesc;
            doc.AutoDocNumber = await DocAutoNumberGeneratorAsync(art.SellerId.Value, art.PeriodId);
            doc.DocNumber = 1;
            doc.DocDate = fp.StartDate;
            doc.CreateDate = DateTime.Now;
            doc.CreatorUserName = art.CreatorUserName;
            doc.IsDeleted = false;
            _db.Acc_Documents.Add(doc);

            List<Acc_Article> Articles = new List<Acc_Article>();
            int rowNumber = 1;
            foreach (var x in articles)
            {
                Acc_Article a = new Acc_Article();
                a.RowNumber = rowNumber;
                rowNumber++;

                a.Id = x.Id;
                a.DocId = doc.Id;
                a.SellerId = x.SellerId;
                a.PeriodId = x.PeriodId;

                a.KolId = x.KolId;
                a.MoeinId = x.MoeinId;
                a.Tafsil4Id = x.Tafsil4Id;
                a.Tafsil4Name = x.Tafsil4Name;
                a.Comment = x.Comment;

                a.Amount = x.Amount;
                a.Bed = x.Bed;
                a.Bes = x.Bes;

                a.CreatorUserName = art.CreatorUserName;
                a.CreateDate = DateTime.Now;
                a.IsDeleted = false;
                a.ProjectId = x.ProjectId;
                Articles.Add(a);
            }

            _db.Acc_Articles.AddRange(Articles);
            try
            {
                if (Convert.ToBoolean(await _db.SaveChangesAsync()))
                {
                    result.Success = true;
                    result.Message = $" سند {docDesc} با موفقیت ثبت شد";
                    result.ShowMessage = true;
                    result.updateType = 1;
                    return result;
                }
            }
            catch (Exception x)
            {
                result.Message = "خطایی در زمان ثبت سند رخ داده است. <br>" + x.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> InsertBulkDocAsync(List<DocArticleDto> articles, string docDesc, short docType)
        {
            clsResult result = new clsResult
            {
                Success = false
            };

            if (!articles.Any())
            {
                result.Message = "اطلاعاتی برای ثبت سند وجود ندارد.";
                return result;
            }

            long totalBed = articles.Sum(n => n.Bed);
            long totalBes = articles.Sum(n => n.Bes);
            if (totalBed != totalBes)
            {
                result.Message = "سند تراز نیست.";
                return result;
            }

            var art = articles.FirstOrDefault();
            if (_db.Acc_Articles.Any(n => n.Id == art.Id))
            {
                result.Message = "سند قبلا ثبت شده است.";
                return result;
            }

            var fp = await _db.Acc_FinancialPeriods.SingleOrDefaultAsync(n => n.Id == art.PeriodId);
            if (fp == null)
            {
                result.Message = "اطلاعات مربوط به دوره مالی یافت نشد";
                return result;
            }

            if (art.DocDate > fp.EndDate || art.DocDate < fp.StartDate)
            {
                result.Message = $"تاریخ سند خارج از بازه دوره مالی {fp.Name} است";
                return result;
            }

            if (fp.SellerId != art.SellerId)
            {
                result.Message = $"دوره مالی انتخاب شده معتبر نمی باشد، از فرم مربوط به انتخاب دوره مالی، دوره موردنظرتان را مجددا انتخاب کنید";
                return result;
            }

            var doc = new Acc_Document
            {
                Id = art.DocId,
                SellerId = art.SellerId.Value,
                PeriodId = art.PeriodId,
                TypeId = docType,
                Description = docDesc,
                AutoDocNumber = await DocAutoNumberGeneratorAsync(art.SellerId.Value, art.PeriodId),
                DocNumber = await DocNumberGeneratorAsync(art.SellerId.Value, art.PeriodId),
                DocDate = fp.EndDate,
                CreateDate = DateTime.Now,
                CreatorUserName = art.CreatorUserName,
                IsDeleted = false
            };

            _db.Acc_Documents.Add(doc);

            var Articles = articles.Select((x, index) => new Acc_Article
            {
                RowNumber = index + 1,
                Id = x.Id,
                DocId = doc.Id,
                SellerId = x.SellerId,
                PeriodId = x.PeriodId,
                KolId = x.KolId,
                MoeinId = x.MoeinId,
                Comment = x.Comment,
                Amount = x.Amount,
                Bed = x.Bed,
                Bes = x.Bes,
                CreatorUserName = art.CreatorUserName,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                ProjectId = art.ProjectId,

            }).ToList();

            try
            {
                await _db.BulkInsertAsync(Articles);

                result.Success = true;
                result.Message = $"سند {docDesc} با موفقیت ثبت شد";
                result.ShowMessage = true;
                result.updateType = 1;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = $"خطایی در زمان ثبت سند رخ داده است.\n{ex.Message}";
                return result;
            }
        }

        public async Task<List<DocMerge_Article>> GetMergeDocsArticlesAsync(Guid[] Docs, bool mergeAccount = false, bool keepTafsil = true)
        {

            var oldArticles = await _db.Acc_Articles
               .Include(n => n.Doc).Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => (!n.IsDeleted && !n.Doc.IsDeleted) && Docs.Contains(n.DocId))
                .ToListAsync();
            if (oldArticles == null) return null;

            List<DocMerge_Article> lst = new List<DocMerge_Article>();

            if (mergeAccount)
            {
                if (!keepTafsil)
                {
                    var bedArts = oldArticles.Where(n => n.Bed > 0).GroupBy(n => n.MoeinId).Select(n => new DocMerge_Article
                    {
                        MoeinId = n.Key,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        DocDate = n.Max(x => x.Doc.DocDate),
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        DocId = n.Max(x => x.DocId),

                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bed).ToList();
                    lst.AddRange(bedArts);
                    //
                    var besArts = oldArticles.Where(n => n.Bes > 0).GroupBy(n => n.MoeinId).Select(n => new DocMerge_Article
                    {
                        MoeinId = n.Key,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        DocDate = n.Max(x => x.Doc.DocDate),
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        DocId = n.Max(x => x.DocId),

                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bes).ToList();
                    lst.AddRange(besArts);

                    return lst;
                }
                else
                {
                    var bedArts = oldArticles.Where(n => n.Bed > 0).GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new DocMerge_Article
                    {
                        MoeinId = n.Key.MoeinId,
                        Tafsil4Id = n.Key.Tafsil4Id,
                        Tafsil4Name = n.Max(x => x.Tafsil4Name),
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        DocDate = n.Max(x => x.Doc.DocDate),
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        DocId = n.Max(x => x.DocId),

                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bed).ToList();
                    lst.AddRange(bedArts);
                    //
                    var besArts = oldArticles.Where(n => n.Bes > 0).GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new DocMerge_Article
                    {
                        MoeinId = n.Key.MoeinId,
                        Tafsil4Id = n.Key.Tafsil4Id,
                        Tafsil4Name = n.Max(x => x.Tafsil4Name),
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        DocDate = n.Max(x => x.Doc.DocDate),
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        DocId = n.Max(x => x.DocId),

                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderBy(n => n.DocNumber).ThenBy(n => n.RowNumber).ToList();
                    lst.AddRange(besArts);
                    //
                    return lst;
                }

            }

            foreach (var article in oldArticles)
            {
                DocMerge_Article articleDto = new DocMerge_Article();
                articleDto.OldId = article.Id;
                articleDto.OldDocId = article.DocId;
                articleDto.PeriodId = article.PeriodId;
                articleDto.DocNumber = article.Doc.DocNumber;
                articleDto.DocDate = article.Doc.DocDate;
                articleDto.KolId = article.KolId;
                articleDto.KolName = article.Moein.MoeinKol.KolName;
                articleDto.MoeinCode = article.Moein.MoeinCode;
                articleDto.MoeinName = article.Moein.MoeinName;
                articleDto.Tafsil4Id = article.Tafsil4Id;
                articleDto.Tafsil4Name = article.Tafsil4Name;
                articleDto.Tafsil5Id = article.Tafsil5Id;
                articleDto.Tafsil5Name = article.Tafsil5Name;
                articleDto.Tafsil6Id = article.Tafsil6Id;
                articleDto.Tafsil6Name = article.Tafsil6Name;
                articleDto.Tafsil7Id = article.Tafsil7Id;
                articleDto.Tafsil7Name = article.Tafsil7Name;
                articleDto.Tafsil8Id = article.Tafsil8Id;
                articleDto.Tafsil8Name = article.Tafsil8Name;
                articleDto.MoeinId = article.MoeinId;
                articleDto.RowNumber = article.RowNumber;
                articleDto.Comment = article.Comment;
                articleDto.Amount = article.Amount;
                articleDto.Bed = article.Bed;
                articleDto.strBed = article.Bed.ToPrice();
                articleDto.Bes = article.Bes;
                articleDto.strBes = article.Bes.ToPrice();

                lst.Add(articleDto);
            }


            return lst.OrderBy(n => n.DocNumber).ThenBy(n => n.RowNumber).ToList();
        }
        //
        public async Task<clsResult> AddMergedDocsAsync(DocMerge_Header dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            var oldArticles = await _db.Acc_Articles
               .Include(n => n.Doc).Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => (!n.IsDeleted && !n.Doc.IsDeleted) && dto.DocsForMerge.Contains(n.DocId))
                .OrderBy(n => n.Doc.DocNumber).ThenBy(n => n.RowNumber)
                .ToListAsync();
            var oldDocNumbers = oldArticles.Select(n => n.Doc.AutoDocNumber).Distinct().ToList();
            string oldDocSign = "ادغام سند(های) ";
            foreach (var docnum in oldDocNumbers)
            {
                oldDocSign += "-" + docnum;
            }

            if (oldArticles == null)
            {
                result.Message = "اطلاعاتی جهت ادغام وجود ندارد";
                return result;
            }
            //
            DocDto_AddNew n = new DocDto_AddNew();
            n.Id = dto.Id;
            n.TypeId = 100;
            n.SellerId = dto.SellerId;
            n.PeriodId = dto.PeriodId;
            n.AtfNumber = dto.AtfNumber;
            n.DocNumber = dto.DocNumber;
            n.CreatorUserName = dto.CreatorUserName;
            n.Description = dto.Description;
            n.strDocDate = dto.strDocDate;
            n.DocDate = dto.DocDate;
            n.IsDeleted = false;
            if (oldDocNumbers.Count > 1 && dto.AddDocsMergedNumbetToDesc)
            {
                n.Description += oldDocSign;
            }
            var addNewDocResult = await CreateDocHeaderAsync(n);
            if (!addNewDocResult.Success)
            {
                result.Message = addNewDocResult.Message;
                return result;
            }

            List<DocArticleDto> lst = new List<DocArticleDto>();
            if (dto.MergeSameAccount)
            {
                if (!dto.MergeSameTafsil)
                {
                    var bedArts = oldArticles.Where(n => n.Bed > 0).GroupBy(n => n.MoeinId).Select(n => new DocArticleDto
                    {
                        Id = new Guid(),
                        DocId = dto.Id,
                        PeriodId = dto.PeriodId.Value,
                        SellerId = dto.SellerId,
                        MoeinId = n.Key,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        DocDate = dto.DocDate,
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        CreatorUserName = dto.CreatorUserName,
                        IsDeleted = false,
                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bed).ToList();
                    lst.AddRange(bedArts);
                    //
                    var besArts = oldArticles.Where(n => n.Bes > 0).GroupBy(n => n.MoeinId).Select(n => new DocArticleDto
                    {
                        Id = new Guid(),
                        DocId = dto.Id,
                        PeriodId = dto.PeriodId.Value,
                        SellerId = dto.SellerId,
                        MoeinId = n.Key,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        DocDate = dto.DocDate,
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        CreatorUserName = dto.CreatorUserName,
                        IsDeleted = false,
                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bes).ToList();
                    lst.AddRange(besArts);
                }
                else
                {
                    var bedArts = oldArticles.Where(n => n.Bed > 0).GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new DocArticleDto
                    {

                        Id = new Guid(),
                        DocId = dto.Id,
                        PeriodId = dto.PeriodId.Value,
                        SellerId = dto.SellerId,
                        MoeinId = n.Key.MoeinId,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        Tafsil4Id = n.Key.Tafsil4Id,
                        Tafsil4Name = n.Max(x => x.Tafsil4Name),
                        DocDate = dto.DocDate,
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        CreatorUserName = dto.CreatorUserName,
                        IsDeleted = false,
                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bed).ToList();
                    lst.AddRange(bedArts);
                    //
                    var besArts = oldArticles.Where(n => n.Bes > 0).GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new DocArticleDto
                    {
                        Id = new Guid(),
                        DocId = dto.Id,
                        PeriodId = dto.PeriodId.Value,
                        SellerId = dto.SellerId,
                        MoeinId = n.Key.MoeinId,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        Tafsil4Id = n.Key.Tafsil4Id,
                        Tafsil4Name = n.Max(x => x.Tafsil4Name),
                        DocDate = dto.DocDate,
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        CreatorUserName = dto.CreatorUserName,
                        IsDeleted = false,
                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bes).ToList();
                    lst.AddRange(besArts);
                }

            }
            else
            {
                foreach (var article in oldArticles)
                {
                    DocArticleDto articleDto = new DocArticleDto();
                    articleDto.Id = new Guid();
                    articleDto.DocId = dto.Id;
                    articleDto.PeriodId = dto.PeriodId.Value;
                    articleDto.SellerId = dto.SellerId;
                    articleDto.DocNumber = dto.DocNumber;
                    articleDto.DocDate = dto.DocDate;
                    articleDto.KolId = article.KolId;
                    articleDto.MoeinCode = article.Moein.MoeinCode;
                    articleDto.MoeinName = article.Moein.MoeinName;
                    articleDto.Tafsil4Id = article.Tafsil4Id;
                    articleDto.Tafsil4Name = article.Tafsil4Name;
                    articleDto.Tafsil5Id = article.Tafsil5Id;
                    articleDto.Tafsil5Name = article.Tafsil5Name;
                    articleDto.Tafsil6Id = article.Tafsil6Id;
                    articleDto.Tafsil6Name = article.Tafsil6Name;
                    articleDto.Tafsil7Id = article.Tafsil7Id;
                    articleDto.Tafsil7Name = article.Tafsil7Name;
                    articleDto.Tafsil8Id = article.Tafsil8Id;
                    articleDto.Tafsil8Name = article.Tafsil8Name;
                    articleDto.MoeinId = article.MoeinId;
                    articleDto.RowNumber = article.RowNumber;
                    articleDto.Comment = article.Comment;
                    articleDto.Amount = article.Amount;
                    articleDto.Bed = article.Bed;
                    articleDto.strBed = article.Bed.ToPrice();
                    articleDto.Bes = article.Bes;
                    articleDto.strBes = article.Bes.ToPrice();
                    articleDto.CreatorUserName = article.CreatorUserName;
                    lst.Add(articleDto);
                }
            }

            var dleteOldArts = await _db.Acc_Articles.Where(n => dto.DocsForMerge.Contains(n.DocId))
                .ExecuteUpdateAsync(x =>
                x.SetProperty(z => z.IsDeleted, true)
                .SetProperty(z => z.DeletedDate, DateTime.Now)
                .SetProperty(z => z.DeleteUserName, dto.CreatorUserName));
            var MergedDocs = await _db.Acc_Documents.Where(n => dto.DocsForMerge.Contains(n.Id))
                .ExecuteUpdateAsync(x =>
                x.SetProperty(z => z.IsDeleted, true)
                .SetProperty(z => z.Description, dto.Id.ToString())
                );

            int rownumber = 1;

            foreach (var art in lst)
            {
                art.RowNumber = rownumber;
                rownumber++;
            }
            var addArtsResult = await AddRangeArticlesAsync(lst);
            if (addArtsResult.Success)
            {
                result.Success = true;
                result.updateType = 1;
                result.Message = "ادغام اسناد با موفقیت انجام شد.";
                return result;
            }
            else
            {
                result.Message = addArtsResult.Message;
                return result;
            }

            return result;
        }
        public async Task<clsResult> CopyDocsAsync(DocMerge_Header dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            result.ShowMessage = true;
            var oldArticles = await _db.Acc_Articles
               .Include(n => n.Doc).Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.IsDeleted == false && dto.DocsForMerge.Contains(n.DocId))
                .ToListAsync();

            if (oldArticles == null)
            {
                result.Message = "اطلاعاتی جهت ادغام وجود ندارد";
                return result;
            }
            //
            DocDto_AddNew n = new DocDto_AddNew();
            n.Id = dto.Id;
            n.TypeId = 100;
            n.SellerId = dto.SellerId;
            n.PeriodId = dto.PeriodId;
            n.AtfNumber = dto.AtfNumber;
            n.DocNumber = dto.DocNumber;
            n.CreatorUserName = dto.CreatorUserName;
            n.Description = dto.Description;
            n.strDocDate = dto.strDocDate;
            n.DocDate = dto.DocDate;
            n.IsDeleted = false;
            var addNewDocResult = await CreateDocHeaderAsync(n);
            if (!addNewDocResult.Success)
            {
                result.Message = addNewDocResult.Message;
                return result;
            }

            List<DocArticleDto> lst = new List<DocArticleDto>();
            if (dto.MergeSameAccount)
            {
                if (!dto.MergeSameTafsil)
                {
                    var bedArts = oldArticles.Where(n => n.Bed > 0).GroupBy(n => n.MoeinId).Select(n => new DocArticleDto
                    {
                        Id = new Guid(),
                        DocId = dto.Id,
                        PeriodId = dto.PeriodId.Value,
                        SellerId = dto.SellerId,
                        MoeinId = n.Key,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        DocDate = dto.DocDate,
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        CreatorUserName = dto.CreatorUserName,
                        IsDeleted = false,
                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bed).ToList();
                    lst.AddRange(bedArts);
                    //
                    var besArts = oldArticles.Where(n => n.Bes > 0).GroupBy(n => n.MoeinId).Select(n => new DocArticleDto
                    {
                        Id = new Guid(),
                        DocId = dto.Id,
                        PeriodId = dto.PeriodId.Value,
                        SellerId = dto.SellerId,
                        MoeinId = n.Key,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        DocDate = dto.DocDate,
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        CreatorUserName = dto.CreatorUserName,
                        IsDeleted = false,
                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bes).ToList();
                    lst.AddRange(besArts);
                }
                else
                {
                    var bedArts = oldArticles.Where(n => n.Bed > 0).GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new DocArticleDto
                    {

                        Id = new Guid(),
                        DocId = dto.Id,
                        PeriodId = dto.PeriodId.Value,
                        SellerId = dto.SellerId,
                        MoeinId = n.Key.MoeinId,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        Tafsil4Id = n.Key.Tafsil4Id,
                        Tafsil4Name = n.Max(x => x.Tafsil4Name),
                        DocDate = dto.DocDate,
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        CreatorUserName = dto.CreatorUserName,
                        IsDeleted = false,
                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bed).ToList();
                    lst.AddRange(bedArts);
                    //
                    var besArts = oldArticles.Where(n => n.Bes > 0).GroupBy(n => new { n.MoeinId, n.Tafsil4Id }).Select(n => new DocArticleDto
                    {
                        Id = new Guid(),
                        DocId = dto.Id,
                        PeriodId = dto.PeriodId.Value,
                        SellerId = dto.SellerId,
                        MoeinId = n.Key.MoeinId,
                        MoeinCode = n.Max(x => x.Moein.MoeinCode),
                        MoeinName = n.Max(x => x.Moein.MoeinName),
                        KolId = n.Max(x => x.KolId),
                        KolName = n.Max(x => x.Moein.MoeinKol.KolName),
                        Tafsil4Id = n.Key.Tafsil4Id,
                        Tafsil4Name = n.Max(x => x.Tafsil4Name),
                        DocDate = dto.DocDate,
                        DocNumber = n.Max(x => x.Doc.DocNumber),
                        Comment = n.Max(x => x.Comment),
                        ArchiveCode = n.Max(x => x.ArchiveCode),
                        CreatorUserName = dto.CreatorUserName,
                        IsDeleted = false,
                        Amount = n.Sum(x => x.Amount),
                        Bed = n.Sum(x => x.Bed),
                        Bes = n.Sum(x => x.Bes),

                    }).OrderByDescending(n => n.Bes).ToList();
                    lst.AddRange(besArts);
                }

            }
            else
            {
                foreach (var article in oldArticles)
                {
                    DocArticleDto articleDto = new DocArticleDto();
                    articleDto.Id = new Guid();
                    articleDto.DocId = dto.Id;
                    articleDto.PeriodId = dto.PeriodId.Value;
                    articleDto.SellerId = dto.SellerId;
                    articleDto.DocNumber = dto.DocNumber;
                    articleDto.DocDate = dto.DocDate;
                    articleDto.KolId = article.KolId;
                    articleDto.MoeinCode = article.Moein.MoeinCode;
                    articleDto.MoeinName = article.Moein.MoeinName;
                    articleDto.Tafsil4Id = article.Tafsil4Id;
                    articleDto.Tafsil4Name = article.Tafsil4Name;
                    articleDto.Tafsil5Id = article.Tafsil5Id;
                    articleDto.Tafsil5Name = article.Tafsil5Name;
                    articleDto.Tafsil6Id = article.Tafsil6Id;
                    articleDto.Tafsil6Name = article.Tafsil6Name;
                    articleDto.Tafsil7Id = article.Tafsil7Id;
                    articleDto.Tafsil7Name = article.Tafsil7Name;
                    articleDto.Tafsil8Id = article.Tafsil8Id;
                    articleDto.Tafsil8Name = article.Tafsil8Name;
                    articleDto.MoeinId = article.MoeinId;
                    articleDto.RowNumber = article.RowNumber;
                    articleDto.Comment = article.Comment;
                    articleDto.Amount = article.Amount;
                    articleDto.Bed = article.Bed;
                    articleDto.strBed = article.Bed.ToPrice();
                    articleDto.Bes = article.Bes;
                    articleDto.strBes = article.Bes.ToPrice();
                    articleDto.CreatorUserName = article.CreatorUserName;
                    lst.Add(articleDto);
                }
            }

            int rownumber = 1;
            foreach (var art in lst)
            {
                art.RowNumber = rownumber;
                rownumber++;
            }
            var addArtsResult = await AddRangeArticlesAsync(lst);
            if (addArtsResult.Success)
            {
                result.Success = true;
                result.updateType = 1;
                result.Message = "کپی از اسناد با موفقیت انجام شد.";
                return result;
            }
            else
            {
                result.Message = addArtsResult.Message;
                return result;
            }

            return result;
        }
        public async Task<DocPrintDto> GetDocPrintAsync(Guid id)
        {
            DocPrintDto doc = new DocPrintDto();

            var header = await _db.Acc_Documents.Include(n => n.DocArticles).Include(n => n.DocPeriod).SingleOrDefaultAsync(n => n.Id == id);
            if (header == null) return null;

            doc.Header = new DocHeaderPrintDto
            {
                Id = header.Id,
                DocAutoNumber = header.AutoDocNumber,
                DocNumber = header.DocNumber,
                DocDate = header.DocDate,
                DocDate_Sh = header.DocDate.LatinToPersian(),
                Description = header.Description,
                FinancePeriodName = header.DocPeriod.Name,
                strTotal = ((long)header.DocArticles.Sum(s => s.Bed)).ToPersianWords(),
                Auther = header.CreatorUserName,
            };

            doc.Articles = await _db.Acc_Articles
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.DocId == id && n.IsDeleted == false)
                .Select(n => new DocArticlePrintDto
                {
                    Id = n.Id,
                    KolId = n.Moein.KolId,
                    KolCode = n.Moein.MoeinKol.KolCode,
                    KolName = n.Moein.MoeinKol.KolName,
                    DocId = n.DocId,
                    MoeinCode = n.Moein.MoeinCode,
                    MoeinName = n.Moein.MoeinName,
                    Tafsil4 = n.Tafsil4Name,
                    Tafsil5 = n.Tafsil5Name,
                    Amount = n.Amount,
                    strAmount = n.Amount.ToText(),
                    Comment = n.Comment,
                    Bed = n.Bed,
                    Bes = n.Bes,
                    Nature = n.Bed > n.Bes ? 1 : 2,
                }).OrderByDescending(n => n.Bed).ToListAsync();

            return doc;

        }

        public async Task<StructuredDocPrintDto> GetStructuredDocPrintAsync(Guid id)
        {
            StructuredDocPrintDto doc = new StructuredDocPrintDto();

            var header = await _db.Acc_Documents
                .Include(n => n.DocArticles)
                .Include(n => n.DocPeriod)
                .SingleOrDefaultAsync(n => n.Id == id);

            if (header == null) return null;

            // تنظیم اطلاعات هدر
            doc.Header = new DocHeaderPrintDto
            {
                Id = header.Id,
                DocAutoNumber = header.AutoDocNumber,
                DocNumber = header.DocNumber,
                DocDate = header.DocDate,
                DocDate_Sh = header.DocDate.LatinToPersian(),
                Description = header.Description,
                FinancePeriodName = header.DocPeriod.Name,
                strTotal = ((long)header.DocArticles.Sum(s => s.Bed)).ToPersianWords(),
                Auther = header.CreatorUserName,
            };

            // دریافت ردیفهای سند و گروه‌بندی آن‌ها
            var articles = await _db.Acc_Articles
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.DocId == id && n.IsDeleted == false)
                .ToListAsync();

            // گروه‌بندی بر اساس حساب کل
            doc.KolGroups = articles
                .GroupBy(a => a.Moein.KolId)
                .Select(kolGroup => new StructuredKolGroupDto
                {
                    KolId = kolGroup.Key,
                    KolCode = kolGroup.Max(s => s.Moein.MoeinKol.KolCode),
                    KolName = kolGroup.Max(s => s.Moein.MoeinKol.KolName),
                    TotalBed = kolGroup.Sum(x => x.Bed),
                    TotalBes = kolGroup.Sum(x => x.Bes),
                    MoeinGroups = kolGroup
                        .GroupBy(m => m.MoeinId)
                        .Select(moeinGroup => new StructuredMoeinGroupDto
                        {
                            MoeinId = moeinGroup.Key,
                            KolId = moeinGroup.Max(n => n.Moein.KolId),
                            MoeinCode = moeinGroup.Max(n => n.Moein.MoeinCode),
                            MoeinName = moeinGroup.Max(n => n.Moein.MoeinName),
                            TotalBed = moeinGroup.Sum(x => x.Bed),
                            TotalBes = moeinGroup.Sum(x => x.Bes),
                            TafsilDetails = moeinGroup
                                .Where(t => t.Id != null)
                                .Select(t => new StructuredTafsilDto
                                {
                                    KolId = t.Moein.KolId,
                                    MoeinId = t.MoeinId,
                                    Tafsil4Id = t.Tafsil4Id,
                                    Tafsil4Name = t.Tafsil4Name,
                                    Bed = t.Bed,
                                    Bes = t.Bes,
                                    Comment = t.Comment
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

            return doc;
        }


        public async Task<DocPrintDto> GetDocPrintKolAsync(Guid id)
        {
            DocPrintDto doc = new DocPrintDto();
            doc.Articles = new List<DocArticlePrintDto>();

            var header = await _db.Acc_Documents.Include(n => n.DocArticles).Include(n => n.DocPeriod).SingleOrDefaultAsync(n => n.Id == id);
            if (header == null) return null;

            doc.Header = new DocHeaderPrintDto
            {
                Id = header.Id,
                DocAutoNumber = header.AutoDocNumber,
                DocNumber = header.DocNumber,
                DocDate = header.DocDate,
                DocDate_Sh = header.DocDate.LatinToPersian(),
                Description = header.Description,
                FinancePeriodName = header.DocPeriod.Name,
                strTotal = ((long)header.DocArticles.Sum(s => s.Bed)).ToPersianWords(),
            };
            var articles = _db.Acc_Articles
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.DocId == id && n.IsDeleted == false).AsQueryable();

            var BedArts = await articles.Where(n => n.Bed > 0)
                .GroupBy(n => n.KolId)
                .Select(n => new DocArticlePrintDto
                {
                    KolId = n.Key,
                    KolCode = n.Max(z => z.Moein.MoeinKol.KolCode),
                    KolName = n.Max(z => z.Moein.MoeinKol.KolName),
                    Bed = n.Sum(z => z.Bed),
                    Bes = n.Sum(z => z.Bes),
                    Amount = n.Sum(z => z.Bed),
                    Nature = 1,
                }).OrderBy(n => n.KolCode).ToListAsync();

            var BesArts = await articles.Where(n => n.Bes > 0)
            .GroupBy(n => n.KolId)
            .Select(n => new DocArticlePrintDto
            {
                KolId = n.Key,
                KolCode = n.Max(z => z.Moein.MoeinKol.KolCode),
                KolName = n.Max(z => z.Moein.MoeinKol.KolName),
                Bed = n.Sum(z => z.Bed),
                Bes = n.Sum(z => z.Bes),
                Amount = n.Sum(z => z.Bes),
                Nature = 2,
            }).OrderBy(n => n.KolCode).ToListAsync();

            doc.Articles.AddRange(BedArts);
            doc.Articles.AddRange(BesArts);

            return doc;

        }
        public async Task<DocPrintDto> GetDocPrintMoeinAsync(Guid id)
        {
            DocPrintDto doc = new DocPrintDto();
            doc.Articles = new List<DocArticlePrintDto>();
            var header = await _db.Acc_Documents.Include(n => n.DocArticles).Include(n => n.DocPeriod).SingleOrDefaultAsync(n => n.Id == id);
            if (header == null) return null;

            doc.Header = new DocHeaderPrintDto
            {
                Id = header.Id,
                DocAutoNumber = header.AutoDocNumber,
                DocNumber = header.DocNumber,
                DocDate = header.DocDate,
                DocDate_Sh = header.DocDate.LatinToPersian(),
                Description = header.Description,
                FinancePeriodName = header.DocPeriod.Name,
                strTotal = ((long)header.DocArticles.Sum(s => s.Bed)).ToPersianWords(),
            };
            var articles = _db.Acc_Articles
                .Include(n => n.Moein).ThenInclude(n => n.MoeinKol)
                .Where(n => n.DocId == id && n.IsDeleted == false).AsQueryable();

            var BedArts = await articles.Where(n => n.Bed > 0)
                .GroupBy(n => n.MoeinId)
                .Select(n => new DocArticlePrintDto
                {
                    MoeinId = n.Key,
                    MoeinCode = n.Max(z => z.Moein.MoeinCode),
                    MoeinName = n.Max(z => z.Moein.MoeinName),
                    KolId = n.Max(z => z.Moein.KolId),
                    KolCode = n.Max(z => z.Moein.MoeinKol.KolCode),
                    KolName = n.Max(z => z.Moein.MoeinKol.KolName),
                    Bed = n.Sum(z => z.Bed),
                    Bes = n.Sum(z => z.Bes),
                    Amount = n.Sum(z => z.Bed),
                    Nature = 1,

                }).OrderBy(n => n.KolCode).ToListAsync();

            var BesArts = await articles.Where(n => n.Bes > 0)
            .GroupBy(n => n.MoeinId)
                .Select(n => new DocArticlePrintDto
                {
                    MoeinId = n.Key,
                    MoeinCode = n.Max(z => z.Moein.MoeinCode),
                    MoeinName = n.Max(z => z.Moein.MoeinName),
                    KolId = n.Max(z => z.Moein.KolId),
                    KolCode = n.Max(z => z.Moein.MoeinKol.KolCode),
                    KolName = n.Max(z => z.Moein.MoeinKol.KolName),
                    Bed = n.Sum(z => z.Bed),
                    Bes = n.Sum(z => z.Bes),
                    Amount = n.Sum(z => z.Bes),
                    Nature = 2,
                }).OrderBy(n => n.KolCode).ToListAsync();

            doc.Articles.AddRange(BedArts);
            doc.Articles.AddRange(BesArts);

            return doc;

        }
        public byte[] GenerateAccountingDocumentExcel(DocDto doc)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("سند حسابداری");
                worksheet.RightToLeft = true;
                workbook.Style.Font.FontName = "BNazanin";
                // Setting up the header
                worksheet.Cell(1, 1).Value = "شماره سند:";
                worksheet.Cell(1, 2).Value = doc.DocNumber;
                worksheet.Cell(1, 4).Value = "تاریخ سند:";
                worksheet.Cell(1, 5).Value = doc.DocDate.HasValue ? doc.DocDate.Value.LatinToPersian() : string.Empty;

                worksheet.Cell(2, 1).Value = "شرح سند:";
                worksheet.Cell(2, 2).Value = doc.Description;
                worksheet.Cell(2, 4).Value = "وضعیت سند:";
                worksheet.Cell(2, 5).Value = doc.StatusId.AccToDocStatusName();

                // Adding article headers
                worksheet.Cell(4, 1).Value = "شماره ردیف";
                worksheet.Cell(4, 2).Value = "حساب کل";
                worksheet.Cell(4, 3).Value = "حساب معین";
                worksheet.Cell(4, 4).Value = "مبلغ بدهکار";
                worksheet.Cell(4, 5).Value = "مبلغ بستانکار";
                worksheet.Cell(4, 6).Value = "شرح آرتیکل";

                // Adding article data
                int row = 5;
                foreach (var article in doc.Articles)
                {
                    worksheet.Cell(row, 1).Value = article.RowNumber;
                    worksheet.Cell(row, 2).Value = article.KolName;
                    worksheet.Cell(row, 3).Value = article.MoeinName;
                    worksheet.Cell(row, 4).Value = article.Bed;
                    worksheet.Cell(row, 5).Value = article.Bes;
                    worksheet.Cell(row, 6).Value = article.Comment;
                    row++;
                }

                // Adding total row
                worksheet.Cell(row, 1).Value = "جمع کل";
                worksheet.Cell(row, 4).FormulaA1 = $"SUM(D5:D{row - 1})";
                worksheet.Cell(row, 5).FormulaA1 = $"SUM(E5:E{row - 1})";

                // Formatting the header
                var headerRange = worksheet.Range("A1:F2");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                var articleHeaderRange = worksheet.Range("A4:F4");
                articleHeaderRange.Style.Font.Bold = true;
                articleHeaderRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                articleHeaderRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                // Formatting the data rows
                var dataRange = worksheet.Range($"A5:F{row}");
                dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                // Adding borders
                var tableRange = worksheet.Range("A4:F" + row);
                tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // Setting column widths
                worksheet.Columns().AdjustToContents();

                // Setting page setup for printing
                worksheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
                worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                worksheet.PageSetup.FitToPages(1, 1);
                worksheet.PageSetup.Margins.Top = 0.5;
                worksheet.PageSetup.Margins.Bottom = 0.5;
                worksheet.PageSetup.Margins.Left = 0.5;
                worksheet.PageSetup.Margins.Right = 0.5;
                worksheet.PageSetup.CenterHorizontally = true;
                worksheet.PageSetup.CenterVertically = true;

                // Save to memory stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        // ===== Hellper 

        public async Task<List<ArticleListItem>> CheckBalanceByArchiveAsync(Guid DocId)
        {
            var arts = await _db.Acc_Articles.Where(n => n.DocId == DocId && !n.IsDeleted).ToListAsync();
            var checkList = arts.GroupBy(n => n.ArchiveCode).Select(n => new ArticleListItem
            {
                ArchveCode = n.Key,
                Count = n.Count(),
                TotalBed = n.Sum(s => s.Bed),
                TotalBes = n.Sum(s => s.Bes),
                Balance = n.Sum(s => s.Bed) > n.Sum(s => s.Bes) ? n.Sum(s => s.Bed - s.Bes) : n.Sum(s => s.Bes - s.Bed),
                Nature = n.Sum(s => s.Bed) > n.Sum(s => s.Bes) ? "بدهکار" : "بستانکار",
            }).Where(n => n.Balance > 0).OrderBy(n => n.ArchveCode).ToList();

            return checkList;
        }


    }
}
