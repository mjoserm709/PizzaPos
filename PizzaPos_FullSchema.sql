-- =============================================
-- PIZZA POS - FULL DATABASE SCHEMA (SQLite)
-- =============================================

PRAGMA foreign_keys = ON;

-- 1. SECURITY & IDENTITY
CREATE TABLE Roles (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1
);

CREATE TABLE Permissions (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    Description TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1
);

CREATE TABLE RolePermissions (
    RoleId INTEGER NOT NULL,
    PermissionId INTEGER NOT NULL,
    PRIMARY KEY (RoleId, PermissionId),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE,
    FOREIGN KEY (PermissionId) REFERENCES Permissions(Id) ON DELETE CASCADE
);

CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    RoleId INTEGER NOT NULL,
    IdentityNumber TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

-- 2. CONFIGURATION & CATALOGS
CREATE TABLE AppConfigs (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    [Key] TEXT NOT NULL UNIQUE,
    Value TEXT NOT NULL,
    Description TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1
);

CREATE TABLE ProductCategories (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Code TEXT NOT NULL UNIQUE,
    Name TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1
);

CREATE TABLE ProductSizes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Code TEXT NOT NULL UNIQUE,
    Name TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1
);

CREATE TABLE OrderStatuses (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Code TEXT NOT NULL UNIQUE,
    Name TEXT NOT NULL,
    DisplayOrder INTEGER NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1
);

CREATE TABLE PaymentMethods (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Code TEXT NOT NULL UNIQUE,
    Name TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1
);

-- 3. BUSINESS ENTITIES
CREATE TABLE Products (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CategoryId INTEGER NOT NULL,
    SizeId INTEGER,
    Name TEXT NOT NULL,
    Description TEXT,
    Price REAL NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1,
    FOREIGN KEY (CategoryId) REFERENCES ProductCategories(Id),
    FOREIGN KEY (SizeId) REFERENCES ProductSizes(Id)
);

CREATE TABLE Customers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Phone TEXT NOT NULL UNIQUE,
    Email TEXT UNIQUE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1
);

CREATE TABLE Addresses (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INTEGER NOT NULL,
    Street TEXT NOT NULL,
    Number TEXT,
    Sector TEXT,
    City TEXT,
    Reference TEXT,
    IsPrimary INTEGER DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
);

CREATE TABLE Orders (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderNumber TEXT NOT NULL UNIQUE,
    CustomerId INTEGER NOT NULL,
    AddressId INTEGER,
    StatusId INTEGER NOT NULL,
    PaymentMethodId INTEGER NOT NULL,
    Subtotal REAL NOT NULL DEFAULT 0,
    TaxAmount REAL NOT NULL DEFAULT 0,
    Total REAL NOT NULL DEFAULT 0,
    Notes TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY (AddressId) REFERENCES Addresses(Id),
    FOREIGN KEY (StatusId) REFERENCES OrderStatuses(Id),
    FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethods(Id)
);

CREATE TABLE OrderDetails (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL,
    ProductId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    UnitPrice REAL NOT NULL,
    Total REAL NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

CREATE TABLE Deliveries (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL UNIQUE,
    CourierId INTEGER,
    DeliveryStatusId INTEGER NOT NULL,
    AssignedAt DATETIME,
    DeliveredAt DATETIME,
    Notes TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy TEXT DEFAULT 'System',
    UpdatedAt DATETIME,
    UpdatedBy TEXT,
    IsActive INTEGER DEFAULT 1,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (CourierId) REFERENCES Users(Id)
);

-- 4. INITIAL SEED DATA
INSERT INTO Roles (Name) VALUES ('admin'), ('cajero'), ('cocinero'), ('repartidor');

INSERT INTO Permissions (Name, Description) VALUES 
('clientes.create', 'Crear clientes'),
('clientes.read', 'Ver clientes'),
('clientes.update', 'Actualizar clientes'),
('clientes.delete', 'Eliminar clientes'),
('usuarios.create', 'Crear usuarios'),
('roles.manage', 'Gestionar roles');

-- Admin user (password_hash debe ser generado por la app, aquí usamos uno temporal)
INSERT INTO Users (FullName, Email, PasswordHash, RoleId) 
VALUES ('Administrador', 'admin@pizzeria.com', 'admin123', 1);

INSERT INTO OrderStatuses (Code, Name, DisplayOrder) VALUES 
('pendiente', 'Pendiente', 1),
('confirmado', 'Confirmado', 2),
('en_preparacion', 'En preparación', 3),
('listo', 'Listo', 4),
('en_camino', 'En camino', 5),
('entregado', 'Entregado', 6),
('cancelado', 'Cancelado', 7);

INSERT INTO PaymentMethods (Code, Name) VALUES 
('efectivo', 'Efectivo'),
('tarjeta', 'Tarjeta'),
('transferencia', 'Transferencia');

INSERT INTO ProductCategories (Code, Name) VALUES 
('pizza', 'Pizza'),
('bebida', 'Bebida');

INSERT INTO AppConfigs ([Key], Value, Description) 
VALUES ('IVA_PERCENTAGE', '15', 'Porcentaje de IVA aplicado');
