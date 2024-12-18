using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class AddReimbursee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReimbursements_Users_UserId",
                table: "UserReimbursements");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserReimbursements",
                newName: "ReimburseeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements",
                newName: "IX_UserReimbursements_ReimburseeId");

            migrationBuilder.CreateTable(
                name: "Reimbursees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    BankAccountNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reimbursees", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserReimbursements_Reimbursees_ReimburseeId",
                table: "UserReimbursements",
                column: "ReimburseeId",
                principalTable: "Reimbursees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReimbursements_Reimbursees_ReimburseeId",
                table: "UserReimbursements");

            migrationBuilder.DropTable(
                name: "Reimbursees");

            migrationBuilder.RenameColumn(
                name: "ReimburseeId",
                table: "UserReimbursements",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReimbursements_ReimburseeId",
                table: "UserReimbursements",
                newName: "IX_UserReimbursements_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReimbursements_Users_UserId",
                table: "UserReimbursements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
