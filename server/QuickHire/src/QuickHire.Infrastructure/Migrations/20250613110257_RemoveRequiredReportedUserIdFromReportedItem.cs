using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredReportedUserIdFromReportedItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReportedItems_GigId",
                table: "ReportedItems");

            migrationBuilder.AlterColumn<string>(
                name: "ReportedUserId",
                table: "ReportedItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedItems_GigId",
                table: "ReportedItems",
                column: "GigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReportedItems_GigId",
                table: "ReportedItems");

            migrationBuilder.AlterColumn<string>(
                name: "ReportedUserId",
                table: "ReportedItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportedItems_GigId",
                table: "ReportedItems",
                column: "GigId",
                unique: true,
                filter: "[GigId] IS NOT NULL");
        }
    }
}
