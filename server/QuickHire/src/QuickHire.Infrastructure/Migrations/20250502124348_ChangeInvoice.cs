using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "Invoices",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false);   
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "Invoices");
        }
    }
}
