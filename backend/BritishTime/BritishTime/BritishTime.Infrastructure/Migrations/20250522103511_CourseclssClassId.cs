using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BritishTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseclssClassId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonSessions_CourseClasses_CourseClasId",
                table: "LessonSessions");

            migrationBuilder.RenameColumn(
                name: "CourseClasId",
                table: "LessonSessions",
                newName: "CourseClassId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonSessions_CourseClasId_Date_StartTime",
                table: "LessonSessions",
                newName: "IX_LessonSessions_CourseClassId_Date_StartTime");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSessions_CourseClasses_CourseClassId",
                table: "LessonSessions",
                column: "CourseClassId",
                principalTable: "CourseClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonSessions_CourseClasses_CourseClassId",
                table: "LessonSessions");

            migrationBuilder.RenameColumn(
                name: "CourseClassId",
                table: "LessonSessions",
                newName: "CourseClasId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonSessions_CourseClassId_Date_StartTime",
                table: "LessonSessions",
                newName: "IX_LessonSessions_CourseClasId_Date_StartTime");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSessions_CourseClasses_CourseClasId",
                table: "LessonSessions",
                column: "CourseClasId",
                principalTable: "CourseClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
