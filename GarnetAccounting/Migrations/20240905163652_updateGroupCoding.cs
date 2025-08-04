using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class updateGroupCoding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transaction_CostCenter_CostCenterId",
                table: "Wh_Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "CostCenterId",
                table: "Wh_Transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<short>(
                name: "GroupType",
                table: "Acc_Coding_Groups",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Order",
                table: "Acc_Coding_Groups",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transaction_CostCenter_CostCenterId",
                table: "Wh_Transaction",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wh_Transaction_CostCenter_CostCenterId",
                table: "Wh_Transaction");

            migrationBuilder.DropColumn(
                name: "GroupType",
                table: "Acc_Coding_Groups");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Acc_Coding_Groups");

            migrationBuilder.AlterColumn<int>(
                name: "CostCenterId",
                table: "Wh_Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wh_Transaction_CostCenter_CostCenterId",
                table: "Wh_Transaction",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
