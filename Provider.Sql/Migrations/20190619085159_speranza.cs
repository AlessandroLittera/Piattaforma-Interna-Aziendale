using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class speranza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "SqlRoles",
                newName: "LastEdit");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "SqlRoles",
                newName: "CreationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "SqlRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "SqlRoles");

            migrationBuilder.RenameColumn(
                name: "LastEdit",
                table: "SqlRoles",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "SqlRoles",
                newName: "From");
        }
    }
}
