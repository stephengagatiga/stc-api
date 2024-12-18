using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class PoPendingDicountChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Discount",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
