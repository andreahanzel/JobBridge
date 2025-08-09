using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBridge.Migrations
{
    /// <inheritdoc />
    public partial class AddJobViewTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobPostId = table.Column<int>(type: "INTEGER", nullable: false),
                    JobSeekerId = table.Column<int>(type: "INTEGER", nullable: true),
                    ViewedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobViews_JobPosts_JobPostId",
                        column: x => x.JobPostId,
                        principalTable: "JobPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobViews_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobViews_JobPostId",
                table: "JobViews",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_JobViews_JobSeekerId",
                table: "JobViews",
                column: "JobSeekerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobViews");
        }
    }
}
