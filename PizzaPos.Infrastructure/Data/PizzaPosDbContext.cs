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
    
    // Configuración
    public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
    public DbSet<PaymentStatus> PaymentStatuses { get; set; } = null!;
    public DbSet<DeliveryStatus> DeliveryStatuses { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
    public DbSet<ProductSize> ProductSizes { get; set; } = null!;

    // Negocio
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public DbSet<Delivery> Deliveries { get; set; } = null!;
    public DbSet<AppConfig> Configs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeo de Tablas (Coincidiendo con PizzaPos_FullSchema.sql)
        modelBuilder.Entity<AppConfig>().ToTable("AppConfigs");
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<Permission>().ToTable("Permissions");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<OrderStatus>().ToTable("OrderStatuses");
        modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatuses");
        modelBuilder.Entity<DeliveryStatus>().ToTable("DeliveryStatuses");
        modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethods");
        modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories");
        modelBuilder.Entity<ProductSize>().ToTable("ProductSizes");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Address>().ToTable("Addresses");
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");
        modelBuilder.Entity<Delivery>().ToTable("Deliveries");

        // Relación Muchos a Muchos Roles-Permisos (Configuración única)
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "rol_permisos",
                j => j.HasOne<Permission>().WithMany().HasForeignKey("permiso_id"),
                j => j.HasOne<Role>().WithMany().HasForeignKey("rol_id"),
                j => {
                    j.HasKey("rol_id", "permiso_id");
                    j.ToTable("rol_permisos");
                });

        var now = DateTime.Now;

        // --- SEED DATA ---
        
        // 1. Estados de Pedido
        modelBuilder.Entity<OrderStatus>().HasData(
            new OrderStatus { Id = 1, Code = "pendiente", Name = "Pendiente", Order = 1, CreatedAt = now, CreatedBy = "System" },
            new OrderStatus { Id = 2, Code = "confirmado", Name = "Confirmado", Order = 2, CreatedAt = now, CreatedBy = "System" },
            new OrderStatus { Id = 3, Code = "en_preparacion", Name = "En preparación", Order = 3, CreatedAt = now, CreatedBy = "System" },
            new OrderStatus { Id = 4, Code = "listo", Name = "Listo", Order = 4, CreatedAt = now, CreatedBy = "System" },
            new OrderStatus { Id = 5, Code = "en_camino", Name = "En camino", Order = 5, CreatedAt = now, CreatedBy = "System" },
            new OrderStatus { Id = 6, Code = "entregado", Name = "Entregado", Order = 6, CreatedAt = now, CreatedBy = "System" },
            new OrderStatus { Id = 7, Code = "cancelado", Name = "Cancelado", Order = 7, CreatedAt = now, CreatedBy = "System" }
        );

        // 2. Estados de Pago
        modelBuilder.Entity<PaymentStatus>().HasData(
            new PaymentStatus { Id = 1, Code = "pendiente", Name = "Pendiente", CreatedAt = now, CreatedBy = "System" },
            new PaymentStatus { Id = 2, Code = "pagado", Name = "Pagado", CreatedAt = now, CreatedBy = "System" }
        );

        // 3. Métodos de Pago
        modelBuilder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { Id = 1, Code = "efectivo", Name = "Efectivo", CreatedAt = now, CreatedBy = "System" },
            new PaymentMethod { Id = 2, Code = "tarjeta", Name = "Tarjeta", CreatedAt = now, CreatedBy = "System" },
            new PaymentMethod { Id = 3, Code = "transferencia", Name = "Transferencia", CreatedAt = now, CreatedBy = "System" }
        );

        // 4. Categorías y Tamaños
        modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory { Id = 1, Code = "pizza", Name = "Pizza", CreatedAt = now, CreatedBy = "System" },
            new ProductCategory { Id = 2, Code = "bebida", Name = "Bebida", CreatedAt = now, CreatedBy = "System" }
        );

        modelBuilder.Entity<ProductSize>().HasData(
            new ProductSize { Id = 1, Code = "personal", Name = "Personal", CreatedAt = now, CreatedBy = "System" },
            new ProductSize { Id = 2, Code = "mediana", Name = "Mediana", CreatedAt = now, CreatedBy = "System" },
            new ProductSize { Id = 3, Code = "familiar", Name = "Familiar", CreatedAt = now, CreatedBy = "System" }
        );

        // 5. Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "admin", CreatedAt = now, CreatedBy = "System" },
            new Role { Id = 2, Name = "cajero", CreatedAt = now, CreatedBy = "System" }
        );

        // 6. Permisos (Listado Completo)
        var permissions = new List<Permission>
        {
            new Permission { Id = 1, Name = "clientes.create", Description = "Crear clientes", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 2, Name = "clientes.read", Description = "Ver clientes", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 3, Name = "clientes.update", Description = "Actualizar clientes", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 4, Name = "clientes.delete", Description = "Eliminar clientes", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 5, Name = "productos.create", Description = "Crear productos", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 6, Name = "productos.read", Description = "Ver productos", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 7, Name = "productos.update", Description = "Actualizar productos", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 8, Name = "productos.delete", Description = "Eliminar productos", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 9, Name = "usuarios.create", Description = "Crear usuarios", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 10, Name = "usuarios.read", Description = "Ver usuarios", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 11, Name = "usuarios.update", Description = "Actualizar usuarios", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 12, Name = "usuarios.delete", Description = "Eliminar usuarios", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 13, Name = "roles.manage", Description = "Administrar roles", CreatedAt = now, CreatedBy = "System" },
            new Permission { Id = 14, Name = "permissions.manage", Description = "Administrar permisos", CreatedAt = now, CreatedBy = "System" }
        };
        modelBuilder.Entity<Permission>().HasData(permissions);

        // 7. Mapeo Admin -> Todos los permisos
        var adminPerms = permissions.Select(p => new { rol_id = 1, permiso_id = p.Id }).ToList();
        modelBuilder.Entity("rol_permisos").HasData(adminPerms);

        // 8. Usuario Admin Principal
        modelBuilder.Entity<User>().HasData(new User { 
            Id = 1, 
            FullName = "Administrador",
            Email = "admin@pizzeria.com", 
            PasswordHash = "admin123", 
            RoleId = 1,
            IdentityNumber = "0000-0000-00000",
            CreatedAt = now,
            CreatedBy = "System",
            IsActive = true 
        });

        // 9. Productos Iniciales
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, CategoryId = 1, SizeId = 2, Name = "Pizza Pepperoni Mediana", Price = 450.00m, CreatedAt = now, CreatedBy = "System" },
            new Product { Id = 2, CategoryId = 1, SizeId = 3, Name = "Pizza Familiar Mixta", Price = 750.00m, CreatedAt = now, CreatedBy = "System" }
        );
        // 10. Configuración del Sistema
        modelBuilder.Entity<AppConfig>().HasData(
            new AppConfig { Id = 1, Key = "IVA_PERCENTAGE", Value = "15", Description = "Porcentaje de IVA aplicado a las ventas", CreatedAt = now, CreatedBy = "System" }
        );
    }
}
