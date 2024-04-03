FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Mediporta.Api/Mediporta.Api.csproj", "Mediporta.Api/"]
COPY ["src/Mediporta.Infrastructure/Mediporta.Infrastructure.csproj", "Mediporta.Infrastructure/"]
COPY ["src/Mediporta.Application/Mediporta.Application.csproj", "Mediporta.Application/"]
COPY ["src/Mediporta.Core/Mediporta.Core.csproj", "Mediporta.Core/"]
RUN dotnet restore "Mediporta.Api/Mediporta.Api.csproj"
COPY . .
WORKDIR "/src/src/Mediporta.Api/"

RUN dotnet build "Mediporta.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build 

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Mediporta.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mediporta.Api.dll"]



