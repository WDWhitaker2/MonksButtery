using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class recievestockfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecievedDate",
                table: "StockOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RecievedInvoiceNumber",
                table: "StockOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecievedUnitSize",
                table: "StockOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecievedUnits",
                table: "StockOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecievedDate",
                table: "StockOrders");

            migrationBuilder.DropColumn(
                name: "RecievedInvoiceNumber",
                table: "StockOrders");

            migrationBuilder.DropColumn(
                name: "RecievedUnitSize",
                table: "StockOrders");

            migrationBuilder.DropColumn(
                name: "RecievedUnits",
                table: "StockOrders");
        }
    }
}
