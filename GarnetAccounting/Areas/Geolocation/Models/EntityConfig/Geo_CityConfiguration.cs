using GarnetAccounting.Areas.Geolocation.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Geolocation.Models.EntityConfig
{
    public class Geo_CityConfiguration : IEntityTypeConfiguration<Geo_City>
    {
        public void Configure(EntityTypeBuilder<Geo_City> builder)
        {
            // تنظیمات کلید اصلی
            builder.HasKey(c => c.Id);

            // ارتباط با استان
            builder.HasOne(c => c.Province)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);

            // داده‌های پیش‌فرض برای شهرها
            builder.HasData(
                // شهرهای استان آذربایجان شرقی
                new Geo_City { Id = 1, PersianName = "تبریز", EnglishName = "Tabriz", UniqueCode = 2101, ProvinceId = 1, IsCapital = true },
                new Geo_City { Id = 2, PersianName = "مراغه", EnglishName = "Maragheh", UniqueCode = 2102, ProvinceId = 1 },
                new Geo_City { Id = 3, PersianName = "میانه", EnglishName = "Mianeh", UniqueCode = 2103, ProvinceId = 1 },
                new Geo_City { Id = 4, PersianName = "مرند", EnglishName = "Marand", UniqueCode = 2104, ProvinceId = 1 },
                new Geo_City { Id = 5, PersianName = "آذرشهر", EnglishName = "Azarshahr", UniqueCode = 2105, ProvinceId = 1 },

                // شهرهای استان آذربایجان غربی
                new Geo_City { Id = 6, PersianName = "ارومیه", EnglishName = "Urmia", UniqueCode = 2201, ProvinceId = 2, IsCapital = true },
                new Geo_City { Id = 7, PersianName = "خوی", EnglishName = "Khoy", UniqueCode = 2202, ProvinceId = 2 },
                new Geo_City { Id = 8, PersianName = "سلماس", EnglishName = "Salmas", UniqueCode = 2203, ProvinceId = 2 },
                new Geo_City { Id = 9, PersianName = "مهاباد", EnglishName = "Mahabad", UniqueCode = 2204, ProvinceId = 2 },
                new Geo_City { Id = 10, PersianName = "پیرانشهر", EnglishName = "Piranshahr", UniqueCode = 2205, ProvinceId = 2 },

                // شهرهای استان اردبیل
                new Geo_City { Id = 11, PersianName = "اردبیل", EnglishName = "Ardabil", UniqueCode = 2301, ProvinceId = 3, IsCapital = true },
                new Geo_City { Id = 12, PersianName = "مشگین‌شهر", EnglishName = "Meshginshahr", UniqueCode = 2302, ProvinceId = 3 },
                new Geo_City { Id = 13, PersianName = "پارس‌آباد", EnglishName = "Parsabad", UniqueCode = 2303, ProvinceId = 3 },
                new Geo_City { Id = 14, PersianName = "خلخال", EnglishName = "Khalkhal", UniqueCode = 2304, ProvinceId = 3 },
                new Geo_City { Id = 15, PersianName = "بیله‌سوار", EnglishName = "Bileh Savar", UniqueCode = 2305, ProvinceId = 3 },

                // شهرهای استان اصفهان
                new Geo_City { Id = 16, PersianName = "اصفهان", EnglishName = "Isfahan", UniqueCode = 2401, ProvinceId = 4, IsCapital = true },
                new Geo_City { Id = 17, PersianName = "کاشان", EnglishName = "Kashan", UniqueCode = 2402, ProvinceId = 4 },
                new Geo_City { Id = 18, PersianName = "خمینی‌شهر", EnglishName = "Khomeinishahr", UniqueCode = 2403, ProvinceId = 4 },
                new Geo_City { Id = 19, PersianName = "فلاورجان", EnglishName = "Falavarjan", UniqueCode = 2404, ProvinceId = 4 },
                new Geo_City { Id = 20, PersianName = "نائین", EnglishName = "Na'in", UniqueCode = 2405, ProvinceId = 4 },

                // شهرهای استان البرز
                new Geo_City { Id = 21, PersianName = "کرج", EnglishName = "Karaj", UniqueCode = 2501, ProvinceId = 5, IsCapital = true },
                new Geo_City { Id = 22, PersianName = "نظرآباد", EnglishName = "Nazarabad", UniqueCode = 2502, ProvinceId = 5 },
                new Geo_City { Id = 23, PersianName = "فردیس", EnglishName = "Fardis", UniqueCode = 2503, ProvinceId = 5 },
                new Geo_City { Id = 24, PersianName = "ساوجبلاغ", EnglishName = "Savojbolagh", UniqueCode = 2504, ProvinceId = 5 },
                new Geo_City { Id = 25, PersianName = "اشتهارد", EnglishName = "Eshtehard", UniqueCode = 2505, ProvinceId = 5 },

                // شهرهای استان ایلام
                new Geo_City { Id = 26, PersianName = "ایلام", EnglishName = "Ilam", UniqueCode = 2601, ProvinceId = 6, IsCapital = true },
                new Geo_City { Id = 27, PersianName = "دهلران", EnglishName = "Dehloran", UniqueCode = 2602, ProvinceId = 6 },
                new Geo_City { Id = 28, PersianName = "ایوان", EnglishName = "Ivan", UniqueCode = 2603, ProvinceId = 6 },
                new Geo_City { Id = 29, PersianName = "آبدانان", EnglishName = "Abdanan", UniqueCode = 2604, ProvinceId = 6 },
                new Geo_City { Id = 30, PersianName = "مهران", EnglishName = "Mehran", UniqueCode = 2605, ProvinceId = 6 },

                // شهرهای استان بوشهر
                new Geo_City { Id = 31, PersianName = "بوشهر", EnglishName = "Bushehr", UniqueCode = 2701, ProvinceId = 7, IsCapital = true },
                new Geo_City { Id = 32, PersianName = "برازجان", EnglishName = "Borazjan", UniqueCode = 2702, ProvinceId = 7 },
                new Geo_City { Id = 33, PersianName = "گناوه", EnglishName = "Genaveh", UniqueCode = 2703, ProvinceId = 7 },
                new Geo_City { Id = 34, PersianName = "خورموج", EnglishName = "Khormoj", UniqueCode = 2704, ProvinceId = 7 },
                new Geo_City { Id = 35, PersianName = "دشتی", EnglishName = "Dashti", UniqueCode = 2705, ProvinceId = 7 },

                // شهرهای استان تهران
                new Geo_City { Id = 36, PersianName = "تهران", EnglishName = "Tehran", UniqueCode = 2801, ProvinceId = 8, IsCapital = true },
                new Geo_City { Id = 37, PersianName = "ری", EnglishName = "Rey", UniqueCode = 2802, ProvinceId = 8 },
                new Geo_City { Id = 38, PersianName = "اسلام‌شهر", EnglishName = "Eslamshahr", UniqueCode = 2803, ProvinceId = 8 },
                new Geo_City { Id = 39, PersianName = "شهریار", EnglishName = "Shahriar", UniqueCode = 2804, ProvinceId = 8 },
                new Geo_City { Id = 40, PersianName = "ورامین", EnglishName = "Varamin", UniqueCode = 2805, ProvinceId = 8 },

                // شهرهای استان چهارمحال و بختیاری
                new Geo_City { Id = 41, PersianName = "شهرکرد", EnglishName = "Shahrekord", UniqueCode = 2901, ProvinceId = 9, IsCapital = true },
                new Geo_City { Id = 42, PersianName = "بروجن", EnglishName = "Borujen", UniqueCode = 2902, ProvinceId = 9 },
                new Geo_City { Id = 43, PersianName = "فارسان", EnglishName = "Farsan", UniqueCode = 2903, ProvinceId = 9 },
                new Geo_City { Id = 44, PersianName = "لردگان", EnglishName = "Lordegan", UniqueCode = 2904, ProvinceId = 9 },
                new Geo_City { Id = 45, PersianName = "فرخ‌شهر", EnglishName = "Fereydunshahr", UniqueCode = 2905, ProvinceId = 9 },

                // شهرهای استان خراسان جنوبی
                new Geo_City { Id = 46, PersianName = "بیرجند", EnglishName = "Birjand", UniqueCode = 3001, ProvinceId = 10, IsCapital = true },
                new Geo_City { Id = 47, PersianName = "قائن", EnglishName = "Qaen", UniqueCode = 3002, ProvinceId = 10 },
                new Geo_City { Id = 48, PersianName = "طبس", EnglishName = "Tabas", UniqueCode = 3003, ProvinceId = 10 },
                new Geo_City { Id = 49, PersianName = "فردوس", EnglishName = "Ferdows", UniqueCode = 3004, ProvinceId = 10 },
                new Geo_City { Id = 50, PersianName = "نهبندان", EnglishName = "Nehbandan", UniqueCode = 3005, ProvinceId = 10 },

                // شهرهای استان خراسان رضوی
                new Geo_City { Id = 51, PersianName = "مشهد", EnglishName = "Mashhad", UniqueCode = 3101, ProvinceId = 11, IsCapital = true },
                new Geo_City { Id = 52, PersianName = "نیشابور", EnglishName = "Neyshabur", UniqueCode = 3102, ProvinceId = 11 },
                new Geo_City { Id = 53, PersianName = "سبزوار", EnglishName = "Sabzevar", UniqueCode = 3103, ProvinceId = 11 },
                new Geo_City { Id = 54, PersianName = "تربت‌حیدریه", EnglishName = "Torbate Heydarieh", UniqueCode = 3104, ProvinceId = 11 },
                new Geo_City { Id = 55, PersianName = "قوچان", EnglishName = "Quchan", UniqueCode = 3105, ProvinceId = 11 },

                // شهرهای استان خراسان شمالی
                new Geo_City { Id = 56, PersianName = "بجنورد", EnglishName = "Bojnord", UniqueCode = 3201, ProvinceId = 12, IsCapital = true },
                new Geo_City { Id = 57, PersianName = "شیروان", EnglishName = "Shirvan", UniqueCode = 3202, ProvinceId = 12 },
                new Geo_City { Id = 58, PersianName = "اسفراین", EnglishName = "Esfarayen", UniqueCode = 3203, ProvinceId = 12 },
                new Geo_City { Id = 59, PersianName = "آشخانه", EnglishName = "Ashkhaneh", UniqueCode = 3204, ProvinceId = 12 },
                new Geo_City { Id = 60, PersianName = "جاجرم", EnglishName = "Jajarm", UniqueCode = 3205, ProvinceId = 12 },

                // شهرهای استان خوزستان
                new Geo_City { Id = 61, PersianName = "اهواز", EnglishName = "Ahvaz", UniqueCode = 3301, ProvinceId = 13, IsCapital = true },
                new Geo_City { Id = 62, PersianName = "آبادان", EnglishName = "Abadan", UniqueCode = 3302, ProvinceId = 13 },
                new Geo_City { Id = 63, PersianName = "خرمشهر", EnglishName = "Khorramshahr", UniqueCode = 3303, ProvinceId = 13 },
                new Geo_City { Id = 64, PersianName = "دزفول", EnglishName = "Dezful", UniqueCode = 3304, ProvinceId = 13 },
                new Geo_City { Id = 65, PersianName = "شوشتر", EnglishName = "Shooshtar", UniqueCode = 3305, ProvinceId = 13 },

                // شهرهای استان زنجان
                new Geo_City { Id = 66, PersianName = "زنجان", EnglishName = "Zanjan", UniqueCode = 3401, ProvinceId = 14, IsCapital = true },
                new Geo_City { Id = 67, PersianName = "ابهر", EnglishName = "Abhar", UniqueCode = 3402, ProvinceId = 14 },
                new Geo_City { Id = 68, PersianName = "خدابنده", EnglishName = "Khoda Bandeh", UniqueCode = 3403, ProvinceId = 14 },
                new Geo_City { Id = 69, PersianName = "خرمدره", EnglishName = "Khoramdareh", UniqueCode = 3404, ProvinceId = 14 },
                new Geo_City { Id = 70, PersianName = "ماه‌نشان", EnglishName = "Mahnashan", UniqueCode = 3405, ProvinceId = 14 },

                // شهرهای استان سمنان
                new Geo_City { Id = 71, PersianName = "سمنان", EnglishName = "Semnan", UniqueCode = 3501, ProvinceId = 15, IsCapital = true },
                new Geo_City { Id = 72, PersianName = "شاهرود", EnglishName = "Shahrood", UniqueCode = 3502, ProvinceId = 15 },
                new Geo_City { Id = 73, PersianName = "دامغان", EnglishName = "Damghan", UniqueCode = 3503, ProvinceId = 15 },
                new Geo_City { Id = 74, PersianName = "گرمسار", EnglishName = "Garmsar", UniqueCode = 3504, ProvinceId = 15 },
                new Geo_City { Id = 75, PersianName = "مهدیشهر", EnglishName = "Mahdishahr", UniqueCode = 3505, ProvinceId = 15 },

                // شهرهای استان سیستان و بلوچستان
                new Geo_City { Id = 76, PersianName = "زاهدان", EnglishName = "Zahedan", UniqueCode = 3601, ProvinceId = 16, IsCapital = true },
                new Geo_City { Id = 77, PersianName = "چابهار", EnglishName = "Chabahar", UniqueCode = 3602, ProvinceId = 16 },
                new Geo_City { Id = 78, PersianName = "ایرانشهر", EnglishName = "Iranshahr", UniqueCode = 3603, ProvinceId = 16 },
                new Geo_City { Id = 79, PersianName = "زابل", EnglishName = "Zabol", UniqueCode = 3604, ProvinceId = 16 },
                new Geo_City { Id = 80, PersianName = "خاش", EnglishName = "Khash", UniqueCode = 3605, ProvinceId = 16 },

                // شهرهای استان فارس
                new Geo_City { Id = 81, PersianName = "شیراز", EnglishName = "Shiraz", UniqueCode = 3701, ProvinceId = 17, IsCapital = true },
                new Geo_City { Id = 82, PersianName = "مرودشت", EnglishName = "Marvdasht", UniqueCode = 3702, ProvinceId = 17 },
                new Geo_City { Id = 83, PersianName = "جهرم", EnglishName = "Jahrom", UniqueCode = 3703, ProvinceId = 17 },
                new Geo_City { Id = 84, PersianName = "لار", EnglishName = "Lar", UniqueCode = 3704, ProvinceId = 17 },
                new Geo_City { Id = 85, PersianName = "فسا", EnglishName = "Fasa", UniqueCode = 3705, ProvinceId = 17 },

                // شهرهای استان قزوین
                new Geo_City { Id = 86, PersianName = "قزوین", EnglishName = "Qazvin", UniqueCode = 3801, ProvinceId = 18, IsCapital = true },
                new Geo_City { Id = 87, PersianName = "تاکستان", EnglishName = "Takestan", UniqueCode = 3802, ProvinceId = 18 },
                new Geo_City { Id = 88, PersianName = "آبیک", EnglishName = "Abyek", UniqueCode = 3803, ProvinceId = 18 },
                new Geo_City { Id = 89, PersianName = "الوند", EnglishName = "Alvand", UniqueCode = 3804, ProvinceId = 18 },
                new Geo_City { Id = 90, PersianName = "بوئین‌زهرا", EnglishName = "Bu’in Zahra", UniqueCode = 3805, ProvinceId = 18 },

                // شهرهای استان قم
                new Geo_City { Id = 91, PersianName = "قم", EnglishName = "Qom", UniqueCode = 3901, ProvinceId = 19, IsCapital = true },
                new Geo_City { Id = 92, PersianName = "کهک", EnglishName = "Kahak", UniqueCode = 3902, ProvinceId = 19 },
                new Geo_City { Id = 93, PersianName = "سلفچگان", EnglishName = "Salafchegan", UniqueCode = 3903, ProvinceId = 19 },
                new Geo_City { Id = 94, PersianName = "جعفریه", EnglishName = "Jafariyeh", UniqueCode = 3904, ProvinceId = 19 },
                new Geo_City { Id = 95, PersianName = "دستجرد", EnglishName = "Dastjerd", UniqueCode = 3905, ProvinceId = 19 },

                // شهرهای استان کردستان
                new Geo_City { Id = 96, PersianName = "سنندج", EnglishName = "Sanandaj", UniqueCode = 4001, ProvinceId = 20, IsCapital = true },
                new Geo_City { Id = 97, PersianName = "مریوان", EnglishName = "Marivan", UniqueCode = 4002, ProvinceId = 20 },
                new Geo_City { Id = 98, PersianName = "سقز", EnglishName = "Saqez", UniqueCode = 4003, ProvinceId = 20 },
                new Geo_City { Id = 99, PersianName = "بانه", EnglishName = "Baneh", UniqueCode = 4004, ProvinceId = 20 },
                new Geo_City { Id = 100, PersianName = "بیجار", EnglishName = "Bijar", UniqueCode = 4005, ProvinceId = 20 },

                // شهرهای استان کرمان
                new Geo_City { Id = 101, PersianName = "کرمان", EnglishName = "Kerman", UniqueCode = 4101, ProvinceId = 21, IsCapital = true },
                new Geo_City { Id = 102, PersianName = "سیرجان", EnglishName = "Sirjan", UniqueCode = 4102, ProvinceId = 21 },
                new Geo_City { Id = 103, PersianName = "رفسنجان", EnglishName = "Rafsanjan", UniqueCode = 4103, ProvinceId = 21 },
                new Geo_City { Id = 104, PersianName = "جیرفت", EnglishName = "Jiroft", UniqueCode = 4104, ProvinceId = 21 },
                new Geo_City { Id = 105, PersianName = "زرند", EnglishName = "Zarand", UniqueCode = 4105, ProvinceId = 21 },

                // شهرهای استان کرمانشاه
                new Geo_City { Id = 106, PersianName = "کرمانشاه", EnglishName = "Kermanshah", UniqueCode = 4201, ProvinceId = 22, IsCapital = true },
                new Geo_City { Id = 107, PersianName = "اسلام‌آباد غرب", EnglishName = "Eslamabad Gharb", UniqueCode = 4202, ProvinceId = 22 },
                new Geo_City { Id = 108, PersianName = "هرسین", EnglishName = "Harsin", UniqueCode = 4203, ProvinceId = 22 },
                new Geo_City { Id = 109, PersianName = "سنقر", EnglishName = "Sonqor", UniqueCode = 4204, ProvinceId = 22 },
                new Geo_City { Id = 110, PersianName = "سرپل ذهاب", EnglishName = "Sarpol-e Zahab", UniqueCode = 4205, ProvinceId = 22 },

                // شهرهای استان کهگیلویه و بویراحمد
                new Geo_City { Id = 111, PersianName = "یاسوج", EnglishName = "Yasuj", UniqueCode = 4301, ProvinceId = 23, IsCapital = true },
                new Geo_City { Id = 112, PersianName = "گچساران", EnglishName = "Gachsaran", UniqueCode = 4302, ProvinceId = 23 },
                new Geo_City { Id = 113, PersianName = "دهدشت", EnglishName = "Dehdasht", UniqueCode = 4303, ProvinceId = 23 },
                new Geo_City { Id = 114, PersianName = "لیکک", EnglishName = "Likak", UniqueCode = 4304, ProvinceId = 23 },
                new Geo_City { Id = 115, PersianName = "سی‌سخت", EnglishName = "Sisakht", UniqueCode = 4305, ProvinceId = 23 },

                // شهرهای استان گلستان
                new Geo_City { Id = 116, PersianName = "گرگان", EnglishName = "Gorgan", UniqueCode = 4401, ProvinceId = 24, IsCapital = true },
                new Geo_City { Id = 117, PersianName = "گنبد کاووس", EnglishName = "Gonbad-e Kavus", UniqueCode = 4402, ProvinceId = 24 },
                new Geo_City { Id = 118, PersianName = "علی‌آباد", EnglishName = "Aliabad", UniqueCode = 4403, ProvinceId = 24 },
                new Geo_City { Id = 119, PersianName = "آق‌قلا", EnglishName = "Aqqala", UniqueCode = 4404, ProvinceId = 24 },
                new Geo_City { Id = 120, PersianName = "مینودشت", EnglishName = "Minoodasht", UniqueCode = 4405, ProvinceId = 24 },

                // شهرهای استان گیلان
                new Geo_City { Id = 121, PersianName = "رشت", EnglishName = "Rasht", UniqueCode = 4501, ProvinceId = 25, IsCapital = true },
                new Geo_City { Id = 122, PersianName = "بندر انزلی", EnglishName = "Bandar-e Anzali", UniqueCode = 4502, ProvinceId = 25 },
                new Geo_City { Id = 123, PersianName = "لاهیجان", EnglishName = "Lahijan", UniqueCode = 4503, ProvinceId = 25 },
                new Geo_City { Id = 124, PersianName = "تالش", EnglishName = "Talesh", UniqueCode = 4504, ProvinceId = 25 },
                new Geo_City { Id = 125, PersianName = "آستارا", EnglishName = "Astara", UniqueCode = 4505, ProvinceId = 25 },

                // شهرهای استان لرستان
                new Geo_City { Id = 126, PersianName = "خرم‌آباد", EnglishName = "Khorramabad", UniqueCode = 4601, ProvinceId = 26, IsCapital = true },
                new Geo_City { Id = 127, PersianName = "بروجرد", EnglishName = "Borujerd", UniqueCode = 4602, ProvinceId = 26 },
                new Geo_City { Id = 128, PersianName = "دورود", EnglishName = "Dorud", UniqueCode = 4603, ProvinceId = 26 },
                new Geo_City { Id = 129, PersianName = "کوهدشت", EnglishName = "Kuhdasht", UniqueCode = 4604, ProvinceId = 26 },
                new Geo_City { Id = 130, PersianName = "الیگودرز", EnglishName = "Aligudarz", UniqueCode = 4605, ProvinceId = 26 },

                // شهرهای استان مازندران
                new Geo_City { Id = 131, PersianName = "ساری", EnglishName = "Sari", UniqueCode = 4701, ProvinceId = 27, IsCapital = true },
                new Geo_City { Id = 132, PersianName = "آمل", EnglishName = "Amol", UniqueCode = 4702, ProvinceId = 27 },
                new Geo_City { Id = 133, PersianName = "بابل", EnglishName = "Babol", UniqueCode = 4703, ProvinceId = 27 },
                new Geo_City { Id = 134, PersianName = "قائم‌شهر", EnglishName = "Qaem Shahr", UniqueCode = 4704, ProvinceId = 27 },
                new Geo_City { Id = 135, PersianName = "چالوس", EnglishName = "Chalus", UniqueCode = 4705, ProvinceId = 27 },

                // شهرهای استان مرکزی
                new Geo_City { Id = 136, PersianName = "اراک", EnglishName = "Arak", UniqueCode = 4801, ProvinceId = 28, IsCapital = true },
                new Geo_City { Id = 137, PersianName = "ساوه", EnglishName = "Saveh", UniqueCode = 4802, ProvinceId = 28 },
                new Geo_City { Id = 138, PersianName = "خمین", EnglishName = "Khomein", UniqueCode = 4803, ProvinceId = 28 },
                new Geo_City { Id = 139, PersianName = "دلیجان", EnglishName = "Delijan", UniqueCode = 4804, ProvinceId = 28 },
                new Geo_City { Id = 140, PersianName = "محلات", EnglishName = "Mahallat", UniqueCode = 4805, ProvinceId = 28 },

                // شهرهای استان هرمزگان
                new Geo_City { Id = 141, PersianName = "بندرعباس", EnglishName = "Bandar Abbas", UniqueCode = 4901, ProvinceId = 29, IsCapital = true },
                new Geo_City { Id = 142, PersianName = "میناب", EnglishName = "Minab", UniqueCode = 4902, ProvinceId = 29 },
                new Geo_City { Id = 143, PersianName = "قشم", EnglishName = "Qeshm", UniqueCode = 4903, ProvinceId = 29 },
                new Geo_City { Id = 144, PersianName = "بندر لنگه", EnglishName = "Bandar Lengeh", UniqueCode = 4904, ProvinceId = 29 },
                new Geo_City { Id = 145, PersianName = "رودان", EnglishName = "Roodan", UniqueCode = 4905, ProvinceId = 29 },

                // شهرهای استان همدان
                new Geo_City { Id = 146, PersianName = "همدان", EnglishName = "Hamedan", UniqueCode = 5001, ProvinceId = 30, IsCapital = true },
                new Geo_City { Id = 147, PersianName = "ملایر", EnglishName = "Malayer", UniqueCode = 5002, ProvinceId = 30 },
                new Geo_City { Id = 148, PersianName = "نهاوند", EnglishName = "Nahavand", UniqueCode = 5003, ProvinceId = 30 },
                new Geo_City { Id = 149, PersianName = "تویسرکان", EnglishName = "Tuyserkan", UniqueCode = 5004, ProvinceId = 30 },
                new Geo_City { Id = 150, PersianName = "کبودرآهنگ", EnglishName = "Kabudarahang", UniqueCode = 5005, ProvinceId = 30 },

                // شهرهای استان یزد
                new Geo_City { Id = 151, PersianName = "یزد", EnglishName = "Yazd", UniqueCode = 5101, ProvinceId = 31, IsCapital = true },
                new Geo_City { Id = 152, PersianName = "میبد", EnglishName = "Meybod", UniqueCode = 5102, ProvinceId = 31 },
                new Geo_City { Id = 153, PersianName = "اردکان", EnglishName = "Ardakan", UniqueCode = 5103, ProvinceId = 31 },
                new Geo_City { Id = 154, PersianName = "بافق", EnglishName = "Bafq", UniqueCode = 5104, ProvinceId = 31 },
                new Geo_City { Id = 155, PersianName = "مهریز", EnglishName = "Mehriz", UniqueCode = 5105, ProvinceId = 31 }
            );
        }
    }
}



