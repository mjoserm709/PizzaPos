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
            
        // --- SEED DATA ---
        var now = DateTime.Now;

        // 1. Permissions
        modelBuilder.Entity<Permission>().HasData(
            new Permission { Id = 1, Name = "users.create", Description = "Crear nuevos usuarios", IsActive = true, CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 2, Name = "users.delete", Description = "Eliminar usuarios", IsActive = true, CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 3, Name = "users.view", Description = "Ver lista de usuarios", IsActive = true, CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 4, Name = "users.edit", Description = "Editar usuarios existentes", IsActive = true, CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 5, Name = "security.roles.manage", Description = "Gestionar roles y permisos", IsActive = true, CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 6, Name = "security.permissions.view", Description = "Ver catálogo de permisos", IsActive = true, CreatedAt = now, CreatedBy = "System" }
        );

        // 2. Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "admin", IsActive = true, CreatedAt = now, CreatedBy = "System" },
            new Role { Id = 2, Name = "editor", IsActive = true, CreatedAt = now, CreatedBy = "System" }
        );

        // 3. Admin User
        modelBuilder.Entity<User>().HasData(new User { 
            Id = 1, 
            Username = "admin", 
            PasswordHash = "admin123", 
            FullName = "Administrador Sistema",
            IdentityNumber = "0000-0000-00000",
            CreatedAt = now,
            CreatedBy = "System",
            IsActive = true 
        });

        // 4. Join Table: User - Roles (Admin user is admin role)
        modelBuilder.Entity("Admin_UserRoles").HasData(new { UserId = 1, RoleId = 1 });

        // 5. Join Table: Role - Permissions (Admin role has all permissions)
        modelBuilder.Entity("Admin_RolePermissions").HasData(
            new { RoleId = 1, PermissionId = 1 },
            new { RoleId = 1, PermissionId = 2 },
            new { RoleId = 1, PermissionId = 3 },
            new { RoleId = 1, PermissionId = 4 },
            new { RoleId = 1, PermissionId = 5 },
            new { RoleId = 1, PermissionId = 6 }
        );
    }
}
