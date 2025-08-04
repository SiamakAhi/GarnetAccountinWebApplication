using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarnetAccounting.Migrations
{
    /// <inheritdoc />
    public partial class Update14040406 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                table: "TreBankTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BankTransactionId",
                table: "Acc_Articles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                table: "Acc_Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MoadianId",
                table: "Acc_Articles",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "TreBankTransactions");

            migrationBuilder.DropColumn(
                name: "BankTransactionId",
                table: "Acc_Articles");

            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "Acc_Articles");

            migrationBuilder.DropColumn(
                name: "MoadianId",
                table: "Acc_Articles");
        }
    }
}
