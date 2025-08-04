using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoadianModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Batchnuber",
                table: "Acc_ModianReports",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CreateAt",
                table: "Acc_ModianReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "TafsilId",
                table: "Acc_ModianReports",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Batchnuber",
                table: "Acc_ModianReports");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Acc_ModianReports");

            migrationBuilder.DropColumn(
                name: "TafsilId",
                table: "Acc_ModianReports");
        }
    }
}
