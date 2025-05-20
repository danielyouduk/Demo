using System.Reflection;
using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Validation;

namespace FleetManagementService.Application.Extensions;

public static class ApplicationServiceRegistrationExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssemblyContaining<FleetManagementValidator<object>>();
        services.AddScoped<PagedRequestQueryValidator>();

        return services;
    }
}