namespace Catalog.Application.Tracks.Dtoes;

public record PagedResultDto<T>
(
    IReadOnlyList<T> Items,
    int Page,
    int PageSize,
    int TotalCount
);
