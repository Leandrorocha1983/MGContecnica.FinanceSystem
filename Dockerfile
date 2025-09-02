# Use a imagem base oficial do .NET 8 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivo de solução
COPY *.sln .

# Copiar arquivos de projeto
COPY src/MGContecnica.Domain/MGContecnica.Domain.csproj src/MGContecnica.Domain/
COPY src/MGContecnica.Infrastructure/MGContecnica.Infrastructure.csproj src/MGContecnica.Infrastructure/
COPY src/MGContecnica.Application/MGContecnica.Application.csproj src/MGContecnica.Application/
COPY src/MGContecnica.API/MGContecnica.API.csproj src/MGContecnica.API/
COPY tests/MGContecnica.Tests.Unit/MGContecnica.Tests.Unit.csproj tests/MGContecnica.Tests.Unit/

# Restaurar dependências
RUN dotnet restore

# Copiar todo o código fonte
COPY . .

# Build da aplicação
RUN dotnet publish src/MGContecnica.API/MGContecnica.API.csproj -c Release -o /app/publish --no-restore

# Usar a imagem base runtime para produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados
COPY --from=build /app/publish .

# Criar diretório para o banco SQLite
RUN mkdir -p /app/data

# Expor a porta
EXPOSE 8080

# Configurar variáveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "MGContecnica.API.dll"]