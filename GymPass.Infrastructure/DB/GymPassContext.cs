using GymPass.Domain.Entities;
using GymPass.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GymPass.Infrastructure.DB;

public class GymPassContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<CheckIn> CheckIns { get; set; }
    public DbSet<Gym> Gyms { get; set; }
    
    public GymPassContext(DbContextOptions<GymPassContext> options) : base(options: options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gym>(gym =>
        {
            gym.HasKey(g => g.Id);
            gym.Property(g => g.Id).HasMaxLength(64).IsRequired();
            gym.Property(g => g.Title).IsRequired().HasMaxLength(255);
            gym.Property(g => g.Description).HasMaxLength(500);
            gym.Property(g => g.Phone);
            gym.Property(g => g.CreatedAt).IsRequired();

            gym.OwnsOne(g => g.Cordinate, cordinate =>
            {
                cordinate.Property(c => c.Latitude).HasColumnName("Latitude").IsRequired();
                cordinate.Property(c => c.Longitude).HasColumnName("Longitude").IsRequired();
            });
        });
        
        modelBuilder.Entity<CheckIn>()
            .HasOne(c => c.Gym)
            .WithMany(g => g.CheckIns)
            .HasForeignKey(c => c.GymId);
        
        base.OnModelCreating(modelBuilder);
    }
}
    
