using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Data.Migrations
{
    public partial class initSendEmailTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SendEmailTracks",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumOfTries = table.Column<int>(type: "int", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlockExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeverityLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendEmailTracks", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendEmailTracks");
        }
    }
}
