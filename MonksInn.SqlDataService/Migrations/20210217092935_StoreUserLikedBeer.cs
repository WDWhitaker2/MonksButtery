using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class StoreUserLikedBeer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreUserLikedBeers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    StoreUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUserLikedBeers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreUserLikedBeers_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreUserLikedBeers_StoreUsers_StoreUserId",
                        column: x => x.StoreUserId,
                        principalTable: "StoreUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreUserLikedBeers_BeerId",
                table: "StoreUserLikedBeers",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreUserLikedBeers_StoreUserId",
                table: "StoreUserLikedBeers",
                column: "StoreUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreUserLikedBeers");
        }
    }
}
