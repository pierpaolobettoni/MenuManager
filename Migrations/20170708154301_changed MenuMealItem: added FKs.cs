using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class changedMenuMealItemaddedFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "MenuMealItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuMealItem_MenuId",
                table: "MenuMealItem",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuMealItem_Menus_MenuId",
                table: "MenuMealItem",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuMealItem_Menus_MenuId",
                table: "MenuMealItem");

            migrationBuilder.DropIndex(
                name: "IX_MenuMealItem_MenuId",
                table: "MenuMealItem");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "MenuMealItem");
        }
    }
}
