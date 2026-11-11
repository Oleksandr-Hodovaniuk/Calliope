namespace Catalog.Application.Tracks.Dtoes;

public record CreateTrackDto
(
    string Name,
    string? Author,
    string? Album,
    byte? Rating,
    Guid? FileId
);
