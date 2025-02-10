# Usando a imagem base do .NET SDK 8.0 para buildar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos de projeto e restaurar as dependências
COPY ../../*.sln .  # Caminho ajustado para o arquivo .sln
COPY ../MyBookRental.Application/*.csproj ./MyBookRental.Application/
COPY ../MyBookRental.Domain/*.csproj ./MyBookRental.Domain/
COPY ../MyBookRental.Infrastructure/*.csproj ./MyBookRental.Infrastructure/
COPY ../MyBookRental.Communication/*.csproj ./MyBookRental.Communication/
COPY ../MyBookRental.Exception/*.csproj ./MyBookRental.Exception/
COPY ./MyBookRental.API/*.csproj ./MyBookRental.API/

RUN dotnet restore

# Copiar o restante dos arquivos e construir a aplicação
COPY ../../ .  # Copiando todo o projeto corretamente
WORKDIR /app/MyBookRental.API
RUN dotnet publish -c Release -o out

# Usando a imagem runtime do .NET 8.0 para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/MyBookRental.API/out .

# Expondo a porta padrão do ASP.NET Core
EXPOSE 6501
ENV ASPNETCORE_URLS=http://+:6501

ENTRYPOINT ["dotnet", "MyBookRental.API.dll"]
