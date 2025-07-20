using FluentValidation;
using VendingMachine.Application.Validation;
using VendingMachine.Application.Validators;

namespace VendingMachine.Application.Commands.Coin.ReplenishBalance;

public class ReplenishBalanceValidator : AbstractValidator<ReplenishBalanceCommand>
{
    public ReplenishBalanceValidator()
    {
        RuleFor(x => x.Coins)
            .MustNotBeEmptyArray()
            .ForEach(itemRule => itemRule.SetValidator(new PaymentCoinValidator()));
    }
}