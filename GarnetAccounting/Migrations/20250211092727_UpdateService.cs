using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeightFactorPercent",
                table: "Cu_RateWeightRanges",
                newName: "IATA_WeightFactorPercent");

            migrationBuilder.AddColumn<string>(
                name: "RatingType",
                table: "Cu_Services",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Courier_WeightFactorPercent",
                table: "Cu_RateWeightRanges",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Cu_RateWeightRanges",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Courier_WeightFactorPercent", "EndWeight", "StartWeight" },
                values: new object[] { 0m, 0.5, 0.001 });

            migrationBuilder.InsertData(
                table: "Cu_RateWeightRanges",
                columns: new[] { "Id", "Courier_WeightFactorPercent", "EndWeight", "IATA_WeightFactorPercent", "StartWeight" },
                values: new object[,]
                {
                    { 2, 0m, 1.0, 0m, 0.501 },
                    { 3, 0m, 1.5, 0m, 1.0009999999999999 },
                    { 4, 0m, 2.0, 0m, 1.5009999999999999 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cu_RateWeightRanges",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cu_RateWeightRanges",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cu_RateWeightRanges",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "RatingType",
                table: "Cu_Services");

            migrationBuilder.DropColumn(
                name: "Courier_WeightFactorPercent",
                table: "Cu_RateWeightRanges");

            migrationBuilder.RenameColumn(
                name: "IATA_WeightFactorPercent",
                table: "Cu_RateWeightRanges",
                newName: "WeightFactorPercent");

            migrationBuilder.UpdateData(
                table: "Cu_RateWeightRanges",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndWeight", "StartWeight" },
                values: new object[] { 2.0, 0.0 });
        }
    }
}
