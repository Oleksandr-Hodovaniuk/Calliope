namespace Catalog.API;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }
}
