using GarnetAccounting.Areas.Geolocation.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Geolocation.Models.EntityConfig
{
    public class Geo_ProvinceConfiguration : IEntityTypeConfiguration<Geo_Province>
    {
        public void Configure(EntityTypeBuilder<Geo_Province> builder)
        {
            // تنظیمات کلید اصلی
            builder.HasKey(p => p.Id);

            // ارتباط با کشور
            builder.HasOne(p => p.Country)
                .WithMany(c => c.Provinces)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            // داده‌های پیش‌فرض
            builder.HasData(
          new Geo_Province { Id = 1, PersianName = "آذربایجان شرقی", EnglishName = "East Azerbaijan", UniqueCode = 1101, CountryId = 1 },
          new Geo_Province { Id = 2, PersianName = "آذربایجان غربی", EnglishName = "West Azerbaijan", UniqueCode = 1102, CountryId = 1 },
          new Geo_Province { Id = 3, PersianName = "اردبیل", EnglishName = "Ardabil", UniqueCode = 1103, CountryId = 1 },
          new Geo_Province { Id = 4, PersianName = "اصفهان", EnglishName = "Isfahan", UniqueCode = 1104, CountryId = 1 },
          new Geo_Province { Id = 5, PersianName = "البرز", EnglishName = "Alborz", UniqueCode = 1105, CountryId = 1 },
          new Geo_Province { Id = 6, PersianName = "ایلام", EnglishName = "Ilam", UniqueCode = 1106, CountryId = 1 },
          new Geo_Province { Id = 7, PersianName = "بوشهر", EnglishName = "Bushehr", UniqueCode = 1107, CountryId = 1 },
          new Geo_Province { Id = 8, PersianName = "تهران", EnglishName = "Tehran", UniqueCode = 1108, CountryId = 1 },
          new Geo_Province { Id = 9, PersianName = "چهارمحال و بختیاری", EnglishName = "Chaharmahal and Bakhtiari", UniqueCode = 1109, CountryId = 1 },
          new Geo_Province { Id = 10, PersianName = "خراسان جنوبی", EnglishName = "South Khorasan", UniqueCode = 1110, CountryId = 1 },
          new Geo_Province { Id = 11, PersianName = "خراسان رضوی", EnglishName = "Razavi Khorasan", UniqueCode = 1111, CountryId = 1 },
          new Geo_Province { Id = 12, PersianName = "خراسان شمالی", EnglishName = "North Khorasan", UniqueCode = 1112, CountryId = 1 },
          new Geo_Province { Id = 13, PersianName = "خوزستان", EnglishName = "Khuzestan", UniqueCode = 1113, CountryId = 1 },
          new Geo_Province { Id = 14, PersianName = "زنجان", EnglishName = "Zanjan", UniqueCode = 1114, CountryId = 1 },
          new Geo_Province { Id = 15, PersianName = "سمنان", EnglishName = "Semnan", UniqueCode = 1115, CountryId = 1 },
          new Geo_Province { Id = 16, PersianName = "سیستان و بلوچستان", EnglishName = "Sistan and Baluchestan", UniqueCode = 1116, CountryId = 1 },
          new Geo_Province { Id = 17, PersianName = "فارس", EnglishName = "Fars", UniqueCode = 1117, CountryId = 1 },
          new Geo_Province { Id = 18, PersianName = "قزوین", EnglishName = "Qazvin", UniqueCode = 1118, CountryId = 1 },
          new Geo_Province { Id = 19, PersianName = "قم", EnglishName = "Qom", UniqueCode = 1119, CountryId = 1 },
          new Geo_Province { Id = 20, PersianName = "کردستان", EnglishName = "Kurdistan", UniqueCode = 1120, CountryId = 1 },
          new Geo_Province { Id = 21, PersianName = "کرمان", EnglishName = "Kerman", UniqueCode = 1121, CountryId = 1 },
          new Geo_Province { Id = 22, PersianName = "کرمانشاه", EnglishName = "Kermanshah", UniqueCode = 1122, CountryId = 1 },
          new Geo_Province { Id = 23, PersianName = "کهگیلویه و بویراحمد", EnglishName = "Kohgiluyeh and Boyer-Ahmad", UniqueCode = 1123, CountryId = 1 },
          new Geo_Province { Id = 24, PersianName = "گلستان", EnglishName = "Golestan", UniqueCode = 1124, CountryId = 1 },
          new Geo_Province { Id = 25, PersianName = "گیلان", EnglishName = "Gilan", UniqueCode = 1125, CountryId = 1 },
          new Geo_Province { Id = 26, PersianName = "لرستان", EnglishName = "Lorestan", UniqueCode = 1126, CountryId = 1 },
          new Geo_Province { Id = 27, PersianName = "مازندران", EnglishName = "Mazandaran", UniqueCode = 1127, CountryId = 1 },
          new Geo_Province { Id = 28, PersianName = "مرکزی", EnglishName = "Markazi", UniqueCode = 1128, CountryId = 1 },
          new Geo_Province { Id = 29, PersianName = "هرمزگان", EnglishName = "Hormozgan", UniqueCode = 1129, CountryId = 1 },
          new Geo_Province { Id = 30, PersianName = "همدان", EnglishName = "Hamedan", UniqueCode = 1130, CountryId = 1 },
          new Geo_Province { Id = 31, PersianName = "یزد", EnglishName = "Yazd", UniqueCode = 1131, CountryId = 1 }
      );

        }
    }
}
