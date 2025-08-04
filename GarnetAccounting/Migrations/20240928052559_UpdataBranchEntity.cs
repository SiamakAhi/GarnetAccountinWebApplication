using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdataBranchEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Cu_Branch",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_CityId",
                table: "Cu_Branch",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Cu_AddressCities_CityId",
                table: "Cu_Branch",
                column: "CityId",
                principalTable: "Cu_AddressCities",
                principalColumn: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Cu_AddressCities_CityId",
                table: "Cu_Branch");

            migrationBuilder.DropIndex(
                name: "IX_Cu_Branch_CityId",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Cu_Branch");
        }
    }
}
