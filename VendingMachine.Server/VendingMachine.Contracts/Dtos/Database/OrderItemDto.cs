using CSharpFunctionalExtensions;

namespace VendingMachine.Contracts.Dtos.Database;

public class OrderItemDto : Entity<Guid>
{
    public Guid OrderId { get; init; }
    
    public Guid ProductId { get; init; }
    
    public string BrandTitle { get; init; } = string.Empty;
    
    public string ProductTitle { get; init; } = string.Empty;
    
    public int ProductPrice { get; init; }
    
    public int Quantity { get; init; }
}
        