using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STC.API.Migrations
{
    public partial class UpdatePOAttachmentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POAttachment");

            migrationBuilder.CreateTable(
                name: "POGuidStatusAttachments",
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
                    table.PrimaryKey("PK_POGuidStatusAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POGuidStatusAttachments_POGuidStatus_POGuidStatusId",
                        column: x => x.POGuidStatusId,
                        principalTable: "POGuidStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POGuidStatusAttachments_POGuidStatusId",
                table: "POGuidStatusAttachments",
                column: "POGuidStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POGuidStatusAttachments");

            migrationBuilder.CreateTable(
                name: "POAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentType = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    File = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    POGuidStatusId = table.Column<int>(nullable: false),
                    Size = table.Column<long>(nullable: false)
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
    }
}
