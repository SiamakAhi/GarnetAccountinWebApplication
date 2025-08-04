using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountingSetting2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyDiscountMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyVatMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReturnToBuyMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReturnToSaleMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleVatMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "saleDiscountMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyDiscountMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "BuyVatMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "ReturnToBuyMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "ReturnToSaleMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "SaleVatMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "saleDiscountMoeinId",
                table: "Acc_Settings");
        }
    }
}
