using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Contracts.Dtos.Database;

namespace VendingMachine.Application.Queries.Brand.GetBrands;

public class GetBrandsHandler : IQueryHandler<BrandDto[]>
{
    private const string BRANDS_KEY = "brands";
    
    private readonly IReadDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public GetBrandsHandler(IReadDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }
    
    public async Task<BrandDto[]> Handle(CancellationToken cancellationToken = default) =>
        await _cacheService.GetAsync(
            BRANDS_KEY,
            async () => await _dbContext.Brands.ToArrayAsync(cancellationToken),
            TimeSpan.FromHours(1),
            cancellationToken);
}