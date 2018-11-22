using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdateAccountEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountIndustryId",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TermsOfPayment",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "AccountContacts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrimaryContact",
                table: "AccountContacts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AccountIndustries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountIndustries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountIndustryId",
                table: "Accounts",
                column: "AccountIndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountIndustries_AccountIndustryId",
                table: "Accounts",
                column: "AccountIndustryId",
                principalTable: "AccountIndustries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountIndustries_AccountIndustryId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountIndustries");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountIndustryId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountIndustryId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TermsOfPayment",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "AccountContacts");

            migrationBuilder.DropColumn(
                name: "PrimaryContact",
                table: "AccountContacts");
        }
    }
}
