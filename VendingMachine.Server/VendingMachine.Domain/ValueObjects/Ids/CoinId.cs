using CSharpFunctionalExtensions;

namespace VendingMachine.Domain.ValueObjects.Ids;

public class CoinId: ValueObject, IComparable<CoinId>
{
    private CoinId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }
        
    public static CoinId Create() => new(Guid.NewGuid());
        
    public static CoinId Empty() => new(Guid.Empty);
        
    public static CoinId Of(Guid id) => new(id);
        
    public int CompareTo(CoinId? id) => Value.CompareTo(id?.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}