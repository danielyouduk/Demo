FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 7002

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["src/Services/SearchService/API/SearchService.Api/SearchService.Api.csproj", "src/Services/SearchService/API/SearchService.Api/"]
COPY ["src/Shared/Services.Core/Services.Core.csproj", "src/Shared/Services.Core/"]
COPY ["src/Services/SearchService/Core/SearchService.Application/SearchService.Application.csproj", "src/Services/SearchService/Core/SearchService.Application/"]
COPY ["src/Services/SearchService/Infrastructure/SearchService.Persistence/SearchService.Persistence.csproj", "src/Services/SearchService/Infrastructure/SearchService.Persistence/"]
RUN dotnet restore "src/Services/SearchService/API/SearchService.Api/SearchService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/SearchService/API/SearchService.Api"
RUN dotnet build "./SearchService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./SearchService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchService.Api.dll"]
