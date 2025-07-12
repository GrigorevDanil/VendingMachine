using CSharpFunctionalExtensions;

namespace VendingMachine.Domain.ValueObjects.Ids;

public class BrandId: ValueObject, IComparable<BrandId>
{
    private BrandId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get;}
        
    public static BrandId Create() => new(Guid.NewGuid());
        
    public static BrandId Empty() => new(Guid.Empty);
        
    public static BrandId Of(Guid id) => new(id);
        
    public int CompareTo(BrandId? id) => Value.CompareTo(id?.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}