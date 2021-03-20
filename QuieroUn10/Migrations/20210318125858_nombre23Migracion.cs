using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class nombre23Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskDate",
                table: "TASK",
                newName: "Start");

            migrationBuilder.AddColumn<bool>(
                name: "AllDay",
                table: "TASK",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TASK",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "TASK",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TASK",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllDay",
                table: "TASK");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TASK");

            migrationBuilder.DropColumn(
                name: "End",
                table: "TASK");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TASK");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "TASK",
                newName: "TaskDate");
        }
    }
}
