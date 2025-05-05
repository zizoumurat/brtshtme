using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BritishTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CrmRecordAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrmRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SecondPhone = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DataProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesRepresentativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataSource = table.Column<int>(type: "int", nullable: false),
                    ExcludeFromCommission = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmRecords_AspNetUsers_DataProviderId",
                        column: x => x.DataProviderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrmRecords_AspNetUsers_SalesRepresentativeId",
                        column: x => x.SalesRepresentativeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrmRecords_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrmRecords_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrmRecords_BranchId",
                table: "CrmRecords",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmRecords_DataProviderId",
                table: "CrmRecords",
                column: "DataProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmRecords_RegionId",
                table: "CrmRecords",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmRecords_SalesRepresentativeId",
                table: "CrmRecords",
                column: "SalesRepresentativeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrmRecords");
        }
    }
}
