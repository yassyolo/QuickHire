using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFAQFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQs_AspNetUsers_UserId",
                table: "FAQs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FAQs",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FAQs_UserId",
                table: "FAQs",
                newName: "IX_FAQs_ApplicationUserId");

            migrationBuilder.AddColumn<bool>(
                name: "Sent",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQs_AspNetUsers_ApplicationUserId",
                table: "FAQs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQs_AspNetUsers_ApplicationUserId",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "Sent",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FAQs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FAQs_ApplicationUserId",
                table: "FAQs",
                newName: "IX_FAQs_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQs_AspNetUsers_UserId",
                table: "FAQs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
