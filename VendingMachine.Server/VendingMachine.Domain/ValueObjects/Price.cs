using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class Price : ValueObject
{
    private Price(int value)
    {
        Value = value;
    }
    public int Value { get;}

    public static Result<Price, Error> Of(int value)
    {
        if (value <= 0) 
            return Errors.General.ValueIsInvalid(nameof(Price));

        return new Price(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}