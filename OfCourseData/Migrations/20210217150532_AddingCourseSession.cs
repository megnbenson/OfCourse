using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OfCourseData.Migrations
{
    public partial class AddingCourseSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseSession",
                columns: table => new
                {
                    CourseSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TimeOfSession = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfSession = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSession", x => x.CourseSessionId);
                    table.ForeignKey(
                        name: "FK_CourseSession_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSession_CourseId",
                table: "CourseSession",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSession");
        }
    }
}
