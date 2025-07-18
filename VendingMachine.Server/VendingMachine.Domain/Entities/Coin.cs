using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Domain.Entities;

/// <summary> Сущность монеты </summary>
public class Coin : Entity<CoinId>
{
    /// <summary> Конструктор для поддержки EF. Не использовать! </summary>
    private Coin(CoinId id): base(id) { }

    public Coin(CoinId id, Denomination denomination, Stock stock) : base(id)
    {
        Denomination = denomination;
        Stock = stock;
    }

    /// <summary> Номинал монеты </summary>
    public Denomination Denomination { get; private set; }
    
    /// <summary> Количество монет, имеющееся в автомате </summary>
    public Stock Stock { get;private set; }

    /// <summary>
    /// Добавления монет
    /// </summary>
    /// <param name="value">Количество монет</param>
    /// <returns></returns>
    public UnitResult<Error> AddStock(int value)
    {
        var result = Stock.Add(value);
        
        if (result.IsFailure) 
            return result.Error;

        Stock = result.Value;
        
        return UnitResult.Success<Error>();
    }
    
    /// <summary>
    /// Вычитание монет
    /// </summary>
    /// <param name="value">Количество монет</param>
    /// <returns></returns>
    public UnitResult<Error> SubtrackStock(int value)
    {
        var result = Stock.Subtract(value);
        
        if (result.IsFailure) 
            return result.Error;

        Stock = result.Value;
        
        return UnitResult.Success<Error>();
    }
}