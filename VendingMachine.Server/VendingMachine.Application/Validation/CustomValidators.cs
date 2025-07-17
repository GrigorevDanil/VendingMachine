using CSharpFunctionalExtensions;
using FluentValidation;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Application.Validation;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }
    
    public static IRuleBuilderOptionsConditions<T, Guid> MustBeValidGuid<T>(
        this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) => 
        {
            if (value == Guid.Empty)
            {
                var propertyName = context.PropertyName;
                var displayName = context.DisplayName ?? propertyName;
            
                var error = Errors.General.ValueIsInvalid(displayName);
                context.AddFailure(error.Serialize());
            }
        });
    }
    
}