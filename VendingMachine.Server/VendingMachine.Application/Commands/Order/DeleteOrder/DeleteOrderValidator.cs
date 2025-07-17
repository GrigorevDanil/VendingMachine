using FluentValidation;
using VendingMachine.Application.Validation;

namespace VendingMachine.Application.Commands.Order.DeleteOrder;

public class DeleteOrderValidator: AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderValidator()
    {
        RuleFor(x => x.OrderId).MustBeValidGuid();
    }
}