using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BritishTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CrmRecordEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrmRecords_AspNetUsers_DataProviderId",
                table: "CrmRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_CrmRecords_AspNetUsers_SalesRepresentativeId",
                table: "CrmRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_CrmRecords_Employees_DataProviderId",
                table: "CrmRecords",
                column: "DataProviderId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrmRecords_Employees_SalesRepresentativeId",
                table: "CrmRecords",
                column: "SalesRepresentativeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrmRecords_Employees_DataProviderId",
                table: "CrmRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_CrmRecords_Employees_SalesRepresentativeId",
                table: "CrmRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_CrmRecords_AspNetUsers_DataProviderId",
                table: "CrmRecords",
                column: "DataProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrmRecords_AspNetUsers_SalesRepresentativeId",
                table: "CrmRecords",
                column: "SalesRepresentativeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
