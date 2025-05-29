using System;
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
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SubSubCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SubSubCategories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MainCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MainCategories");

            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "SubSubCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SubSubCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "SubCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SubCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "MainCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "MainCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "GigFilters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "SubSubCategories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SubSubCategories");

            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "MainCategories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MainCategories");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SubSubCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SubSubCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SubCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SubCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MainCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MainCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "GigFilters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
