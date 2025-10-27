using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInventoryoldModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wh_Inventories");

            migrationBuilder.DropTable(
                name: "Wh_InventoryTransactions");

            migrationBuilder.DropTable(
                name: "Wh_WarehouseDocumentItems");

            migrationBuilder.DropTable(
                name: "Wh_WarehouseDocuments");

            migrationBuilder.DropTable(
                name: "Wh_WarehouseLocations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wh_Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseUnitId = table.Column<int>(type: "int", nullable: false),
                    PackageUnitId = table.Column<int>(type: "int", nullable: false),
                    AvailableStockInBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableStockInPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BasePerPackage = table.Column<int>(type: "int", nullable: false),
                    ConsignmentStockInBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsignmentStockInPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InSupplyStockInBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InSupplyStockInPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OurStockWithOthersInBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OurStockWithOthersInPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    RequestedStockInBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestedStockInPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservedStockInBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservedStockInPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    TotalStockInBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalStockInPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Inventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Wh_Inventories_Wh_UnitOfMeasures_BaseUnitId",
                        column: x => x.BaseUnitId,
                        principalTable: "Wh_UnitOfMeasures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wh_Inventories_Wh_UnitOfMeasures_PackageUnitId",
                        column: x => x.PackageUnitId,
                        principalTable: "Wh_UnitOfMeasures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wh_WarehouseDocuments",
                columns: table => new
                {
                    WarehouseDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationWarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    SourceWarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeleteUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentStatus = table.Column<short>(type: "smallint", nullable: false),
                    DocumentType = table.Column<short>(type: "smallint", nullable: false),
                    EditorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_WarehouseDocuments", x => x.WarehouseDocumentId);
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocuments_Wh_Warehouses_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalTable: "Wh_Warehouses",
                        principalColumn: "WarehouseId");
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocuments_Wh_Warehouses_SourceWarehouseId",
                        column: x => x.SourceWarehouseId,
                        principalTable: "Wh_Warehouses",
                        principalColumn: "WarehouseId");
                });

            migrationBuilder.CreateTable(
                name: "Wh_WarehouseLocations",
                columns: table => new
                {
                    LocationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentLocationId = table.Column<long>(type: "bigint", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LocationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_WarehouseLocations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseLocations_Wh_WarehouseLocations_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalTable: "Wh_WarehouseLocations",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseLocations_Wh_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Wh_Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wh_InventoryTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationWarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    SourceWarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false),
                    WarehouseDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseUnitQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PakageQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityInUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_InventoryTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Wh_InventoryTransactions_Wh_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Wh_Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_InventoryTransactions_Wh_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "Wh_UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_InventoryTransactions_Wh_WarehouseDocuments_WarehouseDocumentId",
                        column: x => x.WarehouseDocumentId,
                        principalTable: "Wh_WarehouseDocuments",
                        principalColumn: "WarehouseDocumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_InventoryTransactions_Wh_Warehouses_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalTable: "Wh_Warehouses",
                        principalColumn: "WarehouseId");
                    table.ForeignKey(
                        name: "FK_Wh_InventoryTransactions_Wh_Warehouses_SourceWarehouseId",
                        column: x => x.SourceWarehouseId,
                        principalTable: "Wh_Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wh_WarehouseDocumentItems",
                columns: table => new
                {
                    DocumentLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityInBaseUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityInUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_WarehouseDocumentItems", x => x.DocumentLineId);
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocumentItems_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocumentItems_Wh_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Wh_Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocumentItems_Wh_WarehouseDocuments_WarehouseDocumentId",
                        column: x => x.WarehouseDocumentId,
                        principalTable: "Wh_WarehouseDocuments",
                        principalColumn: "WarehouseDocumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocumentItems_Wh_WarehouseLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Wh_WarehouseLocations",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_BaseUnitId",
                table: "Wh_Inventories",
                column: "BaseUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_PackageUnitId",
                table: "Wh_Inventories",
                column: "PackageUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_InventoryTransactions_DestinationWarehouseId",
                table: "Wh_InventoryTransactions",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_InventoryTransactions_ProductId",
                table: "Wh_InventoryTransactions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_InventoryTransactions_SourceWarehouseId",
                table: "Wh_InventoryTransactions",
                column: "SourceWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_InventoryTransactions_UnitOfMeasureId",
                table: "Wh_InventoryTransactions",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_InventoryTransactions_WarehouseDocumentId",
                table: "Wh_InventoryTransactions",
                column: "WarehouseDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocumentItems_InvoiceItemId",
                table: "Wh_WarehouseDocumentItems",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocumentItems_LocationId",
                table: "Wh_WarehouseDocumentItems",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocumentItems_ProductId",
                table: "Wh_WarehouseDocumentItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocumentItems_WarehouseDocumentId",
                table: "Wh_WarehouseDocumentItems",
                column: "WarehouseDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocuments_DestinationWarehouseId",
                table: "Wh_WarehouseDocuments",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocuments_SourceWarehouseId",
                table: "Wh_WarehouseDocuments",
                column: "SourceWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseLocations_ParentLocationId",
                table: "Wh_WarehouseLocations",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseLocations_WarehouseId",
                table: "Wh_WarehouseLocations",
                column: "WarehouseId");
        }
    }
}
