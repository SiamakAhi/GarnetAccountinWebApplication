using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class Electronicbooks_AddToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "eBookFileId",
                table: "Acc_Documents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "eBookFileId",
                table: "Acc_Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Acc_ElectronicBookMetaData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinDocNumber = table.Column<int>(type: "int", nullable: false),
                    MaxDocNumber = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDeletedBy = table.Column<bool>(type: "bit", nullable: false),
                    IsDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_ElectronicBookMetaData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acc_ElectronicBookMetaData");

            migrationBuilder.DropColumn(
                name: "eBookFileId",
                table: "Acc_Documents");

            migrationBuilder.DropColumn(
                name: "eBookFileId",
                table: "Acc_Articles");
        }
    }
}
