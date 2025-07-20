using VendingMachine.Application.Commands.Order.Payment;
using VendingMachine.Application.Dtos;

namespace VendingMachine.API.Contracts.Order;

/// <summary>
/// Запрос на оплату заказа
/// </summary>
/// <param name="Coins">Монеты</param>
public record PaymentRequest(DepositCoin[] Coins)
{
    public PaymentCommand ToCommand(Guid OrderId)
    {
        var coins = Coins.Select(x => 
                new DepositCoin(x.Denomination, x.Quantity))
            .ToArray();
            
        return new PaymentCommand(OrderId, coins);
    }
}

