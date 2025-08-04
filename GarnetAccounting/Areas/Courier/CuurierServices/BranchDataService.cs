using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class BranchDataService
    {
        private readonly AppDbContext _db;

        public BranchDataService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<SelectList> SelectList_oldSysBranchsAsync(long sellerId)
        {
            var data = await _db.Cu_Branch.Where(n => n.SellerId == sellerId && n.IsActive)
                .Select(n => new { id = n.OldBranchName, name = n.BranchName }).OrderBy(n => n.name).ToListAsync();

            return new SelectList(data, "id", "name");
        }
        public async Task<SelectList> SelectList_oldSysDistributerAsync(long sellerId)
        {
            var data = await _db.Cu_Branch.Where(n => n.SellerId == sellerId && n.IsActive)
                .Select(n => new { id = n.OldDistRepName, name = n.BranchName }).OrderBy(n => n.name).ToListAsync();

            return new SelectList(data, "id", "name");
        }
        public async Task<SelectList> SelectList_oldSysCitiesAsync(long sellerId)
        {
            var data = await _db.Geo_Cities
                .Select(n => new { id = n.PersianName, name = n.PersianName }).OrderBy(n => n.name).ToListAsync();

            return new SelectList(data, "id", "name");
        }
        public async Task<SelectList> SelectList_CitiesAsync(long sellerId)
        {
            var data = await _db.Geo_Cities
                .Select(n => new { id = n.Id, name = n.PersianName }).OrderBy(n => n.name).ToListAsync();

            return new SelectList(data, "id", "name");
        }

    }
}
