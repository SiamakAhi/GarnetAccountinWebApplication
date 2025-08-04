using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoadian2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "InvoiceSubjectCode",
                table: "Acc_ModianReports",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "InvoiceTypeCode",
                table: "Acc_ModianReports",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceSubjectCode",
                table: "Acc_ModianReports");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeCode",
                table: "Acc_ModianReports");
        }
    }
}
