using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdateUserExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "UserReimbursements");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UserExpenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "UserExpenses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserExpenses");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "UserExpenses");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "UserReimbursements",
                nullable: true);
        }
    }
}
