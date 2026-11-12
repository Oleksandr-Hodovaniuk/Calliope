using AudioStorage.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AudioStorage.Infrastructure.Persistence;

internal static class PersistenceConfiguration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["AUDIO_STORAGE_DB_HOST"];
        var portValue = configuration["AUDIO_STORAGE_DB_PORT"];
        var database = configuration["AUDIO_STORAGE_DB_NAME"];
        var username = configuration["AUDIO_STORAGE_DB_USER"];
        var password = configuration["AUDIO_STORAGE_DB_PASSWORD"];

        if (string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(database) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "Database configuration is incomplete.");
        }

        if (!int.TryParse(portValue, out var port) || port <= 0 || port > 65535)
        {
            throw new InvalidOperationException(
                "DB_PORT is missing or invalid.");
        }

        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = port,
            Database = database,
            Username = username,
            Password = password
        }.ConnectionString;

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IApplicationDbContextInitialiser, ApplicationDbContextInitialiser>();

        return services;
    }
}
