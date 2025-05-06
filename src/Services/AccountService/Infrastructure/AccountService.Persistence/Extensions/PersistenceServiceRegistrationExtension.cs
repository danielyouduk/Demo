using AccountService.Application.Contracts;
using AccountService.Persistence.DatabaseContext;
using AccountService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Persistence.Extensions;

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