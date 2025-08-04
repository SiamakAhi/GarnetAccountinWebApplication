using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class updateCourierModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyRepresentatives_parties_RepresentativeId",
                table: "PartyRepresentatives");

            migrationBuilder.DropTable(
                name: "Cu_ConsignmentServiceType");

            migrationBuilder.DropTable(
                name: "Cu_Fleet_LogisticsFleetNeighborhood");

            migrationBuilder.DropTable(
                name: "Cu_Fleet_LogisticsFleetRoute");

            migrationBuilder.DropTable(
                name: "Cu_NeighborhoodBranch");

            migrationBuilder.DropTable(
                name: "Cu_ServiceRoute");

            migrationBuilder.DropTable(
                name: "Cu_Shiping_ShippingRate");

            migrationBuilder.DropTable(
                name: "Cu_Shipping_ShippingOperation");

            migrationBuilder.DropTable(
                name: "Cu_Vehicle");

            migrationBuilder.DropTable(
                name: "Cu_Route");

            migrationBuilder.DropTable(
                name: "Cu_Pricing_WeightRange");

            migrationBuilder.DropTable(
                name: "Cu_Consignment");

            migrationBuilder.DropTable(
                name: "Cu_Shipping_ShippingRequest");

            migrationBuilder.DropTable(
                name: "Cu_VehicleType");

            migrationBuilder.DropTable(
                name: "Cu_Zone");

            migrationBuilder.DropTable(
                name: "Cu_BillOfLading");

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
                name: "Cu_Branch");

            migrationBuilder.DropTable(
                name: "Cu_Service");

            migrationBuilder.DropTable(
                name: "Cu_Fleet_LogisticsFleet");

            migrationBuilder.DropTable(
                name: "Cu_BranchType");

            migrationBuilder.DropTable(
                name: "Cu_Hub");

            migrationBuilder.DropTable(
                name: "Cu_Fleet_FleetType");

            migrationBuilder.DropTable(
                name: "Cu_Partner");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Cu_BranchType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_BranchType", x => x.Id);
                });

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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectOnRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Fleet_FleetType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Hub",
                columns: table => new
                {
                    HubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    isInAirport = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Hub", x => x.HubId);
                    table.ForeignKey(
                        name: "FK_Cu_Hub_Cu_AddressCities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cu_AddressCities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Hub_Cu_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Cu_Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Partner",
                columns: table => new
                {
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Partner", x => x.PartnerId);
                    table.ForeignKey(
                        name: "FK_Cu_Partner_parties_PersonId",
                        column: x => x.PersonId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Pricing_WeightRange",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    minValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IsSatellite = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Zone", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchHubHubId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchTypeId1 = table.Column<int>(type: "int", nullable: true),
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchTypeId = table.Column<short>(type: "smallint", nullable: false),
                    CommissionPercentage = table.Column<long>(type: "bigint", nullable: true),
                    HubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsHub = table.Column<bool>(type: "bit", nullable: false),
                    IsOwnership = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Branch_Cu_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Cu_Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Branch_Cu_BranchType_BranchTypeId1",
                        column: x => x.BranchTypeId1,
                        principalTable: "Cu_BranchType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_Branch_Cu_Hub_BranchHubHubId",
                        column: x => x.BranchHubHubId,
                        principalTable: "Cu_Hub",
                        principalColumn: "HubId");
                    table.ForeignKey(
                        name: "FK_Cu_Branch_Cu_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Cu_Partner",
                        principalColumn: "PartnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Fleet_LogisticsFleet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FleetTypeId = table.Column<short>(type: "smallint", nullable: false),
                    PartnerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommissionPercentage = table.Column<long>(type: "bigint", nullable: false),
                    FleetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
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
                name: "Cu_Route",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cu_AddressCityCityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cu_AddressCityCityId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cu_ZoneZoneId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Route_Cu_AddressCities_Cu_AddressCityCityId",
                        column: x => x.Cu_AddressCityCityId,
                        principalTable: "Cu_AddressCities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Cu_Route_Cu_AddressCities_Cu_AddressCityCityId1",
                        column: x => x.Cu_AddressCityCityId1,
                        principalTable: "Cu_AddressCities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Cu_Route_Cu_Zone_Cu_ZoneZoneId",
                        column: x => x.Cu_ZoneZoneId,
                        principalTable: "Cu_Zone",
                        principalColumn: "ZoneId");
                });

            migrationBuilder.CreateTable(
                name: "Cu_Shiping_ShippingRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<short>(type: "smallint", nullable: false),
                    WeightRangeId = table.Column<short>(type: "smallint", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false),
                    BaseRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                name: "Cu_BillOfLading",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cu_BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cu_ServiceId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_BillOfLading", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLading_Cu_Branch_Cu_BranchId",
                        column: x => x.Cu_BranchId,
                        principalTable: "Cu_Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLading_Cu_Service_Cu_ServiceId",
                        column: x => x.Cu_ServiceId,
                        principalTable: "Cu_Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cu_NeighborhoodBranch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cu_AddressNeighborhoodNeighborhoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cu_BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_NeighborhoodBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_NeighborhoodBranch_Cu_AddressNeighborhoods_Cu_AddressNeighborhoodNeighborhoodId",
                        column: x => x.Cu_AddressNeighborhoodNeighborhoodId,
                        principalTable: "Cu_AddressNeighborhoods",
                        principalColumn: "NeighborhoodId");
                    table.ForeignKey(
                        name: "FK_Cu_NeighborhoodBranch_Cu_Branch_Cu_BranchId",
                        column: x => x.Cu_BranchId,
                        principalTable: "Cu_Branch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cu_Driver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverLogisticsFleetId = table.Column<int>(type: "int", nullable: false),
                    DriverPersonId = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsFleetId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
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
                    LogisticsFleetId = table.Column<int>(type: "int", nullable: false),
                    RouteId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false)
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
                name: "Cu_ServiceRoute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<short>(type: "smallint", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false)
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
                name: "Cu_Consignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillOfLadingId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsignmentStatusId = table.Column<short>(type: "smallint", nullable: false),
                    ConsignmentTypeId = table.Column<short>(type: "smallint", nullable: false),
                    BaseRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillOfLadingId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    RatePerKilogram = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false)
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
                name: "Cu_Shipping_ShippingRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    LogisticsFleetId = table.Column<int>(type: "int", nullable: true),
                    RequestTypeId = table.Column<short>(type: "smallint", nullable: false),
                    ShippingRequestStatusId = table.Column<short>(type: "smallint", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLocal = table.Column<bool>(type: "bit", nullable: false),
                    OriginAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageCount = table.Column<int>(type: "int", nullable: false),
                    ReceiverNeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    RequestDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequesterActor = table.Column<short>(type: "smallint", nullable: false),
                    SenderNeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVolume = table.Column<double>(type: "float", nullable: false),
                    TotalWeight = table.Column<double>(type: "float", nullable: false)
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
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    vehicleTypeId = table.Column<short>(type: "smallint", nullable: false),
                    Capacity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<short>(type: "smallint", nullable: false)
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
                    DeliveryPersonId1 = table.Column<long>(type: "bigint", nullable: false),
                    RecipientPersonId1 = table.Column<long>(type: "bigint", nullable: false),
                    ShippingRequestId = table.Column<int>(type: "int", nullable: false),
                    CollectionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryPersonId = table.Column<int>(type: "int", nullable: false),
                    HandoverTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false),
                    RecipientPersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Shipping_ShippingOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingOperation_Cu_Consignment_ConsignmentId",
                        column: x => x.ConsignmentId,
                        principalTable: "Cu_Consignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingOperation_Cu_Shipping_ShippingRequest_ShippingRequestId",
                        column: x => x.ShippingRequestId,
                        principalTable: "Cu_Shipping_ShippingRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingOperation_parties_DeliveryPersonId1",
                        column: x => x.DeliveryPersonId1,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Shipping_ShippingOperation_parties_RecipientPersonId1",
                        column: x => x.RecipientPersonId1,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLading_Cu_BranchId",
                table: "Cu_BillOfLading",
                column: "Cu_BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLading_Cu_ServiceId",
                table: "Cu_BillOfLading",
                column: "Cu_ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_AddressId",
                table: "Cu_Branch",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_BranchHubHubId",
                table: "Cu_Branch",
                column: "BranchHubHubId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_BranchTypeId1",
                table: "Cu_Branch",
                column: "BranchTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_PartnerId",
                table: "Cu_Branch",
                column: "PartnerId");

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
                name: "IX_Cu_Hub_AddressId",
                table: "Cu_Hub",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Hub_CityId",
                table: "Cu_Hub",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_NeighborhoodBranch_Cu_AddressNeighborhoodNeighborhoodId",
                table: "Cu_NeighborhoodBranch",
                column: "Cu_AddressNeighborhoodNeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_NeighborhoodBranch_Cu_BranchId",
                table: "Cu_NeighborhoodBranch",
                column: "Cu_BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Partner_PersonId",
                table: "Cu_Partner",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Route_Cu_AddressCityCityId",
                table: "Cu_Route",
                column: "Cu_AddressCityCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Route_Cu_AddressCityCityId1",
                table: "Cu_Route",
                column: "Cu_AddressCityCityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Route_Cu_ZoneZoneId",
                table: "Cu_Route",
                column: "Cu_ZoneZoneId");

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
                name: "FK_PartyRepresentatives_parties_RepresentativeId",
                table: "PartyRepresentatives",
                column: "RepresentativeId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
