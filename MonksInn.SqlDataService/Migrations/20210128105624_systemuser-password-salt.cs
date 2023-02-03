using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class systemuserpasswordsalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EncryptedPassword",
                table: "SystemUsers",
                newName: "HashedPassword");

            migrationBuilder.AddColumn<int>(
                name: "Salt",
                table: "SystemUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "SystemUsers");

            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "SystemUsers",
                newName: "EncryptedPassword");
        }
    }
}
