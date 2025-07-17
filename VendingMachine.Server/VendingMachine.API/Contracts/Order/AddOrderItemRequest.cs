using VendingMachine.Application.Commands.Order.AddOrderItem;

namespace VendingMachine.API.Contracts.Order;

/// <summary>
/// Запрос на добавление товара к заказу
/// </summary>
/// <param name="ProductId">Идентификатор товара</param>
/// <param name="Quantity">Количество товара</param>
public record AddOrderItemRequest(Guid ProductId, int Quantity)
{
    public AddOrderItemCommand ToCommand(Guid orderId) => new (orderId, ProductId, Quantity);
}