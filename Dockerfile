FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG RUNTIME_VERSION=8.0
ARG BUILD_CONFIGURATION=Release

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG RUNTIME_VERSION=8.0
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["PlantillaMicroServicio/PlantillaMicroServicio.csproj", "PlantillaMicroServicio/"]
COPY ["PlantillaMicroServicio.Aplication/PlantillaMicroServicio.Aplication.csproj", "PlantillaMicroServicio.Aplication/"]
COPY ["PlantillaMicroServicio.Dal/PlantillaMicroServicio.Dal.csproj", "PlantillaMicroServicio.Dal/"]
COPY ["PlantillaMicroServicio.Models/PlantillaMicroServicio.Models.csproj", "PlantillaMicroServicio.Models/"]
COPY ["PlantillaMicroServicio.Infrastructure/PlantillaMicroServicio.Infrastructure.csproj", "PlantillaMicroServicio.Infrastructure/"]

RUN dotnet restore "PlantillaMicroServicio/PlantillaMicroServicio.csproj"
COPY . .
WORKDIR "/src/PlantillaMicroServicio"
RUN dotnet build "PlantillaMicroServicio.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PlantillaMicroServicio.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN addgroup --system --gid 1000 appgroup && \
    adduser --system --uid 1000 --ingroup appgroup appuser && \
    chown -R appuser:appgroup /app

USER appuser

HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
    CMD curl -f http://localhost:8080/api/salud || exit 1

ENTRYPOINT ["dotnet", "PlantillaMicroServicio.dll"] 