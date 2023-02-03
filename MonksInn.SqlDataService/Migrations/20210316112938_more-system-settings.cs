using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class moresystemsettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxPintsInContainer",
                table: "SystemSettings",
                newName: "SmtpPort");

            migrationBuilder.AddColumn<string>(
                name: "DefaultFromEmail",
                table: "SystemSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpServer",
                table: "SystemSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultFromEmail",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "SmtpServer",
                table: "SystemSettings");

            migrationBuilder.RenameColumn(
                name: "SmtpPort",
                table: "SystemSettings",
                newName: "MaxPintsInContainer");
        }
    }
}
