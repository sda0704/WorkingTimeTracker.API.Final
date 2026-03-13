using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using WorkingTimeTracker.DataAccess.Configuration;
using WorkingTimeTracker.DataAccess.Entities;

namespace WorkingTimeTracker.DataAccess
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        { 
        }

        public DbSet<ProjectsEntity> Projects { get; set; }
        public DbSet<TasksEntity> Tasks { get; set; }
        public DbSet<TimeEntryEntity> TimeEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectsConfiguration());
            modelBuilder.ApplyConfiguration(new TasksConfiguration());
            modelBuilder.ApplyConfiguration(new TimeEntryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
       

    }
}
