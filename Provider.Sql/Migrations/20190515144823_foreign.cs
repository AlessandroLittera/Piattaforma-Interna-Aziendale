using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class foreign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SqlAccountTypeId",
                table: "SqlAccounts",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SqlAccounts_SqlAccountTypes_SqlAccountTypeId",
                table: "SqlAccounts");

            migrationBuilder.DropIndex(
                name: "IX_SqlAccounts_SqlAccountTypeId",
                table: "SqlAccounts");

            migrationBuilder.DropColumn(
                name: "SqlAccountTypeId",
                table: "SqlAccounts");
        }
    }
}
