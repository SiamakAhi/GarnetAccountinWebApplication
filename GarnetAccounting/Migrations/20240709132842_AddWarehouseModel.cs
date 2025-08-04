using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)7);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)8);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)9);

            migrationBuilder.DeleteData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)10);

            migrationBuilder.AddColumn<int>(
                name: "QuantityInSaleUnit",
                table: "ProductOrServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleUnitCountId",
                table: "ProductOrServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "parties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "Wh_Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sellerId = table.Column<long>(type: "bigint", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ishoghooghi = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EconomicNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseCount = table.Column<int>(type: "int", nullable: false),
                    InvoiceCountLimit = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    licenseExpierDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_DocumentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sellerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operator = table.Column<short>(type: "smallint", nullable: false),
                    ShowInRecipt = table.Column<bool>(type: "bit", nullable: false),
                    ShowInHavale = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_Stock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sellerId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    GoodId = table.Column<long>(type: "bigint", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false),
                    RealStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TempStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Stock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_WarehousePhysicalProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sellerId = table.Column<long>(type: "bigint", nullable: false),
                    InventoryCode = table.Column<int>(type: "int", nullable: false),
                    InventoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCentralWarehouse = table.Column<bool>(type: "bit", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_WarehousePhysicalProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wh_WarehouseDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sellerId = table.Column<long>(type: "bigint", nullable: false),
                    DocNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocTypeId = table.Column<short>(type: "smallint", nullable: false),
                    FpId = table.Column<short>(type: "smallint", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TahvilDahandeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TahvilDahandeId = table.Column<long>(type: "bigint", nullable: false),
                    TahvilGirandehName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TahvilGirandehId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SolicitorshipId = table.Column<int>(type: "int", nullable: false),
                    RgisterUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RgisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditorUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEdit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WharehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_WarehouseDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocument_Wh_WarehousePhysicalProduct_WharehouseId",
                        column: x => x.WharehouseId,
                        principalTable: "Wh_WarehousePhysicalProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocument_parties_TahvilDahandeId",
                        column: x => x.TahvilDahandeId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Wh_WarehouseDocument_parties_TahvilGirandehId",
                        column: x => x.TahvilGirandehId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Wh_Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<short>(type: "smallint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductOrServiceId = table.Column<long>(type: "bigint", nullable: false),
                    UnitCount = table.Column<int>(type: "int", nullable: false),
                    CostCenterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wh_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wh_Transaction_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Wh_Transaction_Measurements_UnitCount",
                        column: x => x.UnitCount,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Wh_Transaction_ProductOrServices_ProductOrServiceId",
                        column: x => x.ProductOrServiceId,
                        principalTable: "ProductOrServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Wh_Transaction_Wh_WarehouseDocument_WarehouseDocumentId",
                        column: x => x.WarehouseDocumentId,
                        principalTable: "Wh_WarehouseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrServices_SaleUnitCountId",
                table: "ProductOrServices",
                column: "SaleUnitCountId");

            migrationBuilder.CreateIndex(
                name: "IX_parties_Wh_CustomerId",
                table: "parties",
                column: "Wh_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Wh_CustomerId",
                table: "AspNetUsers",
                column: "Wh_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Transaction_CostCenterId",
                table: "Wh_Transaction",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Transaction_ProductOrServiceId",
                table: "Wh_Transaction",
                column: "ProductOrServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Transaction_UnitCount",
                table: "Wh_Transaction",
                column: "UnitCount");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_Transaction_WarehouseDocumentId",
                table: "Wh_Transaction",
                column: "WarehouseDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocument_TahvilDahandeId",
                table: "Wh_WarehouseDocument",
                column: "TahvilDahandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocument_TahvilGirandehId",
                table: "Wh_WarehouseDocument",
                column: "TahvilGirandehId");

            migrationBuilder.CreateIndex(
                name: "IX_Wh_WarehouseDocument_WharehouseId",
                table: "Wh_WarehouseDocument",
                column: "WharehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wh_Customer_Wh_CustomerId",
                table: "AspNetUsers",
                column: "Wh_CustomerId",
                principalTable: "Wh_Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_parties_Wh_Customer_Wh_CustomerId",
                table: "parties",
                column: "Wh_CustomerId",
                principalTable: "Wh_Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrServices_Measurements_SaleUnitCountId",
                table: "ProductOrServices",
                column: "SaleUnitCountId",
                principalTable: "Measurements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wh_Customer_Wh_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_parties_Wh_Customer_Wh_CustomerId",
                table: "parties");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrServices_Measurements_SaleUnitCountId",
                table: "ProductOrServices");

            migrationBuilder.DropTable(
                name: "Wh_Customer");

            migrationBuilder.DropTable(
                name: "Wh_DocumentType");

            migrationBuilder.DropTable(
                name: "Wh_Stock");

            migrationBuilder.DropTable(
                name: "Wh_Transaction");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "Wh_WarehouseDocument");

            migrationBuilder.DropTable(
                name: "Wh_WarehousePhysicalProduct");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrServices_SaleUnitCountId",
                table: "ProductOrServices");

            migrationBuilder.DropIndex(
                name: "IX_parties_Wh_CustomerId",
                table: "parties");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Wh_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "QuantityInSaleUnit",
                table: "ProductOrServices");

            migrationBuilder.DropColumn(
                name: "SaleUnitCountId",
                table: "ProductOrServices");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "Wh_CustomerId",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "Wh_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Acc_Coding_Groups",
                columns: new[] { "Id", "AltGroupCode", "Description", "GroupCode", "GroupName", "IsEditable", "SellerId", "TypeId" },
                values: new object[,]
                {
                    { (short)1, null, "", "1", "دارایی های غیرجاری", false, null, (short)1 },
                    { (short)2, null, "", "2", "دارایی های جاری", false, null, (short)1 },
                    { (short)3, null, "", "3", "حقوق مالکانه", false, null, (short)1 },
                    { (short)4, null, "", "4", "بدهی های غیرجاری", false, null, (short)1 },
                    { (short)5, null, "", "5", "بدهی های جاری", false, null, (short)1 },
                    { (short)6, null, "", "6", "فروش و درآمدها", false, null, (short)2 },
                    { (short)7, null, "", "7", "هزینه ها", false, null, (short)2 },
                    { (short)8, null, "", "8", "بهای تمام شده", false, null, (short)2 },
                    { (short)9, null, "", "9", "حسابهای انتظامی", false, null, (short)3 },
                    { (short)10, null, "", "0", "تراز افتتاحیه و اختتامیه", false, null, (short)3 }
                });
        }
    }
}
