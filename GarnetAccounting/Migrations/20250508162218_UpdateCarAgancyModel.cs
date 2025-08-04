using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarAgancyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractoePercent",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "IsCampaign",
                table: "Asa_Receptions");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "Asa_Receptions",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "Asa_Receptions",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "ServiceCode",
                table: "Asa_Receptions",
                newName: "BachRefrense");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "Asa_Receptions",
                newName: "ArchiveNum");

            migrationBuilder.RenameColumn(
                name: "IsDone",
                table: "Asa_Receptions",
                newName: "Campaign");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Asa_Receptions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ContractorShareAmount",
                table: "Asa_Receptions",
                newName: "TechnicianId");

            migrationBuilder.RenameColumn(
                name: "AgencyCode",
                table: "Asa_Receptions",
                newName: "PeriodId");

            migrationBuilder.AlterColumn<string>(
                name: "ReceptionNumber",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Asa_Receptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceCode",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgentCode",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Asa_Receptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CtreateBy",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerFullName",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "DocNum",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ExtraPrice",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDocument",
                table: "Asa_Receptions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KilometersAtReception",
                table: "Asa_Receptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "LocationCode",
                table: "Asa_Receptions",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogisticsCode",
                table: "Asa_Receptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "PersonRate",
                table: "Asa_Receptions",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalGhate",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalOjrat",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<short>(
                name: "TypeCode",
                table: "Asa_Receptions",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentCode",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "CtreateBy",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "CustomerFullName",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "DocNum",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "ExtraPrice",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "HasDocument",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "KilometersAtReception",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "LocationCode",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "LogisticsCode",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "PersonRate",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "TotalGhate",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "TotalOjrat",
                table: "Asa_Receptions");

            migrationBuilder.DropColumn(
                name: "TypeCode",
                table: "Asa_Receptions");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "Asa_Receptions",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "Asa_Receptions",
                newName: "ContractorShareAmount");

            migrationBuilder.RenameColumn(
                name: "PeriodId",
                table: "Asa_Receptions",
                newName: "AgencyCode");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Asa_Receptions",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "Campaign",
                table: "Asa_Receptions",
                newName: "IsDone");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Asa_Receptions",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "BachRefrense",
                table: "Asa_Receptions",
                newName: "ServiceCode");

            migrationBuilder.RenameColumn(
                name: "ArchiveNum",
                table: "Asa_Receptions",
                newName: "ItemType");

            migrationBuilder.AlterColumn<long>(
                name: "ReceptionNumber",
                table: "Asa_Receptions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Asa_Receptions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceCode",
                table: "Asa_Receptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "ContractoePercent",
                table: "Asa_Receptions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCampaign",
                table: "Asa_Receptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
