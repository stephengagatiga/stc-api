using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdateUserReimbursementRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_CreatedById",
                table: "UserReimbursements");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements");

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_CreatedById",
                table: "UserReimbursements",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements",
                column: "ProcessById");

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_CreatedById",
                table: "UserReimbursements");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements");

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_CreatedById",
                table: "UserReimbursements",
                column: "CreatedById",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements",
                column: "ProcessById",
                unique: true,
                filter: "[ProcessById] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements",
                column: "UserId",
                unique: true);
        }
    }
}
