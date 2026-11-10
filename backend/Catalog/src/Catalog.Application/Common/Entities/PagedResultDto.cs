namespace Catalog.Application.Common.Entities;

public record PagedResultDto<T>
(
    IReadOnlyList<T> Items,
    int Page,
    int PageSize,
    int TotalCount
);
