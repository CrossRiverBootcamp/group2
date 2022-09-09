using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transaction.Data.Migrations
{
    public partial class fixspellingmistake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ammount",
                table: "Transactions",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "Ammount");
        }
    }
}
