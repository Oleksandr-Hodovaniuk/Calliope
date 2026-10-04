using Catalog.API.Extensions;

namespace Catalog.API;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddCorsPolicies();

        services.AddJwtAuthentication();

        return services;
    }
}
