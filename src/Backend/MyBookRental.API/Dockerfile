# Usando a imagem base do .NET SDK para buildar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos de projeto e restaurar as dependências
COPY *.sln .
COPY MyBookRental.API/*.csproj ./MyBookRental.API/
COPY MyBookRental.Application/*.csproj ./MyBookRental.Application/
COPY MyBookRental.Domain/*.csproj ./MyBookRental.Domain/
COPY MyBookRental.Infrastructure/*.csproj ./MyBookRental.Infrastructure/
COPY MyBookRental.Communication/*.csproj ./MyBookRental.Communication/
COPY MyBookRental.Exception/*.csproj ./MyBookRental.Exception/

RUN dotnet restore

# Copiar o restante dos arquivos e construir a aplicação
COPY . .
WORKDIR /app/MyBookRental.API
RUN dotnet publish -c Release -o out

# Usando a imagem runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/MyBookRental.API/out .

# Expondo a porta padrão do ASP.NET Core
EXPOSE 6501
ENV ASPNETCORE_URLS=http://+:6501

ENTRYPOINT ["dotnet", "MyBookRental.API.dll"]
