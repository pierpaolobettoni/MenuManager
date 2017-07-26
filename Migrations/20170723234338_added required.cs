using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace clean_aspnet_mvc.Migrations
{
    public partial class addedrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "EventMeal",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeopleAttending",
                table: "EventMeal",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "EventMeal");

            migrationBuilder.DropColumn(
                name: "NumberOfPeopleAttending",
                table: "EventMeal");
        }
    }
}
