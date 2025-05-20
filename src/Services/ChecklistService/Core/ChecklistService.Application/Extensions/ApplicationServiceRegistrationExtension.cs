using System.Reflection;
using ChecklistService.Application.Validation.BaseValidation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Validation;

namespace ChecklistService.Application.Extensions;

public static class ApplicationServiceRegistrationExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssemblyContaining<ChecklistValidator<object>>();
        services.AddScoped<PagedRequestQueryValidator>();
        
        return services;
    }
}