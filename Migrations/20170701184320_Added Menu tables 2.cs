using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class AddedMenutables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroceryCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    GroceryCategoryDescription = table.Column<string>(nullable: true),
                    GroceryCategoryName = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroceryCategory_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroceryItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    GroceryItemName = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroceryItems_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    LocationId = table.Column<int>(nullable: true),
                    MealItemDescription = table.Column<string>(nullable: true),
                    MealItemName = table.Column<string>(nullable: false),
                    MeasureType = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealItems_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealItemIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    LocationId = table.Column<int>(nullable: true),
                    MealItemId = table.Column<int>(nullable: true),
                    MealItemName = table.Column<string>(nullable: false),
                    MealtemDescription = table.Column<string>(nullable: true),
                    MeasureType = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealItemIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealItemIngredients_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MealItemIngredients_MealItems_MealItemId",
                        column: x => x.MealItemId,
                        principalTable: "MealItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroceryCategory_LocationId",
                table: "GroceryCategory",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroceryItems_LocationId",
                table: "GroceryItems",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_LocationId",
                table: "MealItems",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItemIngredients_LocationId",
                table: "MealItemIngredients",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItemIngredients_MealItemId",
                table: "MealItemIngredients",
                column: "MealItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroceryCategory");

            migrationBuilder.DropTable(
                name: "GroceryItems");

            migrationBuilder.DropTable(
                name: "MealItemIngredients");

            migrationBuilder.DropTable(
                name: "MealItems");
        }
    }
}
