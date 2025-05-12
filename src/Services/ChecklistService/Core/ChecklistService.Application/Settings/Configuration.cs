namespace ChecklistService.Application.Settings;

public record Configuration
{
    public required AzureCosmosDb AzureCosmosDb { get; init; }
}

public record AzureCosmosDb
{
    public required string AccountEndpoint { get; init; }
    
    public required string AccountKey { get; init; }

    public required string DatabaseName { get; init; }

    public required string ContainerName { get; init; }
}