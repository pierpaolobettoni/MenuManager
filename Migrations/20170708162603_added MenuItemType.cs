using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class addedMenuItemType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItemType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    LocationId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemType_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "MenuItemTypeId",
                table: "MealItemIngredients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MealItemIngredients_MenuItemTypeId",
                table: "MealItemIngredients",
                column: "MenuItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemType_LocationId",
                table: "MenuItemType",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItemIngredients_MenuItemType_MenuItemTypeId",
                table: "MealItemIngredients",
                column: "MenuItemTypeId",
                principalTable: "MenuItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "MenuItemType");
        }
    }
}
