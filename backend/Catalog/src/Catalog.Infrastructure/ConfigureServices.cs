using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Extensions;
using Catalog.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<IApplicationDbContextInitialiser, ApplicationDbContextInitialiser>();

        return services;   
    }
}
