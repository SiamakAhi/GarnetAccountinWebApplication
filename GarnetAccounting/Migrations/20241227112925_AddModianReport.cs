using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddModianReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acc_ModianReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    IsSaleInvoice = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoicePattern = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalInvoiceAmount = table.Column<long>(type: "bigint", nullable: false),
                    VAT = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FolderInsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerEconomicNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerTradeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerPersonType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SettlementMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearAndPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimitStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceAmountWithoutVAT = table.Column<long>(type: "bigint", nullable: false),
                    ReferringInvoiceIssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NonAccountingStatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceInvoiceTaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceSettlementBalance = table.Column<long>(type: "bigint", nullable: false),
                    HasAccountingDoc = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_ModianReports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acc_ModianReports");
        }
    }
}
