#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7118
EXPOSE 443
ENV ASPNETCORE_URLS=http://*:7118

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["EShop.Carting.WebApi/EShop.Carting.WebApi.csproj", "EShop.Carting.WebApi/"]
COPY ["EShop.Carting.Application/EShop.Carting.Application.csproj", "EShop.Carting.Application/"]
COPY ["EShop.Carting.Domain/EShop.Carting.Domain.csproj", "EShop.Carting.Domain/"]
COPY ["EShop.Carting.Infrastructure/EShop.Carting.Infrastructure.csproj", "EShop.Carting.Infrastructure/"]
RUN dotnet restore "./EShop.Carting.WebApi/./EShop.Carting.WebApi.csproj"
COPY . .
WORKDIR "/src/EShop.Carting.WebApi"
RUN dotnet build "./EShop.Carting.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./EShop.Carting.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Carting.WebApi.dll"]