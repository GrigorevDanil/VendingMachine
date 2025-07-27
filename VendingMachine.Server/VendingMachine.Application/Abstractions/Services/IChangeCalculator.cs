using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Abstractions.Services;

public interface IChangeCalculator
{
    bool CanMakeChange(int amount, IEnumerable<Coin> availableCoins);
}