namespace FleetManagementService.Application.Settings;

public interface IConfiguration
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

public record Configuration : IConfiguration
{
    public required PostgresSqlSettings PostgresSqlSettings { get; init; }
    public required AzureServiceBusSettings AzureServiceBusSettings { get; init; }
    
    IPostgresSqlSettings IConfiguration.PostgresSqlSettings => PostgresSqlSettings;
    IAzureServiceBusSettings IConfiguration.AzureServiceBusSettings => AzureServiceBusSettings;
}

public record AzureServiceBusSettings : IAzureServiceBusSettings
{
    public required string ConnectionString { get; init; }
}

public record PostgresSqlSettings : IPostgresSqlSettings
{
    public required string ConnectionString { get; init; }
}