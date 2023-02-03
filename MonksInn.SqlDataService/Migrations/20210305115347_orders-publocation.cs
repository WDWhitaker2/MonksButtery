using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class orderspublocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PubLocationId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PubLocationId",
                table: "Orders",
                column: "PubLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PubLocations_PubLocationId",
                table: "Orders",
                column: "PubLocationId",
                principalTable: "PubLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PubLocations_PubLocationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PubLocationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PubLocationId",
                table: "Orders");
        }
    }
}
