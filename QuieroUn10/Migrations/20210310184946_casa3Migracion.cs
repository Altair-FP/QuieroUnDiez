using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class casa3Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COURSE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MENU",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Controller = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENU", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STUDIES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SUBJECT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUBJECT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_HAS_MENU",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_HAS_MENU", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ROLE_HAS_MENU_MENU_MenuId",
                        column: x => x.MenuId,
                        principalTable: "MENU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROLE_HAS_MENU_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_ACCOUNT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ACCOUNT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_ACCOUNT_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDIES_HAS_COURSE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudiesId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDIES_HAS_COURSE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STUDIES_HAS_COURSE_COURSE_CourseId",
                        column: x => x.CourseId,
                        principalTable: "COURSE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDIES_HAS_COURSE_STUDIES_StudiesId",
                        column: x => x.StudiesId,
                        principalTable: "STUDIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ADMIN",
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
                    table.PrimaryKey("PK_ADMIN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ADMIN_USER_ACCOUNT_UserAccountId",
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
                name: "USER_TOKEN",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Life = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_TOKEN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_TOKEN_USER_ACCOUNT_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COURSE_HAS_SUBJECT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudiesHasCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSE_HAS_SUBJECT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COURSE_HAS_SUBJECT_STUDIES_HAS_COURSE_StudiesHasCourseId",
                        column: x => x.StudiesHasCourseId,
                        principalTable: "STUDIES_HAS_COURSE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COURSE_HAS_SUBJECT_SUBJECT_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SUBJECT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "COURSE",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "1º" },
                    { 2, "2º" },
                    { 3, "3º" },
                    { 4, "4º" },
                    { 5, "5º" },
                    { 6, "6º" }
                });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "Description", "Enabled", "Name" },
                values: new object[,]
                {
                    { 1, null, false, "ADMIN" },
                    { 2, null, false, "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "STUDIES",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 4, "UNIVERSIDAD" },
                    { 3, "FP" },
                    { 2, "BACHILLERATO" },
                    { 1, "ESO" }
                });

            migrationBuilder.InsertData(
                table: "SUBJECT",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Matemáticas I" },
                    { 2, "Lengua Castellana y Literatura I" },
                    { 3, "Primera Lengua Extranjera I" },
                    { 4, "Filosofía" },
                    { 5, "Biología y Geología" },
                    { 6, "Dibujo Técnico I" },
                    { 7, "Física y Química" }
                });

            migrationBuilder.InsertData(
                table: "STUDIES_HAS_COURSE",
                columns: new[] { "ID", "CourseId", "Nombre", "StudiesId" },
                values: new object[,]
                {
                    { 1, 1, "1º ESO", 1 },
                    { 2, 2, "2º ESO", 1 },
                    { 3, 3, "3º ESO", 1 },
                    { 4, 4, "4º ESO", 1 },
                    { 5, 1, "1º Bachillerato", 2 },
                    { 6, 2, "2º Bachillerato", 2 }
                });

            migrationBuilder.InsertData(
                table: "COURSE_HAS_SUBJECT",
                columns: new[] { "ID", "StudiesHasCourseId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 5, 1 },
                    { 2, 5, 2 },
                    { 3, 5, 3 },
                    { 4, 5, 4 },
                    { 5, 5, 5 },
                    { 6, 5, 6 },
                    { 7, 5, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADMIN_UserAccountId",
                table: "ADMIN",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_HAS_SUBJECT_StudiesHasCourseId",
                table: "COURSE_HAS_SUBJECT",
                column: "StudiesHasCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_HAS_SUBJECT_SubjectId",
                table: "COURSE_HAS_SUBJECT",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_HAS_MENU_MenuId",
                table: "ROLE_HAS_MENU",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_HAS_MENU_RoleId",
                table: "ROLE_HAS_MENU",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_UserAccountId",
                table: "STUDENT",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDIES_HAS_COURSE_CourseId",
                table: "STUDIES_HAS_COURSE",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDIES_HAS_COURSE_StudiesId",
                table: "STUDIES_HAS_COURSE",
                column: "StudiesId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ACCOUNT_RoleId",
                table: "USER_ACCOUNT",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_TOKEN_UserAccountId",
                table: "USER_TOKEN",
                column: "UserAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADMIN");

            migrationBuilder.DropTable(
                name: "COURSE_HAS_SUBJECT");

            migrationBuilder.DropTable(
                name: "ROLE_HAS_MENU");

            migrationBuilder.DropTable(
                name: "STUDENT");

            migrationBuilder.DropTable(
                name: "USER_TOKEN");

            migrationBuilder.DropTable(
                name: "STUDIES_HAS_COURSE");

            migrationBuilder.DropTable(
                name: "SUBJECT");

            migrationBuilder.DropTable(
                name: "MENU");

            migrationBuilder.DropTable(
                name: "USER_ACCOUNT");

            migrationBuilder.DropTable(
                name: "COURSE");

            migrationBuilder.DropTable(
                name: "STUDIES");

            migrationBuilder.DropTable(
                name: "ROLE");
        }
    }
}
