using CSharpFunctionalExtensions;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Domain.Entities;

/// <summary> Сущность элемента заказа </summary>
public class OrderItem : Entity<OrderItemId>
{
    /// <summary> Конструктор для поддержки EF. Не использовать! </summary>
    private OrderItem(OrderItemId id) : base(id) { }

    public OrderItem(Title productTitle, Price productPrice, Quantity quantity, OrderId orderId, ProductId productId)
    {
        ProductTitle = productTitle;
        ProductPrice = productPrice;
        Quantity = quantity;
        OrderId = orderId;
        ProductId = productId;
    }

    /// <summary> Название товара в момент создания заказа </summary>
    public Title ProductTitle { get; private set; }
    
    /// <summary> Стоимость товара в момент создания заказа </summary>
    public Price ProductPrice { get;  private set;}
    
    /// <summary> Количество заказанного товара </summary>
    public Quantity  Quantity { get; private set; }
    
    /// <summary> Идентификатор заказа, к которому принадлежит элемент заказа </summary>
    public OrderId  OrderId { get; }
    
    /// <summary> Идентификатор товара, входящий в заказ </summary>
    public ProductId  ProductId { get; }

    /// <summary>
    /// Обновляет информацию об элементе товара
    /// </summary>
    /// <param name="updatedOrderItem">Обновленный элемент товара</param>
    public void UpdateInfo(OrderItem updatedOrderItem)
    {
        ProductTitle = updatedOrderItem.ProductTitle;
        ProductPrice = updatedOrderItem.ProductPrice;
        Quantity = updatedOrderItem.Quantity;
    }
}