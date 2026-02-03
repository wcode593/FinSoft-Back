# ===================================
# RUNTIME
# ===================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5200
ENV ASPNETCORE_URLS=http://+:5200


# ===================================
# BUILD
# ===================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# ---- copiar csproj primero (MEJOR CACHE) ----
COPY SharedContracts/SharedContracts.csproj SharedContracts/

COPY Accounts/Application/Application.csproj Accounts/Application/
COPY Accounts/DataAccess/DataAccess.csproj Accounts/DataAccess/
COPY Accounts/WebApi/WebApi.csproj Accounts/WebApi/

# restore con dependencias presentes
RUN dotnet restore Accounts/WebApi/WebApi.csproj


# ---- copiar código completo ----
COPY SharedContracts/. SharedContracts/
COPY Accounts/. Accounts/

WORKDIR /src/Accounts/WebApi
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


# ===================================
# FINAL
# ===================================
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]
