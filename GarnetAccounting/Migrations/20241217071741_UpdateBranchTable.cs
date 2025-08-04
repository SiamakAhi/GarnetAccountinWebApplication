using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBranchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "KPOldSystemSales",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_TafsilId",
                table: "Cu_Branch",
                column: "TafsilId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Acc_Coding_Tafsils_TafsilId",
                table: "Cu_Branch",
                column: "TafsilId",
                principalTable: "Acc_Coding_Tafsils",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Acc_Coding_Tafsils_TafsilId",
                table: "Cu_Branch");

            migrationBuilder.DropIndex(
                name: "IX_Cu_Branch_TafsilId",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KPOldSystemSales");
        }
    }
}
