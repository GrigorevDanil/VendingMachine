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

    public static Result<OrderDate, Error> Of(DateTime value)
    {
        if (value != DateTime.UtcNow) 
            return Errors.General.ValueIsInvalid(nameof(OrderDate));
        
        return new OrderDate(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}