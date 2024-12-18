using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenId",
                table: "Opportunities",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "TokenId",
                table: "Components",
                newName: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Opportunities",
                newName: "TokenId");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Components",
                newName: "TokenId");
        }
    }
}
