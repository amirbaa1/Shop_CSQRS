#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Basket/Basket.Api/Basket.Api.csproj", "Basket/Basket.Api/"]
COPY ["Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "Basket/Basket.Infrastructure/"]
COPY ["Common/Common.Infrastructure/Common.Infrastructure.csproj", "Common/Common.Infrastructure/"]
COPY ["Contracts/Contracts/Contracts.csproj", "Contracts/Contracts/"]
COPY ["Common/Common.Api/Common.Api.csproj", "Common/Common.Api/"]
COPY ["Basket/Basket.Domain/Basket.Domain.csproj", "Basket/Basket.Domain/"]
RUN dotnet restore "./Basket/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Basket/Basket.Api"
RUN dotnet build "./Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]