using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickHire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Country_Addresses_AddressId",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Country_AddressId",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "Gigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Gigs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GraduationYear",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Educations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "Gigs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Gigs");

            migrationBuilder.DropColumn(
                name: "GraduationYear",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Educations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Educations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Country",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Country_AddressId",
                table: "Country",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Country_Addresses_AddressId",
                table: "Country",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
