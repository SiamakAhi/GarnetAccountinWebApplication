using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReceptpn3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "lamariElectricContractorId",
                table: "Asa_Receptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "lamariElectricShareAmount",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "lamariMechanicContractorId",
                table: "Asa_Receptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "lamariMechanicShareAmount",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Asa_Receptions_lamariElectricContractorId",
                table: "Asa_Receptions",
                column: "lamariElectricContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Asa_Receptions_lamariMechanicContractorId",
                table: "Asa_Receptions",
                column: "lamariMechanicContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asa_Receptions_Asa_Contractors_lamariElectricContractorId",
                table: "Asa_Receptions",
                column: "lamariElectricContractorId",
                principalTable: "Asa_Contractors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asa_Receptions_Asa_Contractors_lamariMechanicContractorId",
                table: "Asa_Receptions",
                column: "lamariMechanicContractorId",
                principalTable: "Asa_Contractors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asa_Receptions_Asa_Contractors_lamariElectricContractorId",
                table: "Asa_Receptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Asa_Receptions_Asa_Contractors_lamariMechanicContractorId",
                table: "Asa_Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Asa_Receptions_lamariElectricContractorId",
                table: "Asa_Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Asa_Receptions_lamariMechanicContractorId",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "lamariElectricContractorId",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "lamariElectricShareAmount",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "lamariMechanicContractorId",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "lamariMechanicShareAmount",
                table: "Asa_Receptions");
        }
    }
}
