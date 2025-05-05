namespace SearchService.Api.Settings;

public record Configuration
{
    public required Azure Azure { get; init; }
}

public record Azure
{
    public required CosmosDb CosmosDb { get; init; }
}

public record CosmosDb
{
    public required string ConnectionString { get; init; }

    public required string DatabaseName { get; init; }

    public required string ContainerName { get; init; }
}