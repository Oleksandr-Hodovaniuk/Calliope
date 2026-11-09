using Catalog.Application.Interfaces;

namespace Catalog.Api.Extensions;

internal static class DatabaseExtensions
{
    internal static async Task InitialiseDatabaseAsync(this WebApplication app, CancellationToken ct = default)
    {
        using var scope = app.Services.CreateScope();

        var dbInitialiser = scope.ServiceProvider
            .GetRequiredService<IApplicationDbContextInitialiser>();

        await dbInitialiser.InitialiseAsync(ct);
        await dbInitialiser.SeedAsync(ct);
    }
}
