using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class cartitemcellarstock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CellarStockItemPricePerUnit",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CellarStockUnits",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCellarStock",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellarStockItemPricePerUnit",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CellarStockUnits",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IsCellarStock",
                table: "CartItems");
        }
    }
}
