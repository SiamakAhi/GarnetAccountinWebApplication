using GarnetAccounting.Areas.Courier.CuurierInterfaces;
using GarnetAccounting.Models;
using System.Linq.Expressions;

namespace GarnetAccounting.Areas.Courier.CuurierServices
{
    public class OverallService : IOverallService
    {
        private readonly AppDbContext _db;

        public OverallService(AppDbContext db)
        {
            _db = db;
        }
        public bool DoesDataExist<T>(Expression<Func<T, bool>> filter) where T : class
        {
            // چک کردن وجود اطلاعات با استفاده از فیلتر مشخص شده
            return _db.Set<T>().Any(filter);
        }
        //public async Task<string> GenerateBranchCode()
        //{
        //    var lastBranch = await _db.Cu_Branch
        //        .OrderByDescending(e => e.Id)
        //        .FirstOrDefaultAsync();

        //    if (lastBranch == null)
        //    {
        //        return "001";
        //    }
        //    else
        //    {
        //        var lastBranchNumber = int.Parse(lastBranch.BranchCode.Substring(lastBranch.BranchCode.Length - 3));
        //        {
        //            int newBranchNumber = lastBranchNumber + 1;
        //            string newBranchCode = newBranchNumber.ToString();
        //            return newBranchCode;
        //        }
        //    }


        //}
    }

}
