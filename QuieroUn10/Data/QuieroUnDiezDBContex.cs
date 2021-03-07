using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuieroUn10.Models;

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

        }

    }
}
