using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Abstractions.Repositories;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;
using VendingMachine.Infrastructure.DbContexts;

namespace VendingMachine.Infrastructure.Repositories;

public class CoinRepository : ICoinRepository
{
    private readonly WriteDbContext _dbContext;

    public CoinRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CoinId> AddAsync(Coin entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Coins.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public CoinId Save(Coin entity)
    {
        _dbContext.Coins.Attach(entity);
        return entity.Id;
    }

    public CoinId Delete(Coin entity)
    {
        _dbContext.Coins.Remove(entity);
        return entity.Id;
    }

    public async Task<Result<Coin, Error>> GetByIdAsync(CoinId entityId, CancellationToken cancellationToken = default)
    {
        var coin = await _dbContext.Coins.FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        if (coin is null) 
            return Errors.General.NotFound(entityId.Value);
        
        return coin;
    }

    public async Task<Result<Coin, Error>> GetByDenominationAsync(Denomination denomination, CancellationToken cancellationToken = default)
    {
        var coin = await _dbContext.Coins.FirstOrDefaultAsync(x => x.Denomination == denomination, cancellationToken);

        if (coin is null) 
            return Errors.General.NotFound();
        
        return coin;
    }

    public async Task<List<Coin>> GetAllByDescAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Coins.OrderByDescending(x => x.Denomination.Value).ToListAsync(cancellationToken);
}