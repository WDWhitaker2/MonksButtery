using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class orderuserlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreUserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreUserId",
                table: "Orders",
                column: "StoreUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StoreUsers_StoreUserId",
                table: "Orders",
                column: "StoreUserId",
                principalTable: "StoreUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StoreUsers_StoreUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StoreUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StoreUserId",
                table: "Orders");
        }
    }
}
