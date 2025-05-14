namespace FleetManagementService.Application.Settings;

public interface IApplicationConfiguration
{
    IPostgresSqlSettings PostgresSqlSettings { get; }
    IAzureServiceBusSettings AzureServiceBusSettings { get; }
}

public interface IAzureServiceBusSettings
{
    string ConnectionString { get; }
}

public interface IPostgresSqlSettings
{
    string ConnectionString { get; }
}

public record ApplicationConfiguration : IApplicationConfiguration
{
    public required PostgresSqlSettings PostgresSqlSettings { get; init; }
    public required AzureServiceBusSettings AzureServiceBusSettings { get; init; }
    
    IPostgresSqlSettings IApplicationConfiguration.PostgresSqlSettings => PostgresSqlSettings;
    IAzureServiceBusSettings IApplicationConfiguration.AzureServiceBusSettings => AzureServiceBusSettings;
}

public record AzureServiceBusSettings : IAzureServiceBusSettings
{
    public required string ConnectionString { get; init; }
}

public record PostgresSqlSettings : IPostgresSqlSettings
{
    public required string ConnectionString { get; init; }
}