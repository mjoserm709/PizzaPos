-- =============================================
-- SEED SECURITY DATA - PIZZA POS (FIXED)
-- =============================================

-- 1. INSERT MISSING PERMISSIONS
INSERT OR IGNORE INTO Permissions (Name, Description) VALUES 
('pedidos.create', 'Crear nuevos pedidos'),
('pedidos.read', 'Ver tablero de pedidos'),
('pedidos.update_status', 'Cambiar estado de pedidos (Flujo Kanban)'),
('clientes.manage', 'Gestionar catálogo de clientes'),
('productos.manage', 'Gestionar catálogo de productos'),
('usuarios.manage', 'Gestionar usuarios del sistema'),
('seguridad.manage', 'Gestionar roles y permisos');

-- 2. ASSIGN PERMISSIONS TO ROLES (usando rol_permisos)
-- Nota: 1=admin, 2=cajero, 3=cocinero, 4=repartidor

-- ADMIN: Control Total
INSERT OR IGNORE INTO rol_permisos (rol_id, permiso_id) 
SELECT 1, Id FROM Permissions;

-- CAJERO: Pedidos y Clientes
INSERT OR IGNORE INTO rol_permisos (rol_id, permiso_id)
SELECT 2, Id FROM Permissions WHERE Name IN (
    'pedidos.create', 'pedidos.read', 'pedidos.update_status', 
    'clientes.manage', 'clientes.read', 'clientes.create'
);

-- COCINERO: Solo Flujo de Cocina
INSERT OR IGNORE INTO rol_permisos (rol_id, permiso_id)
SELECT 3, Id FROM Permissions WHERE Name IN (
    'pedidos.read', 'pedidos.update_status'
);

-- REPARTIDOR: Solo Flujo de Entrega
INSERT OR IGNORE INTO rol_permisos (rol_id, permiso_id)
SELECT 4, Id FROM Permissions WHERE Name IN (
    'pedidos.read', 'pedidos.update_status'
);

-- 3. TEST USERS (Password: 123456)
INSERT OR IGNORE INTO Users (FullName, Email, PasswordHash, RoleId) VALUES 
('Cajero de Turno', 'cajero@pizzeria.com', '123456', 2),
('Maestro Pizzero', 'cocinero@pizzeria.com', '123456', 3),
('Repartidor Express', 'repartidor@pizzeria.com', '123456', 4);

-- Asegurar que el Admin tenga la contraseña correcta
UPDATE Users SET PasswordHash = '123456' WHERE Email = 'admin@pizzeria.com';
INSERT OR IGNORE INTO Users (FullName, Email, PasswordHash, RoleId) 
VALUES ('Administrador', 'admin@pizzeria.com', '123456', 1);
