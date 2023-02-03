using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class cartsrework1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TappedStockedItemId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "TappedStockedUnits",
                table: "CartItems",
                newName: "TappedStockUnits");

            migrationBuilder.AddColumn<decimal>(
                name: "TappedStockItemPricePerUnit",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TappedStockItemPricePerUnit",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "TappedStockUnits",
                table: "CartItems",
                newName: "TappedStockedUnits");

            migrationBuilder.AddColumn<Guid>(
                name: "TappedStockedItemId",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
