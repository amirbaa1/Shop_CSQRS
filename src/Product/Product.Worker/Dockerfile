FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Product/Product.Worker/Product.Worker.csproj", "Product/Product.Worker/"]
COPY ["EventBus.Messages/EventBus.Messages.csproj", "EventBus.Messages/"]
COPY ["Product/Product.Api/Product.Api.csproj", "Product/Product.Api/"]
COPY ["Product/Product.Infrastructure/Product.Infrastructure.csproj", "Product/Product.Infrastructure/"]
COPY ["Product/Product.Domain/Product.Domain.csproj", "Product/Product.Domain/"]
COPY ["Common/Common.Infrastructure/Common.Infrastructure.csproj", "Common/Common.Infrastructure/"]
COPY ["Contracts/Contracts/Contracts.csproj", "Contracts/Contracts/"]
COPY ["Common/Common.Api/Common.Api.csproj", "Common/Common.Api/"]
RUN dotnet restore "Product/Product.Worker/Product.Worker.csproj"
COPY . .
WORKDIR "/src/Product/Product.Worker"
RUN dotnet build "Product.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Product.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Worker.dll"]
