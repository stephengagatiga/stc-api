using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class AddRequestorNamePOData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "POData",
                table: "POGuidStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestorEmail",
                table: "POGuidStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestorName",
                table: "POGuidStatus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "POData",
                table: "POGuidStatus");

            migrationBuilder.DropColumn(
                name: "RequestorEmail",
                table: "POGuidStatus");

            migrationBuilder.DropColumn(
                name: "RequestorName",
                table: "POGuidStatus");
        }
    }
}
