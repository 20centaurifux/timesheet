using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace timesheetapi.Migrations
{
    public partial class rev4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Minutes",
                table: "Entries",
                newName: "Seconds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Seconds",
                table: "Entries",
                newName: "Minutes");
        }
    }
}
