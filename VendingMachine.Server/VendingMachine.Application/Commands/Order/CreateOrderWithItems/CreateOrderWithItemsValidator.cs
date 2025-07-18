using FluentValidation;
using VendingMachine.Application.Validation;
using VendingMachine.Domain.ValueObjects;

namespace VendingMachine.Application.Commands.Order.CreateOrderWithItems;

public class CreateOrderWithItemsCommandValidator : AbstractValidator<CreateOrderWithItemsCommand>
{
    public CreateOrderWithItemsCommandValidator()
    {
        RuleFor(x => x.OrderItems)
            .MustNotBeEmptyArray()
            .ForEach(itemRule => itemRule.SetValidator(new CreateOrderWithItemsOrderItemValidator()));
    }
}

public class CreateOrderWithItemsOrderItemValidator : AbstractValidator<CreateOrderWithItemsOrderItem>
{
    public CreateOrderWithItemsOrderItemValidator()
    {
        RuleFor(x => x.ProductId).MustBeValidGuid();
        RuleFor(x => x.Quantity).MustBeValueObject(Quantity.Of);
    }
}