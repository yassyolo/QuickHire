using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBillingDetailsUserIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrowsingHistories_Gigs_GigId",
                table: "BrowsingHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Buyers_BillingDetails_BillingDetailsId",
                table: "Buyers");

            migrationBuilder.DropIndex(
                name: "IX_Buyers_BillingDetailsId",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "BillingDetailsId",
                table: "Buyers");

            migrationBuilder.AlterColumn<int>(
                name: "GigId",
                table: "BrowsingHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "BrowsingHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BillingDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BillingDetailsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrowsingHistories_SellerId",
                table: "BrowsingHistories",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BillingDetailsId",
                table: "AspNetUsers",
                column: "BillingDetailsId",
                unique: true,
                filter: "[BillingDetailsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BillingDetails_BillingDetailsId",
                table: "AspNetUsers",
                column: "BillingDetailsId",
                principalTable: "BillingDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BrowsingHistories_Gigs_GigId",
                table: "BrowsingHistories",
                column: "GigId",
                principalTable: "Gigs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BrowsingHistories_Sellers_SellerId",
                table: "BrowsingHistories",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BillingDetails_BillingDetailsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BrowsingHistories_Gigs_GigId",
                table: "BrowsingHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_BrowsingHistories_Sellers_SellerId",
                table: "BrowsingHistories");

            migrationBuilder.DropIndex(
                name: "IX_BrowsingHistories_SellerId",
                table: "BrowsingHistories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BillingDetailsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "BrowsingHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "BillingDetailsId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "BillingDetailsId",
                table: "Buyers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GigId",
                table: "BrowsingHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_BillingDetailsId",
                table: "Buyers",
                column: "BillingDetailsId",
                unique: true,
                filter: "[BillingDetailsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BrowsingHistories_Gigs_GigId",
                table: "BrowsingHistories",
                column: "GigId",
                principalTable: "Gigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buyers_BillingDetails_BillingDetailsId",
                table: "Buyers",
                column: "BillingDetailsId",
                principalTable: "BillingDetails",
                principalColumn: "Id");
        }
    }
}
