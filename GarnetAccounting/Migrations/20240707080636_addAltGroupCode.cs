using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class addAltGroupCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AltGroupCode",
                table: "Acc_Coding_Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)4,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)5,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)6,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)7,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)8,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)9,
                column: "AltGroupCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Acc_Coding_Groups",
                keyColumn: "Id",
                keyValue: (short)10,
                column: "AltGroupCode",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltGroupCode",
                table: "Acc_Coding_Groups");
        }
    }
}
