using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFileFromGigRequirementAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFileUpload",
                table: "GigRequirements");

            migrationBuilder.DropColumn(
                name: "AttachmentUrls",
                table: "GigRequirementAnswers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFileUpload",
                table: "GigRequirements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentUrls",
                table: "GigRequirementAnswers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
