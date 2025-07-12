using CSharpFunctionalExtensions;

namespace VendingMachine.Application.Dtos;

public class ProductDto : Entity<Guid>
{
    public Guid BrandId { get; init; }
    
    public string FilePath { get; init; } = string.Empty;
    
    public string Title { get; init; } = string.Empty;
    
    public decimal Price { get; init; }
    
    public int Stock { get; init; }
}