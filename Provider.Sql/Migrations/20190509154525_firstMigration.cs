using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SqlAccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsMailingList = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlAccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SqlContexts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    SqlAreaId = table.Column<int>(nullable: true),
                    SqlTechnologyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlContexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlContexts_SqlContexts_SqlAreaId",
                        column: x => x.SqlAreaId,
                        principalTable: "SqlContexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SqlContexts_SqlContexts_SqlTechnologyId",
                        column: x => x.SqlTechnologyId,
                        principalTable: "SqlContexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SqlUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SqlPossibleRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SqlContextId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlPossibleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlPossibleRoles_SqlContexts_SqlContextId",
                        column: x => x.SqlContextId,
                        principalTable: "SqlContexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SqlAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nickname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    SqlUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlAccounts_SqlUsers_SqlUserId",
                        column: x => x.SqlUserId,
                        principalTable: "SqlUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SqlRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SqlContextId = table.Column<int>(nullable: true),
                    SqlAccountId = table.Column<int>(nullable: true),
                    SqlPossibleRoleId = table.Column<int>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlRoles_SqlAccounts_SqlAccountId",
                        column: x => x.SqlAccountId,
                        principalTable: "SqlAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SqlRoles_SqlContexts_SqlContextId",
                        column: x => x.SqlContextId,
                        principalTable: "SqlContexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SqlRoles_SqlPossibleRoles_SqlPossibleRoleId",
                        column: x => x.SqlPossibleRoleId,
                        principalTable: "SqlPossibleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlAccounts_SqlUserId",
                table: "SqlAccounts",
                column: "SqlUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlContexts_SqlAreaId",
                table: "SqlContexts",
                column: "SqlAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlContexts_SqlTechnologyId",
                table: "SqlContexts",
                column: "SqlTechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlPossibleRoles_SqlContextId",
                table: "SqlPossibleRoles",
                column: "SqlContextId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlRoles_SqlAccountId",
                table: "SqlRoles",
                column: "SqlAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlRoles_SqlContextId",
                table: "SqlRoles",
                column: "SqlContextId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlRoles_SqlPossibleRoleId",
                table: "SqlRoles",
                column: "SqlPossibleRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlAccountTypes");

            migrationBuilder.DropTable(
                name: "SqlRoles");

            migrationBuilder.DropTable(
                name: "SqlAccounts");

            migrationBuilder.DropTable(
                name: "SqlPossibleRoles");

            migrationBuilder.DropTable(
                name: "SqlUsers");

            migrationBuilder.DropTable(
                name: "SqlContexts");
        }
    }
}
