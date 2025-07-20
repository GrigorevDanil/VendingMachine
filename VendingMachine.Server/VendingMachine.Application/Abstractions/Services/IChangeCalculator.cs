using CSharpFunctionalExtensions;
using VendingMachine.Application.Dtos;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Abstractions.Services;

public interface IChangeCalculator
{
    bool CanMakeChange(int amount, IEnumerable<Coin> availableCoins);
}