using Microsoft.EntityFrameworkCore;
using TaskList.Models;

namespace TaskList.Services
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TodoTask> Tasks { get; set; }
        public DbSet<TimeTracking> TimeTrackings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=tasklist.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoTask>()
                .HasMany(t => t.TimeTrackings)
                .WithOne(tt => tt.Task)
                .HasForeignKey(tt => tt.TaskId);
        }
    }
}
