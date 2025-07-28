using VendingMachine.Contracts.Dtos;

namespace VendingMachine.Contracts.Requests.Order;

/// <summary>
/// Запрос на оплату заказа
/// </summary>
/// <param name="Coins">Монеты</param>
public record PaymentRequest(DepositCoin[] Coins);

