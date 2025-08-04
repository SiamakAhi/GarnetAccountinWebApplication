using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class CourierServiceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceCode",
                table: "Cu_Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Cu_Shipments",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { (short)1, (short)1, "زمینی" },
                    { (short)2, (short)2, "هوایی" },
                    { (short)3, (short)3, "دریایی" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cu_Shipments",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Cu_Shipments",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "Cu_Shipments",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DropColumn(
                name: "ServiceCode",
                table: "Cu_Services");
        }
    }
}
