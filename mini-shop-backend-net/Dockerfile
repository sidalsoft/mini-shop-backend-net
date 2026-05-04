FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["mini-shop-backend-net/mini-shop-backend-net.csproj", "mini-shop-backend-net/"]
RUN dotnet restore "mini-shop-backend-net/mini-shop-backend-net.csproj"
COPY . .
WORKDIR "/src/mini-shop-backend-net"
RUN dotnet build "./mini-shop-backend-net.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./mini-shop-backend-net.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mini-shop-backend-net.dll"]
