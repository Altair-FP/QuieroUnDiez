using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class clase1Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleHasMenu_Menu_MenuId",
                table: "RoleHasMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleHasMenu_Role_RoleId",
                table: "RoleHasMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ACCOUNT_Role_RoleId",
                table: "USER_ACCOUNT");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_USER_ACCOUNT_UserAccountId",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToken",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleHasMenu",
                table: "RoleHasMenu");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "ROLE");

            migrationBuilder.RenameTable(
                name: "UserToken",
                newName: "USER_TOKEN");

            migrationBuilder.RenameTable(
                name: "RoleHasMenu",
                newName: "ROLE_HAS_MENU");

            migrationBuilder.RenameIndex(
                name: "IX_UserToken_UserAccountId",
                table: "USER_TOKEN",
                newName: "IX_USER_TOKEN_UserAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleHasMenu_RoleId",
                table: "ROLE_HAS_MENU",
                newName: "IX_ROLE_HAS_MENU_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleHasMenu_MenuId",
                table: "ROLE_HAS_MENU",
                newName: "IX_ROLE_HAS_MENU_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ROLE",
                table: "ROLE",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_TOKEN",
                table: "USER_TOKEN",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ROLE_HAS_MENU",
                table: "ROLE_HAS_MENU",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Admin_USER_ACCOUNT_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "STUDENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STUDENT_USER_ACCOUNT_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StudentDto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDto", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "Description", "Enabled", "Name" },
                values: new object[] { 1, null, false, "ADMIN" });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "Description", "Enabled", "Name" },
                values: new object[] { 2, null, false, "STUDENT" });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserAccountId",
                table: "Admin",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_UserAccountId",
                table: "STUDENT",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_HAS_MENU_Menu_MenuId",
                table: "ROLE_HAS_MENU",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_HAS_MENU_ROLE_RoleId",
                table: "ROLE_HAS_MENU",
                column: "RoleId",
                principalTable: "ROLE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ACCOUNT_ROLE_RoleId",
                table: "USER_ACCOUNT",
                column: "RoleId",
                principalTable: "ROLE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_TOKEN_USER_ACCOUNT_UserAccountId",
                table: "USER_TOKEN",
                column: "UserAccountId",
                principalTable: "USER_ACCOUNT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_HAS_MENU_Menu_MenuId",
                table: "ROLE_HAS_MENU");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_HAS_MENU_ROLE_RoleId",
                table: "ROLE_HAS_MENU");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ACCOUNT_ROLE_RoleId",
                table: "USER_ACCOUNT");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_TOKEN_USER_ACCOUNT_UserAccountId",
                table: "USER_TOKEN");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "STUDENT");

            migrationBuilder.DropTable(
                name: "StudentDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ROLE",
                table: "ROLE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_TOKEN",
                table: "USER_TOKEN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ROLE_HAS_MENU",
                table: "ROLE_HAS_MENU");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "ROLE",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "USER_TOKEN",
                newName: "UserToken");

            migrationBuilder.RenameTable(
                name: "ROLE_HAS_MENU",
                newName: "RoleHasMenu");

            migrationBuilder.RenameIndex(
                name: "IX_USER_TOKEN_UserAccountId",
                table: "UserToken",
                newName: "IX_UserToken_UserAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_ROLE_HAS_MENU_RoleId",
                table: "RoleHasMenu",
                newName: "IX_RoleHasMenu_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ROLE_HAS_MENU_MenuId",
                table: "RoleHasMenu",
                newName: "IX_RoleHasMenu_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToken",
                table: "UserToken",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleHasMenu",
                table: "RoleHasMenu",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleHasMenu_Menu_MenuId",
                table: "RoleHasMenu",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleHasMenu_Role_RoleId",
                table: "RoleHasMenu",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ACCOUNT_Role_RoleId",
                table: "USER_ACCOUNT",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_USER_ACCOUNT_UserAccountId",
                table: "UserToken",
                column: "UserAccountId",
                principalTable: "USER_ACCOUNT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
