using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTLcourts.Data.Migrations
{
    public partial class MigrationsAnton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Court",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Court",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Court",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Court",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CourtsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checkedin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourtsId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumCheckedIn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkedin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkedin_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkedin_Court_CourtsId",
                        column: x => x.CourtsId,
                        principalTable: "Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateWhen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Court_CourtsId",
                        column: x => x.CourtsId,
                        principalTable: "Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Intentions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumOfPeople = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intentions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intentions_Court_CourtsId",
                        column: x => x.CourtsId,
                        principalTable: "Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Court_CourtsId",
                        column: x => x.CourtsId,
                        principalTable: "Court",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CourtsId",
                table: "AspNetUsers",
                column: "CourtsId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkedin_CourtsId",
                table: "Checkedin",
                column: "CourtsId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkedin_UserId",
                table: "Checkedin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CourtsId",
                table: "Comments",
                column: "CourtsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Intentions_CourtsId",
                table: "Intentions",
                column: "CourtsId");

            migrationBuilder.CreateIndex(
                name: "IX_Intentions_UserId",
                table: "Intentions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CourtsId",
                table: "Ratings",
                column: "CourtsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Court_CourtsId",
                table: "AspNetUsers",
                column: "CourtsId",
                principalTable: "Court",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Court_CourtsId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Checkedin");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Intentions");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CourtsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourtsId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Court",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Court",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Court",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Court",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
