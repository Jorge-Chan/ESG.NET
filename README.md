# ?? Projeto - ESG

Aplicação web desenvolvida em **.NET 8** com o objetivo de monitorar e registrar dados de emissões e compensações de carbono, contribuindo para a construção de **cidades inteligentes e sustentáveis**.

---

## ?? Como executar localmente com Docker

### Pré-requisitos
Antes de iniciar, certifique-se de ter instalado:
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/)

### Passos para rodar localmente

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/Jorge-Chan/ESG.NET.git
   cd ESG.NET3

2. Construa a imagem Docker:
   
 docker build -t fiap-web-esg2 .

3. Execute o container:

 docker run -d -p 8080:8080 fiap-web-esg2

4. Acesse no navegador:
  http://localhost:8080

  A aplicação iniciará utilizando o ambiente Production definido no Dockerfile.

  Pipeline CI/CD


O pipeline automatizado foi desenvolvido com GitHub Actions e realiza build, teste, push da imagem no ACR e deploy automático em dois ambientes no Azure Web App.

Ferramentas utilizadas

GitHub Actions — Orquestra o fluxo de CI/CD.

Azure Container Registry (ACR) — Armazena as imagens Docker publicadas.

Azure Web App — Hospeda a aplicação nos ambientes de staging e produção.

Docker Buildx — Utilizado para build e push otimizados.

Azure CLI — Para autenticação e deploy da imagem no Azure.

Etapas do pipeline

| Etapa                      | Descrição                                                                   |
| -------------------------- | --------------------------------------------------------------------------- |
| **Checkout**               | Faz o download do repositório.                                              |
| **Setup .NET**             | Instala o SDK do .NET 8.                                                    |
| **Restore / Build / Test** | Restaura dependências, compila o projeto e executa testes automatizados.    |
| **Build Docker Image**     | Cria a imagem Docker da aplicação.                                          |
| **Push to ACR**            | Envia a imagem para o Azure Container Registry (`esgregistry2.azurecr.io`). |
| **Deploy STAGING**         | Faz o deploy automático no Web App `emissaocarbono-staging`.                |
| **Deploy PROD**            | Retaga a imagem como `:prod` e faz deploy no Web App `emissaocarbono-api`.  |

Ambos os deploys são executados automaticamente após o build — o pipeline garante que a mesma imagem usada em staging será utilizada em produção.

Containerização

A aplicação foi containerizada com Docker, garantindo portabilidade e reprodutibilidade do ambiente.

# ===========================
# Build stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=Release

# Telemetria e cache
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    ASPNETCORE_TELEMETRY_OPTOUT=1 \
    NUGET_PACKAGES=/root/.nuget/packages

# Copia e restaura dependências
COPY Fiap.Web.ESG2/Fiap.Web.ESG2.csproj ./Fiap.Web.ESG2/
RUN dotnet restore Fiap.Web.ESG2/Fiap.Web.ESG2.csproj

# Copia o restante do código
COPY . .

# Publica a aplicação
RUN dotnet publish Fiap.Web.ESG2/Fiap.Web.ESG2.csproj -c ${BUILD_CONFIGURATION} -o /app/publish --no-restore /p:UseAppHost=false

# ===========================
# Runtime stage
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Fiap.Web.ESG2.dll"]

Estratégias adotadas

Multistage build: reduz o tamanho final da imagem.

Cache de dependências NuGet: acelera builds repetidos.

Variável de ambiente ASPNETCORE_ENVIRONMENT: configurada para Production no container.

Healthcheck HTTP: garante que a aplicação esteja respondendo corretamente.

## ?? Prints do Funcionamento

![Build e Push ACR](images/WhatsApp%20Image%202025-10-14%20at%2022.14.14.jpeg)
![Deploy Staging](images/WhatsApp%20Image%202025-10-14%20at%2022.44.57.jpeg)


Tecnologias Utilizadas


| Categoria           | Tecnologias                             |
| ------------------- | --------------------------------------- |
| **Linguagem**       | C# (.NET 8)                             |
| **Framework**       | ASP.NET Core MVC                        |
| **Testes**          | xUnit, Moq                              |
| **Banco de Dados**  | Oracle (via Entity Framework Core)      |
| **Containerização** | Docker, Docker Buildx                   |
| **CI/CD**           | GitHub Actions, Azure CLI               |
| **Infraestrutura**  | Azure Container Registry, Azure Web App |
| **Versionamento**   | Git + GitHub                            |
