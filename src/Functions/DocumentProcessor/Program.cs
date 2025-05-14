using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Storage.Blobs;
using MassTransit;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using QuestPDF.Infrastructure;
using Configuration = DocumentProcessor.Settings.Configuration;

var builder = FunctionsApplication.CreateBuilder(args);
var appConfig = builder.Services.AddOptions<Configuration>().Bind(builder.Configuration.GetSection(nameof(Configuration)));

builder.Services.AddSingleton<CosmosClient>((serviceProvider) =>
{
    var configuration = serviceProvider.GetRequiredService<IOptions<Configuration>>().Value;

    CosmosClient client = new(
        configuration.AzureCosmosDbSettings.AccountEndpoint, 
        configuration.AzureCosmosDbSettings.AccountKey
    );
    return client;
});

builder.Services.AddSingleton<BlobServiceClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IOptions<Configuration>>().Value;
    
    return new BlobServiceClient(configuration.AzureBlobStorageSettings.ConnectionString);
});

builder.Services.AddMassTransit(config =>
{
    config.UsingAzureServiceBus((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IOptions<Configuration>>().Value;
        
        cfg.Host(configuration.AzureServiceBusSettings.ConnectionString);
        
        cfg.ConfigureJsonSerializerOptions(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            return options;
        });
    });
});
QuestPDF.Settings.License = LicenseType.Community;

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.Converters.Add(new JsonStringEnumConverter());
});


builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();