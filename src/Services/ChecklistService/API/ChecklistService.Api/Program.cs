using ChecklistService.Application.Extensions;
using ChecklistService.Persistence.Extensions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Settings = ChecklistService.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions<Settings.Configuration>().Bind(builder.Configuration.GetSection(nameof(Settings.Configuration)));

builder.Services.AddSingleton<CosmosClient>((serviceProvider) =>
{
    var configurationOptions = serviceProvider.GetRequiredService<IOptions<Settings.Configuration>>();
    var configuration = configurationOptions.Value;

    CosmosClient client = new(
        configuration.AzureCosmosDb.AccountEndpoint, 
        configuration.AzureCosmosDb.AccountKey
    );
    return client;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();