using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdatePOGuidStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "ApproverJobTitle",
                table: "POPendings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestorName",
                table: "POPendings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "POPendingItems",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "POPendingItems",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "SendTOs",
                table: "POGuidStatus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverJobTitle",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "RequestorName",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "SendTOs",
                table: "POGuidStatus");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "POPendingItems",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "POPendingItems",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
