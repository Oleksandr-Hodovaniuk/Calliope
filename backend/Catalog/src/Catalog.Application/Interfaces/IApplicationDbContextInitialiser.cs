namespace Catalog.Application.Interfaces;

public interface IApplicationDbContextInitialiser
{
    Task InitialiseAsync(CancellationToken ct = default);
    Task SeedAsync(CancellationToken ct = default);
}
