using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_com_Invoice_parties_PartyId",
                table: "com_Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_com_InvoiceItem_Wh_Products_ProductId",
                table: "com_InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_com_InvoiceItem_com_Invoice_InvoiceId",
                table: "com_InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_WarehouseDocumentItems_com_InvoiceItem_InvoiceItemId",
                table: "Wh_WarehouseDocumentItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_com_InvoiceItem",
                table: "com_InvoiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_com_Invoice",
                table: "com_Invoice");

            migrationBuilder.RenameTable(
                name: "com_InvoiceItem",
                newName: "InvoiceItems");

            migrationBuilder.RenameTable(
                name: "com_Invoice",
                newName: "Invoices");

            migrationBuilder.RenameIndex(
                name: "IX_com_InvoiceItem_ProductId",
                table: "InvoiceItems",
                newName: "IX_InvoiceItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_com_InvoiceItem_InvoiceId",
                table: "InvoiceItems",
                newName: "IX_InvoiceItems_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_com_Invoice_PartyId",
                table: "Invoices",
                newName: "IX_Invoices_PartyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItems",
                table: "InvoiceItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Wh_Products_ProductId",
                table: "InvoiceItems",
                column: "ProductId",
                principalTable: "Wh_Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_parties_PartyId",
                table: "Invoices",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_WarehouseDocumentItems_InvoiceItems_InvoiceItemId",
                table: "Wh_WarehouseDocumentItems",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceId",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Wh_Products_ProductId",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_parties_PartyId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Wh_WarehouseDocumentItems_InvoiceItems_InvoiceItemId",
                table: "Wh_WarehouseDocumentItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItems",
                table: "InvoiceItems");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "com_Invoice");

            migrationBuilder.RenameTable(
                name: "InvoiceItems",
                newName: "com_InvoiceItem");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_PartyId",
                table: "com_Invoice",
                newName: "IX_com_Invoice_PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_ProductId",
                table: "com_InvoiceItem",
                newName: "IX_com_InvoiceItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "com_InvoiceItem",
                newName: "IX_com_InvoiceItem_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_com_Invoice",
                table: "com_Invoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_com_InvoiceItem",
                table: "com_InvoiceItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_com_Invoice_parties_PartyId",
                table: "com_Invoice",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_com_InvoiceItem_Wh_Products_ProductId",
                table: "com_InvoiceItem",
                column: "ProductId",
                principalTable: "Wh_Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_com_InvoiceItem_com_Invoice_InvoiceId",
                table: "com_InvoiceItem",
                column: "InvoiceId",
                principalTable: "com_Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_WarehouseDocumentItems_com_InvoiceItem_InvoiceItemId",
                table: "Wh_WarehouseDocumentItems",
                column: "InvoiceItemId",
                principalTable: "com_InvoiceItem",
                principalColumn: "Id");
        }
    }
}
