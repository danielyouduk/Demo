using FleetManagementService.Application.Extensions;
using FleetManagementService.Application.Settings;
using FleetManagementService.Persistence.Extensions;
using Serilog;
using Services.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Services.AddApplicationConfiguration<FleetManagementServiceConfiguration>(builder.Configuration);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/fleetmanagement-.txt", rollingInterval: RollingInterval.Day)
);


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

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();