using FluentValidation;
using VendingMachine.Application.Validation;

namespace VendingMachine.Application.Commands.Order.DeleteOrderItem;

public class DeleteOrderItemValidator : AbstractValidator<DeleteOrderItemCommand>
{
    public DeleteOrderItemValidator()
    {
        RuleFor(x => x.OrderId).MustBeValidGuid();
        RuleFor(x => x.OrderItemId).MustBeValidGuid();
    }
}