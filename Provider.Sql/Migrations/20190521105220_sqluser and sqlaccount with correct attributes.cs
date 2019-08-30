using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class sqluserandsqlaccountwithcorrectattributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "SqlAccounts",
                newName: "LastEdit");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "SqlAccounts",
                newName: "DeactivationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "SqlUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "SqlUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "SqlUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdit",
                table: "SqlUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "SqlAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "SqlUsers");

            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "SqlUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "SqlUsers");

            migrationBuilder.DropColumn(
                name: "LastEdit",
                table: "SqlUsers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "SqlAccounts");

            migrationBuilder.RenameColumn(
                name: "LastEdit",
                table: "SqlAccounts",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "DeactivationDate",
                table: "SqlAccounts",
                newName: "From");
        }
    }
}
