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

### **Opción A: Como Template de .NET CLI (Recomendado)**

```bash
# 1. Clonar el repositorio
git clone https://github.com/tu-usuario/Plantilla-Micro-Servicio.git
cd Plantilla-Micro-Servicio

# 2. Registrar la plantilla localmente
dotnet new -i .

# 3. Verificar que esté instalada
dotnet new --list

# Deberías ver algo como:
# micro-servicio  PlantillaMicroservicio   [C#]

# 4. Crear nuevo proyecto
dotnet new micro-servicio -n NombreDeTuProyecto
```

### **Opción B: Clonar Directamente**

```bash
# Clonar el repositorio
git clone https://github.com/tu-usuario/Plantilla-Micro-Servicio.git
cd Plantilla-Micro-Servicio
```

### **Configuración (Solo para Opción B)**

```bash
# Copiar y editar configuración
cp env.example .env

# Variables importantes a cambiar:
# PROJECT_NAME=MiNuevoMicroServicio
# JWT_SECRET=TuClaveSecretaMuyLarga
# DB_PASSWORD=TuContraseñaSegura

# Dar permisos (Linux/macOS)
chmod +x init-project.sh

# Ejecutar script automático
./init-project.sh
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

```bash
# Desarrollo
make dev              # Ejecutar en desarrollo
make build            # Construir proyecto
make test             # Ejecutar tests

# Docker
make up               # Levantar servicios
make down             # Detener servicios
make logs             # Ver logs
make rebuild          # Reconstruir y levantar

# Base de datos
make db-shell         # Acceder a BD
make db-backup        # Crear backup
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

## 🐳 Docker Compose

### **Servicios Incluidos**

| Servicio  | Descripción             | Puerto |
| --------- | ----------------------- | ------ |
| **api**   | Microservicio principal | 8080   |
| **db**    | SQL Server              | 1433   |
| **redis** | Cache                   | 6379   |
| **seq**   | Logging                 | 5341   |

### **Comandos Docker**

```bash
# Desarrollo
docker-compose up -d

# Producción
docker-compose -f docker-compose.prod.yml up -d

# Ver logs
docker-compose logs api
```

## 🚀 Despliegue

### **Desarrollo Local**

```bash
./init-project.sh
```

### **Producción**

```bash
# Configurar variables de producción
cp env.example .env
# Editar .env con valores de producción

# Desplegar
docker-compose -f docker-compose.prod.yml up -d
```

## 🐛 Troubleshooting

### **Problemas Comunes**

```bash
# Puerto ocupado
netstat -ano | findstr :8080  # Windows
lsof -i :8080                 # macOS/Linux

# Docker no funciona
docker --version
docker system prune -f

# Script no ejecuta
chmod +x init-project.sh
bash init-project.sh
```

## 📚 Recursos

- [Documentación .NET 8](https://docs.microsoft.com/en-us/dotnet/)
- [Docker Documentation](https://docs.docker.com/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)

---

¡Tu microservicio está listo para despegar! 🚀
