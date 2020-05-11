using System;
using Microsoft.EntityFrameworkCore;
using Registration.Domain;

namespace Registration.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbServer = Environment.GetEnvironmentVariable("PG_SERVER");
            string dbPort = Environment.GetEnvironmentVariable("PG_SERVER_PORT");
            string databaseName = Environment.GetEnvironmentVariable("PG_DATABASE");
            string dbUsername = Environment.GetEnvironmentVariable("PG_USERNAME");
            string dbPassword = Environment.GetEnvironmentVariable("PG_PASSWORD");

            var connectionString = $"Server={dbServer};Port={dbPort};Database={databaseName};Username={dbUsername};Password={dbPassword}";
            optionsBuilder.UseNpgsql(connectionString);


            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("tblEmployees").HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().Property(e => e.FirstName).HasColumnType("VARCHAR(100)");
            modelBuilder.Entity<Employee>().Property(e => e.LastName).HasColumnType("VARCHAR(100)");
            modelBuilder.Entity<Employee>().Property(e => e.Designation).HasColumnType("VARCHAR(100)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
