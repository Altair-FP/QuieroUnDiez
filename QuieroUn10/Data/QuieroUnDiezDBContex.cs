using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

using QuieroUn10.Models;
using Task = QuieroUn10.Models.Task;
using QuieroUn10.Utilities;

using System.Collections.Generic;
/*//Comando a ejecutar en el cmd, en el directorio del proyecto, para crear los archivos de la migración
dotnet ef migrations add nombreMigracion

//Comando a ejecutar en el cmd, en el directorio del proyecto, para actualizar la BD con los archivos creados durante la migración
dotnet ef database update

Si os falla porque no os lee el comando dotnet ef, ejecutar el comando:
dotnet tool install --global dotnet-ef
*/
namespace QuieroUn10.Data
{
    public class QuieroUnDiezDBContex : DbContext
    {
        public QuieroUnDiezDBContex()
        {

        }

        public QuieroUnDiezDBContex(DbContextOptions<QuieroUnDiezDBContex> options)
           : base(options)
        {

        }

        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<RoleHasMenu> RoleHasMenu { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Study> Studies { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<StudyHasSubject> StudyHasSubject { get; set; }
        public DbSet<CalendarTask> CalendarTask { get; set; }
        public DbSet<Doc> Doc { get; set; }
        public DbSet<StudentHasSubject> StudentHasSubject { get; set; }
        public DbSet<Task> Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().ToTable("USER_ACCOUNT");
            modelBuilder.Entity<RoleHasMenu>().ToTable("ROLE_HAS_MENU");
            modelBuilder.Entity<UserToken>().ToTable("USER_TOKEN");
            modelBuilder.Entity<Role>().ToTable("ROLE");
            modelBuilder.Entity<Student>().ToTable("STUDENT");
            modelBuilder.Entity<Admin>().ToTable("ADMIN");
            modelBuilder.Entity<Menu>().ToTable("MENU");
            modelBuilder.Entity<Study>().ToTable("STUDY");
            modelBuilder.Entity<Subject>().ToTable("SUBJECT");
            modelBuilder.Entity<StudyHasSubject>().ToTable("STUDY_HAS_SUBJECT");
            modelBuilder.Entity<CalendarTask>().ToTable("CALENDAR_TASK");
            modelBuilder.Entity<Doc>().ToTable("DOC");
            modelBuilder.Entity<StudentHasSubject>().ToTable("STUDENT_HAS_SUBJECT");
            modelBuilder.Entity<Task>().ToTable("TASK");

            modelBuilder.Entity<Student>()
               .HasOne(c => c.UserAccount)
               .WithMany()
               .HasForeignKey("UserAccountId")
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Admin>()
               .HasOne(c => c.UserAccount)
               .WithMany()
               .HasForeignKey("UserAccountId")
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Role>().HasData(
               new Role
               {
                   ID = 1,
                   Name = "ADMIN"
               },
               new Role
               {
                   ID = 2,
                   Name = "STUDENT"
               }       
           );

            modelBuilder.Entity<Menu>().HasData(

               new Menu
               {
                   ID = 1,
                   Controller = "UserAccounts",
                   Action = "Index",
                   Label = "User Accounts"
                  
               },
               new Menu
               {
                   ID = 2,
                   Controller = "Roles",
                   Action = "Index",
                   Label = "Roles"
                   
               },
               new Menu
               {
                   ID = 3,
                   Controller = "Students",
                   Action = "Index",
                   Label = "Students"
               },
               new Menu
               {
                   ID = 4,
                   Controller = "AdminDtoes",
                   Action = "Index",
                   Label = "Admins"
               },
               new Menu
               {
                   ID = 5,
                   Controller = "Menus",
                   Action = "Index",
                   Label = "Menus"
               },
               new Menu
               {
                   ID = 6,
                   Controller = "Studies",
                   Action = "Index",
                   Label = "Studies"
               },
               new Menu
               {
                   ID = 7,
                   Controller = "Subjects",
                   Action = "Index",
                   Label = "Subjects"
               },
               new Menu
               {
                   ID = 8,
                   Controller = "StudyHasSubjects",
                   Action = "Index",
                   Label = "Study Has Subjects"
               },
               new Menu
               {
                   ID = 9,
                   Controller = "CalendarTasks",
                   Action = "Index",
                   Label = "Calendario de tareas"
               },
               new Menu
               {
                   ID = 10,
                   Controller = "Docs",
                   Action = "Index",
                   Label = "Documents"
               },
               new Menu
               {
                   ID = 11,
                   Controller = "StudentHasSubjects",
                   Action = "Index",
                   Label = "Asignaturas"
               },
               new Menu
               {
                   ID = 12,
                   Controller = "Tasks",
                   Action = "AllIndex",
                   Label = "Tareas"
               },
               new Menu
               {
                   ID = 13,
                   Controller = "AdminDtoes",
                   Action = "Details",
                   Label = "Profile"
               },
               new Menu
               {
                   ID = 14,
                   Controller = "StudentDtoes",
                   Action = "Details",
                   Label = "Perfil"
               },
               new Menu
               {
                   ID = 15,
                   Controller = "Methods",
                   Action = "Index",
                   Label = "Método Pomodoro"
               },
               new Menu
               {
                   ID = 16,
                   Controller = "StudentHasSubjects",
                   Action = "IndexAdmin",
                   Label = "Student Subject"
               }
           );

            modelBuilder.Entity<RoleHasMenu>().HasData(
                //Admin   
                new RoleHasMenu
                {      
                    ID = 1,
                    RoleId = 1,
                    MenuId = 1
                },
                new RoleHasMenu
                {
                    ID = 2,
                    RoleId = 1,
                    MenuId = 2
                },
                new RoleHasMenu
                {
                    ID = 3,
                    RoleId = 1,
                    MenuId = 3
                },
                new RoleHasMenu
                {
                    ID = 4,
                    RoleId = 1,
                    MenuId = 4
                },
                new RoleHasMenu
                {
                    ID = 5,
                    RoleId = 1,
                    MenuId = 5
                },
                new RoleHasMenu
                {
                    ID = 6,
                    RoleId = 1,
                    MenuId = 6
                },
                new RoleHasMenu
                {
                    ID = 7,
                    RoleId = 1,
                    MenuId = 7
                },
                new RoleHasMenu
                {
                    ID = 8,
                    RoleId = 1,
                    MenuId = 8
                },
                 new RoleHasMenu
                 {
                     ID = 9,
                     RoleId = 1,
                     MenuId = 16
                 },

                //Menu Student
                new RoleHasMenu
                {
                    ID = 13,
                    RoleId = 2,
                    MenuId = 11
                },
                new RoleHasMenu
                {
                    ID = 14,
                    RoleId = 2,
                    MenuId = 12
                },
                new RoleHasMenu
                {
                    ID = 15,
                    RoleId = 2,
                    MenuId = 9
                },
                new RoleHasMenu
                {
                    ID = 16,
                    RoleId = 2,
                    MenuId = 14
                },
                new RoleHasMenu
                {
                    ID = 17,
                    RoleId = 2,
                    MenuId = 15
                }
            );

            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount
                {
                    ID = 1,
                    Username = "admin1",
                    Password = Utility.Encriptar("admin1"),
                    RoleId = 1,
                    Email = "admin1@gmail.com",
                    Active = true
                },
                new UserAccount
                {
                    ID = 2,
                    Username = "admin2",
                    Password = Utility.Encriptar("admin2"),
                    RoleId = 1,
                    Email = "admin2@gmail.com",
                    Active = false
                },
                 new UserAccount
                 {
                     ID = 3,
                     Username = "student1",
                     Password = Utility.Encriptar("student1"),
                     RoleId = 2,
                     Email = "student1@gmail.com",
                     Active = true
                 },
                 new UserAccount
                 {
                     ID = 4,
                     Username = "student2",
                     Password = Utility.Encriptar("student2"),
                     RoleId = 2,
                     Email = "student2@gmail.com",
                     Active = false
                 }

            );

            modelBuilder.Entity<Admin>().HasData(

                 new Admin
                 {
                     ID = 1,
                     Name = "Admin1",
                     Surname = "Admin1",
                     Phone = "698756483",
                     UserAccountId = 1
                 },
                 new Admin
                 {
                     ID = 2,
                     Name = "Admin2",
                     Surname = "Admin2",
                     Phone = "698756483",
                     UserAccountId = 2
                 }
             );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    ID =1,
                    Birthdate = new DateTime(1999,01,14),
                    CalendarTasks = new List<CalendarTask>(),
                    Name = "Alejandro",
                    Phone = "620730065",
                    StudentHasSubjects = new List<StudentHasSubject>(),
                    Surname = "Cruz",
                    UserAccountId = 3,
                    Activate = false
                },
                 new Student
                 {
                     ID = 2,
                     Birthdate = new DateTime(1999, 01, 14),
                     CalendarTasks = new List<CalendarTask>(),
                     Name = "Admin 2",
                     Phone = "666444555",
                     StudentHasSubjects = new List<StudentHasSubject>(),
                     Surname = "Admin",
                     UserAccountId = 4,
                     Activate = false
                 }
           );

            modelBuilder.Entity<Study>().HasData(
                new Study
                {
                    ID = 1,
                    Acronym = "DAW",
                    Name = "Desarrollo de Aplicaciones Web",
                    StudyHasSubjects = new List<StudyHasSubject>()
                },
                new Study
                {
                    ID = 2,
                    Acronym = "DAM",
                    Name = "Desarrollo de Aplicaciones Multiplataforma",
                    StudyHasSubjects = new List<StudyHasSubject>()
                },
                new Study
                {
                    ID = 3,
                    Acronym = "ASIR",
                    Name = "Administración de Sistemas Informáticos en Red",
                    StudyHasSubjects = new List<StudyHasSubject>()
                }
           );

            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    ID = 1,
                    Name = "Sistemas informáticos.",
                    Acronym = "SSII",
                    Course = "1",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 2,
                    Name = "Bases de datos",
                    Acronym = "BBDD",
                    Course = "1",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 3,
                    Name = "Programación",
                    Acronym = "Programación",
                    Course = "1",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 4,
                    Name = "Lenguajes de marcas y sistemas de gestión de información",
                    Acronym = "LM",
                    Course = "1",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 5,
                    Name = "Entornos de desarrollo",
                    Acronym = "ED",
                    Course = "1",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 6,
                    Name = "Formación y orientación laboral",
                    Acronym = "FOL.",
                    Course = "1",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 7,
                    Name = "Desarrollo web en entorno cliente",
                    Acronym = "DWEC",
                    Course = "2",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 8,
                    Name = "Desarrollo web en entorno servidor",
                    Acronym = "DWS",
                    Course = "2",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 9,
                    Name = "Despliegue de aplicaciones web",
                    Acronym = "DAW",
                    Course = "2",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 10,
                    Name = "Diseño de interfaces Web",
                    Acronym = "DIW",
                    Course = "2",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 11,
                    Name = "Empresa e iniciativa emprendedora",
                    Acronym = "Empresa",
                    Course = "2",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                new Subject
                {
                    ID = 12,
                    Name = "Proyecto de desarrollo de aplicaciones web",
                    Acronym = "TFG - DAW",
                    Course = "2",
                    Formal_Subject = true,
                    Student_Create = false,
                    StudyHasSubjects = new List<StudyHasSubject>(),
                    StudentHasSubjects = new List<StudentHasSubject>()
                },
                 new Subject
                 {
                     ID = 13,
                     Name = "Acceso a datos",
                     Acronym = "AD",
                     Course = "2",
                     Formal_Subject = true,
                     Student_Create = false,
                     StudyHasSubjects = new List<StudyHasSubject>(),
                     StudentHasSubjects = new List<StudentHasSubject>()
                 },
                  new Subject
                  {
                      ID = 14,
                      Name = "Desarrollo de interfaces",
                      Acronym = "DI",
                      Course = "2",
                      Formal_Subject = true,
                      Student_Create = false,
                      StudyHasSubjects = new List<StudyHasSubject>(),
                      StudentHasSubjects = new List<StudentHasSubject>()
                  },
                  new Subject
                  {
                      ID = 15,
                      Name = "Programación multimedia y dispositivos móviles.",
                      Acronym = "PMDM",
                      Course = "2",
                      Formal_Subject = true,
                      Student_Create = false,
                      StudyHasSubjects = new List<StudyHasSubject>(),
                      StudentHasSubjects = new List<StudentHasSubject>()
                  },
                  new Subject
                  {
                      ID = 16,
                      Name = "Programación de servicios y procesos",
                      Acronym = "PSP",
                      Course = "2",
                      Formal_Subject = true,
                      Student_Create = false,
                      StudyHasSubjects = new List<StudyHasSubject>(),
                      StudentHasSubjects = new List<StudentHasSubject>()
                  },
                  new Subject
                  {
                      ID = 17,
                      Name = "Sistemas de gestión empresarial",
                      Acronym = "SGE",
                      Course = "2",
                      Formal_Subject = true,
                      Student_Create = false,
                      StudyHasSubjects = new List<StudyHasSubject>(),
                      StudentHasSubjects = new List<StudentHasSubject>()
                  },
                   new Subject
                   {
                       ID = 18,
                       Name = "Proyecto de desarrollo de aplicaciones multiplataforma",
                       Acronym = "TFG - DAM",
                       Course = "2",
                       Formal_Subject = true,
                       Student_Create = false,
                       StudyHasSubjects = new List<StudyHasSubject>(),
                       StudentHasSubjects = new List<StudentHasSubject>()
                   }
                ) ;

            modelBuilder.Entity<StudyHasSubject>().HasData(
                new StudyHasSubject()
                {
                    ID =1,
                    StudyId = 1,
                    SubjectId = 1
                },
                new StudyHasSubject()
                {
                    ID = 2,
                    StudyId = 1,
                    SubjectId = 2
                },
                new StudyHasSubject()
                {
                    ID = 3,
                    StudyId = 1,
                    SubjectId = 3
                },
                new StudyHasSubject()
                {
                    ID = 4,
                    StudyId = 1,
                    SubjectId = 4
                },
                new StudyHasSubject()
                {
                    ID = 5,
                    StudyId = 1,
                    SubjectId = 5
                },
                new StudyHasSubject()
                {
                    ID = 6,
                    StudyId = 1,
                    SubjectId = 6
                },
                new StudyHasSubject()
                {
                    ID = 7,
                    StudyId = 1,
                    SubjectId = 7
                },
                new StudyHasSubject()
                {
                    ID = 8,
                    StudyId = 1,
                    SubjectId = 8
                },
                new StudyHasSubject()
                {
                    ID = 9,
                    StudyId = 1,
                    SubjectId = 9
                },
                new StudyHasSubject()
                {
                    ID = 10,
                    StudyId = 1,
                    SubjectId = 10
                },
                new StudyHasSubject()
                {
                    ID = 11,
                    StudyId = 1,
                    SubjectId = 11
                },
                new StudyHasSubject()
                {
                    ID = 12,
                    StudyId = 1,
                    SubjectId = 12
                },
                new StudyHasSubject()
                {
                    ID = 13,
                    StudyId = 2,
                    SubjectId = 1
                },
                 new StudyHasSubject()
                 {
                     ID = 14,
                     StudyId = 2,
                     SubjectId = 2
                 },
                  new StudyHasSubject()
                  {
                      ID = 15,
                      StudyId = 2,
                      SubjectId = 3
                  },
                   new StudyHasSubject()
                   {
                       ID = 16,
                       StudyId = 2,
                       SubjectId = 4
                   },
                    new StudyHasSubject()
                    {
                        ID = 17,
                        StudyId = 2,
                        SubjectId = 5
                    },
                     new StudyHasSubject()
                     {
                         ID = 18,
                         StudyId = 2,
                         SubjectId = 6
                     },
                     new StudyHasSubject()
                     {
                         ID = 19,
                         StudyId = 2,
                         SubjectId = 13
                     },
                     new StudyHasSubject()
                     {
                         ID = 20,
                         StudyId = 2,
                         SubjectId = 14
                     },
                     new StudyHasSubject()
                     {
                         ID = 21,
                         StudyId = 2,
                         SubjectId = 15
                     },
                     new StudyHasSubject()
                     {
                         ID = 22,
                         StudyId = 2,
                         SubjectId = 16
                     },
                      new StudyHasSubject()
                      {
                          ID = 23,
                          StudyId = 2,
                          SubjectId = 11
                      },
                     new StudyHasSubject()
                     {
                         ID = 24,
                         StudyId = 2,
                         SubjectId = 17
                     },
                     new StudyHasSubject()
                     {
                         ID = 25,
                         StudyId = 2,
                         SubjectId = 18
                     }
                );

        }

    }
}
