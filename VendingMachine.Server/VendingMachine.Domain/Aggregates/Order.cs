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
    private Order(OrderId id) : base(id) { }

    public Order(OrderDate createdAt, TotalAmount totalAmount, IEnumerable<OrderItem> items)
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
        var result = TotalAmount.Add(item.ProductPrice.Value);

        if (result.IsFailure)
            return result.Error;
        
        TotalAmount = result.Value;
        
        _items.Add(item);

        return Result.Success<Error>();
    }
    
    /// <summary>
    /// Удаление элемента заказа из списка заказа
    /// </summary>
    /// <param name="item">Удаляемый элемент заказа</param>
    /// <returns></returns>
    public UnitResult<Error> RemoveItem(OrderItem item)
    {
        var result = TotalAmount.Substract(item.ProductPrice.Value);

        if (result.IsFailure)
            return result.Error;
        
        TotalAmount = result.Value;
        
        _items.Remove(item);

        return Result.Success<Error>();
    }
    
}