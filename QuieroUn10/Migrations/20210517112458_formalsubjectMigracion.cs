using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class formalsubjectMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "SUBJECT",
                newName: "Formal_Subject");

            migrationBuilder.AlterColumn<DateTime>(
                name: "End",
                table: "TASK",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DayEnd",
                table: "CALENDAR_TASK",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 4,
                column: "Controller",
                value: "AdminDtoes");

            migrationBuilder.InsertData(
                table: "MENU",
                columns: new[] { "ID", "Action", "Controller", "Label" },
                values: new object[] { 15, "Index", "Methods", "Método Pomodoro" });

            migrationBuilder.InsertData(
                table: "ROLE_HAS_MENU",
                columns: new[] { "ID", "MenuId", "RoleId" },
                values: new object[] { 17, 15, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.RenameColumn(
                name: "Formal_Subject",
                table: "SUBJECT",
                newName: "Active");

            migrationBuilder.AlterColumn<DateTime>(
                name: "End",
                table: "TASK",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DayEnd",
                table: "CALENDAR_TASK",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 4,
                column: "Controller",
                value: "Admins");

            migrationBuilder.InsertData(
                table: "ROLE_HAS_MENU",
                columns: new[] { "ID", "MenuId", "RoleId" },
                values: new object[,]
                {
                    { 9, 9, 1 },
                    { 10, 10, 1 },
                    { 11, 11, 1 },
                    { 12, 12, 1 }
                });
        }
    }
}
