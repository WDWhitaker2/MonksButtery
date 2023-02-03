using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonksInn.SqlDataService.Migrations
{
    public partial class userspublocationstorepricing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PubLocationId",
                table: "TappedStockItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PubLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultIsDeliveryLocation = table.Column<bool>(type: "bit", nullable: false),
                    DefaultIsTakeawayLocation = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PubLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreDefaultPricings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsWholesalePricing = table.Column<bool>(type: "bit", nullable: false),
                    WholeSaleUnitSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WholeSaleDefaultFlatMarkup = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RetailAbvLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RetailDefaultAbvPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDefaultPricings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWholeSaleUser = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TappedStockItems_PubLocationId",
                table: "TappedStockItems",
                column: "PubLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TappedStockItems_PubLocations_PubLocationId",
                table: "TappedStockItems",
                column: "PubLocationId",
                principalTable: "PubLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TappedStockItems_PubLocations_PubLocationId",
                table: "TappedStockItems");

            migrationBuilder.DropTable(
                name: "PubLocations");

            migrationBuilder.DropTable(
                name: "StoreDefaultPricings");

            migrationBuilder.DropTable(
                name: "StoreUsers");

            migrationBuilder.DropTable(
                name: "SystemUsers");

            migrationBuilder.DropIndex(
                name: "IX_TappedStockItems_PubLocationId",
                table: "TappedStockItems");

            migrationBuilder.DropColumn(
                name: "PubLocationId",
                table: "TappedStockItems");
        }
    }
}
