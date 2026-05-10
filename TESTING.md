# Documentación de Pruebas Unitarias 🧪

Este documento detalla la suite de pruebas automatizadas implementada para el sistema **Pizza POS**.

## 📊 Resumen Ejecutivo
- **Framework**: xUnit
- **Librería de Mocks**: Moq
- **Total de Tests**: 6
- **Estado**: 100% Superados ✅

---

## 🔐 Pruebas de Seguridad (`LoginServiceTests`)
Se validó el motor de autenticación para asegurar la protección de datos y la continuidad del negocio.

### Casos de Prueba:
1.  **Login Exitoso con BCrypt**: Verifica que una contraseña hasehada correctamente permita el acceso y genere un token JWT válido.
2.  **Sistema de Fallback (Compatibilidad)**: Comprueba que el sistema permita el acceso a usuarios antiguos que aún tienen contraseñas en texto plano, evitando bloqueos durante la migración a BCrypt.
3.  **Rechazo de Credenciales Inválidas**: Asegura que el sistema no permita el acceso si la contraseña es incorrecta, protegiendo contra accesos no autorizados.

---

## 🍕 Pruebas de Lógica de Negocio (`OrderServiceTests`)
Se validaron los algoritmos de cálculo y las reglas operativas de la pizzería.

### Casos de Prueba:
1.  **Cálculo de Totales e IVA**:
    *   Valida que el sistema lea correctamente el porcentaje de IVA desde la configuración.
    *   Verifica que la suma de productos, el cálculo del impuesto y el total final coincidan matemáticamente.
2.  **Garantía de Tiempo (Compensación)**:
    *   **Escenario de Retraso**: Si un pedido tiene más de 60 minutos de creación al momento de ser entregado, el test verifica que el sistema genere automáticamente un cupón de compensación del 20% para el cliente.
    *   **Escenario Normal**: Verifica que no se cree ninguna compensación si el pedido se entrega dentro del tiempo estipulado.

---

## 🛠️ Cómo Ejecutar los Tests
Desde la terminal en la raíz del proyecto:
```bash
dotnet test PizzaPos.Tests
```

## 📈 Próximos Pasos Sugeridos
- Implementar tests de integración para los controladores de la API.
- Añadir pruebas de validación de formatos (Emails, Teléfonos).
- Simular fallos de base de datos para probar la resiliencia del sistema.
