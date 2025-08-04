using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class AddAppThemeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "sellerId",
                table: "Cu_BranchUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AppThemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StyleFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CssClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDark = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppThemes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppThemes",
                columns: new[] { "Id", "CssClass", "IsDark", "StyleFileName" },
                values: new object[,]
                {
                    { 1, "morph", false, "bootstrap.morph.rtl.min.css" },
                    { 2, "solar", true, "bootstrap.solar.rtl.min.css" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppThemes");

            migrationBuilder.DropColumn(
                name: "sellerId",
                table: "Cu_BranchUser");
        }
    }
}
