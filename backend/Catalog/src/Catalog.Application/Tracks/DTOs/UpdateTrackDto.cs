namespace Catalog.Application.Tracks.DTOs;

public record UpdateTrackDto(
    string Name,
    string? Author,
    string? Album,
    byte? Rating
);
