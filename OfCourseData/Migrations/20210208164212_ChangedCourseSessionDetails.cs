using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OfCourseData.Migrations
{
    public partial class ChangedCourseSessionDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Courses_CourseId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DateOfSession",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TimeOfSession",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseSessionDetails",
                columns: table => new
                {
                    CourseSessionDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TimeOfSession = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfSession = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSessionDetails", x => x.CourseSessionDetailsId);
                    table.ForeignKey(
                        name: "FK_CourseSessionDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSessionDetails_CourseId",
                table: "CourseSessionDetails",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSessionDetails");

            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfSession",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOfSession",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseId1",
                table: "Courses",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Courses_CourseId1",
                table: "Courses",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
