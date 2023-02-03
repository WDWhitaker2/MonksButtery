using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class undodbchangefortesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestDBChange",
                table: "SystemUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestDBChange",
                table: "SystemUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
