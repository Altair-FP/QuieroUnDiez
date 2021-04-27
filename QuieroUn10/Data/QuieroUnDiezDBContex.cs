using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuieroUn10.Models;
using QuieroUn10.Dtos;
using Task = QuieroUn10.Models.Task;
using QuieroUn10.Utilities;

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
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
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
                   Controller = "Admins",
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
                   Label = "Calendar Tasks"
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
                   Label = "Subjects"
               },
               new Menu
               {
                   ID = 12,
                   Controller = "Tasks",
                   Action = "Index",
                   Label = "Tasks"
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
                   Label = "Profile"
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
                    MenuId = 9
                },
                new RoleHasMenu
                {
                    ID = 10,
                    RoleId = 1,
                    MenuId = 10
                },
                new RoleHasMenu
                {
                    ID = 11,
                    RoleId = 1,
                    MenuId = 11
                },
                new RoleHasMenu
                {
                    ID = 12,
                    RoleId = 1,
                    MenuId = 12
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
                    Active = true
                },
                 new UserAccount
                 {
                     ID = 3,
                     Username = "student1",
                     Password = Utility.Encriptar("student1"),
                     RoleId = 2,
                     Email = "student1@gmail.com",
                     Active = true
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
                }
           );

        }

    }
}
