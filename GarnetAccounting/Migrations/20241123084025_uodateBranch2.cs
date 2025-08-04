using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class uodateBranch2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Representatives_RepresentativeId",
                table: "Cu_Branch");

            migrationBuilder.DropIndex(
                name: "IX_Cu_Branch_RepresentativeId",
                table: "Cu_Branch");

            migrationBuilder.AddColumn<Guid>(
                name: "Cu_RepresentativeId",
                table: "Cu_Branch",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_Cu_RepresentativeId",
                table: "Cu_Branch",
                column: "Cu_RepresentativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Representatives_Cu_RepresentativeId",
                table: "Cu_Branch",
                column: "Cu_RepresentativeId",
                principalTable: "Representatives",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Representatives_Cu_RepresentativeId",
                table: "Cu_Branch");

            migrationBuilder.DropIndex(
                name: "IX_Cu_Branch_Cu_RepresentativeId",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "Cu_RepresentativeId",
                table: "Cu_Branch");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_RepresentativeId",
                table: "Cu_Branch",
                column: "RepresentativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Representatives_RepresentativeId",
                table: "Cu_Branch",
                column: "RepresentativeId",
                principalTable: "Representatives",
                principalColumn: "Id");
        }
    }
}
