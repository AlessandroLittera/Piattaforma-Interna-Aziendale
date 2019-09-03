using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class bigError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlRoles");

            migrationBuilder.DropTable(
                name: "SqlPossibleRoles");

            migrationBuilder.DropTable(
                name: "SqlContexts");

            migrationBuilder.DropColumn(
                name: "IsMailingList",
                table: "SqlAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "SqlAccounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SqlRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastEdit = table.Column<DateTime>(nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SqlVeicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlVeicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SqlRequestAssignements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SqlAccountId = table.Column<int>(nullable: true),
                    SqlRequestId = table.Column<int>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlRequestAssignements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlRequestAssignements_SqlAccounts_SqlAccountId",
                        column: x => x.SqlAccountId,
                        principalTable: "SqlAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SqlRequestAssignements_SqlRequests_SqlRequestId",
                        column: x => x.SqlRequestId,
                        principalTable: "SqlRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SqlVeicleAssignements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SqlAccountId = table.Column<int>(nullable: true),
                    SqlVeicleId = table.Column<int>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlVeicleAssignements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlVeicleAssignements_SqlAccounts_SqlAccountId",
                        column: x => x.SqlAccountId,
                        principalTable: "SqlAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SqlVeicleAssignements_SqlVeicles_SqlVeicleId",
                        column: x => x.SqlVeicleId,
                        principalTable: "SqlVeicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlRequestAssignements_SqlAccountId",
                table: "SqlRequestAssignements",
                column: "SqlAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlRequestAssignements_SqlRequestId",
                table: "SqlRequestAssignements",
                column: "SqlRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlVeicleAssignements_SqlAccountId",
                table: "SqlVeicleAssignements",
                column: "SqlAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SqlVeicleAssignements_SqlVeicleId",
                table: "SqlVeicleAssignements",
                column: "SqlVeicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlRequestAssignements");

            migrationBuilder.DropTable(
                name: "SqlVeicleAssignements");

            migrationBuilder.DropTable(
                name: "SqlRequests");

            migrationBuilder.DropTable(
                name: "SqlVeicles");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "SqlAccounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsMailingList",
                table: "SqlAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SqlContexts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    LastEdit = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
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
                name: "SqlRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    LastEdit = table.Column<DateTime>(nullable: false),
                    SqlAccountId = table.Column<int>(nullable: true),
                    SqlContextId = table.Column<int>(nullable: true),
                    SqlPossibleRoleId = table.Column<int>(nullable: true)
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
    }
}
