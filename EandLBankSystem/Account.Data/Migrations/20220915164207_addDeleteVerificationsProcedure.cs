using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Data.Migrations
{
    public partial class addDeleteVerificationsProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"EXEC('CREATE PROCEDURE usp_DeleteExpiredEmailVerifications AS DELETE FROM EmailVerifications WHERE [ExpirationTime] <= GETDATE()')";
            migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = "DROP PROC usp_DeleteExpiredEmailVerifications";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
