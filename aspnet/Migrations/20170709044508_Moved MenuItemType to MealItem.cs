using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class MovedMenuItemTypetoMealItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItemIngredients_MenuItemType_MenuItemTypeId",
                table: "MealItemIngredients");

            migrationBuilder.DropIndex(
                name: "IX_MealItemIngredients_MenuItemTypeId",
                table: "MealItemIngredients");

            migrationBuilder.DropColumn(
                name: "MenuItemTypeId",
                table: "MealItemIngredients");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemTypeId",
                table: "MenuMealItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuMealItem_MenuItemTypeId",
                table: "MenuMealItem",
                column: "MenuItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuMealItem_MenuItemType_MenuItemTypeId",
                table: "MenuMealItem",
                column: "MenuItemTypeId",
                principalTable: "MenuItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuMealItem_MenuItemType_MenuItemTypeId",
                table: "MenuMealItem");

            migrationBuilder.DropIndex(
                name: "IX_MenuMealItem_MenuItemTypeId",
                table: "MenuMealItem");

            migrationBuilder.DropColumn(
                name: "MenuItemTypeId",
                table: "MenuMealItem");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemTypeId",
                table: "MealItemIngredients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MealItemIngredients_MenuItemTypeId",
                table: "MealItemIngredients",
                column: "MenuItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItemIngredients_MenuItemType_MenuItemTypeId",
                table: "MealItemIngredients",
                column: "MenuItemTypeId",
                principalTable: "MenuItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
