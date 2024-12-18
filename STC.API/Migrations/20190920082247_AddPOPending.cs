using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class AddPOPending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "POPendingId",
                table: "POPendingItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "POPending",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReferenceNumber = table.Column<string>(nullable: false),
                    Supplier = table.Column<string>(nullable: false),
                    ContactPerson = table.Column<string>(nullable: false),
                    Customer = table.Column<string>(nullable: false),
                    EstimatedArrival = table.Column<DateTime>(nullable: false),
                    AccountExecutive = table.Column<string>(nullable: false),
                    Currency = table.Column<string>(nullable: false),
                    Approver = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POPending", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POPendingItems_POPendingId",
                table: "POPendingItems",
                column: "POPendingId");

            migrationBuilder.AddForeignKey(
                name: "FK_POPendingItems_POPending_POPendingId",
                table: "POPendingItems",
                column: "POPendingId",
                principalTable: "POPending",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POPendingItems_POPending_POPendingId",
                table: "POPendingItems");

            migrationBuilder.DropTable(
                name: "POPending");

            migrationBuilder.DropIndex(
                name: "IX_POPendingItems_POPendingId",
                table: "POPendingItems");

            migrationBuilder.DropColumn(
                name: "POPendingId",
                table: "POPendingItems");
        }
    }
}
