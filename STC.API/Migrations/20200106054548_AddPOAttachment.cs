using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class AddPOAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                table: "POPendings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledOn",
                table: "POPendings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenApproved",
                table: "POPendings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedOn",
                table: "POPendings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextLineBreakCount",
                table: "POPendings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "POAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    POGuidStatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    File = table.Column<byte[]>(nullable: false),
                    Size = table.Column<long>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POAttachment_POGuidStatus_POGuidStatusId",
                        column: x => x.POGuidStatusId,
                        principalTable: "POGuidStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POAttachment_POGuidStatusId",
                table: "POAttachment",
                column: "POGuidStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POAttachment");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "CancelledOn",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "HasBeenApproved",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "ReceivedOn",
                table: "POPendings");

            migrationBuilder.DropColumn(
                name: "TextLineBreakCount",
                table: "POPendings");
        }
    }
}
