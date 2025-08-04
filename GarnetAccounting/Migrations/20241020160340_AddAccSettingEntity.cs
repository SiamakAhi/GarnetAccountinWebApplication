using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddAccSettingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acc_Settings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    AccLevel = table.Column<short>(type: "smallint", nullable: true),
                    DocPrintDefault = table.Column<short>(type: "smallint", nullable: true),
                    ShowAllTafsil = table.Column<bool>(type: "bit", nullable: false),
                    MandatoryTafsil = table.Column<bool>(type: "bit", nullable: false),
                    PrintCreator = table.Column<bool>(type: "bit", nullable: false),
                    Approver1Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approver1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approver2Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approver2Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acc_Settings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acc_Settings");
        }
    }
}
