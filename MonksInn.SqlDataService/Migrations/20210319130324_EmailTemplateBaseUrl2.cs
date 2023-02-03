using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class EmailTemplateBaseUrl2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailTemplateBaseUrl",
                table: "SystemSettings",
                newName: "EmailTemplateBaseWebUrl");

            migrationBuilder.AddColumn<string>(
                name: "EmailTemplateBaseBackendUrl",
                table: "SystemSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailTemplateBaseBackendUrl",
                table: "SystemSettings");

            migrationBuilder.RenameColumn(
                name: "EmailTemplateBaseWebUrl",
                table: "SystemSettings",
                newName: "EmailTemplateBaseUrl");
        }
    }
}
