using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Catalog.API.Extensions;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.Authority = "http://localhost:8080/realms/Calliope";

                opt.Audience = "backend-api";

                opt.RequireHttpsMetadata = false;
            });

        return services;
    }
}
