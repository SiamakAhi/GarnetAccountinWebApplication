using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAsaSetting2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DebtorTafsil4Free",
                table: "Asa_Settings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DebtorTafsil4Warranty",
                table: "Asa_Settings",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtorTafsil4Free",
                table: "Asa_Settings");

            migrationBuilder.DropColumn(
                name: "DebtorTafsil4Warranty",
                table: "Asa_Settings");
        }
    }
}
