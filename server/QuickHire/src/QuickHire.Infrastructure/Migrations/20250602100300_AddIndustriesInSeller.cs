using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndustriesInSeller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IndustryId",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "Invoices",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IndustrySkillSeller",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    IndustrySkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustrySkillSeller", x => new { x.SellerId, x.IndustrySkillId });
                    table.ForeignKey(
                        name: "FK_IndustrySkillSeller_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndustrySkillSeller_SubCategories_IndustrySkillId",
                        column: x => x.IndustrySkillId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_IndustryId",
                table: "Sellers",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SellerId",
                table: "Invoices",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustrySkillSeller_IndustrySkillId",
                table: "IndustrySkillSeller",
                column: "IndustrySkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Sellers_SellerId",
                table: "Invoices",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_MainCategories_IndustryId",
                table: "Sellers",
                column: "IndustryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Sellers_SellerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_MainCategories_IndustryId",
                table: "Sellers");

            migrationBuilder.DropTable(
                name: "IndustrySkillSeller");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_IndustryId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SellerId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "AspNetUsers");
        }
    }
}
