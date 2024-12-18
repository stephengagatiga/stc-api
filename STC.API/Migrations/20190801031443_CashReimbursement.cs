using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class CashReimbursement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReimbursementBatches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RecordsCount = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12, 4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReimbursementBatches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserReimbursements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ReimbursementBatchId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ReimbursementPeriodStart = table.Column<DateTime>(nullable: false),
                    ReimbursementPeriodEnd = table.Column<DateTime>(nullable: false),
                    ReimbursementStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReimbursements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReimbursements_ReimbursementBatches_ReimbursementBatchId",
                        column: x => x.ReimbursementBatchId,
                        principalTable: "ReimbursementBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserReimbursements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserReimbursementId = table.Column<int>(nullable: false),
                    ExepenseId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(12, 4)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExpenses_Expense_ExepenseId",
                        column: x => x.ExepenseId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExpenses_UserReimbursements_UserReimbursementId",
                        column: x => x.UserReimbursementId,
                        principalTable: "UserReimbursements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExpenses_ExepenseId",
                table: "UserExpenses",
                column: "ExepenseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExpenses_UserReimbursementId",
                table: "UserExpenses",
                column: "UserReimbursementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_ReimbursementBatchId",
                table: "UserReimbursements",
                column: "ReimbursementBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReimbursements_UserId",
                table: "UserReimbursements",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExpenses");

            migrationBuilder.DropTable(
                name: "UserReimbursements");

            migrationBuilder.DropTable(
                name: "ReimbursementBatches");
        }
    }
}
