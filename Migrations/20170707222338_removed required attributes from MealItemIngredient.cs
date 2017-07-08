using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class removedrequiredattributesfromMealItemIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItemIngredients_MenuMealItem_MenuMealItemId",
                table: "MealItemIngredients");

            migrationBuilder.DropIndex(
                name: "IX_MealItemIngredients_MenuMealItemId",
                table: "MealItemIngredients");

            migrationBuilder.DropColumn(
                name: "MenuMealItemId",
                table: "MealItemIngredients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuMealItemId",
                table: "MealItemIngredients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealItemIngredients_MenuMealItemId",
                table: "MealItemIngredients",
                column: "MenuMealItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItemIngredients_MenuMealItem_MenuMealItemId",
                table: "MealItemIngredients",
                column: "MenuMealItemId",
                principalTable: "MenuMealItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
