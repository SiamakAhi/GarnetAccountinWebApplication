using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBranchAndCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cu_BranchUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBLIssuerDistributor",
                table: "Cu_BranchUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDispatchVehicle",
                table: "Cu_BranchUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDistCollectManager",
                table: "Cu_BranchUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternalBLIssuer",
                table: "Cu_BranchUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInternalBLIssuer",
                table: "Cu_BranchUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternalBLIssuer",
                table: "Cu_Branch",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInternalBLIssuer",
                table: "Cu_Branch",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OldBranchName",
                table: "Cu_Branch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldDistRepName",
                table: "Cu_Branch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TreCarrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeRateToRial = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreCarrencies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TreCarrencies",
                columns: new[] { "Id", "ExchangeRateToRial", "FullName", "SellerId", "ShortName" },
                values: new object[,]
                {
                    { 1, 1m, "ریال ایران", 120L, "IRR" },
                    { 2, 770000m, "دلار آمریکا", 120L, "USD" },
                    { 3, 810000m, "یورو", 120L, "EUR" },
                    { 4, 920000m, "پوند انگلیس", 120L, "GBP" },
                    { 5, 6500m, "یوآن چین", 120L, "CNY" },
                    { 6, 11400m, "درهم امارات", 120L, "AED" },
                    { 7, 6000m, "لیره ترکیه", 120L, "TRY" },
                    { 8, 35m, "دینار عراق", 120L, "IQD" },
                    { 9, 11500m, "ریال قطر", 120L, "QAR" },
                    { 10, 11200m, "ریال سعودی", 120L, "SAR" },
                    { 11, 140000m, "دینار کویت", 120L, "KWD" },
                    { 12, 380m, "ین ژاپن", 120L, "JPY" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreCarrencies");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cu_BranchUser");

            migrationBuilder.DropColumn(
                name: "IsBLIssuerDistributor",
                table: "Cu_BranchUser");

            migrationBuilder.DropColumn(
                name: "IsDispatchVehicle",
                table: "Cu_BranchUser");

            migrationBuilder.DropColumn(
                name: "IsDistCollectManager",
                table: "Cu_BranchUser");

            migrationBuilder.DropColumn(
                name: "IsExternalBLIssuer",
                table: "Cu_BranchUser");

            migrationBuilder.DropColumn(
                name: "IsInternalBLIssuer",
                table: "Cu_BranchUser");

            migrationBuilder.DropColumn(
                name: "IsExternalBLIssuer",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "IsInternalBLIssuer",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "OldBranchName",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "OldDistRepName",
                table: "Cu_Branch");
        }
    }
}
