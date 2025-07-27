namespace VendingMachine.Contracts.Requests.Order;

/// <summary>
/// Запрос на добавление товара к заказу
/// </summary>
/// <param name="ProductId">Идентификатор товара</param>
/// <param name="Quantity">Количество товара</param>
public record AddOrderItemRequest(Guid ProductId, int Quantity);