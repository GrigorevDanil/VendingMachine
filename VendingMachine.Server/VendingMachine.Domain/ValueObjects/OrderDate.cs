using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class OrderDate : ValueObject
{
    private OrderDate(DateTime value)
    {
        Value = value;
    }
    public DateTime Value { get; }

    public static Result<OrderDate, Error> Create()
    {
        return new OrderDate(DateTime.UtcNow);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}