using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class MultiCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "multi_date",
                table: "Attendance",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "multi_date",
                table: "Attendance");
        }
    }
}
