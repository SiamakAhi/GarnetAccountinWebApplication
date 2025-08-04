using GarnetAccounting.Areas.Geolocation.GeolocationInterfaces;
using GarnetAccounting.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Geolocation.GeolocationServices
{
    public class GeoGeneralService : IGeoGeneralService
    {
        private readonly AppDbContext _db;

        public GeoGeneralService(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<SelectList> SelectItems_CountriesAsync()
        {
            var countries = await _db.Geo_Countries.Select(n => new { id = n.Id, name = n.PersianName }).ToListAsync();
            return new SelectList(countries, "id", "name");
        }

        public async Task<SelectList> SelectItems_ProvincesAsync(int CountryId = 1)
        {
            var provinces = await _db.Geo_Provinces.Where(n => n.CountryId == CountryId).Select(p => new { id = p.Id, name = p.PersianName, group = p.Country.PersianName }).ToListAsync();

            return new SelectList(provinces, "id", "name", "group");
        }
        public async Task<SelectList> SelectItems_CitiesAsync(int CountryId = 1)
        {
            var provinces = await _db.Geo_Cities.Select(p => new { id = p.Id, name = p.PersianName, group = p.Province.PersianName }).ToListAsync();
            return new SelectList(provinces, "id", "name", "group", "group");
        }

    }
}
