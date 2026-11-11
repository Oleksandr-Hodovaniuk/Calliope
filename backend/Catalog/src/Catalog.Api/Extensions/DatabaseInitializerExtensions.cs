using Catalog.Application.Interfaces;

namespace Catalog.Api.Extensions;

internal static class DatabaseExtensions
{
    internal static async Task InitialiseDatabaseAsync(this WebApplication app, CancellationToken ct = default)
    {
        if (app.Environment.IsDevelopment() && app.Configuration.GetValue<bool>("SEED_ENABLED"))
        {
            try
            {
                using var scope = app.Services.CreateScope();

                var dbInitialiser = scope.ServiceProvider
                    .GetRequiredService<IApplicationDbContextInitialiser>();

                await dbInitialiser.InitialiseAsync(ct);
                await dbInitialiser.SeedAsync(ct);
            }
            catch (Exception ex)
            {
                app.Logger.LogCritical(ex, "Database initialization failed.");
                throw;
            }
        }
    }
}
