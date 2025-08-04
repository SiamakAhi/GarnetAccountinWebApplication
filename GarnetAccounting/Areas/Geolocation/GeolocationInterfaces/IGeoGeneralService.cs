using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarnetAccounting.Areas.Geolocation.GeolocationInterfaces
{
    public interface IGeoGeneralService
    {
        Task<SelectList> SelectItems_CountriesAsync();
        Task<SelectList> SelectItems_ProvincesAsync(int CountryId = 1);
        Task<SelectList> SelectItems_CitiesAsync(int CountryId = 1);
    }
}
