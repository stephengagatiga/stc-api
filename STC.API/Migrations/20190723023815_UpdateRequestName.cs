using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdateRequestName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenAction",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TokenSubject",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "Request");

            migrationBuilder.AddColumn<int>(
                name: "RequestAction",
                table: "Request",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestSubject",
                table: "Request",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "Request",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestAction",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "RequestSubject",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "Request");

            migrationBuilder.AddColumn<int>(
                name: "TokenAction",
                table: "Request",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TokenSubject",
                table: "Request",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TokenType",
                table: "Request",
                nullable: false,
                defaultValue: 0);
        }
    }
}
