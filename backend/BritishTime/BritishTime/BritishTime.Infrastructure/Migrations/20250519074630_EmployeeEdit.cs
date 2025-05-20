using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BritishTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone3",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ApplyOvertime",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OvertimeHourlyRate",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OvertimeQuota",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SpecialLessonHourlyRate",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyOvertime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OvertimeHourlyRate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OvertimeQuota",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SpecialLessonHourlyRate",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone3",
                table: "Employees",
                type: "nvarchar(20)",
                nullable: true);
        }
    }
}
