namespace Catalog.Application.Tracks.Dtoes;

public record TrackDto
(
    Guid Id,
    string Name,
    string? Author,
    string? Album,
    byte? Rating,
    bool HasFile,
    DateTimeOffset CreatedAt
);
