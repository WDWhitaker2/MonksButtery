using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class fixedspellingofrecieve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOrders_Beers_RecievedBeerId",
                table: "StockOrders");

            migrationBuilder.RenameColumn(
                name: "RecievedUnits",
                table: "StockOrders",
                newName: "ReceivedUnits");

            migrationBuilder.RenameColumn(
                name: "RecievedUnitSize",
                table: "StockOrders",
                newName: "ReceivedUnitSize");

            migrationBuilder.RenameColumn(
                name: "RecievedUnitPrice",
                table: "StockOrders",
                newName: "ReceivedUnitPrice");

            migrationBuilder.RenameColumn(
                name: "RecievedInvoiceNumber",
                table: "StockOrders",
                newName: "ReceivedInvoiceNumber");

            migrationBuilder.RenameColumn(
                name: "RecievedDate",
                table: "StockOrders",
                newName: "ReceivedDate");

            migrationBuilder.RenameColumn(
                name: "RecievedBeerId",
                table: "StockOrders",
                newName: "ReceivedBeerId");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrders_RecievedBeerId",
                table: "StockOrders",
                newName: "IX_StockOrders_ReceivedBeerId");

            migrationBuilder.RenameColumn(
                name: "PaymentRecieved",
                table: "Orders",
                newName: "PaymentReceived");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrders_Beers_ReceivedBeerId",
                table: "StockOrders",
                column: "ReceivedBeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOrders_Beers_ReceivedBeerId",
                table: "StockOrders");

            migrationBuilder.RenameColumn(
                name: "ReceivedUnits",
                table: "StockOrders",
                newName: "RecievedUnits");

            migrationBuilder.RenameColumn(
                name: "ReceivedUnitSize",
                table: "StockOrders",
                newName: "RecievedUnitSize");

            migrationBuilder.RenameColumn(
                name: "ReceivedUnitPrice",
                table: "StockOrders",
                newName: "RecievedUnitPrice");

            migrationBuilder.RenameColumn(
                name: "ReceivedInvoiceNumber",
                table: "StockOrders",
                newName: "RecievedInvoiceNumber");

            migrationBuilder.RenameColumn(
                name: "ReceivedDate",
                table: "StockOrders",
                newName: "RecievedDate");

            migrationBuilder.RenameColumn(
                name: "ReceivedBeerId",
                table: "StockOrders",
                newName: "RecievedBeerId");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrders_ReceivedBeerId",
                table: "StockOrders",
                newName: "IX_StockOrders_RecievedBeerId");

            migrationBuilder.RenameColumn(
                name: "PaymentReceived",
                table: "Orders",
                newName: "PaymentRecieved");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrders_Beers_RecievedBeerId",
                table: "StockOrders",
                column: "RecievedBeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
