using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Repositories;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Domain.Aggregates;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;
using VendingMachine.Infrastructure.DbContexts;

namespace VendingMachine.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly WriteDbContext _dbContext;

    public OrderRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<OrderId> AddAsync(Order entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Orders.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public OrderId Save(Order entity)
    {
        _dbContext.Orders.Attach(entity);
        return entity.Id;
    }

    public OrderId Delete(Order entity)
    {
        _dbContext.Orders.Remove(entity);
        return entity.Id;
    }

    public async Task<Result<Order, Error>> GetByIdAsync(OrderId entityId, CancellationToken cancellationToken = default)
    {
        var order = await _dbContext.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        if (order is null) 
            return Errors.General.NotFound(entityId.Value);
        
        return order;
    }

    public async Task<List<Order>> GetAllUnpaidAsync(CancellationToken cancellationToken = default)
    {
        var orders = await _dbContext.Orders.Where(x => x.Status == OrderStatus.AwaitPayment).ToListAsync(cancellationToken);
        
        return orders;
    }
}