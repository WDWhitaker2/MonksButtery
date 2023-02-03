using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class fixnameDeliveryDateAllocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryTimeAllocations",
                table: "DeliveryTimeAllocations");

            migrationBuilder.RenameTable(
                name: "DeliveryTimeAllocations",
                newName: "DeliveryDateAllocations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryDateAllocations",
                table: "DeliveryDateAllocations",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryDateAllocations",
                table: "DeliveryDateAllocations");

            migrationBuilder.RenameTable(
                name: "DeliveryDateAllocations",
                newName: "DeliveryTimeAllocations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryTimeAllocations",
                table: "DeliveryTimeAllocations",
                column: "Id");
        }
    }
}
