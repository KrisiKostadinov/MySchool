using Microsoft.EntityFrameworkCore.Migrations;

namespace MySchool.Data.Migrations
{
    public partial class @int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MySchoolUserId",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_MySchoolUserId",
                table: "AspNetRoles",
                column: "MySchoolUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_MySchoolUserId",
                table: "AspNetRoles",
                column: "MySchoolUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_MySchoolUserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_MySchoolUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "MySchoolUserId",
                table: "AspNetRoles");
        }
    }
}
