namespace VendingMachine.Contracts.Dtos.Database;

public class ProductDto
{
    public Guid Id { get; init; }
    
    public Guid BrandId { get; init; }
    
    public string ImageUrl { get; init; } = string.Empty;
    
    public string Title { get; init; } = string.Empty;
    
    public int Price { get; init; }
    
    public int Stock { get; init; }
}