using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeChangesToConversations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Gigs");

            migrationBuilder.RenameColumn(
                name: "IsStarredBySeller",
                table: "Conversations",
                newName: "IsStarredByParticipantB");

            migrationBuilder.RenameColumn(
                name: "IsStarredByBuyer",
                table: "Conversations",
                newName: "IsStarredByParticipantA");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "IsStarredByParticipantB",
                table: "Conversations",
                newName: "IsStarredBySeller");

            migrationBuilder.RenameColumn(
                name: "IsStarredByParticipantA",
                table: "Conversations",
                newName: "IsStarredByBuyer");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Gigs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
