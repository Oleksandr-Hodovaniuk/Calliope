using AudioStorage.Application.Interfaces;
using AudioStorage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AudioStorage.Infrastructure.Persistence;

internal class ApplicationDbContextInitialiser(ApplicationDbContext _context)
    : IApplicationDbContextInitialiser
{
    public async Task InitialiseAsync(CancellationToken ct = default)
    {
        if (_context.Database.IsNpgsql())
        {
            await _context.Database.MigrateAsync(ct);
        }
    }

    public async Task SeedAsync(CancellationToken ct = default)
    {
        var audioFile1 = new AudioFile
        {
            Id = Guid.Parse("01a1ca88-bd64-7b39-af99-1e2d86c7dc9b"),
            Size = 123,
            Duration = TimeSpan.FromSeconds(300)
        };

        var audioFile2 = new AudioFile
        {
            Id = Guid.Parse("01a1ca8e-cba4-7935-99b0-f57934807b34"),
            Size = 145,
            Duration = TimeSpan.FromSeconds(360)
        };

        var audioFile3 = new AudioFile
        {
            Id = Guid.Parse("01a1ca8e-f0bf-7da4-81ab-e3c5b3498158"),
            Size = 98,
            Duration = TimeSpan.FromSeconds(279)
        };

        _context.AddRange(audioFile1, audioFile2, audioFile3);
        await _context.SaveChangesAsync(ct);
    }
}
