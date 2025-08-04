using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReception3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankMoeinId",
                table: "Asa_Settings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BankTafsilId",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankMoeinId",
                table: "Asa_Settings");

            migrationBuilder.DropColumn(
                name: "BankTafsilId",
                table: "Asa_Receptions");
        }
    }
}
