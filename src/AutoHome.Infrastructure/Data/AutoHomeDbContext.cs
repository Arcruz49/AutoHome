using AutoHome.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoHome.Infrastructure.Data;

public class AutoHomeDbContext : DbContext
{
    public AutoHomeDbContext(DbContextOptions<AutoHomeDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Config> Configs => Set<Config>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasData(new Config { Id = 1, RegisterRouteEnabled = true });
        });
    }
}