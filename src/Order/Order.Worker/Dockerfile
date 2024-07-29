FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Order/Order.Worker/Order.Worker.csproj", "Order/Order.Worker/"]
COPY ["Contracts/Contracts/Contracts.csproj", "Contracts/Contracts/"]
COPY ["Order/Order.Domain/Order.Domain.csproj", "Order/Order.Domain/"]
COPY ["Common/Common.Infrastructure/Common.Infrastructure.csproj", "Common/Common.Infrastructure/"]
COPY ["EventBus.Messages/EventBus.Messages.csproj", "EventBus.Messages/"]
COPY ["Common/Common.Api/Common.Api.csproj", "Common/Common.Api/"]
COPY ["Order/Order.Infrastructure/Order.Infrastructure.csproj", "Order/Order.Infrastructure/"]
COPY ["Order/Order.Api/Order.Api.csproj", "Order/Order.Api/"]
RUN dotnet restore "Order/Order.Worker/Order.Worker.csproj"
COPY . .
WORKDIR "/src/Order/Order.Worker"
RUN dotnet build "Order.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Order.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Worker.dll"]
