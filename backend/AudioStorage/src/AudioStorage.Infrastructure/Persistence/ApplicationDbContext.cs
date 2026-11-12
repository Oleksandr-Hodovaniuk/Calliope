using AudioStorage.Application.Interfaces;
using AudioStorage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AudioStorage.Infrastructure.Persistence;

internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options),
    IApplicationDbContext
{
    public DbSet<AudioFile> AudioFiles => Set<AudioFile>();

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
    {
        return Database.BeginTransactionAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}
