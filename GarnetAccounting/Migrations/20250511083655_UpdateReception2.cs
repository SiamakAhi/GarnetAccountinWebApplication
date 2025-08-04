using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReception2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Asa_Receptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asa_Receptions_ServiceId",
                table: "Asa_Receptions",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asa_Receptions_Asa_Services_ServiceId",
                table: "Asa_Receptions",
                column: "ServiceId",
                principalTable: "Asa_Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asa_Receptions_Asa_Services_ServiceId",
                table: "Asa_Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Asa_Receptions_ServiceId",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Asa_Receptions");
        }
    }
}
