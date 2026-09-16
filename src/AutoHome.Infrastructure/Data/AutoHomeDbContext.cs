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
    public DbSet<Light> Lights => Set<Light>();

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

        modelBuilder.Entity<Light>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.DeviceId).IsUnique();
            entity.Property(u => u.DeviceId).IsRequired().HasMaxLength(256);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(200);
            entity.Property(u => u.IpAddress).HasMaxLength(45);
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasData(new Config { Id = 1, RegisterRouteEnabled = true });
        });
    }
}