using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMainCategoryToPOrtfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Buyers_BuyerId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Sellers_SellerId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_BuyerId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_SellerId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Conversations",
                newName: "ParticipantBId");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "Conversations",
                newName: "ParticipantAId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverRole",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderRole",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParticipantARole",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParticipantBRole",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_MainCategoryId",
                table: "Portfolios",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_MainCategories_MainCategoryId",
                table: "Portfolios",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_MainCategories_MainCategoryId",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_MainCategoryId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ReceiverRole",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderRole",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ParticipantARole",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "ParticipantBRole",
                table: "Conversations");

            migrationBuilder.RenameColumn(
                name: "ParticipantBId",
                table: "Conversations",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "ParticipantAId",
                table: "Conversations",
                newName: "BuyerId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Portfolios",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_BuyerId",
                table: "Conversations",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_SellerId",
                table: "Conversations",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Buyers_BuyerId",
                table: "Conversations",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Sellers_SellerId",
                table: "Conversations",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
