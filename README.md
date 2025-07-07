# 🚀 Plantilla de Microservicio Profesional - Clean Architecture

## 📋 Descripción

Esta plantilla está diseñada para construir microservicios profesionales en .NET siguiendo los principios de **Clean Architecture** y **Domain-Driven Design (DDD)**. La estructura del proyecto promueve la separación de responsabilidades, la mantenibilidad y el escalado, implementando patrones modernos como **CQRS** y **MediatR**.

## ✨ Características Principales

### 🏗️ Arquitectura

- **Clean Architecture** con separación clara de capas
- **Domain-Driven Design (DDD)** implementado
- **CQRS** con MediatR para separación de comandos y consultas
- **SOLID Principles** aplicados en toda la estructura

### 🔐 Seguridad

- **Autenticación JWT** profesional y configurable
- **Validación de tokens** con claims estándar
- **Renovación automática** de tokens
- **Configuración de seguridad** flexible

### 📚 Documentación

- **Swagger/OpenAPI** integrado con autenticación JWT
- **Documentación automática** de endpoints
- **Interfaz interactiva** para pruebas de API
- **Anotaciones** completas en controladores

### 🛠️ Infraestructura

- **Middleware de excepciones** unificado y profesional
- **Logging estructurado** con Serilog
- **Sistema de caché** configurable
- **Manejo de errores** robusto con respuestas consistentes

### 🗄️ Base de Datos

- **Entity Framework Core** configurado
- **Soporte multi-proveedor**: SQL Server, PostgreSQL, MySQL
- **Migrations** automáticas
- **Repository Pattern** implementado

## 📁 Estructura del Proyecto

```
PlantillaMicroServicio/
├── PlantillaMicroServicio/              # API Layer (Controllers, Middleware)
├── PlantillaMicroServicio.Aplication/   # Application Layer (Use Cases, Services)
├── PlantillaMicroServicio.Dal/          # Data Access Layer (Repositories, Context)
├── PlantillaMicroServicio.Models/       # Domain Models
├── PlantillaMicroServicio.Infrastructure/ # Infrastructure Layer (JWT, Config)
└── .template.config/                    # Template Configuration
```

### 🎯 Responsabilidades por Capa

| Capa               | Responsabilidad                            | Tecnologías                           |
| ------------------ | ------------------------------------------ | ------------------------------------- |
| **API**            | Controllers, Middleware, Configuración Web | ASP.NET Core, Swagger                 |
| **Application**    | Casos de Uso, CQRS, Servicios              | MediatR, FluentValidation, AutoMapper |
| **Infrastructure** | Autenticación, Configuración Externa       | JWT, CORS, Logging                    |
| **DAL**            | Acceso a Datos, Repositorios               | Entity Framework, SQL                 |
| **Models**         | Entidades de Dominio                       | POCOs, DTOs                           |

## 🚀 Instalación

### Requisitos Previos

- **.NET 8.0 SDK** o superior
- **Visual Studio 2022**, **VS Code** o **Rider**
- Conocimientos básicos de Clean Architecture y DDD

### Instalación de la Plantilla

1. **Clonar el repositorio:**

```bash
git clone https://github.com/andymrrr/Plantilla-Micro-Servicio.git
```

2. **Instalar la plantilla:**

```bash
dotnet new -i .
```

3. **Verificar instalación:**

```bash
dotnet new --list
```

Deberías ver:

```bash
micro-servicio  Plantilla Microservicio Profesional  [C#]
```

## 🎯 Crear un Nuevo Proyecto

### Opción 1: Comando Simple

```bash
dotnet new micro-servicio -n MiServicio
```

### Opción 2: Con Parámetros Personalizados

```bash
dotnet new micro-servicio -n MiServicio -CompanyName "MiEmpresa" -UseJWT true -DatabaseProvider postgresql
```

### Opción 3: Modo Interactivo

```bash
dotnet new micro-servicio --interactive
```

## ⚙️ Parámetros de Configuración

| Parámetro          | Tipo   | Descripción                                  | Default         |
| ------------------ | ------ | -------------------------------------------- | --------------- |
| `ProjectName`      | string | Nombre del proyecto                          | MiMicroServicio |
| `CompanyName`      | string | Nombre de la empresa                         | MiEmpresa       |
| `UseJWT`           | bool   | Incluir autenticación JWT                    | true            |
| `UseSwagger`       | bool   | Incluir documentación Swagger                | true            |
| `UseCaching`       | bool   | Incluir sistema de caché                     | true            |
| `UseLogging`       | bool   | Incluir sistema de logging                   | true            |
| `DatabaseProvider` | choice | Proveedor de BD (sqlserver/postgresql/mysql) | sqlserver       |
| `FrameworkVersion` | choice | Versión de .NET (net8.0/net7.0)              | net8.0          |

## 🔐 Autenticación JWT

### Configuración

```json
{
  "ConfiguracionJwt": {
    "Llave": "tu-llave-secreta-muy-larga",
    "Asunto": "PlantillaMicroServicio",
    "Audiencia": "PlantillaMicroServicio",
    "DuracionMinuto": 60,
    "ClockSkewMinutos": 2,
    "ValidarIssuer": true,
    "ValidarAudience": true
  }
}
```

### Endpoints de Autenticación

- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/refresh` - Renovar token
- `GET /api/auth/validate` - Validar token
- `GET /api/auth/info` - Información del token

### Credenciales de Prueba

- **Username:** `admin`
- **Password:** `password`

## 📖 Documentación con Swagger

### Acceso

```
http://localhost:5000/swagger
```

### Características

- ✅ **Autenticación JWT** integrada
- ✅ **Botón Authorize** para tokens
- ✅ **Documentación automática** de endpoints
- ✅ **Interfaz interactiva** para pruebas
- ✅ **Ejemplos de uso** incluidos

## 🛡️ Middleware de Excepciones

### Características

- **Manejo unificado** de todas las excepciones
- **Logging estructurado** con niveles apropiados
- **Respuestas consistentes** en formato JSON
- **Códigos HTTP** apropiados por tipo de error
- **Headers CORS** automáticos
- **Seguridad** (oculta detalles en producción)

### Tipos de Excepciones Manejadas

| Excepción                     | HTTP Code | Descripción                |
| ----------------------------- | --------- | -------------------------- |
| `ValidationException`         | 400       | Errores de validación      |
| `ArgumentException`           | 400       | Parámetros inválidos       |
| `UnauthorizedAccessException` | 401       | Acceso no autorizado       |
| `KeyNotFoundException`        | 404       | Recurso no encontrado      |
| `TimeoutException`            | 408       | Tiempo de espera agotado   |
| **Otros**                     | 500       | Error interno del servidor |

## 🗄️ Configuración de Base de Datos

### SQL Server

```json
{
  "ConnectionStrings": {
    "PlantillaMicroServicio": "Server=localhost;Database=mi_bd;User Id=usuario;Password=clave;TrustServerCertificate=True"
  }
}
```

### PostgreSQL

```json
{
  "ConnectionStrings": {
    "PlantillaMicroServicio": "Host=localhost;Database=mi_bd;Username=usuario;Password=clave"
  }
}
```

## 🚀 Ejecutar el Proyecto

1. **Restaurar paquetes:**

```bash
dotnet restore
```

2. **Compilar:**

```bash
dotnet build
```

3. **Ejecutar:**

```bash
dotnet run
```

4. **Abrir Swagger:**

```
http://localhost:5000/swagger
```

## 🧪 Pruebas

### 1. Probar Autenticación

```bash
# Login
curl -X POST "http://localhost:5000/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"password"}'
```

### 2. Probar Endpoint Protegido

```bash
# Validar token
curl -X GET "http://localhost:5000/api/auth/validate" \
  -H "Authorization: Bearer TU_TOKEN_AQUI"
```

### 3. Probar Salud del Sistema

```bash
# Health check
curl -X GET "http://localhost:5000/api/salud"
```

## 🔧 Personalización

### Agregar Nuevos Endpoints

1. Crear controlador en `PlantillaMicroServicio/Controllers/`
2. Implementar lógica en `PlantillaMicroServicio.Aplication/Feature/`
3. Configurar repositorio en `PlantillaMicroServicio.Dal/`

### Configurar Nuevas Validaciones

1. Crear validadores en `PlantillaMicroServicio.Aplication/Feature/`
2. Heredar de `AbstractValidator<T>`
3. Registrar en `Program.cs`

### Agregar Nuevos Servicios

1. Crear interfaz en la capa apropiada
2. Implementar en la capa correspondiente
3. Registrar en el contenedor DI

## 📝 Logs

### Configuración

```json
{
  "SerilogConfig": {
    "Level": "Information",
    "Path": "logs/log.txt",
    "Shared": true
  }
}
```

### Niveles de Log

- **Information**: Operaciones normales
- **Warning**: Validaciones y errores esperados
- **Error**: Errores de aplicación
- **Fatal**: Errores críticos

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 👨‍💻 Autor

**Andy Miguel Reyes R.**

- GitHub: [@andymrrr](https://github.com/andymrrr)

## 🙏 Agradecimientos

- Clean Architecture por Uncle Bob
- Domain-Driven Design por Eric Evans
- .NET Community por las herramientas y librerías

---

⭐ **Si te gusta esta plantilla, dale una estrella al repositorio!**
