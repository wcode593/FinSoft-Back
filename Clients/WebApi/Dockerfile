FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5600
ENV ASPNETCORE_URLS=http://+:5600

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Persons/Domain/Domain.csproj Persons/Domain/
COPY Persons/Application/Application.csproj Persons/Application/
COPY Persons/DataAccess/DataAccess.csproj Persons/DataAccess/
COPY Persons/WebApi/WebApi.csproj Persons/WebApi/

RUN dotnet restore Persons/WebApi/WebApi.csproj

COPY Persons/. Persons/

WORKDIR /src/Persons/WebApi

# Publicar la aplicación en Release
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

# Comando de entrada
ENTRYPOINT ["dotnet", "WebApi.dll"]

