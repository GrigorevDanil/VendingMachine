using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Dtos;
using VendingMachine.Application.Dtos.Database;

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