using CSharpFunctionalExtensions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Domain.Entities;

/// <summary> Сущность товара (Напитки) </summary>
public class Product : Entity<ProductId>
{
    /// <summary> Конструктор для поддержки EF. Не использовать! </summary>
    private Product(ProductId id) : base(id) { }

    public Product(Title title, Price price, Stock stock, BrandId brandId)
    {
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
}