using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountingSetting1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BuyIsAutoGenerate",
                table: "Acc_Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BuyMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyPartyMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SaleIsAutoGenerate",
                table: "Acc_Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WarehouseIsAutoGenerate",
                table: "Acc_Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "saleMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "salePartyMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyIsAutoGenerate",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "BuyMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "BuyPartyMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "SaleIsAutoGenerate",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "WarehouseIsAutoGenerate",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "WarehouseMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "saleMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "salePartyMoeinId",
                table: "Acc_Settings");
        }
    }
}
