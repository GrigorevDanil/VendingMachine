using FluentValidation;
using VendingMachine.Application.Validation;
using VendingMachine.Contracts.Dtos;
using VendingMachine.Domain.ValueObjects;

namespace VendingMachine.Application.Validators;

public class PaymentCoinValidator : AbstractValidator<DepositCoin>
{
    public PaymentCoinValidator()
    {
        RuleFor(x => x.Denomination).MustBeValueObject(Denomination.Of);
        RuleFor(x => x.Quantity).MustBeValueObject(Quantity.Of);
    }
}