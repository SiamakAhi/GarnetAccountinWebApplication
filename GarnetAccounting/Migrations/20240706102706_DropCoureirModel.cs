using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class DropCoureirModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cu_AddressCountries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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
                    CityId = table.Column<int>(type: "int", nullable: false),
                    DistrictNo = table.Column<byte>(type: "tinyint", nullable: true),
                    IsSuburbs = table.Column<bool>(type: "bit", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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
                    Cu_AddressCityCityId1 = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    AddressText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefaultForReceiver = table.Column<bool>(type: "bit", nullable: false),
                    IsDefaultForSender = table.Column<bool>(type: "bit", nullable: false),
                    Landline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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
                columns: new[] { "CountryId", "Abbreviation", "NameEn", "NameFa", "SellerId" },
                values: new object[] { 1, "IRIB", "Iran", "ایران", 0L });

            migrationBuilder.InsertData(
                table: "Cu_AddressStates",
                columns: new[] { "StateId", "CountryId", "NameEn", "NameFa", "SellerId" },
                values: new object[,]
                {
                    { 1, 1, "East Azerbaijan", "آذربایجان شرقی", 0L },
                    { 2, 1, "West Azerbaijan", "آذربایجان غربی", 0L },
                    { 3, 1, "Ardabil", "اردبیل", 0L },
                    { 4, 1, "Isfahan", "اصفهان", 0L },
                    { 5, 1, "Alborz", "البرز", 0L },
                    { 6, 1, "Ilam", "ایلام", 0L },
                    { 7, 1, "Bushehr", "بوشهر", 0L },
                    { 8, 1, "Tehran", "تهران", 0L },
                    { 9, 1, "Chaharmahal and Bakhtiari", "چهارمحال و بختیاری", 0L },
                    { 10, 1, "South Khorasan", "خراسان جنوبی", 0L },
                    { 11, 1, "Razavi Khorasan", "خراسان رضوی", 0L },
                    { 12, 1, "North Khorasan", "خراسان شمالی", 0L },
                    { 13, 1, "Khuzestan", "خوزستان", 0L },
                    { 14, 1, "Zanjan", "زنجان", 0L },
                    { 15, 1, "Semnan", "سمنان", 0L },
                    { 16, 1, "Sistan and Baluchestan", "سیستان و بلوچستان", 0L },
                    { 17, 1, "Fars", "فارس", 0L },
                    { 18, 1, "Qazvin", "قزوین", 0L },
                    { 19, 1, "Qom", "قم", 0L },
                    { 20, 1, "Kurdistan", "کردستان", 0L },
                    { 21, 1, "Kerman", "کرمان", 0L },
                    { 22, 1, "Kermanshah", "کرمانشاه", 0L },
                    { 23, 1, "Kohgiluyeh and Boyer-Ahmad", "کهگیلویه و بویراحمد", 0L },
                    { 24, 1, "Golestan", "گلستان", 0L },
                    { 25, 1, "Gilan", "گیلان", 0L },
                    { 26, 1, "Lorestan", "لرستان", 0L },
                    { 27, 1, "Mazandaran", "مازندران", 0L },
                    { 28, 1, "Markazi", "مرکزی", 0L },
                    { 29, 1, "Hormozgan", "هرمزگان", 0L },
                    { 30, 1, "Hamadan", "همدان", 0L },
                    { 31, 1, "Yazd", "یزد", 0L }
                });

            migrationBuilder.InsertData(
                table: "Cu_AddressCities",
                columns: new[] { "CityId", "Abbreviation", "NameEn", "NameFa", "SellerId", "StateId" },
                values: new object[,]
                {
                    { 1, "TBZ", "Tabriz", "تبریز", 0L, 1 },
                    { 2, "OMH", "Urmia", "ارومیه", 0L, 2 },
                    { 3, "ADU", "Ardabil", "اردبیل", 0L, 3 },
                    { 4, "IFN", "Isfahan", "اصفهان", 0L, 4 },
                    { 5, "THR", "Karaj", "کرج", 0L, 5 },
                    { 6, "IIL", "Ilam", "ایلام", 0L, 6 },
                    { 7, "BUZ", "Bushehr", "بوشهر", 0L, 7 },
                    { 8, "THR", "Tehran", "تهران", 0L, 8 },
                    { 9, "CHK", "Shahr-e Kord", "شهرکرد", 0L, 9 },
                    { 10, "XBJ", "Birjand", "بیرجند", 0L, 10 },
                    { 11, "MHD", "Mashhad", "مشهد", 0L, 11 },
                    { 12, "BJR", "Bojnourd", "بجنورد", 0L, 12 },
                    { 13, "AWZ", "Ahvaz", "اهواز", 0L, 13 },
                    { 14, "JWN", "Zanjan", "زنجان", 0L, 14 },
                    { 15, "SMN", "Semnan", "سمنان", 0L, 15 },
                    { 16, "ZAH", "Zahedan", "زاهدان", 0L, 16 },
                    { 17, "SYZ", "Shiraz", "شیراز", 0L, 17 },
                    { 18, "GZW", "Qazvin", "قزوین", 0L, 18 },
                    { 19, "QOM", "Qom", "قم", 0L, 19 },
                    { 20, "SDG", "Sanandaj", "سنندج", 0L, 20 },
                    { 21, "KER", "Kerman", "کرمان", 0L, 21 },
                    { 22, "KSH", "Kermanshah", "کرمانشاه", 0L, 22 },
                    { 23, "YES", "Yasuj", "یاسوج", 0L, 23 },
                    { 24, "GBT", "Gorgan", "گرگان", 0L, 24 },
                    { 25, "RAS", "Rasht", "رشت", 0L, 25 },
                    { 26, "KHD", "Khorramabad", "خرم‌آباد", 0L, 26 },
                    { 27, "SRY", "Sari", "ساری", 0L, 27 },
                    { 28, "AJK", "Arak", "اراک", 0L, 28 },
                    { 29, "BND", "Bandar Abbas", "بندرعباس", 0L, 29 },
                    { 30, "HDM", "Hamadan", "همدان", 0L, 30 },
                    { 31, "AZD", "Yazd", "یزد", 0L, 31 }
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
    }
}
