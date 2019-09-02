using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class bye : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SqlAccounts_SqlAccountTypes_SqlAccountTypeId",
                table: "SqlAccounts");

            migrationBuilder.DropTable(
                name: "SqlAccountTypes");

            migrationBuilder.DropIndex(
                name: "IX_SqlAccounts_SqlAccountTypeId",
                table: "SqlAccounts");

            migrationBuilder.DropColumn(
                name: "SqlAccountTypeId",
                table: "SqlAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "SqlAccounts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsMailingList",
                table: "SqlAccounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "SqlAccounts");

            migrationBuilder.DropColumn(
                name: "IsMailingList",
                table: "SqlAccounts");

            migrationBuilder.AddColumn<int>(
                name: "SqlAccountTypeId",
                table: "SqlAccounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SqlAccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    IsMailingList = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlAccountTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlAccounts_SqlAccountTypeId",
                table: "SqlAccounts",
                column: "SqlAccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SqlAccounts_SqlAccountTypes_SqlAccountTypeId",
                table: "SqlAccounts",
                column: "SqlAccountTypeId",
                principalTable: "SqlAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
