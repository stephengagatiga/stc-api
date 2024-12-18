using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class PONullableFieldsForDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountExecutiveId",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "AccountExecutiveName",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "POPendings");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "POPendings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "POPendings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "POPendings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "POPendings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ContactPersonName",
                table: "POPendings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ApproverName",
                table: "POPendings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ApproverEmail",
                table: "POPendings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "POPendingItemsJsonString",
                table: "POPendings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierAddress",
                table: "POPendings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "POPendingItemsJsonString",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "SupplierAddress",
                table: "POPendings");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPersonName",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApproverName",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApproverEmail",
                table: "POPendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountExecutiveId",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccountExecutiveName",
                table: "POPendings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
