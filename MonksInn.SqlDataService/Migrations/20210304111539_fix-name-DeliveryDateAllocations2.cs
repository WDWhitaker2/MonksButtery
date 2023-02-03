using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class fixnameDeliveryDateAllocations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryDateAllocationId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryDateAllocationId",
                table: "Orders",
                column: "DeliveryDateAllocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryDateAllocations_DeliveryDateAllocationId",
                table: "Orders",
                column: "DeliveryDateAllocationId",
                principalTable: "DeliveryDateAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryDateAllocations_DeliveryDateAllocationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryDateAllocationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryDateAllocationId",
                table: "Orders");
        }
    }
}
