using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReceptionModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asa_Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceName = table.Column<long>(type: "bigint", nullable: false),
                    MoeinId = table.Column<int>(type: "int", nullable: false),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asa_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Asa_Settings",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    SaleMoeinId = table.Column<int>(type: "int", nullable: false),
                    SaleFreeTafsilId = table.Column<long>(type: "bigint", nullable: true),
                    SaleWarrantyTfsilId = table.Column<long>(type: "bigint", nullable: true),
                    NonCommercialCreditorMoeinId = table.Column<int>(type: "int", nullable: false),
                    CommercialDebtorMoeinId = table.Column<int>(type: "int", nullable: false),
                    SaleVatMoeinId = table.Column<int>(type: "int", nullable: false),
                    FreeTafsiltId = table.Column<long>(type: "bigint", nullable: true),
                    WarrantyTafsilId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asa_Settings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asa_Services");

            migrationBuilder.DropTable(
                name: "Asa_Settings");
        }
    }
}
