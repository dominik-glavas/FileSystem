using FileSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FileSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<AppFile> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Folder>()
                .HasIndex(f => new { f.Name, f.Path })
                .IsUnique();

            builder.Entity<AppFile>()
                .HasIndex(f => new { f.Name, f.Path })
                .IsUnique();
        }
    }
}
