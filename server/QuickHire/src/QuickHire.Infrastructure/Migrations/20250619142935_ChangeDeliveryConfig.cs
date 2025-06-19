using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeliveryConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Messages_MessageId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Messages_MessageId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_MessageId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_MessageId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Revisions");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Deliveries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Revisions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_MessageId",
                table: "Revisions",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_MessageId",
                table: "Deliveries",
                column: "MessageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Messages_MessageId",
                table: "Deliveries",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Messages_MessageId",
                table: "Revisions",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
