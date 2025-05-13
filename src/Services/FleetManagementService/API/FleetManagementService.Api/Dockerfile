FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 5001
EXPOSE 7777

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["src/Services/FleetManagementService/API/FleetManagementService.Api/FleetManagementService.Api.csproj", "src/Services/FleetManagementService/API/FleetManagementService.Api/"]
COPY ["src/Shared/Services.Core/Services.Core.csproj", "src/Shared/Services.Core/"]
COPY ["src/Services/FleetManagementService/Core/FleetManagementService.Application/FleetManagementService.Application.csproj", "src/Services/FleetManagementService/Core/FleetManagementService.Application/"]
COPY ["src/Services/FleetManagementService/Core/FleetManagementService.Domain/FleetManagementService.Domain.csproj", "src/Services/FleetManagementService/Core/FleetManagementService.Domain/"]
COPY ["src/Services/FleetManagementService/Infrastructure/FleetManagementService.Persistence/FleetManagementService.Persistence.csproj", "src/Services/FleetManagementService/Infrastructure/FleetManagementService.Persistence/"]
RUN dotnet restore "src/Services/FleetManagementService/API/FleetManagementService.Api/FleetManagementService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/FleetManagementService/API/FleetManagementService.Api"
RUN dotnet build "./FleetManagementService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./FleetManagementService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetManagementService.Api.dll"]
