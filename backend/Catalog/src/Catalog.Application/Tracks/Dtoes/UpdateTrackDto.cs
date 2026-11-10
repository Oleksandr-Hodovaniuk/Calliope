namespace Catalog.Application.Tracks.Dtoes;

public record UpdateTrackDto
(
    string Name,
    string? Author,
    string? Album,
    byte? Rating,
    bool HasFile
);
