using CSharpFunctionalExtensions;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Abstractions.Repositories;

public interface ICoinRepository : IRepository<Coin, CoinId>
{
    Task<Result<Coin, Error>> GetByDenominationAsync(Denomination denomination, CancellationToken cancellationToken = default);
    
    Task<List<Coin>> GetAllByDescAsync(CancellationToken cancellationToken = default);
}