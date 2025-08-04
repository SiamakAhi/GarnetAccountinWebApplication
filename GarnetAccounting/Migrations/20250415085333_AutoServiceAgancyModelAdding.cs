using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AutoServiceAgancyModelAdding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asa_Contractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TafsilId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asa_Contractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asa_Contractors_Acc_Coding_Tafsils_TafsilId",
                        column: x => x.TafsilId,
                        principalTable: "Acc_Coding_Tafsils",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Asa_Receptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    ReceptionNumber = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyCode = table.Column<int>(type: "int", nullable: true),
                    ReceptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceCode = table.Column<int>(type: "int", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCampaign = table.Column<bool>(type: "bit", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    ContractoePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContractorShareAmount = table.Column<long>(type: "bigint", nullable: true),
                    ContractorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asa_Receptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asa_Receptions_Asa_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Asa_Contractors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asa_Contractors_TafsilId",
                table: "Asa_Contractors",
                column: "TafsilId");

            migrationBuilder.CreateIndex(
                name: "IX_Asa_Receptions_ContractorId",
                table: "Asa_Receptions",
                column: "ContractorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asa_Receptions");

            migrationBuilder.DropTable(
                name: "Asa_Contractors");
        }
    }
}
