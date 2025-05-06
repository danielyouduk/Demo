using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleService.Application.Contracts.Persistence;
using VehicleService.Persistence.DatabaseContext;
using VehicleService.Persistence.Repositories;

namespace VehicleService.Persistence.Extensions;

public static class PersistenceServiceRegistrationExtension
{
    public static void AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<VehicleDatabaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<IVehicleRepository, VehicleRepository>();
    }
}