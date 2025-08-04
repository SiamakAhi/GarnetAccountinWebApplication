using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class updateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wh_Customer_Wh_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Cu_BranchType_BranchTypeId1",
                table: "Cu_Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Cu_Hub_BranchHubHubId",
                table: "Cu_Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_parties_Wh_Customer_Wh_CustomerId",
                table: "parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transaction_CostCenter_CostCenterId",
                table: "Wh_Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transaction_Measurements_UnitCount",
                table: "Wh_Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transaction_ProductOrServices_ProductOrServiceId",
                table: "Wh_Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transaction_Wh_WarehouseDocument_WarehouseDocumentId",
                table: "Wh_Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_WarehouseDocument_Wh_WarehousePhysicalProduct_WharehouseId",
                table: "Wh_WarehouseDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_WarehouseDocument_parties_TahvilDahandeId",
                table: "Wh_WarehouseDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_WarehouseDocument_parties_TahvilGirandehId",
                table: "Wh_WarehouseDocument");

            migrationBuilder.DropTable(
                name: "Cu_LogisticsFleet");

            migrationBuilder.DropTable(
                name: "Cu_LogisticsFleetNeighborhood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_WarehousePhysicalProduct",
                table: "Wh_WarehousePhysicalProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_WarehouseDocument",
                table: "Wh_WarehouseDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Transaction",
                table: "Wh_Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Stock",
                table: "Wh_Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_DocumentType",
                table: "Wh_DocumentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Customer",
                table: "Wh_Customer");

            migrationBuilder.DropColumn(
                name: "CommissionPercentage",
                table: "Cu_Partner");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Cu_Partner");

            migrationBuilder.RenameTable(
                name: "Wh_WarehousePhysicalProduct",
                newName: "Wh_Warehouses");

            migrationBuilder.RenameTable(
                name: "Wh_WarehouseDocument",
                newName: "Wh_TransactionHeader");

            migrationBuilder.RenameTable(
                name: "Wh_Transaction",
                newName: "Wh_Transactions");

            migrationBuilder.RenameTable(
                name: "Wh_Stock",
                newName: "Wh_Stocks");

            migrationBuilder.RenameTable(
                name: "Wh_DocumentType",
                newName: "Wh_DocumentTypes");

            migrationBuilder.RenameTable(
                name: "Wh_Customer",
                newName: "Wh_Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_WarehouseDocument_WharehouseId",
                table: "Wh_TransactionHeader",
                newName: "IX_Wh_TransactionHeader_WharehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_WarehouseDocument_TahvilGirandehId",
                table: "Wh_TransactionHeader",
                newName: "IX_Wh_TransactionHeader_TahvilGirandehId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_WarehouseDocument_TahvilDahandeId",
                table: "Wh_TransactionHeader",
                newName: "IX_Wh_TransactionHeader_TahvilDahandeId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_Transaction_WarehouseDocumentId",
                table: "Wh_Transactions",
                newName: "IX_Wh_Transactions_WarehouseDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_Transaction_UnitCount",
                table: "Wh_Transactions",
                newName: "IX_Wh_Transactions_UnitCount");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_Transaction_ProductOrServiceId",
                table: "Wh_Transactions",
                newName: "IX_Wh_Transactions_ProductOrServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_Transaction_CostCenterId",
                table: "Wh_Transactions",
                newName: "IX_Wh_Transactions_CostCenterId");

            migrationBuilder.AddColumn<bool>(
                name: "BranchManagerApprove",
                table: "KPOldSystemSales",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DistributorApprove",
                table: "KPOldSystemSales",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cu_ZoneZoneId",
                table: "Cu_Route",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Cu_Branch",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Cu_Branch",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Cu_Branch",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "CommissionPercentage",
                table: "Cu_Branch",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchTypeId1",
                table: "Cu_Branch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchHubHubId",
                table: "Cu_Branch",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<short>(
                name: "Cu_ServiceId",
                table: "Cu_BillOfLading",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Warehouses",
                table: "Wh_Warehouses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_TransactionHeader",
                table: "Wh_TransactionHeader",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Transactions",
                table: "Wh_Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Stocks",
                table: "Wh_Stocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_DocumentTypes",
                table: "Wh_DocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Customers",
                table: "Wh_Customers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cu_ConsignmentStatus",
                columns: table => new
                {
                    ConsignmentStatusId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_ConsignmentStatus", x => x.ConsignmentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Cu_ConsignmentType",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectOnRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_ConsignmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Fleet_FleetType",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Fleet_FleetType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Pricing_WeightRange",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    minValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    maxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Pricing_WeightRange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Service",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Shiping_RequestType",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Shiping_RequestType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Shipping_ShippingRequestStatus",
                columns: table => new
                {
                    ShippingRequestStatusId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Shipping_ShippingRequestStatus", x => x.ShippingRequestStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Cu_VehicleType",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_VehicleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Zone",
                columns: table => new
                {
                    ZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSatellite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Zone", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Consignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    ContentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RatePerKilogram = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillOfLadingId = table.Column<int>(type: "int", nullable: false),
                    BillOfLadingId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsignmentTypeId = table.Column<short>(type: "smallint", nullable: false),
                    ConsignmentStatusId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Consignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Consignment_Cu_BillOfLading_BillOfLadingId1",
                        column: x => x.BillOfLadingId1,
                        principalTable: "Cu_BillOfLading",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Consignment_Cu_ConsignmentStatus_ConsignmentStatusId",
                        column: x => x.ConsignmentStatusId,
                        principalTable: "Cu_ConsignmentStatus",
                        principalColumn: "ConsignmentStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Consignment_Cu_ConsignmentType_ConsignmentTypeId",
                        column: x => x.ConsignmentTypeId,
                        principalTable: "Cu_ConsignmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Fleet_LogisticsFleet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FleetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true),
                    PartnerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FleetTypeId = table.Column<short>(type: "smallint", nullable: false),
                    CommissionPercentage = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Fleet_LogisticsFleet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Fleet_LogisticsFleet_Cu_Fleet_FleetType_FleetTypeId",
                        column: x => x.FleetTypeId,
                        principalTable: "Cu_Fleet_FleetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Fleet_LogisticsFleet_Cu_Partner_PartnerId1",
                        column: x => x.PartnerId1,
                        principalTable: "Cu_Partner",
                        principalColumn: "PartnerId");
                });

            migrationBuilder.CreateTable(
                name: "Cu_ConsignmentServiceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsignmentTypeId = table.Column<short>(type: "smallint", nullable: false),
                    ServiceId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_ConsignmentServiceType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_ConsignmentServiceType_Cu_ConsignmentType_ConsignmentTypeId",
                        column: x => x.ConsignmentTypeId,
                        principalTable: "Cu_ConsignmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_ConsignmentServiceType_Cu_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Cu_Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_ServiceRoute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    RouteId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_ServiceRoute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_ServiceRoute_Cu_Route_RouteId1",
                        column: x => x.RouteId1,
                        principalTable: "Cu_Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_ServiceRoute_Cu_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Cu_Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Shiping_ShippingRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightRangeId = table.Column<short>(type: "smallint", nullable: false),
                    BaseRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceId = table.Column<short>(type: "smallint", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Shiping_ShippingRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Shiping_ShippingRate_Cu_Pricing_WeightRange_WeightRangeId",
                        column: x => x.WeightRangeId,
                        principalTable: "Cu_Pricing_WeightRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Shiping_ShippingRate_Cu_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Cu_Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Shiping_ShippingRate_Cu_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Cu_Zone",
                        principalColumn: "ZoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Driver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DriverPersonId = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsFleetId = table.Column<int>(type: "int", nullable: false),
                    DriverLogisticsFleetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Driver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Driver_Cu_Fleet_LogisticsFleet_DriverLogisticsFleetId",
                        column: x => x.DriverLogisticsFleetId,
                        principalTable: "Cu_Fleet_LogisticsFleet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Driver_parties_DriverPersonId",
                        column: x => x.DriverPersonId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Fleet_LogisticsFleetNeighborhood",
                columns: table => new
                {
                    MyProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cu_AddressNeighborhoodNeighborhoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cu_Fleet_LogisticsFleetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Fleet_LogisticsFleetNeighborhood", x => x.MyProperty);
                    table.ForeignKey(
                        name: "FK_Cu_Fleet_LogisticsFleetNeighborhood_Cu_AddressNeighborhoods_Cu_AddressNeighborhoodNeighborhoodId",
                        column: x => x.Cu_AddressNeighborhoodNeighborhoodId,
                        principalTable: "Cu_AddressNeighborhoods",
                        principalColumn: "NeighborhoodId");
                    table.ForeignKey(
                        name: "FK_Cu_Fleet_LogisticsFleetNeighborhood_Cu_Fleet_LogisticsFleet_Cu_Fleet_LogisticsFleetId",
                        column: x => x.Cu_Fleet_LogisticsFleetId,
                        principalTable: "Cu_Fleet_LogisticsFleet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cu_Fleet_LogisticsFleetRoute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    RouteId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogisticsFleetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Fleet_LogisticsFleetRoute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Fleet_LogisticsFleetRoute_Cu_Fleet_LogisticsFleet_LogisticsFleetId",
                        column: x => x.LogisticsFleetId,
                        principalTable: "Cu_Fleet_LogisticsFleet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Fleet_LogisticsFleetRoute_Cu_Route_RouteId1",
                        column: x => x.RouteId1,
                        principalTable: "Cu_Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Shipping_ShippingRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterActor = table.Column<short>(type: "smallint", nullable: false),
                    RequestNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLocal = table.Column<bool>(type: "bit", nullable: false),
                    PackageCount = table.Column<int>(type: "int", nullable: false),
                    TotalWeight = table.Column<double>(type: "float", nullable: false),
                    TotalVolume = table.Column<double>(type: "float", nullable: false),
                    OriginAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestTypeId = table.Column<short>(type: "smallint", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SenderNeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    ReceiverNeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    LogisticsFleetId = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    ShippingRequestStatusId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Shipping_ShippingRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingRequest_Cu_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Cu_Driver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingRequest_Cu_Fleet_LogisticsFleet_LogisticsFleetId",
                        column: x => x.LogisticsFleetId,
                        principalTable: "Cu_Fleet_LogisticsFleet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingRequest_Cu_Shiping_RequestType_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "Cu_Shiping_RequestType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingRequest_Cu_Shipping_ShippingRequestStatus_ShippingRequestStatusId",
                        column: x => x.ShippingRequestStatusId,
                        principalTable: "Cu_Shipping_ShippingRequestStatus",
                        principalColumn: "ShippingRequestStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeId = table.Column<short>(type: "smallint", nullable: false),
                    vehicleTypeId = table.Column<short>(type: "smallint", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Vehicle_Cu_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Cu_Driver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_Vehicle_Cu_VehicleType_vehicleTypeId",
                        column: x => x.vehicleTypeId,
                        principalTable: "Cu_VehicleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Shipping_ShippingOperation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsignmentId = table.Column<int>(type: "int", nullable: false),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    ShippingRequestId = table.Column<int>(type: "int", nullable: false),
                    DeliveryPersonId = table.Column<int>(type: "int", nullable: false),
                    DeliveryPersonId1 = table.Column<long>(type: "bigint", nullable: false),
                    CollectionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HandoverTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecipientPersonId = table.Column<int>(type: "int", nullable: false),
                    RecipientPersonId1 = table.Column<long>(type: "bigint", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Shipping_ShippingOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingOperation_Cu_Consignment_ConsignmentId",
                        column: x => x.ConsignmentId,
                        principalTable: "Cu_Consignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingOperation_Cu_Shipping_ShippingRequest_ShippingRequestId",
                        column: x => x.ShippingRequestId,
                        principalTable: "Cu_Shipping_ShippingRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingOperation_parties_DeliveryPersonId1",
                        column: x => x.DeliveryPersonId1,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingOperation_parties_RecipientPersonId1",
                        column: x => x.RecipientPersonId1,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Route_Cu_ZoneZoneId",
                table: "Cu_Route",
                column: "Cu_ZoneZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLading_Cu_ServiceId",
                table: "Cu_BillOfLading",
                column: "Cu_ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Consignment_BillOfLadingId1",
                table: "Cu_Consignment",
                column: "BillOfLadingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Consignment_ConsignmentStatusId",
                table: "Cu_Consignment",
                column: "ConsignmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Consignment_ConsignmentTypeId",
                table: "Cu_Consignment",
                column: "ConsignmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_ConsignmentServiceType_ConsignmentTypeId",
                table: "Cu_ConsignmentServiceType",
                column: "ConsignmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_ConsignmentServiceType_ServiceId",
                table: "Cu_ConsignmentServiceType",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Driver_DriverLogisticsFleetId",
                table: "Cu_Driver",
                column: "DriverLogisticsFleetId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Driver_DriverPersonId",
                table: "Cu_Driver",
                column: "DriverPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Fleet_LogisticsFleet_FleetTypeId",
                table: "Cu_Fleet_LogisticsFleet",
                column: "FleetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Fleet_LogisticsFleet_PartnerId1",
                table: "Cu_Fleet_LogisticsFleet",
                column: "PartnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Fleet_LogisticsFleetNeighborhood_Cu_AddressNeighborhoodNeighborhoodId",
                table: "Cu_Fleet_LogisticsFleetNeighborhood",
                column: "Cu_AddressNeighborhoodNeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Fleet_LogisticsFleetNeighborhood_Cu_Fleet_LogisticsFleetId",
                table: "Cu_Fleet_LogisticsFleetNeighborhood",
                column: "Cu_Fleet_LogisticsFleetId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Fleet_LogisticsFleetRoute_LogisticsFleetId",
                table: "Cu_Fleet_LogisticsFleetRoute",
                column: "LogisticsFleetId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Fleet_LogisticsFleetRoute_RouteId1",
                table: "Cu_Fleet_LogisticsFleetRoute",
                column: "RouteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_ServiceRoute_RouteId1",
                table: "Cu_ServiceRoute",
                column: "RouteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_ServiceRoute_ServiceId",
                table: "Cu_ServiceRoute",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shiping_ShippingRate_ServiceId",
                table: "Cu_Shiping_ShippingRate",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shiping_ShippingRate_WeightRangeId",
                table: "Cu_Shiping_ShippingRate",
                column: "WeightRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shiping_ShippingRate_ZoneId",
                table: "Cu_Shiping_ShippingRate",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shipping_ShippingOperation_ConsignmentId",
                table: "Cu_Shipping_ShippingOperation",
                column: "ConsignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shipping_ShippingOperation_DeliveryPersonId1",
                table: "Cu_Shipping_ShippingOperation",
                column: "DeliveryPersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shipping_ShippingOperation_RecipientPersonId1",
                table: "Cu_Shipping_ShippingOperation",
                column: "RecipientPersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shipping_ShippingOperation_ShippingRequestId",
                table: "Cu_Shipping_ShippingOperation",
                column: "ShippingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shipping_ShippingRequest_DriverId",
                table: "Cu_Shipping_ShippingRequest",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shipping_ShippingRequest_LogisticsFleetId",
                table: "Cu_Shipping_ShippingRequest",
                column: "LogisticsFleetId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shipping_ShippingRequest_RequestTypeId",
                table: "Cu_Shipping_ShippingRequest",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Shipping_ShippingRequest_ShippingRequestStatusId",
                table: "Cu_Shipping_ShippingRequest",
                column: "ShippingRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Vehicle_DriverId",
                table: "Cu_Vehicle",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Vehicle_vehicleTypeId",
                table: "Cu_Vehicle",
                column: "vehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wh_Customers_Wh_CustomerId",
                table: "AspNetUsers",
                column: "Wh_CustomerId",
                principalTable: "Wh_Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_BillOfLading_Cu_Service_Cu_ServiceId",
                table: "Cu_BillOfLading",
                column: "Cu_ServiceId",
                principalTable: "Cu_Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Cu_BranchType_BranchTypeId1",
                table: "Cu_Branch",
                column: "BranchTypeId1",
                principalTable: "Cu_BranchType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Cu_Hub_BranchHubHubId",
                table: "Cu_Branch",
                column: "BranchHubHubId",
                principalTable: "Cu_Hub",
                principalColumn: "HubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Route_Cu_Zone_Cu_ZoneZoneId",
                table: "Cu_Route",
                column: "Cu_ZoneZoneId",
                principalTable: "Cu_Zone",
                principalColumn: "ZoneId");

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_TransactionHeader_parties_TahvilGirandehId",
                table: "Wh_TransactionHeader",
                column: "TahvilGirandehId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transactions_CostCenter_CostCenterId",
                table: "Wh_Transactions",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transactions_Measurements_UnitCount",
                table: "Wh_Transactions",
                column: "UnitCount",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transactions_ProductOrServices_ProductOrServiceId",
                table: "Wh_Transactions",
                column: "ProductOrServiceId",
                principalTable: "ProductOrServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transactions_Wh_TransactionHeader_WarehouseDocumentId",
                table: "Wh_Transactions",
                column: "WarehouseDocumentId",
                principalTable: "Wh_TransactionHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wh_Customers_Wh_CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_BillOfLading_Cu_Service_Cu_ServiceId",
                table: "Cu_BillOfLading");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Cu_BranchType_BranchTypeId1",
                table: "Cu_Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Cu_Hub_BranchHubHubId",
                table: "Cu_Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Route_Cu_Zone_Cu_ZoneZoneId",
                table: "Cu_Route");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transactions_CostCenter_CostCenterId",
                table: "Wh_Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transactions_Measurements_UnitCount",
                table: "Wh_Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transactions_ProductOrServices_ProductOrServiceId",
                table: "Wh_Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transactions_Wh_TransactionHeader_WarehouseDocumentId",
                table: "Wh_Transactions");

            migrationBuilder.DropTable(
                name: "Cu_ConsignmentServiceType");

            migrationBuilder.DropTable(
                name: "Cu_Fleet_LogisticsFleetNeighborhood");

            migrationBuilder.DropTable(
                name: "Cu_Fleet_LogisticsFleetRoute");

            migrationBuilder.DropTable(
                name: "Cu_ServiceRoute");

            migrationBuilder.DropTable(
                name: "Cu_Shiping_ShippingRate");

            migrationBuilder.DropTable(
                name: "Cu_Shipping_ShippingOperation");

            migrationBuilder.DropTable(
                name: "Cu_Vehicle");

            migrationBuilder.DropTable(
                name: "Cu_Pricing_WeightRange");

            migrationBuilder.DropTable(
                name: "Cu_Service");

            migrationBuilder.DropTable(
                name: "Cu_Zone");

            migrationBuilder.DropTable(
                name: "Cu_Consignment");

            migrationBuilder.DropTable(
                name: "Cu_Shipping_ShippingRequest");

            migrationBuilder.DropTable(
                name: "Cu_VehicleType");

            migrationBuilder.DropTable(
                name: "Cu_ConsignmentStatus");

            migrationBuilder.DropTable(
                name: "Cu_ConsignmentType");

            migrationBuilder.DropTable(
                name: "Cu_Driver");

            migrationBuilder.DropTable(
                name: "Cu_Shiping_RequestType");

            migrationBuilder.DropTable(
                name: "Cu_Shipping_ShippingRequestStatus");

            migrationBuilder.DropTable(
                name: "Cu_Fleet_LogisticsFleet");

            migrationBuilder.DropTable(
                name: "Cu_Fleet_FleetType");

            migrationBuilder.DropIndex(
                name: "IX_Cu_Route_Cu_ZoneZoneId",
                table: "Cu_Route");

            migrationBuilder.DropIndex(
                name: "IX_Cu_BillOfLading_Cu_ServiceId",
                table: "Cu_BillOfLading");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Warehouses",
                table: "Wh_Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Transactions",
                table: "Wh_Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_TransactionHeader",
                table: "Wh_TransactionHeader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Stocks",
                table: "Wh_Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_DocumentTypes",
                table: "Wh_DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wh_Customers",
                table: "Wh_Customers");

            migrationBuilder.DropColumn(
                name: "BranchManagerApprove",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "DistributorApprove",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "Cu_ZoneZoneId",
                table: "Cu_Route");

            migrationBuilder.DropColumn(
                name: "Cu_ServiceId",
                table: "Cu_BillOfLading");

            migrationBuilder.RenameTable(
                name: "Wh_Warehouses",
                newName: "Wh_WarehousePhysicalProduct");

            migrationBuilder.RenameTable(
                name: "Wh_Transactions",
                newName: "Wh_Transaction");

            migrationBuilder.RenameTable(
                name: "Wh_TransactionHeader",
                newName: "Wh_WarehouseDocument");

            migrationBuilder.RenameTable(
                name: "Wh_Stocks",
                newName: "Wh_Stock");

            migrationBuilder.RenameTable(
                name: "Wh_DocumentTypes",
                newName: "Wh_DocumentType");

            migrationBuilder.RenameTable(
                name: "Wh_Customers",
                newName: "Wh_Customer");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_Transactions_WarehouseDocumentId",
                table: "Wh_Transaction",
                newName: "IX_Wh_Transaction_WarehouseDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_Transactions_UnitCount",
                table: "Wh_Transaction",
                newName: "IX_Wh_Transaction_UnitCount");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_Transactions_ProductOrServiceId",
                table: "Wh_Transaction",
                newName: "IX_Wh_Transaction_ProductOrServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_Transactions_CostCenterId",
                table: "Wh_Transaction",
                newName: "IX_Wh_Transaction_CostCenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_TransactionHeader_WharehouseId",
                table: "Wh_WarehouseDocument",
                newName: "IX_Wh_WarehouseDocument_WharehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_TransactionHeader_TahvilGirandehId",
                table: "Wh_WarehouseDocument",
                newName: "IX_Wh_WarehouseDocument_TahvilGirandehId");

            migrationBuilder.RenameIndex(
                name: "IX_Wh_TransactionHeader_TahvilDahandeId",
                table: "Wh_WarehouseDocument",
                newName: "IX_Wh_WarehouseDocument_TahvilDahandeId");

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionPercentage",
                table: "Cu_Partner",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PersonType",
                table: "Cu_Partner",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Cu_Branch",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Cu_Branch",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Cu_Branch",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionPercentage",
                table: "Cu_Branch",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchTypeId1",
                table: "Cu_Branch",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchHubHubId",
                table: "Cu_Branch",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_WarehousePhysicalProduct",
                table: "Wh_WarehousePhysicalProduct",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Transaction",
                table: "Wh_Transaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_WarehouseDocument",
                table: "Wh_WarehouseDocument",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Stock",
                table: "Wh_Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_DocumentType",
                table: "Wh_DocumentType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wh_Customer",
                table: "Wh_Customer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cu_LogisticsFleet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cu_PartnerPartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_LogisticsFleet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_LogisticsFleet_Cu_Partner_Cu_PartnerPartnerId",
                        column: x => x.Cu_PartnerPartnerId,
                        principalTable: "Cu_Partner",
                        principalColumn: "PartnerId");
                });

            migrationBuilder.CreateTable(
                name: "Cu_LogisticsFleetNeighborhood",
                columns: table => new
                {
                    MyProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cu_AddressNeighborhoodNeighborhoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_LogisticsFleetNeighborhood", x => x.MyProperty);
                    table.ForeignKey(
                        name: "FK_Cu_LogisticsFleetNeighborhood_Cu_AddressNeighborhoods_Cu_AddressNeighborhoodNeighborhoodId",
                        column: x => x.Cu_AddressNeighborhoodNeighborhoodId,
                        principalTable: "Cu_AddressNeighborhoods",
                        principalColumn: "NeighborhoodId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cu_LogisticsFleet_Cu_PartnerPartnerId",
                table: "Cu_LogisticsFleet",
                column: "Cu_PartnerPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_LogisticsFleetNeighborhood_Cu_AddressNeighborhoodNeighborhoodId",
                table: "Cu_LogisticsFleetNeighborhood",
                column: "Cu_AddressNeighborhoodNeighborhoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wh_Customer_Wh_CustomerId",
                table: "AspNetUsers",
                column: "Wh_CustomerId",
                principalTable: "Wh_Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Cu_BranchType_BranchTypeId1",
                table: "Cu_Branch",
                column: "BranchTypeId1",
                principalTable: "Cu_BranchType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Cu_Hub_BranchHubHubId",
                table: "Cu_Branch",
                column: "BranchHubHubId",
                principalTable: "Cu_Hub",
                principalColumn: "HubId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_parties_Wh_Customer_Wh_CustomerId",
                table: "parties",
                column: "Wh_CustomerId",
                principalTable: "Wh_Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transaction_CostCenter_CostCenterId",
                table: "Wh_Transaction",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transaction_Measurements_UnitCount",
                table: "Wh_Transaction",
                column: "UnitCount",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transaction_ProductOrServices_ProductOrServiceId",
                table: "Wh_Transaction",
                column: "ProductOrServiceId",
                principalTable: "ProductOrServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transaction_Wh_WarehouseDocument_WarehouseDocumentId",
                table: "Wh_Transaction",
                column: "WarehouseDocumentId",
                principalTable: "Wh_WarehouseDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_WarehouseDocument_Wh_WarehousePhysicalProduct_WharehouseId",
                table: "Wh_WarehouseDocument",
                column: "WharehouseId",
                principalTable: "Wh_WarehousePhysicalProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_WarehouseDocument_parties_TahvilDahandeId",
                table: "Wh_WarehouseDocument",
                column: "TahvilDahandeId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_WarehouseDocument_parties_TahvilGirandehId",
                table: "Wh_WarehouseDocument",
                column: "TahvilGirandehId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
