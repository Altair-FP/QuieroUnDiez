using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class actualizacionMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "CALENDAR_TASK",
                newName: "DayStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "DayEnd",
                table: "CALENDAR_TASK",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 14,
                column: "MenuId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 15,
                column: "MenuId",
                value: 14);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayEnd",
                table: "CALENDAR_TASK");

            migrationBuilder.RenameColumn(
                name: "DayStart",
                table: "CALENDAR_TASK",
                newName: "Day");

            migrationBuilder.UpdateData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 14,
                column: "MenuId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 15,
                column: "MenuId",
                value: 12);

            migrationBuilder.InsertData(
                table: "ROLE_HAS_MENU",
                columns: new[] { "ID", "MenuId", "RoleId" },
                values: new object[] { 16, 14, 2 });
        }
    }
}
