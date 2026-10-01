namespace Catalog.Application.Tracks.DTOs;

public record TrackDto(
    Guid Id,
    string Name,
    string? Author,
    string? Album,
    byte? Rating,
    bool HasFile,
    DateTime CreatedAt
);
