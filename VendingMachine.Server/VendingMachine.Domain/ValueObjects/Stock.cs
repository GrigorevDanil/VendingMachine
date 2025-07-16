using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class Stock : ValueObject
{
    private Stock(int value)
    {
        Value = value;
    }
    public int Value { get; }

    public static Result<Stock, Error> Of(int value)
    {
        if (value < 0) 
            return Errors.General.ValueIsInvalid(nameof(Stock));

        return new Stock(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}