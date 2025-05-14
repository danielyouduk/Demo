namespace ChecklistService.Application.Settings;

public record Configuration
{
    public required AzureCosmosDbSettings AzureCosmosDbSettings { get; init; }
    
    public required AzureServiceBusSettings AzureServiceBusSettings { get; init; }
}

public record AzureCosmosDbSettings
{
    public required string AccountEndpoint { get; init; }
    
    public required string AccountKey { get; init; }

    public required string DatabaseName { get; init; }

    public required string ContainerName { get; init; }
}

public record AzureServiceBusSettings
{
    public required string ConnectionString { get; init; }
}