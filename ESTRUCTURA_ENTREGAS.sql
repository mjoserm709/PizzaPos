PRAGMA foreign_keys = ON;

-- =========================
-- TABLAS DE SEGURIDAD
-- =========================

CREATE TABLE roles (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  nombre TEXT NOT NULL UNIQUE,
  descripcion TEXT,
  activo INTEGER DEFAULT 1
);

CREATE TABLE permisos (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  nombre TEXT NOT NULL UNIQUE,
  descripcion TEXT,
  activo INTEGER DEFAULT 1
);

CREATE TABLE rol_permisos (
  rol_id INTEGER NOT NULL,
  permiso_id INTEGER NOT NULL,
  PRIMARY KEY (rol_id, permiso_id),
  FOREIGN KEY (rol_id) REFERENCES roles(id),
  FOREIGN KEY (permiso_id) REFERENCES permisos(id)
);

CREATE TABLE usuarios (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  nombre TEXT NOT NULL,
  email TEXT NOT NULL UNIQUE,
  password_hash TEXT NOT NULL,
  rol_id INTEGER NOT NULL,
  activo INTEGER DEFAULT 1,
  creado_en DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (rol_id) REFERENCES roles(id)
);

-- =========================
-- TABLAS DE CONFIGURACION
-- =========================

CREATE TABLE estados_pedido (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  codigo TEXT NOT NULL UNIQUE,
  nombre TEXT NOT NULL,
  orden INTEGER NOT NULL,
  activo INTEGER DEFAULT 1
);

CREATE TABLE estados_pago (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  codigo TEXT NOT NULL UNIQUE,
  nombre TEXT NOT NULL,
  activo INTEGER DEFAULT 1
);

CREATE TABLE estados_entrega (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  codigo TEXT NOT NULL UNIQUE,
  nombre TEXT NOT NULL,
  orden INTEGER NOT NULL,
  activo INTEGER DEFAULT 1
);

CREATE TABLE metodos_pago (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  codigo TEXT NOT NULL UNIQUE,
  nombre TEXT NOT NULL,
  activo INTEGER DEFAULT 1
);

CREATE TABLE categorias_producto (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  codigo TEXT NOT NULL UNIQUE,
  nombre TEXT NOT NULL,
  activo INTEGER DEFAULT 1
);

CREATE TABLE tamanos_producto (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  codigo TEXT NOT NULL UNIQUE,
  nombre TEXT NOT NULL,
  activo INTEGER DEFAULT 1
);

-- =========================
-- TABLAS PRINCIPALES
-- =========================

CREATE TABLE clientes (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  nombre TEXT NOT NULL,
  telefono TEXT NOT NULL UNIQUE,
  email TEXT UNIQUE,
  activo INTEGER DEFAULT 1,
  creado_en DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE direcciones (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  cliente_id INTEGER NOT NULL,
  calle TEXT NOT NULL,
  numero TEXT,
  sector TEXT,
  ciudad TEXT,
  referencia TEXT,
  es_principal INTEGER DEFAULT 0,
  activo INTEGER DEFAULT 1,
  FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

CREATE TABLE productos (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  categoria_id INTEGER NOT NULL,
  tamano_id INTEGER,
  nombre TEXT NOT NULL,
  descripcion TEXT,
  precio REAL NOT NULL,
  activo INTEGER DEFAULT 1,
  FOREIGN KEY (categoria_id) REFERENCES categorias_producto(id),
  FOREIGN KEY (tamano_id) REFERENCES tamanos_producto(id)
);

CREATE TABLE pedidos (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  cliente_id INTEGER NOT NULL,
  direccion_id INTEGER NOT NULL,
  estado_pedido_id INTEGER NOT NULL,
  estado_pago_id INTEGER NOT NULL,
  metodo_pago_id INTEGER,
  subtotal REAL NOT NULL DEFAULT 0,
  impuesto REAL NOT NULL DEFAULT 0,
  total REAL NOT NULL DEFAULT 0,
  observacion TEXT,
  creado_por INTEGER,
  creado_en DATETIME DEFAULT CURRENT_TIMESTAMP,

  FOREIGN KEY (cliente_id) REFERENCES clientes(id),
  FOREIGN KEY (direccion_id) REFERENCES direcciones(id),
  FOREIGN KEY (estado_pedido_id) REFERENCES estados_pedido(id),
  FOREIGN KEY (estado_pago_id) REFERENCES estados_pago(id),
  FOREIGN KEY (metodo_pago_id) REFERENCES metodos_pago(id),
  FOREIGN KEY (creado_por) REFERENCES usuarios(id)
);

CREATE TABLE pedido_detalles (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  pedido_id INTEGER NOT NULL,
  producto_id INTEGER NOT NULL,
  cantidad INTEGER NOT NULL,
  precio_unitario REAL NOT NULL,
  subtotal REAL NOT NULL,

  FOREIGN KEY (pedido_id) REFERENCES pedidos(id),
  FOREIGN KEY (producto_id) REFERENCES productos(id)
);

CREATE TABLE entregas (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  pedido_id INTEGER NOT NULL UNIQUE,
  repartidor_id INTEGER,
  estado_entrega_id INTEGER NOT NULL,
  fecha_asignacion DATETIME,
  fecha_entrega DATETIME,
  observacion TEXT,

  FOREIGN KEY (pedido_id) REFERENCES pedidos(id),
  FOREIGN KEY (repartidor_id) REFERENCES usuarios(id),
  FOREIGN KEY (estado_entrega_id) REFERENCES estados_entrega(id)
);

-- =========================
-- CARGA DE ESTADOS
-- =========================

INSERT INTO estados_pedido (codigo, nombre, orden) VALUES
('pendiente', 'Pendiente', 1),
('confirmado', 'Confirmado', 2),
('en_preparacion', 'En preparación', 3),
('listo', 'Listo', 4),
('en_camino', 'En camino', 5),
('entregado', 'Entregado', 6),
('cancelado', 'Cancelado', 7);

INSERT INTO estados_pago (codigo, nombre) VALUES
('pendiente', 'Pendiente'),
('pagado', 'Pagado'),
('fallido', 'Fallido'),
('reembolsado', 'Reembolsado');

INSERT INTO estados_entrega (codigo, nombre, orden) VALUES
('pendiente', 'Pendiente', 1),
('asignado', 'Asignado', 2),
('en_camino', 'En camino', 3),
('entregado', 'Entregado', 4),
('fallido', 'Entrega fallida', 5);

INSERT INTO metodos_pago (codigo, nombre) VALUES
('efectivo', 'Efectivo'),
('tarjeta', 'Tarjeta'),
('transferencia', 'Transferencia');

INSERT INTO categorias_producto (codigo, nombre) VALUES
('pizza', 'Pizza'),
('bebida', 'Bebida'),
('extra', 'Extra');

INSERT INTO tamanos_producto (codigo, nombre) VALUES
('personal', 'Personal'),
('mediana', 'Mediana'),
('familiar', 'Familiar');

-- =========================
-- CARGA DE ROLES
-- =========================

INSERT INTO roles (nombre, descripcion) VALUES
('admin', 'Administrador general del sistema'),
('cajero', 'Registra clientes, pedidos y pagos'),
('cocinero', 'Gestiona preparación de pedidos'),
('repartidor', 'Gestiona entregas asignadas'),
('cliente', 'Cliente final del sistema');

-- =========================
-- CARGA DE PERMISOS
-- =========================

INSERT INTO permisos (nombre, descripcion) VALUES
('clientes.create', 'Crear clientes'),
('clientes.read', 'Ver clientes'),
('clientes.update', 'Actualizar clientes'),
('clientes.delete', 'Eliminar clientes'),

('direcciones.create', 'Crear direcciones'),
('direcciones.read', 'Ver direcciones'),
('direcciones.update', 'Actualizar direcciones'),
('direcciones.delete', 'Eliminar direcciones'),

('productos.create', 'Crear productos'),
('productos.read', 'Ver productos'),
('productos.update', 'Actualizar productos'),
('productos.delete', 'Eliminar productos'),

('pedidos.create', 'Crear pedidos'),
('pedidos.read', 'Ver pedidos'),
('pedidos.update', 'Actualizar pedidos'),
('pedidos.delete', 'Eliminar pedidos'),
('pedidos.update_status', 'Cambiar estado de pedido'),
('pedidos.read_own', 'Ver pedidos propios'),
('pedidos.cancel_own', 'Cancelar pedidos propios'),

('pagos.create', 'Registrar pagos'),
('pagos.read', 'Ver pagos'),
('pagos.update', 'Actualizar pagos'),

('entregas.read', 'Ver entregas'),
('entregas.assign', 'Asignar entregas'),
('entregas.update_status', 'Cambiar estado de entrega'),
('entregas.read_assigned', 'Ver entregas asignadas'),

('usuarios.create', 'Crear usuarios'),
('usuarios.read', 'Ver usuarios'),
('usuarios.update', 'Actualizar usuarios'),
('usuarios.delete', 'Eliminar usuarios'),

('roles.manage', 'Administrar roles'),
('permissions.manage', 'Administrar permisos'),

('config.read', 'Ver configuración'),
('config.manage', 'Administrar configuración');

-- =========================
-- ASIGNACION DE PERMISOS A ROLES
-- =========================

-- Admin: todos los permisos
INSERT INTO rol_permisos (rol_id, permiso_id)
SELECT r.id, p.id
FROM roles r, permisos p
WHERE r.nombre = 'admin';

-- Cajero
INSERT INTO rol_permisos (rol_id, permiso_id)
SELECT r.id, p.id
FROM roles r
JOIN permisos p ON p.nombre IN (
  'clientes.create',
  'clientes.read',
  'clientes.update',
  'direcciones.create',
  'direcciones.read',
  'direcciones.update',
  'productos.read',
  'pedidos.create',
  'pedidos.read',
  'pedidos.update',
  'pagos.create',
  'pagos.read',
  'config.read'
)
WHERE r.nombre = 'cajero';

-- Cocinero
INSERT INTO rol_permisos (rol_id, permiso_id)
SELECT r.id, p.id
FROM roles r
JOIN permisos p ON p.nombre IN (
  'pedidos.read',
  'pedidos.update_status',
  'productos.read',
  'config.read'
)
WHERE r.nombre = 'cocinero';

-- Repartidor
INSERT INTO rol_permisos (rol_id, permiso_id)
SELECT r.id, p.id
FROM roles r
JOIN permisos p ON p.nombre IN (
  'entregas.read_assigned',
  'entregas.update_status',
  'direcciones.read',
  'clientes.read',
  'pedidos.read',
  'config.read'
)
WHERE r.nombre = 'repartidor';

-- Cliente
INSERT INTO rol_permisos (rol_id, permiso_id)
SELECT r.id, p.id
FROM roles r
JOIN permisos p ON p.nombre IN (
  'productos.read',
  'pedidos.create',
  'pedidos.read_own',
  'pedidos.cancel_own',
  'direcciones.create',
  'direcciones.read',
  'direcciones.update'
)
WHERE r.nombre = 'cliente';

-- =========================
-- USUARIO ADMIN INICIAL
-- =========================

INSERT INTO usuarios (nombre, email, password_hash, rol_id)
SELECT 
  'Administrador',
  'admin@pizzeria.com',
  'CAMBIAR_POR_PASSWORD_HASH_REAL',
  id
FROM roles
WHERE nombre = 'admin';

-- =========================
-- PRODUCTOS DE EJEMPLO
-- =========================

INSERT INTO productos (categoria_id, tamano_id, nombre, descripcion, precio)
VALUES
(
  (SELECT id FROM categorias_producto WHERE codigo = 'pizza'),
  (SELECT id FROM tamanos_producto WHERE codigo = 'mediana'),
  'Pizza Pepperoni Mediana',
  'Pizza con queso mozzarella y pepperoni',
  450.00
),
(
  (SELECT id FROM categorias_producto WHERE codigo = 'pizza'),
  (SELECT id FROM tamanos_producto WHERE codigo = 'familiar'),
  'Pizza Familiar Mixta',
  'Pizza familiar con jamón, queso, vegetales y pepperoni',
  750.00
),
(
  (SELECT id FROM categorias_producto WHERE codigo = 'bebida'),
  NULL,
  'Refresco 2 Litros',
  'Bebida gaseosa de 2 litros',
  150.00
);