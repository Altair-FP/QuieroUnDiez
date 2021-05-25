using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class ultimaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 9,
                column: "Label",
                value: "Calendario de tareas");

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 11,
                column: "Label",
                value: "Asignaturas");

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 12,
                column: "Label",
                value: "Tareas");

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 14,
                column: "Label",
                value: "Perfil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 9,
                column: "Label",
                value: "Calendar Tasks");

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 11,
                column: "Label",
                value: "Subjects");

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 12,
                column: "Label",
                value: "Tasks");

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 14,
                column: "Label",
                value: "Profile");
        }
    }
}
