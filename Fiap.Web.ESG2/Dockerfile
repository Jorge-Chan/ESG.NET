# ===========================
# Build stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=Release

# Telemetria / cache NuGet
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    ASPNETCORE_TELEMETRY_OPTOUT=1 \
    NUGET_PACKAGES=/root/.nuget/packages \
    NUGET_FALLBACK_PACKAGES=""

# 0) Usa o NuGet.config do projeto (zera fallback folders)
COPY NuGet.config ./NuGet.config

# 1) Copia o csproj e faz o restore (gera obj/project.assets.json)
COPY Fiap.Web.ESG2.csproj ./
RUN dotnet restore Fiap.Web.ESG2.csproj --nologo

# 2) Copia o restante do código
COPY . .

# ⚠️ Não apagar ./obj (precisa do project.assets.json para o publish)
# Apenas limpa a pasta bin se quiser
RUN rm -rf ./bin

# 3) Publish (sem apphost, e sem restaurar de novo)
RUN dotnet publish Fiap.Web.ESG2.csproj \
    -c ${BUILD_CONFIGURATION} \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# ===========================
# Runtime stage
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_EnableDiagnostics=0 \
    DOTNET_GCServer=1 \
    DOTNET_PRINT_TELEMETRY_MESSAGE=false

EXPOSE 8080

# Healthcheck simples — altere /health se tiver endpoint próprio
HEALTHCHECK --interval=30s --timeout=5s --start-period=20s --retries=3 \
  CMD curl -fsS http://localhost:8080/ || exit 1

COPY --from=build /app/publish /app

RUN adduser --disabled-password --gecos "" appuser \
 && chown -R appuser:appuser /app
USER appuser

ENTRYPOINT ["dotnet", "Fiap.Web.ESG2.dll"]
