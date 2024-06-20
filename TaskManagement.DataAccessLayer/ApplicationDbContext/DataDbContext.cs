using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TaskManagement.DataAccessLayer.ApplicationDbContext.Configuration;
using TaskManagement.DataAccessLayer.DataModels;

namespace TaskManagement.DataAccessLayer.ApplicationDbContext
{
    public class DataDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string text = File.ReadAllText(@"..\TaskManagement.DataAccessLayer\ApplicationDbContext\ConnectionString.json");
            var connectionString = JsonSerializer.Deserialize<ConnectionString>(text);

            optionsBuilder.UseSqlServer(connectionString.DefaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new SystemParameterConfiguration());
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<SystemParameter> SystemParameters { get; set; }
    }
}
