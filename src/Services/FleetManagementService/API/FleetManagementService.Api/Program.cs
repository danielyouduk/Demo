using FleetManagementService.Application.Extensions;
using FleetManagementService.Application.Settings;
using FleetManagementService.Persistence.Extensions;
using Services.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Services.AddApplicationConfiguration<FleetManagementServiceConfiguration>(builder.Configuration);

// Add services to the container.
builder.Services.AddSingleton(appConfig);
builder.Services.AddControllers();

builder.Services
    .AddApplicationServices()
    .AddPersistenceServices(appConfig)
    .AddMessageBusServices(appConfig)
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

// Configure the HTTP request pipeline.
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();