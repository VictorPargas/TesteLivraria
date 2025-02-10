# Usando a imagem base do .NET SDK 8.0 para buildar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar a solução e todos os projetos
COPY . .

# Restaurar dependências
RUN dotnet restore

# Build da aplicação
WORKDIR /app/src/Backend/MyBookRental.API
RUN dotnet publish -c Release -o /app/out

# Usando a imagem runtime do .NET 8.0 para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expondo a porta padrão do ASP.NET Core
EXPOSE 6501
ENV ASPNETCORE_URLS=http://+:6501

ENTRYPOINT ["dotnet", "MyBookRental.API.dll"]
