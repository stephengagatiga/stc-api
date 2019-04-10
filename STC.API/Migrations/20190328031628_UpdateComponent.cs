using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdateComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Components_AccountExecutiveId",
                table: "Components");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentTypeId",
                table: "Components",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Components_AccountExecutiveId",
                table: "Components",
                column: "AccountExecutiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Components_AccountExecutiveId",
                table: "Components");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentTypeId",
                table: "Components",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_AccountExecutiveId",
                table: "Components",
                column: "AccountExecutiveId",
                unique: true);
        }
    }
}
