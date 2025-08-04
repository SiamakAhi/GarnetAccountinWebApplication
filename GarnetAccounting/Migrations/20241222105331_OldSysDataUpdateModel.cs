using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class OldSysDataUpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DistributerBranchId",
                table: "KPOldSystemSales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistributerId",
                table: "KPOldSystemSales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KPOldSystemSales_DistributerBranchId",
                table: "KPOldSystemSales",
                column: "DistributerBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_KPOldSystemSales_Cu_BranchUser_DistributerBranchId",
                table: "KPOldSystemSales",
                column: "DistributerBranchId",
                principalTable: "Cu_BranchUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPOldSystemSales_Cu_BranchUser_DistributerBranchId",
                table: "KPOldSystemSales");

            migrationBuilder.DropIndex(
                name: "IX_KPOldSystemSales_DistributerBranchId",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "DistributerBranchId",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "DistributerId",
                table: "KPOldSystemSales");
        }
    }
}
