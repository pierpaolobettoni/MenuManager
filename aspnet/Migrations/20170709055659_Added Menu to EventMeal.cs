using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class AddedMenutoEventMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "EventMeal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventMeal_MenuId",
                table: "EventMeal",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventMeal_Menus_MenuId",
                table: "EventMeal",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventMeal_Menus_MenuId",
                table: "EventMeal");

            migrationBuilder.DropIndex(
                name: "IX_EventMeal_MenuId",
                table: "EventMeal");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "EventMeal");
        }
    }
}
