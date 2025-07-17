using FluentValidation;
using VendingMachine.Application.Validation;
using VendingMachine.Domain.ValueObjects;

namespace VendingMachine.Application.Commands.Order.AddOrderItem;

public class AddOrderItemValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemValidator()
    {
        RuleFor(x => x.OrderId).MustBeValidGuid();
        RuleFor(x => x.ProductId).MustBeValidGuid();
        RuleFor(x => x.Quantity).MustBeValueObject(Quantity.Of);
    }
}