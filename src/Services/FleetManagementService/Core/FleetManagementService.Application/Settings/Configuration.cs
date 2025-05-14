namespace FleetManagementService.Application.Settings;

public record Configuration
{
    public required PostgresSqlSettings PostgresSqlSettings { get; init; }
    
    public required AzureServiceBusSettings AzureServiceBusSettings { get; init; }
}

public record AzureServiceBusSettings
{
    public required string ConnectionString { get; init; }
}

public record PostgresSqlSettings
{
    public required string ConnectionString { get; init; }
}