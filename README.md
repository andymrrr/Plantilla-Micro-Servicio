# 🚀 Plantilla Microservicio .NET - Clean Architecture

Esta plantilla está diseñada para construir microservicios en .NET siguiendo los principios de **Clean Architecture**. La estructura del proyecto promueve la separación de responsabilidades, la mantenibilidad y el escalado, al mismo tiempo que implementa el patrón **CQRS (Command Query Responsibility Segregation)**, utilizando **Entity Framework Core** como ORM para la gestión de datos.

## ✨ Características Principales

### **Arquitectura Modular y Desacoplada**

- ✅ **Clean Architecture** implementada
- ✅ **CQRS** con MediatR
- ✅ **Entity Framework Core** configurado
- ✅ **Repository Pattern** con Unit of Work

### **Infraestructura Profesional**

- ✅ **Docker profesional** con multi-stage builds
- ✅ **JWT Authentication** y **CORS** configurado
- ✅ **Logging estructurado** con Serilog
- ✅ **Scripts de inicialización** automática

### **Características Técnicas**

- ✅ Compatible con **.NET 8.0** y superior
- ✅ Basada en principios **SOLID**
- ✅ Configuración predefinida para evitar carpetas innecesarias
- ✅ Soporte para **migraciones** de base de datos

## 🚀 Instalación Rápida

### **1. Obtener la Plantilla**

```bash
# Clonar el repositorio
git clone https://github.com/tu-usuario/Plantilla-Micro-Servicio.git
cd Plantilla-Micro-Servicio
```

### **2. Instalar como Template de .NET CLI**

```bash
# Instalar la plantilla
dotnet new install .

# Si ya está instalada, reinstalar con force
dotnet new install . --force

# Verificar que esté instalada
dotnet new list

# Deberías ver algo como:
# micro-servicio  PlantillaMicroservicio   [C#]
```

### **3. Crear Nuevo Proyecto**

```bash
# Crear proyecto con la plantilla
dotnet new micro-servicio -n NombreDeTuProyecto

# Esto creará un directorio con la estructura organizada según Clean Architecture
```

### **Acceder a Servicios**

- 🌐 **API**: http://localhost:8080
- 📚 **Swagger**: http://localhost:8080/swagger
- 📊 **Seq (Logs)**: http://localhost:5341

## 📋 Prerrequisitos

### **Requisitos Previos**

- **SDK de .NET 8.0** o superior
- **Herramientas de desarrollo** como Visual Studio, Visual Studio Code, o JetBrains Rider
- **Familiaridad con Clean Architecture** y patrones CQRS
- **Entity Framework Core** (incluido en la plantilla)
- **Docker Desktop** (versión 20.10+) - Opcional para desarrollo local

## 🏗️ Estructura del Proyecto

### **Arquitectura Modular y Desacoplada**

```
📁 [TuProyecto]/                    # 🎯 API Layer (Presentación)
├── Controllers/                    # Controladores REST
├── Middleware/                     # Middleware personalizado
└── Program.cs                      # Punto de entrada

📁 [TuProyecto].Aplication/         # 🧠 Application Layer (Lógica de Negocio)
├── Feature/                        # CQRS con MediatR
│   ├── Consulta/                   # Queries (Consultas)
│   └── Comando/                    # Commands (Comandos)
├── Servicios/                      # Servicios de aplicación
└── Mapeo/                          # AutoMapper

📁 [TuProyecto].Dal/                # 🗄️ Data Access Layer
├── Contexto/                       # DbContext EF Core
├── Core/                           # Repository Pattern
│   ├── Interfaces/                 # IRepository, IUnitOfWork
│   └── Repositories/               # Implementaciones

📁 [TuProyecto].Infrastructure/     # 🔧 Infrastructure Layer
├── Authentication/                 # JWT
├── Logging/                        # Serilog
└── Extensions/                     # Configuraciones

📁 [TuProyecto].Models/             # 📦 Shared Models
├── Api/                            # DTOs
├── Configuracion/                  # Clases de configuración
└── Entidades/                      # Entidades
```

### **Capas de la Arquitectura**

- **API**: Exposición de endpoints de la API RESTful para interactuar con los clientes
- **Application (BLL)**: Contiene los Modelos y la lógica de negocio
- **DAL**: Acceso a datos utilizando Entity Framework
- **Infrastructure**: Configuraciones externas y servicios de infraestructura
- **Models**: Modelos compartidos entre capas

## 🛠️ Comandos Útiles

### **Gestión de Templates**

```bash
# Listar templates instalados
dotnet new list

# Desinstalar template
dotnet new uninstall PlantillaMicroservicio

# Reinstalar template
dotnet new install . --force

# Ver detalles del template
dotnet new micro-servicio --help
```

### **Desarrollo del Proyecto**

```bash
# Construir proyecto
dotnet build

# Ejecutar en desarrollo
dotnet run

# Ejecutar tests
dotnet test

# Restaurar dependencias
dotnet restore
```

## 🔧 Configuración por Ambiente

### **Desarrollo (`appsettings.Development.json`)**

```json
{
  "ConfiguracionCors": {
    "OrigenesPermitidos": ["http://localhost:3000", "http://localhost:4200"],
    "PermitirCredenciales": true,
    "MetodosPermitidos": ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
    "HeadersPermitidos": ["Content-Type", "Authorization", "X-Requested-With"]
  }
}
```

### **Producción (`appsettings.Production.json`)**

```json
{
  "ConfiguracionCors": {
    "OrigenesPermitidos": ["https://tu-frontend-produccion.com"],
    "PermitirCredenciales": true,
    "MetodosPermitidos": ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
    "HeadersPermitidos": ["Content-Type", "Authorization", "X-Requested-With"]
  }
}
```

## 🐳 Docker (Opcional)

### **Servicios Disponibles**

| Servicio  | Descripción             | Puerto |
| --------- | ----------------------- | ------ |
| **api**   | Microservicio principal | 8080   |
| **db**    | SQL Server              | 1433   |
| **redis** | Cache                   | 6379   |
| **seq**   | Logging                 | 5341   |

### **Comandos Docker**

```bash
# Levantar servicios
docker-compose up -d

# Ver logs
docker-compose logs api

# Detener servicios
docker-compose down
```

## 🚀 Despliegue

### **Desarrollo Local**

```bash
# Ejecutar el proyecto
dotnet run

# O con Docker (opcional)
docker-compose up -d
```

### **Producción**

```bash
# Publicar la aplicación
dotnet publish -c Release

# O con Docker
docker build -t mi-microservicio .
docker run -p 8080:8080 mi-microservicio
```

## 🐛 Troubleshooting

### **Problemas Comunes**

```bash
# Template no aparece en la lista
dotnet new list
dotnet new install . --force

# Error al crear proyecto
dotnet new micro-servicio -n MiProyecto --force

# Puerto ocupado
netstat -ano | findstr :8080  # Windows
lsof -i :8080                 # macOS/Linux

# Proyecto no compila
dotnet restore
dotnet build
```

## 📚 Recursos

- [Documentación .NET 8](https://docs.microsoft.com/en-us/dotnet/)
- [Docker Documentation](https://docs.docker.com/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)

---

¡Tu microservicio está listo para despegar! 🚀
