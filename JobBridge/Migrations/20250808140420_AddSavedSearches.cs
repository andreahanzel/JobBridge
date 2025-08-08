using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBridge.Migrations
{
    /// <inheritdoc />
    public partial class AddSavedSearches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedSearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobSeekerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SearchName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    JobTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EmploymentType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    MinSalary = table.Column<decimal>(type: "TEXT", nullable: true),
                    MaxSalary = table.Column<decimal>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedSearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedSearches_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedSearches_JobSeekerId",
                table: "SavedSearches",
                column: "JobSeekerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedSearches");
        }
    }
}
