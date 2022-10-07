using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class MultiCalendars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "multi_date",
                table: "Attendance",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "multi_date",
                table: "Attendance",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
