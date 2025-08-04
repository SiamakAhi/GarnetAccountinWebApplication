using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoaidan4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceTypeCode",
                table: "Acc_ModianReports",
                newName: "ReportType");

            migrationBuilder.AddColumn<short>(
                name: "InvoiceStatusCode",
                table: "Acc_ModianReports",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "UserComment",
                table: "Acc_ModianReports",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceStatusCode",
                table: "Acc_ModianReports");

            migrationBuilder.DropColumn(
                name: "UserComment",
                table: "Acc_ModianReports");

            migrationBuilder.RenameColumn(
                name: "ReportType",
                table: "Acc_ModianReports",
                newName: "InvoiceTypeCode");
        }
    }
}
