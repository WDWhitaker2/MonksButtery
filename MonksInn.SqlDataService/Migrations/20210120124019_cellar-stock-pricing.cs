using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class cellarstockpricing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockLevel",
                table: "CellarStockItems");

            migrationBuilder.AlterColumn<int>(
                name: "RecievedUnits",
                table: "StockOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "RecievedUnitPrice",
                table: "StockOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPrice",
                table: "CellarStockItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecievedUnitPrice",
                table: "StockOrders");

            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "CellarStockItems");

            migrationBuilder.AlterColumn<int>(
                name: "RecievedUnits",
                table: "StockOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockLevel",
                table: "CellarStockItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
