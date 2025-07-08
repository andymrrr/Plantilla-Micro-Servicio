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

### 1. **Clonar el Template**

```bash
git clone <tu-repositorio>
cd Plantilla-Micro-Servicio
```

### 2. **Ejecutar Script de Inicialización**

```bash
./init-project.sh
```

El script te guiará a través de:

- ✅ Configuración del nombre del proyecto
- ✅ Selección de base de datos
- ✅ Configuración de puertos
- ✅ Generación de credenciales seguras
- ✅ Renombrado automático de archivos
- ✅ Despliegue inicial

### 3. **Acceder a los Servicios**

Una vez completado, tendrás acceso a:

- 🌐 **API**: http://localhost:8080
- 📚 **Swagger**: http://localhost:8080/swagger
- 📊 **Seq (Logs)**: http://localhost:5341
- 🗄️ **Base de Datos**: localhost:1433

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

## 📁 Estructura del Proyecto

```
Plantilla-Micro-Servicio/
├── 📁 [TuProyecto]/                 # API Principal
├── 📁 [TuProyecto].Aplication/      # Lógica de Aplicación
├── 📁 [TuProyecto].Dal/             # Acceso a Datos
├── 📁 [TuProyecto].Infrastructure/  # Infraestructura
├── 📁 [TuProyecto].Models/          # Modelos
├── 🐳 Dockerfile                    # Imagen Docker
├── 🐙 docker-compose.yml           # Servicios de desarrollo
├── 🏭 docker-compose.prod.yml      # Servicios de producción
├── ⚙️ Makefile                     # Comandos útiles
├── 🔧 init-project.sh              # Script de inicialización
├── 📄 env.example                  # Variables de entorno
└── 📖 README.md                    # Este archivo
```

## 🔧 Configuración

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

### **Problemas Comunes**

#### **Puerto ya en uso**

```bash
# Cambiar puerto en .env
API_PORT=8081
```

#### **Base de datos no conecta**

```bash
# Verificar logs
make logs-db

# Reiniciar servicios
make restart
```

#### **Contenedor no inicia**

```bash
# Ver logs detallados
docker-compose logs api

# Reconstruir imagen
make rebuild
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
