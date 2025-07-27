namespace VendingMachine.Contracts.Requests.Order;

/// <summary>
/// Запрос на создание заказа с товарами
/// </summary>
/// <param name="OrderItems">Товар из заказа</param>
public record CreateOrderWithItemsRequest(CreateOrderWithItemsOrderItemRequest[] OrderItems);

/// <summary>
/// Запрос на создание товара из заказа
/// </summary>
/// <param name="ProductId">Идентификатор товара</param>
/// <param name="Quantity">Количество</param>
public record CreateOrderWithItemsOrderItemRequest(Guid ProductId, int Quantity);