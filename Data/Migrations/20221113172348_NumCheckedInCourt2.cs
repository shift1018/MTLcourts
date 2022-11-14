using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTLcourts.Data.Migrations
{
    public partial class NumCheckedInCourt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumCheckedIn",
                table: "Court",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumCheckedIn",
                table: "Court");
        }
    }
}
