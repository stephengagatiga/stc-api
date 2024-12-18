using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class ChangeDecimalPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "UserExpenses",
                type: "decimal(12, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "ReimbursementBatches",
                type: "decimal(12, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 4)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "UserExpenses",
                type: "decimal(12, 4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "ReimbursementBatches",
                type: "decimal(12, 4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 2)");
        }
    }
}
