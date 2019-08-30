using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class multipleusersforaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SqlAccounts_SqlUsers_SqlUserId",
                table: "SqlAccounts");

            migrationBuilder.DropIndex(
                name: "IX_SqlAccounts_SqlUserId",
                table: "SqlAccounts");

            migrationBuilder.DropColumn(
                name: "SqlUserId",
                table: "SqlAccounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "SqlAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SqlAssignements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SqlUserId = table.Column<int>(nullable: true),
                    SqlAccountId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    LastEdit = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlAssignements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlAssignements_SqlAccounts_SqlAccountId",
                        column: x => x.SqlAccountId,
                        principalTable: "SqlAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SqlAssignements_SqlUsers_SqlUserId",
                        column: x => x.SqlUserId,
                        principalTable: "SqlUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlAssignements_SqlAccountId",
                table: "SqlAssignements",
                column: "SqlAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlAssignements_SqlUserId",
                table: "SqlAssignements",
                column: "SqlUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlAssignements");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "SqlAccounts");

            migrationBuilder.AddColumn<int>(
                name: "SqlUserId",
                table: "SqlAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SqlAccounts_SqlUserId",
                table: "SqlAccounts",
                column: "SqlUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SqlAccounts_SqlUsers_SqlUserId",
                table: "SqlAccounts",
                column: "SqlUserId",
                principalTable: "SqlUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
