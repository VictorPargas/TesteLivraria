# Usando a imagem base do .NET SDK 8.0 para buildar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o arquivo da solução
COPY MyBookRental.sln .

# Copiar os arquivos de cada projeto com caminhos corretos
COPY src/Backend/MyBookRental.Application/*.csproj ./src/Backend/MyBookRental.Application/
COPY src/Backend/MyBookRental.Domain/*.csproj ./src/Backend/MyBookRetal.Domain/
COPY src/Backend/MyBookRental.Infrastructure/*.csproj ./src/Backend/MyBookRental.Infrastructure/
COPY src/Backend/MyBookRental.API/*.csproj ./src/Backend/MyBookRental.API/

COPY src/Shared/MyBookRental.Communication/*.csproj ./src/Shared/MyBookRental.Communication/
COPY src/Shared/MyBookRental.Exceptions/*.csproj ./src/Shared/MyBookRental.Exceptions/

# Restaurar as dependências
RUN dotnet restore

# Copiar todos os arquivos do projeto
COPY . .

# Build da aplicação
WORKDIR /app/src/Backend/MyBookRental.API
RUN dotnet publish -c Release -o /app/out

# Usar a imagem runtime do .NET 8.0 para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expor a porta padrão do ASP.NET Core
EXPOSE 6501
ENV ASPNETCORE_URLS=http://+:6501

ENTRYPOINT ["dotnet", "MyBookRental.API.dll"]
