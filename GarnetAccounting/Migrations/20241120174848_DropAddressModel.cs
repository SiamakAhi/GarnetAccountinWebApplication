using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class DropAddressModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_Branch_Cu_AddressCities_CityId",
                table: "Cu_Branch");

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

            migrationBuilder.DropIndex(
                name: "IX_Cu_Branch_CityId",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "HasDistribution",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "HasPickup",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "HasSale",
                table: "Cu_Branch");

            migrationBuilder.AddColumn<bool>(
                name: "IsBillOfLadingIssuer",
                table: "Cu_Branch",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIntercityFleet",
                table: "Cu_Branch",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUrbanFleet",
                table: "Cu_Branch",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBillOfLadingIssuer",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "IsIntercityFleet",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "IsUrbanFleet",
                table: "Cu_Branch");

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Cu_Branch",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDistribution",
                table: "Cu_Branch",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPickup",
                table: "Cu_Branch",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSale",
                table: "Cu_Branch",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cu_AddressCountries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cu_AddressCountries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Cu_AddressStates",
                columns: table => new
                {
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                name: "Cu_AddressCities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictNo = table.Column<byte>(type: "tinyint", nullable: true),
                    IsSuburbs = table.Column<bool>(type: "bit", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "Cu_Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NeighborhoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    AddressText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefaultForReceiver = table.Column<bool>(type: "bit", nullable: false),
                    IsDefaultForSender = table.Column<bool>(type: "bit", nullable: false),
                    Landline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Cu_Branch_CityId",
                table: "Cu_Branch",
                column: "CityId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_Branch_Cu_AddressCities_CityId",
                table: "Cu_Branch",
                column: "CityId",
                principalTable: "Cu_AddressCities",
                principalColumn: "CityId");
        }
    }
}
