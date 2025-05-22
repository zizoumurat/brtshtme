using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BritishTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LessonSessionEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonSessionTemplates_CourseClasses_CourseClasId",
                table: "LessonSessionTemplates");

            migrationBuilder.RenameColumn(
                name: "CourseClasId",
                table: "LessonSessionTemplates",
                newName: "CourseClassId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonSessionTemplates_CourseClasId_Day",
                table: "LessonSessionTemplates",
                newName: "IX_LessonSessionTemplates_CourseClassId_Day");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSessionTemplates_CourseClasses_CourseClassId",
                table: "LessonSessionTemplates",
                column: "CourseClassId",
                principalTable: "CourseClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonSessionTemplates_CourseClasses_CourseClassId",
                table: "LessonSessionTemplates");

            migrationBuilder.RenameColumn(
                name: "CourseClassId",
                table: "LessonSessionTemplates",
                newName: "CourseClasId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonSessionTemplates_CourseClassId_Day",
                table: "LessonSessionTemplates",
                newName: "IX_LessonSessionTemplates_CourseClasId_Day");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSessionTemplates_CourseClasses_CourseClasId",
                table: "LessonSessionTemplates",
                column: "CourseClasId",
                principalTable: "CourseClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
