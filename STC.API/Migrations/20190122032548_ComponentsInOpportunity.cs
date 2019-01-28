using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class ComponentsInOpportunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpportunityId1",
                table: "Components",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_OpportunityId1",
                table: "Components",
                column: "OpportunityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Opportunities_OpportunityId1",
                table: "Components",
                column: "OpportunityId1",
                principalTable: "Opportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Opportunities_OpportunityId1",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_OpportunityId1",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "OpportunityId1",
                table: "Components");
        }
    }
}
