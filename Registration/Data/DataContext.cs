using System;
using Microsoft.EntityFrameworkCore;
using Registration.Domain;

namespace Registration.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("tblEmployees").HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().Property(e => e.FirstName).HasColumnType("VARCHAR(100)");
            modelBuilder.Entity<Employee>().Property(e => e.LastName).HasColumnType("VARCHAR(100)");
            modelBuilder.Entity<Employee>().Property(e => e.Designation).HasColumnType("VARCHAR(100)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
