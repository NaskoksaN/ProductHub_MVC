﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addShopingCartToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShopingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Order Id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "Product id"),
                    Count = table.Column<int>(type: "int", nullable: false, comment: "Count of product"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopingCarts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopingCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopingCarts_ApplicationUserId",
                table: "ShopingCarts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopingCarts_ProductId",
                table: "ShopingCarts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopingCarts");
        }
    }
}
