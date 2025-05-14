using Microsoft.Extensions.Options;
using SearchService.Api.Data;
using SearchService.Api.Settings;
using SearchService.Application.Extensions;
using SearchService.Application.Settings;
using Services.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Services.AddApplicationConfiguration<SearchServiceConfiguration>(builder.Configuration);

builder.Services.AddSingleton(appConfig);
builder.Services.AddControllers();

builder.Services.AddMessageBusServices(appConfig);

var app = builder.Build();

try
{
    var configuration = app.Services.GetRequiredService<IOptions<Configuration>>().Value;
    await DbInitializer.InitializeAsync(app, configuration);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();