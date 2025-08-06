using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBridge.Migrations
{
    /// <inheritdoc />
    public partial class AddJobPostColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename Title column to JobTitle
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "JobPosts",
                newName: "JobTitle");

            // Add missing columns
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExperienceLevel",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkArrangement",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove added columns
            migrationBuilder.DropColumn(
                name: "Department",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ExperienceLevel",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "WorkArrangement",
                table: "JobPosts");

            // Rename JobTitle back to Title
            migrationBuilder.RenameColumn(
                name: "JobTitle",
                table: "JobPosts",
                newName: "Title");
        }
    }
}
