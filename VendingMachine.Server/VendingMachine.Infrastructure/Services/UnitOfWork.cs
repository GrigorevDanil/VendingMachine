using VendingMachine.Application.Abstractions;
using VendingMachine.Infrastructure.DbContexts;

namespace VendingMachine.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly WriteDbContext _dbContext;

    public UnitOfWork(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => 
        await _dbContext.SaveChangesAsync(cancellationToken);
}