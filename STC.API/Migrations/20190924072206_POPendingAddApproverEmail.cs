using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class POPendingAddApproverEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POPendingItems_POPending_POPendingId",
                table: "POPendingItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_POPending",
                table: "POPending");

            migrationBuilder.RenameTable(
                name: "POPending",
                newName: "POPendings");

            migrationBuilder.RenameColumn(
                name: "Approver",
                table: "POPendings",
                newName: "ApproverName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimatedArrival",
                table: "POPendings",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "ApproverEmail",
                table: "POPendings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_POPendings",
                table: "POPendings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_POPendingItems_POPendings_POPendingId",
                table: "POPendingItems",
                column: "POPendingId",
                principalTable: "POPendings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POPendingItems_POPendings_POPendingId",
                table: "POPendingItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_POPendings",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "ApproverEmail",
                table: "POPendings");

            migrationBuilder.RenameTable(
                name: "POPendings",
                newName: "POPending");

            migrationBuilder.RenameColumn(
                name: "ApproverName",
                table: "POPending",
                newName: "Approver");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimatedArrival",
                table: "POPending",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_POPending",
                table: "POPending",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_POPendingItems_POPending_POPendingId",
                table: "POPendingItems",
                column: "POPendingId",
                principalTable: "POPending",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
