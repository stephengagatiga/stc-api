using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdatePropertyOfComponentVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentVersions_Components_ComponentId1",
                table: "ComponentVersions");

            migrationBuilder.DropIndex(
                name: "IX_ComponentVersions_ComponentId1",
                table: "ComponentVersions");

            migrationBuilder.DropColumn(
                name: "ComponentId1",
                table: "ComponentVersions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Validity",
                table: "ComponentVersions",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TargetCloseMonth",
                table: "ComponentVersions",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "ComponentTypeId",
                table: "ComponentVersions",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Validity",
                table: "ComponentVersions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TargetCloseMonth",
                table: "ComponentVersions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComponentTypeId",
                table: "ComponentVersions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComponentId1",
                table: "ComponentVersions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentVersions_ComponentId1",
                table: "ComponentVersions",
                column: "ComponentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentVersions_Components_ComponentId1",
                table: "ComponentVersions",
                column: "ComponentId1",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
