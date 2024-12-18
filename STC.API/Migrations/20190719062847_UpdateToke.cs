using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdateToke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TakeActionOn",
                table: "Tokens",
                newName: "ImplementedOn");

            migrationBuilder.AddColumn<int>(
                name: "ImplementedBy",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImplementedBy",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "ImplementedOn",
                table: "Tokens",
                newName: "TakeActionOn");
        }
    }
}
