using VendingMachine.Application.Commands.Coin.ReplenishBalance;
using VendingMachine.Application.Commands.Order.Payment;
using VendingMachine.Application.Dtos;

namespace VendingMachine.API.Contracts.Coin;

public record ReplenishBalanceRequest(DepositCoin[] Coins)
{
    public ReplenishBalanceCommand ToCommand()
    {
        var coins = Coins.Select(x => 
                new DepositCoin(x.Denomination, x.Quantity))
            .ToArray();
            
        return new ReplenishBalanceCommand(coins);
    }
}