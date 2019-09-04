using Microsoft.EntityFrameworkCore.Migrations;

namespace Provider.Sql.Migrations
{
    public partial class dscriptionfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descrizione",
                table: "SqlRequests",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "SqlRequestAssignements",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descrizione",
                table: "SqlRequests");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "SqlRequestAssignements");
        }
    }
}
