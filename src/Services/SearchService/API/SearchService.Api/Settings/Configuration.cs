namespace SearchService.Api.Settings;

public record Configuration
{
    public required MongoDbSettings MongoDbSettings { get; init; }
    
    public required AzureServiceBusSettings AzureServiceBusSettings { get; init; }
}

public record MongoDbSettings
{
    public required string ConnectionString { get; init; }
    
    public required string DatabaseName { get; init; }
}

public record AzureServiceBusSettings
{
    public required string ConnectionString { get; init; }
}