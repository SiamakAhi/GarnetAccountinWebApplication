using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectModel_Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Con_Projects_parties_EmployerId",
                table: "Con_Projects");

            migrationBuilder.DropIndex(
                name: "IX_Con_Projects_EmployerId",
                table: "Con_Projects");

            migrationBuilder.RenameColumn(
                name: "EmployerId",
                table: "Con_Projects",
                newName: "TafsilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TafsilId",
                table: "Con_Projects",
                newName: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Con_Projects_EmployerId",
                table: "Con_Projects",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Con_Projects_parties_EmployerId",
                table: "Con_Projects",
                column: "EmployerId",
                principalTable: "parties",
                principalColumn: "Id");
        }
    }
}
