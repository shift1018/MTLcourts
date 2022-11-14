using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTLcourts.Data.Migrations
{
    public partial class PlayersCheckedInCourt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumCheckedIn",
                table: "Court",
                newName: "PlayersCheckedIn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayersCheckedIn",
                table: "Court",
                newName: "NumCheckedIn");
        }
    }
}
