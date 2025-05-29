using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using QuickHire.Application.Common.Behaviors;
using QuickHire.Application.Common.Mapping;
using FluentValidation;

namespace QuickHire.Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, Assembly assembly)
    {
        services//.AddMediator(assembly)
            //.AddFluentValidation(assembly)
            .AddMapster(assembly);
        return services;
    }
    private static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
        });

        return services;
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }

    public static void AddMapster(this IServiceCollection services, Assembly assembly)
    {
        services.RegisterMapsterConfiguration(assembly);
    }

}
