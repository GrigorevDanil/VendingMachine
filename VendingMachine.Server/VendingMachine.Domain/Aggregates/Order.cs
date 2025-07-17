using CSharpFunctionalExtensions;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Domain.Aggregates;

/// <summary> Сущность заказа </summary>
public class Order : Entity<OrderId>
{
    /// <summary> Список представляющий состав заказа, с которым можно работать внутри сущности </summary>
    private List<OrderItem> _items = [];
    
    /// <summary> Конструктор для поддержки EF. Не использовать! </summary>
    private Order(OrderId id):base(id) { }

    public Order(OrderId id, OrderDate createdAt, TotalAmount totalAmount, IEnumerable<OrderItem> items) : base(id)
    {
        CreatedAt = createdAt;
        TotalAmount = totalAmount;
        _items = items.ToList();
    }

    /// <summary> Дата и время создания заказ </summary>
    public OrderDate CreatedAt { get; }
    
    /// <summary> Итоговая стоимость заказа </summary>
    public TotalAmount TotalAmount { get; private set; }
    
    /// <summary> Список представляющий состав заказа </summary>
    public IReadOnlyList<OrderItem> Items => _items;

    /// <summary>
    /// Добавление элемента заказа в список заказа
    /// </summary>
    /// <param name="item">Добавляемый элемент заказа</param>
    /// <returns></returns>
    public UnitResult<Error> AddItem(OrderItem item)
    {
        var result = TotalAmount.Add(item.ProductPrice.Value * item.Quantity.Value);

        if (result.IsFailure)
            return result.Error;
        
        TotalAmount = result.Value;
        
        _items.Add(item);

        return Result.Success<Error>();
    }
    
    /// <summary>
    /// Получить товар из заказа по id
    /// </summary>
    /// <param name="orderItemId">Идентификатор товара из заказа</param>
    /// <returns></returns>
    public Result<OrderItem, Error> GetOrderItemById(OrderItemId orderItemId)
    {
        var orderItem = _items.FirstOrDefault(i => i.Id == orderItemId);
        if (orderItem is null)
            return Errors.General.NotFound(orderItemId.Value);

        return orderItem;
    }
    
    /// <summary>
    /// Удаляет товар из заказа по id
    /// </summary>
    /// <param name="orderItemId">Идентификатор товара из заказа</param>
    /// <returns></returns>
    public UnitResult<Error> DeleteOrderItemById(OrderItemId orderItemId)
    {
        var orderItem = _items.FirstOrDefault(i => i.Id == orderItemId);
        if (orderItem is null)
            return Errors.General.NotFound(orderItemId.Value);

        var substractResult = TotalAmount.Subtract(orderItem.ProductPrice.Value * orderItem.Quantity.Value);

        if (substractResult.IsFailure)
            return substractResult.Error;
        
        TotalAmount = substractResult.Value;
        
        _items.Remove(orderItem);
        
        return Result.Success<Error>();
    }

    /// <summary>
    /// Обновить количество товар в заказе по id
    /// </summary>
    /// <param name="orderItemId">Идентификатор товара из заказа</param>
    /// <param name="quantity">Количество товара</param>
    /// <returns></returns>
    public UnitResult<Error> UpdateQuantityByOrderItemId(OrderItemId orderItemId, Quantity quantity)
    {
        var orderItem = _items.FirstOrDefault(i => i.Id == orderItemId);
        if (orderItem is null)
            return Errors.General.NotFound(orderItemId.Value);
        
        var subtractResult = TotalAmount.Subtract(orderItem.ProductPrice.Value * orderItem.Quantity.Value);
        if (subtractResult.IsFailure)
            return subtractResult.Error;
        
        TotalAmount = subtractResult.Value;

        orderItem.UpdateQuantity(quantity);
        
        var addResult = TotalAmount.Add(orderItem.ProductPrice.Value * orderItem.Quantity.Value);
        if (addResult.IsFailure)
            return addResult.Error;

        TotalAmount = addResult.Value;
        
        return Result.Success<Error>();
    }
}