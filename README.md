# PizzaPos 🍕

Sistema de Punto de Venta (POS) profesional para una Pizzería, desarrollado con **.NET 8** y **SQL Server**.

## 🚀 Características Principales
- **Autenticación y Seguridad**: Sistema de login robusto con roles y permisos. Contraseñas protegidas mediante hasehado industrial **BCrypt**.
- **Gestión de Pedidos (Punto de Venta)**: Interfaz intuitiva para toma de pedidos con búsqueda rápida de clientes y catálogo dinámico.
- **Facturación Integrada**: Generación automática de recibos profesionales para impresoras térmicas (80mm) al despachar pedidos.
- **Tablero Kanban**: Gestión de pedidos en tiempo real con estados (Pendiente, En Preparación, Listo, En Camino, Entregado).
- **Módulos Administrativos**:
  - Gestión de Clientes y Direcciones.
  - Catálogo de Productos y Precios.
  - Gestión de Usuarios y Roles (RBAC).
  - Configuración de Seguridad (Permisos granulares).
- **Pruebas Unitarias**: Suite de tests automatizados para asegurar la integridad de los cálculos y la seguridad.

## 🛠️ Estructura del Proyecto
- **PizzaPos.Api**: Backend ASP.NET Core Web API siguiendo arquitectura modular.
- **PizzaPos.WinForms**: Frontend de escritorio moderno y reactivo.
- **PizzaPos.Infrastructure**: Capa de persistencia con Entity Framework Core y SQL Server.

## ⚙️ Requisitos
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** (Express, LocalDB o Estándar)
- Visual Studio 2022 o VS Code.

## 🏁 Instalación y Configuración

### 1. Base de Datos
Ejecuta el script consolidado en tu servidor de SQL Server:
- Archivo: `PizzaPos_Final_Consolidated.sql`
- Este script crea las tablas, permisos, roles y datos iniciales de prueba.

### 2. Ejecutar la Aplicación
Puedes iniciar todo el sistema (API + Frontend) con un solo comando:

#### Usando PowerShell:
```powershell
.\run-all.ps1
```

### 🧪 Pruebas Unitarias
Para ejecutar la suite de pruebas:
```bash
dotnet test PizzaPos.Tests
```
*Las pruebas cubren la lógica de hasehado de contraseñas, validación de JWT y cálculos de pedidos con compensaciones por retraso.*
Para más detalles, consulta el documento de [Documentación de Pruebas (TESTING.md)](TESTING.md).

### 🔐 Gestión de Secretos (Ejemplo)
El sistema está preparado para manejar secretos de forma segura. Consulta `PizzaPos.Infrastructure/Examples/SecretManagementExample.cs` para ver cómo se abstraen las llaves de API y credenciales de BD.

#### Usando Batch:
```cmd
run-all.bat
```

## 👤 Usuarios por Defecto
- **Admin**: `admin@pizzeria.com` / `admin123`
- **Cajero**: `cajero@pizzeria.com` / `123456`
- **Cocinero**: `cocinero@pizzeria.com` / `123456`
- **Repartidor**: `repartidor@pizzeria.com` / `123456`

---
*Desarrollado como una solución integral para la gestión operativa de pizzerías.*
