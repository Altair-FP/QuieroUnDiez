using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class subjectActiveMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "SUBJECT",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 11,
                column: "Label",
                value: "Subjects");

            migrationBuilder.UpdateData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 13,
                column: "MenuId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 15,
                column: "MenuId",
                value: 9);

            migrationBuilder.InsertData(
                table: "ROLE_HAS_MENU",
                columns: new[] { "ID", "MenuId", "RoleId" },
                values: new object[] { 16, 14, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SUBJECT");

            migrationBuilder.UpdateData(
                table: "MENU",
                keyColumn: "ID",
                keyValue: 11,
                column: "Label",
                value: "Student Subjects");

            migrationBuilder.UpdateData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 13,
                column: "MenuId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ROLE_HAS_MENU",
                keyColumn: "ID",
                keyValue: 15,
                column: "MenuId",
                value: 14);
        }
    }
}
