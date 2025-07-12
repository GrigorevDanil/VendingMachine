using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class FilePath: ValueObject
{
    private FilePath(string value)
    {
        Value = value;
    }
    public string Value { get;}

    public static Result<FilePath, Error> Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsInvalid(nameof(FilePath));
        }

        return new FilePath(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}