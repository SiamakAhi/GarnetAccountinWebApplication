using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientPersonCode",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderPersonCode",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "projectId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IATACode",
                table: "Geo_Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DestinationBranchId",
                table: "Cu_BillOfLadings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "Con_Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Con_Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProjectNumber",
                table: "Con_Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 10,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 11,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 12,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 13,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 14,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 15,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 16,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 17,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 18,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 19,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 20,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 21,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 22,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 23,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 24,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 25,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 26,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 27,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 28,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 29,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 30,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 31,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 32,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 33,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 34,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 35,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 36,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 37,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 38,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 39,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 40,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 41,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 42,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 43,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 44,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 45,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 46,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 47,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 48,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 49,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 50,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 51,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 52,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 53,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 54,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 55,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 56,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 57,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 58,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 59,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 60,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 61,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 62,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 63,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 64,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 65,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 66,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 67,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 68,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 69,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 70,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 71,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 72,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 73,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 74,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 75,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 76,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 77,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 78,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 79,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 80,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 81,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 82,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 83,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 84,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 85,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 86,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 87,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 88,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 89,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 90,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 91,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 92,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 93,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 94,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 95,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 96,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 97,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 98,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 99,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 100,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 101,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 102,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 103,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 104,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 105,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 106,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 107,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 108,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 109,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 110,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 111,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 112,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 113,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 114,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 115,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 116,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 117,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 118,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 119,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 120,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 121,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 122,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 123,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 124,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 125,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 126,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 127,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 128,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 129,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 130,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 131,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 132,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 133,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 134,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 135,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 136,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 137,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 138,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 139,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 140,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 141,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 142,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 143,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 144,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 145,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 146,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 147,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 148,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 149,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 150,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 151,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 152,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 153,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 154,
                column: "IATACode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Geo_Cities",
                keyColumn: "Id",
                keyValue: 155,
                column: "IATACode",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_projectId",
                table: "Invoices",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_Cu_BillOfLadings_DestinationBranchId",
                table: "Cu_BillOfLadings",
                column: "DestinationBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Con_Projects_ClientId",
                table: "Con_Projects",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Con_Projects_parties_ClientId",
                table: "Con_Projects",
                column: "ClientId",
                principalTable: "parties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Branch_DestinationBranchId",
                table: "Cu_BillOfLadings",
                column: "DestinationBranchId",
                principalTable: "Cu_Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Con_Projects_projectId",
                table: "Invoices",
                column: "projectId",
                principalTable: "Con_Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Con_Projects_parties_ClientId",
                table: "Con_Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_BillOfLadings_Cu_Branch_DestinationBranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Con_Projects_projectId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_projectId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Cu_BillOfLadings_DestinationBranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropIndex(
                name: "IX_Con_Projects_ClientId",
                table: "Con_Projects");

            migrationBuilder.DropColumn(
                name: "RecipientPersonCode",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "SenderPersonCode",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "projectId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IATACode",
                table: "Geo_Cities");

            migrationBuilder.DropColumn(
                name: "DestinationBranchId",
                table: "Cu_BillOfLadings");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Con_Projects");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Con_Projects");

            migrationBuilder.DropColumn(
                name: "ProjectNumber",
                table: "Con_Projects");
        }
    }
}
