using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMessageFromCustomOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomOffers_Messages_MessageId",
                table: "CustomOffers");

            migrationBuilder.DropIndex(
                name: "IX_CustomOffers_MessageId",
                table: "CustomOffers");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "CustomOffers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "CustomOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomOffers_MessageId",
                table: "CustomOffers",
                column: "MessageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomOffers_Messages_MessageId",
                table: "CustomOffers",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
