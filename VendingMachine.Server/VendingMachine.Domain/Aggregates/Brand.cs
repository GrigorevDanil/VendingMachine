using CSharpFunctionalExtensions;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Domain.Aggregates;

/// <summary> Сущность бренда </summary>
public class Brand : Entity<BrandId>
{
    /// <summary> Список товаров входящих в бренд, с которым можно работать внутри сущности </summary>
    private List<Product> _products = [];
    
    /// <summary> Конструктор для поддержки EF. Не использовать! </summary>
    private Brand(BrandId id):base(id) { }
    
    public Brand(BrandId id, Title title, IEnumerable<Product> products) : base(id)
    {
        Title = title;
        _products = products.ToList();
    }
    
    /// <summary> Название бренда </summary>
    public Title Title { get; }
    
    /// <summary> Список товаров входящих в бренд </summary>
    public IReadOnlyList<Product> Products => _products;
    
    /// <summary>
    /// Добавление товара к бренду
    /// </summary>
    /// <param name="product">Добавляемый товар</param>
    /// <returns></returns>
    public void AddProduct(Product product) => _products.Add(product);
    
    /// <summary>
    /// Удаление товара из бренда
    /// </summary>
    /// <param name="product">Удаляемый товар</param>
    /// <returns></returns>
    public void RemoveProduct(Product product) => _products.Remove(product);
}   
