# Etapa 1: Definindo a imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Defina o diretório de trabalho no contêiner
WORKDIR /src

# Copie todos os arquivos .csproj e restaure as dependências
COPY ["EmployeeManagementApi/EmployeeManagementApi.csproj", "EmployeeManagementApi/"]
COPY ["EmployeeManagement.Application/EmployeeManagement.Application.csproj", "EmployeeManagement.Application/"]
COPY ["EmployeeManagement.Infrastructure/EmployeeManagement.Infrastructure.csproj", "EmployeeManagement.Infrastructure/"]
COPY ["EmployeeManagement.Domain/EmployeeManagement.Domain.csproj", "EmployeeManagement.Domain/"]
COPY ["EmployeeManagement.Tests/EmployeeManagement.Tests.csproj", "EmployeeManagement.Tests/"]

# Restaura as dependências do projeto
RUN dotnet restore "EmployeeManagementApi/EmployeeManagementApi.csproj"

# Copia o restante dos arquivos para o contêiner
COPY . .

# Publica a aplicação
RUN dotnet publish "EmployeeManagementApi/EmployeeManagementApi.csproj" -c Release -o /app/publish

# Etapa 2: Definindo a imagem base para o ambiente de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copia os arquivos publicados da etapa anterior
COPY --from=build /app/publish .

# Defina o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "EmployeeManagementApi.dll"]
