using QuickHire.Infrastructure.Extensions;
using static QuickHire.Infrastructure.Extensions.ServiceCollectionExtensions;
using static QuickHire.Application.Common.Extensions.ServiceCollectionExtensions;
using System.Reflection;
namespace QuickHire.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }

    public static IServiceCollection RegisterApplication(this IServiceCollection services, Assembly assembly)
    {
        services.AddApplication(assembly);
        return services;
    }
}
