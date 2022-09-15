using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Data.Migrations
{
    public partial class startDeleteExpiredRecordsjob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var startJobSql = @"USE msdb;
GO
EXEC sp_update_jobstep   
@job_name = 'DeleteExpiredRecords',
@step_id = 1,
@database_name = N'EandL.Account'

EXEC dbo.sp_start_job N'DeleteExpiredRecords'
GO
USE [EandL.Account]";
            migrationBuilder.Sql(startJobSql);
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
