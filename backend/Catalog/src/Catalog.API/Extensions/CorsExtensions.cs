namespace Catalog.API.Extensions;

internal static class CorsExtensions
{
    internal static IServiceCollection AddCorsPolicies(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("Angular", policy =>
            {
                policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        return services;
    }
}
