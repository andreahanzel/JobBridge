using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBridge.Migrations
{
    /// <inheritdoc />
    public partial class AddedRemainingFieldsToJobPosting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "JobPosts",
                newName: "Urgent");

            migrationBuilder.RenameColumn(
                name: "Requirements",
                table: "JobPosts",
                newName: "Timezone");

            migrationBuilder.RenameColumn(
                name: "NumberOfApplications",
                table: "JobPosts",
                newName: "NumberOfApplicants");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "JobPosts",
                newName: "RequiredSkills");

            migrationBuilder.RenameColumn(
                name: "DatePosted",
                table: "JobPosts",
                newName: "RequiredQualifications");

            migrationBuilder.RenameColumn(
                name: "ApplicationLink",
                table: "JobPosts",
                newName: "PreferredQualifications");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalCompensation",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDeadline",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationMethod",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExternalApplicationUrl",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Featured",
                table: "JobPosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JobSummary",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KeyResponsibilities",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumSalary",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumSalary",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NiceToHaveSkills",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalCompensation",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ApplicationDeadline",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ApplicationMethod",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ExternalApplicationUrl",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "Featured",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "JobSummary",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "KeyResponsibilities",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "MaximumSalary",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "MinimumSalary",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "NiceToHaveSkills",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "JobPosts");

            migrationBuilder.RenameColumn(
                name: "Urgent",
                table: "JobPosts",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "Timezone",
                table: "JobPosts",
                newName: "Requirements");

            migrationBuilder.RenameColumn(
                name: "RequiredSkills",
                table: "JobPosts",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "RequiredQualifications",
                table: "JobPosts",
                newName: "DatePosted");

            migrationBuilder.RenameColumn(
                name: "PreferredQualifications",
                table: "JobPosts",
                newName: "ApplicationLink");

            migrationBuilder.RenameColumn(
                name: "NumberOfApplicants",
                table: "JobPosts",
                newName: "NumberOfApplications");
        }
    }
}
