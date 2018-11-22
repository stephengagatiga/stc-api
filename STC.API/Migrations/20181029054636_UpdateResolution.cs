using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdateResolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketResolutions");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedOn",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resolution",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RootCause",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RootCause",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "TicketResolutions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cause = table.Column<string>(nullable: true),
                    Closed = table.Column<bool>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Solution = table.Column<string>(nullable: true),
                    TicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketResolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketResolutions_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketResolutions_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketResolutions_CreatedById",
                table: "TicketResolutions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketResolutions_TicketId",
                table: "TicketResolutions",
                column: "TicketId");
        }
    }
}
