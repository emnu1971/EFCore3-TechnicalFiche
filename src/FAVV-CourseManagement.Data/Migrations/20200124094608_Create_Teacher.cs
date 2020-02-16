using Microsoft.EntityFrameworkCore.Migrations;

namespace FAVV_CourseManagement.Data.Migrations
{
    public partial class Create_Teacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TeachedById",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeachedById",
                table: "Courses",
                column: "TeachedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeachedById",
                table: "Courses",
                column: "TeachedById",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeachedById",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeachedById",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeachedById",
                table: "Courses");
        }
    }
}
