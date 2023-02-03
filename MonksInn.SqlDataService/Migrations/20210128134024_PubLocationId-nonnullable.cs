using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class PubLocationIdnonnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TappedStockItems_PubLocations_PubLocationId",
                table: "TappedStockItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "PubLocationId",
                table: "TappedStockItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TappedStockItems_PubLocations_PubLocationId",
                table: "TappedStockItems",
                column: "PubLocationId",
                principalTable: "PubLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TappedStockItems_PubLocations_PubLocationId",
                table: "TappedStockItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "PubLocationId",
                table: "TappedStockItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TappedStockItems_PubLocations_PubLocationId",
                table: "TappedStockItems",
                column: "PubLocationId",
                principalTable: "PubLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
