using System;
using CacheService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CacheService.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbServer = Environment.GetEnvironmentVariable("PG_SERVER");
            string dbPort = Environment.GetEnvironmentVariable("PG_SERVER_PORT");
            string databaseName = Environment.GetEnvironmentVariable("PG_DATABASE");
            string dbUsername = Environment.GetEnvironmentVariable("PG_USERNAME");
            string dbPassword = Environment.GetEnvironmentVariable("PG_PASSWORD");

            var connectionString = $"Server={dbServer};Port={dbPort};Database={databaseName};Username={dbUsername};Password={dbPassword}";

            if (string.IsNullOrEmpty(dbServer))
            {
                connectionString = configuration.GetValue<string>("ConnectionString");
            }

            optionsBuilder.UseNpgsql(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().ToTable("tblSkills").HasKey(s => s.Id);
            modelBuilder.Entity<Skill>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Skill>().Property(s => s.SkillName).HasColumnType("VARCHAR(50)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
