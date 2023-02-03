using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class StoreUserRegistrationToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreUserRegistrationTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUserRegistrationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreUserRegistrationTokens_StoreUsers_StoreUserId",
                        column: x => x.StoreUserId,
                        principalTable: "StoreUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreUserRegistrationTokens_StoreUserId",
                table: "StoreUserRegistrationTokens",
                column: "StoreUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreUserRegistrationTokens");
        }
    }
}
