using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Entities;

namespace TaskManager.Api.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(x => x.Title).IsRequired().HasMaxLength(80);
                entity.Property(x => x.Description).HasMaxLength(500);

                entity.Property(x => x.Status).IsRequired();
                entity.Property(x => x.Priority).IsRequired();

                entity.Property(x => x.DueDate);
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.UpdatedAt);

                entity.HasIndex(x => x.CreatedAt);
                entity.HasIndex(x => x.Status);
                entity.HasIndex(x => x.Priority);
            });
        }
    }
}

