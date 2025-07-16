using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Abstractions;

public interface IRepository<TEntity, TId> where TEntity : class
{
    Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    TId Save(TEntity entity, CancellationToken cancellationToken = default);
    
    TId Delete(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<Result<TEntity, Error>> GetByIdAsync(TId entityId, CancellationToken cancellationToken = default);
}