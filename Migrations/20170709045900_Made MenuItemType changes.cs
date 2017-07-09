using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class MadeMenuItemTypechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
