using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using FleetManagementService.Application.Settings;
using FleetManagementService.Persistence.Common;
using FleetManagementService.Persistence.DatabaseContext;
using FleetManagementService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagementService.Persistence.Extensions;

public static class PersistenceServiceRegistrationExtension
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, FleetManagementServiceConfiguration serviceConfiguration)
    {
        services.AddDbContext<FleetManagementDatabaseContext>((serviceProvider, options) =>
        {
            var config = serviceProvider.GetRequiredService<FleetManagementServiceConfiguration>();
            options.UseNpgsql(config.PostgresSqlSettings.ConnectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        
        return services;
    }
}