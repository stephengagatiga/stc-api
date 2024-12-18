using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class CashReimbursementChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReimbursementPeriodEnd",
                table: "UserReimbursements");

            migrationBuilder.DropColumn(
                name: "ExpenseDate",
                table: "UserExpenses");

            migrationBuilder.RenameColumn(
                name: "ReimbursementPeriodStart",
                table: "UserReimbursements",
                newName: "ReimbursementDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReimbursementDate",
                table: "UserReimbursements",
                newName: "ReimbursementPeriodStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReimbursementPeriodEnd",
                table: "UserReimbursements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpenseDate",
                table: "UserExpenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
