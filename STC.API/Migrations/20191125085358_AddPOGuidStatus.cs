using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class AddPOGuidStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POAuditTrails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    POPendingId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POAuditTrails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POAuditTrails_POPendings_POPendingId",
                        column: x => x.POPendingId,
                        principalTable: "POPendings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_POAuditTrails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POGuidStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    POGuid = table.Column<Guid>(nullable: false),
                    POStatus = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POGuidStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POAuditTrails_POPendingId",
                table: "POAuditTrails",
                column: "POPendingId");

            migrationBuilder.CreateIndex(
                name: "IX_POAuditTrails_UserId",
                table: "POAuditTrails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POAuditTrails");

            migrationBuilder.DropTable(
                name: "POGuidStatus");
        }
    }
}
