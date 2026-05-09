# PizzaPos

Sistema de Punto de Venta (POS) para una Pizzería, desarrollado con .NET 8.

## Estructura del Proyecto

El proyecto está dividido en dos partes principales:

- **PizzaPos.Api**: Backend desarrollado con ASP.NET Core Web API. Maneja la lógica de negocio, acceso a datos y endpoints para el frontend.
- **PizzaPos.WinForms**: Frontend desarrollado con Windows Forms. Es la interfaz de escritorio para los usuarios del sistema.

## Requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (recomendado) o VS Code.

## Cómo ejecutar

### Ejecutar la API
1. Navega a la carpeta de la API:
   ```bash
   cd PizzaPos.Api
   ```
2. Ejecuta el proyecto:
   ```bash
   dotnet run
   ```
   La API estará disponible en `http://localhost:5267` (o el puerto configurado en `launchSettings.json`). Puedes acceder a la documentación Swagger en `http://localhost:5267/swagger`.

### Ejecutar el Frontend (WinForms)
1. Navega a la carpeta de WinForms:
   ```bash
   cd PizzaPos.WinForms
   ```
2. Ejecuta el proyecto:
   ```bash
   dotnet run
   ```

### Ejecutar Ambos (API + Frontend)
Si quieres iniciar ambos al mismo tiempo:

#### Opción 1: Usando el script de PowerShell
```powershell
.\run-all.ps1
```

#### Opción 2: Usando el archivo Batch
```cmd
run-all.bat
```

#### Opción 3: Visual Studio 2022
1. Haz clic derecho en la Solución (`PizzaPos`) en el Explorador de Soluciones.
2. Selecciona **Configurar proyectos de inicio...** (Configure Startup Projects...).
3. Selecciona **Proyectos de inicio múltiples** (Multiple startup projects).
4. Establece la acción de ambos proyectos (`PizzaPos.Api` y `PizzaPos.WinForms`) en **Iniciar** (Start).
5. Haz clic en **Aceptar** y luego presiona **F5**.

## Características Actuales
- [x] Estructura de solución y proyectos.
- [x] Endpoint de estado (`/api/status`) en la API.
- [x] Formulario base en WinForms con validación de conexión a la API.

## Próximos Pasos
- [ ] Implementar modelos de datos (Pizza, Pedido, Cliente).
- [ ] Configurar base de datos (Entity Framework Core).
- [ ] Crear formularios para gestión de inventario y toma de pedidos.
