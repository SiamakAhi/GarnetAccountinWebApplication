using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Branch_DestinationBranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "CalculatedWeight",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "DeductionsTotal",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "OtherExpensesTotal",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "PayableAmount",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "ShipmentCount",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "TotalCargoFare",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "TotalVatPrice",
                table: "Cu_BillOfLadings");

            migrationBuilder.RenameColumn(
                name: "DestinationBranchId",
                table: "Cu_BillOfLadings",
                newName: "DestinationHubId");

            migrationBuilder.RenameIndex(
                name: "IX_Cu_BillOfLadings_DestinationBranchId",
                table: "Cu_BillOfLadings",
                newName: "IX_Cu_BillOfLadings_DestinationHubId");

            migrationBuilder.AddColumn<Guid>(
                name: "Cu_BranchId",
                table: "Cu_BillOfLadings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OriginHubId",
                table: "Cu_BillOfLadings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ReceiverAddress",
                table: "Cu_BillOfLadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderAddress",
                table: "Cu_BillOfLadings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_Cu_BranchId",
                table: "Cu_BillOfLadings",
                column: "Cu_BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_OriginHubId",
                table: "Cu_BillOfLadings",
                column: "OriginHubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Branch_Cu_BranchId",
                table: "Cu_BillOfLadings",
                column: "Cu_BranchId",
                principalTable: "Cu_Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Hubs_DestinationHubId",
                table: "Cu_BillOfLadings",
                column: "DestinationHubId",
                principalTable: "Cu_Hubs",
                principalColumn: "HubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Hubs_OriginHubId",
                table: "Cu_BillOfLadings",
                column: "OriginHubId",
                principalTable: "Cu_Hubs",
                principalColumn: "HubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Branch_Cu_BranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Hubs_DestinationHubId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Hubs_OriginHubId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropIndex(
                name: "IX_Cu_BillOfLadings_Cu_BranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropIndex(
                name: "IX_Cu_BillOfLadings_OriginHubId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "Cu_BranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "OriginHubId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "ReceiverAddress",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "SenderAddress",
                table: "Cu_BillOfLadings");

            migrationBuilder.RenameColumn(
                name: "DestinationHubId",
                table: "Cu_BillOfLadings",
                newName: "DestinationBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Cu_BillOfLadings_DestinationHubId",
                table: "Cu_BillOfLadings",
                newName: "IX_Cu_BillOfLadings_DestinationBranchId");

            migrationBuilder.AddColumn<double>(
                name: "CalculatedWeight",
                table: "Cu_BillOfLadings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cu_BillOfLadings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "DeductionsTotal",
                table: "Cu_BillOfLadings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OtherExpensesTotal",
                table: "Cu_BillOfLadings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PayableAmount",
                table: "Cu_BillOfLadings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentCount",
                table: "Cu_BillOfLadings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TotalCargoFare",
                table: "Cu_BillOfLadings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalVatPrice",
                table: "Cu_BillOfLadings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Branch_DestinationBranchId",
                table: "Cu_BillOfLadings",
                column: "DestinationBranchId",
                principalTable: "Cu_Branch",
                principalColumn: "Id");
        }
    }
}
