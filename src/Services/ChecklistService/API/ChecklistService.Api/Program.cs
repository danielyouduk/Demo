using ChecklistService.Application.Extensions;
using ChecklistService.Application.Settings;
using ChecklistService.Persistence.Extensions;
using Services.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Services.AddApplicationConfiguration<ChecklistServiceConfiguration>(builder.Configuration);

// Add services to the container.
builder.Services.AddSingleton(appConfig);
builder.Services.AddControllers();

builder.Services
    .AddApplicationServices()
    .AddPersistenceServices(appConfig)
    .AddMessageBusServices(appConfig)
    .AddCosmosDbServices(appConfig)
    .AddOpenApi()
    .AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();