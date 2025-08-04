using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class KpOldData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KPOldSystemSales",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineCode = table.Column<long>(type: "bigint", nullable: true),
                    BillOfLadingGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillOfLadingNumber = table.Column<long>(type: "bigint", nullable: true),
                    NonSystemicBillOfLadingNumber = table.Column<long>(type: "bigint", nullable: true),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoFare = table.Column<long>(type: "bigint", nullable: true),
                    BillOfLadingCount = table.Column<long>(type: "bigint", nullable: true),
                    CashPaymentAmount = table.Column<long>(type: "bigint", nullable: true),
                    NonCashPaymentAmount = table.Column<long>(type: "bigint", nullable: true),
                    SenderCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderWeight = table.Column<long>(type: "bigint", nullable: true),
                    RecipientWeight = table.Column<long>(type: "bigint", nullable: true),
                    BaseFare = table.Column<long>(type: "bigint", nullable: true),
                    ExtraFare = table.Column<long>(type: "bigint", nullable: true),
                    TotalFare = table.Column<long>(type: "bigint", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillOfLadingType = table.Column<long>(type: "bigint", nullable: true),
                    RegisterCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillOfLadingState = table.Column<long>(type: "bigint", nullable: true),
                    Discount = table.Column<long>(type: "bigint", nullable: true),
                    PaymentByCash = table.Column<long>(type: "bigint", nullable: true),
                    PaymentByPos = table.Column<long>(type: "bigint", nullable: true),
                    PaymentByCredit = table.Column<long>(type: "bigint", nullable: true),
                    InputBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageCount = table.Column<long>(type: "bigint", nullable: true),
                    SenderCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderProvince = table.Column<long>(type: "bigint", nullable: true),
                    RecipientProvince = table.Column<long>(type: "bigint", nullable: true),
                    CarNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PenaltyAmount = table.Column<long>(type: "bigint", nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceCode = table.Column<long>(type: "bigint", nullable: true),
                    DataEntryDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordLock = table.Column<long>(type: "bigint", nullable: true),
                    BillOfLadingGroupCode = table.Column<long>(type: "bigint", nullable: true),
                    DataEntryUserCode = table.Column<long>(type: "bigint", nullable: true),
                    DataEntryUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditUserCode = table.Column<long>(type: "bigint", nullable: true),
                    EditRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NonSystemicRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillOfLadingCopyCount = table.Column<long>(type: "bigint", nullable: true),
                    TaxAmount = table.Column<long>(type: "bigint", nullable: true),
                    SenderContact = table.Column<long>(type: "bigint", nullable: true),
                    RecipientContact = table.Column<long>(type: "bigint", nullable: true),
                    PenaltyCancel = table.Column<long>(type: "bigint", nullable: true),
                    ServiceGroupCode = table.Column<long>(type: "bigint", nullable: true),
                    InformationEntryDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordLockStatus = table.Column<long>(type: "bigint", nullable: true),
                    BillOfLadingGroupCategory = table.Column<long>(type: "bigint", nullable: true),
                    DataEntryOperatorCode = table.Column<long>(type: "bigint", nullable: true),
                    DataEntryOperatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditInformationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditOperatorCode = table.Column<long>(type: "bigint", nullable: true),
                    EditDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountingDocNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPOldSystemSales", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KPOldSystemSales");
        }
    }
}
