using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class cellarstockchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecievedBeerId",
                table: "StockOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitSize",
                table: "CellarStockItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockOrders_RecievedBeerId",
                table: "StockOrders",
                column: "RecievedBeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrders_Beers_RecievedBeerId",
                table: "StockOrders",
                column: "RecievedBeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOrders_Beers_RecievedBeerId",
                table: "StockOrders");

            migrationBuilder.DropIndex(
                name: "IX_StockOrders_RecievedBeerId",
                table: "StockOrders");

            migrationBuilder.DropColumn(
                name: "RecievedBeerId",
                table: "StockOrders");

            migrationBuilder.DropColumn(
                name: "UnitSize",
                table: "CellarStockItems");
        }
    }
}
