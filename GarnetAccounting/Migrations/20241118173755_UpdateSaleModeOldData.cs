using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSaleModeOldData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Representatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDistributor",
                table: "Representatives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIssuer",
                table: "Representatives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AgentConfirmationDate",
                table: "KPOldSystemSales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AgentDetailedCode",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedByAgent",
                table: "KPOldSystemSales",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedByIssuingBranch",
                table: "KPOldSystemSales",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistributionCostDescription",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuingBranchConfirmationDate",
                table: "KPOldSystemSales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IssuingBranchDetailedCode",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OtherDistributionCosts",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WarehousingCost",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "IsDistributor",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "IsIssuer",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "AgentConfirmationDate",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "AgentDetailedCode",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "ConfirmedByAgent",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "ConfirmedByIssuingBranch",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "DistributionCostDescription",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "IssuingBranchConfirmationDate",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "IssuingBranchDetailedCode",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "OtherDistributionCosts",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "WarehousingCost",
                table: "KPOldSystemSales");
        }
    }
}
