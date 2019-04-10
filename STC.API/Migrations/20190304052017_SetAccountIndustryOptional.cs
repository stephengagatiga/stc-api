using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class SetAccountIndustryOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountIndustries_AccountIndustryId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TargetCloseMonth",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Validity",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "POC",
                table: "Components",
                newName: "Poc");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Opportunities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetCloseDate",
                table: "Components",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidityDate",
                table: "Components",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountIndustryId",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountIndustries_AccountIndustryId",
                table: "Accounts",
                column: "AccountIndustryId",
                principalTable: "AccountIndustries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountIndustries_AccountIndustryId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TargetCloseDate",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ValidityDate",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "Poc",
                table: "Components",
                newName: "POC");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Opportunities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetCloseMonth",
                table: "Components",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Validity",
                table: "Components",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "AccountIndustryId",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountIndustries_AccountIndustryId",
                table: "Accounts",
                column: "AccountIndustryId",
                principalTable: "AccountIndustries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
