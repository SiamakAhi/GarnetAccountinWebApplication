using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountingSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClosingAccountMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTemprory",
                table: "Acc_Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KholaseSoodVaZianMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpeningccountMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoodVaZianAnbashtehMoeinId",
                table: "Acc_Settings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingAccountMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "InventoryMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "IsTemprory",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "KholaseSoodVaZianMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "OpeningccountMoeinId",
                table: "Acc_Settings");

            migrationBuilder.DropColumn(
                name: "SoodVaZianAnbashtehMoeinId",
                table: "Acc_Settings");
        }
    }
}
