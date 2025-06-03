using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoundexFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBriefs_MainCategories_MainCategoryId",
                table: "ProjectBriefs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBriefs_SubCategories_SubCategoryId",
                table: "ProjectBriefs");

            migrationBuilder.DropIndex(
                name: "IX_ProjectBriefs_MainCategoryId",
                table: "ProjectBriefs");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "ProjectBriefs");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "ProjectBriefs",
                newName: "SubSubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectBriefs_SubCategoryId",
                table: "ProjectBriefs",
                newName: "IX_ProjectBriefs_SubSubCategoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SuitableSellerProjectBriefs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBriefs_SubSubCategories_SubSubCategoryId",
                table: "ProjectBriefs",
                column: "SubSubCategoryId",
                principalTable: "SubSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBriefs_SubSubCategories_SubSubCategoryId",
                table: "ProjectBriefs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SuitableSellerProjectBriefs");

            migrationBuilder.RenameColumn(
                name: "SubSubCategoryId",
                table: "ProjectBriefs",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectBriefs_SubSubCategoryId",
                table: "ProjectBriefs",
                newName: "IX_ProjectBriefs_SubCategoryId");

            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "ProjectBriefs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBriefs_MainCategoryId",
                table: "ProjectBriefs",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBriefs_MainCategories_MainCategoryId",
                table: "ProjectBriefs",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBriefs_SubCategories_SubCategoryId",
                table: "ProjectBriefs",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
