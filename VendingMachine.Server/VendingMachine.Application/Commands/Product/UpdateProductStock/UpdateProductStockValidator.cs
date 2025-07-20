using FluentValidation;
using VendingMachine.Application.Validation;
using VendingMachine.Domain.ValueObjects;

namespace VendingMachine.Application.Commands.Product.UpdateProductStock;

public class UpdateProductStockValidator : AbstractValidator<UpdateProductStockCommand>
{
    public UpdateProductStockValidator()
    {
        RuleFor(x => x.ProductId).MustBeValidGuid();
        RuleFor(x => x.Stock).MustBeValueObject(Stock.Of);
    }
}