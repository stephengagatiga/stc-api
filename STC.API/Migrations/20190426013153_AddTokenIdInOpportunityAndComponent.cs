using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class AddTokenIdInOpportunityAndComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TokenId",
                table: "Opportunities",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TokenId",
                table: "Components",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Components");
        }
    }
}
