using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreWeb_CRUDGraphQL.Migrations
{
    public partial class TableStoreProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreProducts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    StoreID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_ProductID",
                table: "StoreProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_StoreID",
                table: "StoreProducts",
                column: "StoreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProducts");
        }
    }
}
