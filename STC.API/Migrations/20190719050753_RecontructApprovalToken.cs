using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class RecontructApprovalToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentVersions_Components_ComponentId",
                table: "ComponentVersions");

            migrationBuilder.DropIndex(
                name: "IX_ComponentVersions_ComponentId",
                table: "ComponentVersions");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Tokens");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Tokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TakeActionOn",
                table: "Tokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TokenAction",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TokenSubject",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TokenType",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComponentId1",
                table: "ComponentVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComponentVersionId",
                table: "Components",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentVersions_Components_ComponentId1",
                table: "ComponentVersions");

            migrationBuilder.DropIndex(
                name: "IX_ComponentVersions_ComponentId1",
                table: "ComponentVersions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "TakeActionOn",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "TokenAction",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "TokenSubject",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ComponentId1",
                table: "ComponentVersions");

            migrationBuilder.DropColumn(
                name: "ComponentVersionId",
                table: "Components");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Tokens",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentVersions_ComponentId",
                table: "ComponentVersions",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentVersions_Components_ComponentId",
                table: "ComponentVersions",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
