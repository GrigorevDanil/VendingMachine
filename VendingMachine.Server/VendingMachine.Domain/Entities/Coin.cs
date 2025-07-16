using CSharpFunctionalExtensions;
using VendingMachine.Domain.Enums;
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
    /// Обновление информации о монете
    /// </summary>
    /// <param name="updatedCoin">Обновленная монета</param>
    public void UpdateInfo(Coin updatedCoin)
    {
        Denomination = updatedCoin.Denomination;
        Stock = updatedCoin.Stock;
    }
}