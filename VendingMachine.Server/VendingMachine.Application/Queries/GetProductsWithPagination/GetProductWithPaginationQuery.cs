using VendingMachine.Application.Abstractions;

namespace VendingMachine.Application.Queries.GetProductsWithPagination;

public record GetProductWithPaginationQuery(
    Guid? BrandId,
    string? Title,
    decimal? MinPrice,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;