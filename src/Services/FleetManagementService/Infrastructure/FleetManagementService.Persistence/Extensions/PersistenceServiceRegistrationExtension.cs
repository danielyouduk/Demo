using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Persistence.DatabaseContext;
using FleetManagementService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagementService.Persistence.Extensions;

public static class PersistenceServiceRegistrationExtension
{
    public static void AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AccountDatabaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAccountRepository, AccountRepository>();
    }
}