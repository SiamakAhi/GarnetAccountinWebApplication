using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddGelocationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geo_Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersianName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NumericCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geo_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Geo_Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersianName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UniqueCode = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geo_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Geo_Provinces_Geo_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Geo_Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Geo_Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersianName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UniqueCode = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    IsCapital = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geo_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Geo_Cities_Geo_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Geo_Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Geo_Countries",
                columns: new[] { "Id", "Code", "EnglishName", "NumericCode", "PersianName" },
                values: new object[,]
                {
                    { 1, "IR", "Iran", 364, "ایران" },
                    { 2, "KW", "Kuwait", 414, "کویت" },
                    { 3, "CA", "Canada", 124, "کانادا" },
                    { 4, "GB", "United Kingdom", 826, "انگلیس" },
                    { 5, "DE", "Germany", 276, "آلمان" },
                    { 6, "SE", "Sweden", 752, "سوئد" },
                    { 7, "CH", "Switzerland", 756, "سوئیس" },
                    { 8, "FR", "France", 250, "فرانسه" },
                    { 9, "AE", "United Arab Emirates", 784, "امارات" },
                    { 10, "OM", "Oman", 512, "عمان" },
                    { 11, "AU", "Australia", 36, "استرالیا" }
                });

            migrationBuilder.InsertData(
                table: "Geo_Provinces",
                columns: new[] { "Id", "CountryId", "EnglishName", "PersianName", "UniqueCode" },
                values: new object[,]
                {
                    { 1, 1, "East Azerbaijan", "آذربایجان شرقی", 1101 },
                    { 2, 1, "West Azerbaijan", "آذربایجان غربی", 1102 },
                    { 3, 1, "Ardabil", "اردبیل", 1103 },
                    { 4, 1, "Isfahan", "اصفهان", 1104 },
                    { 5, 1, "Alborz", "البرز", 1105 },
                    { 6, 1, "Ilam", "ایلام", 1106 },
                    { 7, 1, "Bushehr", "بوشهر", 1107 },
                    { 8, 1, "Tehran", "تهران", 1108 },
                    { 9, 1, "Chaharmahal and Bakhtiari", "چهارمحال و بختیاری", 1109 },
                    { 10, 1, "South Khorasan", "خراسان جنوبی", 1110 },
                    { 11, 1, "Razavi Khorasan", "خراسان رضوی", 1111 },
                    { 12, 1, "North Khorasan", "خراسان شمالی", 1112 },
                    { 13, 1, "Khuzestan", "خوزستان", 1113 },
                    { 14, 1, "Zanjan", "زنجان", 1114 },
                    { 15, 1, "Semnan", "سمنان", 1115 },
                    { 16, 1, "Sistan and Baluchestan", "سیستان و بلوچستان", 1116 },
                    { 17, 1, "Fars", "فارس", 1117 },
                    { 18, 1, "Qazvin", "قزوین", 1118 },
                    { 19, 1, "Qom", "قم", 1119 },
                    { 20, 1, "Kurdistan", "کردستان", 1120 },
                    { 21, 1, "Kerman", "کرمان", 1121 },
                    { 22, 1, "Kermanshah", "کرمانشاه", 1122 },
                    { 23, 1, "Kohgiluyeh and Boyer-Ahmad", "کهگیلویه و بویراحمد", 1123 },
                    { 24, 1, "Golestan", "گلستان", 1124 },
                    { 25, 1, "Gilan", "گیلان", 1125 },
                    { 26, 1, "Lorestan", "لرستان", 1126 },
                    { 27, 1, "Mazandaran", "مازندران", 1127 },
                    { 28, 1, "Markazi", "مرکزی", 1128 },
                    { 29, 1, "Hormozgan", "هرمزگان", 1129 },
                    { 30, 1, "Hamedan", "همدان", 1130 },
                    { 31, 1, "Yazd", "یزد", 1131 }
                });

            migrationBuilder.InsertData(
                table: "Geo_Cities",
                columns: new[] { "Id", "EnglishName", "IsCapital", "PersianName", "ProvinceId", "UniqueCode" },
                values: new object[,]
                {
                    { 1, "Tabriz", true, "تبریز", 1, 2101 },
                    { 2, "Maragheh", false, "مراغه", 1, 2102 },
                    { 3, "Mianeh", false, "میانه", 1, 2103 },
                    { 4, "Marand", false, "مرند", 1, 2104 },
                    { 5, "Azarshahr", false, "آذرشهر", 1, 2105 },
                    { 6, "Urmia", true, "ارومیه", 2, 2201 },
                    { 7, "Khoy", false, "خوی", 2, 2202 },
                    { 8, "Salmas", false, "سلماس", 2, 2203 },
                    { 9, "Mahabad", false, "مهاباد", 2, 2204 },
                    { 10, "Piranshahr", false, "پیرانشهر", 2, 2205 },
                    { 11, "Ardabil", true, "اردبیل", 3, 2301 },
                    { 12, "Meshginshahr", false, "مشگین‌شهر", 3, 2302 },
                    { 13, "Parsabad", false, "پارس‌آباد", 3, 2303 },
                    { 14, "Khalkhal", false, "خلخال", 3, 2304 },
                    { 15, "Bileh Savar", false, "بیله‌سوار", 3, 2305 },
                    { 16, "Isfahan", true, "اصفهان", 4, 2401 },
                    { 17, "Kashan", false, "کاشان", 4, 2402 },
                    { 18, "Khomeinishahr", false, "خمینی‌شهر", 4, 2403 },
                    { 19, "Falavarjan", false, "فلاورجان", 4, 2404 },
                    { 20, "Na'in", false, "نائین", 4, 2405 },
                    { 21, "Karaj", true, "کرج", 5, 2501 },
                    { 22, "Nazarabad", false, "نظرآباد", 5, 2502 },
                    { 23, "Fardis", false, "فردیس", 5, 2503 },
                    { 24, "Savojbolagh", false, "ساوجبلاغ", 5, 2504 },
                    { 25, "Eshtehard", false, "اشتهارد", 5, 2505 },
                    { 26, "Ilam", true, "ایلام", 6, 2601 },
                    { 27, "Dehloran", false, "دهلران", 6, 2602 },
                    { 28, "Ivan", false, "ایوان", 6, 2603 },
                    { 29, "Abdanan", false, "آبدانان", 6, 2604 },
                    { 30, "Mehran", false, "مهران", 6, 2605 },
                    { 31, "Bushehr", true, "بوشهر", 7, 2701 },
                    { 32, "Borazjan", false, "برازجان", 7, 2702 },
                    { 33, "Genaveh", false, "گناوه", 7, 2703 },
                    { 34, "Khormoj", false, "خورموج", 7, 2704 },
                    { 35, "Dashti", false, "دشتی", 7, 2705 },
                    { 36, "Tehran", true, "تهران", 8, 2801 },
                    { 37, "Rey", false, "ری", 8, 2802 },
                    { 38, "Eslamshahr", false, "اسلام‌شهر", 8, 2803 },
                    { 39, "Shahriar", false, "شهریار", 8, 2804 },
                    { 40, "Varamin", false, "ورامین", 8, 2805 },
                    { 41, "Shahrekord", true, "شهرکرد", 9, 2901 },
                    { 42, "Borujen", false, "بروجن", 9, 2902 },
                    { 43, "Farsan", false, "فارسان", 9, 2903 },
                    { 44, "Lordegan", false, "لردگان", 9, 2904 },
                    { 45, "Fereydunshahr", false, "فرخ‌شهر", 9, 2905 },
                    { 46, "Birjand", true, "بیرجند", 10, 3001 },
                    { 47, "Qaen", false, "قائن", 10, 3002 },
                    { 48, "Tabas", false, "طبس", 10, 3003 },
                    { 49, "Ferdows", false, "فردوس", 10, 3004 },
                    { 50, "Nehbandan", false, "نهبندان", 10, 3005 },
                    { 51, "Mashhad", true, "مشهد", 11, 3101 },
                    { 52, "Neyshabur", false, "نیشابور", 11, 3102 },
                    { 53, "Sabzevar", false, "سبزوار", 11, 3103 },
                    { 54, "Torbate Heydarieh", false, "تربت‌حیدریه", 11, 3104 },
                    { 55, "Quchan", false, "قوچان", 11, 3105 },
                    { 56, "Bojnord", true, "بجنورد", 12, 3201 },
                    { 57, "Shirvan", false, "شیروان", 12, 3202 },
                    { 58, "Esfarayen", false, "اسفراین", 12, 3203 },
                    { 59, "Ashkhaneh", false, "آشخانه", 12, 3204 },
                    { 60, "Jajarm", false, "جاجرم", 12, 3205 },
                    { 61, "Ahvaz", true, "اهواز", 13, 3301 },
                    { 62, "Abadan", false, "آبادان", 13, 3302 },
                    { 63, "Khorramshahr", false, "خرمشهر", 13, 3303 },
                    { 64, "Dezful", false, "دزفول", 13, 3304 },
                    { 65, "Shooshtar", false, "شوشتر", 13, 3305 },
                    { 66, "Zanjan", true, "زنجان", 14, 3401 },
                    { 67, "Abhar", false, "ابهر", 14, 3402 },
                    { 68, "Khoda Bandeh", false, "خدابنده", 14, 3403 },
                    { 69, "Khoramdareh", false, "خرمدره", 14, 3404 },
                    { 70, "Mahnashan", false, "ماه‌نشان", 14, 3405 },
                    { 71, "Semnan", true, "سمنان", 15, 3501 },
                    { 72, "Shahrood", false, "شاهرود", 15, 3502 },
                    { 73, "Damghan", false, "دامغان", 15, 3503 },
                    { 74, "Garmsar", false, "گرمسار", 15, 3504 },
                    { 75, "Mahdishahr", false, "مهدیشهر", 15, 3505 },
                    { 76, "Zahedan", true, "زاهدان", 16, 3601 },
                    { 77, "Chabahar", false, "چابهار", 16, 3602 },
                    { 78, "Iranshahr", false, "ایرانشهر", 16, 3603 },
                    { 79, "Zabol", false, "زابل", 16, 3604 },
                    { 80, "Khash", false, "خاش", 16, 3605 },
                    { 81, "Shiraz", true, "شیراز", 17, 3701 },
                    { 82, "Marvdasht", false, "مرودشت", 17, 3702 },
                    { 83, "Jahrom", false, "جهرم", 17, 3703 },
                    { 84, "Lar", false, "لار", 17, 3704 },
                    { 85, "Fasa", false, "فسا", 17, 3705 },
                    { 86, "Qazvin", true, "قزوین", 18, 3801 },
                    { 87, "Takestan", false, "تاکستان", 18, 3802 },
                    { 88, "Abyek", false, "آبیک", 18, 3803 },
                    { 89, "Alvand", false, "الوند", 18, 3804 },
                    { 90, "Bu’in Zahra", false, "بوئین‌زهرا", 18, 3805 },
                    { 91, "Qom", true, "قم", 19, 3901 },
                    { 92, "Kahak", false, "کهک", 19, 3902 },
                    { 93, "Salafchegan", false, "سلفچگان", 19, 3903 },
                    { 94, "Jafariyeh", false, "جعفریه", 19, 3904 },
                    { 95, "Dastjerd", false, "دستجرد", 19, 3905 },
                    { 96, "Sanandaj", true, "سنندج", 20, 4001 },
                    { 97, "Marivan", false, "مریوان", 20, 4002 },
                    { 98, "Saqez", false, "سقز", 20, 4003 },
                    { 99, "Baneh", false, "بانه", 20, 4004 },
                    { 100, "Bijar", false, "بیجار", 20, 4005 },
                    { 101, "Kerman", true, "کرمان", 21, 4101 },
                    { 102, "Sirjan", false, "سیرجان", 21, 4102 },
                    { 103, "Rafsanjan", false, "رفسنجان", 21, 4103 },
                    { 104, "Jiroft", false, "جیرفت", 21, 4104 },
                    { 105, "Zarand", false, "زرند", 21, 4105 },
                    { 106, "Kermanshah", true, "کرمانشاه", 22, 4201 },
                    { 107, "Eslamabad Gharb", false, "اسلام‌آباد غرب", 22, 4202 },
                    { 108, "Harsin", false, "هرسین", 22, 4203 },
                    { 109, "Sonqor", false, "سنقر", 22, 4204 },
                    { 110, "Sarpol-e Zahab", false, "سرپل ذهاب", 22, 4205 },
                    { 111, "Yasuj", true, "یاسوج", 23, 4301 },
                    { 112, "Gachsaran", false, "گچساران", 23, 4302 },
                    { 113, "Dehdasht", false, "دهدشت", 23, 4303 },
                    { 114, "Likak", false, "لیکک", 23, 4304 },
                    { 115, "Sisakht", false, "سی‌سخت", 23, 4305 },
                    { 116, "Gorgan", true, "گرگان", 24, 4401 },
                    { 117, "Gonbad-e Kavus", false, "گنبد کاووس", 24, 4402 },
                    { 118, "Aliabad", false, "علی‌آباد", 24, 4403 },
                    { 119, "Aqqala", false, "آق‌قلا", 24, 4404 },
                    { 120, "Minoodasht", false, "مینودشت", 24, 4405 },
                    { 121, "Rasht", true, "رشت", 25, 4501 },
                    { 122, "Bandar-e Anzali", false, "بندر انزلی", 25, 4502 },
                    { 123, "Lahijan", false, "لاهیجان", 25, 4503 },
                    { 124, "Talesh", false, "تالش", 25, 4504 },
                    { 125, "Astara", false, "آستارا", 25, 4505 },
                    { 126, "Khorramabad", true, "خرم‌آباد", 26, 4601 },
                    { 127, "Borujerd", false, "بروجرد", 26, 4602 },
                    { 128, "Dorud", false, "دورود", 26, 4603 },
                    { 129, "Kuhdasht", false, "کوهدشت", 26, 4604 },
                    { 130, "Aligudarz", false, "الیگودرز", 26, 4605 },
                    { 131, "Sari", true, "ساری", 27, 4701 },
                    { 132, "Amol", false, "آمل", 27, 4702 },
                    { 133, "Babol", false, "بابل", 27, 4703 },
                    { 134, "Qaem Shahr", false, "قائم‌شهر", 27, 4704 },
                    { 135, "Chalus", false, "چالوس", 27, 4705 },
                    { 136, "Arak", true, "اراک", 28, 4801 },
                    { 137, "Saveh", false, "ساوه", 28, 4802 },
                    { 138, "Khomein", false, "خمین", 28, 4803 },
                    { 139, "Delijan", false, "دلیجان", 28, 4804 },
                    { 140, "Mahallat", false, "محلات", 28, 4805 },
                    { 141, "Bandar Abbas", true, "بندرعباس", 29, 4901 },
                    { 142, "Minab", false, "میناب", 29, 4902 },
                    { 143, "Qeshm", false, "قشم", 29, 4903 },
                    { 144, "Bandar Lengeh", false, "بندر لنگه", 29, 4904 },
                    { 145, "Roodan", false, "رودان", 29, 4905 },
                    { 146, "Hamedan", true, "همدان", 30, 5001 },
                    { 147, "Malayer", false, "ملایر", 30, 5002 },
                    { 148, "Nahavand", false, "نهاوند", 30, 5003 },
                    { 149, "Tuyserkan", false, "تویسرکان", 30, 5004 },
                    { 150, "Kabudarahang", false, "کبودرآهنگ", 30, 5005 },
                    { 151, "Yazd", true, "یزد", 31, 5101 },
                    { 152, "Meybod", false, "میبد", 31, 5102 },
                    { 153, "Ardakan", false, "اردکان", 31, 5103 },
                    { 154, "Bafq", false, "بافق", 31, 5104 },
                    { 155, "Mehriz", false, "مهریز", 31, 5105 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Geo_Cities_ProvinceId",
                table: "Geo_Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Geo_Provinces_CountryId",
                table: "Geo_Provinces",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geo_Cities");

            migrationBuilder.DropTable(
                name: "Geo_Provinces");

            migrationBuilder.DropTable(
                name: "Geo_Countries");
        }
    }
}
