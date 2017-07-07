using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class addedgrocerycategorytogroceryitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroceryCategoryId",
                table: "GroceryItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroceryItems_GroceryCategoryId",
                table: "GroceryItems",
                column: "GroceryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroceryItems_GroceryCategory_GroceryCategoryId",
                table: "GroceryItems",
                column: "GroceryCategoryId",
                principalTable: "GroceryCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroceryItems_GroceryCategory_GroceryCategoryId",
                table: "GroceryItems");

            migrationBuilder.DropIndex(
                name: "IX_GroceryItems_GroceryCategoryId",
                table: "GroceryItems");

            migrationBuilder.DropColumn(
                name: "GroceryCategoryId",
                table: "GroceryItems");
        }
    }
}
