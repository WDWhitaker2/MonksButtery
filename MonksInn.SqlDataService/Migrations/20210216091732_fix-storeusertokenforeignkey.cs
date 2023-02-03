using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class fixstoreusertokenforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreUserPasswordResetTokens_SystemUsers_SystemUserId",
                table: "StoreUserPasswordResetTokens");

            migrationBuilder.RenameColumn(
                name: "SystemUserId",
                table: "StoreUserPasswordResetTokens",
                newName: "StoreUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoreUserPasswordResetTokens_SystemUserId",
                table: "StoreUserPasswordResetTokens",
                newName: "IX_StoreUserPasswordResetTokens_StoreUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreUserPasswordResetTokens_StoreUsers_StoreUserId",
                table: "StoreUserPasswordResetTokens",
                column: "StoreUserId",
                principalTable: "StoreUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreUserPasswordResetTokens_StoreUsers_StoreUserId",
                table: "StoreUserPasswordResetTokens");

            migrationBuilder.RenameColumn(
                name: "StoreUserId",
                table: "StoreUserPasswordResetTokens",
                newName: "SystemUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoreUserPasswordResetTokens_StoreUserId",
                table: "StoreUserPasswordResetTokens",
                newName: "IX_StoreUserPasswordResetTokens_SystemUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreUserPasswordResetTokens_SystemUsers_SystemUserId",
                table: "StoreUserPasswordResetTokens",
                column: "SystemUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
