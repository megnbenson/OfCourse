using Microsoft.EntityFrameworkCore.Migrations;

namespace OfCourseData.Migrations
{
    public partial class ChangedCustomerBookedCoursesToSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCustomer");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CourseSessionDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CourseId",
                table: "Customers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSessionDetails_CustomerId",
                table: "CourseSessionDetails",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSessionDetails_Customers_CustomerId",
                table: "CourseSessionDetails",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Courses_CourseId",
                table: "Customers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSessionDetails_Customers_CustomerId",
                table: "CourseSessionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Courses_CourseId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CourseId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CourseSessionDetails_CustomerId",
                table: "CourseSessionDetails");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CourseSessionDetails");

            migrationBuilder.CreateTable(
                name: "CourseCustomer",
                columns: table => new
                {
                    BookedCoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    BookedCustomersCustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCustomer", x => new { x.BookedCoursesCourseId, x.BookedCustomersCustomerId });
                    table.ForeignKey(
                        name: "FK_CourseCustomer_Courses_BookedCoursesCourseId",
                        column: x => x.BookedCoursesCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCustomer_Customers_BookedCustomersCustomerId",
                        column: x => x.BookedCustomersCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCustomer_BookedCustomersCustomerId",
                table: "CourseCustomer",
                column: "BookedCustomersCustomerId");
        }
    }
}
