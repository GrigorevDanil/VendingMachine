using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Abstractions.Repositories.Base;

public interface IRepository<TEntity, TId> where TEntity : class
{
    Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    TId Save(TEntity entity);
    
    TId Delete(TEntity entity);
    
    Task<Result<TEntity, Error>> GetByIdAsync(TId entityId, CancellationToken cancellationToken = default);
}