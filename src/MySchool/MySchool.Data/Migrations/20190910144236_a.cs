using Microsoft.EntityFrameworkCore.Migrations;

namespace MySchool.Data.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "BirthMonth",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BirthMonth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BirthDay",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BirthMonth",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BirthYear",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BirthDay",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BirthMonth",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BirthYear",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }
    }
}
