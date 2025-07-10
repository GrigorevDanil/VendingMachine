using CSharpFunctionalExtensions;

namespace VendingMachine.Domain.ValueObjects.Ids;

public class ProductId: ValueObject, IComparable<ProductId>
{
    private ProductId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }
        
    public static ProductId Create() => new(Guid.NewGuid());
        
    public static ProductId Empty() => new(Guid.Empty);
        
    public static ProductId Of(Guid id) => new(id);
        
    public int CompareTo(ProductId? id) => Value.CompareTo(id?.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}