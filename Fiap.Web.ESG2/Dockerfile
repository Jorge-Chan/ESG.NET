# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Como o build context é ./Fiap.Web.ESG2, o .csproj está aqui mesmo
COPY Fiap.Web.ESG2.csproj ./
RUN dotnet restore Fiap.Web.ESG2.csproj

# Copia o restante do projeto
COPY . .
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Fiap.Web.ESG2.csproj -c $BUILD_CONFIGURATION -o /app/out /p:UseAppHost=false

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Fiap.Web.ESG2.dll"]
