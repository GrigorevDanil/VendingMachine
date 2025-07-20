using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Dtos.Database;
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
            p => p.Title.ToLower().Contains(query.Title!.ToLower()));
        
        productsQuery = productsQuery.WhereIf(
            query.MinPrice.HasValue,
            p => p.Price >= query.MinPrice!);
        
        var maxPrice = await productsQuery
            .Select(p => p.Price)
            .DefaultIfEmpty() 
            .MaxAsync(cancellationToken);


        var pagedListOptions = new Dictionary<string, object>
        {
            { "maxPrice", maxPrice },
        };

        return await productsQuery
            .ToPagedList(query.Page, query.PageSize, pagedListOptions, cancellationToken);
    }
}