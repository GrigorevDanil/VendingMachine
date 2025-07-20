using CSharpFunctionalExtensions;
using VendingMachine.Domain.Aggregates;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Domain.Entities;

/// <summary> Сущность товара (Напитки) </summary>
public class Product : Entity<ProductId>
{
    /// <summary> Конструктор для поддержки EF. Не использовать! </summary>
    private Product(ProductId id) : base(id) { }

    public Product(ProductId id, ImageUrl imageUrl, Title title, Price price, Stock stock, BrandId brandId) : base(id)
    {
        ImageUrl = imageUrl;
        Title = title;
        Price = price;
        Stock = stock;
        BrandId = brandId;
    }
    
    /// <summary> URL картинки </summary>
    public ImageUrl ImageUrl { get; private set; }

    /// <summary> Название товара </summary>
    public Title Title { get; private set; }
    
    /// <summary> Стоимость товара </summary>
    public Price Price { get; private set; }
    
    /// <summary> Запас товара </summary>
    public Stock Stock { get;private set; }
    
    /// <summary> Идентификатор бренда, к которому принадлежит товар </summary>
    public BrandId BrandId { get; }
    
    /// <summary> Навигационное свойство бренд </summary>
    public Brand Brand { get; private set; }
    
    /// <summary>
    /// Установка количества товара
    /// </summary>
    /// <param name="value">количество</param>
    /// <returns></returns>
    public UnitResult<Error> SetStock(int value)
    {
        var result = Stock.Of(value);

        if (result.IsFailure)
            return result.Error;
        
        Stock = result.Value;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Добавление количества товара
    /// </summary>
    /// <param name="value">Добавляемое количество</param>
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
    /// Вычитание количества товара
    /// </summary>
    /// <param name="value">Вычитаемое количество</param>
    /// <returns></returns>
    public UnitResult<Error> SubstractStock(int value)
    {
        var result = Stock.Subtract(value);

        if (result.IsFailure)
            return result.Error;
        
        Stock = result.Value;

        return UnitResult.Success<Error>();
    }
}