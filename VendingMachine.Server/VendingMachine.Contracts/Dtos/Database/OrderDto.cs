using CSharpFunctionalExtensions;

namespace VendingMachine.Contracts.Dtos.Database;

public class OrderDto: Entity<Guid>
{
    public string Status { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public int TotalAmount {get; init;}
    
    public OrderItemDto[] Items { get; init; } = [];
}