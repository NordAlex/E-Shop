#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["EShop.Catalog.WebApi/EShop.Catalog.WebApi.csproj", "EShop.Catalog.WebApi/"]
COPY ["EShop.Catalog.Application/EShop.Catalog.Application.csproj", "EShop.Catalog.Application/"]
COPY ["EShop.Catalog.Domain/EShop.Catalog.Domain.csproj", "EShop.Catalog.Domain/"]
COPY ["EShop.Catalog.Infrastructure/EShop.Catalog.Infrastructure.csproj", "EShop.Catalog.Infrastructure/"]
RUN dotnet restore "./EShop.Catalog.WebApi/./EShop.Catalog.WebApi.csproj"
COPY . .
WORKDIR "/src/EShop.Catalog.WebApi"
RUN dotnet build "./EShop.Catalog.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./EShop.Catalog.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Catalog.WebApi.dll"]