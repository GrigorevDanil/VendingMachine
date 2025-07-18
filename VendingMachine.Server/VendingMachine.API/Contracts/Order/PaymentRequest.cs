using VendingMachine.Application.Commands.Order.Payment;

namespace VendingMachine.API.Contracts.Order;

/// <summary>
/// Запрос на оплату заказа
/// </summary>
/// <param name="OrderId">Идентификатор заказа</param>
/// <param name="Coins">Монеты</param>
public record PaymentRequest(PaymentCoinRequest[] Coins)
{
    public PaymentCommand ToCommand(Guid OrderId)
    {
        var coins = Coins.Select(x => 
                new PaymentCoin(x.Denomination, x.Quantity))
            .ToArray();
            
        return new PaymentCommand(OrderId, coins);
    }
}

public record PaymentCoinRequest(int Denomination, int Quantity);