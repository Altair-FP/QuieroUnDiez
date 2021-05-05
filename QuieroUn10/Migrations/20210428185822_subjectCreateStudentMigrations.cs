using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class subjectCreateStudentMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Student_Create",
                table: "SUBJECT",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Student_Create",
                table: "SUBJECT");
        }
    }
}
