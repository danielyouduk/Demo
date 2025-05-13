namespace DocumentProcessor.Settings;

public record Configuration
{
    public required AzureCosmosDb AzureCosmosDb { get; init; }
    
    public required BlobStorageSettings BlobStorageSettings { get; init; }
}

public record AzureCosmosDb
{
    public required string AccountEndpoint { get; init; }
    
    public required string AccountKey { get; init; }

    public required string DatabaseName { get; init; }

    public required string ContainerName { get; init; }
}

public class BlobStorageSettings
{
    public required string ConnectionString { get; init; }
}
