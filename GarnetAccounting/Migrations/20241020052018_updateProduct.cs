using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class updateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "ProductType",
                table: "Wh_Products",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<bool>(
                name: "HasInventory",
                table: "Wh_Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsService",
                table: "Wh_Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Products_PakageCountId",
                table: "Wh_Products",
                column: "PakageCountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Products_Wh_UnitOfMeasures_PakageCountId",
                table: "Wh_Products",
                column: "PakageCountId",
                principalTable: "Wh_UnitOfMeasures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Products_Wh_UnitOfMeasures_PakageCountId",
                table: "Wh_Products");

            migrationBuilder.DropIndex(
                name: "IX_Wh_Products_PakageCountId",
                table: "Wh_Products");

            migrationBuilder.DropColumn(
                name: "HasInventory",
                table: "Wh_Products");

            migrationBuilder.DropColumn(
                name: "IsService",
                table: "Wh_Products");

            migrationBuilder.AlterColumn<short>(
                name: "ProductType",
                table: "Wh_Products",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);
        }
    }
}
