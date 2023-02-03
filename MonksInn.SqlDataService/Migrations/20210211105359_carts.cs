using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class carts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CartSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TappedStockedItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TappedStockItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TappedStockedUnits = table.Column<int>(type: "int", nullable: false),
                    CellarStockItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_CartSessions_CartSessionId",
                        column: x => x.CartSessionId,
                        principalTable: "CartSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_CellarStockItems_CellarStockItemId",
                        column: x => x.CellarStockItemId,
                        principalTable: "CellarStockItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_TappedStockItems_TappedStockItemId",
                        column: x => x.TappedStockItemId,
                        principalTable: "TappedStockItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreUsers_CartSessionID",
                table: "StoreUsers",
                column: "CartSessionID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartSessionId",
                table: "CartItems",
                column: "CartSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CellarStockItemId",
                table: "CartItems",
                column: "CellarStockItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_TappedStockItemId",
                table: "CartItems",
                column: "TappedStockItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreUsers_CartSessions_CartSessionID",
                table: "StoreUsers",
                column: "CartSessionID",
                principalTable: "CartSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreUsers_CartSessions_CartSessionID",
                table: "StoreUsers");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "CartSessions");

            migrationBuilder.DropIndex(
                name: "IX_StoreUsers_CartSessionID",
                table: "StoreUsers");
        }
    }
}
