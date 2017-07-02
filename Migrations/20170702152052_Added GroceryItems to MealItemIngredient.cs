using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class AddedGroceryItemstoMealItemIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItemIngredients_MealItems_MealItemId",
                table: "MealItemIngredients");

            migrationBuilder.DropColumn(
                name: "MealItemName",
                table: "MealItemIngredients");

            migrationBuilder.DropColumn(
                name: "MealtemDescription",
                table: "MealItemIngredients");

            migrationBuilder.AddColumn<int>(
                name: "GroceryItemId",
                table: "MealItemIngredients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MealItemId",
                table: "MealItemIngredients",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealItemIngredients_GroceryItemId",
                table: "MealItemIngredients",
                column: "GroceryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItemIngredients_GroceryItems_GroceryItemId",
                table: "MealItemIngredients",
                column: "GroceryItemId",
                principalTable: "GroceryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealItemIngredients_MealItems_MealItemId",
                table: "MealItemIngredients",
                column: "MealItemId",
                principalTable: "MealItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItemIngredients_GroceryItems_GroceryItemId",
                table: "MealItemIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MealItemIngredients_MealItems_MealItemId",
                table: "MealItemIngredients");

            migrationBuilder.DropIndex(
                name: "IX_MealItemIngredients_GroceryItemId",
                table: "MealItemIngredients");

            migrationBuilder.DropColumn(
                name: "GroceryItemId",
                table: "MealItemIngredients");

            migrationBuilder.AddColumn<string>(
                name: "MealItemName",
                table: "MealItemIngredients",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MealtemDescription",
                table: "MealItemIngredients",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MealItemId",
                table: "MealItemIngredients",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MealItemIngredients_MealItems_MealItemId",
                table: "MealItemIngredients",
                column: "MealItemId",
                principalTable: "MealItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
