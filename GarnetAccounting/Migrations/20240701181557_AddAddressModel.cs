using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cu_AddressCountries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressCountries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_AddressStates",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressStates", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_Cu_AddressStates_Cu_AddressCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Cu_AddressCountries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_AddressCities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressCities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cu_AddressCities_Cu_AddressStates_StateId",
                        column: x => x.StateId,
                        principalTable: "Cu_AddressStates",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_AddressNeighborhoods",
                columns: table => new
                {
                    NeighborhoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DistrictNo = table.Column<byte>(type: "tinyint", nullable: true),
                    IsSuburbs = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressNeighborhoods", x => x.NeighborhoodId);
                    table.ForeignKey(
                        name: "FK_Cu_AddressNeighborhoods_Cu_AddressCities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cu_AddressCities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Route",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cu_AddressCityCityId = table.Column<int>(type: "int", nullable: true),
                    Cu_AddressCityCityId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Route_Cu_AddressCities_Cu_AddressCityCityId",
                        column: x => x.Cu_AddressCityCityId,
                        principalTable: "Cu_AddressCities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Cu_Route_Cu_AddressCities_Cu_AddressCityCityId1",
                        column: x => x.Cu_AddressCityCityId1,
                        principalTable: "Cu_AddressCities",
                        principalColumn: "CityId");
                });

            migrationBuilder.CreateTable(
                name: "Cu_Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Landline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefaultForSender = table.Column<bool>(type: "bit", nullable: false),
                    IsDefaultForReceiver = table.Column<bool>(type: "bit", nullable: false),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Cu_Addresses_Cu_AddressNeighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalTable: "Cu_AddressNeighborhoods",
                        principalColumn: "NeighborhoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Addresses_Cu_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Cu_Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_Addresses_parties_PersonId",
                        column: x => x.PersonId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_LogisticsFleetNeighborhood",
                columns: table => new
                {
                    MyProperty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cu_AddressNeighborhoodNeighborhoodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_LogisticsFleetNeighborhood", x => x.MyProperty);
                    table.ForeignKey(
                        name: "FK_Cu_LogisticsFleetNeighborhood_Cu_AddressNeighborhoods_Cu_AddressNeighborhoodNeighborhoodId",
                        column: x => x.Cu_AddressNeighborhoodNeighborhoodId,
                        principalTable: "Cu_AddressNeighborhoods",
                        principalColumn: "NeighborhoodId");
                });

            migrationBuilder.CreateTable(
                name: "Cu_NeighborhoodBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cu_AddressNeighborhoodNeighborhoodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_NeighborhoodBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_NeighborhoodBranch_Cu_AddressNeighborhoods_Cu_AddressNeighborhoodNeighborhoodId",
                        column: x => x.Cu_AddressNeighborhoodNeighborhoodId,
                        principalTable: "Cu_AddressNeighborhoods",
                        principalColumn: "NeighborhoodId");
                });

            migrationBuilder.InsertData(
                table: "Cu_AddressCountries",
                columns: new[] { "CountryId", "Abbreviation", "NameEn", "NameFa" },
                values: new object[] { 1, "IRIB", "Iran", "ایران" });

            migrationBuilder.InsertData(
                table: "Cu_AddressStates",
                columns: new[] { "StateId", "CountryId", "NameEn", "NameFa" },
                values: new object[,]
                {
                    { 1, 1, "East Azerbaijan", "آذربایجان شرقی" },
                    { 2, 1, "West Azerbaijan", "آذربایجان غربی" },
                    { 3, 1, "Ardabil", "اردبیل" },
                    { 4, 1, "Isfahan", "اصفهان" },
                    { 5, 1, "Alborz", "البرز" },
                    { 6, 1, "Ilam", "ایلام" },
                    { 7, 1, "Bushehr", "بوشهر" },
                    { 8, 1, "Tehran", "تهران" },
                    { 9, 1, "Chaharmahal and Bakhtiari", "چهارمحال و بختیاری" },
                    { 10, 1, "South Khorasan", "خراسان جنوبی" },
                    { 11, 1, "Razavi Khorasan", "خراسان رضوی" },
                    { 12, 1, "North Khorasan", "خراسان شمالی" },
                    { 13, 1, "Khuzestan", "خوزستان" },
                    { 14, 1, "Zanjan", "زنجان" },
                    { 15, 1, "Semnan", "سمنان" },
                    { 16, 1, "Sistan and Baluchestan", "سیستان و بلوچستان" },
                    { 17, 1, "Fars", "فارس" },
                    { 18, 1, "Qazvin", "قزوین" },
                    { 19, 1, "Qom", "قم" },
                    { 20, 1, "Kurdistan", "کردستان" },
                    { 21, 1, "Kerman", "کرمان" },
                    { 22, 1, "Kermanshah", "کرمانشاه" },
                    { 23, 1, "Kohgiluyeh and Boyer-Ahmad", "کهگیلویه و بویراحمد" },
                    { 24, 1, "Golestan", "گلستان" },
                    { 25, 1, "Gilan", "گیلان" },
                    { 26, 1, "Lorestan", "لرستان" },
                    { 27, 1, "Mazandaran", "مازندران" },
                    { 28, 1, "Markazi", "مرکزی" },
                    { 29, 1, "Hormozgan", "هرمزگان" },
                    { 30, 1, "Hamadan", "همدان" },
                    { 31, 1, "Yazd", "یزد" }
                });

            migrationBuilder.InsertData(
                table: "Cu_AddressCities",
                columns: new[] { "CityId", "Abbreviation", "NameEn", "NameFa", "StateId" },
                values: new object[,]
                {
                    { 1, "TBZ", "Tabriz", "تبریز", 1 },
                    { 2, "OMH", "Urmia", "ارومیه", 2 },
                    { 3, "ADU", "Ardabil", "اردبیل", 3 },
                    { 4, "IFN", "Isfahan", "اصفهان", 4 },
                    { 5, "THR", "Karaj", "کرج", 5 },
                    { 6, "IIL", "Ilam", "ایلام", 6 },
                    { 7, "BUZ", "Bushehr", "بوشهر", 7 },
                    { 8, "THR", "Tehran", "تهران", 8 },
                    { 9, "CHK", "Shahr-e Kord", "شهرکرد", 9 },
                    { 10, "XBJ", "Birjand", "بیرجند", 10 },
                    { 11, "MHD", "Mashhad", "مشهد", 11 },
                    { 12, "BJR", "Bojnourd", "بجنورد", 12 },
                    { 13, "AWZ", "Ahvaz", "اهواز", 13 },
                    { 14, "JWN", "Zanjan", "زنجان", 14 },
                    { 15, "SMN", "Semnan", "سمنان", 15 },
                    { 16, "ZAH", "Zahedan", "زاهدان", 16 },
                    { 17, "SYZ", "Shiraz", "شیراز", 17 },
                    { 18, "GZW", "Qazvin", "قزوین", 18 },
                    { 19, "QOM", "Qom", "قم", 19 },
                    { 20, "SDG", "Sanandaj", "سنندج", 20 },
                    { 21, "KER", "Kerman", "کرمان", 21 },
                    { 22, "KSH", "Kermanshah", "کرمانشاه", 22 },
                    { 23, "YES", "Yasuj", "یاسوج", 23 },
                    { 24, "GBT", "Gorgan", "گرگان", 24 },
                    { 25, "RAS", "Rasht", "رشت", 25 },
                    { 26, "KHD", "Khorramabad", "خرم‌آباد", 26 },
                    { 27, "SRY", "Sari", "ساری", 27 },
                    { 28, "AJK", "Arak", "اراک", 28 },
                    { 29, "BND", "Bandar Abbas", "بندرعباس", 29 },
                    { 30, "HDM", "Hamadan", "همدان", 30 },
                    { 31, "AZD", "Yazd", "یزد", 31 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cu_AddressCities_StateId",
                table: "Cu_AddressCities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Addresses_BranchId",
                table: "Cu_Addresses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Addresses_NeighborhoodId",
                table: "Cu_Addresses",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Addresses_PersonId",
                table: "Cu_Addresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_AddressNeighborhoods_CityId",
                table: "Cu_AddressNeighborhoods",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_AddressStates_CountryId",
                table: "Cu_AddressStates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_LogisticsFleetNeighborhood_Cu_AddressNeighborhoodNeighborhoodId",
                table: "Cu_LogisticsFleetNeighborhood",
                column: "Cu_AddressNeighborhoodNeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_NeighborhoodBranch_Cu_AddressNeighborhoodNeighborhoodId",
                table: "Cu_NeighborhoodBranch",
                column: "Cu_AddressNeighborhoodNeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Route_Cu_AddressCityCityId",
                table: "Cu_Route",
                column: "Cu_AddressCityCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Route_Cu_AddressCityCityId1",
                table: "Cu_Route",
                column: "Cu_AddressCityCityId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cu_Addresses");

            migrationBuilder.DropTable(
                name: "Cu_LogisticsFleetNeighborhood");

            migrationBuilder.DropTable(
                name: "Cu_NeighborhoodBranch");

            migrationBuilder.DropTable(
                name: "Cu_Route");

            migrationBuilder.DropTable(
                name: "Cu_Branch");

            migrationBuilder.DropTable(
                name: "Cu_AddressNeighborhoods");

            migrationBuilder.DropTable(
                name: "Cu_AddressCities");

            migrationBuilder.DropTable(
                name: "Cu_AddressStates");

            migrationBuilder.DropTable(
                name: "Cu_AddressCountries");
        }
    }
}
