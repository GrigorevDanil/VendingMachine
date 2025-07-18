using CSharpFunctionalExtensions;

namespace VendingMachine.Application.Dtos;

public class OrderItemDto : Entity<Guid>
{
    public Guid OrderId { get; init; }
    
    public Guid ProductId { get; init; }
    
    public string BrandTitle { get; init; } = string.Empty;
    
    public string ProductTitle { get; init; } = string.Empty;
    
    public decimal ProductPrice { get; init; }
    
    public int Quantity { get; init; }
}
        