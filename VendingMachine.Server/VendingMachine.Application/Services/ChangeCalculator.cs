using VendingMachine.Application.Abstractions.Services;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services;

public class ChangeCalculator : IChangeCalculator
{
    public bool CanMakeChange(int amount, IEnumerable<Coin> availableCoins)
    {
        var denominations = availableCoins
            .Where(coin => coin.Stock.Value > 0)
            .Select(coin => coin.Denomination.Value)
            .OrderByDescending(d => d)
            .ToArray();

        return CanMakeChangeRecursive(amount, denominations, 0);
    }

    private static bool CanMakeChangeRecursive(int remaining, int[] denominations, int index)
    {
        if (remaining == 0) return true;
        
        if (index >= denominations.Length) return false;

        var currentDenomination = denominations[index];
        
        var maxPossible = remaining / currentDenomination;

        for (var count = maxPossible; count >= 0; count--)
        {
            var newRemaining = remaining - count * currentDenomination;
            
            if (CanMakeChangeRecursive(newRemaining, denominations, index + 1))
                return true;
        }

        return false;
    }
    
}