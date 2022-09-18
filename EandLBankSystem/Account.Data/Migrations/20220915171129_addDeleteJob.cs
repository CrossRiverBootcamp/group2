using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Data.Migrations
{
    public partial class addDeleteJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createJobSql = @"USE msdb;

EXEC dbo.sp_add_job
    @job_name = 'DeleteExpiredRecords'

EXEC sp_add_jobstep
    @job_name = 'DeleteExpiredRecords',
    @step_name = N'process step',
    @subsystem = N'TSQL',
    @command = 'usp_DeleteExpiredEmailVerifications'  

declare @start_date varchar(8) = convert(varchar(8), GETDATE(), 112)

EXEC sp_add_jobschedule
@job_name = 'DeleteExpiredRecords',
@name = 'process schedule',
@freq_type=4,
@freq_interval = 1,
@active_start_date = @start_date,
@active_start_time = '000000'

EXEC dbo.sp_add_jobserver
    @job_name = N'DeleteExpiredRecords' ;
USE [EandL.Account];";

            migrationBuilder.Sql(createJobSql);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropJobSql = @"EXEC(' USE msdb; GO EXEC sp_delete_job @job_name = N'DeleteExpiredRecords'; GO ')";
            migrationBuilder.Sql(dropJobSql);
        }
    }
}
