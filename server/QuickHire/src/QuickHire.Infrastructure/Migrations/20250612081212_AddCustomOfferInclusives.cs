using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomOfferInclusives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomOfferInclusives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomOfferId = table.Column<int>(type: "int", nullable: false),
                    PaymentPlanIncludeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomOfferInclusives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomOfferInclusives_CustomOffers_CustomOfferId",
                        column: x => x.CustomOfferId,
                        principalTable: "CustomOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomOfferInclusives_PaymentPlanIncludes_PaymentPlanIncludeId",
                        column: x => x.PaymentPlanIncludeId,
                        principalTable: "PaymentPlanIncludes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomOfferInclusives_CustomOfferId",
                table: "CustomOfferInclusives",
                column: "CustomOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomOfferInclusives_PaymentPlanIncludeId",
                table: "CustomOfferInclusives",
                column: "PaymentPlanIncludeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomOfferInclusives");
        }
    }
}
