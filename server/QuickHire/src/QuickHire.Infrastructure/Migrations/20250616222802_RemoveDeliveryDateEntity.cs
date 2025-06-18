using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeliveryDateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDeliveryDates");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SelectedPaymentPlanId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AcceptedAt",
                table: "Deliveries");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SelectedPaymentPlanId",
                table: "Orders",
                column: "SelectedPaymentPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_SelectedPaymentPlanId",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedAt",
                table: "Deliveries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDeliveryDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ChangeDateReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsChanged = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveryDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryDates_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SelectedPaymentPlanId",
                table: "Orders",
                column: "SelectedPaymentPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryDates_OrderId",
                table: "OrderDeliveryDates",
                column: "OrderId",
                unique: true);
        }
    }
}
