using Azure.Search.Documents;
using MassTransit;
using Microsoft.Azure.Cosmos;
using SearchService.Api.Consumers;
using SearchService.Api.Repositories;
using SearchService.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton(s =>
{
    var configuration = s.GetService<IConfiguration>();
    var cosmosClient = new CosmosClient(configuration["Azure:CosmosDb:ConnectionString"]);

    // Pass the correct settings to the repository
    var repository = new DriverCosmosRepository(
        cosmosClient,
        configuration["Azure:CosmosDb:DatabaseName"],
        configuration["Azure:CosmosDb:ContainerName"]);
    
    InitializeCosmosDbAsync(
        cosmosClient, 
        configuration["Azure:CosmosDb:DatabaseName"], 
        configuration["Azure:CosmosDb:ContainerName"]
    ).GetAwaiter().GetResult(); 
    
    return repository;
});


builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<DriverCreatedConsumer>();

    config.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["Azure:ServiceBus:ConnectionString"]);

        // Configure the DriverCreatedConsumer for this service
        cfg.ReceiveEndpoint("driver-created-search-queue", e =>
        {
            e.ConfigureConsumer<DriverCreatedConsumer>(context);
        });
    });
});

builder.Services.AddSingleton(s =>
{
    return new SearchClient(
        new Uri(builder.Configuration["Azure:AzureSearch:Endpoint"]),
        builder.Configuration["Azure:AzureSearch:IndexName"],
        new Azure.AzureKeyCredential(builder.Configuration["Azure:AzureSearch:ApiKey"]));
});

builder.Services.AddScoped<DriverSearchService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();



static async Task InitializeCosmosDbAsync(CosmosClient cosmosClient, string databaseName, string containerName)
{
    var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
    // Customize partition key path as per your design
    await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties
    {
        Id = containerName,
        PartitionKeyPath = "/demo"
    });
}