using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "com_Invoice");

            migrationBuilder.RenameColumn(
                name: "QuantityInUnit",
                table: "com_InvoiceItem",
                newName: "VatRate");

            migrationBuilder.RenameColumn(
                name: "LineAmount",
                table: "com_InvoiceItem",
                newName: "VatPrice");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "com_InvoiceItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "com_InvoiceItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "com_InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "EditorUserId",
                table: "com_InvoiceItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPrice",
                table: "com_InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "com_InvoiceItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                table: "com_InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceBeForDescount",
                table: "com_InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityInPakageUnit",
                table: "com_InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityInPerPakage",
                table: "com_InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalQuantity",
                table: "com_InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ArchiveRef",
                table: "com_Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "com_Invoice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "com_Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EditorUserId",
                table: "com_Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinancePeriodId",
                table: "com_Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InvoiceAutoNumber",
                table: "com_Invoice",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceSubject",
                table: "com_Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "com_Invoice",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SequenceNumber",
                table: "com_Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SettlementTypeId",
                table: "com_Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TaxInvoiceNumber",
                table: "com_Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Visitor",
                table: "com_Invoice",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "VisitorPercent",
                table: "com_Invoice",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "status",
                table: "com_Invoice",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_com_Invoice_PartyId",
                table: "com_Invoice",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_com_Invoice_parties_PartyId",
                table: "com_Invoice",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_com_Invoice_parties_PartyId",
                table: "com_Invoice");

            migrationBuilder.DropIndex(
                name: "IX_com_Invoice_PartyId",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "PriceBeForDescount",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "QuantityInPakageUnit",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "QuantityInPerPakage",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "com_InvoiceItem");

            migrationBuilder.DropColumn(
                name: "ArchiveRef",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "FinancePeriodId",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "InvoiceAutoNumber",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "InvoiceSubject",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "SettlementTypeId",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "TaxInvoiceNumber",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "Visitor",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "VisitorPercent",
                table: "com_Invoice");

            migrationBuilder.DropColumn(
                name: "status",
                table: "com_Invoice");

            migrationBuilder.RenameColumn(
                name: "VatRate",
                table: "com_InvoiceItem",
                newName: "QuantityInUnit");

            migrationBuilder.RenameColumn(
                name: "VatPrice",
                table: "com_InvoiceItem",
                newName: "LineAmount");

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "com_InvoiceItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "com_Invoice",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
