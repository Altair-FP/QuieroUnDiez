using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuieroUn10.Models;
using QuieroUn10.Dtos;

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
        public DbSet<StudentDto> StudentDto { get; set; }
        public DbSet<Student> Student { get; set; }

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


        }


    }
}
