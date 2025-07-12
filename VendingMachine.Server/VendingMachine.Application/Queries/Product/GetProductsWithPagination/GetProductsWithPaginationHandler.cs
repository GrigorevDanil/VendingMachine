using System.Linq.Expressions;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Extensions;
using VendingMachine.Application.Models;

namespace VendingMachine.Application.Queries.Product.GetProductsWithPagination;

public class GetProductsWithPaginationHandler : IQueryHandler<PagedList<ProductDto>,GetProductWithPaginationQuery>
{
    private readonly IReadDbContext _dbContext;

    public GetProductsWithPaginationHandler(IReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedList<ProductDto>> Handle(GetProductWithPaginationQuery query, CancellationToken cancellationToken = default)
    {
        var productsQuery = _dbContext.Products;

        Expression<Func<ProductDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "title" => (product) => product.Title,
            "price" => (product) => product.Price,
            _ => (product) => product.Id
        };

        productsQuery = query.SortDirection?.ToLower() == "desc"
            ? productsQuery.OrderByDescending(keySelector)
            : productsQuery.OrderBy(keySelector);
        
        productsQuery = productsQuery.WhereIf(
            query.BrandId.HasValue,
            p => p.BrandId == query.BrandId);

        productsQuery = productsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Title),
            p => p.Title.Contains(query.Title!));
        
        productsQuery = productsQuery.WhereIf(
            query.MinPrice.HasValue,
            p => p.Price >= query.MinPrice!);

        return await productsQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}