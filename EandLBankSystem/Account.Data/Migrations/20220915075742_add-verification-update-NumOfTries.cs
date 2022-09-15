using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Data.Migrations
{
    public partial class addverificationupdateNumOfTries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numOfTries",
                table: "EmailVerifications",
                newName: "NumOfTries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfTries",
                table: "EmailVerifications",
                newName: "numOfTries");
        }
    }
}
