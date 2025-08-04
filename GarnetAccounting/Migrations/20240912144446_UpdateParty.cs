using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateParty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CEOContactNumber",
                table: "parties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CEOName",
                table: "parties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxAuditor",
                table: "parties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxFileNumber",
                table: "parties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxPanelPassword",
                table: "parties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxTrackingNumber",
                table: "parties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxUnitAddress",
                table: "parties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxUnitCode",
                table: "parties",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEOContactNumber",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "CEOName",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "TaxAuditor",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "TaxFileNumber",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "TaxPanelPassword",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "TaxTrackingNumber",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "TaxUnitAddress",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "TaxUnitCode",
                table: "parties");
        }
    }
}
