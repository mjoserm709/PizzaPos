# Arquitectura Técnica - Pizza POS 🍕

Este documento explica cómo está construido el sistema y cómo interactúan sus componentes. El proyecto sigue los principios de **Arquitectura Limpia (Clean Architecture)** y **Arquitectura Hexagonal**.

## 🏗️ Capas del Proyecto

### 1. PizzaPos.Domain (El Corazón)
Contiene las reglas de negocio puras. No tiene dependencias de librerías externas ni de la base de datos.
- **Entities**: Definición de los objetos de negocio (`Order`, `Product`, `Customer`, `User`).
- **Interfaces**: Contratos para los repositorios (`IOrderRepository`, etc.).

### 2. PizzaPos.Application (Lógica de Orquestación)
Define qué puede hacer el sistema (casos de uso).
- **Services**: Implementan la lógica como el cálculo de IVA, compensaciones por retraso y autenticación.
- **DTOs**: Objetos de transferencia de datos para comunicar el API con el exterior de forma segura.

### 3. PizzaPos.Infrastructure (Implementación Técnica)
Aquí es donde el sistema "toca" el mundo real.
- **Persistence**: Uso de **Entity Framework Core** para SQL Server.
- **Identity**: Generación de tokens **JWT** y lógica de seguridad.
- **Repositories**: Implementación real de las interfaces del dominio para guardar/leer de la base de datos.

### 4. PizzaPos.Api (La Puerta de Entrada)
Expone la funcionalidad mediante endpoints REST.
- **Controllers**: Gestionan las peticiones HTTP y las dirigen a los servicios correspondientes.
- **Real-Time**: Integración con **SignalR** para que el Frontend sepa al instante cuando hay un nuevo pedido.

### 5. PizzaPos.WinForms (La Interfaz de Usuario)
Cliente moderno que consume la API.
- **UserControls**: Componentes reutilizables para cada módulo (Admin, Pedidos, Seguridad).
- **Toast Notifications**: Feedback visual premium para el usuario.
- **SignalR Client**: Escucha los eventos del servidor para actualizar la interfaz sin refrescar.

---

## 🔐 Seguridad y Acceso
- **BCrypt**: Las contraseñas nunca se guardan en texto plano; se hasean con el algoritmo más seguro actualmente.
- **RBAC (Role Based Access Control)**: El sistema verifica los permisos del usuario (`usuarios.manage`, `pedidos.crear`) antes de permitir cualquier acción, tanto en el Backend como en el UI.

## 📡 Comunicación de Datos
- **DynamicResponse<T>**: Todas las respuestas del servidor siguen un formato estándar, lo que permite que el cliente maneje errores y mensajes de éxito de forma consistente.
