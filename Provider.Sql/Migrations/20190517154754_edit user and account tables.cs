using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class edituserandaccounttables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "SqlUsers");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "SqlAccounts",
                newName: "Email");

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "SqlAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                table: "SqlAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "SqlAccounts");

            migrationBuilder.DropColumn(
                name: "To",
                table: "SqlAccounts");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "SqlAccounts",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SqlUsers",
                nullable: true);
        }
    }
}
