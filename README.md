# 🚀 Plantilla Microservicio .NET - Docker Profesional

Una plantilla completa y profesional para crear microservicios en .NET 8 con Docker, diseñada para ser **completamente configurable** y **listo para producción**.

## ✨ Características Principales

### 🐳 **Docker Profesional**

- **Multi-stage builds** para optimización
- **Variables dinámicas** para configuración automática
- **Health checks** integrados
- **Usuario no-root** para seguridad
- **Logging estructurado** con Serilog

### 🔧 **Configuración Automática**

- **Script de inicialización** que renombra todo automáticamente
- **Variables de entorno** dinámicas
- **Configuración por ambiente** (Development/Production)
- **Generación automática** de contraseñas seguras

### 🏗️ **Arquitectura Limpia**

- **Clean Architecture** implementada
- **CQRS** con MediatR
- **Dependency Injection** configurado
- **Repository Pattern** con Unit of Work

### 📊 **Monitoreo y Logging**

- **Serilog** con logging estructurado
- **Seq** para visualización de logs
- **Prometheus** para métricas
- **Grafana** para dashboards
- **Health checks** automáticos

### 🔒 **Seguridad**

- **JWT Authentication** configurado
- **HTTPS** listo para producción
- **CORS configurado profesionalmente** con configuración por ambiente
- **Rate limiting** integrado

## 🚀 Inicio Rápido

### 🎯 **¿Qué es esta Plantilla?**

Esta es una **plantilla enterprise-grade** para crear microservicios en .NET 8 siguiendo los principios de **Clean Architecture** y **CQRS**. Es una solución completa y profesional que incluye:

- ✅ **Clean Architecture** implementada con separación clara de capas
- ✅ **CQRS (Command Query Responsibility Segregation)** con MediatR
- ✅ **Entity Framework Core** configurado para acceso a datos
- ✅ **Docker profesional** con multi-stage builds y optimización
- ✅ **Logging estructurado** con Serilog y Seq
- ✅ **JWT Authentication** configurado y listo
- ✅ **CORS configurado profesionalmente** por ambiente
- ✅ **Scripts de inicialización automática** con renombrado inteligente
- ✅ **Configuración por ambiente** (Development/Production)
- ✅ **Health checks** y monitoreo integrado
- ✅ **Rate limiting** y seguridad avanzada

### 📋 **Prerrequisitos**

Antes de comenzar, asegúrate de tener instalado:

- ✅ **.NET 8.0 SDK** o superior
- ✅ **Docker Desktop** (versión 20.10 o superior)
- ✅ **Git** (versión 2.30 o superior)
- ✅ **Make** (opcional, para usar comandos abreviados)
- ✅ **Familiaridad con Clean Architecture y CQRS**
- ✅ **Entity Framework Core** (incluido en la plantilla)

#### **Instalar Docker Desktop**

**Windows:**

```bash
# Descargar desde: https://www.docker.com/products/docker-desktop
# Ejecutar el instalador y seguir las instrucciones
```

**macOS:**

```bash
# Descargar desde: https://www.docker.com/products/docker-desktop
# Arrastrar Docker.app a Applications
```

**Linux (Ubuntu/Debian):**

```bash
# Actualizar repositorios
sudo apt update

# Instalar dependencias
sudo apt install apt-transport-https ca-certificates curl gnupg lsb-release

# Agregar clave GPG de Docker
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

# Agregar repositorio de Docker
echo "deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# Instalar Docker
sudo apt update
sudo apt install docker-ce docker-ce-cli containerd.io

# Agregar usuario al grupo docker
sudo usermod -aG docker $USER

# Iniciar Docker
sudo systemctl start docker
sudo systemctl enable docker
```

#### **Instalar .NET 8.0 SDK**

**Windows:**

```bash
# Descargar desde: https://dotnet.microsoft.com/download/dotnet/8.0
# Ejecutar el instalador
```

**macOS:**

```bash
# Con Homebrew
brew install dotnet

# O descargar desde: https://dotnet.microsoft.com/download/dotnet/8.0
```

**Linux (Ubuntu/Debian):**

```bash
# Agregar repositorio de Microsoft
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Instalar SDK
sudo apt update
sudo apt install dotnet-sdk-8.0
```

#### **Instalar Git**

**Windows:**

```bash
# Descargar desde: https://git-scm.com/download/win
# Ejecutar el instalador
```

**macOS:**

```bash
# Con Homebrew
brew install git

# O descargar desde: https://git-scm.com/download/mac
```

**Linux:**

```bash
# Ubuntu/Debian
sudo apt update && sudo apt install git

# CentOS/RHEL
sudo yum install git
```

#### **Instalar Make (Opcional)**

**Windows:**

```bash
# Con Chocolatey
choco install make

# Con Scoop
scoop install make
```

**macOS:**

```bash
# Ya viene instalado por defecto
```

**Linux:**

```bash
# Ubuntu/Debian
sudo apt install make

# CentOS/RHEL
sudo yum install make
```

### 1. **Obtener la Plantilla**

#### **Opción A: Clonar desde GitHub (Recomendado)**

```bash
# Clonar el repositorio
git clone https://github.com/tu-usuario/Plantilla-Micro-Servicio.git
cd Plantilla-Micro-Servicio

# O si prefieres tu propio fork
git clone https://github.com/tu-usuario/Plantilla-Micro-Servicio.git MiNuevoMicroServicio
cd MiNuevoMicroServicio
```

#### **Opción B: Usar como Template de GitHub**

1. Ve a https://github.com/tu-usuario/Plantilla-Micro-Servicio
2. Haz clic en **"Use this template"**
3. Selecciona **"Create a new repository"**
4. Dale un nombre a tu nuevo microservicio
5. Clona tu nuevo repositorio

#### **Opción C: Descargar ZIP**

1. Ve a https://github.com/tu-usuario/Plantilla-Micro-Servicio
2. Haz clic en **"Code"** → **"Download ZIP"**
3. Extrae el archivo donde quieras
4. Renombra la carpeta a tu proyecto

### 2. **Personalizar la Plantilla**

```bash
# Copiar archivo de variables de entorno
cp env.example .env

# Editar configuración con tu información
# Puedes usar cualquier editor de texto
notepad .env  # Windows
nano .env     # Linux/macOS
code .env     # VS Code
```

#### **Variables Importantes a Configurar:**

```bash
# Nombre de tu microservicio
PROJECT_NAME=MiNuevoMicroServicio

# Configuración de base de datos
DATABASE_PROVIDER=sqlserver  # o postgres
DB_PASSWORD=TuContraseñaSegura

# Configuración JWT
JWT_SECRET=TuClaveSecretaMuyLargaAlMenos64Caracteres

# Puertos (cambiar si están ocupados)
API_PORT=8080
DB_PORT=1433
```

#### **Renombrar Archivos (Opcional)**

Si quieres cambiar el nombre de la plantilla:

```bash
# Renombrar carpetas del proyecto
mv PlantillaMicroServicio MiNuevoMicroServicio
mv PlantillaMicroServicio.Aplication MiNuevoMicroServicio.Aplication
mv PlantillaMicroServicio.Dal MiNuevoMicroServicio.Dal
mv PlantillaMicroServicio.Infrastructure MiNuevoMicroServicio.Infrastructure
mv PlantillaMicroServicio.Models MiNuevoMicroServicio.Models

# Renombrar archivos de proyecto
mv PlantillaMicroServicio.csproj MiNuevoMicroServicio.csproj
mv PlantillaMicroServicio.Aplication.csproj MiNuevoMicroServicio.Aplication.csproj
mv PlantillaMicroServicio.Dal.csproj MiNuevoMicroServicio.Dal.csproj
mv PlantillaMicroServicio.Infrastructure.csproj MiNuevoMicroServicio.Infrastructure.csproj
mv PlantillaMicroServicio.Models.csproj MiNuevoMicroServicio.Models.csproj
mv PlantillaMicroServicio.sln MiNuevoMicroServicio.sln
```

### 3. **Inicializar el Proyecto**

#### **Opción A: Script Automático (Recomendado)**

```bash
# Dar permisos de ejecución (Linux/macOS)
chmod +x init-project.sh

# Ejecutar script de inicialización
./init-project.sh
```

El script te guiará a través de:

- ✅ Configuración del nombre del proyecto
- ✅ Selección de base de datos
- ✅ Configuración de puertos
- ✅ Generación de credenciales seguras
- ✅ Renombrado automático de archivos
- ✅ Despliegue inicial

#### **Opción B: Inicialización Manual**

```bash
# 1. Levantar servicios
docker-compose up -d

# 2. Verificar que todo funcione
docker-compose ps

# 3. Ver logs
docker-compose logs api
```

### 4. **Verificar Instalación**

```bash
# Verificar que Docker esté funcionando
docker --version
docker-compose --version

# Verificar que los servicios estén corriendo
docker ps

# Ver logs de la aplicación
docker-compose logs api
```

### 4. **Verificar Instalación**

```bash
# Verificar que Docker esté funcionando
docker --version
docker-compose --version

# Verificar que los servicios estén corriendo
docker ps

# Ver logs de la aplicación
docker-compose logs api
```

### 5. **Acceder a los Servicios**

Una vez completado, tendrás acceso a:

- 🌐 **API**: http://localhost:8080
- 📚 **Swagger**: http://localhost:8080/swagger
- 📊 **Seq (Logs)**: http://localhost:5341
- 🗄️ **Base de Datos**: localhost:1433

### 6. **Comenzar a Desarrollar**

¡Tu microservicio está listo! Ahora puedes:

```bash
# 1. Abrir en tu IDE favorito
code .  # VS Code
# o
dotnet open MiNuevoMicroServicio.sln  # Visual Studio

# 2. Crear tu primer endpoint
# Editar: MiNuevoMicroServicio/Controllers/TuController.cs

# 3. Agregar tu lógica de negocio
# Editar: MiNuevoMicroServicio.Aplication/Feature/

# 4. Configurar tu base de datos
# Editar: MiNuevoMicroServicio.Dal/Contexto/
```

### ⚡ **Instalación Rápida (Para Expertos)**

Si ya tienes Docker instalado y conoces el proceso:

```bash
# Clonar y configurar en un comando
git clone https://github.com/tu-usuario/Plantilla-Micro-Servicio.git && cd Plantilla-Micro-Servicio && cp env.example .env && ./init-project.sh
```

### 🎯 **Usar como Template de GitHub**

Para hacer tu plantilla más fácil de usar:

1. **Hacer la plantilla pública** en GitHub
2. **Agregar el botón "Use this template"**:
   - Ve a Settings → General
   - Marca "Template repository"
3. **Los usuarios podrán crear repositorios** basados en tu plantilla

#### **Ventajas del Template de GitHub:**

- ✅ **Crea repositorios nuevos** automáticamente
- ✅ **Mantiene el historial** de Git limpio
- ✅ **Fácil de usar** para otros desarrolladores
- ✅ **Actualizaciones automáticas** disponibles

### 🔧 **Instalación Manual (Sin Script)**

Si prefieres hacerlo paso a paso:

```bash
# 1. Clonar repositorio
git clone <tu-repositorio>
cd Plantilla-Micro-Servicio

# 2. Configurar variables de entorno
cp env.example .env
# Editar .env con tus preferencias

# 3. Levantar servicios
docker-compose up -d

# 4. Verificar que todo funcione
docker-compose ps
curl http://localhost:8080/health
```

## 🛠️ Comandos Útiles

### **Desarrollo Local**

```bash
# Ejecutar en modo desarrollo
make dev

# Construir proyecto
make build

# Ejecutar tests
make test

# Limpiar archivos de build
make clean
```

### **Docker**

```bash
# Levantar servicios
make up

# Ver logs
make logs

# Detener servicios
make down

# Reconstruir y levantar
make rebuild

# Ver estado de servicios
make status
```

### **Base de Datos**

```bash
# Acceder al shell de la BD
make db-shell

# Crear backup
make db-backup

# Restaurar backup
make db-restore BACKUP_FILE=archivo.bak
```

### **Monitoreo**

```bash
# Verificar salud de la API
make api-health

# Abrir Swagger
make api-swagger

# Abrir Seq
make logs-view
```

## 🏗️ Arquitectura y Estructura

### **Clean Architecture Implementada**

Esta plantilla sigue los principios de **Clean Architecture** con una separación clara de responsabilidades:

```
📁 [TuProyecto]/                    # 🎯 API Layer (Presentación)
├── Controllers/                    # Controladores REST
├── Middleware/                     # Middleware personalizado
├── Extensions/                     # Extensiones de configuración
└── Program.cs                      # Punto de entrada

📁 [TuProyecto].Aplication/         # 🧠 Application Layer (Lógica de Aplicación)
├── Feature/                        # Patrón CQRS con MediatR
│   ├── Consulta/                   # Queries (Consultas)
│   └── Comando/                    # Commands (Comandos)
├── Servicios/                      # Servicios de aplicación
├── Mapeo/                          # AutoMapper profiles
└── Middleware/                     # Middleware de aplicación

📁 [TuProyecto].Dal/                # 🗄️ Data Access Layer
├── Contexto/                       # DbContext de Entity Framework
├── Core/                           # Interfaces y abstracciones
│   ├── Interfaces/                 # IRepository, IUnitOfWork
│   └── Repositories/               # Implementaciones
└── Extension.cs                    # Configuración de DAL

📁 [TuProyecto].Infrastructure/     # 🔧 Infrastructure Layer
├── Authentication/                 # JWT Authentication
├── Logging/                        # Serilog configuration
├── Configuration/                  # Configuraciones externas
└── Extensions/                     # Extensiones de infraestructura

📁 [TuProyecto].Models/             # 📦 Shared Models
├── Api/                            # DTOs y modelos de API
├── Configuracion/                  # Clases de configuración
└── Entidades/                      # Entidades de dominio
```

### **Patrón CQRS Implementado**

La plantilla utiliza **CQRS (Command Query Responsibility Segregation)** con MediatR:

```csharp
// Ejemplo de Query
public class VerificarEstadoConsulta : IRequest<ControlSalud>
{
    public string Servicio { get; set; }
}

public class VerificarEstadoHandler : IRequestHandler<VerificarEstadoConsulta, ControlSalud>
{
    public async Task<ControlSalud> Handle(VerificarEstadoConsulta request, CancellationToken cancellationToken)
    {
        // Lógica de consulta
    }
}

// Ejemplo de Command
public class CrearUsuarioComando : IRequest<int>
{
    public string Nombre { get; set; }
    public string Email { get; set; }
}
```

### **Entity Framework Core**

Configurado con **Repository Pattern** y **Unit of Work**:

```csharp
// Interfaz del repositorio
public interface IRepositorio<T> where T : class
{
    Task<T> ObtenerPorIdAsync(int id);
    Task<IEnumerable<T>> ObtenerTodosAsync();
    Task AgregarAsync(T entidad);
    Task ActualizarAsync(T entidad);
    Task EliminarAsync(T entidad);
}

// Unit of Work
public interface IPlantillaMicroServicioUoW
{
    IRepositorio<T> Repositorio<T>() where T : class;
    Task<int> GuardarCambiosAsync();
}
```

## 🔧 Configuración

### **Características Técnicas Avanzadas**

#### **Docker Profesional**

- **Multi-stage builds** para optimización de imágenes
- **Usuario no-root** para seguridad
- **Health checks** integrados
- **Variables dinámicas** para configuración automática
- **Optimización de capas** para builds más rápidos

#### **Logging y Monitoreo**

- **Serilog** con logging estructurado
- **Seq** para visualización de logs
- **Request/Response logging** automático
- **Error tracking** con contexto completo
- **Performance monitoring** integrado

#### **Seguridad Enterprise**

- **JWT Authentication** configurado
- **CORS profesional** por ambiente
- **Rate limiting** integrado
- **HTTPS** listo para producción
- **Secrets management** con Docker

#### **Base de Datos**

- **Entity Framework Core** configurado
- **Repository Pattern** implementado
- **Unit of Work** pattern
- **Migrations** automáticas
- **Connection pooling** optimizado

### **Sistema de Variables de Entorno Profesional**

El template implementa un **sistema enterprise-grade** de variables de entorno que permite:

- ✅ **Configuración sin re-despliegue** - Cambia variables sin rebuild
- ✅ **Validación automática** - Verifica configuración antes del despliegue
- ✅ **Generación automática** - Scripts que crean configuraciones válidas
- ✅ **Separación por ambiente** - Development/Staging/Production
- ✅ **Secrets management** - Manejo seguro de credenciales

### **Variables Requeridas**

```bash
# Variables CRÍTICAS (deben estar configuradas)
PROJECT_NAME=MiMicroServicio          # Nombre del proyecto
JWT_SECRET=TuClaveSecretaMuyLarga     # Al menos 64 caracteres
DB_PASSWORD=TuContraseñaSegura        # Contraseña de base de datos
```

### **Variables Importantes**

```bash
# Configuración de la aplicación
ASPNETCORE_ENVIRONMENT=Development
DATABASE_PROVIDER=sqlserver
API_PORT=8080
DB_PORT=1433
REDIS_PORT=6379
SEQ_PORT=5341
```

### **Variables Opcionales**

```bash
# Configuración avanzada
ASPNETCORE_KESTREL_MAX_CONNECTIONS=100
SERILOG_LEVEL=Information
CACHE_EXPIRATION_SECONDS=300
CORS_ORIGINS=http://localhost:3000
RATE_LIMIT_PER_MINUTE=100
```

## 🌐 Configuración de CORS Profesional

El microservicio incluye una **configuración de CORS profesional** que permite especificar exactamente qué orígenes, métodos y headers están permitidos para cada ambiente.

### **Configuración por Ambiente**

#### **Desarrollo (`appsettings.Development.json`)**

```json
{
  "ConfiguracionCors": {
    "OrigenesPermitidos": [
      "http://localhost:3000",
      "http://localhost:4200",
      "http://localhost:8080",
      "http://localhost:5173"
    ],
    "PermitirCredenciales": true,
    "MetodosPermitidos": ["GET", "POST", "PUT", "DELETE", "OPTIONS", "PATCH"],
    "HeadersPermitidos": [
      "Content-Type",
      "Authorization",
      "X-Requested-With",
      "Accept",
      "Origin"
    ]
  }
}
```

#### **Producción (`appsettings.Production.json`)**

```json
{
  "ConfiguracionCors": {
    "OrigenesPermitidos": [
      "https://tu-frontend-produccion.com",
      "https://www.tu-frontend-produccion.com"
    ],
    "PermitirCredenciales": true,
    "MetodosPermitidos": ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
    "HeadersPermitidos": ["Content-Type", "Authorization", "X-Requested-With"]
  }
}
```

### **Cómo Configurar para tu Frontend**

1. **Identifica la URL de tu frontend** (ej: `https://mi-app.com`)
2. **Agrega la URL al array `OrigenesPermitidos`** en el archivo de configuración correspondiente
3. **Reinicia el microservicio** para aplicar los cambios

### **Ventajas de esta Configuración**

- ✅ **Seguridad mejorada** - Solo orígenes específicos permitidos
- ✅ **Configuración por ambiente** - Diferentes reglas para dev/prod
- ✅ **Fácil mantenimiento** - Cambios sin modificar código
- ✅ **Flexibilidad** - Control granular sobre métodos y headers
- ✅ **Credenciales seguras** - Soporte para cookies y headers de autorización

### **Comandos de Configuración**

```bash
# Configuración rápida (recomendado)
./quick-setup.sh

# Configuración manual
make env-setup
make validate-env

# Validar configuración
./validate-env.sh
```

### **Entornos Disponibles**

#### **Desarrollo**

```bash
docker-compose up -d
```

#### **Producción**

```bash
docker-compose -f docker-compose.prod.yml up -d
```

## 🐳 Docker Compose

### **Servicios Incluidos**

| Servicio       | Descripción             | Puerto |
| -------------- | ----------------------- | ------ |
| **api**        | Microservicio principal | 8080   |
| **db**         | SQL Server              | 1433   |
| **redis**      | Cache                   | 6379   |
| **seq**        | Logging                 | 5341   |
| **nginx**      | Load Balancer (prod)    | 80/443 |
| **prometheus** | Métricas (prod)         | 9090   |
| **grafana**    | Dashboards (prod)       | 3000   |

### **Volúmenes**

- `db_data`: Datos de SQL Server
- `redis_data`: Datos de Redis
- `seq_data`: Logs de Seq
- `prometheus_data`: Métricas de Prometheus
- `grafana_data`: Dashboards de Grafana

## 🔒 Seguridad

### **Configuración Automática**

- ✅ Contraseñas generadas automáticamente
- ✅ Claves JWT seguras
- ✅ Usuario no-root en contenedores
- ✅ Health checks implementados
- ✅ Rate limiting configurado

### **Recomendaciones de Producción**

1. **Cambiar todas las contraseñas** por defecto
2. **Configurar HTTPS** con certificados válidos
3. **Usar secrets management** (Docker Secrets, Kubernetes Secrets)
4. **Configurar firewalls** apropiados
5. **Implementar backup automático** de bases de datos

## 📊 Monitoreo y Logging

### **Logging Estructurado**

```csharp
// Ejemplo de logging
_logger.Information("Usuario {UserId} accedió al sistema", userId);
```

### **Métricas Disponibles**

- Request/response times
- Error rates
- Memory usage
- CPU usage
- Database connections

### **Dashboards Predefinidos**

- API Performance
- Error Tracking
- System Resources
- Database Metrics

## 🚀 Despliegue

### **Desarrollo Local**

```bash
# Inicializar proyecto
./init-project.sh

# O manualmente
make init
```

### **Producción**

```bash
# Configurar variables de producción
cp env.example .env
# Editar .env con valores de producción

# Desplegar
docker-compose -f docker-compose.prod.yml up -d
```

### **Escalado**

```bash
# Escalar API a 3 réplicas
make scale REPLICAS=3
```

## 🔧 Personalización

### **Agregar Nuevos Servicios**

1. Agregar servicio en `docker-compose.yml`
2. Configurar variables en `env.example`
3. Actualizar `Makefile` con comandos útiles

### **Cambiar Base de Datos**

1. Modificar `DATABASE_PROVIDER` en `.env`
2. Actualizar connection strings
3. Cambiar imagen en docker-compose

### **Agregar Nuevas APIs**

1. Crear controladores en la capa API
2. Implementar handlers en Application
3. Agregar repositorios en Dal

## 🐛 Troubleshooting

### **Problemas de Instalación**

#### **Docker no está instalado o no funciona**

```bash
# Verificar instalación
docker --version

# Si no está instalado, seguir instrucciones de instalación arriba
# Si está instalado pero no funciona:

# Windows: Reiniciar Docker Desktop
# macOS: Reiniciar Docker Desktop
# Linux: sudo systemctl restart docker
```

#### **Puerto ya en uso**

```bash
# Ver qué está usando el puerto
netstat -ano | findstr :8080  # Windows
lsof -i :8080                 # macOS/Linux

# Cambiar puerto en .env
API_PORT=8081
```

#### **Permisos de archivo (Linux/macOS)**

```bash
# Dar permisos de ejecución
chmod +x init-project.sh
chmod +x *.sh

# Si hay problemas con Docker
sudo usermod -aG docker $USER
# Reiniciar sesión después
```

#### **Base de datos no conecta**

```bash
# Verificar logs
make logs-db

# Reiniciar servicios
make restart

# Verificar variables de entorno
cat .env | grep DB_
```

#### **Contenedor no inicia**

```bash
# Ver logs detallados
docker-compose logs api

# Reconstruir imagen
make rebuild

# Limpiar contenedores y volúmenes
docker-compose down -v
docker system prune -f
```

#### **Script de inicialización falla**

```bash
# Verificar que el archivo existe
ls -la init-project.sh

# Ejecutar con bash explícitamente
bash init-project.sh

# Verificar permisos
chmod +x init-project.sh
```

#### **Problemas de red**

```bash
# Verificar que Docker tenga acceso a internet
docker run hello-world

# Si hay proxy corporativo, configurar Docker
# Windows/macOS: Docker Desktop > Settings > Resources > Proxies
# Linux: /etc/systemd/system/docker.service.d/http-proxy.conf
```

## 📚 Recursos Adicionales

- [Documentación de .NET 8](https://docs.microsoft.com/en-us/dotnet/)
- [Docker Documentation](https://docs.docker.com/)
- [Serilog Documentation](https://serilog.net/)
- [Seq Documentation](https://datalust.co/docs/)

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

---

## 🎯 Próximos Pasos

1. **Ejecuta el script de inicialización**: `./init-project.sh`
2. **Personaliza la configuración** según tus necesidades
3. **Desarrolla tu lógica de negocio** en las capas correspondientes
4. **Configura monitoreo** para producción
5. **Despliega en tu infraestructura** preferida

¡Tu microservicio está listo para despegar! 🚀
