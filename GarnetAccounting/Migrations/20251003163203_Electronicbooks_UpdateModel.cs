using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class Electronicbooks_UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsDeletedBy",
                table: "Acc_ElectronicBookMetaData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeletedBy",
                table: "Acc_ElectronicBookMetaData",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
