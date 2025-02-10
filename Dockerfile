# Usando a imagem base do .NET SDK 8.0 com verificação de integridade (sha256)
FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build
WORKDIR /App

# Copiar apenas arquivos de projeto (.csproj) e a solução (.sln) para restaurar dependências
COPY MyBookRental.sln ./

COPY src/Backend/MyBookRental.Application/*.csproj ./src/Backend/MyBookRental.Application/
COPY src/Backend/MyBookRetal.Domain/*.csproj ./src/Backend/MyBookRetal.Domain/
COPY src/Backend/MyBookRental.Infrastructure/*.csproj ./src/Backend/MyBookRental.Infrastructure/
COPY src/Backend/MyBookRental.API/*.csproj ./src/Backend/MyBookRental.API/

COPY src/Shared/MyBookRental.Communication/*.csproj ./src/Shared/MyBookRental.Communication/
COPY src/Shared/MyBookRental.Exceptions/*.csproj ./src/Shared/MyBookRental.Exceptions/    

# Restaurar as dependências como uma camada distinta
RUN dotnet restore

# Copiar o restante dos arquivos do projeto
COPY . .

# Build e publish da aplicação em modo Release
WORKDIR /App/src/Backend/MyBookRental.API
RUN dotnet publish -c Release -o /App/out

# Construir a imagem final do runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff
WORKDIR /App
COPY --from=build /App/out .

# Expor a porta padrão do ASP.NET Core
EXPOSE 6501
ENV ASPNETCORE_URLS=http://+:6501

# Definir o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "MyBookRental.API.dll"]
