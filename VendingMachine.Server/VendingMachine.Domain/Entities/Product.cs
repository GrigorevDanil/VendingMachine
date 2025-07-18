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

    public Product(ProductId id, FilePath filePath, Title title, Price price, Stock stock, BrandId brandId) : base(id)
    {
        FilePath = filePath;
        Title = title;
        Price = price;
        Stock = stock;
        BrandId = brandId;
    }
    
    /// <summary> Путь к картинке </summary>
    public FilePath FilePath { get; private set; }

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
    /// Обновляет информацию о товаре
    /// </summary>
    /// <param name="updatedProduct">Обновленный товар</param>
    public void UpdateInfo(Product updatedProduct)
    {
        FilePath = updatedProduct.FilePath;
        Title = updatedProduct.Title;
        Price = updatedProduct.Price;
        Stock = updatedProduct.Stock;
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

        return Result.Success<Error>();
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

        return Result.Success<Error>();
    }
}