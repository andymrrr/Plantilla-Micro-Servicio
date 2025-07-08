# 🚀 Plantilla Microservicio .NET - Clean Architecture

Plantilla profesional para crear microservicios en .NET 8 siguiendo **Clean Architecture** y **CQRS** con Entity Framework Core.

## ✨ Características Principales

- ✅ **Clean Architecture** implementada
- ✅ **CQRS** con MediatR
- ✅ **Entity Framework Core** configurado
- ✅ **Docker profesional** con multi-stage builds
- ✅ **JWT Authentication** y **CORS** configurado
- ✅ **Logging estructurado** con Serilog
- ✅ **Scripts de inicialización** automática

## 🚀 Instalación Rápida

### 1. **Obtener la Plantilla**

```bash
# Clonar el repositorio
git clone https://github.com/tu-usuario/Plantilla-Micro-Servicio.git
cd Plantilla-Micro-Servicio
```

### 2. **Configurar Variables**

```bash
# Copiar y editar configuración
cp env.example .env

# Variables importantes a cambiar:
# PROJECT_NAME=MiNuevoMicroServicio
# JWT_SECRET=TuClaveSecretaMuyLarga
# DB_PASSWORD=TuContraseñaSegura
```

### 3. **Inicializar Proyecto**

```bash
# Dar permisos (Linux/macOS)
chmod +x init-project.sh

# Ejecutar script automático
./init-project.sh
```

### 4. **Acceder a Servicios**

- 🌐 **API**: http://localhost:8080
- 📚 **Swagger**: http://localhost:8080/swagger
- 📊 **Seq (Logs)**: http://localhost:5341

## 📋 Prerrequisitos

- **.NET 8.0 SDK** o superior
- **Docker Desktop** (versión 20.10+)
- **Git** (versión 2.30+)

## 🏗️ Estructura del Proyecto

```
📁 [TuProyecto]/                    # API Layer
├── Controllers/                    # Controladores REST
├── Middleware/                     # Middleware personalizado
└── Program.cs                      # Punto de entrada

📁 [TuProyecto].Aplication/         # Application Layer
├── Feature/                        # CQRS con MediatR
│   ├── Consulta/                   # Queries
│   └── Comando/                    # Commands
├── Servicios/                      # Servicios de aplicación
└── Mapeo/                          # AutoMapper

📁 [TuProyecto].Dal/                # Data Access Layer
├── Contexto/                       # DbContext EF Core
├── Core/                           # Repository Pattern
│   ├── Interfaces/                 # IRepository, IUnitOfWork
│   └── Repositories/               # Implementaciones

📁 [TuProyecto].Infrastructure/     # Infrastructure Layer
├── Authentication/                 # JWT
├── Logging/                        # Serilog
└── Extensions/                     # Configuraciones

📁 [TuProyecto].Models/             # Shared Models
├── Api/                            # DTOs
├── Configuracion/                  # Clases de configuración
└── Entidades/                      # Entidades
```

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
