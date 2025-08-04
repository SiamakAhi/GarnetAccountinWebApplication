using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wh_Customers_Wh_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_parties_Wh_Customers_Wh_CustomerId",
                table: "parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_TransactionHeader_Wh_Warehouses_WharehouseId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_TransactionHeader_parties_TahvilDahandeId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_TransactionHeader_parties_TahvilGirandehId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "productOrServiceTypes");

            migrationBuilder.DropTable(
                name: "Wh_Customers");

            migrationBuilder.DropTable(
                name: "Wh_DocumentTypes");

            migrationBuilder.DropTable(
                name: "Wh_Stocks");

            migrationBuilder.DropTable(
                name: "Wh_Transactions");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "ProductOrServices");

            migrationBuilder.DropTable(
                name: "InvoiceTypes");

            migrationBuilder.DropTable(
                name: "SettlementTypes");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "StuffCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Warehouses",
                table: "Wh_Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_parties_Wh_CustomerId",
                table: "parties");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Wh_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_TransactionHeader",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropIndex(
                name: "IX_Wh_TransactionHeader_TahvilDahandeId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropIndex(
                name: "IX_Wh_TransactionHeader_TahvilGirandehId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropIndex(
                name: "IX_Wh_TransactionHeader_WharehouseId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "InventoryCode",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "InventoryName",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "SubId",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "Wh_CustomerId",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "Wh_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropColumn(
                name: "DocNumber",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropColumn(
                name: "EditorUser",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropColumn(
                name: "RgisterUser",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropColumn(
                name: "SolicitorshipId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropColumn(
                name: "TahvilDahandeId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropColumn(
                name: "TahvilGirandehId",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropColumn(
                name: "WharehouseId",
                table: "Wh_TransactionHeader");

            migrationBuilder.RenameTable(
                name: "Wh_TransactionHeader",
                newName: "Wh_WarehouseDocuments");

            migrationBuilder.RenameColumn(
                name: "sellerId",
                table: "Wh_Warehouses",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "IsCentralWarehouse",
                table: "Wh_Warehouses",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "sellerId",
                table: "Wh_WarehouseDocuments",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "TahvilGirandehName",
                table: "Wh_WarehouseDocuments",
                newName: "DocumentNumber");

            migrationBuilder.RenameColumn(
                name: "TahvilDahandeName",
                table: "Wh_WarehouseDocuments",
                newName: "CreatorName");

            migrationBuilder.RenameColumn(
                name: "RgisterDate",
                table: "Wh_WarehouseDocuments",
                newName: "DocumentDate");

            migrationBuilder.RenameColumn(
                name: "LastEdit",
                table: "Wh_WarehouseDocuments",
                newName: "LastUpdate");

            migrationBuilder.RenameColumn(
                name: "FpId",
                table: "Wh_WarehouseDocuments",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "DocTypeId",
                table: "Wh_WarehouseDocuments",
                newName: "DocumentStatus");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Wh_WarehouseDocuments",
                newName: "CreatDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Wh_WarehouseDocuments",
                newName: "WarehouseDocumentId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Wh_Warehouses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "Wh_Warehouses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Wh_Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseCode",
                table: "Wh_Warehouses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "Wh_Warehouses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteUserName",
                table: "Wh_WarehouseDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Wh_WarehouseDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DestinationWarehouseId",
                table: "Wh_WarehouseDocuments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditorName",
                table: "Wh_WarehouseDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Wh_WarehouseDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Wh_WarehouseDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SourceWarehouseId",
                table: "Wh_WarehouseDocuments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Warehouses",
                table: "Wh_Warehouses",
                column: "WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_WarehouseDocuments",
                table: "Wh_WarehouseDocuments",
                column: "WarehouseDocumentId");

            migrationBuilder.CreateTable(
                name: "com_Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceType = table.Column<short>(type: "smallint", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartyId = table.Column<long>(type: "bigint", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_ProductCategories",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_ProductCategories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Wh_ProductCategories_Wh_ProductCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Wh_ProductCategories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Wh_UnitOfMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_UnitOfMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_WarehouseLocations",
                columns: table => new
                {
                    LocationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    LocationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentLocationId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                name: "Wh_Products",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    BaseUnitId = table.Column<int>(type: "int", nullable: false),
                    PakageCountId = table.Column<int>(type: "int", nullable: true),
                    QuantityInPakage = table.Column<int>(type: "int", nullable: false),
                    ProductType = table.Column<short>(type: "smallint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VATRate = table.Column<float>(type: "real", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ForeignCurrencyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LocalCurrencyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    NetWeight = table.Column<float>(type: "real", nullable: true),
                    OtherLegalChargesSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherLegalChargesRate = table.Column<float>(type: "real", nullable: true),
                    OtherLegalChargesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OtherTaxesSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherTaxesRate = table.Column<float>(type: "real", nullable: true),
                    OtherTaxesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Wh_Products_Wh_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Wh_ProductCategories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Wh_Products_Wh_UnitOfMeasures_BaseUnitId",
                        column: x => x.BaseUnitId,
                        principalTable: "Wh_UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "com_InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false),
                    QuantityInUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityInBaseUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_com_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_com_InvoiceItem_Wh_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Wh_Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_com_InvoiceItem_com_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "com_Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wh_Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaximumQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Inventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Wh_Inventories_Wh_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Wh_Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_Inventories_Wh_WarehouseLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Wh_WarehouseLocations",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK_Wh_Inventories_Wh_Warehouses_WarehouseId",
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
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionType = table.Column<short>(type: "smallint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    SourceWarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    DestinationWarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false),
                    QuantityInUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PakageQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseUnitQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Wh_ProductUnits",
                columns: table => new
                {
                    ProductUnitId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false),
                    UnitType = table.Column<short>(type: "smallint", nullable: false),
                    ConversionFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_ProductUnits", x => x.ProductUnitId);
                    table.ForeignKey(
                        name: "FK_Wh_ProductUnits_Wh_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Wh_Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_ProductUnits_Wh_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "Wh_UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Wh_WarehouseDocumentItems",
                columns: table => new
                {
                    DocumentLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false),
                    QuantityInUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityInBaseUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_WarehouseDocumentItems", x => x.DocumentLineId);
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
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocumentItems_com_InvoiceItem_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "com_InvoiceItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocuments_DestinationWarehouseId",
                table: "Wh_WarehouseDocuments",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocuments_SourceWarehouseId",
                table: "Wh_WarehouseDocuments",
                column: "SourceWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_com_InvoiceItem_InvoiceId",
                table: "com_InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_com_InvoiceItem_ProductId",
                table: "com_InvoiceItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_LocationId",
                table: "Wh_Inventories",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_ProductId",
                table: "Wh_Inventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Inventories_WarehouseId",
                table: "Wh_Inventories",
                column: "WarehouseId");

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
                name: "IX_Wh_ProductCategories_ParentCategoryId",
                table: "Wh_ProductCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Products_BaseUnitId",
                table: "Wh_Products",
                column: "BaseUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Products_CategoryId",
                table: "Wh_Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_ProductUnits_ProductId",
                table: "Wh_ProductUnits",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_ProductUnits_UnitOfMeasureId",
                table: "Wh_ProductUnits",
                column: "UnitOfMeasureId");

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
                name: "IX_Wh_WarehouseLocations_ParentLocationId",
                table: "Wh_WarehouseLocations",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseLocations_WarehouseId",
                table: "Wh_WarehouseLocations",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_WarehouseDocuments_Wh_Warehouses_DestinationWarehouseId",
                table: "Wh_WarehouseDocuments",
                column: "DestinationWarehouseId",
                principalTable: "Wh_Warehouses",
                principalColumn: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_WarehouseDocuments_Wh_Warehouses_SourceWarehouseId",
                table: "Wh_WarehouseDocuments",
                column: "SourceWarehouseId",
                principalTable: "Wh_Warehouses",
                principalColumn: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wh_WarehouseDocuments_Wh_Warehouses_DestinationWarehouseId",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_WarehouseDocuments_Wh_Warehouses_SourceWarehouseId",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropTable(
                name: "Wh_Inventories");

            migrationBuilder.DropTable(
                name: "Wh_InventoryTransactions");

            migrationBuilder.DropTable(
                name: "Wh_ProductUnits");

            migrationBuilder.DropTable(
                name: "Wh_WarehouseDocumentItems");

            migrationBuilder.DropTable(
                name: "Wh_WarehouseLocations");

            migrationBuilder.DropTable(
                name: "com_InvoiceItem");

            migrationBuilder.DropTable(
                name: "Wh_Products");

            migrationBuilder.DropTable(
                name: "com_Invoice");

            migrationBuilder.DropTable(
                name: "Wh_ProductCategories");

            migrationBuilder.DropTable(
                name: "Wh_UnitOfMeasures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Warehouses",
                table: "Wh_Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_WarehouseDocuments",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Wh_WarehouseDocuments_DestinationWarehouseId",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Wh_WarehouseDocuments_SourceWarehouseId",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "WarehouseCode",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "Wh_Warehouses");

            migrationBuilder.DropColumn(
                name: "DeleteUserName",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropColumn(
                name: "DestinationWarehouseId",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropColumn(
                name: "EditorName",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.DropColumn(
                name: "SourceWarehouseId",
                table: "Wh_WarehouseDocuments");

            migrationBuilder.RenameTable(
                name: "Wh_WarehouseDocuments",
                newName: "Wh_TransactionHeader");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Wh_Warehouses",
                newName: "sellerId");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Wh_Warehouses",
                newName: "IsCentralWarehouse");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Wh_TransactionHeader",
                newName: "sellerId");

            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "Wh_TransactionHeader",
                newName: "LastEdit");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "Wh_TransactionHeader",
                newName: "FpId");

            migrationBuilder.RenameColumn(
                name: "DocumentStatus",
                table: "Wh_TransactionHeader",
                newName: "DocTypeId");

            migrationBuilder.RenameColumn(
                name: "DocumentNumber",
                table: "Wh_TransactionHeader",
                newName: "TahvilGirandehName");

            migrationBuilder.RenameColumn(
                name: "DocumentDate",
                table: "Wh_TransactionHeader",
                newName: "RgisterDate");

            migrationBuilder.RenameColumn(
                name: "CreatorName",
                table: "Wh_TransactionHeader",
                newName: "TahvilDahandeName");

            migrationBuilder.RenameColumn(
                name: "CreatDate",
                table: "Wh_TransactionHeader",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "WarehouseDocumentId",
                table: "Wh_TransactionHeader",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Wh_Warehouses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wh_Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "InventoryCode",
                table: "Wh_Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "InventoryName",
                table: "Wh_Warehouses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SubId",
                table: "Wh_Warehouses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Wh_CustomerId",
                table: "parties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Wh_CustomerId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Wh_TransactionHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocNumber",
                table: "Wh_TransactionHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EditorUser",
                table: "Wh_TransactionHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RgisterUser",
                table: "Wh_TransactionHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SolicitorshipId",
                table: "Wh_TransactionHeader",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TahvilDahandeId",
                table: "Wh_TransactionHeader",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TahvilGirandehId",
                table: "Wh_TransactionHeader",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WharehouseId",
                table: "Wh_TransactionHeader",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Warehouses",
                table: "Wh_Warehouses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_TransactionHeader",
                table: "Wh_TransactionHeader",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productOrServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productOrServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettlementTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StuffCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    TafsilCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true),
                    customerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuffCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    EconomicNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceCountLimit = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Ishoghooghi = table.Column<bool>(type: "bit", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LicenseCount = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    licenseExpierDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operator = table.Column<short>(type: "smallint", nullable: false),
                    ShowInHavale = table.Column<bool>(type: "bit", nullable: false),
                    ShowInRecipt = table.Column<bool>(type: "bit", nullable: false),
                    sellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoodId = table.Column<long>(type: "bigint", nullable: false),
                    RealStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TempStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    sellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    SettlementTypeId = table.Column<int>(type: "int", nullable: false),
                    AccountingSystemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Billid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cdcd = table.Column<int>(type: "int", nullable: true),
                    Cdcn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractRegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallmentPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoicePattern = table.Column<int>(type: "int", nullable: true),
                    InvoiceSellerCount = table.Column<int>(type: "int", nullable: true),
                    InvoiceSubject = table.Column<int>(type: "int", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tax17 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tocv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tonw = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Torv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalBill = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalOtherDutyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPreDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalVatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalVatOfPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    buyerBranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cashPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    contractRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    invoiceReferenceTaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sellerBranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sellerCustomsCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sellerCustomsLicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalTable: "InvoiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_SettlementTypes_SettlementTypeId",
                        column: x => x.SettlementTypeId,
                        principalTable: "SettlementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_parties_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "parties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_parties_SellerId",
                        column: x => x.SellerId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrServices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SaleUnitCountId = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasurementId = table.Column<int>(type: "int", nullable: false),
                    AccountingSystemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ForeignCurrencyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsService = table.Column<bool>(type: "bit", nullable: false),
                    LocalCurrencyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetWeight = table.Column<float>(type: "real", nullable: true),
                    OtherLegalChargesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OtherLegalChargesRate = table.Column<float>(type: "real", nullable: true),
                    OtherLegalChargesSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherTaxesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OtherTaxesRate = table.Column<float>(type: "real", nullable: true),
                    OtherTaxesSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityInSaleUnit = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATRate = table.Column<float>(type: "real", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrServices_Measurements_SaleUnitCountId",
                        column: x => x.SaleUnitCountId,
                        principalTable: "Measurements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductOrServices_Measurements_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrServices_StuffCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "StuffCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyTypeId = table.Column<int>(type: "int", nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    ProductOrServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bros_TalaHagh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Consfee_TalaOjrat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nw = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Olr = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductOrServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowNumber = table.Column<int>(type: "int", nullable: true),
                    Spro_TalaSood = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sscv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ssrv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tcpbs_TalaTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    brokerSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    buyerSRegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cashOfPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    constructionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    currencyFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    otherLegalRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    otherLegalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    overDutyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    overDutyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    overDutyTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pspd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    sellerProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    totalConstructionProfitBrokerSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    vatOfPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    vatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Currencies_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_ProductOrServices_ProductOrServiceId",
                        column: x => x.ProductOrServiceId,
                        principalTable: "ProductOrServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wh_Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostCenterId = table.Column<int>(type: "int", nullable: true),
                    ProductOrServiceId = table.Column<long>(type: "bigint", nullable: false),
                    UnitCount = table.Column<int>(type: "int", nullable: false),
                    WarehouseDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wh_Transactions_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wh_Transactions_Measurements_UnitCount",
                        column: x => x.UnitCount,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_Transactions_ProductOrServices_ProductOrServiceId",
                        column: x => x.ProductOrServiceId,
                        principalTable: "ProductOrServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wh_Transactions_Wh_TransactionHeader_WarehouseDocumentId",
                        column: x => x.WarehouseDocumentId,
                        principalTable: "Wh_TransactionHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "InvoiceTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 1, "فروش" },
                    { 2, 2, "اصلاحی" },
                    { 3, 3, "ابطال" },
                    { 4, 4, "برگشت از فروش" },
                    { 5, 5, "خرید" },
                    { 6, 6, "برگشت از خرید" }
                });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "1627", "عدد" },
                    { 2, "164", "کیلوگرم" },
                    { 3, "165", "متر" },
                    { 4, "1631", "دستگاه" },
                    { 5, "1637", "لیتر" },
                    { 6, "1628", "بسته" },
                    { 7, "1622", "گرم" },
                    { 8, "1676", "نفر" },
                    { 9, "16103", "ساعت" },
                    { 10, "169", "تن" },
                    { 11, "1614", "گالن" },
                    { 12, "1624", "کارتن" },
                    { 13, "1620", "دست" },
                    { 14, "161", "برگ" },
                    { 15, "1629", "پاکت" },
                    { 16, "1645", "مترمربع" },
                    { 17, "1643", "جفت" },
                    { 18, "1653", "واحد" },
                    { 19, "1640", "تخته" },
                    { 20, "1660", "شانه" },
                    { 21, "1654", "بندیل" },
                    { 22, "1687", "فاقد بسته بندی" },
                    { 23, "1638", "بطری" },
                    { 24, "1615", "کیسه" },
                    { 25, "1633", "سیلندر" },
                    { 26, "16121", "نفر-ساعت" },
                    { 27, "168", "حلب" },
                    { 28, "1665", "شیت" },
                    { 29, "1636", "جام" },
                    { 30, "16113", "سال" },
                    { 31, "16112", "ماه" },
                    { 32, "16104", "روز" },
                    { 33, "16111", "دقیقه" },
                    { 34, "16115", "سانتی متر" },
                    { 35, "16114", "قطعه" },
                    { 36, "16119", "گیگابایت بر ثانیه" },
                    { 37, "90", "نسخه (جلد)" },
                    { 38, "1611", "لنگه" },
                    { 39, "1612", "عدل" },
                    { 40, "1613", "جعبه" },
                    { 41, "1618", "توپ" },
                    { 42, "1619", "ست" },
                    { 43, "1641", "رول" }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 1, "نقدی" },
                    { 2, 2, "واریز به حساب" },
                    { 3, 3, "چک" },
                    { 4, 4, "چک واگذاری" }
                });

            migrationBuilder.InsertData(
                table: "SettlementTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 1, "نقد" },
                    { 2, 2, "نسیه" },
                    { 3, 3, "نقد/نسیه" }
                });

            migrationBuilder.InsertData(
                table: "productOrServiceTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "1", "عمومی" },
                    { 2, "2", "قرارداد پیمانکاری" },
                    { 3, "3", "طلا، جواهر و پلاتین" },
                    { 4, "4", "قبوض خدماتی" },
                    { 5, "5", "بلیط هواپیما" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_parties_Wh_CustomerId",
                table: "parties",
                column: "Wh_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Wh_CustomerId",
                table: "AspNetUsers",
                column: "Wh_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_TransactionHeader_TahvilDahandeId",
                table: "Wh_TransactionHeader",
                column: "TahvilDahandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_TransactionHeader_TahvilGirandehId",
                table: "Wh_TransactionHeader",
                column: "TahvilGirandehId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_TransactionHeader_WharehouseId",
                table: "Wh_TransactionHeader",
                column: "WharehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_CurrencyTypeId",
                table: "InvoiceItems",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductOrServiceId",
                table: "InvoiceItems",
                column: "ProductOrServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BuyerId",
                table: "Invoices",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceTypeId",
                table: "Invoices",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SellerId",
                table: "Invoices",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SettlementTypeId",
                table: "Invoices",
                column: "SettlementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrServices_CategoryId",
                table: "ProductOrServices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrServices_SaleUnitCountId",
                table: "ProductOrServices",
                column: "SaleUnitCountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrServices_UnitOfMeasurementId",
                table: "ProductOrServices",
                column: "UnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Transactions_CostCenterId",
                table: "Wh_Transactions",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Transactions_ProductOrServiceId",
                table: "Wh_Transactions",
                column: "ProductOrServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Transactions_UnitCount",
                table: "Wh_Transactions",
                column: "UnitCount");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Transactions_WarehouseDocumentId",
                table: "Wh_Transactions",
                column: "WarehouseDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wh_Customers_Wh_CustomerId",
                table: "AspNetUsers",
                column: "Wh_CustomerId",
                principalTable: "Wh_Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_parties_Wh_Customers_Wh_CustomerId",
                table: "parties",
                column: "Wh_CustomerId",
                principalTable: "Wh_Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_TransactionHeader_Wh_Warehouses_WharehouseId",
                table: "Wh_TransactionHeader",
                column: "WharehouseId",
                principalTable: "Wh_Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_TransactionHeader_parties_TahvilDahandeId",
                table: "Wh_TransactionHeader",
                column: "TahvilDahandeId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_TransactionHeader_parties_TahvilGirandehId",
                table: "Wh_TransactionHeader",
                column: "TahvilGirandehId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
