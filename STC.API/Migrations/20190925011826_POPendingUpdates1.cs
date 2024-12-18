using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class POPendingUpdates1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "POPendings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EstimatedArrivalString",
                table: "POPendings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternalNote",
                table: "POPendings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "POPendings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestorEmail",
                table: "POPendings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "POPendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "POPendings",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "EstimatedArrivalString",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "InternalNote",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "RequestorEmail",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "POPendings");
        }
    }
}
