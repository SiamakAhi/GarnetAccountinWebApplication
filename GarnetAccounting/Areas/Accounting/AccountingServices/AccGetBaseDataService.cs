using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{

    public class AccGetBaseDataService : IAccGetBaseDataService
    {
        private readonly AppDbContext _db;
        public AccGetBaseDataService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<SelectList> GetKolsAsync(long sellerId)
        {
            var kols = await _db.Acc_Coding_Kols.Where(n => n.SellerId == sellerId)
                .Select(n => new
                {
                    id = n.Id,
                    name = n.KolName,
                    code = n.KolCode
                }).OrderBy(n => n.code).ToListAsync();

            return new SelectList(kols, "id", "name");
        }
        public async Task<SelectList> GetMoeinsAsync(long sellerId)
        {
            var moeins = await _db.Acc_Coding_Moeins.Where(n => n.SellerId == sellerId)
                .Select(n => new
                {
                    id = n.Id,
                    name = n.MoeinName,
                    code = n.MoeinCode
                }).OrderBy(n => n.code).ToListAsync();

            return new SelectList(moeins, "id", "name");
        }
        public async Task<List<KolDto>> GetUsedKolsAsync(long sellerId)
        {
            List<int>? kolsid = await _db.Acc_Articles.Include(n => n.Moein)
                .Where(n => n.Doc.SellerId == sellerId)
                   .Select(n => n.Moein.KolId).Distinct().ToListAsync<int>();

            var kols = await _db.Acc_Coding_Kols
                .Where(n => kolsid != null ? kolsid.Contains(n.Id) : n.SellerId == sellerId)
                .Select(n => new KolDto
                {
                    Id = n.Id,
                    KolName = n.KolName,
                    Nature = n.Nature,
                }).ToListAsync();

            return kols;
        }
        public async Task<List<KolDto>> GetUsedKolsByTafsilGroupAsync(long sellerId, List<int>? groups)
        {
            List<long>? tafsils = new List<long>();
            if (groups != null)
                tafsils = await _db.Acc_Coding_TafsilToGroups.Where(n => groups.Contains(n.GroupId))
                    .Select(n => n.TafsilId).Distinct().ToListAsync();

            List<int>? kolsid = await _db.Acc_Articles.Include(n => n.Moein)
                .Where(n => n.Doc.SellerId == sellerId
                && (tafsils != null ? tafsils.Contains(n.Tafsil4Id.Value) : n.MoeinId > 0))
                .Select(n => n.Moein.KolId).Distinct().ToListAsync<int>();

            var kols = await _db.Acc_Coding_Kols
                .Where(n => kolsid != null ? kolsid.Contains(n.Id) : n.SellerId == sellerId)
                .Select(n => new KolDto
                {
                    Id = n.Id,
                    KolName = n.KolName,
                    Nature = n.Nature,
                }).ToListAsync();

            return kols;
        }
        public async Task<List<KolDto>> GetCascadingUsedKolsAsync(long sellerId, List<int>? groups, List<long>? tafsils = null)
        {
            List<int>? kolsid = null;
            var query = _db.Acc_Articles.Include(n => n.Moein).AsNoTracking().Where(n => n.Doc.SellerId == sellerId)
                .AsQueryable();

            List<long>? groupTafsils = new List<long>();

            //== اولویت با حسابهای تفصیلی است
            if (tafsils?.Count > 0)
            {
                query = query.Where(n => tafsils.Contains(n.Tafsil4Id.Value)).AsQueryable();
            }
            else if (tafsils?.Count == 0 && groups?.Count > 0)
            {
                tafsils = await _db.Acc_Coding_TafsilToGroups.Where(n => groups.Contains(n.GroupId))
                     .Select(n => n.TafsilId).Distinct().ToListAsync();
                query = query.Where(n => tafsils.Contains(n.Tafsil4Id.Value)).AsQueryable();

            }

            kolsid = await query.Select(n => n.Moein.KolId).Distinct().ToListAsync<int>();
            var kols = await _db.Acc_Coding_Kols
                     .Where(n => kolsid != null ? kolsid.Contains(n.Id) : n.SellerId == sellerId)
                      .Select(n => new KolDto
                      {
                          Id = n.Id,
                          KolName = n.KolName,
                          Nature = n.Nature,
                      }).ToListAsync();

            return kols;
        }
        public async Task<List<KolDto>> GetUsedKolsByTafsilAsync(long sellerId, List<long>? tafsils)
        {
            if (tafsils?.Count == 0)
                tafsils = null;

            List<int>? kolsid = await _db.Acc_Articles.Include(n => n.Moein)
                .Where(n => n.Doc.SellerId == sellerId
                && (tafsils != null ? tafsils.Contains(n.Tafsil4Id.Value) : n.MoeinId > 0))
                .Select(n => n.Moein.KolId).Distinct().ToListAsync<int>();

            var kols = await _db.Acc_Coding_Kols
                .Where(n => kolsid != null ? kolsid.Contains(n.Id) : n.SellerId == sellerId)
                .Select(n => new KolDto
                {
                    Id = n.Id,
                    KolName = n.KolName,
                    Nature = n.Nature,
                }).ToListAsync();

            return kols;
        }
        public async Task<SelectList> SelectList_UsedKolsByTafsilAsync(long sellerId, List<long>? tafsils)
        {
            if (tafsils?.Count == 0)
                tafsils = null;

            List<int>? kolsid = await _db.Acc_Articles.Include(n => n.Moein)
                .Where(n => n.Doc.SellerId == sellerId
                && (tafsils != null ? tafsils.Contains(n.Tafsil4Id.Value) : n.MoeinId > 0))
                .Select(n => n.Moein.KolId).Distinct().ToListAsync<int>();

            var kols = await _db.Acc_Coding_Kols
                .Where(n => kolsid != null ? kolsid.Contains(n.Id) : n.SellerId == sellerId)
                .Select(n => new KolDto
                {
                    Id = n.Id,
                    KolName = n.KolName,
                    Nature = n.Nature,
                }).ToListAsync();

            return new SelectList(kols, "Id", "KolName");
        }
        public async Task<List<MoeinDto>> GetUsedMoeinsByKolsAsync(long sellerId, List<int>? kols)
        {
            if (kols?.Count == 0)
                kols = null;

            var moeins = await _db.Acc_Coding_Moeins
                .Where(n => (kols != null) ? kols.Contains(n.KolId) : n.SellerId == sellerId)
                .Select(n => new MoeinDto
                {
                    Id = n.Id,
                    MoeinName = n.MoeinName,
                    Nature = n.Nature,
                    KolId = n.KolId,
                }).ToListAsync();

            return moeins;
        }
        public async Task<List<MoeinDto>> GetUsedMoeinsByKolAndTafsilAsync(long sellerId, List<long>? tafsils, List<int>? kols = null)
        {
            if (kols?.Count == 0)
                kols = null;
            var query = _db.Acc_Articles.Where(n =>
           (n.SellerId == sellerId && !n.IsDeleted)).AsQueryable();

            if (kols?.Count > 0)
                query = query.Where(n => kols.Contains(n.Moein.KolId));
            if (tafsils?.Count > 0)
                query = query.Where(n => tafsils.Contains(n.Tafsil4Id.Value));

            List<int> moeinIds = await query.Select(n => n.MoeinId).Distinct().ToListAsync<int>();

            var moeins = await _db.Acc_Coding_Moeins
                .Where(n => moeinIds.Contains(n.Id))
                .Select(n => new MoeinDto
                {
                    Id = n.Id,
                    MoeinName = n.MoeinName,
                    Nature = n.Nature,
                    KolId = n.KolId,
                }).ToListAsync();

            return moeins;
        }
        public async Task<SelectList> SelectListUsedMoeinsByKolsAsync(long sellerId, List<int>? kols)
        {
            var query = _db.Acc_Coding_Moeins.Where(n => n.SellerId == sellerId).AsQueryable();
            if (kols?.Count > 0)
                query = query.Where(n => kols.Contains(n.KolId));

            var moeins = await query
                .Select(n => new MoeinDto
                {
                    Id = n.Id,
                    MoeinName = n.MoeinName,
                    Nature = n.Nature,
                    KolId = n.KolId,
                }).ToListAsync();

            return new SelectList(moeins, "Id", "MoeinName");
        }

        public async Task<List<MoeinDto>> GetUsedMoeinsByTafsilAsync(long sellerId, List<long>? tafsils)
        {
            if (tafsils?.Count == 0)
                tafsils = null;

            List<int>? MoeinsId = await _db.Acc_Articles
                .Where(n => n.Doc.SellerId == sellerId
                && (tafsils != null ? tafsils.Contains(n.Tafsil4Id.Value) : n.MoeinId > 0))
                .Select(n => n.MoeinId).Distinct().ToListAsync<int>();

            var moeins = await _db.Acc_Coding_Moeins
                .Where(n => (MoeinsId != null || MoeinsId.Count == 0) ? MoeinsId.Contains(n.Id) : n.SellerId == sellerId)
                .Select(n => new MoeinDto
                {
                    Id = n.Id,
                    MoeinName = n.MoeinName,
                    Nature = n.Nature,
                    KolId = n.KolId,
                }).ToListAsync();

            return moeins;
        }


        // Tafsil
        public async Task<SelectList> SelectList_TafsilGroupAsync(Int64 sellerId)
        {
            var lst = await _db.Acc_Coding_TafsilGroups.Where(n => n.SellerId == sellerId || n.SellerId == null)
                   .Select(n => new { id = n.Id, name = n.GroupName }).OrderBy(n => n.name).ToListAsync();
            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_TafsilsAsync(Int64? SellerId = null)
        {
            var lst = await _db.Acc_Coding_Tafsils.Where(n => n.SellerId == SellerId)
                 .Select(n => new { id = n.Id, name = n.Name }).OrderBy(n => n.name).ToListAsync();
            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_UsageTafsilsAsync(Int64? SellerId = null)
        {
            // دریافت داده‌ها از پایگاه داده
            var lst = await _db.Acc_Articles
                .Where(n => SellerId != null ? n.SellerId == SellerId : n.Tafsil4Id > 0 && n.Tafsil4Id != null && n.IsDeleted == false)
                .Select(n => new { id = n.Tafsil4Id, name = n.Tafsil4Name })
                .Distinct()
                .OrderBy(x => x.name)
                .ToListAsync();

            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_UsageTafsils5Async(Int64? SellerId = null)
        {
            // دریافت داده‌ها از پایگاه داده
            var lst = await _db.Acc_Articles
                .Where(n => SellerId != null ? n.SellerId == SellerId : n.Tafsil5Id > 0 && n.Tafsil5Id != null && n.IsDeleted == false)
                .Select(n => new { id = n.Tafsil5Id, name = n.Tafsil5Name })
                .Distinct()
                .OrderBy(x => x.name)
                .ToListAsync();

            return new SelectList(lst, "id", "name");
        }
        public async Task<SelectList> SelectList_UsageTafsils6Async(Int64? SellerId = null)
        {
            // دریافت داده‌ها از پایگاه داده
            var lst = await _db.Acc_Articles
                .Where(n => SellerId != null ? n.SellerId == SellerId : n.Tafsil6Id > 0 && n.Tafsil6Id != null && n.IsDeleted == false)
                .Select(n => new { id = n.Tafsil6Id, name = n.Tafsil6Name })
                .Distinct()
                .OrderBy(x => x.name)
                .ToListAsync();

            return new SelectList(lst, "id", "name");
        }
        public async Task<List<TafsilDto>> GetTafsilByTafsilGroupAsync(long sellerId, int? GroupId)
        {
            var query = _db.Acc_Coding_TafsilToGroups.AsNoTracking().Include(n => n.TafsilAccount)
                .Where(n => n.TafsilAccount.SellerId == null || n.TafsilAccount.SellerId == sellerId).AsQueryable();
            if (GroupId.HasValue)
                query = query.Where(n => n.GroupId == GroupId);

            return await query.Select(n => new TafsilDto
            {
                Id = n.TafsilAccount.Id,
                Name = n.TafsilAccount.Name
            }).OrderBy(n => n.Name).ToListAsync();
        }
        public async Task<List<TafsilDto>> GetTafsil5Async(long sellerId, List<long?> tafsils4)
        {
            var query = _db.Acc_Articles.AsNoTracking()
                .Where(n => n.SellerId == sellerId && !n.IsDeleted).Select(n => new { t4 = n.Tafsil4Id, t5 = n.Tafsil5Id }).AsQueryable();
            if (tafsils4.Any())
                query = query.Where(n => tafsils4.Contains(n.t4));

            List<long?> tafsilId = await query.Select(n => n.t5).Distinct().ToListAsync();
            var tafsilList = await _db.Acc_Coding_Tafsils.AsNoTracking().Where(n => tafsilId.Contains(n.Id))
                .Select(n => new TafsilDto
                {
                    Id = n.Id,
                    Name = n.Name
                }).OrderBy(n => n.Name).ToListAsync();
            return tafsilList;
        }
        public async Task<List<long>> GetGroupTafsilIdsAsync(long sellerId, int? GroupId)
        {
            var query = _db.Acc_Coding_TafsilToGroups.AsNoTracking().Include(n => n.TafsilAccount)
                 .Where(n => n.TafsilAccount.SellerId == null || n.TafsilAccount.SellerId == sellerId).AsQueryable();
            if (GroupId.HasValue)
                query = query.Where(n => n.GroupId == GroupId);

            List<long> tafsils = await query.Select(n => n.TafsilId).ToListAsync();

            return tafsils;
        }
    }
}
