using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWarehouse2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Inventories_Wh_Products_ProductId",
                table: "Wh_Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Inventories_Wh_Warehouses_WarehouseId",
                table: "Wh_Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Wh_Inventories_ProductId",
                table: "Wh_Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Wh_Inventories_WarehouseId",
                table: "Wh_Inventories");

            migrationBuilder.AddColumn<decimal>(
                name: "AvailableStockInBase",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvailableStockInPackage",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "BasePerPackage",
                table: "Wh_Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseUnitId",
                table: "Wh_Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ConsignmentStockInBase",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ConsignmentStockInPackage",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InSupplyStockInBase",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InSupplyStockInPackage",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OurStockWithOthersInBase",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OurStockWithOthersInPackage",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PackageUnitId",
                table: "Wh_Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RequestedStockInBase",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RequestedStockInPackage",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReservedStockInBase",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReservedStockInPackage",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalStockInBase",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalStockInPackage",
                table: "Wh_Inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_BaseUnitId",
                table: "Wh_Inventories",
                column: "BaseUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_PackageUnitId",
                table: "Wh_Inventories",
                column: "PackageUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Inventories_Wh_UnitOfMeasures_BaseUnitId",
                table: "Wh_Inventories",
                column: "BaseUnitId",
                principalTable: "Wh_UnitOfMeasures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Inventories_Wh_UnitOfMeasures_PackageUnitId",
                table: "Wh_Inventories",
                column: "PackageUnitId",
                principalTable: "Wh_UnitOfMeasures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Inventories_Wh_UnitOfMeasures_BaseUnitId",
                table: "Wh_Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Inventories_Wh_UnitOfMeasures_PackageUnitId",
                table: "Wh_Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Wh_Inventories_BaseUnitId",
                table: "Wh_Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Wh_Inventories_PackageUnitId",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "AvailableStockInBase",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "AvailableStockInPackage",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "BasePerPackage",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "BaseUnitId",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "ConsignmentStockInBase",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "ConsignmentStockInPackage",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "InSupplyStockInBase",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "InSupplyStockInPackage",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "OurStockWithOthersInBase",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "OurStockWithOthersInPackage",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "PackageUnitId",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "RequestedStockInBase",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "RequestedStockInPackage",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "ReservedStockInBase",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "ReservedStockInPackage",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "TotalStockInBase",
                table: "Wh_Inventories");

            migrationBuilder.DropColumn(
                name: "TotalStockInPackage",
                table: "Wh_Inventories");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_ProductId",
                table: "Wh_Inventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_WarehouseId",
                table: "Wh_Inventories",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Inventories_Wh_Products_ProductId",
                table: "Wh_Inventories",
                column: "ProductId",
                principalTable: "Wh_Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Inventories_Wh_Warehouses_WarehouseId",
                table: "Wh_Inventories",
                column: "WarehouseId",
                principalTable: "Wh_Warehouses",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
