using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class POPendingColumnsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Supplier",
                table: "POPendings",
                newName: "SupplierName");

            migrationBuilder.RenameColumn(
                name: "Customer",
                table: "POPendings",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "ContactPerson",
                table: "POPendings",
                newName: "ContactPersonName");

            migrationBuilder.RenameColumn(
                name: "AccountExecutive",
                table: "POPendings",
                newName: "AccountExecutiveName");

            migrationBuilder.AddColumn<int>(
                name: "AccountExecutiveId",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContactPersonId",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountExecutiveId",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "ContactPersonId",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "POPendings");

            migrationBuilder.RenameColumn(
                name: "SupplierName",
                table: "POPendings",
                newName: "Supplier");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "POPendings",
                newName: "Customer");

            migrationBuilder.RenameColumn(
                name: "ContactPersonName",
                table: "POPendings",
                newName: "ContactPerson");

            migrationBuilder.RenameColumn(
                name: "AccountExecutiveName",
                table: "POPendings",
                newName: "AccountExecutive");
        }
    }
}
