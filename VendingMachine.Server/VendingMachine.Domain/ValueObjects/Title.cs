﻿using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;

namespace VendingMachine.Domain.ValueObjects;

public class Title : ValueObject
{
    public const int MAX_LENGTH = 255;

    private Title(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static Result<Title, Error> Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH) 
            return Errors.General.ValueIsInvalid(nameof(Title));

        return new Title(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}