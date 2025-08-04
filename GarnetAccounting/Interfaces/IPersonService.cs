using GarnetAccounting.ViewModels;
using GarnetAccounting.ViewModels.PartyDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonDto>> PersenAsync(long Seller, string? name = null, string? phone = null, short? legal = null, string? nationalId = null, string? tafsilCode = null, bool? vendor = null, bool? customer = null);
        IQueryable<PersonDto> PersenAsQuery(PersonFilterDto filter);
        Task<SelectList> SelectList_PartyTypeAsync();
        Task<PersonDto> GetPersonDtoAsync(long Id);
        Task<clsResult> AddPersonAsync(PersonDto dto);
        Task<clsResult> UpdatePersonAsync(PersonDto dto);

        Task<clsResult> SetPersonTafsilAsync(long personId, long tafsilId);
        Task<clsResult> RemoveTafsilFromPersonAsync(long personId);
        Task<string> GetPersonNameByIdAsync(Int64 id);
        Task<SelectList> SelectList_PersenAsync(Int64 sellerId, short? role = null);
        Task<SelectList> SelectList_PersenListAsync(Int64 sellerId, bool? isVendor = null, bool? isCustomer = null);
        Task<clsResult> DeletePersonAsync(Int64 id);
        Task<long> GetOrCreatePersonIdAsync(string personName, string? personCode = null, string? nationalId = null, string? economicCode = null);

        // ==== Party Representative
        Task<SelectList> SelectList_PersenRepresentativesAsync(Int64 sellerId);
        Task<List<PresentativeDto>> GetPersenRepresentativesDtoAsync(Int64 sellerId);
        Task<SelectList> SelectItems_TafsilsByGroupAsync(int[] groupIds, long sellerId);
        Task<clsResult> AddPartyRepresentativeAsync(long PartyId, long RepresentativeId);
        Task<clsResult> RemovePartyRepresentativeAsync(long PartyId, long RepresentativeId);
        Task<clsResult> CreateBulkTafsilsAsync(long sellerId);
    }
}
