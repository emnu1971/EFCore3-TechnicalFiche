using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FAVV_CourseManagement.Data.Migrations
{
    public partial class Create_Disenrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disenrollment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disenrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disenrollment_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disenrollment_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disenrollment_CourseId",
                table: "Disenrollment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Disenrollment_StudentId",
                table: "Disenrollment",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disenrollment");
        }
    }
}
