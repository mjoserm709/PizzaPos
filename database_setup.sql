-- Script de creación de base de datos PizzaPos (SQLite)

-- Tabla de Roles
CREATE TABLE [Admin_Roles] (
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT,
    [Name] TEXT NOT NULL UNIQUE
);

-- Tabla de Permisos
CREATE TABLE [Admin_Permissions] (
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT,
    [Name] TEXT NOT NULL UNIQUE,
    [Description] TEXT NULL
);

-- Tabla Intermedia RolePermissions
CREATE TABLE [Admin_RolePermissions] (
    [RoleId] INTEGER NOT NULL,
    [PermissionId] INTEGER NOT NULL,
    PRIMARY KEY ([RoleId], [PermissionId]),
    FOREIGN KEY ([RoleId]) REFERENCES [Admin_Roles]([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([PermissionId]) REFERENCES [Admin_Permissions]([Id]) ON DELETE CASCADE
);

-- Tabla de Usuarios
CREATE TABLE [Admin_Users] (
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT,
    [Username] TEXT NOT NULL UNIQUE,
    [PasswordHash] TEXT NOT NULL,
    [IsActive] INTEGER DEFAULT 1,
    [RoleId] INTEGER NOT NULL,
    FOREIGN KEY ([RoleId]) REFERENCES [Admin_Roles]([Id])
);

-- Datos Iniciales (Seed Data)
-- Roles
INSERT INTO [Admin_Roles] ([Name]) VALUES ('Admin');
INSERT INTO [Admin_Roles] ([Name]) VALUES ('Waiter');
INSERT INTO [Admin_Roles] ([Name]) VALUES ('Chef');

-- Permisos
INSERT INTO [Admin_Permissions] ([Name], [Description]) VALUES ('Menu.View', 'Ver el menú');
INSERT INTO [Admin_Permissions] ([Name], [Description]) VALUES ('Order.Create', 'Crear pedidos');
INSERT INTO [Admin_Permissions] ([Name], [Description]) VALUES ('Order.Edit', 'Editar pedidos');
INSERT INTO [Admin_Permissions] ([Name], [Description]) VALUES ('Admin.ManageUsers', 'Gestionar usuarios y roles');

-- Asignar Permisos a Roles
-- Admin tiene todo
INSERT INTO [Admin_RolePermissions] ([RoleId], [PermissionId])
SELECT (SELECT Id FROM [Admin_Roles] WHERE Name = 'Admin'), Id FROM [Admin_Permissions];

-- Mesero solo puede ver menú y crear pedidos
INSERT INTO [Admin_RolePermissions] ([RoleId], [PermissionId])
VALUES 
((SELECT Id FROM [Admin_Roles] WHERE Name = 'Waiter'), (SELECT Id FROM [Admin_Permissions] WHERE Name = 'Menu.View')),
((SELECT Id FROM [Admin_Roles] WHERE Name = 'Waiter'), (SELECT Id FROM [Admin_Permissions] WHERE Name = 'Order.Create'));

-- Usuario Admin inicial (Password: admin123)
INSERT INTO [Admin_Users] ([Username], [PasswordHash], [RoleId])
VALUES ('admin', 'admin123', (SELECT Id FROM [Admin_Roles] WHERE Name = 'Admin'));
