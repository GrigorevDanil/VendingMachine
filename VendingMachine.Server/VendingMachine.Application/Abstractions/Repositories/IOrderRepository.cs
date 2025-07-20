using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Domain.Aggregates;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Abstractions.Repositories;

public interface IOrderRepository : IRepository<Order,OrderId>
{
    Task<List<Order>> GetAllUnpaidAsync(CancellationToken cancellationToken = default);
}