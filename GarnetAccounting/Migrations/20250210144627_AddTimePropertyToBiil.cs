using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddTimePropertyToBiil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Branch_Cu_BranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropIndex(
                name: "IX_Cu_BillOfLadings_Cu_BranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "Cu_BranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "IssuanceTime",
                table: "Cu_BillOfLadings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuanceTime",
                table: "Cu_BillOfLadings");

            migrationBuilder.AddColumn<Guid>(
                name: "Cu_BranchId",
                table: "Cu_BillOfLadings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_Cu_BranchId",
                table: "Cu_BillOfLadings",
                column: "Cu_BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Branch_Cu_BranchId",
                table: "Cu_BillOfLadings",
                column: "Cu_BranchId",
                principalTable: "Cu_Branch",
                principalColumn: "Id");
        }
    }
}
