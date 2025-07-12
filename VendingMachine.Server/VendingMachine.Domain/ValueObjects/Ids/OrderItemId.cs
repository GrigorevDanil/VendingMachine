using CSharpFunctionalExtensions;

namespace VendingMachine.Domain.ValueObjects.Ids;

public class OrderItemId: ValueObject, IComparable<OrderItemId>
{
    private OrderItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get;  }
        
    public static OrderItemId Create() => new(Guid.NewGuid());
        
    public static OrderItemId Empty() => new(Guid.Empty);
        
    public static OrderItemId Of(Guid id) => new(id);
        
    public int CompareTo(OrderItemId? id) => Value.CompareTo(id?.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}