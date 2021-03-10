using Microsoft.EntityFrameworkCore.Migrations;

namespace QuieroUn10.Migrations
{
    public partial class casa2Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COURSE_HAS_SUBJECT");

            migrationBuilder.DropTable(
                name: "STUDIES_HAS_COURSE");

            migrationBuilder.DropTable(
                name: "COURSE");

            migrationBuilder.DropTable(
                name: "STUDIES");

            migrationBuilder.DeleteData(
                table: "SUBJECT",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SUBJECT",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SUBJECT",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SUBJECT",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SUBJECT",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SUBJECT",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SUBJECT",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.AddColumn<string>(
                name: "Acronym",
                table: "SUBJECT",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "SUBJECT",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_STUDY_HAS_SUBJECT_StudyId",
                table: "STUDY_HAS_SUBJECT",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDY_HAS_SUBJECT_SubjectId",
                table: "STUDY_HAS_SUBJECT",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDY_HAS_SUBJECT");

            migrationBuilder.DropTable(
                name: "STUDY");

            migrationBuilder.DropColumn(
                name: "Acronym",
                table: "SUBJECT");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "SUBJECT");

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
                name: "STUDIES_HAS_COURSE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudiesId = table.Column<int>(type: "int", nullable: false)
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
                name: "COURSE_HAS_SUBJECT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudiesHasCourseId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
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
                table: "STUDIES",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 4, "UNIVERSIDAD" },
                    { 3, "FP" },
                    { 1, "ESO" },
                    { 2, "BACHILLERATO" }
                });

            migrationBuilder.InsertData(
                table: "SUBJECT",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 6, "Dibujo Técnico I" },
                    { 1, "Matemáticas I" },
                    { 2, "Lengua Castellana y Literatura I" },
                    { 3, "Primera Lengua Extranjera I" },
                    { 4, "Filosofía" },
                    { 5, "Biología y Geología" },
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
                name: "IX_COURSE_HAS_SUBJECT_StudiesHasCourseId",
                table: "COURSE_HAS_SUBJECT",
                column: "StudiesHasCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_HAS_SUBJECT_SubjectId",
                table: "COURSE_HAS_SUBJECT",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDIES_HAS_COURSE_CourseId",
                table: "STUDIES_HAS_COURSE",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_STUDIES_HAS_COURSE_StudiesId",
                table: "STUDIES_HAS_COURSE",
                column: "StudiesId");
        }
    }
}
