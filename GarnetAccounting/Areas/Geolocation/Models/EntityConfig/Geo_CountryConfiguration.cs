

using GarnetAccounting.Areas.Geolocation.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Geolocation.Models.EntityConfig
{
    public class Geo_CountryConfiguration : IEntityTypeConfiguration<Geo_Country>
    {
        public void Configure(EntityTypeBuilder<Geo_Country> builder)
        {
            // تنظیمات فیلدها
            builder.HasKey(c => c.Id);

            // داده‌های اولیه
            builder.HasData(
                new Geo_Country { Id = 1, PersianName = "ایران", EnglishName = "Iran", Code = "IR", NumericCode = 364 },
                new Geo_Country { Id = 2, PersianName = "کویت", EnglishName = "Kuwait", Code = "KW", NumericCode = 414 },
                new Geo_Country { Id = 3, PersianName = "کانادا", EnglishName = "Canada", Code = "CA", NumericCode = 124 },
                new Geo_Country { Id = 4, PersianName = "انگلیس", EnglishName = "United Kingdom", Code = "GB", NumericCode = 826 },
                new Geo_Country { Id = 5, PersianName = "آلمان", EnglishName = "Germany", Code = "DE", NumericCode = 276 },
                new Geo_Country { Id = 6, PersianName = "سوئد", EnglishName = "Sweden", Code = "SE", NumericCode = 752 },
                new Geo_Country { Id = 7, PersianName = "سوئیس", EnglishName = "Switzerland", Code = "CH", NumericCode = 756 },
                new Geo_Country { Id = 8, PersianName = "فرانسه", EnglishName = "France", Code = "FR", NumericCode = 250 },
                new Geo_Country { Id = 9, PersianName = "امارات", EnglishName = "United Arab Emirates", Code = "AE", NumericCode = 784 },
                new Geo_Country { Id = 10, PersianName = "عمان", EnglishName = "Oman", Code = "OM", NumericCode = 512 },
                new Geo_Country { Id = 11, PersianName = "استرالیا", EnglishName = "Australia", Code = "AU", NumericCode = 36 }
            );
        }
    }
}
