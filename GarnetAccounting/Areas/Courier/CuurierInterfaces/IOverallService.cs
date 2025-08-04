using System.Linq.Expressions;

namespace GarnetAccounting.Areas.Courier.CuurierInterfaces
{
    public interface IOverallService
    {
        bool DoesDataExist<T>(Expression<Func<T, bool>> filter) where T : class;
        //public Task<string> GenerateBranchCode();
    }
}
