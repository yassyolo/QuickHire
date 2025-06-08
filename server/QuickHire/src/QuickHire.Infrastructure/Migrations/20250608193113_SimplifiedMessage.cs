using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SimplifiedMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Deliveries_DeliveryId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Revisions_RevisionId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_DeliveryId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RevisionId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CustomOfferId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RevisionId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ParticipantBRole",
                table: "Conversations",
                newName: "ParticipantBMode");

            migrationBuilder.RenameColumn(
                name: "ParticipantARole",
                table: "Conversations",
                newName: "ParticipantAMode");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PayloadJson",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantBId",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantAId",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PayloadJson",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ParticipantBMode",
                table: "Conversations",
                newName: "ParticipantBRole");

            migrationBuilder.RenameColumn(
                name: "ParticipantAMode",
                table: "Conversations",
                newName: "ParticipantARole");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CustomOfferId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RevisionId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParticipantBId",
                table: "Conversations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ParticipantAId",
                table: "Conversations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DeliveryId",
                table: "Messages",
                column: "DeliveryId",
                unique: true,
                filter: "[DeliveryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RevisionId",
                table: "Messages",
                column: "RevisionId",
                unique: true,
                filter: "[RevisionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Deliveries_DeliveryId",
                table: "Messages",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Revisions_RevisionId",
                table: "Messages",
                column: "RevisionId",
                principalTable: "Revisions",
                principalColumn: "Id");
        }
    }
}
