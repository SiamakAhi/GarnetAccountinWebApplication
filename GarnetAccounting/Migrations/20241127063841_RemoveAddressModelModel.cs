using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAddressModelModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Cu_Branch",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_CityId",
                table: "Cu_Branch",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Geo_Cities_CityId",
                table: "Cu_Branch",
                column: "CityId",
                principalTable: "Geo_Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Geo_Cities_CityId",
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
