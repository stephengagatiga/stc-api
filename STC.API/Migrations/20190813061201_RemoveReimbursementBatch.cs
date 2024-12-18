using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class RemoveReimbursementBatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReimbursements_ReimbursementBatches_ReimbursementBatchId",
                table: "UserReimbursements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReimbursements_Users_UserId",
                table: "UserReimbursements");

            migrationBuilder.DropTable(
                name: "ReimbursementBatches");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_ReimbursementBatchId",
                table: "UserReimbursements");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements");

            migrationBuilder.DropColumn(
                name: "ReimbursementBatchId",
                table: "UserReimbursements");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "UserReimbursements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessById",
                table: "UserReimbursements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "UserReimbursements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_CreatedById",
                table: "UserReimbursements",
                column: "CreatedById",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements",
                column: "ProcessById",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReimbursements_Users_CreatedById",
                table: "UserReimbursements",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReimbursements_Users_ProcessById",
                table: "UserReimbursements",
                column: "ProcessById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReimbursements_Users_UserId",
                table: "UserReimbursements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReimbursements_Users_CreatedById",
                table: "UserReimbursements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReimbursements_Users_ProcessById",
                table: "UserReimbursements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReimbursements_Users_UserId",
                table: "UserReimbursements");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_CreatedById",
                table: "UserReimbursements");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_ProcessById",
                table: "UserReimbursements");

            migrationBuilder.DropIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "UserReimbursements");

            migrationBuilder.DropColumn(
                name: "ProcessById",
                table: "UserReimbursements");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "UserReimbursements");

            migrationBuilder.AddColumn<int>(
                name: "ReimbursementBatchId",
                table: "UserReimbursements",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReimbursementBatches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RecordsCount = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReimbursementBatches", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_ReimbursementBatchId",
                table: "UserReimbursements",
                column: "ReimbursementBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReimbursements_ReimbursementBatches_ReimbursementBatchId",
                table: "UserReimbursements",
                column: "ReimbursementBatchId",
                principalTable: "ReimbursementBatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReimbursements_Users_UserId",
                table: "UserReimbursements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
