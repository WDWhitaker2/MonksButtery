using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class storeuserpasswordsalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EncryptedPassword",
                table: "StoreUsers",
                newName: "HashedPassword");

            migrationBuilder.AddColumn<int>(
                name: "Salt",
                table: "StoreUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "StoreUsers");

            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "StoreUsers",
                newName: "EncryptedPassword");
        }
    }
}
