using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Dtos;

namespace VendingMachine.Application.Queries.Brand.GetBrands;

public class GetBrandsHandler : IQueryHandler<BrandDto[]>
{
    private readonly IReadDbContext _dbContext;

    public GetBrandsHandler(IReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<BrandDto[]> Handle(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Brands.ToArrayAsync(cancellationToken);
    }
}