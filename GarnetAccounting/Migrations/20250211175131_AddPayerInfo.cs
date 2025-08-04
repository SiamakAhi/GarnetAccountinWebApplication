using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddPayerInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxpayerInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisteredAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanySubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeclarationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEOName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEONationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEOPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEOAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember1NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember1Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember1Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember2Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember2NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember2Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember2Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember3Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember3NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember3Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardMember3Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxUnitCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxOfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxGroupHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxAuditor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeniorTaxAuditor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiefTaxAuditor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreRegistrationTrackingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxFileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueTaxMemoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetPanelPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingSoftwareName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingSoftwarePassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterfaceSoftwareName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterfaceSoftwarePassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CFOName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CFOMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinancialAdvisorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinancialAdvisorMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountantMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditFirmName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxpayerInfos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxpayerInfos");
        }
    }
}
