# Usando a imagem base do .NET SDK 8.0 para buildar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o arquivo da solução (sln) que deve estar na raiz do projeto
COPY ../../../*.sln .

# Copiar os arquivos dos projetos com os caminhos corretos
COPY ../../../src/Backend/MyBookRental.Application/*.csproj ./Backend/MyBookRental.Application/
COPY ../../../src/Backend/MyBookRental.Domain/*.csproj ./Backend/MyBookRental.Domain/
COPY ../../../src/Backend/MyBookRental.Infrastructure/*.csproj ./Backend/MyBookRental.Infrastructure/
COPY ../../../src/Backend/MyBookRental.API/*.csproj ./Backend/MyBookRental.API/

COPY ../../../src/Shared/MyBookRental.Communication/*.csproj ./Shared/MyBookRental.Communication/
COPY ../../../src/Shared/MyBookRental.Exceptions/*.csproj ./Shared/MyBookRental.Exceptions/

# Restaurar dependências do .NET
RUN dotnet restore

# Copiar todos os arquivos do projeto para o container
COPY ../../../ ./

# Build da aplicação
WORKDIR /app/src/Backend/MyBookRental.API
RUN dotnet publish -c Release -o out

# Usando a imagem runtime do .NET 8.0 para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/Backend/MyBookRental.API/out .

# Expondo a porta padrão do ASP.NET Core
EXPOSE 6501
ENV ASPNETCORE_URLS=http://+:6501

ENTRYPOINT ["dotnet", "MyBookRental.API.dll"]
