using VendingMachine.Application.Commands.Order.CreateOrderWithItems;

namespace VendingMachine.API.Contracts.Order;

/// <summary>
/// Запрос на создание заказа с товарами
/// </summary>
/// <param name="OrderItems">Товар из заказа</param>
public record CreateOrderWithItemsRequest(CreateOrderWithItemsOrderItemRequest[] OrderItems)
{
    public CreateOrderWithItemsCommand ToCommand()
    {
        var orderItems = OrderItems.Select(x => 
                new CreateOrderWithItemsOrderItem(x.ProductId, x.Quantity))
            .ToArray();
            
        return new CreateOrderWithItemsCommand(orderItems);
    }
}

/// <summary>
/// Запрос на создание товара из заказа
/// </summary>
/// <param name="ProductId">Идентификатор товара</param>
/// <param name="Quantity">Количество</param>
public record CreateOrderWithItemsOrderItemRequest(Guid ProductId, int Quantity);