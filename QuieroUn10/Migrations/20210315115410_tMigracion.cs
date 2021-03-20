using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class tMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "STUDY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SUBJECT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Course = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "STUDY_HAS_SUBJECT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDY_HAS_SUBJECT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STUDY_HAS_SUBJECT_STUDY_StudyId",
                        column: x => x.StudyId,
                        principalTable: "STUDY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDY_HAS_SUBJECT_SUBJECT_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SUBJECT",
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
                name: "CALENDAR_TASK",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CALENDAR_TASK", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CALENDAR_TASK_STUDENT_StudentId",
                        column: x => x.StudentId,
                        principalTable: "STUDENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT_HAS_SUBJECT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT_HAS_SUBJECT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STUDENT_HAS_SUBJECT_STUDENT_StudentId",
                        column: x => x.StudentId,
                        principalTable: "STUDENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDENT_HAS_SUBJECT_SUBJECT_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SUBJECT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DOC",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocByte = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DocSourceFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentHasSubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DOC_STUDENT_HAS_SUBJECT_StudentHasSubjectId",
                        column: x => x.StudentHasSubjectId,
                        principalTable: "STUDENT_HAS_SUBJECT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TASK",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StudentHasSubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TASK_STUDENT_HAS_SUBJECT_StudentHasSubjectId",
                        column: x => x.StudentHasSubjectId,
                        principalTable: "STUDENT_HAS_SUBJECT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MENU",
                columns: new[] { "ID", "Action", "Controller", "Label" },
                values: new object[,]
                {
                    { 1, "Index", "UserAccounts", "User Accounts" },
                    { 2, "Index", "Roles", "Roles" },
                    { 3, "Index", "Students", "Students" },
                    { 4, "Index", "Admins", "Admins" },
                    { 5, "Index", "Menus", "Menus" },
                    { 6, "Index", "Studies", "Studies" },
                    { 7, "Index", "Subjects", "Subjects" },
                    { 8, "Index", "StudyHasSubjects", "Study Has Subjects" },
                    { 9, "Index", "CalendarTasks", "Calendar Tasks" },
                    { 10, "Index", "Docs", "Documents" },
                    { 11, "Index", "StudentHasSubjects", "Student Subjects" },
                    { 12, "Index", "Tasks", "Tasks" },
                    { 13, "Details", "AdminDtoes", "Profile" },
                    { 14, "Details", "StudentDtoes", "Profile" }
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
                table: "ROLE_HAS_MENU",
                columns: new[] { "ID", "MenuId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 15, 12, 2 },
                    { 14, 10, 2 },
                    { 13, 9, 2 },
                    { 12, 12, 1 },
                    { 11, 11, 1 },
                    { 16, 14, 2 },
                    { 9, 9, 1 },
                    { 10, 10, 1 },
                    { 7, 7, 1 },
                    { 6, 6, 1 },
                    { 5, 5, 1 },
                    { 4, 4, 1 },
                    { 3, 3, 1 },
                    { 2, 2, 1 },
                    { 8, 8, 1 }
                });

            migrationBuilder.InsertData(
                table: "USER_ACCOUNT",
                columns: new[] { "ID", "Active", "Email", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, true, "admin1@gmail.com", "YQBkAG0AaQBuADEA", 1, "admin1" },
                    { 2, true, "admin2@gmail.com", "YQBkAG0AaQBuADIA", 1, "admin2" },
                    { 3, true, "student1@gmail.com", "cwB0AHUAZABlAG4AdAAxAA==", 2, "student1" }
                });

            migrationBuilder.InsertData(
                table: "ADMIN",
                columns: new[] { "ID", "Name", "Phone", "Surname", "UserAccountId" },
                values: new object[] { 1, "Admin1", "698756483", "Admin1", 1 });

            migrationBuilder.InsertData(
                table: "ADMIN",
                columns: new[] { "ID", "Name", "Phone", "Surname", "UserAccountId" },
                values: new object[] { 2, "Admin2", "698756483", "Admin2", 2 });

            migrationBuilder.InsertData(
                table: "STUDENT",
                columns: new[] { "ID", "Birthdate", "Name", "Phone", "Surname", "UserAccountId" },
                values: new object[] { 1, new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alejandro", "620730065", "Cruz", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_ADMIN_UserAccountId",
                table: "ADMIN",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CALENDAR_TASK_StudentId",
                table: "CALENDAR_TASK",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_StudentHasSubjectId",
                table: "DOC",
                column: "StudentHasSubjectId");

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
                name: "IX_STUDENT_HAS_SUBJECT_StudentId",
                table: "STUDENT_HAS_SUBJECT",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_HAS_SUBJECT_SubjectId",
                table: "STUDENT_HAS_SUBJECT",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDY_HAS_SUBJECT_StudyId",
                table: "STUDY_HAS_SUBJECT",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDY_HAS_SUBJECT_SubjectId",
                table: "STUDY_HAS_SUBJECT",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_StudentHasSubjectId",
                table: "TASK",
                column: "StudentHasSubjectId");

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
                name: "CALENDAR_TASK");

            migrationBuilder.DropTable(
                name: "DOC");

            migrationBuilder.DropTable(
                name: "ROLE_HAS_MENU");

            migrationBuilder.DropTable(
                name: "STUDY_HAS_SUBJECT");

            migrationBuilder.DropTable(
                name: "TASK");

            migrationBuilder.DropTable(
                name: "USER_TOKEN");

            migrationBuilder.DropTable(
                name: "MENU");

            migrationBuilder.DropTable(
                name: "STUDY");

            migrationBuilder.DropTable(
                name: "STUDENT_HAS_SUBJECT");

            migrationBuilder.DropTable(
                name: "STUDENT");

            migrationBuilder.DropTable(
                name: "SUBJECT");

            migrationBuilder.DropTable(
                name: "USER_ACCOUNT");

            migrationBuilder.DropTable(
                name: "ROLE");
        }
    }
}
