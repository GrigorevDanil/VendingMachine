using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class OrderStatus: ValueObject
{
    public const int MAX_LENGTH = 12;
    
    public static readonly OrderStatus Complete = new("Complete");
    public static readonly OrderStatus AwaitPayment = new("AwaitPayment");
    
    private static readonly OrderStatus[] _types =
    [
        Complete,AwaitPayment
    ];
    
    public string Value { get; }

    private OrderStatus(string value)
    {
        Value = value;
    }
    
    public static Result<OrderStatus, Error> Of(string value)
    {
        var orderStatus = value.Trim().ToUpper();

        if (_types.Any(t => t.Value == orderStatus) == false)
        {
            return Errors.General.ValueIsInvalid(nameof(OrderStatus));
        }

        if (string.IsNullOrEmpty(orderStatus) || orderStatus.Length > MAX_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(OrderStatus));

        return new OrderStatus(orderStatus);
    }
    

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}