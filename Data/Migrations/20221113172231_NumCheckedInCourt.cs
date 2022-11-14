using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTLcourts.Data.Migrations
{
    public partial class NumCheckedInCourt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkedin_AspNetUsers_UserId",
                table: "Checkedin");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Checkedin",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkedin_AspNetUsers_UserId",
                table: "Checkedin",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkedin_AspNetUsers_UserId",
                table: "Checkedin");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Checkedin",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkedin_AspNetUsers_UserId",
                table: "Checkedin",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
