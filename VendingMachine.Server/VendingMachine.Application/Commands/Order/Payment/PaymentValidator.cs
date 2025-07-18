using FluentValidation;
using VendingMachine.Application.Validation;
using VendingMachine.Domain.ValueObjects;

namespace VendingMachine.Application.Commands.Order.Payment;

public class PaymentValidator : AbstractValidator<PaymentCommand>
{
    public PaymentValidator()
    {
        RuleFor(x => x.OrderId).MustBeValidGuid();
        RuleFor(x => x.Coins)
            .MustNotBeEmptyArray()
            .ForEach(itemRule => itemRule.SetValidator(new PaymentCoinValidator()));

    }
}

public class PaymentCoinValidator : AbstractValidator<PaymentCoin>
{
    public PaymentCoinValidator()
    {
        RuleFor(x => x.Denomination).MustBeValueObject(Denomination.Of);
        RuleFor(x => x.Quantity).MustBeValueObject(Quantity.Of);
    }
}