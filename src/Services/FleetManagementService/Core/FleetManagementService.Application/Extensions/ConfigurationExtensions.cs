using FleetManagementService.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagementService.Application.Extensions;

public class ConfigurationBuilder
{
    private readonly IServiceCollection _services;
    private readonly IApplicationConfiguration _applicationConfig;

    public ConfigurationBuilder(IServiceCollection services, IApplicationConfiguration applicationConfig)
    {
        _services = services;
        _applicationConfig = applicationConfig;
    }

    public ConfigurationBuilder AddSetting<TInterface, TImplementation>(Func<IApplicationConfiguration, TImplementation> configSelector) 
        where TInterface : class
        where TImplementation : class, TInterface
    {
        _services.AddSingleton<TInterface>(configSelector(_applicationConfig));
        return this;
    }
}

public static class ConfigurationExtensions
{
    public static ConfigurationBuilder AddApplicationConfiguration(
        this IServiceCollection services,
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
        
        services.AddSingleton<IApplicationConfiguration>(applicationConfig);

        return new ConfigurationBuilder(services, applicationConfig);
    }
}
