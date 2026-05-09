using Microsoft.EntityFrameworkCore;
using PizzaPos.Domain.Entities;

namespace PizzaPos.Infrastructure.Data;

public class PizzaPosDbContext : DbContext
{
    public PizzaPosDbContext(DbContextOptions<PizzaPosDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // SQLite doesn't support schemas. Using table prefixes for 'admin' logical separation.
        modelBuilder.Entity<User>().ToTable("Admin_Users");
        modelBuilder.Entity<Role>().ToTable("Admin_Roles");
        modelBuilder.Entity<Permission>().ToTable("Admin_Permissions");

        // Many-to-many relationship for Role-Permission
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "Admin_RolePermissions",
                j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j => j.ToTable("Admin_RolePermissions")
            );

        // User-Role relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        // Seed Data
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Waiter" }
        );

        modelBuilder.Entity<Permission>().HasData(
            new Permission { Id = 1, Name = "Menu.View" },
            new Permission { Id = 2, Name = "Order.Create" },
            new Permission { Id = 3, Name = "Admin.Manage" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = "admin123", RoleId = 1 }
        );
    }
}
