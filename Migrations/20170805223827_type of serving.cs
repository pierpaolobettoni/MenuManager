using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class typeofserving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasureType",
                table: "MealItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MealItems");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfServing",
                table: "MealItems",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfServing",
                table: "MealItems");

            migrationBuilder.AddColumn<string>(
                name: "MeasureType",
                table: "MealItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "MealItems",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
