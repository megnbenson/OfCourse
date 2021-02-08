using Microsoft.EntityFrameworkCore.Migrations;

namespace OfCourseData.Migrations
{
    public partial class RemovedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionLength",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "SessionLengthMinutes",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionLengthMinutes",
                table: "Courses");

            migrationBuilder.AddColumn<float>(
                name: "SessionLength",
                table: "Courses",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
