# ===========================
# Build stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=Release

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    ASPNETCORE_TELEMETRY_OPTOUT=1 \
    NUGET_PACKAGES=/root/.nuget/packages \
    NUGET_FALLBACK_PACKAGES=""

# 1) Copia solução e csproj para aproveitar cache de restore
COPY Fiap.Web.ESG2.sln ./
COPY Fiap.Web.ESG2/Fiap.Web.ESG2.csproj Fiap.Web.ESG2/

# 2) Restore inicial (aproveita cache)
RUN dotnet restore Fiap.Web.ESG2/Fiap.Web.ESG2.csproj --nologo

# 3) Copia o resto do código
COPY . .

# 4) (IMPORTANTE) Restore completo após copiar tudo
WORKDIR /src/Fiap.Web.ESG2
RUN dotnet restore --nologo

# 5) Publish (sem apphost). Pode manter --no-restore agora porque restauramos acima
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

# Healthcheck opcional
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

HEALTHCHECK --interval=30s --timeout=5s --start-period=20s --retries=3 \
  CMD curl -fsS http://localhost:8080/ || exit 1

COPY --from=build /app/publish /app

RUN adduser --disabled-password --gecos "" appuser \
 && chown -R appuser:appuser /app
USER appuser

ENTRYPOINT ["dotnet", "Fiap.Web.ESG2.dll"]
