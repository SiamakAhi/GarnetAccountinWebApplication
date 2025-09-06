using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountingSetting140406091 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sz_Buy",
                table: "Acc_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "sz_CalcSystemicPayanDoreh",
                table: "Acc_Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "sz_Costs",
                table: "Acc_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sz_Incomm",
                table: "Acc_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sz_Inventory",
                table: "Acc_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sz_ReturnToBuy",
                table: "Acc_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sz_ReturnToSale",
                table: "Acc_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sz_Sale",
                table: "Acc_Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "sz_ShowCostByKol",
                table: "Acc_Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sz_Buy",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "sz_CalcSystemicPayanDoreh",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "sz_Costs",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "sz_Incomm",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "sz_Inventory",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "sz_ReturnToBuy",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "sz_ReturnToSale",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "sz_Sale",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "sz_ShowCostByKol",
                table: "Acc_Settings");
        }
    }
}
