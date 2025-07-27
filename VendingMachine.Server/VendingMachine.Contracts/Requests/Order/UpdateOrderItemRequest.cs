namespace VendingMachine.Contracts.Requests.Order;

/// <summary>
/// Запрос на обновление количества товара в заказе
/// </summary>
/// <param name="Quantity">Количество товара</param>
public record UpdateOrderItemRequest(int Quantity);