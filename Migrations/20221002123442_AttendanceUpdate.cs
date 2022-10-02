using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class AttendanceUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Attendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IsHalfDay",
                table: "Attendance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsLate",
                table: "Attendance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarkAttendanceBY",
                table: "Attendance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Attendance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingFrom",
                table: "Attendance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Attendance",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "IsHalfDay",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "IsLate",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "MarkAttendanceBY",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "WorkingFrom",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Attendance");
        }
    }
}
