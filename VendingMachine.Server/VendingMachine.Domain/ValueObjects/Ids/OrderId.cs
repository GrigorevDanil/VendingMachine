using CSharpFunctionalExtensions;

namespace VendingMachine.Domain.ValueObjects.Ids;

public class OrderId: ValueObject, IComparable<OrderId>
{
    private OrderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get;  }
        
    public static OrderId Create() => new(Guid.NewGuid());
        
    public static OrderId Empty() => new(Guid.Empty);
        
    public static OrderId Of(Guid id) => new(id);
        
    public int CompareTo(OrderId? id) => Value.CompareTo(id?.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}