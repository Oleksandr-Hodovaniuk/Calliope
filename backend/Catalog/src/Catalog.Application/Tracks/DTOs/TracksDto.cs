namespace Catalog.Application.Tracks.DTOs;

public record TracksDto(
    IReadOnlyList<TrackDto> Tracks,
    int Page,
    int TotalCount
);