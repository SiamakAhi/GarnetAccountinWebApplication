using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class updateCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LamariServiceId",
                table: "Asa_Receptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asa_Receptions_LamariServiceId",
                table: "Asa_Receptions",
                column: "LamariServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asa_Receptions_Asa_LamariServices_LamariServiceId",
                table: "Asa_Receptions",
                column: "LamariServiceId",
                principalTable: "Asa_LamariServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asa_Receptions_Asa_LamariServices_LamariServiceId",
                table: "Asa_Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Asa_Receptions_LamariServiceId",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "LamariServiceId",
                table: "Asa_Receptions");
        }
    }
}
