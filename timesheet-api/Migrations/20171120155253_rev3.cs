using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace timesheetapi.Migrations
{
    public partial class rev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Entries");

            migrationBuilder.AddColumn<int>(
                name: "Minutes",
                table: "Entries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "Entries");

            migrationBuilder.AddColumn<float>(
                name: "Hours",
                table: "Entries",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
