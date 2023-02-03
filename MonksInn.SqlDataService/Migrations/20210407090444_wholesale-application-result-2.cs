using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class wholesaleapplicationresult2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WholesaleApplications_StoreUserId",
                table: "WholesaleApplications",
                column: "StoreUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleApplications_StoreUsers_StoreUserId",
                table: "WholesaleApplications",
                column: "StoreUserId",
                principalTable: "StoreUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleApplications_StoreUsers_StoreUserId",
                table: "WholesaleApplications");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleApplications_StoreUserId",
                table: "WholesaleApplications");
        }
    }
}
