using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UserReimbursementSetSomePropertyNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessById",
                table: "UserReimbursements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessOn",
                table: "UserReimbursements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "UserReimbursements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements",
                column: "ProcessById",
                unique: true,
                filter: "[ProcessById] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements");

            migrationBuilder.DropColumn(
                name: "ProcessOn",
                table: "UserReimbursements");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "UserReimbursements");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessById",
                table: "UserReimbursements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements",
                column: "ProcessById",
                unique: true);
        }
    }
}
