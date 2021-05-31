using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class proyectoterminadoMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MENU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Controller = table.Column<string>(maxLength: 30, nullable: false),
                    Action = table.Column<string>(maxLength: 20, nullable: false),
                    Label = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENU", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STUDY",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Acronym = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SUBJECT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Formal_Subject = table.Column<bool>(nullable: false),
                    Student_Create = table.Column<bool>(nullable: false),
                    Course = table.Column<string>(maxLength: 50, nullable: false),
                    Acronym = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUBJECT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_HAS_MENU",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    MenuId = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 10, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudyId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Surname = table.Column<string>(maxLength: 40, nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    UserAccountId = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Surname = table.Column<string>(maxLength: 40, nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    UserAccountId = table.Column<int>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Activate = table.Column<bool>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: false),
                    GeneratedDate = table.Column<DateTime>(nullable: false),
                    Life = table.Column<int>(nullable: false),
                    UserAccountId = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DayStart = table.Column<DateTime>(nullable: false),
                    DayEnd = table.Column<DateTime>(nullable: true),
                    StudentId = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InscriptionDate = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocByte = table.Column<byte[]>(nullable: true),
                    DocSourceFileName = table.Column<string>(nullable: true),
                    DocContentType = table.Column<string>(nullable: true),
                    StudentHasSubjectId = table.Column<int>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    AllDay = table.Column<bool>(nullable: false),
                    ClassName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    StudentHasSubjectId = table.Column<int>(nullable: false)
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
                    { 16, "IndexAdmin", "StudentHasSubjects", "Student Subject" },
                    { 15, "Index", "Methods", "Método Pomodoro" },
                    { 14, "Details", "StudentDtoes", "Perfil" },
                    { 13, "Details", "AdminDtoes", "Profile" },
                    { 12, "AllIndex", "Tasks", "Tareas" },
                    { 11, "Index", "StudentHasSubjects", "Asignaturas" },
                    { 9, "Index", "CalendarTasks", "Calendario de tareas" },
                    { 10, "Index", "Docs", "Documents" },
                    { 7, "Index", "Subjects", "Subjects" },
                    { 6, "Index", "Studies", "Studies" },
                    { 5, "Index", "Menus", "Menus" },
                    { 4, "Index", "Admins", "Admins" },
                    { 3, "Index", "Students", "Students" },
                    { 2, "Index", "Roles", "Roles" },
                    { 8, "Index", "StudyHasSubjects", "Study Has Subjects" }
                });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "Description", "Enabled", "Name" },
                values: new object[,]
                {
                    { 2, null, false, "STUDENT" },
                    { 1, null, false, "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "STUDY",
                columns: new[] { "ID", "Acronym", "Name" },
                values: new object[,]
                {
                    { 3, "ASIR", "Administración de Sistemas Informáticos en Red" },
                    { 2, "DAM", "Desarrollo de Aplicaciones Multiplataforma" },
                    { 1, "DAW", "Desarrollo de Aplicaciones Web" }
                });

            migrationBuilder.InsertData(
                table: "SUBJECT",
                columns: new[] { "ID", "Acronym", "Course", "Formal_Subject", "Name", "Student_Create" },
                values: new object[,]
                {
                    { 16, "PSP", "2", true, "Programación de servicios y procesos", false },
                    { 15, "PMDM", "2", true, "Programación multimedia y dispositivos móviles.", false },
                    { 14, "DI", "2", true, "Desarrollo de interfaces", false },
                    { 13, "AD", "2", true, "Acceso a datos", false },
                    { 12, "TFG - DAW", "2", true, "Proyecto de desarrollo de aplicaciones web", false },
                    { 11, "Empresa", "2", true, "Empresa e iniciativa emprendedora", false },
                    { 10, "DIW", "2", true, "Diseño de interfaces Web", false },
                    { 17, "SGE", "2", true, "Sistemas de gestión empresarial", false },
                    { 9, "DAW", "2", true, "Despliegue de aplicaciones web", false },
                    { 7, "DWEC", "2", true, "Desarrollo web en entorno cliente", false },
                    { 6, "FOL.", "1", true, "Formación y orientación laboral", false },
                    { 5, "ED", "1", true, "Entornos de desarrollo", false },
                    { 4, "LM", "1", true, "Lenguajes de marcas y sistemas de gestión de información", false },
                    { 3, "Programación", "1", true, "Programación", false },
                    { 2, "BBDD", "1", true, "Bases de datos", false },
                    { 1, "SSII", "1", true, "Sistemas informáticos.", false },
                    { 8, "DWS", "2", true, "Desarrollo web en entorno servidor", false },
                    { 18, "TFG - DAM", "2", true, "Proyecto de desarrollo de aplicaciones multiplataforma", false }
                });

            migrationBuilder.InsertData(
                table: "ROLE_HAS_MENU",
                columns: new[] { "ID", "MenuId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 17, 15, 2 },
                    { 16, 14, 2 },
                    { 15, 9, 2 },
                    { 13, 11, 2 },
                    { 10, 13, 1 },
                    { 9, 16, 1 },
                    { 14, 12, 2 },
                    { 7, 7, 1 },
                    { 6, 6, 1 },
                    { 5, 5, 1 },
                    { 4, 4, 1 },
                    { 3, 3, 1 },
                    { 2, 2, 1 },
                    { 8, 8, 1 }
                });

            migrationBuilder.InsertData(
                table: "STUDY_HAS_SUBJECT",
                columns: new[] { "ID", "StudyId", "SubjectId" },
                values: new object[,]
                {
                    { 7, 1, 7 },
                    { 8, 1, 8 },
                    { 9, 1, 9 },
                    { 10, 1, 10 },
                    { 11, 1, 11 },
                    { 20, 2, 14 },
                    { 12, 1, 12 },
                    { 19, 2, 13 },
                    { 18, 2, 6 },
                    { 21, 2, 15 },
                    { 22, 2, 16 },
                    { 23, 2, 11 },
                    { 6, 1, 6 },
                    { 2, 1, 2 },
                    { 5, 1, 5 },
                    { 16, 2, 4 },
                    { 4, 1, 4 },
                    { 15, 2, 3 },
                    { 3, 1, 3 },
                    { 14, 2, 2 },
                    { 24, 2, 17 },
                    { 13, 2, 1 },
                    { 1, 1, 1 },
                    { 17, 2, 5 },
                    { 25, 2, 18 }
                });

            migrationBuilder.InsertData(
                table: "USER_ACCOUNT",
                columns: new[] { "ID", "Active", "Email", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 4, false, "student2@gmail.com", "QQBsAHQAYQBpAHIAMQAyADMAJAAlAA==", 2, "student2" },
                    { 3, true, "student1@gmail.com", "QQBsAHQAYQBpAHIAMQAyADMAJAAlAA==", 2, "student1" },
                    { 2, false, "admin2@gmail.com", "QQBsAHQAYQBpAHIAMQAyADMAJAAlAA==", 1, "admin2" },
                    { 1, true, "admin1@gmail.com", "QQBsAHQAYQBpAHIAMQAyADMAJAAlAA==", 1, "admin1" }
                });

            migrationBuilder.InsertData(
                table: "ADMIN",
                columns: new[] { "ID", "Name", "Phone", "Surname", "UserAccountId" },
                values: new object[,]
                {
                    { 1, "Admin1", "698756483", "Admin1", 1 },
                    { 2, "Admin2", "698756483", "Admin2", 2 }
                });

            migrationBuilder.InsertData(
                table: "STUDENT",
                columns: new[] { "ID", "Activate", "Birthdate", "Name", "Phone", "Surname", "UserAccountId" },
                values: new object[,]
                {
                    { 1, false, new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student 1", "666444555", "Student 1", 3 },
                    { 2, false, new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student 2", "666444555", "Student 2", 4 }
                });

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
