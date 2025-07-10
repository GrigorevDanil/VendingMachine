using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class TotalAmount : ValueObject
{
    public const string TYPE_NAME = "decimal(18,2)";
    
    private TotalAmount(decimal value)
    {
        Value = value;
    }
    public decimal Value { get; private set; }

    public static Result<TotalAmount, Error> Of(decimal value)
    {
        if (value <= 0) 
            return Errors.General.ValueIsInvalid(nameof(TotalAmount));

        return new TotalAmount(value);
    }

    public Result<TotalAmount, Error> Add(decimal addedValue)
    {
        if (addedValue <= 0) 
            return Errors.General.ValueIsInvalid(nameof(TotalAmount));
        
        return new TotalAmount(Value + addedValue);
    }
    
    public Result<TotalAmount, Error> Substract(decimal substractedValue)
    {
        if (substractedValue <= 0) 
            return Errors.General.ValueIsInvalid(nameof(TotalAmount));
        
        return new TotalAmount(Value - substractedValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}