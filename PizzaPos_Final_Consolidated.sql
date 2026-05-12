/*
================================================================================
PIZZA POS - CONSOLIDATED SQL SERVER SCRIPT
================================================================================
Este script contiene el esquema completo y los datos iniciales optimizados.
Compatible con SQL Server.
================================================================================
*/

IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'PizzaPos'
)
BEGIN
    CREATE DATABASE PizzaPos;
END
GO

USE PizzaPos
GO

-- 1. LIMPIEZA (Orden inverso de dependencias)
IF OBJECT_ID('dbo.UserPermissions', 'U') IS NOT NULL DROP TABLE dbo.UserPermissions;
IF OBJECT_ID('dbo.RolePermissions', 'U') IS NOT NULL DROP TABLE dbo.RolePermissions;
IF OBJECT_ID('dbo.Deliveries', 'U') IS NOT NULL DROP TABLE dbo.Deliveries;
IF OBJECT_ID('dbo.OrderDetails', 'U') IS NOT NULL DROP TABLE dbo.OrderDetails;
IF OBJECT_ID('dbo.Compensations', 'U') IS NOT NULL DROP TABLE dbo.Compensations;
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.Addresses', 'U') IS NOT NULL DROP TABLE dbo.Addresses;
IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL DROP TABLE dbo.Products;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL DROP TABLE dbo.Roles;
IF OBJECT_ID('dbo.Permissions', 'U') IS NOT NULL DROP TABLE dbo.Permissions;
IF OBJECT_ID('dbo.AppConfigs', 'U') IS NOT NULL DROP TABLE dbo.AppConfigs;
IF OBJECT_ID('dbo.ProductCategories', 'U') IS NOT NULL DROP TABLE dbo.ProductCategories;
IF OBJECT_ID('dbo.ProductSizes', 'U') IS NOT NULL DROP TABLE dbo.ProductSizes;
IF OBJECT_ID('dbo.OrderStatuses', 'U') IS NOT NULL DROP TABLE dbo.OrderStatuses;
IF OBJECT_ID('dbo.PaymentStatuses', 'U') IS NOT NULL DROP TABLE dbo.PaymentStatuses;
IF OBJECT_ID('dbo.DeliveryStatuses', 'U') IS NOT NULL DROP TABLE dbo.DeliveryStatuses;
IF OBJECT_ID('dbo.PaymentMethods', 'U') IS NOT NULL DROP TABLE dbo.PaymentMethods;
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL DROP TABLE dbo.Customers;
GO

-- 2. SEGURIDAD E IDENTIDAD
CREATE TABLE Roles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Permissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(MAX) NOT NULL DEFAULT '',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE RolePermissions (
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    PRIMARY KEY (RoleId, PermissionId),
    CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE,
    CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES Permissions(Id) ON DELETE CASCADE
);

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    RoleId INT NOT NULL,
    IdentityNumber NVARCHAR(50) NOT NULL DEFAULT '',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

CREATE TABLE UserPermissions (
    UserId INT NOT NULL,
    PermissionId INT NOT NULL,
    PRIMARY KEY (UserId, PermissionId),
    CONSTRAINT FK_UserPermissions_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UserPermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES Permissions(Id) ON DELETE CASCADE
);
GO

-- 3. CONFIGURACIÓN Y CATÁLOGOS
CREATE TABLE AppConfigs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Key] NVARCHAR(100) NOT NULL UNIQUE,
    Value NVARCHAR(MAX) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL DEFAULT '',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE ProductCategories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE ProductSizes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE OrderStatuses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    DisplayOrder INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE PaymentStatuses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE DeliveryStatuses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    DisplayOrder INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE PaymentMethods (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- 4. ENTIDADES DE NEGOCIO
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL,
    SizeId INT,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL DEFAULT '',
    Price DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES ProductCategories(Id),
    CONSTRAINT FK_Products_Sizes FOREIGN KEY (SizeId) REFERENCES ProductSizes(Id)
);

CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(255) UNIQUE,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Addresses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    Street NVARCHAR(255) NOT NULL,
    Number NVARCHAR(50),
    Sector NVARCHAR(100),
    City NVARCHAR(100),
    Reference NVARCHAR(MAX) NOT NULL DEFAULT '',
    IsPrimary BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Addresses_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
);

CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderNumber NVARCHAR(50) NOT NULL UNIQUE,
    CustomerId INT NOT NULL,
    AddressId INT,
    StatusId INT NOT NULL,
    PaymentMethodId INT NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Total DECIMAL(18,2) NOT NULL DEFAULT 0,
    Notes NVARCHAR(MAX) NOT NULL DEFAULT '',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    CONSTRAINT FK_Orders_Addresses FOREIGN KEY (AddressId) REFERENCES Addresses(Id),
    CONSTRAINT FK_Orders_Statuses FOREIGN KEY (StatusId) REFERENCES OrderStatuses(Id),
    CONSTRAINT FK_Orders_PaymentMethods FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethods(Id)
);

CREATE TABLE OrderDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

CREATE TABLE Deliveries (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL UNIQUE,
    CourierId INT,
    DeliveryStatusId INT NOT NULL,
    AssignedAt DATETIME2,
    DeliveredAt DATETIME2,
    Notes NVARCHAR(MAX) NOT NULL DEFAULT '',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Deliveries_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Deliveries_Couriers FOREIGN KEY (CourierId) REFERENCES Users(Id),
    CONSTRAINT FK_Deliveries_Statuses FOREIGN KEY (DeliveryStatusId) REFERENCES DeliveryStatuses(Id)
);

CREATE TABLE Compensations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    SourceOrderId INT,
    Description NVARCHAR(MAX) NOT NULL DEFAULT '',
    DiscountAmount DECIMAL(18,2) NOT NULL,
    IsRedeemed BIT NOT NULL DEFAULT 0,
    RedeemedAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System',
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(100),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Compensations_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    CONSTRAINT FK_Compensations_Orders FOREIGN KEY (SourceOrderId) REFERENCES Orders(Id)
);
GO

-- 5. DATOS INICIALES (SEED DATA)
SET NOCOUNT ON;

-- 5.1 Roles
INSERT INTO Roles (Name) VALUES ('admin'), ('cajero'), ('cocinero'), ('repartidor');

-- 5.2 Permisos
INSERT INTO Permissions (Name, Description) VALUES 
('clientes.create', 'Crear clientes'),
('clientes.read', 'Ver clientes'),
('clientes.update', 'Actualizar clientes'),
('clientes.delete', 'Eliminar clientes'),
('usuarios.create', 'Crear usuarios'),
('roles.manage', 'Gestionar roles'),
('pedidos.create', 'Crear nuevos pedidos'),
('pedidos.read', 'Ver tablero de pedidos'),
('pedidos.update_status', 'Cambiar estado de pedidos (Flujo Kanban)'),
('clientes.manage', 'Gestionar catálogo de clientes'),
('productos.manage', 'Gestionar catálogo de productos'),
('usuarios.manage', 'Gestionar usuarios del sistema'),
('seguridad.manage', 'Gestionar roles y permisos');

-- 5.3 Asignar todos los permisos al Admin (Id 1)
INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT 1, Id FROM Permissions;

-- 5.4 Usuarios Iniciales
INSERT INTO Users (FullName, Email, PasswordHash, RoleId, IdentityNumber) 
VALUES ('Administrador', 'admin@pizzeria.com', '$2a$11$bwa8gi5ttCLJNZGUq6IWI.aOnJ0U9YqbBATnkDs6VNTarlQQLY02G', 1, '0000-0000-00000'),
       ('Cajero de Turno', 'cajero@pizzeria.com', '$2a$11$VSRBqLWqTT4FFViDtRiTmO1j.4wU7bhQFEc8cPBTynDgbB/m6PCWy', 2, '1111-1111-11111'),
       ('Maestro Pizzero', 'cocinero@pizzeria.com', '$2a$11$VSRBqLWqTT4FFViDtRiTmO1j.4wU7bhQFEc8cPBTynDgbB/m6PCWy', 3, '2222-2222-22222'),
       ('Repartidor Express', 'repartidor@pizzeria.com', '$2a$11$VSRBqLWqTT4FFViDtRiTmO1j.4wU7bhQFEc8cPBTynDgbB/m6PCWy', 4, '3333-3333-33333');

-- 5.5 Estados de Pedido
INSERT INTO OrderStatuses (Code, Name, DisplayOrder) VALUES 
('pendiente', 'Pendiente', 1),
('confirmado', 'Confirmado', 2),
('en_preparacion', 'En preparación', 3),
('listo', 'Listo', 4),
('en_camino', 'En camino', 5),
('entregado', 'Entregado', 6),
('cancelado', 'Cancelado', 7);

-- 5.6 Estados de Entrega
INSERT INTO DeliveryStatuses (Code, Name, DisplayOrder) VALUES 
('pendiente', 'Pendiente de Asignación', 1),
('en_camino', 'En Camino', 2),
('entregado', 'Entregado', 3),
('devuelto', 'Devuelto', 4);

-- 5.7 Métodos de Pago
INSERT INTO PaymentMethods (Code, Name) VALUES 
('efectivo', 'Efectivo'),
('tarjeta', 'Tarjeta'),
('transferencia', 'Transferencia');

-- 5.8 Categorías de Producto
INSERT INTO ProductCategories (Code, Name) VALUES 
('pizza', 'Pizza'),
('bebida', 'Bebida'),
('postre', 'Postre'),
('entrada', 'Entrada');

-- 5.9 Tamaños de Producto
INSERT INTO ProductSizes (Code, Name) VALUES 
('personal', 'Personal'),
('mediana', 'Mediana'),
('familiar', 'Familiar');

-- 5.10 Configuración de App
INSERT INTO AppConfigs ([Key], Value, Description) 
VALUES ('IVA_PERCENTAGE', '15', 'Porcentaje de IVA aplicado');

-- 5.11 Productos Iniciales
INSERT INTO Products (CategoryId, SizeId, Name, Price, Description) VALUES 
(1, 1, 'Pizza Pepperoni Personal', 180.00, 'Pizza con pepperoni y queso mozzarella'),
(1, 2, 'Pizza Pepperoni Mediana', 280.00, 'Pizza con pepperoni y queso mozzarella'),
(1, 3, 'Pizza Pepperoni Familiar', 450.00, 'Pizza con pepperoni y queso mozzarella'),
(1, 1, 'Pizza Suprema Personal', 220.00, 'Jamón, pepperoni, carne, cebolla, chile verde, hongos'),
(1, 2, 'Pizza Suprema Mediana', 350.00, 'Jamón, pepperoni, carne, cebolla, chile verde, hongos'),
(1, 3, 'Pizza Suprema Familiar', 520.00, 'Jamón, pepperoni, carne, cebolla, chile verde, hongos'),
(2, NULL, 'Coca Cola 600ml', 35.00, 'Refresco de cola'),
(2, NULL, 'Coca Cola 1.5L', 65.00, 'Refresco de cola familiar'),
(4, NULL, 'Palitroques con Queso', 95.00, 'Pan horneado con ajo y mozzarella'),
(3, NULL, 'Brownie con Helado', 60.00, 'Brownie de chocolate caliente con helado de vainilla');

-- 5.12 Clientes y Direcciones de Prueba
INSERT INTO Customers (FullName, Phone, Email) VALUES 
('Juan Perez', '99887766', 'juan@email.com'),
('Maria Rodriguez', '88776655', 'maria@email.com');

INSERT INTO Addresses (CustomerId, Street, Number, Sector, City, Reference, IsPrimary) VALUES 
(1, 'Residencial Los Pinos', 'Casa 5', 'Bloque A', 'Tegucigalpa', 'Frente al parque', 1),
(2, 'Colonia El Prado', '#123', 'Calle Principal', 'Tegucigalpa', 'Casa color azul', 1);

PRINT 'Base de datos consolidada y datos iniciales creados exitosamente.';
GO
