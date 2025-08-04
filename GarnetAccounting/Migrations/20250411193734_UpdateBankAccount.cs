using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBankAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MoeinId",
                table: "BankAccounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_MoeinId",
                table: "BankAccounts",
                column: "MoeinId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Acc_Coding_Moeins_MoeinId",
                table: "BankAccounts",
                column: "MoeinId",
                principalTable: "Acc_Coding_Moeins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Acc_Coding_Moeins_MoeinId",
                table: "BankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_MoeinId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "MoeinId",
                table: "BankAccounts");
        }
    }
}
