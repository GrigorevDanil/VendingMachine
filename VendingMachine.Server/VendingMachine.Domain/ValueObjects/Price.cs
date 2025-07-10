using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class Price : ValueObject
{
    public const string TYPE_NAME = "decimal(18,2)";
    
    private Price(decimal value)
    {
        Value = value;
    }
    public decimal Value { get; }

    public static Result<Price, Error> Of(decimal value)
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