using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class dateforcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SqlAssignements_SqlAccounts_SqlAccountId",
                table: "SqlAssignements");

            migrationBuilder.DropForeignKey(
                name: "FK_SqlAssignements_SqlUsers_SqlUserId",
                table: "SqlAssignements");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SqlUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "SqlContexts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "SqlContexts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdit",
                table: "SqlContexts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "SqlUserId",
                table: "SqlAssignements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SqlAccountId",
                table: "SqlAssignements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "SqlAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SqlAssignements_SqlAccounts_SqlAccountId",
                table: "SqlAssignements",
                column: "SqlAccountId",
                principalTable: "SqlAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SqlAssignements_SqlUsers_SqlUserId",
                table: "SqlAssignements",
                column: "SqlUserId",
                principalTable: "SqlUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SqlAssignements_SqlAccounts_SqlAccountId",
                table: "SqlAssignements");

            migrationBuilder.DropForeignKey(
                name: "FK_SqlAssignements_SqlUsers_SqlUserId",
                table: "SqlAssignements");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "SqlContexts");

            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "SqlContexts");

            migrationBuilder.DropColumn(
                name: "LastEdit",
                table: "SqlContexts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SqlUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "SqlUserId",
                table: "SqlAssignements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SqlAccountId",
                table: "SqlAssignements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "SqlAccounts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_SqlAssignements_SqlAccounts_SqlAccountId",
                table: "SqlAssignements",
                column: "SqlAccountId",
                principalTable: "SqlAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SqlAssignements_SqlUsers_SqlUserId",
                table: "SqlAssignements",
                column: "SqlUserId",
                principalTable: "SqlUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
