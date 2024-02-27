using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class bet_column_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "master",
                table: "Bets",
                newName: "WinAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "BetAmount",
                schema: "master",
                table: "Bets",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetAmount",
                schema: "master",
                table: "Bets");

            migrationBuilder.RenameColumn(
                name: "WinAmount",
                schema: "master",
                table: "Bets",
                newName: "Amount");
        }
    }
}
