using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddCourierDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cu_BillOfLadingCostItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RateImpactTypeCode = table.Column<short>(type: "smallint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForBillOfLading = table.Column<bool>(type: "bit", nullable: false),
                    ForConsignment = table.Column<bool>(type: "bit", nullable: false),
                    IsAutoAdded = table.Column<bool>(type: "bit", nullable: false),
                    AccountMoeinId = table.Column<int>(type: "int", nullable: true),
                    AccountTafsilId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_BillOfLadingCostItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_BillOfLadingStatuses",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendNotificationToCustomer = table.Column<bool>(type: "bit", nullable: false),
                    SendNotificationToOperations = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_BillOfLadingStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_FinancialTransactionOperations",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_FinancialTransactionOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_FinancialTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionTypeId = table.Column<short>(type: "smallint", nullable: false),
                    MyProperty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_FinancialTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_FinancialTransactionTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_FinancialTransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Hubs",
                columns: table => new
                {
                    HubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    HubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    HubAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Hubs", x => x.HubId);
                    table.ForeignKey(
                        name: "FK_Cu_Hubs_Geo_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Geo_Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cu_PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_RateBaseKValues",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    KValue = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_RateBaseKValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_RateImpactTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RateImpactTypeCode = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_RateImpactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_RateWeightRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartWeight = table.Column<double>(type: "float", nullable: false),
                    EndWeight = table.Column<double>(type: "float", nullable: false),
                    WeightFactorPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_RateWeightRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_RateZones",
                columns: table => new
                {
                    ZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSatellite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_RateZones", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceName_En = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShipmentTypeCode = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Shipments",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Shipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_ConsignmentNatures",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAirTransportable = table.Column<bool>(type: "bit", nullable: false),
                    IsGroundTransportable = table.Column<bool>(type: "bit", nullable: false),
                    RateImpactTypeId = table.Column<short>(type: "smallint", nullable: false),
                    RateImpactValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_ConsignmentNatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_ConsignmentNatures_Cu_RateImpactTypes_RateImpactTypeId",
                        column: x => x.RateImpactTypeId,
                        principalTable: "Cu_RateImpactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Routes",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteName_En = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OriginCityId = table.Column<int>(type: "int", nullable: false),
                    DestinationCityId = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Routes", x => x.RouteId);
                    table.ForeignKey(
                        name: "FK_Cu_Routes_Cu_RateZones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Cu_RateZones",
                        principalColumn: "ZoneId");
                    table.ForeignKey(
                        name: "FK_Cu_Routes_Geo_Cities_DestinationCityId",
                        column: x => x.DestinationCityId,
                        principalTable: "Geo_Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_Routes_Geo_Cities_OriginCityId",
                        column: x => x.OriginCityId,
                        principalTable: "Geo_Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cu_BillOfLadings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    WaybillNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipmentCount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculatedWeight = table.Column<double>(type: "float", nullable: false),
                    TotalCargoFare = table.Column<long>(type: "bigint", nullable: false),
                    OtherExpensesTotal = table.Column<long>(type: "bigint", nullable: false),
                    DeductionsTotal = table.Column<long>(type: "bigint", nullable: false),
                    TotalVatPrice = table.Column<long>(type: "bigint", nullable: false),
                    PayableAmount = table.Column<long>(type: "bigint", nullable: false),
                    IssuanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    SettelmentType = table.Column<short>(type: "smallint", nullable: true),
                    LastStatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillOfLadingStatusId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_BillOfLadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLadings_Cu_BillOfLadingStatuses_BillOfLadingStatusId",
                        column: x => x.BillOfLadingStatusId,
                        principalTable: "Cu_BillOfLadingStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLadings_Cu_Branch_OriginBranchId",
                        column: x => x.OriginBranchId,
                        principalTable: "Cu_Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLadings_Cu_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Cu_Routes",
                        principalColumn: "RouteId");
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLadings_Cu_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Cu_Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLadings_parties_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "parties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLadings_parties_SenderId",
                        column: x => x.SenderId,
                        principalTable: "parties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cu_Consignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Volume = table.Column<float>(type: "real", nullable: false),
                    ContentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    ServiceInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiverSignature = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CargoFare = table.Column<long>(type: "bigint", nullable: false),
                    TotalCostPrice = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    VatRate = table.Column<float>(type: "real", nullable: false),
                    VatPrice = table.Column<long>(type: "bigint", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    BillOfLadingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NatureTypeId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Consignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Consignments_Cu_BillOfLadings_BillOfLadingId",
                        column: x => x.BillOfLadingId,
                        principalTable: "Cu_BillOfLadings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_Consignments_Cu_ConsignmentNatures_NatureTypeId",
                        column: x => x.NatureTypeId,
                        principalTable: "Cu_ConsignmentNatures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cu_BillCosts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    BillOfLadingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_BillCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_BillCosts_Cu_BillOfLadingCostItems_CostTypeId",
                        column: x => x.CostTypeId,
                        principalTable: "Cu_BillOfLadingCostItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_BillCosts_Cu_BillOfLadings_BillOfLadingId",
                        column: x => x.BillOfLadingId,
                        principalTable: "Cu_BillOfLadings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_BillCosts_Cu_Consignments_ConsignmentId",
                        column: x => x.ConsignmentId,
                        principalTable: "Cu_Consignments",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cu_BillOfLadingStatuses",
                columns: new[] { "Id", "Code", "Name", "SendNotificationToCustomer", "SendNotificationToOperations" },
                values: new object[,]
                {
                    { (short)1, "1", "یادداشت", true, true },
                    { (short)2, "2", "جدید", true, true },
                    { (short)3, "3", "در حال جمع آوری", true, true },
                    { (short)4, "4", "ورود به هاب مبدأ", true, true },
                    { (short)5, "5", "آماده رهسپاری", true, true },
                    { (short)6, "6", "در حال ارسال به شهر مقصد", true, true },
                    { (short)7, "7", "ورودد به هاب مقصد", true, true },
                    { (short)8, "8", "در حال توزیع", true, true },
                    { (short)9, "9", "تحویل شد", true, true },
                    { (short)10, "10", "در حال برکشت به فرستنده", true, true },
                    { (short)11, "11", "برگشت شد", true, true }
                });

            migrationBuilder.InsertData(
                table: "Cu_PaymentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "نقد" },
                    { (short)2, "واریز به حساب" },
                    { (short)3, "کارتخوان" },
                    { (short)4, "درگاه پرداخت" },
                    { (short)5, "چک" }
                });

            migrationBuilder.InsertData(
                table: "Cu_RateBaseKValues",
                columns: new[] { "Id", "KValue", "SellerId" },
                values: new object[,]
                {
                    { (short)1, 10000L, 3L },
                    { (short)2, 15000L, 120L }
                });

            migrationBuilder.InsertData(
                table: "Cu_RateImpactTypes",
                columns: new[] { "Id", "Description", "Name", "RateImpactTypeCode" },
                values: new object[,]
                {
                    { (short)1, null, "ثابت", (short)1 },
                    { (short)2, null, "درصداز کل بارنامه", (short)2 },
                    { (short)3, null, "درصد از مبلغ حمل بار", (short)3 },
                    { (short)4, null, "محاسبه توسط کاربر", (short)4 },
                    { (short)5, null, "محاسبه سیستمی", (short)4 }
                });

            migrationBuilder.InsertData(
                table: "Cu_RateWeightRanges",
                columns: new[] { "Id", "EndWeight", "StartWeight", "WeightFactorPercent" },
                values: new object[] { 1, 2.0, 0.0, 0m });

            migrationBuilder.InsertData(
                table: "Cu_RateZones",
                columns: new[] { "ZoneId", "IsSatellite", "Name", "SellerId" },
                values: new object[,]
                {
                    { 1, false, "زون 1", 3L },
                    { 2, false, "زون 2", 3L },
                    { 3, false, "زون 3", 3L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_HubId",
                table: "Cu_Branch",
                column: "HubId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillCosts_BillOfLadingId",
                table: "Cu_BillCosts",
                column: "BillOfLadingId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillCosts_ConsignmentId",
                table: "Cu_BillCosts",
                column: "ConsignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillCosts_CostTypeId",
                table: "Cu_BillCosts",
                column: "CostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_BillOfLadingStatusId",
                table: "Cu_BillOfLadings",
                column: "BillOfLadingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_OriginBranchId",
                table: "Cu_BillOfLadings",
                column: "OriginBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_ReceiverId",
                table: "Cu_BillOfLadings",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_RouteId",
                table: "Cu_BillOfLadings",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_SenderId",
                table: "Cu_BillOfLadings",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_ServiceId",
                table: "Cu_BillOfLadings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_ConsignmentNatures_RateImpactTypeId",
                table: "Cu_ConsignmentNatures",
                column: "RateImpactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Consignments_BillOfLadingId",
                table: "Cu_Consignments",
                column: "BillOfLadingId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Consignments_NatureTypeId",
                table: "Cu_Consignments",
                column: "NatureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Hubs_CityId",
                table: "Cu_Hubs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Routes_DestinationCityId",
                table: "Cu_Routes",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Routes_OriginCityId",
                table: "Cu_Routes",
                column: "OriginCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Routes_ZoneId",
                table: "Cu_Routes",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Cu_Hubs_HubId",
                table: "Cu_Branch",
                column: "HubId",
                principalTable: "Cu_Hubs",
                principalColumn: "HubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Cu_Hubs_HubId",
                table: "Cu_Branch");

            migrationBuilder.DropTable(
                name: "Cu_BillCosts");

            migrationBuilder.DropTable(
                name: "Cu_FinancialTransactionOperations");

            migrationBuilder.DropTable(
                name: "Cu_FinancialTransactions");

            migrationBuilder.DropTable(
                name: "Cu_FinancialTransactionTypes");

            migrationBuilder.DropTable(
                name: "Cu_Hubs");

            migrationBuilder.DropTable(
                name: "Cu_PaymentTypes");

            migrationBuilder.DropTable(
                name: "Cu_RateBaseKValues");

            migrationBuilder.DropTable(
                name: "Cu_RateWeightRanges");

            migrationBuilder.DropTable(
                name: "Cu_Shipments");

            migrationBuilder.DropTable(
                name: "Cu_BillOfLadingCostItems");

            migrationBuilder.DropTable(
                name: "Cu_Consignments");

            migrationBuilder.DropTable(
                name: "Cu_BillOfLadings");

            migrationBuilder.DropTable(
                name: "Cu_ConsignmentNatures");

            migrationBuilder.DropTable(
                name: "Cu_BillOfLadingStatuses");

            migrationBuilder.DropTable(
                name: "Cu_Routes");

            migrationBuilder.DropTable(
                name: "Cu_Services");

            migrationBuilder.DropTable(
                name: "Cu_RateImpactTypes");

            migrationBuilder.DropTable(
                name: "Cu_RateZones");

            migrationBuilder.DropIndex(
                name: "IX_Cu_Branch_HubId",
                table: "Cu_Branch");
        }
    }
}
