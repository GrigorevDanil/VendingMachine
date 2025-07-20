using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class TotalAmount : ValueObject
{
    private TotalAmount(int value)
    {
        Value = value;
    }
    public int Value { get; private set; }

    public static Result<TotalAmount, Error> Of(int value)
    {
        if (value < 0) 
            return Errors.General.ValueIsInvalid(nameof(TotalAmount));

        return new TotalAmount(value);
    }

    public Result<TotalAmount, Error> Add(int addedValue)
    {
        if (addedValue <= 0) 
            return Errors.General.ValueIsInvalid(nameof(TotalAmount));
        
        return new TotalAmount(Value + addedValue);
    }
    
    public Result<TotalAmount, Error> Subtract(int subtractedValue)
    {
        if (subtractedValue <= 0) 
            return Errors.General.ValueIsInvalid(nameof(TotalAmount));
        
        return new TotalAmount(Value - subtractedValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}