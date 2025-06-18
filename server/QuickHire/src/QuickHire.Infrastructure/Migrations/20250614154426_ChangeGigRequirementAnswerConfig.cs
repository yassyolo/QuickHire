using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGigRequirementAnswerConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GigRequirementAnswers_GigRequirementId",
                table: "GigRequirementAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_GigRequirementAnswers_GigRequirementId",
                table: "GigRequirementAnswers",
                column: "GigRequirementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GigRequirementAnswers_GigRequirementId",
                table: "GigRequirementAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_GigRequirementAnswers_GigRequirementId",
                table: "GigRequirementAnswers",
                column: "GigRequirementId",
                unique: true);
        }
    }
}
