using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddPackagingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cu_Packagings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    PackageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ForExport = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Packagings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cu_Packagings",
                columns: new[] { "Id", "ForExport", "Name", "PackageCode", "Price", "SellerId" },
                values: new object[,]
                {
                    { 1, false, "پاکت", "80", 0L, 3L },
                    { 2, false, "کارتن", "81", 0L, 3L },
                    { 3, false, "یونولیت", "82", 0L, 3L },
                    { 4, false, "کلد باکس", "83", 0L, 3L },
                    { 5, false, "جعبه چوبی", "84", 0L, 3L },
                    { 6, false, "جعبه فلزی", "85", 0L, 3L },
                    { 7, false, "پالت", "86", 0L, 3L },
                    { 8, false, "چمدان", "87", 0L, 3L },
                    { 9, false, "نایلون پیچ", "88", 0L, 3L },
                    { 10, false, "پاکت و کارتن", "89", 0L, 3L },
                    { 11, false, "پاکت و نایلون", "90", 0L, 3L },
                    { 12, false, "پاکت و یونولیت", "91", 0L, 3L },
                    { 13, false, "پاکت و کارتن و نایلون", "92", 0L, 3L },
                    { 14, false, "پاکت و نایلون و کارتن و یونولیت", "93", 0L, 3L },
                    { 15, false, "کارتن و جعبه چوبی", "94", 0L, 3L },
                    { 16, false, "کارتن و جعبه چوبی و نایلون", "95", 0L, 3L },
                    { 17, false, "جعبه چوبی و نایلون", "96", 0L, 3L },
                    { 18, false, "باکس پزشکی", "97", 0L, 3L },
                    { 19, false, "پالت و کارتن", "98", 0L, 3L },
                    { 20, false, "کارتن و نایلون پیچ", "99", 0L, 3L },
                    { 21, true, "box", "100", 0L, 3L },
                    { 22, false, "کیسه", "101", 0L, 3L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cu_Packagings");
        }
    }
}
