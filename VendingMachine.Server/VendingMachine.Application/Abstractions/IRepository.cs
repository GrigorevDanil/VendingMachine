using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Abstractions;

public interface IRepository<TEntity, TId> where TEntity : class
{
    Task<TId> Add(TEntity entity, CancellationToken cancellationToken = default);
    
    TId Save(TEntity entity, CancellationToken cancellationToken = default);
    
    TId Delete(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<Result<TEntity, Error>> GetById(TId entityId, CancellationToken cancellationToken = default);
}