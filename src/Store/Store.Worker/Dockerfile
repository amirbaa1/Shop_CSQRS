FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Store/Store.Worker/Store.Worker.csproj", "Store/Store.Worker/"]
COPY ["Common/Common.Infrastructure/Common.Infrastructure.csproj", "Common/Common.Infrastructure/"]
COPY ["Contracts/Contracts/Contracts.csproj", "Contracts/Contracts/"]
COPY ["Store/Store.Domain/Store.Domain.csproj", "Store/Store.Domain/"]
COPY ["Store/Store.Infrastructure/Store.Infrastructure.csproj", "Store/Store.Infrastructure/"]
RUN dotnet restore "Store/Store.Worker/Store.Worker.csproj"
COPY . .
WORKDIR "/src/Store/Store.Worker"
RUN dotnet build "Store.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Store.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.Worker.dll"]
