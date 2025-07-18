using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;
using VendingMachine.Infrastructure.DbContexts;

namespace VendingMachine.Infrastructure.Repositories;

public class ProductRepository : IRepository<Product, ProductId>
{
    private readonly WriteDbContext _dbContext;

    public ProductRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductId> AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public ProductId Save(Product entity)
    {
        _dbContext.Products.Attach(entity);
        return entity.Id;
    }

    public ProductId Delete(Product entity)
    {
        _dbContext.Products.Remove(entity);
        return entity.Id;
    }

    public async Task<Result<Product, Error>> GetByIdAsync(ProductId entityId, CancellationToken cancellationToken = default)
    {
        var product = await _dbContext.Products.Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        if (product is null) 
            return Errors.General.NotFound(entityId.Value);
        
        return product;
    }
}
