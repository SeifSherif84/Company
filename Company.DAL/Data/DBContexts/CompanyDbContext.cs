using Company.DAL.Data.Configurations;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.DBContexts
{
    public class CompanyDbContext : DbContext
    {

        public CompanyDbContext() : base() 
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-C167I9J; Database = Company; Trusted_Connection = True; TrustServerCertificate = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfigurations());
        }

        public DbSet<Department> Departments { get; set; }
    }
}
