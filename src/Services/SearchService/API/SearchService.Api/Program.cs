using Azure.Search.Documents;
using MassTransit;
using SearchService.Api.Consumers;
using SearchService.Api.Repositories;
using SearchService.Api.Services;
using Settings = SearchService.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOptions<Settings.Configuration>().Bind(builder.Configuration.GetSection(nameof(Settings.Configuration)));

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<DriverCreatedConsumer>();

    config.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["Azure:ServiceBus:ConnectionString"]);

        cfg.SubscriptionEndpoint(
            subscriptionName: "fleet-management-driver-created-search",
            topicPath:"fleet-management-driver-created", 
            configure: e =>
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
builder.Services.AddScoped<IDriverCosmosRepository, DriverCosmosRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();
