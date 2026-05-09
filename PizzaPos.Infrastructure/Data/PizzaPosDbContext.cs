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

        modelBuilder.Entity<User>().ToTable("Admin_Users");
        modelBuilder.Entity<Role>().ToTable("Admin_Roles");
        modelBuilder.Entity<Permission>().ToTable("Admin_Permissions");

        // User - Roles (Many-to-Many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "Admin_UserRoles",
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j => j.ToTable("Admin_UserRoles")
            );

        // Role - Permissions (Many-to-Many)
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "Admin_RolePermissions",
                j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j => j.ToTable("Admin_RolePermissions")
            );

        // User - Additional Permissions (Many-to-Many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.AdditionalPermissions)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "Admin_UserPermissions",
                j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j => j.ToTable("Admin_UserPermissions")
            );
            
        // Initial Seed
        modelBuilder.Entity<Role>().HasData(new Role 
        { 
            Id = 1, 
            Name = "Admin",
            CreatedAt = DateTime.Now,
            CreatedBy = "System",
            IsActive = true
        });
        modelBuilder.Entity<User>().HasData(new User { 
            Id = 1, 
            Username = "admin", 
            PasswordHash = "admin123", 
            FullName = "Administrador Sistema",
            IdentityNumber = "0000-0000-00000",
            CreatedAt = DateTime.Now,
            CreatedBy = "System",
            IsActive = true 
        });

        // Relación Admin - Usuario
        modelBuilder.Entity("Admin_UserRoles").HasData(new { UserId = 1, RoleId = 1 });
    }
}
