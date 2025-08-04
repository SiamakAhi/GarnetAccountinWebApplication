using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class KpOldDataAddChangeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountingDocNumber",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "BillOfLadingCopyCount",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "BillOfLadingCount",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "BillOfLadingGroupCategory",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "BillOfLadingState",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "BillOfLadingType",
                table: "KPOldSystemSales");

            migrationBuilder.RenameColumn(
                name: "TotalFare",
                table: "KPOldSystemSales",
                newName: "VAT");

            migrationBuilder.RenameColumn(
                name: "TaxAmount",
                table: "KPOldSystemSales",
                newName: "TransitSeparationFee");

            migrationBuilder.RenameColumn(
                name: "ServiceGroupCode",
                table: "KPOldSystemSales",
                newName: "TransitMiscellaneousFee");

            migrationBuilder.RenameColumn(
                name: "SenderWeight",
                table: "KPOldSystemSales",
                newName: "TransitCargoFare");

            migrationBuilder.RenameColumn(
                name: "SenderProvince",
                table: "KPOldSystemSales",
                newName: "TotalServiceFee");

            migrationBuilder.RenameColumn(
                name: "SenderContact",
                table: "KPOldSystemSales",
                newName: "TotalBillOfLadingAmount");

            migrationBuilder.RenameColumn(
                name: "SenderCompany",
                table: "KPOldSystemSales",
                newName: "TransitRepresentative");

            migrationBuilder.RenameColumn(
                name: "SenderCity",
                table: "KPOldSystemSales",
                newName: "ToTransitDestination");

            migrationBuilder.RenameColumn(
                name: "RegisterCode",
                table: "KPOldSystemSales",
                newName: "ToDestination");

            migrationBuilder.RenameColumn(
                name: "RecordLockStatus",
                table: "KPOldSystemSales",
                newName: "StampFee");

            migrationBuilder.RenameColumn(
                name: "RecipientWeight",
                table: "KPOldSystemSales",
                newName: "RoundingAmount");

            migrationBuilder.RenameColumn(
                name: "RecipientProvince",
                table: "KPOldSystemSales",
                newName: "RecipientCustomerCode");

            migrationBuilder.RenameColumn(
                name: "RecipientContact",
                table: "KPOldSystemSales",
                newName: "PackagingFee");

            migrationBuilder.RenameColumn(
                name: "RecipientCompany",
                table: "KPOldSystemSales",
                newName: "SenderZoneName");

            migrationBuilder.RenameColumn(
                name: "RecipientCity",
                table: "KPOldSystemSales",
                newName: "SenderZoneCode");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "KPOldSystemSales",
                newName: "SenderZoneAddress");

            migrationBuilder.RenameColumn(
                name: "PenaltyCancel",
                table: "KPOldSystemSales",
                newName: "OtherOriginFees");

            migrationBuilder.RenameColumn(
                name: "PenaltyAmount",
                table: "KPOldSystemSales",
                newName: "OldSeparationFee");

            migrationBuilder.RenameColumn(
                name: "PaymentByPos",
                table: "KPOldSystemSales",
                newName: "MiscellaneousFee");

            migrationBuilder.RenameColumn(
                name: "PaymentByCredit",
                table: "KPOldSystemSales",
                newName: "InsuranceFee");

            migrationBuilder.RenameColumn(
                name: "PaymentByCash",
                table: "KPOldSystemSales",
                newName: "GoodsCount");

            migrationBuilder.RenameColumn(
                name: "PackageCount",
                table: "KPOldSystemSales",
                newName: "DistributionOrSeparationFee");

            migrationBuilder.RenameColumn(
                name: "NonSystemicRemarks",
                table: "KPOldSystemSales",
                newName: "SenderPhone");

            migrationBuilder.RenameColumn(
                name: "NonCashPaymentAmount",
                table: "KPOldSystemSales",
                newName: "DestinationMiscellaneousFee");

            migrationBuilder.RenameColumn(
                name: "InputBy",
                table: "KPOldSystemSales",
                newName: "SenderNationalCode");

            migrationBuilder.RenameColumn(
                name: "InformationEntryDate",
                table: "KPOldSystemSales",
                newName: "SenderAddress");

            migrationBuilder.RenameColumn(
                name: "ExtraFare",
                table: "KPOldSystemSales",
                newName: "DeclaredGoodsValue");

            migrationBuilder.RenameColumn(
                name: "EditUserCode",
                table: "KPOldSystemSales",
                newName: "CustomerCode");

            migrationBuilder.RenameColumn(
                name: "EditRemarks",
                table: "KPOldSystemSales",
                newName: "RecipientZoneName");

            migrationBuilder.RenameColumn(
                name: "EditOperatorCode",
                table: "KPOldSystemSales",
                newName: "CollectionOrSeparationFee");

            migrationBuilder.RenameColumn(
                name: "EditInformationDate",
                table: "KPOldSystemSales",
                newName: "RecipientZoneAddress");

            migrationBuilder.RenameColumn(
                name: "EditDetails",
                table: "KPOldSystemSales",
                newName: "RecipientPhone");

            migrationBuilder.RenameColumn(
                name: "DeliveryRemarks",
                table: "KPOldSystemSales",
                newName: "RecipientCode");

            migrationBuilder.RenameColumn(
                name: "DataEntryOperatorName",
                table: "KPOldSystemSales",
                newName: "RecipientAddress");

            migrationBuilder.RenameColumn(
                name: "DataEntryOperatorCode",
                table: "KPOldSystemSales",
                newName: "CancellationPenalty");

            migrationBuilder.RenameColumn(
                name: "CashPaymentAmount",
                table: "KPOldSystemSales",
                newName: "AddedValue");

            migrationBuilder.RenameColumn(
                name: "CargoType",
                table: "KPOldSystemSales",
                newName: "RateType");

            migrationBuilder.RenameColumn(
                name: "CarNumber",
                table: "KPOldSystemSales",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "CancelReason",
                table: "KPOldSystemSales",
                newName: "POSReceiptNumber");

            migrationBuilder.RenameColumn(
                name: "BranchCode",
                table: "KPOldSystemSales",
                newName: "FromOrigin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "KPOldSystemSales",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<float>(
                name: "ActualCargoWeight",
                table: "KPOldSystemSales",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Cancellation",
                table: "KPOldSystemSales",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                table: "KPOldSystemSales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancellationUser",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "ChargeableWeight",
                table: "KPOldSystemSales",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contents",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourierName",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreditCompany",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryConfirmation",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DestinationRepresentative",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EditUserName",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FinancialInformation",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceInformation",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VolumetricWeight",
                table: "KPOldSystemSales",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualCargoWeight",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "Cancellation",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "CancellationDate",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "CancellationUser",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "ChargeableWeight",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "Contents",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "CourierName",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "CreditCompany",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "DeliveryConfirmation",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "DestinationRepresentative",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "EditUserName",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "FinancialInformation",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "ServiceInformation",
                table: "KPOldSystemSales");

            migrationBuilder.DropColumn(
                name: "VolumetricWeight",
                table: "KPOldSystemSales");

            migrationBuilder.RenameColumn(
                name: "VAT",
                table: "KPOldSystemSales",
                newName: "TotalFare");

            migrationBuilder.RenameColumn(
                name: "TransitSeparationFee",
                table: "KPOldSystemSales",
                newName: "TaxAmount");

            migrationBuilder.RenameColumn(
                name: "TransitRepresentative",
                table: "KPOldSystemSales",
                newName: "SenderCompany");

            migrationBuilder.RenameColumn(
                name: "TransitMiscellaneousFee",
                table: "KPOldSystemSales",
                newName: "ServiceGroupCode");

            migrationBuilder.RenameColumn(
                name: "TransitCargoFare",
                table: "KPOldSystemSales",
                newName: "SenderWeight");

            migrationBuilder.RenameColumn(
                name: "TotalServiceFee",
                table: "KPOldSystemSales",
                newName: "SenderProvince");

            migrationBuilder.RenameColumn(
                name: "TotalBillOfLadingAmount",
                table: "KPOldSystemSales",
                newName: "SenderContact");

            migrationBuilder.RenameColumn(
                name: "ToTransitDestination",
                table: "KPOldSystemSales",
                newName: "SenderCity");

            migrationBuilder.RenameColumn(
                name: "ToDestination",
                table: "KPOldSystemSales",
                newName: "RegisterCode");

            migrationBuilder.RenameColumn(
                name: "StampFee",
                table: "KPOldSystemSales",
                newName: "RecordLockStatus");

            migrationBuilder.RenameColumn(
                name: "SenderZoneName",
                table: "KPOldSystemSales",
                newName: "RecipientCompany");

            migrationBuilder.RenameColumn(
                name: "SenderZoneCode",
                table: "KPOldSystemSales",
                newName: "RecipientCity");

            migrationBuilder.RenameColumn(
                name: "SenderZoneAddress",
                table: "KPOldSystemSales",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "SenderPhone",
                table: "KPOldSystemSales",
                newName: "NonSystemicRemarks");

            migrationBuilder.RenameColumn(
                name: "SenderNationalCode",
                table: "KPOldSystemSales",
                newName: "InputBy");

            migrationBuilder.RenameColumn(
                name: "SenderAddress",
                table: "KPOldSystemSales",
                newName: "InformationEntryDate");

            migrationBuilder.RenameColumn(
                name: "RoundingAmount",
                table: "KPOldSystemSales",
                newName: "RecipientWeight");

            migrationBuilder.RenameColumn(
                name: "RecipientZoneName",
                table: "KPOldSystemSales",
                newName: "EditRemarks");

            migrationBuilder.RenameColumn(
                name: "RecipientZoneAddress",
                table: "KPOldSystemSales",
                newName: "EditInformationDate");

            migrationBuilder.RenameColumn(
                name: "RecipientPhone",
                table: "KPOldSystemSales",
                newName: "EditDetails");

            migrationBuilder.RenameColumn(
                name: "RecipientCustomerCode",
                table: "KPOldSystemSales",
                newName: "RecipientProvince");

            migrationBuilder.RenameColumn(
                name: "RecipientCode",
                table: "KPOldSystemSales",
                newName: "DeliveryRemarks");

            migrationBuilder.RenameColumn(
                name: "RecipientAddress",
                table: "KPOldSystemSales",
                newName: "DataEntryOperatorName");

            migrationBuilder.RenameColumn(
                name: "RateType",
                table: "KPOldSystemSales",
                newName: "CargoType");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "KPOldSystemSales",
                newName: "CarNumber");

            migrationBuilder.RenameColumn(
                name: "PackagingFee",
                table: "KPOldSystemSales",
                newName: "RecipientContact");

            migrationBuilder.RenameColumn(
                name: "POSReceiptNumber",
                table: "KPOldSystemSales",
                newName: "CancelReason");

            migrationBuilder.RenameColumn(
                name: "OtherOriginFees",
                table: "KPOldSystemSales",
                newName: "PenaltyCancel");

            migrationBuilder.RenameColumn(
                name: "OldSeparationFee",
                table: "KPOldSystemSales",
                newName: "PenaltyAmount");

            migrationBuilder.RenameColumn(
                name: "MiscellaneousFee",
                table: "KPOldSystemSales",
                newName: "PaymentByPos");

            migrationBuilder.RenameColumn(
                name: "InsuranceFee",
                table: "KPOldSystemSales",
                newName: "PaymentByCredit");

            migrationBuilder.RenameColumn(
                name: "GoodsCount",
                table: "KPOldSystemSales",
                newName: "PaymentByCash");

            migrationBuilder.RenameColumn(
                name: "FromOrigin",
                table: "KPOldSystemSales",
                newName: "BranchCode");

            migrationBuilder.RenameColumn(
                name: "DistributionOrSeparationFee",
                table: "KPOldSystemSales",
                newName: "PackageCount");

            migrationBuilder.RenameColumn(
                name: "DestinationMiscellaneousFee",
                table: "KPOldSystemSales",
                newName: "NonCashPaymentAmount");

            migrationBuilder.RenameColumn(
                name: "DeclaredGoodsValue",
                table: "KPOldSystemSales",
                newName: "ExtraFare");

            migrationBuilder.RenameColumn(
                name: "CustomerCode",
                table: "KPOldSystemSales",
                newName: "EditUserCode");

            migrationBuilder.RenameColumn(
                name: "CollectionOrSeparationFee",
                table: "KPOldSystemSales",
                newName: "EditOperatorCode");

            migrationBuilder.RenameColumn(
                name: "CancellationPenalty",
                table: "KPOldSystemSales",
                newName: "DataEntryOperatorCode");

            migrationBuilder.RenameColumn(
                name: "AddedValue",
                table: "KPOldSystemSales",
                newName: "CashPaymentAmount");

            migrationBuilder.AlterColumn<string>(
                name: "EditDate",
                table: "KPOldSystemSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountingDocNumber",
                table: "KPOldSystemSales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BillOfLadingCopyCount",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BillOfLadingCount",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BillOfLadingGroupCategory",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BillOfLadingState",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BillOfLadingType",
                table: "KPOldSystemSales",
                type: "bigint",
                nullable: true);
        }
    }
}
