FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5400
ENV ASPNETCORE_URLS=http://+:5400

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# ---- copiar SOLO csproj primero (cache restore) ----
COPY SharedContracts/SharedContracts.csproj SharedContracts/

COPY Movements/Application/Application.csproj Movements/Application/
COPY Movements/DataAccess/DataAccess.csproj Movements/DataAccess/
COPY Movements/WebApi/WebApi.csproj Movements/WebApi/

# restore con todas las dependencias presentes
RUN dotnet restore Movements/WebApi/WebApi.csproj


# ---- copiar código completo después ----
COPY SharedContracts/. SharedContracts/
COPY Movements/. Movements/

WORKDIR /src/Movements/WebApi
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]

