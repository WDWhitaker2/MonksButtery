using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class stockOrderbeerlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StockOrders_BeerId",
                table: "StockOrders",
                column: "BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrders_Beers_BeerId",
                table: "StockOrders",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOrders_Beers_BeerId",
                table: "StockOrders");

            migrationBuilder.DropIndex(
                name: "IX_StockOrders_BeerId",
                table: "StockOrders");
        }
    }
}
