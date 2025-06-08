using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCustomRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeactivatedRecords_ReportedItems_ReportedItemId",
                table: "DeactivatedRecords");

            migrationBuilder.DropTable(
                name: "CustomRequests");

            migrationBuilder.DropIndex(
                name: "IX_DeactivatedRecords_ReportedItemId",
                table: "DeactivatedRecords");

            migrationBuilder.DropColumn(
                name: "CustomRequestId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ReportedItemId",
                table: "DeactivatedRecords");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Revisions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Revisions");

            migrationBuilder.AddColumn<int>(
                name: "CustomRequestId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportedItemId",
                table: "DeactivatedRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GigId = table.Column<int>(type: "int", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomRequestNumber = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryTimeInDays = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomRequests_Gigs_GigId",
                        column: x => x.GigId,
                        principalTable: "Gigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomRequests_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeactivatedRecords_ReportedItemId",
                table: "DeactivatedRecords",
                column: "ReportedItemId",
                unique: true,
                filter: "[ReportedItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomRequests_GigId",
                table: "CustomRequests",
                column: "GigId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomRequests_MessageId",
                table: "CustomRequests",
                column: "MessageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeactivatedRecords_ReportedItems_ReportedItemId",
                table: "DeactivatedRecords",
                column: "ReportedItemId",
                principalTable: "ReportedItems",
                principalColumn: "Id");
        }
    }
}
