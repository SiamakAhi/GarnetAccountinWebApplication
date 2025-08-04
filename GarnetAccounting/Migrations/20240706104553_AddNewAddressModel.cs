using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAddressModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cu_AddressCountries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressCountries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Cu_BranchType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_BranchType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Partner",
                columns: table => new
                {
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false)
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
                name: "Cu_AddressStates",
                columns: table => new
                {
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressStates", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_Cu_AddressStates_Cu_AddressCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Cu_AddressCountries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Cu_AddressCities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressCities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cu_AddressCities_Cu_AddressStates_StateId",
                        column: x => x.StateId,
                        principalTable: "Cu_AddressStates",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_AddressNeighborhoods",
                columns: table => new
                {
                    NeighborhoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DistrictNo = table.Column<byte>(type: "tinyint", nullable: true),
                    IsSuburbs = table.Column<bool>(type: "bit", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressNeighborhoods", x => x.NeighborhoodId);
                    table.ForeignKey(
                        name: "FK_Cu_AddressNeighborhoods_Cu_AddressCities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cu_AddressCities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Route",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    Cu_AddressCityCityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cu_AddressCityCityId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Cu_Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    AddressText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Landline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefaultForSender = table.Column<bool>(type: "bit", nullable: false),
                    IsDefaultForReceiver = table.Column<bool>(type: "bit", nullable: false),
                    NeighborhoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Cu_Addresses_Cu_AddressNeighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalTable: "Cu_AddressNeighborhoods",
                        principalColumn: "NeighborhoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cu_Addresses_parties_PersonId",
                        column: x => x.PersonId,
                        principalTable: "parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Cu_Hub",
                columns: table => new
                {
                    HubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isInAirport = table.Column<bool>(type: "bit", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Hub", x => x.HubId);
                    table.ForeignKey(
                        name: "FK_Cu_Hub_Cu_AddressCities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cu_AddressCities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cu_Hub_Cu_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Cu_Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cu_Branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOwnership = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CommissionPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsHub = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    BranchTypeId = table.Column<short>(type: "smallint", nullable: false),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchHubHubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    BranchTypeId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_Branch_Cu_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Cu_Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cu_Branch_Cu_BranchType_BranchTypeId1",
                        column: x => x.BranchTypeId1,
                        principalTable: "Cu_BranchType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cu_Branch_Cu_Hub_BranchHubHubId",
                        column: x => x.BranchHubHubId,
                        principalTable: "Cu_Hub",
                        principalColumn: "HubId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cu_Branch_Cu_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Cu_Partner",
                        principalColumn: "PartnerId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cu_BillOfLading",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cu_BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_BillOfLading", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cu_BillOfLading_Cu_Branch_Cu_BranchId",
                        column: x => x.Cu_BranchId,
                        principalTable: "Cu_Branch",
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

            migrationBuilder.CreateIndex(
                name: "IX_Cu_AddressCities_StateId",
                table: "Cu_AddressCities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Addresses_NeighborhoodId",
                table: "Cu_Addresses",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Addresses_PersonId",
                table: "Cu_Addresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_AddressNeighborhoods_CityId",
                table: "Cu_AddressNeighborhoods",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_AddressStates_CountryId",
                table: "Cu_AddressStates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLading_Cu_BranchId",
                table: "Cu_BillOfLading",
                column: "Cu_BranchId");

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
                name: "IX_Cu_Hub_AddressId",
                table: "Cu_Hub",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Hub_CityId",
                table: "Cu_Hub",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_LogisticsFleet_Cu_PartnerPartnerId",
                table: "Cu_LogisticsFleet",
                column: "Cu_PartnerPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_LogisticsFleetNeighborhood_Cu_AddressNeighborhoodNeighborhoodId",
                table: "Cu_LogisticsFleetNeighborhood",
                column: "Cu_AddressNeighborhoodNeighborhoodId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cu_BillOfLading");

            migrationBuilder.DropTable(
                name: "Cu_LogisticsFleet");

            migrationBuilder.DropTable(
                name: "Cu_LogisticsFleetNeighborhood");

            migrationBuilder.DropTable(
                name: "Cu_NeighborhoodBranch");

            migrationBuilder.DropTable(
                name: "Cu_Route");

            migrationBuilder.DropTable(
                name: "Cu_Branch");

            migrationBuilder.DropTable(
                name: "Cu_BranchType");

            migrationBuilder.DropTable(
                name: "Cu_Hub");

            migrationBuilder.DropTable(
                name: "Cu_Partner");

            migrationBuilder.DropTable(
                name: "Cu_Addresses");

            migrationBuilder.DropTable(
                name: "Cu_AddressNeighborhoods");

            migrationBuilder.DropTable(
                name: "Cu_AddressCities");

            migrationBuilder.DropTable(
                name: "Cu_AddressStates");

            migrationBuilder.DropTable(
                name: "Cu_AddressCountries");
        }
    }
}
