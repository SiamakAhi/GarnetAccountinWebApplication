using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerIdToAddressModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Cu_Route",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Cu_Branch",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Cu_AddressStates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Cu_AddressNeighborhoods",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Cu_Addresses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Cu_AddressCountries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Cu_AddressCities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 1,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 2,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 3,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 4,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 5,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 6,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 7,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 8,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 9,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 10,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 11,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 12,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 13,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 14,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 15,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 16,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 17,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 18,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 19,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 20,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 21,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 22,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 23,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 24,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 25,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 26,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 27,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 28,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 29,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 30,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCities",
                keyColumn: "CityId",
                keyValue: 31,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressCountries",
                keyColumn: "CountryId",
                keyValue: 1,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 1,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 2,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 3,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 4,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 5,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 6,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 7,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 8,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 9,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 10,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 11,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 12,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 13,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 14,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 15,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 16,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 17,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 18,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 19,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 20,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 21,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 22,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 23,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 24,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 25,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 26,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 27,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 28,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 29,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 30,
                column: "SellerId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Cu_AddressStates",
                keyColumn: "StateId",
                keyValue: 31,
                column: "SellerId",
                value: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Cu_Route");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Cu_Branch");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Cu_AddressStates");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Cu_AddressNeighborhoods");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Cu_Addresses");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Cu_AddressCountries");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Cu_AddressCities");
        }
    }
}
