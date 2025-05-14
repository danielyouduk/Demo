using FleetManagementService.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagementService.Application.Extensions;

public static class ConfigurationExtensions
{
    public static void AddApplicationConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var applicationConfig = configuration.GetSection(nameof(ApplicationConfiguration))
            .Get<ApplicationConfiguration>(options =>
            {
                options.ErrorOnUnknownConfiguration = true;
                options.BindNonPublicProperties = false;
            }) ?? throw new InvalidOperationException($"Configuration section '{nameof(ApplicationConfiguration)}' is missing or invalid.");

        services.AddOptions<ApplicationConfiguration>()
            .Bind(configuration.GetSection(nameof(ApplicationConfiguration)));
        
        // Register interfaces
        services.AddSingleton<IApplicationConfiguration>(applicationConfig);
        services.AddSingleton<IAzureServiceBusSettings>(applicationConfig.AzureServiceBusSettings);
        services.AddSingleton<IPostgresSqlSettings>(applicationConfig.PostgresSqlSettings);
    }

}