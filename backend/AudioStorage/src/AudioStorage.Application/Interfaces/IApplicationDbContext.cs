using AudioStorage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AudioStorage.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<AudioFile> AudioFiles { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);
}
