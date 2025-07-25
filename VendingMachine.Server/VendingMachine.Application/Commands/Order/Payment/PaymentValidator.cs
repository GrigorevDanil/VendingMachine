﻿using FluentValidation;
using VendingMachine.Application.Validation;
using VendingMachine.Application.Validators;
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

