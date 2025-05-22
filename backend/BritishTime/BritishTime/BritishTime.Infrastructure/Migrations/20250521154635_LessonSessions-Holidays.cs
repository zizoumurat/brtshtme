using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BritishTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LessonSessionsHolidays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseClasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonSessions_CourseClasses_CourseClasId",
                        column: x => x.CourseClasId,
                        principalTable: "CourseClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonSessions_Employees_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonSessionTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseClasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonSessionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonSessionTemplates_CourseClasses_CourseClasId",
                        column: x => x.CourseClasId,
                        principalTable: "CourseClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonSessionTemplates_Employees_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_Date_BranchId_CourseClassId",
                table: "Holidays",
                columns: new[] { "Date", "BranchId", "CourseClassId" },
                unique: true,
                filter: "[BranchId] IS NOT NULL AND [CourseClassId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LessonSessions_CourseClasId_Date_StartTime",
                table: "LessonSessions",
                columns: new[] { "CourseClasId", "Date", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_LessonSessions_TeacherId",
                table: "LessonSessions",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonSessionTemplates_CourseClasId_Day",
                table: "LessonSessionTemplates",
                columns: new[] { "CourseClasId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonSessionTemplates_TeacherId",
                table: "LessonSessionTemplates",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "LessonSessions");

            migrationBuilder.DropTable(
                name: "LessonSessionTemplates");
        }
    }
}
