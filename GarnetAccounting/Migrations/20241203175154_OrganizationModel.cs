using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountantRemark",
                table: "Acc_Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Acc_Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewDate",
                table: "Acc_Articles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrgDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentDepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgDepartments_OrgDepartments_ParentDepartmentId",
                        column: x => x.ParentDepartmentId,
                        principalTable: "OrgDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrgPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgPositions_OrgDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "OrgDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrgEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    RelationshipType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgEmployees_OrgEmployees_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "OrgEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrgEmployees_OrgPositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "OrgPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrgDepartments_ParentDepartmentId",
                table: "OrgDepartments",
                column: "ParentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgEmployees_PositionId",
                table: "OrgEmployees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgEmployees_SupervisorId",
                table: "OrgEmployees",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgPositions_DepartmentId",
                table: "OrgPositions",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_parties_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                principalTable: "parties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_parties_PersonId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OrgEmployees");

            migrationBuilder.DropTable(
                name: "OrgPositions");

            migrationBuilder.DropTable(
                name: "OrgDepartments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccountantRemark",
                table: "Acc_Articles");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Acc_Articles");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "Acc_Articles");
        }
    }
}
