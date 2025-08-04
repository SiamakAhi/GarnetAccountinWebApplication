using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReceptionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ContractorPersentage",
                table: "Asa_Receptions",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<long>(
                name: "ContractorShareMoney",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractorPersentage",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "ContractorShareMoney",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "Asa_Receptions");
        }
    }
}
