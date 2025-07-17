using FluentValidation;
using VendingMachine.Application.Validation;

namespace VendingMachine.Application.Commands.Order.UpdateOrderItem;

public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemValidator()
    {
        RuleFor(x => x.OrderId).MustBeValidGuid();
        RuleFor(x => x.OrderItemId).MustBeValidGuid();
    }
}