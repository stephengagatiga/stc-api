using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UserReimbursementNullReimbursementBatchId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReimbursementBatchId",
                table: "UserReimbursements",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReimbursementBatchId",
                table: "UserReimbursements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
