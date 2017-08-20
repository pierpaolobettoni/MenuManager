using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class MovedMenuItemTypetoMealItemnotMENUMealItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuItemTypeId",
                table: "MealItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_MenuItemTypeId",
                table: "MealItems",
                column: "MenuItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItems_MenuItemType_MenuItemTypeId",
                table: "MealItems",
                column: "MenuItemTypeId",
                principalTable: "MenuItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItems_MenuItemType_MenuItemTypeId",
                table: "MealItems");

            migrationBuilder.DropIndex(
                name: "IX_MealItems_MenuItemTypeId",
                table: "MealItems");

            migrationBuilder.DropColumn(
                name: "MenuItemTypeId",
                table: "MealItems");
        }
    }
}
