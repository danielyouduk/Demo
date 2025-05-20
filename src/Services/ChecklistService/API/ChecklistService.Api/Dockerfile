FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 4001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["src/Services/ChecklistService/API/ChecklistService.Api/ChecklistService.Api.csproj", "src/Services/ChecklistService/API/ChecklistService.Api/"]
COPY ["src/Services/ChecklistService/Core/ChecklistService.Application/ChecklistService.Application.csproj", "src/Services/ChecklistService/Core/ChecklistService.Application/"]
COPY ["src/Shared/Services.Core/Services.Core.csproj", "src/Shared/Services.Core/"]
COPY ["src/Services/FleetManagementService/Core/FleetManagementService.Application/FleetManagementService.Application.csproj", "src/Services/FleetManagementService/Core/FleetManagementService.Application/"]
COPY ["src/Services/FleetManagementService/Core/FleetManagementService.Domain/FleetManagementService.Domain.csproj", "src/Services/FleetManagementService/Core/FleetManagementService.Domain/"]
COPY ["src/Services/ChecklistService/Core/ChecklistService.Domain/ChecklistService.Domain.csproj", "src/Services/ChecklistService/Core/ChecklistService.Domain/"]
COPY ["src/Services/ChecklistService/Infrastructure/ChecklistService.Persistence/ChecklistService.Persistence.csproj", "src/Services/ChecklistService/Infrastructure/ChecklistService.Persistence/"]
RUN dotnet restore "src/Services/ChecklistService/API/ChecklistService.Api/ChecklistService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/ChecklistService/API/ChecklistService.Api"
RUN dotnet build "./ChecklistService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./ChecklistService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChecklistService.Api.dll"]
