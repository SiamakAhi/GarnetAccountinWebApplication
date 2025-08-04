using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cu_ConsignmentNatures_Cu_RateImpactTypes_RateImpactTypeId",
                table: "Cu_ConsignmentNatures");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Acc_Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Con_Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployerId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectAmount = table.Column<long>(type: "bigint", nullable: true),
                    ContractDurationDays = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Con_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Con_Projects_parties_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "parties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acc_Articles_ProjectId",
                table: "Acc_Articles",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Con_Projects_EmployerId",
                table: "Con_Projects",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acc_Articles_Con_Projects_ProjectId",
                table: "Acc_Articles",
                column: "ProjectId",
                principalTable: "Con_Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_ConsignmentNatures_Cu_RateImpactTypes_RateImpactTypeId",
                table: "Cu_ConsignmentNatures",
                column: "RateImpactTypeId",
                principalTable: "Cu_RateImpactTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acc_Articles_Con_Projects_ProjectId",
                table: "Acc_Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Cu_ConsignmentNatures_Cu_RateImpactTypes_RateImpactTypeId",
                table: "Cu_ConsignmentNatures");

            migrationBuilder.DropTable(
                name: "Con_Projects");

            migrationBuilder.DropIndex(
                name: "IX_Acc_Articles_ProjectId",
                table: "Acc_Articles");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Acc_Articles");

            migrationBuilder.AddForeignKey(
                name: "FK_Cu_ConsignmentNatures_Cu_RateImpactTypes_RateImpactTypeId",
                table: "Cu_ConsignmentNatures",
                column: "RateImpactTypeId",
                principalTable: "Cu_RateImpactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
