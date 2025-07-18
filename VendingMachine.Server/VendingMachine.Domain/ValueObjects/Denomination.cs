using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class Denomination : ValueObject
{
    public const int MAX_LENGTH = 2;
    
    public static readonly Denomination One = new(1);
    public static readonly Denomination Two = new(2);
    public static readonly Denomination Five = new(5);
    public static readonly Denomination Ten = new(10);
    
    private static readonly Denomination[] _types =
    [
        One,Two,Five,Ten,
    ];
    
    public int Value { get; }

    private Denomination(int value)
    {
        Value = value;
    }
    
    public static Result<Denomination, Error> Of(int value)
    {
        var denomination = value;

        if (_types.Any(t => t.Value == denomination) == false)
        {
            return Errors.General.ValueIsInvalid(nameof(Denomination));
        }

        return new Denomination(denomination);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}