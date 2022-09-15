using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Data.Migrations
{
    public partial class startDeleteExpiredReocordJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dropJobSql = @"USE msdb;
GO
EXEC dbo.sp_add_jobserver  
    @job_name = N'DeleteExpiredRecords' ; 
EXEC dbo.sp_start_job N'DeleteExpiredRecords'
GO
USE [EandL.Account]";
            migrationBuilder.Sql(dropJobSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropJobSql = @"USE msdb;
GO
EXEC sp_stop_job @job_name = N'DeleteExpiredRecords'
GO USE [EandL.Account]";
            migrationBuilder.Sql(dropJobSql);
        }
    }
}
