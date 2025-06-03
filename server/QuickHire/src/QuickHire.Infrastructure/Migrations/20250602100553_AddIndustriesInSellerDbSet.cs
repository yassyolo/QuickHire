using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndustriesInSellerDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndustrySkillSeller");

            migrationBuilder.CreateTable(
                name: "IndustrySkillSellers",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    IndustrySkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustrySkillSellers", x => new { x.SellerId, x.IndustrySkillId });
                    table.ForeignKey(
                        name: "FK_IndustrySkillSellers_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndustrySkillSellers_SubCategories_IndustrySkillId",
                        column: x => x.IndustrySkillId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndustrySkillSellers_IndustrySkillId",
                table: "IndustrySkillSellers",
                column: "IndustrySkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndustrySkillSellers");

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
                name: "IX_IndustrySkillSeller_IndustrySkillId",
                table: "IndustrySkillSeller",
                column: "IndustrySkillId");
        }
    }
}
