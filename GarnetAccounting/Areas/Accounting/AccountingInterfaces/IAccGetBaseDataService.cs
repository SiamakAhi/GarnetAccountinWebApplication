using GarnetAccounting.Areas.Accounting.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Accounting.AccountingInterfaces
{
    public interface IAccGetBaseDataService
    {
        Task<SelectList> GetKolsAsync(long sellerId);
        Task<SelectList> GetMoeinsAsync(long sellerId);
        Task<List<KolDto>> GetUsedKolsAsync(long sellerId);
        Task<List<KolDto>> GetUsedKolsByTafsilGroupAsync(long sellerId, List<int>? groups);
        Task<List<KolDto>> GetCascadingUsedKolsAsync(long sellerId, List<int>? groups, List<long>? tafsils = null);
        Task<List<KolDto>> GetUsedKolsByTafsilAsync(long sellerId, List<long>? tafsils);
        Task<SelectList> SelectList_UsedKolsByTafsilAsync(long sellerId, List<long>? tafsils);
        Task<List<MoeinDto>> GetUsedMoeinsByKolsAsync(long sellerId, List<int>? kols);
        Task<SelectList> SelectListUsedMoeinsByKolsAsync(long sellerId, List<int>? kols);
        Task<List<MoeinDto>> GetUsedMoeinsByKolAndTafsilAsync(long sellerId, List<long>? tafsils, List<int>? kols = null);
        Task<List<MoeinDto>> GetUsedMoeinsByTafsilAsync(long sellerId, List<long>? tafsils);
        Task<List<TafsilDto>> GetTafsilByTafsilGroupAsync(long sellerId, int? GroupId);
        Task<List<TafsilDto>> GetTafsil5Async(long sellerId, List<long?> tafsils4);
        Task<List<long>> GetGroupTafsilIdsAsync(long sellerId, int? GroupId);


        //
        Task<SelectList> SelectList_TafsilGroupAsync(Int64 sellerId);
        Task<SelectList> SelectList_UsageTafsilsAsync(Int64? SellerId = null);
        Task<SelectList> SelectList_UsageTafsils5Async(Int64? SellerId = null);
        Task<SelectList> SelectList_UsageTafsils6Async(Int64? SellerId = null);
    }
}
