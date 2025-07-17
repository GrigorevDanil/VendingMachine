using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;

namespace VendingMachine.Application.Queries.Product.GetProductsWithPagination;

public record GetProductWithPaginationQuery(
    Guid? BrandId,
    string? Title,
    decimal? MinPrice,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;