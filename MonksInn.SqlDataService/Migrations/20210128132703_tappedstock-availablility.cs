using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class tappedstockavailablility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForDelivery",
                table: "TappedStockItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ForTakeaway",
                table: "TappedStockItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RetailBeerType",
                table: "StoreDefaultPricings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForDelivery",
                table: "TappedStockItems");

            migrationBuilder.DropColumn(
                name: "ForTakeaway",
                table: "TappedStockItems");

            migrationBuilder.DropColumn(
                name: "RetailBeerType",
                table: "StoreDefaultPricings");
        }
    }
}
