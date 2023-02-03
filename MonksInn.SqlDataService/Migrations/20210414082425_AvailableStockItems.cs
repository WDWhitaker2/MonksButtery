using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class AvailableStockItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CellarStockItemId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CellarStockItemPricePerUnit",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CellarStockUnitSize",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CellarStockItemId",
                table: "OrderItems",
                column: "CellarStockItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_CellarStockItems_CellarStockItemId",
                table: "OrderItems",
                column: "CellarStockItemId",
                principalTable: "CellarStockItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_CellarStockItems_CellarStockItemId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_CellarStockItemId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CellarStockItemId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CellarStockItemPricePerUnit",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CellarStockUnitSize",
                table: "OrderItems");
        }
    }
}
