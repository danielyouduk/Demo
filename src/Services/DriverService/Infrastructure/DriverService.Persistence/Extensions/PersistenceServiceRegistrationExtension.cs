using DriverService.Application.Contracts.Persistence;
using DriverService.Persistence.DatabaseContext;
using DriverService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DriverService.Persistence.Extensions;

public static class PersistenceServiceRegistrationExtension
{
    public static void AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DriverDatabaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<IDriverQueryRepository, DriverQueryRepository>();
        services.AddScoped<IDriverReportRepository, DriverReportRepository>();
    }
}