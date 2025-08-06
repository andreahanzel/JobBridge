using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBridge.Migrations
{
    /// <inheritdoc />
    public partial class updatedJobPostFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Urgent",
                table: "JobPosts",
                newName: "IsUrgent");

            migrationBuilder.RenameColumn(
                name: "PostDate",
                table: "JobPosts",
                newName: "PostedDate");

            migrationBuilder.RenameColumn(
                name: "Featured",
                table: "JobPosts",
                newName: "IsFeatured");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "JobPosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "JobPosts");

            migrationBuilder.RenameColumn(
                name: "PostedDate",
                table: "JobPosts",
                newName: "PostDate");

            migrationBuilder.RenameColumn(
                name: "IsUrgent",
                table: "JobPosts",
                newName: "Urgent");

            migrationBuilder.RenameColumn(
                name: "IsFeatured",
                table: "JobPosts",
                newName: "Featured");
        }
    }
}
