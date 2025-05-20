using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BritishTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ClassType = table.Column<int>(type: "int", nullable: false),
                    EducationType = table.Column<int>(type: "int", nullable: false),
                    ScheduleType = table.Column<int>(type: "int", nullable: false),
                    LessonScheduleDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseClasses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseClasses_ClassRooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "ClassRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseClasses_LessonScheduleDefinitions_LessonScheduleDefinitionId",
                        column: x => x.LessonScheduleDefinitionId,
                        principalTable: "LessonScheduleDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseClasses_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_BranchId",
                table: "CourseClasses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_ClassroomId",
                table: "CourseClasses",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_LessonScheduleDefinitionId",
                table: "CourseClasses",
                column: "LessonScheduleDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_LevelId",
                table: "CourseClasses",
                column: "LevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseClasses");
        }
    }
}
