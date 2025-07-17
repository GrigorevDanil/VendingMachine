using VendingMachine.Application.Commands.Order.UpdateOrderItem;

namespace VendingMachine.API.Contracts.Order;

/// <summary>
/// Запрос на обновление количества товара в заказе
/// </summary>
/// <param name="Quantity">Количество товара</param>
public record UpdateOrderItemRequest(int Quantity)
{
    public UpdateOrderItemCommand ToCommand(Guid orderId, Guid orderItemId) => new UpdateOrderItemCommand(orderId, orderItemId, Quantity);
}