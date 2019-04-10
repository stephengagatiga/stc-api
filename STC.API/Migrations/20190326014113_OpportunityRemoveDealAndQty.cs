using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class OpportunityRemoveDealAndQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealSize",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "TotalQty",
                table: "Opportunities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DealSize",
                table: "Opportunities",
                type: "decimal(12, 4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalQty",
                table: "Opportunities",
                nullable: false,
                defaultValue: 0);
        }
    }
}
