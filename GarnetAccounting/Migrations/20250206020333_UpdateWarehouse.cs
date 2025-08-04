using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Inventories_Wh_WarehouseLocations_LocationId",
                table: "Wh_Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Wh_Inventories_LocationId",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "MaximumQuantity",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "MinimumQuantity",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "QuantityOnHand",
                table: "Wh_Inventories");

            migrationBuilder.AddColumn<int>(
                name: "MoeinId",
                table: "Wh_Warehouses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TafsilId",
                table: "Wh_Warehouses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "WarehouseType",
                table: "Wh_Warehouses",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumQuantity",
                table: "Wh_Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumQuantity",
                table: "Wh_Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MoeinId",
                table: "Wh_ProductCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TafsilId",
                table: "Wh_ProductCategories",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoeinId",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "TafsilId",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "WarehouseType",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "MaximumQuantity",
                table: "Wh_Products");

            migrationBuilder.DropColumn(
                name: "MinimumQuantity",
                table: "Wh_Products");

            migrationBuilder.DropColumn(
                name: "MoeinId",
                table: "Wh_ProductCategories");

            migrationBuilder.DropColumn(
                name: "TafsilId",
                table: "Wh_ProductCategories");

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "Wh_Inventories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumQuantity",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumQuantity",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityOnHand",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_LocationId",
                table: "Wh_Inventories",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Inventories_Wh_WarehouseLocations_LocationId",
                table: "Wh_Inventories",
                column: "LocationId",
                principalTable: "Wh_WarehouseLocations",
                principalColumn: "LocationId");
        }
    }
}
