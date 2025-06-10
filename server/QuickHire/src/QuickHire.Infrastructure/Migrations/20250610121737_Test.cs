using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GigMetadatas_FilterOptions_GigId",
                table: "GigMetadatas");

            migrationBuilder.DropIndex(
                name: "IX_GigMetadatas_GigId",
                table: "GigMetadatas");

            migrationBuilder.CreateIndex(
                name: "IX_GigMetadatas_FilterOptionId",
                table: "GigMetadatas",
                column: "FilterOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_GigMetadatas_GigId",
                table: "GigMetadatas",
                column: "GigId");

            migrationBuilder.AddForeignKey(
                name: "FK_GigMetadatas_FilterOptions_FilterOptionId",
                table: "GigMetadatas",
                column: "FilterOptionId",
                principalTable: "FilterOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GigMetadatas_FilterOptions_FilterOptionId",
                table: "GigMetadatas");

            migrationBuilder.DropIndex(
                name: "IX_GigMetadatas_FilterOptionId",
                table: "GigMetadatas");

            migrationBuilder.DropIndex(
                name: "IX_GigMetadatas_GigId",
                table: "GigMetadatas");

            migrationBuilder.CreateIndex(
                name: "IX_GigMetadatas_GigId",
                table: "GigMetadatas",
                column: "GigId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GigMetadatas_FilterOptions_GigId",
                table: "GigMetadatas",
                column: "GigId",
                principalTable: "FilterOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
