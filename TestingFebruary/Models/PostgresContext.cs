using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestingFebruary.Models
{
    public class PostgresContext :DbContext
    {
        public DbSet<Person> usersql { get; set; }

        public PostgresContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost; Port=5432;Database=testmp;Username=postgres;Password=fuckdas26#l;");
            optionsBuilder.UseNpgsql("server=localhost; port=5432;UserId=postgres;Password=fuckdas26#l;database=managerpackage;");
            //optionsBuilder.UseMySql("server=localhost; port=3306;UserId=root;Password=nurma26#l;database=testpsql;");
        }
    }
}
