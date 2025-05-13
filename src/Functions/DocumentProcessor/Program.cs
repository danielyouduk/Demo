using DocumentProcessor;
using MassTransit;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Services.Core.Events.ChecklistsEvents;
using Configuration = DocumentProcessor.Settings.Configuration;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddOptions<Configuration>().Bind(builder.Configuration.GetSection(nameof(Configuration)));
builder.Services.AddSingleton<CosmosClient>((serviceProvider) =>
{
    var configurationOptions = serviceProvider.GetRequiredService<IOptions<Configuration>>();
    var configuration = configurationOptions.Value;

    CosmosClient client = new(
        configuration.AzureCosmosDb.AccountEndpoint, 
        configuration.AzureCosmosDb.AccountKey
    );
    return client;
});

builder.Services.AddMassTransit(config =>
{
    config.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["Configuration:AzureServiceBus:ConnectionString"]);

        // Configure the consumer endpoint
        cfg.ReceiveEndpoint("pdf-generator", e =>
        {
            e.Consumer<ChecklistSubmittedConsumer>();
        });

        // Configure message topology
        cfg.Message<ChecklistSubmitted>(x => 
            x.SetEntityName("checklist-submitted"));
    });
});


builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();